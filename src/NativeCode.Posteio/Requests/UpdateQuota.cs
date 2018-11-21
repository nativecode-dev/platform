namespace NativeCode.Posteio.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateQuota
    {
        [Required]
        public string Email { get; set; }

        public int QuotaCount { get; set; }

        public int QuotaSize { get; set; }
    }
}
