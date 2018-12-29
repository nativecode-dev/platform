namespace NativeCode.Core.Messaging
{
    using System;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Envelopes;
    using Exceptions;
    using Extensions;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    internal class RabbitQueueTopic<T> : QueuePattern<T>
        where T : IQueueMessage
    {
        private readonly IModel channel;

        private readonly Subject<T> subject;

        public RabbitQueueTopic(IConnection connection, IQueueSerializer serializer, ILogger<T> logger)
            : this(connection, serializer, logger, typeof(T).GetQueueName(), typeof(T).GetRouteName())
        {
        }

        public RabbitQueueTopic(
            IConnection connection,
            IQueueSerializer serializer,
            ILogger<T> logger,
            string queue = default,
            string route = default)
            : base(serializer, logger)
        {
            this.channel = connection.CreateModel();
            this.subject = new Subject<T>();

            this.Connection = connection;
            this.QueueName = queue;
            this.Route = string.IsNullOrWhiteSpace(route) ? string.Empty : route;

            this.Bind();
        }

        protected IConnection Connection { get; }

        protected string ExchangeName => this.GetExchangeName();

        public override void Acknowledge(ulong deliveryTag)
        {
            this.channel.BasicAck(deliveryTag, false);
        }

        public override IObservable<T> AsObservable()
        {
            return this.CreateObservable();
        }

        public override Task Publish(T message)
        {
            // NOTE: Rabbit doesn't support an async method to publish messages
            // because you want to know immediately if publishing failed. However,
            // if a different queue is used the task ensures we are forward
            // compatible.
            var envelope = new RequestEnvelope<T>(message);
            var bytes = this.Serializer.Serialize(envelope);
            this.channel.BasicPublish(this.ExchangeName, this.Route, body: bytes);

            return Task.CompletedTask;
        }

        public override void Requeue(ulong deliveryTag)
        {
            this.channel.BasicNack(deliveryTag, false, true);
        }

        protected override void ReleaseManaged()
        {
            this.subject.Dispose();
            this.channel.Dispose();
        }

        private void Bind()
        {
            this.channel.ExchangeDeclare(this.ExchangeName, ExchangeType.Topic, true);
            this.channel.QueueDeclare(this.QueueName, true, false, false);
            this.channel.QueueBind(this.QueueName, this.ExchangeName, this.Route);
        }

        /// <summary>
        ///     This is the heart of the message loop, as it opens a channel to the queue and waits for responses
        ///     via an event handler. If you add multiple consumers by creating more than one <see cref="IObservable{T}" />,
        ///     be aware that they will both receive the message. If you don't want this behavior, setup a proper queue
        ///     mechanism whereby work comes in via a work queue and publishes results on another queue representing
        ///     finished work (which has its own consumer).
        /// </summary>
        /// <returns></returns>
        private IObservable<T> CreateObservable()
        {
            // TODO: Need to create a separate class that derives or uses a Subject. When
            // the subject is disposed, the event handler should be unwired. You don't want
            // to see me when I'm not unwired.
            var consumer = new AsyncEventingBasicConsumer(this.channel);
            this.channel.BasicConsume(this.QueueName, false, consumer);

            // NOTE: We don't have to release this event handler because:
            // a) It's a lambda and we're wrapping an async method for a void returning event.
            // b) The AsyncEventingBasicConsumer instance is internal and when we're done with the observable,
            // either through ProcessCompleted or cancelling the stream, the GC will remove both for us.
            consumer.Received += async (sender, args) =>
            {
                // TODO: Allow consumers to override acknowledgment. Likely the best option is to
                // use a context object that returns the consumer's intent and we call the ack/nack
                // methods ourselves. It's a YAGNI until someone actually needs this feature.
                // TODO: Need to implement a throttling method in order to ensure we can control
                // how many messages can be processed at any given moment.
                try
                {
                    var envelope = this.Serializer.Deserialize<RequestEnvelope<T>>(args.Body);

                    if (envelope.Message != null)
                    {
                        envelope.Message.DeliveryTag = args.DeliveryTag;
                        this.subject.OnNext(envelope.Message);
                    }
                }
                catch (QueueDeserializationException qde)
                {
                    // Log the error, but we don't want to fail.
                    this.Logger.LogDebug(qde.Message);
                    this.Logger.LogDebug(qde.StackTrace);

                    // TODO: We don't want to requeue a message that can't be deserialized.
                    // However, we should also queue it somewhere for analysis. That
                    // will be your job to make that happen!
                    this.channel.BasicNack(args.DeliveryTag, false, false);
                }
                catch (Exception ex)
                {
                    // Log the error, but we don't want to fail.
                    this.Logger.LogDebug(ex.Message);
                    this.Logger.LogDebug(ex.StackTrace);

                    // TODO: We want to requeue this directly back to the work queue because
                    // it might be something that is available at a later time.
                    // In a real production environment, this would go to a new queue for
                    // re-queuing along with the exception to be resolved.
                    this.channel.BasicNack(args.DeliveryTag, false, true);
                }

                await Task.Yield();
            };

            return this.subject;
        }
    }
}
