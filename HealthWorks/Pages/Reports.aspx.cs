using BusinessUtility;
using LoggerUtility;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class Reports : System.Web.UI.Page
    {
        TicketDetailMethods  ticketDetailMethods = new TicketDetailMethods();
        public DataTable lobjDT;
        DateTime now = DateTime.Now;
        CustomLogger customLogger = new CustomLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "HealthWorksAI - Ticket";
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
                    BindData();
                    BindDate();
                }
                HideHelpLink();
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.btnDownload);
               
            }
            catch(Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        private void Active_DeactiveLinks()
        {
            LinkButton lb = (LinkButton)Master.FindControl("lbTicketHistory");
            lb.CssClass = "dropdown-item active";
        }

        public void HideHelpLink()
        {
            ((HtmlControl)Master.FindControl("liFullScreen")).Attributes["class"] = "hidden";
        }

        public void BindDate()
        {
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            txtStartDate.Text = firstDay.Date.ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }

        public void BindData()
        {
            Gvd_PastTicket.DataSource = ticketDetailMethods.viewPastTicket();
            Gvd_PastTicket.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchTicketUsingDates();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void SearchTicketUsingDates()
        {
            Gvd_PastTicket.DataSource = ticketDetailMethods.PastTicketfilterDisplay(txtStartDate.Text, txtEndDate.Text);
            Gvd_PastTicket.DataBind();
        }

        protected void Gvd_PastTicket_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortingDirection = string.Empty;
                if (direction == SortDirection.Ascending)
                {
                    direction = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    direction = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                Gvd_PastTicket.DataSource = ticketDetailMethods.PastTicketfilterDisplay(txtStartDate.Text, txtEndDate.Text);
                lobjDT = Gvd_PastTicket.DataSource as DataTable;
                if (lobjDT != null)
                {
                    DataView sortedView1 = new DataView(lobjDT);
                    sortedView1.Sort = e.SortExpression + " " + sortingDirection;
                    Session["SortedView"] = sortedView1;
                    Gvd_PastTicket.DataSource = sortedView1;
                    Gvd_PastTicket.DataBind();
                }
            }
            catch(Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Gvd_PastTicket.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Charset = "";
                    string FileName = "PageTracking.xls";
                    StringWriter strwritter = new StringWriter();
                    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                    Gvd_PastTicket.GridLines = GridLines.Both;
                    Gvd_PastTicket.HeaderStyle.Font.Bold = true;
                    Gvd_PastTicket.RenderControl(htmltextwrtter);
                    Response.Write(strwritter.ToString());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }

        }
    }
}