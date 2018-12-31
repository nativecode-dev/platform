namespace NativeCode.Core.Processing
{
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Nito.AsyncEx;

    public abstract class Processor<TContext, TResults> : IProcessor<TContext, TResults>
        where TContext : ProcessorContext
    {
        public TResults Process(TContext context)
        {
            return AsyncContext.Run(() => this.ProcessAsync(context, CancellationToken.None));
        }

        public async Task<TResults> ProcessAsync(TContext context, CancellationToken cancellationToken)
        {
            var results = await this.ProcessContext(context, cancellationToken)
                .NoCapture();

            await this.ProcessResults(results, context, cancellationToken)
                .NoCapture();

            return results;
        }

        protected abstract Task<TResults> ProcessContext(TContext context, CancellationToken cancellationToken);

        protected abstract Task ProcessResults(TResults results, TContext context, CancellationToken cancellationToken);
    }
}
