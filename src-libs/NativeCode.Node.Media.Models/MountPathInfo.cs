namespace NativeCode.Node.Media.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MountPathInfo
    {
        public List<MountPathFileInfo> Files { get; } = new List<MountPathFileInfo>();

        public MountInfo Mount { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Path { get; set; }
    }
}
