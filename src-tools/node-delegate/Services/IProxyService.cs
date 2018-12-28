namespace node_delegate.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using node_delegate.Models;

    public interface IProxyService
    {
        Task<ContainerResult> CreateContainer([NotNull] ContainerConfig parameters);

        Task<Container> GetContainerById([NotNull] string id);

        Task<IList<Container>> GetContainers([NotNull] ContainerFilter parameters);
    }
}
