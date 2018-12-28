namespace NativeCode.Node.Media.Services.Storage
{
    using System.Threading.Tasks;
    using Data.Storage;
    using JetBrains.Annotations;

    public interface IFileMonitorService
    {
        Task StartMonitor([NotNull] MountPath mountPath);

        Task StopMonitor([NotNull] MountPath mountPath);
    }
}
