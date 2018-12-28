namespace NativeCode.Core.Messaging
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWorkDispatcher<T>
        where T : IQueueMessage
    {
        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
