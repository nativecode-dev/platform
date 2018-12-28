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

            var serilog = CreateSerilogConfig(elasticsearch.Url, name);
            Log.Logger = serilog.ReadFrom.Configuration(configuration)
                .CreateLogger();

            return services;
        }

        public static LoggerConfiguration CreateSerilogConfig(string elasticSearchUrl, string name)
        {
            var esconfig = new ElasticsearchSinkOptions(new Uri(elasticSearchUrl))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"log-{name}-{{0:yyyy.MM.dd}}",
                MinimumLogEventLevel = LogEventLevel.Verbose,
            };

            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithAssemblyName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .WriteTo.Elasticsearch(esconfig)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Trace();
        }
    }
}
