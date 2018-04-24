namespace NativeCode.Posteio.Resources
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using NativeCode.Posteio.Requests;
    using NativeCode.Posteio.Responses;

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
            return this.GetResource(string.Format(QuotaUrlFormat, email));
        }

        public Task<bool> Update([NotNull] UpdateQuota quota)
        {
            return this.UpdateResource(string.Format(QuotaUrlFormat, quota.Email), quota);
        }
    }
}
