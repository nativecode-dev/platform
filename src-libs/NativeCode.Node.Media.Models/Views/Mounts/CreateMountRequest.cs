namespace NativeCode.Node.Media.Models.Views.Mounts
{
    using System.ComponentModel.DataAnnotations;
    using Core.Enums;

    public class CreateMountRequest : ViewModelRequest
    {
        public MountType Type { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
