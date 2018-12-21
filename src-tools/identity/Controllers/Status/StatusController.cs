namespace identity.Controllers.Status
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Route("[controller]")]
    public class StatusController : Controller
    {
        public StatusController(IOptions<AppOptions> options)
        {
            this.Options = options.Value;
        }

        protected AppOptions Options { get; }

        [HttpGet("info")]
        public IActionResult Info()
        {
            return this.View(new StatusInfoViewModel()
            {
                MachineName = Environment.MachineName,
                Options = this.Options,
            });
        }
    }
}
