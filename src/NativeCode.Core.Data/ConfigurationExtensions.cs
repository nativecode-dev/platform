namespace NativeCode.Core.Data
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Adds the context seeder.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IServiceCollection AddContextSeeder<TContext>(
            this IServiceCollection services,
            Func<IServiceProvider, DataContextSeeder<TContext>> factory = null)
            where TContext : DbContext
        {
            if (factory == null)
            {
                services.AddScoped<IDataContextSeeder<TContext>, DataContextSeeder<TContext>>();
            }
            else
            {
                services.AddScoped<IDataContextSeeder<TContext>, DataContextSeeder<TContext>>(factory);
            }

            return services;
        }
    }
}
