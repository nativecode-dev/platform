namespace NativeCode.Clients.Radarr.Responses
{
    public class DiskSpace
    {
        public long FreeSpace { get; set; }

        public string Label { get; set; }

        public string Path { get; set; }

        public long TotalSpace { get; set; }
    }
}
