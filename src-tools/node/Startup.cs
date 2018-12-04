namespace node
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NativeCode.Core.Extensions;
    using NativeCode.Node.Core.Options;
    using Newtonsoft.Json.Converters;
    using NSwag;

    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.Configuration = configuration;
            this.HostEnv = hostingEnvironment;
        }

        protected IConfiguration Configuration { get; }

        protected IHostingEnvironment HostEnv { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOption<NodeOptions>(this.Configuration, out var node);

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
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
