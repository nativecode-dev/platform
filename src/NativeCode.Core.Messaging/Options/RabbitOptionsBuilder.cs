namespace NativeCode.Core.Messaging.Options
{
    using System;

    public class RabbitOptionsBuilder
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string VirtualHost { get; set; }

        public RabbitOptionsBuilder SetHost(string hostname, int port = 5672)
        {
            this.Host = hostname;
            this.Port = port;

            return this;
        }

        /// <summary>
        /// Set the virtual host, the default if not specified is '/'
        /// </summary>
        /// <param name="virtualHost"></param>
        /// <returns></returns>
        public RabbitOptionsBuilder OnVirtualHost(string virtualHost)
        {
            this.VirtualHost = virtualHost;

            return this;
        }

        public RabbitOptionsBuilder WithCredientials(string username, string password)
        {
            this.Username = username;
            this.Password = password;

            return this;
        }

        internal RabbitOptions Build()
        {
            if (string.IsNullOrWhiteSpace(this.Host))
            {
                throw new Exception("A host for RabbitMQ must be specified");
            }

            return new RabbitOptions
            {
                Host = this.Host,
                Port = this.Port,
                DispatchConsumersAsync = true,
                VirtualHost = this.VirtualHost,
                User = this.Username,
                Password = this.Password,
            };
        }
    }
}