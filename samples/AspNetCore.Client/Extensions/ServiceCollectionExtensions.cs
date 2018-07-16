using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThirdPartyConnectDemo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Integrates ABP to AspNet Core.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</typeparam>
        /// <param name="services">Services.</param>
        /// <param name="optionsAction">An action to get/modify options</param>
        public static IServiceProvider AddAbp<TStartupModule>(this IServiceCollection services, [CanBeNull] Action<AbpBootstrapperOptions> optionsAction = null)
            where TStartupModule : AbpModule
        {
            var abpBootstrapper = AddAbpBootstrapper<TStartupModule>(services, optionsAction);

            ConfigureAspNetCore(services, abpBootstrapper.IocManager);

            return WindsorRegistrationHelper.CreateServiceProvider(abpBootstrapper.IocManager.IocContainer, services);
        }

        private static void ConfigureAspNetCore(IServiceCollection services, IIocResolver iocResolver)
        {
            //See https://github.com/aspnet/Mvc/issues/3936 to know why we added these services.
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Use DI to create controllers
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //Use DI to create view components
            services.Replace(ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator>());

            //Change anti forgery filters (to work proper with non-browser clients)
            services.Replace(ServiceDescriptor.Transient<AutoValidateAntiforgeryTokenAuthorizationFilter, AbpAutoValidateAntiforgeryTokenAuthorizationFilter>());
            services.Replace(ServiceDescriptor.Transient<ValidateAntiforgeryTokenAuthorizationFilter, AbpValidateAntiforgeryTokenAuthorizationFilter>());

            //Add feature providers
            var partManager = services.GetSingletonServiceOrNull<ApplicationPartManager>();
            partManager?.FeatureProviders.Add(new AbpAppServiceControllerFeatureProvider(iocResolver));

            //Configure JSON serializer
            services.Configure<MvcJsonOptions>(jsonOptions =>
            {
                jsonOptions.SerializerSettings.Converters.Insert(0, new AbpDateTimeConverter());
            });

            //Configure MVC
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddAbp(services);
            });

            //Configure Razor
            services.Insert(0,
                ServiceDescriptor.Singleton<IConfigureOptions<RazorViewEngineOptions>>(
                    new ConfigureOptions<RazorViewEngineOptions>(
                        (options) =>
                        {
                            options.FileProviders.Add(new EmbeddedResourceViewFileProvider(iocResolver));
                        }
                    )
                )
            );
        }

        private static AbpBootstrapper AddAbpBootstrapper<TStartupModule>(IServiceCollection services, Action<AbpBootstrapperOptions> optionsAction)
            where TStartupModule : AbpModule
        {
            var abpBootstrapper = AbpBootstrapper.Create<TStartupModule>(optionsAction);
            services.AddSingleton(abpBootstrapper);
            return abpBootstrapper;
        }
    }
}
