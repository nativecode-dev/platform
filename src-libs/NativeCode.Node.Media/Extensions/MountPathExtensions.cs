namespace NativeCode.Node.Media.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Data.Storage;

    public static class MountPathExtensions
    {
        public static Uri GetMountUri(this MountPath source)
        {
            return new Uri(source.GetMountUrl());
        }

        [SuppressMessage("Microsoft.Design", "CA1055")]
        public static string GetMountUrl(this MountPath source)
        {
            switch (source.Mount.Type)
            {
                case MountType.Nfs:
                    return source.GetNfsMountUrl();

                case MountType.Smb:
                    return source.GetSmbMountUrl();

                default:
                    return source.GetLocalMountUrl();
            }
        }

        private static string GetNfsMountUrl(this MountPath source)
        {
            var credentials = source.Mount.Credentials;
            var userinfo = string.Empty;

            if (credentials != null && credentials.Type == CredentialType.Basic)
            {
                userinfo = $"{credentials.Login}:{credentials.Password}@";
            }

            return $"nfs://{userinfo}{source.Mount.Host}/{source.Path}";
        }

        private static string GetSmbMountUrl(this MountPath source)
        {
            return $"file://{source.Mount.Host}{source.Path}";
        }

        private static string GetLocalMountUrl(this MountPath source)
        {
            var credentials = source.Mount.Credentials;
            var userinfo = string.Empty;

            if (credentials != null && credentials.Type == CredentialType.Basic)
            {
                userinfo = $"{credentials.Login}:{credentials.Password}@";
            }

            return $"file://{userinfo}{source.Mount.Host}/{source.Path}";
        }
    }
}
