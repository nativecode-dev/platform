namespace NativeCode.Posteio.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateMailbox
    {
        public bool Disabled { get; set; }

        [Required]
        public string Email { get; set; }

        public string Name { get; set; }

        public string PasswordPlaintext { get; set; }

        public bool SuperAdmin { get; set; }
    }
}
