namespace NativeCode.Node.Identity.SeedModels
{
    using System;

    public class UserInfo
    {
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public Guid Id { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
