using System.Web.Mvc;
using System.Web.Routing;
using ML.Core;
using ML.Core.Contracts;

namespace ML.Web
{
    public class RouteConfig : IInitializer
    {
        private readonly RouteCollection _routes;
        public RouteConfig(RouteCollection routes)
        {
            _routes = routes;
        }
        public void Initialize()
        {
            _routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            _routes.MapMvcAttributeRoutes();
        }
    }
}
