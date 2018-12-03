namespace NativeCode.Core.Messaging
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWorkDispatcher<T>
        where T : IQueueMessage
    {
        Task Start(CancellationToken cancellationToken);

        void Stop();
    }
}