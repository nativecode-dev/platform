namespace NativeCode.Tests
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    public abstract class WhenTestingDependencies : WhenTesting
    {
        protected WhenTestingDependencies()
        {
            this.Provider = this.Init(new ServiceCollection());
        }

        protected IServiceProvider Provider { get; }

        protected IServiceProvider Init(IServiceCollection services)
        {
            return this.RegisterServices(services).BuildServiceProvider();
        }

        protected abstract IServiceCollection RegisterServices(IServiceCollection services);
    }
}
