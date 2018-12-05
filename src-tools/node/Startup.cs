namespace node
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Extensions;
    using NativeCode.Node.Core.Options;
    using Newtonsoft.Json.Converters;
    using NSwag;
    using Serilog;
    using Serilog.Exceptions;
    using Serilog.Sinks.Elasticsearch;

    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory logging)
        {
            this.Configuration = configuration;
            this.HostEnv = hostingEnvironment;
            this.Logging = logging;
        }

        protected IConfiguration Configuration { get; }

        protected IHostingEnvironment HostEnv { get; }

        protected ILoggerFactory Logging { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOption<NodeOptions>(this.Configuration, out var node);
            services.AddOption<ElasticSearchOptions>(this.Configuration, out var elasticsearch);

            var esconfig = new ElasticsearchSinkOptions(new Uri(elasticsearch.Url))
            {
                AutoRegisterTemplate = true
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .ReadFrom.Configuration(this.Configuration)
                .WriteTo.Elasticsearch(esconfig)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Trace()
                .CreateLogger();

            this.Logging.AddSerilog();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = node.RedisHost;
                options.InstanceName = Program.Name;
            });

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerDocument(options =>
            {
                options.DocumentName = Program.Version;
                options.Title = Program.Name;
            });

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (this.HostEnv.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders();
            app.UseMvc();
            app.UseSwagger(config =>
            {
                config.PostProcess = (settings, c) =>
                {
                    settings.Schemes.Clear();
                    settings.Schemes.Add(SwaggerSchema.Https);
                };
            });
            app.UseSwaggerUi3();
        }
    }
}
