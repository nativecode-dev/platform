namespace identity
{
    using AutoMapper;
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
    using NativeCode.Core;
    using NativeCode.Core.Extensions;
    using NativeCode.Core.Mvc;
    using NativeCode.Core.Storage;
    using NativeCode.Integrations.AmazonWebServices.Storage;
    using NativeCode.Node.Core.Options;
    using NativeCode.Node.Core.WebHosting;
    using NativeCode.Node.Identity;
    using NativeCode.Node.Identity.Entities;
    using NativeCode.Node.Identity.JsonConverters;
    using NativeCode.Node.Identity.SeedModels;

    public class Startup : AspNetStartup<AppOptions>
    {
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggingFactory,
            ILogger<AspNetStartup<AppOptions>> logger)
            : base(configuration, hostingEnvironment, loggingFactory, logger)
        {
        }

        protected override string AppName => "Identity";

        protected override string AppProduct => "Platform";

        protected override string AppVersion => "v1";

        protected override AuthenticationBuilder ConfigueAuthentication(IServiceCollection services)
        {
            var authentication = services.AddAuthentication()
                .AddCookie();

            authentication.AddApplicationCookie();

            return authentication;
        }

        protected override IApplicationBuilder ConfigureApp(IApplicationBuilder app)
        {
            if (this.HostingEnvironment.IsProduction())
            {
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            return app
                .UseAuthentication()
                .UseCors()
                .UseForwardedHeaders()
                .UseHealthChecks("/health")
                .UseHttpsRedirection()
                .UseIdentityServer()
                .UseMvcWithDefaultRoute()
                .UseStaticFiles()
                .UseSwagger(settings => settings.DocumentName = Program.AppName)
                .UseSwaggerUi3();
        }

        protected override IServiceCollection ConfigureAppServices(IServiceCollection services)
        {
            services
                .AddOption<AppOptions>(this.Configuration)
                .AddOption<RedisOptions>(this.Configuration, out var redis)
                .AddAws(this.Configuration)
                .AddIdentityConverters()
                .AddRemoteFileStore()
                .AddSwaggerDocument(options =>
                {
                    options.DocumentName = this.AppName;
                    options.OperationProcessors.Add(new UnauthorizedOperationProcessor());
                });

            services
                .AddRemoteFileStore()
                .AddTransient<IRemoteFileStoreProvider, SimpleStorageServiceProvider>();

            services
                .AddHealthChecks()
                .AddDbContextCheck<IdentityDataContext>();

            this.ConfigureAutoMapper(services);
            this.ConfigureDbContext(services);
            this.ConfigureIdentityServer(services, redis);
            this.ConfigureMvc(services.AddMvcCore());

            return services;
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
                .AddCors(
                    options => options.AddDefaultPolicy(
                        configure =>
                        {
                            options.DefaultPolicyName = this.Options.ApiScope;
                            configure.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin()
                                .AllowCredentials();
                        }))
                .AddMvcOptions(
                    options =>
                    {
                        var policy = ScopePolicy.Create(this.Options.ApiScope);
                        options.Filters.Add(new AuthorizeFilter(policy));
                    });
        }

        private IServiceCollection ConfigureAutoMapper(IServiceCollection services)
        {
            return services.AddAutoMapper(
                options =>
                {
                    options.CreateMap<User, UserInfo>()
                        .ForSourceMember(src => src.Email, expr => expr.DoNotValidate())
                        .ForSourceMember(src => src.EmailConfirmed, expr => expr.DoNotValidate())
                        .ForSourceMember(src => src.Id, expr => expr.DoNotValidate())
                        .ForSourceMember(src => src.UserName, expr => expr.DoNotValidate())
                        .ReverseMap();
                });
        }

        private IServiceCollection ConfigureDbContext(IServiceCollection services)
        {
            return services.AddDbContext<IdentityDataContext>(
                options =>
                {
                    var connectionString = this.Configuration.GetConnectionString(nameof(IdentityDataContext));
                    this.Logger.LogTrace("{@connectionString}", connectionString);
                    options.UseSqlServer(connectionString, opts => opts.MigrationsAssembly("NativeCode.Node.Identity"));
#if DEBUG
                    options.EnableSensitiveDataLogging();
#endif
                });
        }

        private IServiceCollection ConfigureIdentityServer(IServiceCollection services, RedisOptions redis)
        {
            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityDataContext>();

            services
                .AddDataProtection()
                .PersistKeysToDbContext<IdentityDataContext>();

            services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.PublicOrigin = this.Options.Authority;
                    this.Logger.LogTrace("{@options}", options);
                })
#if DEBUG
                .AddDeveloperSigningCredential()
#endif
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
                    options.RedisConnectionString = redis.Host;
                    this.Logger.LogTrace("{@options}", options);
                });

            return services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    this.Options.ApiScope,
                    configure =>
                    {
                        options.DefaultPolicy = configure.RequireAuthenticatedUser()
                            .RequireScope(this.Options.ApiScope)
                            .Build();
                    });
            });
        }
    }
}
