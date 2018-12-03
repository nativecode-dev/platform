namespace NativeCode.Core.Messaging
{
    using System;
    using System.Collections.Generic;

    public abstract class QueueMessage : IQueueMessage
    {
        public Guid MessageIdentifier { get; } = Guid.NewGuid();

        public DateTime MessageSent { get; } = DateTime.UtcNow;

        public string SourceMachine { get; } = Environment.MachineName;

        public IList<string> TargetMachines { get; } = new List<string>();
        public ulong DeliveryTag { get; set; }
    }
}
