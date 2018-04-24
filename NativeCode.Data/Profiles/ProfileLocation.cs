namespace NativeCode.Data.Profiles
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NativeCode.Core.Data;

    public class ProfileLocation : Entity<Guid>
    {
        [MaxLength(DataTypeSize.Text.Normal)]
        public string Address { get; set; }

        [MaxLength(DataTypeSize.PostalCode)]
        public string PostalCode { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }

        public int? StateId { get; set; }

        public ProfileLocationType Type { get; set; }
    }
}
