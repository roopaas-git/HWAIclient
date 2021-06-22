using CommonUtility;
using LoggerUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class PdfView : System.Web.UI.Page
    {
        public string htmlString = string.Empty;
        CustomLogger customLogger = new CustomLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    Active_DeactiveLinks();
                }

                if (!IsPostBack)
                {
                    PDFDisplay();


                }
                HideHelpLink();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void PDFDisplay()
        {
            if (Session["PDFURL"] != null)
            {
                string Pdfsrc = ConfigurationManager.AppSettings[Session["pdfURL"].ToString()].ToString();
                pdfdisplay.Src = Pdfsrc;
                Session["FileName"] = Pdfsrc;
            }

        }
        public void HideHelpLink()
        {
            //LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            //lbFullScreen.Visible = false;
        }

        private void Active_DeactiveLinks()
        {

            if (Session["ActiveLink"] != null)
            {
                string LinkName = Session["ActiveLink"].ToString();
                HtmlControl htmlctl = ((HtmlControl)Master.FindControl(LinkName));
                if (htmlctl != null)
                {
                    htmlctl.Attributes["class"] = "active";
                    ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
                }
            }

            //HtmlControl htmlctl = ((HtmlControl)Master.FindControl("UnlimitedIntelC1L4Li"));
            //if (htmlctl != null)
            //{
            //    htmlctl.Attributes["class"] = "active";
            //    ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";

            //}
        }

        protected void lbFullScreen_Click(object sender, EventArgs e)
        {
            try
            {
                //string viewPdf = Constants.TPVWhitepaper;
                //Session["FileName"] = viewPdf;
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('FullScreenView.aspx' ,'_blank');", true);
                //Response.Redirect("FullScreenView.aspx");
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }
    }
}