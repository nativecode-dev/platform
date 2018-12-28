namespace node
{
    public class NodeOptions : NativeCode.Node.Core.Options.NodeOptions
    {
        public int WorkerCount { get; set; } = 10;
    }
}
