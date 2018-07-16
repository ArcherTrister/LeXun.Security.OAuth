using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AspNetCore.Client.Data;
using AspNetCore.Client.Models;
using AspNetCore.Client.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Client
{
    public class Startup
    {
        #region 2.0

        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables()
        //        .AddUserSecrets<Startup>();

        //    Configuration = builder.Build();
        //}

        //public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //    services.AddIdentity<ApplicationUser, ApplicationRole>()
        //        .AddEntityFrameworkStores<ApplicationDbContext>()
        //        .AddDefaultTokenProviders();

        //    // Add application services.
        //    services.AddTransient<IEmailSender, EmailSender>();

        //    services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
        //    {
        //        microsoftOptions.CallbackPath = new PathString("/signin-microsoft");
        //        microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
        //        microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
        //    }).AddGoogle(googleOptions =>
        //    {
        //        googleOptions.CallbackPath = new PathString("/signin-google");
        //        googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
        //        googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
        //    }).AddQQ(qqOptions =>
        //    {
        //        qqOptions.AppId = Configuration["Authentication:QQ:AppId"];
        //        qqOptions.AppKey = Configuration["Authentication:QQ:AppKey"];
        //    }).AddWeChat(wechatOptions => {
        //        wechatOptions.AppId = Configuration["Authentication:WeChat:AppId"];
        //        wechatOptions.AppSecret = Configuration["Authentication:WeChat:AppSecret"];
        //    });

        //    services.AddMvc();
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //        app.UseBrowserLink();
        //        app.UseDatabaseErrorPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //    }

        //    loggerFactory.AddConsole();

        //    app.UseStaticFiles();

        //    app.UseAuthentication();

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}

        #endregion 2.0

        #region 2.1

        //Microsoft.VisualStudio.Web.BrowserLink  2.0
        //app.UseBrowserLink();2.0
        //ILoggerFactory loggerFactory 2.0
        //loggerFactory.AddConsole(); 2.0
        //Microsoft.AspNetCore.CookiePolicy 2.1
        //Microsoft.AspNetCore.Http.Features 2.1
        //Microsoft.AspNetCore.Mvc.Core 2.1
        //Microsoft.AspNetCore.HttpsPolicy 2.1
        //Microsoft.AspNetCore.Identity.UI 替换全部登录注册控制器和页面 2.1

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmsSender, SmsSender>();

            services.AddExternalIdentityProviders(_configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)//
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseMiddleware<Logging.RequestLoggerMiddleware>();

            loggerFactory.AddConsole();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion 2.1
    }

    public static class ServiceExtensions
    {
        public static IServiceCollection AddExternalIdentityProviders(this IServiceCollection services, IConfiguration configuration)
        {
            // configures the OpenIdConnect handlers to persist the state parameter into the server-side IDistributedCache.
            //services.AddOidcStateDataFormatterCache("aad", "demoidsrv");

            services.AddAuthentication()
            //.AddGoogle("Google", options =>
            //{
            //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //    options.ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com";
            //    options.ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh";
            //})

            //.AddOpenIdConnect("adfs", "ADFS", options =>
            //{
            //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

            //    options.Authority = "https://adfs.leastprivilege.vm/adfs";
            //    options.ClientId = "c0ea8d99-f1e7-43b0-a100-7dee3f2e5c3c";
            //    options.ResponseType = "id_token";

            //    options.CallbackPath = "/signin-adfs";
            //    options.SignedOutCallbackPath = "/signout-callback-adfs";
            //    options.RemoteSignOutPath = "/signout-adfs";
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        NameClaimType = "name",
            //        RoleClaimType = "role"
            //    };
            //})
            //.AddWsFederation("adfs-wsfed", "ADFS with WS-Fed", options =>
            //{
            //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

            //    options.MetadataAddress = "https://adfs4.local/federationmetadata/2007-06/federationmetadata.xml";
            //    options.Wtrealm = "urn:test";

            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        NameClaimType = "name",
            //        RoleClaimType = "role"
            //    };
            //})
            //    .AddMicrosoftAccount(microsoftOptions =>
            //{
            //    microsoftOptions.CallbackPath = new PathString("/signin-microsoft");
            //    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
            //    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
            //})

            .AddBaidu(options =>
            {
                options.ClientId = configuration["Authentication:Baidu:AppId"];
                options.ClientSecret = configuration["Authentication:Baidu:AppSecret"];
            })
            .AddQQ(options =>
            {
                options.ClientId = configuration["Authentication:QQ:AppId"];
                options.ClientSecret = configuration["Authentication:QQ:AppKey"];
            })
            .AddWeChat(options =>
            {
                options.ClientId = configuration["Authentication:WeChat:AppId"];
                options.ClientSecret = configuration["Authentication:WeChat:AppSecret"];
            })
            .AddAlipay(options =>
            {
                options.ClientId = configuration["Authentication:Alipay:AppId"];
                options.ClientSecret = configuration["Authentication:Alipay:MerchantPrivateKey"];
                options.AlipayPublicKey = configuration["Authentication:Alipay:AlipayPublicKey"];
            })
            .AddCsdn(options =>
            {
                options.ClientId = "1100787";
                options.ClientSecret = "4a69488c93af4efa9adff607cd04eeca";
            });

            return services;
        }
    }
}