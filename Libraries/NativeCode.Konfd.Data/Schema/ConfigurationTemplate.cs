namespace NativeCode.Konfd.Data.Schema
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class ConfigurationTemplate : Entity<Guid>
    {
        public int TemplateVersion { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}
