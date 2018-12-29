namespace NativeCode.Core.Options
{
    using System;

    public class AwsOptions
    {
        public string AccessKey { get; set; } = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");

        public string Region { get; set; } = "us-east-1";

        public string Secretkey { get; set; } = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");
    }
}
