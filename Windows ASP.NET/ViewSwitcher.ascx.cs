using Microsoft.AspNet.FriendlyUrls.Resolvers;
using System;
using System.Web;
using System.Web.Routing;

namespace Windows_ASP.NET
{
    public partial class ViewSwitcher : System.Web.UI.UserControl
    {
        protected string CurrentView
        {
            get; private set;
        }

        protected string AlternateView
        {
            get; private set;
        }

        protected string SwitchUrl
        {
            get; private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Determine current view
            bool isMobile = WebFormsFriendlyUrlResolver.IsMobileView(new HttpContextWrapper(this.Context));
            this.CurrentView = isMobile ? "Mobile" : "Desktop";

            // Determine alternate view
            this.AlternateView = isMobile ? "Desktop" : "Mobile";

            // Create switch URL from the route, e.g. ~/__FriendlyUrls_SwitchView/Mobile?ReturnUrl=/Page
            string switchViewRouteName = "AspNet.FriendlyUrls.SwitchView";
            RouteBase switchViewRoute = RouteTable.Routes[switchViewRouteName];
            if (switchViewRoute == null)
            {
                // Friendly URLs is not enabled or the name of the switch view route is out of sync
                this.Visible = false;
                return;
            }
            string url = this.GetRouteUrl(switchViewRouteName, new
            {
                view = this.AlternateView,
                __FriendlyUrls_SwitchViews = true
            });
            url += "?ReturnUrl=" + HttpUtility.UrlEncode(this.Request.RawUrl);
            this.SwitchUrl = url;
        }
    }
}