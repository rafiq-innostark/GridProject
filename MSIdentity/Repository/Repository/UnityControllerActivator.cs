using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Repository.Repository
{
    /// <summary>
    /// Provides fine-grained control over how controllers are instantiated using
    /// dependency injection.
    /// </summary>
    public class UnityControllerActivator : IControllerActivator
    {
        #region Public

        /// <summary>
        /// Creates a controller.
        /// </summary>
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }

        #endregion
    }

}
