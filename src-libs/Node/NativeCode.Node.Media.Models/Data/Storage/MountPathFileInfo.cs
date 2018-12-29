namespace NativeCode.Node.Media.Models.Data.Storage
{
    using System.ComponentModel.DataAnnotations;

    public class MountPathFileInfo : DataModel
    {
        [Required]
        [MaxLength(1024)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(4096)]
        public string FilePath { get; set; }

        [Required]
        public byte[] Hash { get; set; }

        public MountPathInfo MountPathInfo { get; set; }

        public long Size { get; set; }
    }
}
