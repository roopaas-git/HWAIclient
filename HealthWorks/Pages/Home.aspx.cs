using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Web.UI.WebControls;
using DataUtility;
using System.Data;
using System.Web.UI;
using System.Configuration;

namespace HealthWorks.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        PageTracking PT = new PageTracking();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                   // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
                    CheckAccountType();

                }
            }
        }
        private void CheckAccountType()
        {
            if (Constants.AccountType == Constants.platinum)
            {
                //divPostAEP.Attributes.Add("class", "card hw-dashboard-card on-hover-shadow disabled");
                //divPerformanceIntelligence.Attributes.Add("class", "card hw-dashboard-card on-hover-shadow disabled");
                //lbEnrollmentForecastAEP.CssClass = "hw-dashboard-link disabled";
                //lbSalesAndTerms.CssClass = "hw-dashboard-link disabled";
                //lbHospitalCompare.CssClass = "hw-dashboard-link disabled";
                //lbProviderScorecard.CssClass = "hw-dashboard-link disabled";
            }
            if (Constants.AccountType == Constants.gold)
            {
                //  divPerformanceIntelligence.Attributes.Add("class", "card hw-dashboard-card on-hover-shadow disabled");
                ////  divPostAEP.Attributes.Add("class", "card hw-dashboard-card on-hover-shadow disabled");
                //  lbEnrollmentForecastAEP.CssClass = "hw-dashboard-link disabled";
                //  lbSalesAndTerms.CssClass = "hw-dashboard-link disabled";
                //  lbHospitalCompare.CssClass = "hw-dashboard-link disabled";
                //  lbProviderScorecard.CssClass = "hw-dashboard-link disabled";
                //  lbMPF.CssClass= "hw-dashboard-link disabled";
                //lbOOPC.CssClass= "hw-dashboard-link disabled";
            }
            if (Constants.AccountType == Constants.silver)
            {
                //  divPostAEP.Attributes.Add("class", "card hw-dashboard-card on-hover-shadow disabled");
                //divPerformanceIntelligence.Attributes.Add("class", "card hw-dashboard-card on-hover-shadow disabled");
                //lbEnrollmentForecastAEP.CssClass = "hw-dashboard-link disabled";
                //lbSalesAndTerms.CssClass = "hw-dashboard-link disabled";
                //lbHospitalCompare.CssClass = "hw-dashboard-link disabled";
                //lbProviderScorecard.CssClass = "hw-dashboard-link disabled";
                //lbMPF.CssClass = "hw-dashboard-link disabled";
                // lbOOPC.CssClass = "hw-dashboard-link disabled";
            }
        }
        protected void lbExternal_Click(object sender, EventArgs e)
        {
            LinkButton lbExternal = sender as LinkButton;
            PT.InsertDataIntoDB(lbExternal.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbExternal.CommandName);
            string redirectToPage = string.Empty;
            redirectToPage = "~/Pages/" + lbExternal.CommandName;
            Response.Redirect(redirectToPage);
        }
        protected void Dashboard_Click(object sender, EventArgs e)
        {
            LinkButton lbExternal = sender as LinkButton;
            PT.InsertDataIntoDB(lbExternal.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbExternal.CommandName);
            Session["dashboardURL"] = null;
            Session["dashboardURL"] = lbExternal.CommandArgument;
            Session["ActiveLink"] = null;
            Session["ActiveLink"] = lbExternal.ID.ToString() + "Li";
            Session["DashboardHeight"] = null;
            if (lbExternal.CommandName != "")
            {
                Session["DashboardHeight"] = lbExternal.CommandName;
            }
            Response.Redirect("~/Pages/Dashboard.aspx");
        }
        protected void Whitepaper_Click(object sender, EventArgs e)
        {
            LinkButton lbExternal = sender as LinkButton;
            PT.InsertDataIntoDB(lbExternal.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbExternal.CommandName);
            Session["whitepaperURL"] = null;
            Session["whitepaperURL"] = lbExternal.CommandArgument;
            Session["ActiveLink"] = null;
            Session["ActiveLink"] = lbExternal.ID.ToString() + "Li";
            Session["WhitepaperHeight"] = null;
            Session["WhitepaperHeight"] = lbExternal.CommandName;
            Response.Redirect("~/Pages/Whitepaper.aspx");
        }
        protected void lbDataCube_Click(object sender, EventArgs e)
        {
            string userName = Session["UserName"].ToString();
            LinkButton lbDataCube = sender as LinkButton;
            PT.InsertDataIntoDB(lbDataCube.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbDataCube.CommandName);
            string redirectToCube = "https://datacube.analytics-hub.com/Pages/" + Constants.DataCubeSiteName + "/" + lbDataCube.CommandName + ".aspx?un=" + userName;
            Response.Redirect(redirectToCube);
        }
        protected void lbDownload_Click(object sender, EventArgs e)
        {
            LinkButton lbBenefitCompetitiveGrid = sender as LinkButton;
            DownloadTracker downloadTracker = new DownloadTracker();
            DataTable dataTable = downloadTracker.GetUploadedReports(lbBenefitCompetitiveGrid.CommandArgument.ToString());

            if (dataTable.Rows.Count > 0)
            {
                PT.InsertDataIntoDB(lbBenefitCompetitiveGrid.CommandName, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbBenefitCompetitiveGrid.CommandArgument.Replace(" ",string.Empty));
                string filename = dataTable.Rows[0][0].ToString();
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename="+ filename );
                string destPath = Server.MapPath("/Documents/UploadFiles/"+filename);
                Response.TransmitFile(destPath);               
                Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "callwarning();", true);
            }
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            string userName = Session["UserName"].ToString();
            string redirectToCube = "https://datacube.analytics-hub.com/Pages/TEG/Census.aspx?un=" + userName;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('" + redirectToCube + "' ,'_blank');", true);

        }
        protected void Clickhere_Click(object sender, EventArgs e)
        {
            EmailServices emailServices = new EmailServices();
            string RenewSubscriptionEmail = ConfigurationManager.AppSettings["RenewSubscriptionEmail"].ToString();
            emailServices.SendRenewSubscriptiondetails(RenewSubscriptionEmail, Session["UserName"].ToString());
            Response.Redirect("~/Default.aspx");
        }
        protected void lbSocio_Click(object sender, EventArgs e)
        {
            LinkButton lbSocio = sender as LinkButton;
            PT.InsertDataIntoDB(lbSocio.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbSocio.CommandArgument);
            string redirectToCube = lbSocio.CommandName.ToString();
            Response.Redirect(redirectToCube);
        }

    }
}