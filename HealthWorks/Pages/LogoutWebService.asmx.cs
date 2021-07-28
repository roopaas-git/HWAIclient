using CommonUtility;
using DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HealthWorks.Pages
{
    /// <summary>
    /// Summary description for LogoutWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LogoutWebService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public void UpdateStatus(string username)
        {
            try
            {
                UserDetails userDetails = new UserDetails();
                userDetails.LogoutSession(username);
            }
            catch (Exception)
            {

            }

        }
        [WebMethod(EnableSession = true)]
        public void SendMailonLikeUnlike(string username, string dashboard, string like)
        {
            EmailServices emailServices = new EmailServices();
            emailServices.SendMailonLikeUnlike(username, dashboard, like);

        }
    }
}
