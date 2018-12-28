namespace NativeCode.Node.Media.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class MountPathFileInfo
    {
        [Required]
        [MaxLength(1024)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(4096)]
        public string FilePath { get; set; }

        [Required]
        [SuppressMessage("Microsoft.Performance", "CA1819")]
        public byte[] Hash { get; set; }

        public MountPathInfo MountPathInfo { get; set; }

        public long Size { get; set; }
    }
}
