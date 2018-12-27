namespace NativeCode.Node.Media.Data.Library.Plex
{
    using System;
    using NativeCode.Core.Data;

    public class PlexServerInfo : Entity<Guid>
    {
        public string Host { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string Token { get; set; }
    }
}
