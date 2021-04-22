using DataUtility;
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
    public partial class TrackPageUsage : System.Web.UI.Page
    {
        UserTracker userTracker;
        CustomLogger customLogger = new CustomLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "HealthWorksAI - Tracker";
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
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    DateTime firstDay = new DateTime(year, month, 1);

                    txtStartDate.Text = firstDay.ToShortDateString();
                    txtEndDate.Text = DateTime.Now.Date.ToShortDateString();
                    BtnSearch_Click(this, null);
                }
                HideHelpLink();

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(this.lbImagBtn);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void HideHelpLink()
        {
            LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            lbFullScreen.Visible = false;
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("TrackPage"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";

            }
           
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                userTracker = new UserTracker();
                DataTable dataTable = userTracker.GetPortalUsageStats(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text));

                if (dataTable.Rows.Count > 0)
                {
                    GV_PageTracking.DataSource = dataTable;
                    GV_PageTracking.DataBind();

                }
                else
                {
                    GV_PageTracking.DataSource = null;
                    GV_PageTracking.DataBind();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);   
            }
        }

        protected void imgBtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (GV_PageTracking.Rows.Count > 0)
                {
                    ExportToExcel(GV_PageTracking, "PageTracking.xls");
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        public void ExportToExcel(GridView gridView, string fileName)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                gridView.GridLines = GridLines.Both;
                gridView.HeaderStyle.Font.Bold = true;
                gridView.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }
    }
}