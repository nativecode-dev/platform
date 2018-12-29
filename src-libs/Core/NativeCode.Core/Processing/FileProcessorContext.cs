namespace NativeCode.Core.Processing
{
    using System.Collections.Generic;
    using System.Linq;

    public class FileProcessorContext : ProcessorContext
    {
        public FileProcessorContext(IEnumerable<FileOperationInfo> operations)
        {
            this.FileOperations = operations.ToList();
        }

        public IReadOnlyList<FileOperationInfo> FileOperations { get; }
    }
}
