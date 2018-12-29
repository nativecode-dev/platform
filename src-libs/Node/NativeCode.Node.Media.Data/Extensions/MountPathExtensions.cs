namespace NativeCode.Node.Media.Data.Extensions
{
    using System;
    using Core.Enums;
    using Data.Storage;

    public static class MountPathExtensions
    {
        public static Uri GetMountUri(this MountPath source)
        {
            return source.GetMountUrl();
        }

        public static Uri GetMountUrl(this MountPath source)
        {
            switch (source.Mount.Type)
            {
                case MountType.Nfs:
                    return new Uri(source.GetNfsMountUrl());

                case MountType.Smb:
                    return new Uri(source.GetSmbMountUrl());

                default:
                    return new Uri(source.GetLocalMountUrl());
            }
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
    }
}
