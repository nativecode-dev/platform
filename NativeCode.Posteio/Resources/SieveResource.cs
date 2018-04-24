namespace NativeCode.Posteio.Resources
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using NativeCode.Posteio.Requests;
    using NativeCode.Posteio.Responses;

    using RestSharp;

    public class SieveResource : ClientResource<SieveScript, CreateSieve, UpdateSieve>
    {
        private const string SieveUrlFormat = "boxes/{0}/sieve";

        internal SieveResource(RestClient client)
            : base(client)
        {
        }

        public Task<SieveScript> Get([NotNull] string email)
        {
            return this.GetResource(string.Format(SieveUrlFormat, email));
        }

        public Task<bool> Update([NotNull] UpdateSieve sieve)
        {
            return this.UpdateResource(string.Format(SieveUrlFormat, sieve.Email), sieve);
        }
    }
}
