using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace Scrumee.Infrastructure
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController( RequestContext requestContext, string controllerName )
        {
            try
            {
                var controllerType = base.GetControllerType( requestContext, controllerName );
                return ObjectFactory.GetInstance( controllerType ) as IController;
            }
            catch ( Exception )
            {
                // Use the default logic
                return base.CreateController( requestContext, controllerName );
            }
        }
    }
}
