namespace NativeCode.Clients.Radarr.Resources
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NativeCode.Clients.Radarr.Responses;
    using NativeCode.Clients.Radarr.Responses.Commands;
    using NativeCode.Core;

    using RestSharp;

    public class CommandResource : ResourceBase, IResourceLookup<int, Command>
    {
        public CommandResource(IRestClient client, IObjectSerializer serializer)
            : base(client, serializer)
        {
        }

        public Task<IEnumerable<Command>> All()
        {
            return this.GetCollection<Command>("command");
        }

        public Task Execute<TOptions>(CommandKind command, TOptions options)
            where TOptions : CommandOptions
        {
            return this.Post($"command/{command}", options);
        }

        public Task<Command> Get(int request)
        {
            return this.GetSingle<Command>($"command/{request}");
        }
    }
}
