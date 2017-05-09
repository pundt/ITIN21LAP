using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace hearthstone.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// einmalig - wenn die Applikation gestartet wird
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // log4net start logging!!!
            XmlConfigurator.Configure();
        }

        /// bei JEDEM Aufruf
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        /// immer dann wenn Authentizifiert werden muss
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            /// check if there is an authentication ticket (delivered with this request)
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            
            /// in case there IS a ticket
            if (authCookie!=null)
            {
                /// get and decrypt it
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                
                /// get data from ticket (username and roles)
                /// and set a principal (tmt: a generic "logon" - almost like a windows logon)
                /// with given data
                Context.User = new GenericPrincipal(new GenericIdentity(ticket.Name), new string[] { ticket.UserData });
            }
        }
    }
}
