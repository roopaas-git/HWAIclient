using CommonUtility;
using DataUtility;
using HealthWorks.Content.BO;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class HealthMaster : System.Web.UI.MasterPage
    {
        #region variables
        UserDetails userDetails;
        PageTracking PT = new PageTracking();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Hdn_username.Value = Session["UserName"].ToString();
                userDetails = new UserDetails();
                string SessionID = userDetails.GetSessionID(Session["UserName"].ToString());
                if (SessionID != null && SessionID != Session["SessionID"].ToString())
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        if (Session["FirstName"] == null)
                        {
                            Bind_User_FirstName();
                            lblUserName.Text = Session["FirstName"].ToString();
                        }
                        else
                        {
                            lblUserName.Text = Session["FirstName"].ToString();
                        }

                        CheckAccountType();
                        EnableDisableLinks();
                    }
                }
            }
        }
        private void EnableDisableLinks()
        {
            string domainName = Session["UserName"].ToString().Split('@')[1];

            if (domainName != Constants.teganalytics && domainName != Constants.healthworksai)
            {
                SupportLi.Visible = false;
            }
        }
        private void Bind_User_FirstName()
        {
            userDetails = new UserDetails();

            Session["FirstName"] = userDetails.GetFirstName(Session["UserName"].ToString());
        }
        private void CheckAccountType()
        {
            if (Constants.AccountType == Constants.platinum)
            {
                //id_PerformanceMgmt1.Attributes.Add("class", "disabled");
                //liPostAEP.Attributes.Add("class", "disabled");
                //lbEnrollmentForecastAEP.Attributes.Add("class", "disabled");
                //lbSalesAndTerms.Attributes.Add("class", "disabled");
                //Hospital.Attributes.Add("class", "disabled");
                //ProviderSC.Attributes.Add("class", "disabled");
            }
            if (Constants.AccountType == Constants.gold)
            {
                //id_PerformanceMgmt1.Attributes.Add("class", "disabled");
                //liPostAEP.Attributes.Add("class", "disabled");
                //lbEnrollmentForecastAEP.Attributes.Add("class", "disabled");
                //lbSalesAndTerms.Attributes.Add("class", "disabled");
                //Hospital.Attributes.Add("class", "disabled");
                //ProviderSC.Attributes.Add("class", "disabled");
                //OOpcSimulator.Attributes.Add("class", "disabled");
                //MPFSimulator.Attributes.Add("class","disabled");
            }
            if (Constants.AccountType == Constants.silver)
            {

                //id_PerformanceMgmt1.Attributes.Add("class", "disabled");
                //liPostAEP.Attributes.Add("class", "disabled");
                //lbEnrollmentForecastAEP.Attributes.Add("class", "disabled");
                //lbSalesAndTerms.Attributes.Add("class", "disabled");
                //Hospital.Attributes.Add("class", "disabled");
                //ProviderSC.Attributes.Add("class", "disabled");
                //OOpcSimulator.Attributes.Add("class", "disabled");
                //MPFSimulator.Attributes.Add("class", "disabled");
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
        protected void lbDataCube_Click(object sender, EventArgs e)
        {
            string userName = Session["UserName"].ToString();
            LinkButton lbDataCube = sender as LinkButton;
            PT.InsertDataIntoDB(lbDataCube.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbDataCube.CommandName);

            string redirectToCube = "https://datacube.analytics-hub.com/Pages/" + Constants.DataCubeSiteName + "/" + lbDataCube.CommandName + ".aspx?un=" + userName;
            Response.Redirect(redirectToCube);
        }
        protected void lbInternal_Click(object sender, EventArgs e)
        {
            LinkButton lbInternal = sender as LinkButton;

            string redirectToPage = "~/Pages/" + lbInternal.CommandArgument + ".aspx";
            Response.Redirect(redirectToPage);

        }
        protected void lbChangePassWord_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/ChangePassword.aspx");
        }
        protected void lbLogout_Click(object sender, EventArgs e)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.LogoutSession(Session["UserName"].ToString());
            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }
        protected void lbFullScreen_Click(object sender, EventArgs e)
        {
            Response.Redirect("FullScreenView.aspx");
        }
        protected void lbDownload_Click(object sender, EventArgs e)
        {
            LinkButton lbBenefitCompetitiveGrid = sender as LinkButton;
            DownloadTracker downloadTracker = new DownloadTracker();
            DataTable dataTable = downloadTracker.GetUploadedReports(lbBenefitCompetitiveGrid.CommandArgument.ToString());

            if (dataTable.Rows.Count > 0)
            {
                PT.InsertDataIntoDB(lbBenefitCompetitiveGrid.CommandName, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbBenefitCompetitiveGrid.CommandArgument.Replace(" ", string.Empty));

                string filename = dataTable.Rows[0][0].ToString();
                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                string destPath = Server.MapPath("/Documents/UploadFiles/" + filename);
                Response.TransmitFile(destPath);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "callwarning();", true);
            }
        }
        protected void lbSocio_Click(object sender, EventArgs e)
        {
            LinkButton lbSocio = sender as LinkButton;
            PT.InsertDataIntoDB(lbSocio.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbSocio.CommandArgument);
            string redirectToCube = lbSocio.CommandName.ToString();
            Response.Redirect(redirectToCube);
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
        protected void WhitepaperPDf_Click(object sender, EventArgs e)
        {
            LinkButton lbExternal = sender as LinkButton;
            PT.InsertDataIntoDB(lbExternal.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbExternal.CommandName);
            Session["PDFURL"] = null;
            Session["PDFURL"] = lbExternal.CommandArgument;
            Session["ActiveLink"] = null;
            Session["ActiveLink"] = lbExternal.ID.ToString() + "Li";
            Response.Redirect("~/Pages/PdfView.aspx");
        }
    }
}