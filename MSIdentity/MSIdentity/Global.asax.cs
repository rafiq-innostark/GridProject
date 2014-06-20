using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Interfaces.Main_Repositories;
using Microsoft.Practices.Unity;
using Repository.Main_Repository;
using Repository.Repository;


namespace MSIdentity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer container;
        protected void Application_Start()
        {
            RegisterIoC();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Register types with the IoC
        /// </summary>
        private static void RegisterTypes()
        {
            container.RegisterType(typeof(IProductRepository), typeof(ProductRepository));
            container.RegisterType(typeof(IControllerActivator), typeof(UnityControllerActivator));
            container.RegisterType(typeof(ICategoryRepository), typeof(CategoryRepository));

        }
        /// <summary>
        /// Register unity 
        /// </summary>
        public static void RegisterIoC()
        {
            if (container == null)
            {
                container = CreateUnityContainer();
            }
        }
        /// <summary>
        /// Create the unity container
        /// </summary>
        private static IUnityContainer CreateUnityContainer()
        {
            container = new UnityContainer();
            RegisterTypes();

            return container;
        }

        /// <summary>
        /// Container
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    RegisterIoC();
                }
                return container;
            }
        }
    }
}
