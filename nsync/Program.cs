namespace NativeCode.Sync
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;

    using DigitalOcean.API;

    using NativeCode.Core.Extensions;
    using NativeCode.Posteio;
    using NativeCode.Posteio.Requests;

    using Nito.AsyncEx;

    using Console = Colorful.Console;

    internal class Program
    {
        private static readonly string DigitalOceanApiKey = Environment.GetEnvironmentVariable("APIKEY_DIGITALOCEAN");

        private static readonly string MailServerHostName = Environment.GetEnvironmentVariable("MAILSERVER_HOSTNAME");

        private static readonly string MailServerPassword = Environment.GetEnvironmentVariable("MAILSERVER_PASSWORD");

        private static readonly string MailServerUserName = Environment.GetEnvironmentVariable("MAILSERVER_USERNAME");

        public static void Main(string[] args)
        {
            AsyncContext.Run(() => Run(args));
        }

        private static async Task Run(IEnumerable<string> args)
        {
            args.ForEach(arg => Console.WriteLine(arg, "program_argument", Color.DimGray));

            var digitalocean = new DigitalOceanClient(DigitalOceanApiKey);
            var posteio = new PosteioClient(MailServerHostName, MailServerUserName, MailServerPassword);

            var mxdomains = await posteio.Domains.Query();
            var nsdomains = await digitalocean.Domains.GetAll();

            foreach (var nsdomain in nsdomains)
            {
                try
                {
                    var mxdomain = mxdomains.Results.SingleOrDefault(md => md.Name == nsdomain.Name);

                    if (mxdomain != null)
                    {
                        Console.WriteLine(mxdomain.Forward ? $"-> {mxdomain.ForwardDomain}" : $"[{mxdomain.Home}]", nsdomain.Name);

                        var mailboxes = await posteio.Mailboxes.Query($"@{mxdomain.Name}");

                        foreach (var mailbox in mailboxes.Results)
                        {
                            Console.WriteLine(mailbox.Address, mxdomain.Name, Color.CadetBlue);
                        }

                        continue;
                    }

                    var domain = new CreateDomain
                    {
                        DomainBin = false,
                        DomainBinAddress = string.Empty,
                        DomainName = nsdomain.Name,
                        Forward = true,
                        ForwardDomain = "nativecode.com",
                    };

                    if (await posteio.Domains.Create(domain) == false)
                    {
                        Console.WriteLine($"Failed to create domain: {domain.DomainName}.", Color.DarkRed);
                        continue;
                    }

                    Console.WriteLine($"Created new forwarding domain: {domain.DomainName}.", Color.DarkGreen);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, Color.Red);
                }
            }

            Console.WriteLine("Press any key to continue...", Color.DarkOliveGreen);
            Console.ReadKey(true);
        }
    }
}
