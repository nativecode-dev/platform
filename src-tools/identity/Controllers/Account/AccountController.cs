namespace identity.Controllers.Account
{
    using System.Threading.Tasks;
    using IdentityServer4.Events;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NativeCode.Node.Identity.Entities;

    [Route("[controller]")]
    public class AccountController : Controller
    {
        public AccountController(UserManager<User> users, SignInManager<User> signin, IEventService events, IIdentityServerInteractionService interaction,
            ILogger<AccountController> logger)
        {
            this.Events = events;
            this.Interaction = interaction;
            this.Logger = logger;
            this.Signin = signin;
            this.Users = users;
        }

        protected IEventService Events { get; }

        protected IIdentityServerInteractionService Interaction { get; }

        protected ILogger<AccountController> Logger { get; }

        protected SignInManager<User> Signin { get; }

        protected UserManager<User> Users { get; }

        [AllowAnonymous]
        [HttpGet("AccessDenied")]
        public IActionResult Denied([FromQuery] string returnUrl)
        {
            var model = new DeniedViewModel
            {
                ReturnUrl = returnUrl,
            };

            return this.View(model);
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };

            return this.View(model);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model, string button)
        {
            if (button != "login")
            {
                var context = await this.Interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    await this.Interaction.GrantConsentAsync(context, ConsentResponse.Denied);
                    return this.Redirect(model.ReturnUrl);
                }

                return this.Redirect("~/");
            }

            var user = new User
            {
                UserName = model.Login,
            };

            this.Logger.LogInformation($"Attemping login: {model.Login}");

            var result = await this.Signin.PasswordSignInAsync(user, model.Password, model.Persist, true);

            this.Logger.LogInformation("{@result}", new
            {
                result.IsLockedOut,
                result.IsNotAllowed,
                result.RequiresTwoFactor,
                result.Succeeded,
            });

            if (result.Succeeded)
            {
                var found = await this.Users.FindByEmailAsync(model.Login);
                await this.Events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));
                if (this.Interaction.IsValidReturnUrl(model.ReturnUrl) || this.Url.IsLocalUrl(model.ReturnUrl))
                {
                    return this.Redirect(model.ReturnUrl);
                }

                return this.Redirect("~/");
            }

            await this.Events.RaiseAsync(new UserLoginFailureEvent(model.Login, "invalid credentials"));

            this.ModelState.AddModelError(string.Empty, "Invalid credentials.");

            return this.Redirect(model.ReturnUrl);
        }
    }
}
