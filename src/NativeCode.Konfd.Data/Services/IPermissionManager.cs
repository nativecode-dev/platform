namespace NativeCode.Konfd.Data.Services
{
    using System.Threading.Tasks;

    using NativeCode.Konfd.Data.Security;

    public interface IPermissionManager
    {
        void Assert(SecurityKey key, Resource resource);

        Task Assign(SecurityKey key, Role role);

        Task Revoke(SecurityKey key);

        Task Revoke(SecurityKey key, Permission permission);
    }
}
