using System;
using System.Web.Mvc;
using Ninject;

namespace Web.Infrastructure
{
    [CoverageExcludeAttribute]
    public class TrackyControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel = new StandardKernel(new TrackyServices());

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            return _kernel.Get(controllerType) as IController;
        }
    }
}