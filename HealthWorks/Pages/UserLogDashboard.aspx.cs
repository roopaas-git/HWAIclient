using CommonUtility;
using LoggerUtility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TableauUtility;

namespace HealthWorks.Pages
{
    public partial class UserLogDashboard : System.Web.UI.Page
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
                    
                    LoadHelpPdf();
                }

                LoadReport();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        private void LoadHelpPdf()
        {
            //Session["FileName"] = Constants.UserLogDashboardPdf;
            //LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            //lbFullScreen.Visible = false;
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("UserLogPage"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";

            }

        }

        private void LoadReport()
        {
            string tableauServerName = Constants.TableauServerProtocol;
            string tableauUserName = Constants.tableauUser;
            string httpType = Constants.httyType;
            string tableauSiteName = Constants.tableauSiteName;

            TableauTicket tableauTicket = new TableauTicket();
            string tableauTicketId = tableauTicket.GetTableauTicket(tableauServerName, tableauUserName, tableauSiteName);

            if (tableauTicketId == "-1")
            {
                return;
            }
            else
            {
                string url = tableauTicket.GetTableauURL(tableauServerName, tableauTicketId, Constants.UserLogDashboard);
                tableauViewFrame.Attributes[Constants.iframeSRC] = url;
            }
        }

      
    }
}