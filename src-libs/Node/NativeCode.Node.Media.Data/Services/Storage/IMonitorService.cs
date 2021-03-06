namespace NativeCode.Node.Media.Data.Services.Storage
{
    using System.Threading.Tasks;
    using Data.Storage;
    using JetBrains.Annotations;

    public interface IMonitorService
    {
        Task StartMonitor([NotNull] MountPath mountPath);

        Task StopMonitor([NotNull] MountPath mountPath);
    }
}
