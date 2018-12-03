namespace NativeCode.Core.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Adds the context seeder.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddContextSeeder<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IContextSeeder<TContext>, ContextSeeder<TContext>>();

            return services;
        }
    }
}