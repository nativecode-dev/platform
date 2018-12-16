namespace NativeCode.Node.Core
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NativeCode.Core.Extensions;
    using Options;
    using Serilog;
    using Serilog.Events;
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
                MinimumLogEventLevel = LogEventLevel.Verbose,
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithAssemblyName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .WriteTo.Elasticsearch(esconfig)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Trace()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return services;
        }
    }
}
