namespace node_delegate.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Docker.DotNet;

    using Microsoft.Extensions.Options;

    using node_delegate.Models;
    using node_delegate.Options;

    public class ProxyService : IProxyService
    {
        public ProxyService(IOptions<DockerClientOptions> options, IMapper mapper)
        {
            this.Configuration = options.Value.Configuration();
            this.Client = this.Configuration.CreateClient(options.Value.Version);
            this.Mapper = mapper;
        }

        protected DockerClient Client { get; }

        protected DockerClientConfiguration Configuration { get; }

        protected IMapper Mapper { get; }

        public async Task<ContainerResult> CreateContainer(ContainerConfig parameters)
        {
            var created = await this.Client.Containers.CreateContainerAsync(parameters);
            return this.Mapper.Map<ContainerResult>(created);
        }

        public async Task<Container> GetContainerById(string id)
        {
            var containers = await this.GetContainers(new ContainerFilter());
            return containers.FirstOrDefault(c => c.ID == id);
        }

        public async Task<IList<Container>> GetContainers(ContainerFilter parameters)
        {
            var containers = await this.Client.Containers.ListContainersAsync(parameters);
            return this.Mapper.Map<IList<Container>>(containers);
        }
    }
}
