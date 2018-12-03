namespace NativeCode.Clients.Posteio
{
    using Resources;

    public interface IPosteioClient
    {
        DomainResource Domains { get; }

        MailboxResource Mailboxes { get; }
    }
}