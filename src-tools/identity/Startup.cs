namespace identity
{
    using System;
    using AutoMapper;
    using IdentityServer4;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Authorization;
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
            services.AddIdentityConverters();

            this.ConfigureAutoMapper(services);
            this.ConfigureDbContext(services);
            this.ConfigureIdentityServer(services, app, redis);
            this.ConfigureAutoMapper(services);
            this.ConfigureMvc(services, app);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.ApiName = app.ApiName;
                    options.ApiSecret = app.ApiSecret;
                    options.Authority = app.Authority;
                    options.RequireHttpsMetadata = app.RequireHttpsMetadata;
                    options.JwtValidationClockSkew = app.ClockSkew;
                });

            services.AddSwaggerDocument(options => { options.DocumentName = Program.AppName; });

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
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
            app.UseMvc();
            app.UseMvcWithDefaultRoute();
            app.UseSwaggerUi3();
            app.UseSwagger(settings => { settings.DocumentName = Program.AppName; });
        }

        private void ConfigureMvc(IServiceCollection services, AppOptions app)
        {
            services.AddMvc()
                .AddModelValidator()
                .AddMvcOptions(options =>
                {
                    var policy = ScopePolicy.Create(app.ApiScope);
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthorization(options =>
                {
                    options.AddPolicy(app.ApiScope, configure =>
                    {
                        configure.RequireAuthenticatedUser();
                        configure.RequireScope(app.ApiScope);
                    });
                })
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(configure =>
                    {
                        configure.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()
                            .AllowCredentials();
                    });
                });
        }

        private void ConfigureIdentityServer(IServiceCollection services, AppOptions app, RedisOptions redis)
        {
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

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(options =>
            {
                options.CreateMap<User, UserInfo>()
                    .ForSourceMember(source => source.Email, expression => expression.DoNotValidate())
                    .ForSourceMember(source => source.EmailConfirmed, expression => expression.DoNotValidate())
                    .ForSourceMember(source => source.Id, expression => expression.DoNotValidate())
                    .ForSourceMember(source => source.UserName, expression => expression.DoNotValidate())
                    .ReverseMap();
            });
        }
    }
}
