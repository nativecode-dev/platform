namespace identity.Controllers.Error
{
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    public class ErrorController : Controller
    {
        public ErrorController(IIdentityServerInteractionService interaction)
        {
            this.Interaction = interaction;
        }

        protected IIdentityServerInteractionService Interaction { get; }

        [HttpGet("error")]
        public async Task<IActionResult> Error(string errorId)
        {
            var error = await this.Interaction.GetErrorContextAsync(errorId);

            if (error != null)
            {
                var model = new ErrorViewModel
                {
                    Error = error,
                };

                return this.View(model);
            }

            return this.Redirect("~/");
        }
    }
}
