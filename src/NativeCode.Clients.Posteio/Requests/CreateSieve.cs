namespace NativeCode.Clients.Posteio.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSieve
    {
        [Required]
        public string Email { get; set; }

        public string Script { get; set; }
    }
}
