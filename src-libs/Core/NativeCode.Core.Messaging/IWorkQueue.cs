namespace NativeCode.Core.Messaging
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWorkQueue<TRequest, TResult> : IDisposable
        where TRequest : IQueueMessage
        where TResult : IQueueMessage
    {
        IWorkConsumer<TRequest> Consumer { get; }

        IWorkDispatcher<TResult> Dispatcher { get; }

        Task Publish(TRequest message);

        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
