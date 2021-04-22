using CommonUtility;
using LoggerUtility;
using System;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TableauUtility;

namespace HealthWorks.Pages
{
    public partial class Whitepaper : System.Web.UI.Page
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
            // Session["FileName"] = Constants.HospitalComparePdf;
           // LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
           // lbFullScreen.Visible = false;
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
            string tableauSiteName = Constants.tableauWhitepaperSiteName;

            TableauTicket tableauTicket = new TableauTicket();
            string tableauTicketId = tableauTicket.GetTableauTicket(tableauServerName, tableauUserName, tableauSiteName);

            if (tableauTicketId == "-1")
            {
                return;
            }
            else
            {
                if (Session["whitepaperURL"] != null)
                {
                    string whitepaperURL = ConfigurationManager.AppSettings[Session["whitepaperURL"].ToString()].ToString();
                    string url = tableauTicket.GetWhitepaperTableauURL(tableauServerName, tableauTicketId, whitepaperURL);
                    tableauViewFrame.Attributes[Constants.iframeSRC] = url;
                    if (Session["WhitepaperHeight"] != null)
                    {
                        string height = Session["WhitepaperHeight"].ToString();
                        tableauViewFrame.Style.Add("Height", height);
                    }

                }
            }
        }
    }
}