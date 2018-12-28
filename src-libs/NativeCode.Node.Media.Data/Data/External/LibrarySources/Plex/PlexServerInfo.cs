namespace NativeCode.Node.Media.Data.Data.External.LibrarySources.Plex
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NativeCode.Core.Data;

    public class PlexServerInfo : Entity<Guid>
    {
        [Required]
        public string Host { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string Token { get; set; }
    }
}
