namespace identity.Controllers.Default
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    public class DefaultController : Controller
    {
        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
