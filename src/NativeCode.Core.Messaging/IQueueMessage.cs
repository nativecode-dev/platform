namespace NativeCode.Core.Messaging
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Presents a marker for types that are used as topics.
    /// </summary>
    public interface IQueueMessage
    {
        /// <summary>
        /// Gets a unique message identifier.
        /// </summary>
        Guid MessageIdentifier { get; }

        /// <summary>
        /// Gets the <see cref="DateTime" /> message was sent.
        /// </summary>
        DateTime MessageSent { get; }

        string SourceMachine { get; }

        IList<string> TargetMachines { get; }

        ulong DeliveryTag { get; set; }
    }
}
