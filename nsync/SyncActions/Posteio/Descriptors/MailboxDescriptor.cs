namespace NativeCode.Sync.SyncActions.Posteio.Descriptors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    using NativeCode.Posteio.Responses;

    public class MailboxDescriptor : PosteioDescriptor
    {
        public MailboxDescriptor(IDistributedCache cache, string userAgent, string hostname, string username, string password)
            : base(cache, userAgent, hostname, username, password)
        {
        }

        public override Task CreateKey(string key, ISyncDescriptor source)
        {
            return Task.CompletedTask;
        }

        public override Task RemoveKey(string key, ISyncDescriptor source)
        {
            return Task.CompletedTask;
        }

        protected override async Task<IReadOnlyCollection<string>> DescriptorKeys()
        {
            var results = new List<Mailbox>();
            var mailboxes = await this.Client.Mailboxes.Query();
            results.AddRange(mailboxes.Results);

            while (mailboxes.Page < mailboxes.LastPage)
            {
                mailboxes = await this.Client.Mailboxes.Query(null, mailboxes.Page + 1, mailboxes.Paging);
                results.AddRange(mailboxes.Results);
            }

            return results.Select(r => r.Name).ToList();
        }

        protected override async Task<object> DescriptorValue(string key, string name)
        {
            var mailbox = await this.Client.Domains.Get(key);
            return this.GetPropertyValue(name, mailbox);
        }
    }
}
