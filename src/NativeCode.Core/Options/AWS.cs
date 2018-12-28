namespace NativeCode.Core.Options
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AWS
    {
        public string AccessKey { get; set; } = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");

        public string Secretkey { get; set; } = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");

        public string Region { get; set; } = "us-east-1";
    }
}
