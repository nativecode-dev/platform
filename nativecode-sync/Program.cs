namespace NativeCode.Sync
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;

    using Colorful;

    using DigitalOcean.API;

    using NativeCode.Posteio;

    using Console = Colorful.Console;

    internal class Program
    {
        private static readonly string DigitalOceanApiKey = Environment.GetEnvironmentVariable("APIKEY_DIGITALOCEAN");

        private static readonly string MailServerHostName = Environment.GetEnvironmentVariable("MAILSERVER_HOSTNAME");

        private static readonly string MailServerPassword = Environment.GetEnvironmentVariable("MAILSERVER_PASSWORD");

        private static readonly string MailServerUserName = Environment.GetEnvironmentVariable("MAILSERVER_USERNAME");

        public static async Task Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var digitalocean = new DigitalOceanClient(DigitalOceanApiKey);
            var posteio = new PosteioClient(MailServerHostName, MailServerUserName, MailServerPassword);

            var mxdomains = await posteio.Domains.Query();
            var nsdomains = await digitalocean.Domains.GetAll();

            foreach (var nsdomain in nsdomains)
            {
                try
                {
                    var mxdomain = mxdomains.Results.Single(md => md.Name == nsdomain.Name);

                    Trace.WriteLine(mxdomain.Forward ? $"-> {mxdomain.ForwardDomain}" : $"[{mxdomain.Home}]", nsdomain.Name);

                    var mailboxes = await posteio.Mailboxes.Query($"@{mxdomain.Name}");

                    foreach (var mailbox in mailboxes.Results)
                    {
                        Trace.WriteLine(mailbox.Address, mxdomain.Name);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Press any key to continue...", Color.DarkOliveGreen);
            Console.ReadKey(true);
        }

        public class ConsoleTraceListener : TraceListener
        {
            public override void Write(string message)
            {
                Console.Write(message);
            }

            public override void WriteLine(string message)
            {
                var style = new StyleSheet(Color.White);
                // Domains used as categories in traces.
                style.AddStyle("^([a-z,-]+.[a-z]+):", Color.Cyan);
                // Anything that looks like an email.
                style.AddStyle("([\\w,.,-]+@[a-z,-]+.[a-z]+)", Color.DarkCyan);

                Console.WriteLineStyled(message, style);
            }
        }
    }
}
