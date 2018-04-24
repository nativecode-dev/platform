namespace NativeCode.Posteio
{
    using NativeCode.Posteio.Resources;

    public interface IPosteioClient
    {
        DomainResource Domains { get; }

        MailboxResource Mailboxes { get; }
    }
}
