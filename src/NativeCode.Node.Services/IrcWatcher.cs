namespace NativeCode.Node.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Extensions;
    using Core.Messaging;
    using Core.Services;
    using IrcDotNet;
    using Messages;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Nito.AsyncEx;

    public class IrcWatcher : HostedService<IrcWatcherOptions>
    {
        private const string StripPattern = @"[\x02\x1F\x0F\x16]|\x03(\d\d?(,\d\d?)?)?";

        private static readonly IEnumerable<string> MovieCategories = new List<string>
        {
            "Blu-Ray",
            "Movie Boxsets",
            "Movies",
        };

        private static readonly IEnumerable<string> ShowCategories = new List<string>
        {
            "HD Boxsets",
            "TV Boxsets",
            "TV-HD",
        };

        private static readonly Regex AnnouncePattern =
            new Regex(
                @"(New Torrent|Size|Category|Uploader|Link):\s+\((?:\s+)([\w\s\.\-\:\/\?\=\[\]\{\}<>\+]+)(?:\s+)\)");

        private readonly IDictionary<string, Action<string, IrcRelease>> propertyMap;

        public IrcWatcher(IOptions<IrcWatcherOptions> options, ILogger<IrcWatcher> logger,
            IQueueManager queue, IMapper mapper, IDistributedCache cache) : base(options)
        {
            this.Cache = cache;
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

        protected IDistributedCache Cache { get; }

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
            var username = this.GetUserName();

            var registration = new IrcUserRegistrationInfo
            {
                NickName = username,
                RealName = username,
                UserName = username,
            };

            this.Client.Connect(this.Options.Host, this.Options.UseSsl, registration);
            this.Logger.LogInformation($"Connected to {this.Options.Host} as {username}");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.Client.Disconnect();
            this.Logger.LogInformation($"Disconnected from {this.Options.Host}");
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
            AsyncContext.Run(() => this.HandleMessage(e.Text));
        }

        private static string Strip(string original)
        {
            return Regex.Replace(original, StripPattern, string.Empty);
        }

        private async Task HandleMessage(string message)
        {
            var stripped = Strip(message);
            var matches = AnnouncePattern.Matches(stripped);

            var release = new IrcRelease();

            foreach (Match match in matches)
            {
                var property = match.Groups[1]
                    .Value.Trim();

                var value = match.Groups[2]
                    .Value.Trim();

                var map = this.propertyMap[property];
                map(value, release);
            }

            if (string.IsNullOrWhiteSpace(release.Name))
            {
                this.Logger.LogError($"Found non-parsable name: {release.Name} {stripped}");

                return;
            }

            var cached = await this.Cache.GetAsync(release.Link);

            if (cached != null)
            {
                return;
            }

            this.Cache.Set(release.Link, release.ToJsonBytes());

            if (MovieCategories.Contains(release.Category))
            {
                await this.Movies.Publish(this.Mapper.Map<MovieRelease>(release));
            }
            else if (ShowCategories.Contains(release.Category))
            {
                await this.Shows.Publish(this.Mapper.Map<SeriesRelease>(release));
            }

            this.Logger.LogInformation($"Announced: {release.Name} [{release.Category}] {release.Link}");
        }

        private string GetUserName()
        {
            var process = Process.GetCurrentProcess();

            // NOTE: In a Docker container, the PID is always 1 so we don't
            // want to use the PID in the name.
            if (process.Id == 1)
            {
                return $"{this.Options.UserName}-{Environment.MachineName}";
            }

            return $"{this.Options.UserName}-{process.Id}";
        }
    }
}
