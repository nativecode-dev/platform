namespace NativeCode.Node.Media.Data.Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using NativeCode.Core.Data;

    public class MountPath : Entity<Guid>
    {
        public List<MountPathFile> Files { get; set; } = new List<MountPathFile>();

        [ForeignKey(nameof(MountId))]
        public Mount Mount { get; set; }

        public Guid MountId { get; set; }

        public string Path { get; set; }
    }
}
