namespace NativeCode.Core.Storage
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public abstract class RemoteFileStoreContext : ICloneable
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Requires default constructor")]
        protected RemoteFileStoreContext()
        {
        }

        protected RemoteFileStoreContext(RemoteFileStoreContext clone)
        {
            this.BasePath = clone.BasePath;
            this.Host = clone.Host;
        }

        public string BasePath { get; set; }

        public string FilePath { get; set; }

        public string Host { get; set; }

        /// <inheritdoc />
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public abstract RemoteFileStoreContext Clone();
    }
}
