namespace NativeCode.Node.Media.Models.Views.Mounts
{
    using System.ComponentModel.DataAnnotations;
    using Core.Enums;

    public class CreateMountRequest : ViewModelRequest
    {
        [Required]
        public string Name { get; set; }

        public MountType Type { get; set; }
    }
}
