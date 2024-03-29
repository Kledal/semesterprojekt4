﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Makerlab
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Frontend", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "OneLevelNested",
            //    url: "api/{controller}/{id}/{action}",
            //    defaults: new { action = RouteParameter.Optional }
            //);

        }
    }
}
