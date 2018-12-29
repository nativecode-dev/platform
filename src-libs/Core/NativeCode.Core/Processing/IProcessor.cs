namespace NativeCode.Core.Processing
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IProcessor<in TContext, TResults>
        where TContext : ProcessorContext
    {
        TResults Process(TContext context);

        Task<TResults> ProcessAsync(TContext context, CancellationToken cancellationToken = default);
    }
}
