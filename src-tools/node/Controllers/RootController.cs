namespace node.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using NativeCode.Node.Core.Options;

    [Route("")]
    [ApiController]
    public class RootController : ControllerBase
    {
        public RootController(IOptions<NodeOptions> options)
        {
            this.Options = options.Value;
        }

        protected NodeOptions Options { get; }

        [HttpGet("options")]
        public IActionResult Get()
        {
            return new JsonResult(this.Options);
        }
    }
}
