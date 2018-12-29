namespace NativeCode.Clients.Posteio.Requests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateMailbox
    {
        public bool Disabled { get; set; }

        [Required]
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        public string PasswordPlaintext { get; set; }

        public IEnumerable<string> RedirectTo { get; set; }

        public bool SuperAdmin { get; set; }
    }
}
