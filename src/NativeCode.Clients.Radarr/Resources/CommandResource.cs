namespace NativeCode.Clients.Radarr.Resources
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Serialization;
    using Requests.Commands;
    using Responses;
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

        public Task<Command> Get(int request)
        {
            return this.GetSingle<Command>($"command/{request}");
        }

        public Task<Command> Run<TOptions>(TOptions options)
            where TOptions : CommandOptions
        {
            return this.PostResponse<TOptions, Command>("command", options);
        }
    }
}