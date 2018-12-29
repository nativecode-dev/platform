namespace NativeCode.Core.Messaging
{
    using System;

    public abstract class QueueMessage : IQueueMessage
    {
        public ulong DeliveryTag { get; set; }

        public Guid MessageIdentifier { get; } = Guid.NewGuid();

        public DateTime MessageSent { get; } = DateTime.UtcNow;

        public string SourceMachine { get; } = Environment.MachineName;

        public string TargetMachine { get; set; }
    }
}
