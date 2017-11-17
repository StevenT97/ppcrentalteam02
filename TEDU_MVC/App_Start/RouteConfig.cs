using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TEDU_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
    "Default",
    "{controller}/{action}/{id}",
    new { controller = "Home", action = "Index", id = UrlParameter.Optional }
   
);
            routes.MapRoute(
        name: "Login",
        url: "dang-nhap",
        defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional },
        namespaces: new[] { "TEDU_MVC.Controllers" }
    );
        }
    }
}
