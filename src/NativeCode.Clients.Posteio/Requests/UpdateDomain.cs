namespace NativeCode.Clients.Posteio.Requests
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateDomain
    {
        public bool DomainBin { get; set; }

        public string DomainBinAddress { get; set; }

        public bool Forward { get; set; }

        public string ForwardDomain { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
