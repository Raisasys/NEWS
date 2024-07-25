using Core;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Service.Web;



partial class WebExtensions
{

    private static void UseSpecificControllers<TControllerBase>(this ApplicationPartManager partManager, params Type[] controllerTypes)
    {
        partManager.FeatureProviders.Add(new InternalControllerFeatureProvider<TControllerBase>());
        partManager.ApplicationParts.Clear();
        partManager.ApplicationParts.Add(new SelectedControllersApplicationParts(controllerTypes));
    }
    private static void UseAppControllers<TControllerBase>(this ApplicationPartManager partManager)
    {
        partManager.FeatureProviders.Add(new InternalControllerFeatureProvider<TControllerBase>());
        partManager.ApplicationParts.Clear();
        partManager.ApplicationParts.Add(new SelectedControllersApplicationParts(TypeProvider.AppControllers<TControllerBase>().ToArray()));
    }

    public static IMvcCoreBuilder UseSpecificControllers<TControllerBase>(this IMvcCoreBuilder mvcCoreBuilder, params Type[] controllerTypes)
        => mvcCoreBuilder.ConfigureApplicationPartManager(partManager => partManager.UseSpecificControllers<TControllerBase>(controllerTypes));

    public static IMvcCoreBuilder AddAppControllers<TControllerBase>(this IMvcCoreBuilder mvcCoreBuilder)
        => mvcCoreBuilder.ConfigureApplicationPartManager(partManager => partManager.UseAppControllers<TControllerBase>());


    private class SelectedControllersApplicationParts : ApplicationPart, IApplicationPartTypeProvider
    {
        public SelectedControllersApplicationParts()
        {
            Name = "Only allow selected controllers";
        }

        public SelectedControllersApplicationParts(Type[] types)
        {
            Types = types.Select(x => x.GetTypeInfo()).ToArray();
        }

        public override string Name { get; }

        public IEnumerable<TypeInfo> Types { get; }
    }

    private class InternalControllerFeatureProvider<TControllerBase> : ControllerFeatureProvider
    {
        private const string ControllerTypeNameSuffix = "Controller";

        protected override bool IsController(TypeInfo typeInfo)
        {
            if (!typeInfo.IsClass)
            {
                return false;
            }

            if (typeInfo.IsAbstract)
            {
                return false;
            }

            if (typeInfo.ContainsGenericParameters)
            {
                return false;
            }

            if (!typeInfo.IsSubclassOf(typeof(TControllerBase)))
            {
                return false;
            }

            if (!AppConfig.Env.IsDevelopment())
            {
                if (typeInfo.IsDefined(typeof(DevelopmentControllerAttribute)))
                {
                    return false;
                }
            }


            /*if (typeInfo.IsDefined(typeof(Microsoft.AspNetCore.Mvc.NonControllerAttribute)))
            {
                return false;
            }*/

            /*if (!typeInfo.Name.EndsWith(ControllerTypeNameSuffix, StringComparison.OrdinalIgnoreCase) &&
                       !typeInfo.IsDefined(typeof(Microsoft.AspNetCore.Mvc.ControllerAttribute)))
            {
                return false;
            }*/

            return true;
        }
    }
}
