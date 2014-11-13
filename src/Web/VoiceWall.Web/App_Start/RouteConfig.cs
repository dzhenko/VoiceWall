﻿using System;
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
                name: "Search",
                url: "Search/{action}/{search}",
                defaults: new { controller = "Search", action = "Index", search = UrlParameter.Optional },
                namespaces: new[] { "VoiceWall.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "Profile/{action}/{id}/{username}",
                defaults: new { controller = "Profile", action = "Index", id = UrlParameter.Optional, username = UrlParameter.Optional },
                namespaces: new[] { "VoiceWall.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "VoiceWall.Web.Controllers" }
            );

            routes.MapRoute(
                name: "StaticPages",
                url: "{action}",
                defaults: new { controller = "StaticPages" },
                namespaces: new[] { "VoiceWall.Web.Controllers" }
            );
        }
    }
}
