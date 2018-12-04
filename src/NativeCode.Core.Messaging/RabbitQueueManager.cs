namespace NativeCode.Core.Messaging
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Options;
    using RabbitMQ.Client;
    using Reliability;

    /// <summary>
    /// NOTE: This should be registered as a singleton per-application. RabbitMQ has
    /// its own connection pooling and shares a single socket via channels
    /// <see cref="T:RabbitMQ.Client.IModel" />. Rabbit actually runs two background threads, but that is
    /// an implementation detail.
    /// </summary>
    public class RabbitQueueManager : Disposable, IQueueManager
    {
        public RabbitQueueManager(IQueueSerializer serializer, IOptions<RabbitOptions> options, ILoggerFactory logFactory)
        {
            var factory = new ConnectionFactory
            {
                DispatchConsumersAsync = options.Value.DispatchConsumersAsync,
                HostName = options.Value.Host,
                Password = options.Value.Password,
                Port = options.Value.Port,
                UserName = options.Value.User,
                VirtualHost = options.Value.VirtualHost
            };

            this.Connection = factory.CreateConnection();
            this.Options = options.Value;
            this.LogFactory = logFactory;
            this.Serializer = serializer;
        }

        protected IConnection Connection { get; }

        protected RabbitOptions Options { get; }

        protected ILoggerFactory LogFactory { get; }

        protected IQueueSerializer Serializer { get; }

        public IQueueTopic<T> GetIncomingQueue<T>()
            where T : IQueueMessage
        {
            return new RabbitQueueTopic<T>(this.Connection, this.Serializer, this.LogFactory.CreateLogger<T>());
        }

        public IQueueTopic<T> GetIncomingQueue<T>(string queue, string route = default(string))
            where T : IQueueMessage
        {
            return new RabbitQueueTopic<T>(this.Connection, this.Serializer, this.LogFactory.CreateLogger<T>(), queue,
                route);
        }

        public IQueueTopic<T> GetOutgoingQueue<T>()
            where T : IQueueMessage
        {
            return new RabbitQueueTopic<T>(this.Connection, this.Serializer, this.LogFactory.CreateLogger<T>());
        }

        public IQueueTopic<T> GetOutgoingQueue<T>(string queue, string route = default(string))
            where T : IQueueMessage
        {
            return new RabbitQueueTopic<T>(this.Connection, this.Serializer, this.LogFactory.CreateLogger<T>(), queue,
                route);
        }

        protected override void ReleaseManaged()
        {
            this.Connection.Dispose();
        }
    }
}
