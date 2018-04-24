namespace NativeCode.Sync.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using NativeCode.Sync.Models.Default;

    [Route("")]
    [Produces("application/json")]
    public class DefaultController : Controller
    {
        [HttpGet("version")]
        public AppVersionInfo Get()
        {
            return new AppVersionInfo();
        }
    }
}
