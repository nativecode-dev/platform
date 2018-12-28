namespace NativeCode.Node.Media.Data.Data.Storage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using NativeCode.Core.Data;

    public class MountPathFile : Entity<Guid>
    {
        [Required]
        [MaxLength(1024)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(4096)]
        public string FilePath { get; set; }

        [Required]
        public byte[] Hash { get; set; }

        [ForeignKey(nameof(MountPathId))]
        public MountPath MountPath { get; set; }

        public Guid MountPathId { get; set; }

        public long Size { get; set; }
    }
}
