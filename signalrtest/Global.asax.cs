using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SignalRTest
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["Active"] = true;
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            UserSession.End(this.Session.SessionID);
        }
    }
}