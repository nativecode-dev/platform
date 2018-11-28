namespace NativeCode.Core.Processing
{
    public abstract class FileProcessor<TContext, TResults> : Processor<TContext, TResults>
        where TContext : FileProcessorContext
    {
    }
}