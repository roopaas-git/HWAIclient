using Autofac;
using DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace HealthWorks
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
           // var builder = new ContainerBuilder();
        }

        void Session_End(object sender, EventArgs e)
        {
            if (Session["UserName"].ToString() != null)
            {
                UserDetails userDetails = new UserDetails();
                userDetails.LogoutSession(Session["UserName"].ToString());
            }
        }
    }
}