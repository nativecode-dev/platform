namespace NativeCode.Konfd.Data.Services
{
    using NativeCode.Konfd.Data.Security;

    public interface IResourceSecurity
    {
        bool HasPermission(SecurityKey key, Resource resource);
    }
}
