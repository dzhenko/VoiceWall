using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VoiceWall.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Own Profile",
                url: "profle/own",
                defaults: new { controller = "Profile", action = "Own" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "profle/{action}/{id}/{username}",
                defaults: new { controller="Profile", action = "Find", id = UrlParameter.Optional, username = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Static",
                url: "{action}",
                defaults: new { controller = "StaticPages" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "VoiceWall.Web.Controllers" }
            );
        }
    }
}
