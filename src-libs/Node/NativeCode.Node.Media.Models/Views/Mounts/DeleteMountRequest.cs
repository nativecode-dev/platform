namespace NativeCode.Node.Media.Models.Views.Mounts
{
    using System;

    public class DeleteMountRequest : ViewModelRequest
    {
        public Guid Id { get; set; }
    }
}
