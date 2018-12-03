namespace NativeCode.Clients.Posteio.Resources
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Requests;
    using Responses;
    using RestSharp;

    public class MailboxResource : ClientResource<Mailbox, CreateMailbox, UpdateMailbox>
    {
        private const string MailboxQueryUrlFormat = "boxes?query={0}&page={1}&paging={2}";

        private const string MailboxUrl = "boxes";

        private const string MailboxUrlFormat = "boxes/{0}";

        internal MailboxResource(RestClient client)
            : base(client)
        {
            this.Quotas = new MailboxQuotaResource(client);
            this.Scripts = new SieveResource(client);
        }

        public MailboxQuotaResource Quotas { get; }

        public SieveResource Scripts { get; }

        public Task<bool> Create([NotNull] CreateMailbox mailbox)
        {
            return this.CreateResource(MailboxUrl, mailbox);
        }

        public Task<bool> Delete([NotNull] string email)
        {
            return this.DeleteResource(string.Format(MailboxUrlFormat, email));
        }

        public Task<Mailbox> Get([NotNull] string email)
        {
            return this.GetResource(string.Format(MailboxUrlFormat, email));
        }

        public Task<ResponsePage<Mailbox>> Query(string query = default(string), int page = 1, int paging = 50)
        {
            return this.QueryResource(string.Format(MailboxQueryUrlFormat, query, page, paging));
        }

        public Task<bool> Update([NotNull] UpdateMailbox mailbox)
        {
            return this.UpdateResource(string.Format(MailboxUrlFormat, mailbox.Email), mailbox);
        }
    }
}