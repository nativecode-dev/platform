namespace NativeCode.Node.Media.Data.Data.Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using NativeCode.Core.Data;

    public class MountPath : Entity<Guid>
    {
        public List<MountPathFile> Files { get; } = new List<MountPathFile>();

        [ForeignKey(nameof(MountId))]
        public Mount Mount { get; set; }

        public Guid MountId { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Path { get; set; }
    }
}
