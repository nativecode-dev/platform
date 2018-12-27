namespace NativeCode.Node.Media.Services
{
    using Data.Storage;
    using JetBrains.Annotations;

    public interface IFileMonitorService
    {
        void StartMonitor([NotNull] MountPath mountPath);

        void StopMonitor([NotNull] MountPath mountPath);
    }
}
