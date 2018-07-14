using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ThirdPartyConnectDemo.Filter;
using ThirdPartyConnectDemo.Reflection;

namespace ThirdPartyConnectDemo.Providers
{
    public class ServiceControllerFeatureProvider : ControllerFeatureProvider//IApplicationFeatureProvider<ControllerFeature>
    {
        //private const string ControllerTypeNameSuffix = "Controller";
        private readonly IEnumerable<Type> _serviceTypes;
        public ServiceControllerFeatureProvider(IEnumerable<Type> ServiceTypes)
        {
            _serviceTypes = ServiceTypes;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in Reflection.CurrentAssembiles.SelectMany(o => o.DefinedTypes))
            {
                if (IsController(type) || _serviceTypes.Any(o => type.IsClass && o.IsAssignableFrom(type)) && !feature.Controllers.Contains(type))
                {
                    feature.Controllers.Add(type);
                }
            }
        }

        public object AppDomain { get; private set; }

        public void PopulateFeature(AssemblyName assemblyName, IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            //var asse = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.Name.Contains("Common")).First();

            //var currentAssembly = Assembly.Load(asse);

            var currentAssembly = Assembly.Load(assemblyName);
            var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<GeneratedControllerAttribute>().Any());

            foreach (var candidate in candidates)
            {
                feature.Controllers.Add(
                    typeof(BaseController<>).MakeGenericType(candidate).GetTypeInfo()
                );
            }
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            var type = typeInfo.AsType();

            if (!typeof(IApplicationService).IsAssignableFrom(type) ||
                !typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
            {
                return false;
            }

            var remoteServiceAttr = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(typeInfo);

            if (remoteServiceAttr != null && !remoteServiceAttr.IsEnabledFor(type))
            {
                return false;
            }

            //var configuration = _iocResolver.Resolve<AbpAspNetCoreConfiguration>().ControllerAssemblySettings.GetSettingOrNull(type);
            //return configuration != null && configuration.TypePredicate(type);
            return true;
        }




    }
}
