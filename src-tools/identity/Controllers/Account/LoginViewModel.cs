namespace identity.Controllers.Account
{
    public class LoginViewModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public bool Persist { get; set; }

        public string ReturnUrl { get; set; }
    }
}
