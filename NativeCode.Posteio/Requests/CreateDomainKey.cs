namespace NativeCode.Posteio.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDomainKey
    {
        [Required]
        public string Name { get; set; }
    }
}
