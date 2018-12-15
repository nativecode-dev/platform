namespace NativeCode.Node.Core
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NativeCode.Core.Extensions;
    using Options;
    using Serilog;
    using Serilog.Exceptions;
    using Serilog.Sinks.Elasticsearch;

    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration, string name)
        {
            services.AddOption<ElasticSearchOptions>(configuration, out var elasticsearch);

            var esconfig = new ElasticsearchSinkOptions(new Uri(elasticsearch.Url))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"log-{name}-{{0:yyyy.MM.dd}}",
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Elasticsearch(esconfig)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Trace()
                .CreateLogger();

            return services;
        }
    }
}
