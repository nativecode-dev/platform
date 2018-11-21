namespace NativeCode.Konfd.Data.Schema
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class Tag : Entity<Guid>
    {
        [Required]
        [MaxLength(DataTypeSize.Name.Normal)]
        public string Name { get; set; }
    }
}
