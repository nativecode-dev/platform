namespace node
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Converters;
    using Options;

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
            this.Options<NodeOptions>(services);

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
            app.UseSwagger();
            app.UseSwaggerUi3();
        }

        private void Options<T>(IServiceCollection services) where T : class
        {
            var section = this.Configuration.GetSection(typeof(T).Name);
            services.Configure<T>(section);
        }

        private static RewriteOptions CreateRewriteRules()
        {
            return new RewriteOptions()
                .AddRedirect("^$", "swagger")
                .AddRedirectToHttps();
        }
    }
}
