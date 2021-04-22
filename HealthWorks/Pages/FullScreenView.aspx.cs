using System;

namespace HealthWorks.Pages
{
    public partial class FullScreenView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["FileName"] != null)
                {
                    if (!IsPostBack)
                    {
                        string path = string.Empty;
                        path =  Session["FileName"].ToString() + "#zoom=80";
                        displayPDF.Attributes["src"] = path;
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch(Exception ex)
            {
               
            }
        }
    }
}