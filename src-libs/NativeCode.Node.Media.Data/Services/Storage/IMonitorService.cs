namespace NativeCode.Node.Media.Data.Services.Storage
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using NativeCode.Node.Media.Data.Data.Storage;

    public interface IMonitorService
    {
        Task StartMonitor([NotNull] MountPath mountPath);

        Task StopMonitor([NotNull] MountPath mountPath);
    }
}
