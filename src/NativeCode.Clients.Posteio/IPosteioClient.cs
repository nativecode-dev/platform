namespace NativeCode.Clients.Posteio
{
    using NativeCode.Clients.Posteio.Resources;

    public interface IPosteioClient
    {
        DomainResource Domains { get; }

        MailboxResource Mailboxes { get; }
    }
}
