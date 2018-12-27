namespace NativeCode.Node.Media.Data.Storage
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using NativeCode.Core.Data;

    public class MountPathFile : Entity<Guid>
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public byte[] Hash { get; set; }

        [ForeignKey(nameof(MountPathId))]
        public MountPath MountPath { get; set; }

        public Guid MountPathId { get; set; }

        public long Size { get; set; }
    }
}
