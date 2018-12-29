namespace NativeCode.Core.Messaging
{
    using System;

    /// <summary>
    ///     Presents a marker for types that are used as topics.
    /// </summary>
    public interface IQueueMessage
    {
        /// <summary>
        ///     Gets or sets the delivery tag.
        /// </summary>
        ulong DeliveryTag { get; set; }

        /// <summary>
        ///     Gets a unique message identifier.
        /// </summary>
        Guid MessageIdentifier { get; }

        /// <summary>
        ///     Gets the <see cref="DateTime" /> message was sent.
        /// </summary>
        DateTime MessageSent { get; }

        /// <summary>Gets the source machine.</summary>
        /// <value>The source machine.</value>
        string SourceMachine { get; }

        /// <summary>
        ///     Gets the target machines.
        /// </summary>
        /// <value>
        ///     The target machines.
        /// </value>
        string TargetMachine { get; set; }
    }
}
