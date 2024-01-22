using Microsoft.AspNet.FriendlyUrls;
using System.Web.Mvc;
using System.Web.Routing;

namespace Windows_ASP.NET
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            _ = routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new
                    {
                        action = "Index",
                        id = UrlParameter.Optional
                    }
                );
        }
    }
}
