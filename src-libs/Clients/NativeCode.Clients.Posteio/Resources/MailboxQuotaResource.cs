namespace NativeCode.Clients.Posteio.Resources
{
    using System.Globalization;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Requests;
    using Responses;
    using RestSharp;

    public class MailboxQuotaResource : ClientResource<MailboxQuota, UpdateQuota, UpdateQuota>
    {
        private const string QuotaUrlFormat = "boxes/{0}/quota";

        internal MailboxQuotaResource(RestClient client)
            : base(client)
        {
        }

        public Task<MailboxQuota> Get([NotNull] string email)
        {
            return this.GetResource(string.Format(CultureInfo.CurrentCulture, QuotaUrlFormat, email));
        }

        public Task<bool> Update([NotNull] UpdateQuota quota)
        {
            return this.UpdateResource(string.Format(CultureInfo.CurrentCulture, QuotaUrlFormat, quota.Email), quota);
        }
    }
}
