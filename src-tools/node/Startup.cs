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
    using NativeCode.Node.Core;
    using NativeCode.Node.Core.Options;
    using Newtonsoft.Json.Converters;
    using NSwag;
    using Serilog;

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

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOption<NodeOptions>(this.Configuration, out var node);
            services.AddOption<ElasticSearchOptions>(this.Configuration, out var elasticsearch);
            services.AddSerilog(this.Configuration, elasticsearch.Url);

            Log.Logger.Information("Startup: {@node}", node);

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = node.RedisHost;
                options.InstanceName = Program.AppName;
            });

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerDocument(options =>
            {
                options.DocumentName = Program.Version;
                options.Title = Program.AppName;
            });

            return services.BuildServiceProvider();
        }
    }
}
