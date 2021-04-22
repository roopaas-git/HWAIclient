using CommonUtility;
using DataUtility;
using LoggerUtility;
using System;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TableauUtility;
namespace HealthWorks.Pages
{
    public partial class Dashboard : System.Web.UI.Page
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
            //Session["FileName"] = Constants.CompetitorAnalysisPDF;
            //LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            //lbFullScreen.Visible = true;
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
        }
        private void LoadReport()
        {
            string tableauServerName = Constants.TableauServerProtocol;
            string tableauUserName = Constants.tableauUser;
            string httpType = Constants.httyType;
            string tableauSiteName = Constants.tableauSiteName;

            TableauTicket tableauTicket = new TableauTicket();
            string tableauTicketId = tableauTicket.GetTableauTicket(tableauServerName, tableauUserName, tableauSiteName);

            if (Request.QueryString["DashboardName"] != null)
            {
                Session["dashboardURL"] = Request.QueryString["DashboardName"].ToString();
            }

            if (tableauTicketId == "-1")
            {
                string dashboardURL = ConfigurationManager.AppSettings[Session["dashboardURL"].ToString()].ToString();
                lblDashboard.Text = dashboardURL;
                return;
            }
            else
            {
                if (Session["dashboardURL"] != null)
                {
                    string dashboardURL = ConfigurationManager.AppSettings[Session["dashboardURL"].ToString()].ToString();
                    Console.Write(dashboardURL.ToString());
                    string url = tableauTicket.GetTableauURL(tableauServerName, tableauTicketId, dashboardURL);
                    tableauViewFrame.Attributes[Constants.iframeSRC] = url;
                    if (Session["DashboardHeight"] != null)
                    {
                        string height = Session["DashboardHeight"].ToString();
                        tableauViewFrame.Style.Add("Height", height);
                    }
                }
            }
        }
    }
}