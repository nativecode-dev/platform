namespace identity
{
    using System;
    using AutoMapper;
    using IdentityServer4;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Mvc;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Core.WebHosting;
    using NativeCode.Node.Identity;
    using NativeCode.Node.Identity.Entities;
    using NativeCode.Node.Identity.JsonConverters;
    using NativeCode.Node.Identity.SeedModels;

    public class Startup : AspNetStartup<AppOptions>
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory loggingFactory,
            ILogger<AspNetStartup<AppOptions>> logger) : base(configuration, hostingEnvironment, loggingFactory, logger)
        {
        }

        protected override string AppName => "Identity";

        protected override string AppProduct => "Platform";

        protected override string AppVersion => "v1";

        public override void Configure(IApplicationBuilder app)
        {
            if (this.HostingEnvironment.IsProduction())
            {
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors();
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseForwardedHeaders();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.UseSwaggerUi3();
            app.UseSwagger(settings => { settings.DocumentName = Program.AppName; });
        }

        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOption<AppOptions>(this.Configuration, out var app);
            services.AddOption<RedisOptions>(this.Configuration, out var redis);

            services.AddIdentityConverters();

            this.ConfigureAutoMapper(services);
            this.ConfigureDbContext(services);
            this.ConfigureIdentityServer(services, redis);
            this.ConfigureAutoMapper(services);
            this.ConfigureMvc(services.AddMvcCore());

            services.AddSwaggerDocument(options =>
            {
                options.DocumentName = this.AppName;
                options.OperationProcessors.Add(new UnauthorizedOperationProcessor());
            });

            return services.BuildServiceProvider();
        }

        protected override AuthenticationBuilder CreateAuthenticationBuilder(IServiceCollection services)
        {
            var authentication = services
                .AddAuthentication()
                .AddCookie();

            authentication.AddApplicationCookie();

            return authentication;
        }

        protected override IApplicationBuilder ConfigureMiddleware(IApplicationBuilder app)
        {
            return app;
        }

        protected override IMvcCoreBuilder ConfigureMvc(IMvcCoreBuilder builder)
        {
            return builder.AddApiExplorer()
                .AddAuthorization()
                .AddCacheTagHelper()
                .AddCookieTempDataProvider()
                .AddDataAnnotations()
                .AddModelValidator()
                .AddRazorPages()
                .AddRazorViewEngine()
                .AddViews()
                .AddCors(options => options.AddDefaultPolicy(configure =>
                {
                    options.DefaultPolicyName = this.Options.ApiScope;
                    configure.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials();
                }))
                .AddMvcOptions(options =>
                {
                    var policy = ScopePolicy.Create(this.Options.ApiScope);
                    options.Filters.Add(new AuthorizeFilter(policy));
                });
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(options =>
            {
                options.CreateMap<User, UserInfo>()
                    .ForSourceMember(src => src.Email, expr => expr.DoNotValidate())
                    .ForSourceMember(src => src.EmailConfirmed, expr => expr.DoNotValidate())
                    .ForSourceMember(src => src.Id, expr => expr.DoNotValidate())
                    .ForSourceMember(src => src.UserName, expr => expr.DoNotValidate())
                    .ReverseMap();
            });
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = this.Configuration.GetConnectionString(nameof(IdentityDataContext));
                this.Logger.LogTrace("{@connectionString}", connectionString);
                options.UseSqlServer(connectionString, opts => opts.MigrationsAssembly("NativeCode.Node.Identity"));
#if DEBUG
                options.EnableSensitiveDataLogging();
#endif
            });
        }

        private void ConfigureIdentityServer(IServiceCollection services, RedisOptions redis)
        {
            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityDataContext>();

            services.AddDataProtection()
                .PersistKeysToDbContext<IdentityDataContext>();

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.PublicOrigin = this.Options.Authority;
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(this.Options.ApiScope, configure =>
                {
                    options.DefaultPolicy = configure
                        .RequireAuthenticatedUser()
                        .RequireScope(this.Options.ApiScope)
                        .Build();
                });
            });
        }
    }
}
