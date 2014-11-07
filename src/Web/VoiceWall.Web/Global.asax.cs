namespace VoiceWall.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using VoiceWall.Web.Infrastructure.Mapping;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEnginesConfig.RegisterViewEngines();
            (new AutoMapperConfig(Assembly.GetExecutingAssembly())).Execute();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}


// child action only controller for partials
// all partials begin with _
// reqire ajax attribute (request.isAjax)
// post get redirect
// static pages have seperate route ?
// user can not comment more than once 30 sec ?
// child action - caching - on comment/wall item - needs to be accesable from the outside after ajax like / comment etc.
// cache by cache profiles (short, medium, custom)
// comments and wall items vary by id and string - when requesting update pass guid and solved :) (in partial view actions)
// cache tracks count of comments ?

//audit info / Ideleatable entitiy Iorderable Iauding info copy -> paste

// i deleatable entity - watch demo