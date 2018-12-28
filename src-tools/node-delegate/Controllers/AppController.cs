namespace node_delegate.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        public AppController(IProxyService proxy)
        {
            this.Proxy = proxy;
        }

        protected IProxyService Proxy { get; }

        [HttpGet("{name}")]
        public Task Get(string name)
        {
            return Task.CompletedTask;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContainerConfig configuration)
        {
            var container = await this.Proxy.CreateContainer(configuration);
            return this.Ok(container);
        }
    }
}
