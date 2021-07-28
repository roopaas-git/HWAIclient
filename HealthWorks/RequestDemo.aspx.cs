using CommonUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthWorks
{
    public partial class RequestDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            EmailServices emailServices = new EmailServices();
            emailServices.SendRequestDemoMail(txtFirstName.Text, txtCompanyName.Text,ddlUserType.SelectedValue,txtEmail.Text);
            Response.Redirect("~/Default.aspx");

        }

    }
}