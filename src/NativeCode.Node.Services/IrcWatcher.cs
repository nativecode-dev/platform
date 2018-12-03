namespace NativeCode.Node.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Messaging;
    using Core.Services;
    using IrcDotNet;
    using Messages;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class IrcWatcher : HostedService<IrcWatchOptions>
    {
        private const string StripPattern = @"[\x02\x1F\x0F\x16]|\x03(\d\d?(,\d\d?)?)?";

        private static readonly IEnumerable<string> MovieCategories = new List<string>
        {
            "Blu-Ray",
            "DVD",
            "Movies",
        };

        private static readonly IEnumerable<string> ShowCategories = new List<string>
        {
            "HD-Boxsets",
            "TV-HD",
            "TV-SD",
        };

        private static readonly Regex AnnouncePattern =
            new Regex(
                @"(New Torrent|Size|Category|Uploader|Link):\s+\((?:\s+)([\w\s\.\-\:\/\?\=\[\]\{\}<>]+)(?:\s+)\)");

        private readonly IDictionary<string, Action<string, IrcRelease>> propertyMap;

        public IrcWatcher(IOptions<IrcWatchOptions> options, ILogger<IrcWatcher> logger,
            IQueueManager queue, IMapper mapper) : base(options)
        {
            this.Client = new StandardIrcClient();
            this.Client.Connected += this.ClientOnConnected;
            this.Logger = logger;
            this.Mapper = mapper;
            this.Movies = queue.GetOutgoingQueue<MovieRelease>();
            this.Shows = queue.GetOutgoingQueue<SeriesRelease>();

            this.propertyMap = new Dictionary<string, Action<string, IrcRelease>>
            {
                {"New Torrent", (property, release) => release.Name = property},
                {"Size", (property, release) => release.Size = property},
                {"Category", (property, release) => release.Category = property},
                {"Uploader", (property, release) => release.Uploader = property},
                {
                    "Link",
                    (property, release) =>
                        release.Link = $"{property}&type=rss&secret_key={this.Options.XspeedsSecret}".Replace("details.php", "download.php")
                },
            };
        }

        protected StandardIrcClient Client { get; }

        protected ILogger<IrcWatcher> Logger { get; }

        protected IMapper Mapper { get; }

        protected IQueueTopic<MovieRelease> Movies { get; }

        protected IQueueTopic<SeriesRelease> Shows { get; }

        protected override void ReleaseManaged()
        {
            this.Client.Dispose();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var registration = new IrcUserRegistrationInfo
            {
                NickName = this.Options.NickName,
                RealName = this.Options.RealName,
                UserName = this.Options.UserName,
            };

            this.Client.Connect(this.Options.Server, this.Options.UseSsl, registration);
            this.Logger.LogInformation($"Connected to {this.Options.Server}");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.Client.Disconnect();
            this.Logger.LogInformation($"Disconnected from {this.Options.Server}");
            return Task.CompletedTask;
        }

        private void ClientOnConnected(object sender, EventArgs e)
        {
            this.Client.LocalUser.JoinedChannel += this.LocalUserOnJoinedChannel;
        }

        private void LocalUserOnJoinedChannel(object sender, IrcChannelEventArgs e)
        {
            e.Channel.MessageReceived += this.ChannelOnMessageReceived;
        }

        private void ChannelOnMessageReceived(object sender, IrcMessageEventArgs e)
        {
            var stripped = Strip(e.Text);
            var matches = AnnouncePattern.Matches(stripped);

            var release = new IrcRelease();

            foreach (Match match in matches)
            {
                var property = match.Groups[1]
                    .Value.Trim();

                var value = match.Groups[2]
                    .Value.Trim();

                var action = this.propertyMap[property];
                action(value, release);
            }

            if (MovieCategories.Contains(release.Category))
            {
                this.Movies.Publish(this.Mapper.Map<MovieRelease>(release));
            }
            else if (ShowCategories.Contains(release.Category))
            {
                this.Shows.Publish(this.Mapper.Map<SeriesRelease>(release));
            }

            this.Logger.LogInformation(JsonConvert.SerializeObject(release));
        }

        private static string Strip(string original)
        {
            return Regex.Replace(original, StripPattern, string.Empty);
        }
    }
}
