namespace identity
{
    using System;
    using AutoMapper;
    using IdentityServer4;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Data;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Mvc;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Identity;
    using NativeCode.Node.Identity.Entities;
    using NativeCode.Node.Identity.JsonConverters;
    using NativeCode.Node.Identity.SeedModels;

    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILogger<Startup> logger)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
            this.Logger = logger;
        }

        protected IConfiguration Configuration { get; }

        protected IHostingEnvironment HostingEnvironment { get; }

        protected ILogger<Startup> Logger { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOption<AppOptions>(this.Configuration, out var app);
            services.AddOption<RedisOptions>(this.Configuration, out var redis);

            services.AddContextSeeder<IdentityDataContext>();

            services.AddAutoMapper(options =>
            {
                options.CreateMap<User, UserInfo>()
                    .ForSourceMember(source => source.Email, expression => expression.DoNotValidate())
                    .ForSourceMember(source => source.EmailConfirmed, expression => expression.DoNotValidate())
                    .ForSourceMember(source => source.Id, expression => expression.DoNotValidate())
                    .ForSourceMember(source => source.UserName, expression => expression.DoNotValidate())
                    .ReverseMap();
            });

            services.AddIdentityConverters();

            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = this.Configuration.GetConnectionString(nameof(IdentityDataContext));
                this.Logger.LogTrace("{@connectionString}", connectionString);
                options.UseSqlServer(connectionString, opts => opts.MigrationsAssembly("NativeCode.Node.Identity"));
#if DEBUG
                options.EnableSensitiveDataLogging();
#endif
            });

            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<IdentityDataContext>();

            services.AddIdentityServer(options =>
                {
                    options.Authentication.CookieAuthenticationScheme =
                        IdentityServerConstants.DefaultCookieAuthenticationScheme;
                    options.Cors.CorsPaths.Add("/token");
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.IssuerUri = app.Authority;
                    this.Logger.LogTrace("{@options}", options);
                })
                .AddAspNetIdentity<User>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        var connectionString = this.Configuration.GetConnectionString(nameof(IdentityDataContext));
                        builder.UseSqlServer(connectionString);
                    };
                    this.Logger.LogTrace("{@options}", options);
                })
                .AddConfigurationStoreCache()
                .AddJwtBearerClientAuthentication()
                .AddOperationalStore(options =>
                {
                    options.Db = redis.RedisOperationalStore;
                    options.KeyPrefix = redis.RedisOperationalStoreKey;
                    options.RedisConnectionString = redis.RedisConnection;
                    this.Logger.LogTrace("{@options}", options);
                });

            services.AddMvc()
                .AddModelValidator()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (this.HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}
