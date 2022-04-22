using CommonUtility;
using BusinessUtility;
using DataUtility;
using HealthWorks.Content.BO;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;

namespace HealthWorks.Pages
{
    public partial class EnrollmentScenarioList : System.Web.UI.Page
    {
        EnrollmentScenarioDetailMethods enrollmentScenarioDetailMethods;
        private DataTable dataTable = new DataTable();
        public static int isFirst = 0;
        PageTracking PT = new PageTracking();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ClientId"] = ConfigurationManager.AppSettings["ClientId"];

            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Bind_Scenario();
                        Bind_UserName();
                    }
                    Active_DeactiveLinks();
                    this.Validate();
                }

                //if (Session["UserName"].ToString().Contains("teganalytics.com") || Session["UserName"].ToString().Contains("healthworksai.com"))
                //{
                //    btnUpload.Visible = true;

                //}
                //else
                //{
                //    btnUpload.Visible = false;
                //}
            }
            catch (Exception ex1)
            {

            }
        }
        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ProductIntelC2L5li"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
            }
        }
        public void Bind_Scenario()
        {
            string UserName = Session["UserName"].ToString();
            enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
            dataTable = enrollmentScenarioDetailMethods.LoadEnrollmentScenarios(UserName, Convert.ToInt32(Session["ClientId"]));
            grdScenario.DataSource = dataTable;
            grdScenario.DataBind();
            Session["BenefitScenarios"] = dataTable;

            ViewState["grdScenario"] = dataTable;
            ViewState["grdScenarioSortDirection"] = "Asc";

            if (Session["EnrollmentSimulatorPlanListScenarioID"] != null)
            {
                grdScenario.AllowPaging = false;
                grdScenario.DataBind();
                string ScenarioID = Session["EnrollmentSimulatorPlanListScenarioID"].ToString();
                int rowindex = 0;
                for (int i = 0; i < grdScenario.Rows.Count; i++)
                {
                    GridViewRow row = grdScenario.Rows[i];
                    Label lblId = (Label)row.FindControl("lblId");
                    if (lblId != null && lblId.Text == ScenarioID)
                    {
                        rowindex = row.RowIndex;
                        grdScenario.AllowPaging = true;
                        grdScenario.PageIndex = (rowindex / 6);
                        grdScenario.DataBind();
                        for (int j = 0; j < grdScenario.Rows.Count; j++)
                        {
                            Label lblId1 = (Label)grdScenario.Rows[j].FindControl("lblId");
                            if (lblId1 != null && lblId1.Text == ScenarioID)
                            {
                                RadioButton rb = (RadioButton)grdScenario.Rows[j].FindControl("rbSelect");
                                rb.Checked = true;
                                Label LblStatus = (Label)row.FindControl("LblStatus");
                                UpdateTopProgressBar(Convert.ToInt32(ScenarioID), LblStatus.Text.ToLower().Replace(" ", "_").Trim());
                                // btnUpload.Enabled = true;

                                LinkButton Lnk_output = (LinkButton)grdScenario.Rows[j].FindControl("Lnk_output");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "DoPostBack", "openEnrollmentSimulatorOutput('" + Lnk_output.ClientID + "')", true);

                                break;
                            }
                        }
                    }
                }
                Session["EnrollmentSimulatorPlanListScenarioID"] = null;
            }
        }

        public void Bind_UserName()
        {
            enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
            dataTable = enrollmentScenarioDetailMethods.BindEnrollmentUserName(Session["UserName"].ToString());
            ddlUser.DataSource = dataTable;
            ddlUser.DataTextField = "UserName";
            ddlUser.DataBind();
        }
        public void Bind_UserDetails()
        {
            var userDomainFormat = "@" + Session["UserName"].ToString().Split('@')[1].ToLower().ToString();

            if (userDomainFormat == "@teganalytics.com" || userDomainFormat == "@healthworksai.com")
            {
                ddlUserDetails.Visible = true;
                requiredUserDetails.Visible = true;

                ddlUserDetails.Items.Clear();
                enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
                dataTable = enrollmentScenarioDetailMethods.BindEnrollmentUserDetails();
                ddlUserDetails.DataSource = dataTable;
                ddlUserDetails.DataTextField = "FirstName";
                ddlUserDetails.DataValueField = "UserName";
                ddlUserDetails.DataBind();

                ddlUserDetails.SelectedValue = Session["UserName"].ToString();
            }
            else
            {
                ddlUserDetails.Visible = false;
                requiredUserDetails.Visible = false;
            }

        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            // PT.InsertDataIntoDB("BenefitSimulatorPlanList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentPlanList.aspx");
            Response.Redirect("~/Pages/EnrollmentPlanList.aspx");
        }
        protected void rbSelect_CheckedChanged(object sender, EventArgs e)
        {

            btnLoad.Enabled = true;
            // btnUpload.Enabled = false;
            int rowIndex = Convert.ToInt32(((sender as RadioButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = grdScenario.Rows[rowIndex];

            RadioButton rb = (RadioButton)row.FindControl("rbSelect");
            if (rb.Checked)
            {
                id_nav.Style.Add("pointer-events", "auto");
                Session["EnrollmentSimulatorScenarioID"] = (row.FindControl("lblId") as Label).Text;
                Session["EnrollmentSimulatorScenarioName"] = (row.FindControl("LblScenarioName") as Label).Text;
                // EnableTopMenuLinks(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));

                Label LblStatus = (Label)row.FindControl("LblStatus");

                UpdateTopProgressBar(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()), LblStatus.Text.ToLower().Replace(" ", "_").Trim());

                //if (LblStatus.Text == "Processing")
                //{
                //    btnUpload.Enabled = true;
                //}
                //else
                //{
                //    btnUpload.Enabled = false;

                //}
                if (EnableDisableButtons(LblStatus.Text.Replace(" ", "_"), false))
                {
                    btnLoad.Enabled = true;
                }
                else
                {
                    btnLoad.Enabled = false;
                }

            }
        }
        private void UpdateTopProgressBar(int scenarioId, string scenarioStatus)
        {
            Session["ProgressBarStatus"] = null;

            CreateScenario.Attributes["class"] = "";
            DownloadPBP.Attributes["class"] = "";
            UploadPBP.Attributes["class"] = "";
            ResultsReady.Attributes["class"] = "";

            LBScenarios.Enabled = false;
            LBPlans.Enabled = false;
            LBPlans_UploadPBP.Enabled = false;
            LBPlans_ResultsReady.Enabled = false;

            switch (scenarioStatus)
            {
                case "not_submitted":

                    EnrollmentPlansUserInputsMethods enrollmentPlansUserInputsMethods = new EnrollmentPlansUserInputsMethods();
                    DataTable plansDT = enrollmentPlansUserInputsMethods.GetEnrollmentSimulatorSavedPlans(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));
                    if (plansDT.Rows.Count > 0)
                    {
                        Session["ProgressBarStatus"] = 2;
                        CreateScenario.Attributes["class"] = "active";
                        DownloadPBP.Attributes["class"] = "active";
                        LBPlans.Enabled = true;
                        LBPlans_UploadPBP.Enabled = true;
                    }
                    else
                    {
                        Session["ProgressBarStatus"] = 1;
                        CreateScenario.Attributes["class"] = "active";
                        LBPlans.Enabled = true;                        
                    }
                    break;

                case "processing":
                    Session["ProgressBarStatus"] = 3;
                    CreateScenario.Attributes["class"] = "active";
                    DownloadPBP.Attributes["class"] = "active";
                    UploadPBP.Attributes["class"] = "active";
                    break;

                case "ready":
                    Session["ProgressBarStatus"] = 4;
                    CreateScenario.Attributes["class"] = "active";
                    DownloadPBP.Attributes["class"] = "active";
                    UploadPBP.Attributes["class"] = "active";
                    ResultsReady.Attributes["class"] = "active";
                    break;

                default:
                    break;
            }
        }
       
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            grdScenario.EditIndex = e.NewEditIndex;
            Bind_Scenario();
            if (Session["EnrollmentSimulatorScenarioID"] != null)
            {
                btnLoad.Enabled = false;
                Session["EnrollmentSimulatorScenarioID"] = null;
                Session["EnrollmentSimulatorScenarioName"] = null;
                id_nav.Style.Add("pointer-events", "none");
                //id_Quick.Style.Add("pointer-events", "none");
                //id_Simulate.Style.Add("pointer-events", "none");
            }
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            grdScenario.EditIndex = -1;
            Bind_Scenario();

            if (Session["EnrollmentSimulatorScenarioID"] != null)
            {
                btnLoad.Enabled = false;
                Session["EnrollmentSimulatorScenarioID"] = null;
                Session["EnrollmentSimulatorScenarioName"] = null;
                id_nav.Style.Add("pointer-events", "none");
                //id_Quick.Style.Add("pointer-events", "none");
                //id_Simulate.Style.Add("pointer-events", "none");
            }
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int alert = 0;
            enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
            Label lblSid = (Label)grdScenario.Rows[e.RowIndex].FindControl("lblId");
            TextBox TxtScenarioName = (TextBox)grdScenario.Rows[e.RowIndex].FindControl("txtScenarioName");
            TextBox TxtDescription = (TextBox)grdScenario.Rows[e.RowIndex].FindControl("txtDescription");
            string oldValue = ((Label)grdScenario.Rows[e.RowIndex].FindControl("oldValue")).Text;
            if (TxtScenarioName.Text != oldValue)
            {
                alert = 1;
            }
            string CreatedBy = Session["UserName"].ToString();
            int result = enrollmentScenarioDetailMethods.UpdateEnrollmentScenario(Convert.ToInt32(lblSid.Text.ToString()), TxtScenarioName.Text.ToString(), TxtDescription.Text.ToString(), CreatedBy, alert, Convert.ToInt32(Session["ClientId"]));
            if (result == 1 && alert == 1)
            {
                string message = "Scenario name already exists.Please provide a different name.";
                string script = "window.onload = function(){ alert('"; script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", script, true);
            }

            grdScenario.EditIndex = -1;
            Bind_Scenario();

            if (Session["EnrollmentSimulatorScenarioID"] != null)
            {
                btnLoad.Enabled = false;
                Session["EnrollmentSimulatorScenarioID"] = null;
                Session["EnrollmentSimulatorScenarioName"] = null;
                id_nav.Style.Add("pointer-events", "none");
                //id_Quick.Style.Add("pointer-events", "none");
                //id_Simulate.Style.Add("pointer-events", "none");
            }
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
            int row = e.RowIndex;
            Label lbl = (Label)grdScenario.Rows[row].FindControl("LblId");
            Label lblScenarioName = (Label)grdScenario.Rows[row].FindControl("LblScenarioName");
            enrollmentScenarioDetailMethods.DeleteEnrollmentScenario(Convert.ToInt32(lbl.Text));
            Bind_Scenario();

            if (Session["EnrollmentSimulatorScenarioID"] != null)
            {
                btnLoad.Enabled = false;
                Session["EnrollmentSimulatorScenarioID"] = null;
                Session["EnrollmentSimulatorScenarioName"] = null;
                id_nav.Style.Add("pointer-events", "none");
                ResetTopProgressBar();
                //id_Quick.Style.Add("pointer-events", "none");
                //id_Simulate.Style.Add("pointer-events", "none");
            }
        }
        protected void grdScenario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Label LblShareCount = (Label)e.Row.FindControl("LblShareCount");
                LinkButton Lnk_Share = (LinkButton)e.Row.FindControl("Lnk_Share");
                //if (LblShareCount != null && Lnk_Share != null)
                //{
                //    if (LblShareCount.Text != "0")
                //    {
                //        Lnk_Share.Enabled = true;
                //    }
                //    else
                //    {
                //        Lnk_Share.Enabled = false;
                //        Lnk_Share.Style.Add("pointer-events", "none");
                //        Lnk_Share.Style.Add("color", "gray");
                //        Lnk_Share.Style.Add("opacity", "0.2");
                //    }
                //}


                Label LblStatus = (Label)e.Row.FindControl("LblStatus");

                LinkButton Lnk_output = (LinkButton)e.Row.FindControl("Lnk_output");
                if (LblStatus != null && Lnk_output != null)
                {
                    if (LblStatus.Text == "3")
                    {
                        Lnk_output.Enabled = true;
                        Lnk_Share.Enabled = true;
                    }
                    else
                    {
                        Lnk_output.Enabled = false;
                        Lnk_output.Style.Add("pointer-events", "none");
                        Lnk_output.Style.Add("color", "gray");
                        Lnk_output.Style.Add("opacity", "0.2");

                        Lnk_Share.Enabled = false;
                        Lnk_Share.Style.Add("pointer-events", "none");
                        Lnk_Share.Style.Add("color", "gray");
                        Lnk_Share.Style.Add("opacity", "0.2");
                    }
                }
                LblStatus.Text = DisplayProcessState(Convert.ToByte(LblStatus.Text));

                Label LblSubmittedBy = (Label)e.Row.FindControl("LblSubmittedBy");
                Label LblCreatedBy = (Label)e.Row.FindControl("LblCreatedBy");
                LinkButton Lnk_Delete = (LinkButton)e.Row.FindControl("Lnk_Delete");

                if (LblCreatedBy.Text != Session["UserName"].ToString())
                {
                    var userDomainFormat = "@" + Session["UserName"].ToString().Split('@')[1].ToLower().ToString();

                    if (userDomainFormat == "@healthworksai.com")
                    {
                        Lnk_Delete.Enabled = true;
                    }
                    else
                    {
                        Lnk_Delete.Enabled = false;
                        Lnk_Delete.Style.Add("pointer-events", "none");
                        Lnk_Delete.Style.Add("color", "gray");
                        Lnk_Delete.Style.Add("opacity", "0.2");
                    }

                    Lnk_Share.Enabled = false;
                    Lnk_Share.Style.Add("pointer-events", "none");
                    Lnk_Share.Style.Add("color", "gray");
                    Lnk_Share.Style.Add("opacity", "0.2");
                }
                else
                {
                    Lnk_Delete.Enabled = true;
                    Lnk_Share.Enabled = true;
                }

                Label Pre_AEP_Enrolment = (Label)e.Row.FindControl("Pre_AEP_Enrolment");
                if (Pre_AEP_Enrolment.Text != "")
                    Pre_AEP_Enrolment.Text = Convert.ToInt32(Pre_AEP_Enrolment.Text).ToString("N0");

                Label Post_AEP_Enrolment = (Label)e.Row.FindControl("Post_AEP_Enrolment");
                if (Post_AEP_Enrolment.Text != "")
                    Post_AEP_Enrolment.Text = Convert.ToInt32(Post_AEP_Enrolment.Text).ToString("N0");

                Label Simulated_Results = (Label)e.Row.FindControl("Simulated_Results");
                if (Simulated_Results.Text != "")
                    Simulated_Results.Text = Convert.ToInt32(Simulated_Results.Text).ToString("N0");
            }
        }
        protected void lbCreate_Click(object sender, EventArgs e)//that plus sign at the top right
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
            txtScenario.Text = "";
            txtScenarioDesc.Text = "";
            scenarioExists.Visible = false;
            Bind_UserDetails();
        }
        protected void btnPopUpSave_Click(object sender, EventArgs e)//willsave the things to database
        {
            enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
            int result;
            string Scenario = txtScenario.Text.ToString();
            string ScenarioDesc = txtScenarioDesc.Text.ToString();
            string CreatedBy = Session["UserName"].ToString();
            string UserName = string.Empty;

            var userDomainFormat = "@" + Session["UserName"].ToString().Split('@')[1].ToLower().ToString();

            if (userDomainFormat == "@teganalytics.com" || userDomainFormat == "@healthworksai.com")
            {
                UserName = ddlUserDetails.SelectedValue.ToString();
            }
            else
            {
                UserName = Session["UserName"].ToString();
            }

            int Status = 1;
            Session["UserFullName"] = UserName;
            Session["ProcessStatus"] = Status;
            Session["ProgressBarStatus"] = null;
            result = enrollmentScenarioDetailMethods.InsertEnrollmentScenario(Scenario, ScenarioDesc, CreatedBy, "-", "-", UserName, Status, 0, Convert.ToInt32(Session["ClientId"]));

            if (result != 0)
            {
                scenarioExists.Visible = false;
                Session["EnrollmentSimulatorScenarioID"] = result;
                Session["EnrollmentSimulatorScenarioName"] = txtScenario.Text;
                txtScenario.Text = "";
                txtScenarioDesc.Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeModal2();", true);
                PT.InsertDataIntoDB("BenefitSimulatorPlanList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentPlanList.aspx");
                Response.Redirect("~/Pages/EnrollmentPlanList.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
                scenarioExists.Visible = true;
            }
            Bind_Scenario();
        }
        protected void grdScenario_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {


        }
        protected void Share_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);

            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = grdScenario.Rows[rowIndex];
            ScenarioNameS.Text = (row.FindControl("LblScenarioName") as Label).Text;
            ScenarioDescS.Text = (row.FindControl("LblDescription") as Label).Text;
            string ScenarioID = (row.FindControl("lblId") as Label).Text;
            Session["SharedScenario"] = null;
            Session["SharedScenario"] = ScenarioID;

            //  Bind_UserName();
            Bind_Scenario();
            txtMessage.Text = "";
            ddlUser.SelectedIndex = 0;


        }
        protected void Lnk_output(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = grdScenario.Rows[rowIndex];
            string ScenarioID = (row.FindControl("lblId") as Label).Text;

            enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
            DataTable DT = enrollmentScenarioDetailMethods.GetEnrollmentSimResults(Convert.ToInt32(ScenarioID));
            GrdOutput.DataSource = DT;
            GrdOutput.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal5()", true);
            //ScenarioNameS.Text = (row.FindControl("LblScenarioName") as Label).Text;
            //ScenarioDescS.Text = (row.FindControl("LblDescription") as Label).Text;
            //
            //Session["SharedScenario"] = null;
            //Session["SharedScenario"] = ScenarioID;

            ////  Bind_UserName();
            //Bind_Scenario();
            //txtMessage.Text = "";
            //ddlUser.SelectedIndex = 0;


        }
        protected void btnPopUpShare_Click(object sender, EventArgs e)
        {
            EnrollmentScenarioDetailMethods enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
            string SharedUser = ddlUser.SelectedValue.ToString();
            string SharedDesc = txtMessage.Text.ToString();
            string CreatedBy = Session["UserName"].ToString();
            string SName = ScenarioNameS.Text;
            enrollmentScenarioDetailMethods.RemoveAlreadySharedEnrollmentScenario(SharedUser, SName);
            var submittedByEmail = enrollmentScenarioDetailMethods.GetEnrollmentScenario(Convert.ToInt32(Session["SharedScenario"].ToString()));

            int NewScenarioID = enrollmentScenarioDetailMethods.InsertEnrollmentScenario(ScenarioNameS.Text.ToString(), ScenarioDescS.Text.ToString(), SharedUser, CreatedBy, SharedDesc, submittedByEmail.Rows[0]["SubmittedBy"].ToString(), 3, 1, Convert.ToInt32(Session["ClientId"]));
            if (NewScenarioID != 0)
            {
                if (Session["SharedScenario"] != null)
                {
                    int OldScenarioID = Convert.ToInt32(Session["SharedScenario"].ToString());
                    enrollmentScenarioDetailMethods.ShareEnrollmentScenario(OldScenarioID, NewScenarioID);
                    string message = "Shared the plan sucessfully.";
                    string script = "window.onload = function(){ alert('"; script += message;
                    script += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", script, true);
                }
            }
            Bind_Scenario();
            txtMessage.Text = "";
        }
        protected void LBScenarios_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("BenefitSimulatorScenarioList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentScenarioList.aspx");
                Response.Redirect("~/Pages/EnrollmentScenarioList.aspx");
            }
            catch (Exception ex1)
            {

            }
        }
        protected void LBPlans_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("BenefitSimulatorPlanList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentPlanList.aspx");
                Response.Redirect("~/Pages/EnrollmentPlanList.aspx");
            }
            catch (Exception ex1)
            {

            }
        }
        protected void LBQuickAccess_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                if (Session["EnrollmentSimulatorScenarioID"] != null)
                {
                    DataTable dataTable = new DataTable();
                    EnrollmentPlansUserInputs objEnrollmentPlansUserInputs = new EnrollmentPlansUserInputs();
                    EnrollmentPlansUserInputsMethods enrollmentPlansUserInputsMethods = new EnrollmentPlansUserInputsMethods();
                    dataTable = enrollmentPlansUserInputsMethods.GetEnrollmentSimulatorSavedPlans(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));
                    if (dataTable.Rows.Count > 0)
                    {
                        objEnrollmentPlansUserInputs.MarketId = Convert.ToInt32(dataTable.Rows[0]["MarketId"]);
                        objEnrollmentPlansUserInputs.SubMarketId = Convert.ToInt32(dataTable.Rows[0]["SubMarketId"]);
                        objEnrollmentPlansUserInputs.StateIds = dataTable.Rows[0]["StateIds"].ToString();
                        objEnrollmentPlansUserInputs.SalesRegionIds = dataTable.Rows[0]["SalesRegionIds"].ToString();
                        objEnrollmentPlansUserInputs.CountyIds = dataTable.Rows[0]["CountyIds"].ToString();
                        objEnrollmentPlansUserInputs.FootprintIds = dataTable.Rows[0]["FootprintIds"].ToString();
                        objEnrollmentPlansUserInputs.PlanCategoryIds = dataTable.Rows[0]["PlanCategoryIds"].ToString();
                        objEnrollmentPlansUserInputs.PremiumIds = dataTable.Rows[0]["PremiumIds"].ToString();
                        objEnrollmentPlansUserInputs.PlanTypeIds = dataTable.Rows[0]["PlanTypeIds"].ToString();

                        objEnrollmentPlansUserInputs.ScenarioID = Convert.ToInt32(dataTable.Rows[0]["ScenarioID"]);
                        objEnrollmentPlansUserInputs.BidId = dataTable.Rows[0]["BidID"].ToString();
                        objEnrollmentPlansUserInputs.PlanName = dataTable.Rows[0]["PlanName"].ToString();
                        objEnrollmentPlansUserInputs.BidLevelStateIds = dataTable.Rows[0]["BidLevelStateIds"].ToString();
                        objEnrollmentPlansUserInputs.BidLevelCountyIds = dataTable.Rows[0]["BidLevelCountyIds"].ToString();

                        Session["EnrollmentSimulatorSavedPlan"] = objEnrollmentPlansUserInputs;

                        PT.InsertDataIntoDB("BenefitSimulatorQuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentQuickAccessSimulation.aspx");
                        Response.Redirect("~/Pages/EnrollmentQuickAccessSimulation.aspx");
                    }
                    else
                    {
                        Session["EnrollmentSimulatorSavedPlan"] = null;
                    }
                }
                              
            }
            catch (Exception ex1)
            {

            }
        }
        protected void LBSimulatedOutput_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("EnrollmentResults", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManageSimulatedOutput.aspx");
                Response.Redirect("~/Pages/ManageSimulatedOutput.aspx");
            }
            catch (Exception ex1)
            {

            }
        }
        public string DisplayProcessState(byte processState)
        {
            string updatedstatus = string.Empty;

            switch (processState)
            {
                case 1:
                    updatedstatus = (ProcessStatus.Not_Submitted).ToString().Replace("_", " ");
                    break;

                case 2:
                    updatedstatus = (ProcessStatus.Processing).ToString();
                    break;

                case 3:
                    updatedstatus = (ProcessStatus.Ready).ToString();
                    break;

                default:
                    updatedstatus = "-";
                    break;
            }

            return updatedstatus;
        }
        public bool EnableDisableButtons(dynamic processState, bool isAnalyze)
        {
            byte enumProcessState = Convert.ToByte((ProcessStatus)Enum.Parse(typeof(ProcessStatus), processState));

            bool updatedstatus = false;
            if (isAnalyze)
            {
                switch (enumProcessState)
                {
                    case 1:
                    case 2:
                        updatedstatus = false;
                        break;

                    case 3:
                        updatedstatus = true;
                        break;

                    default:
                        updatedstatus = false;
                        break;
                }
            }
            else
            {
                switch (enumProcessState)
                {
                    case 1:
                        updatedstatus = true;
                        break;

                    case 2:
                    case 3:
                    default:
                        updatedstatus = false;
                        break;
                }
            }

            return updatedstatus;
        }

        public enum ProcessStatus
        {
            Not_Submitted​ = 1,
            Processing = 2,
            Ready = 3
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            PT.InsertDataIntoDB("BenefitQuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentQuickAccess.aspx");
            Response.Redirect("~/Pages/EnrollmentQuickAccess.aspx");
        }
        protected void GrdOutput_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Pre_AEP_Enrolment = (Label)e.Row.FindControl("Pre_AEP_Enrolment");
                Pre_AEP_Enrolment.Text = Convert.ToInt32(Pre_AEP_Enrolment.Text).ToString("N0");

                Label Post_AEP_Enrolment = (Label)e.Row.FindControl("Post_AEP_Enrolment");
                Post_AEP_Enrolment.Text = Convert.ToInt32(Post_AEP_Enrolment.Text).ToString("N0");

                Label Simulated_Results = (Label)e.Row.FindControl("Simulated_Results");
                Simulated_Results.Text = Convert.ToInt32(Simulated_Results.Text).ToString("N0");

                Label Simulated_Delta = (Label)e.Row.FindControl("Simulated_Delta");
                Simulated_Delta.Text = Convert.ToInt32(Simulated_Delta.Text).ToString("N0");

                Label Growth = (Label)e.Row.FindControl("Growth");
                Growth.Text = Convert.ToInt32(Growth.Text).ToString("N0");
                //Growth.Text = Convert.ToDouble(Growth.Text).ToString("P1");

            }
        }
        protected void GrdOutput_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string x = "~/" + e.CommandArgument.ToString();
                string filename = Path.GetFileName(x);
                string FName = Server.MapPath(x);
                Response.Clear();
                Response.ContentType = "application/*.*";
                Response.AppendHeader("Content-Disposition", "attachment; filename=\" " + filename + "\"");
                Response.TransmitFile(FName);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }
        }
        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            DT = (DataTable)Session["BenefitScenarios"];

            string FilterExpression = string.Format("CONVERT({0}, System.String) like '%{1}%'", ddlFilter.SelectedValue, txt_search.Text.Trim());
            DataView dv = new DataView(DT);
            if (txt_search.Text.Trim() != string.Empty)
            {
                dv.RowFilter = FilterExpression;
            }

            DT = dv.ToTable();
            grdScenario.DataSource = DT;
            grdScenario.DataBind();
            ViewState["grdScenario"] = DT;
            ViewState["grdScenarioSortDirection"] = "Asc";
            btnLoad.Enabled = false;

            // btnUpload.Enabled = false;
        }
        private void SortGridview(GridView gridView, GridViewSortEventArgs e, out SortDirection sortDirection, out string sortField)
        {
            sortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null && gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (sortField == gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"] == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }

                gridView.Attributes["CurrentSortField"] = sortField;
                gridView.Attributes["CurrentSortDirection"] = (sortDirection == SortDirection.Ascending ? "ASC" : "DESC");
            }
        }
        protected void grdScenario_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string sortField = string.Empty;

            SortGridview((GridView)sender, e, out sortDirection, out sortField);
            string strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";

            DataTable dtrslt = (DataTable)ViewState["grdScenario"];
            dtrslt.DefaultView.Sort = e.SortExpression + " " + strSortDirection;
            dtrslt = dtrslt.DefaultView.ToTable();
            grdScenario.DataSource = dtrslt;
            grdScenario.DataBind();
        }
        protected void grdScenario_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridView grdView = (GridView)sender;

            if (grdView.Attributes["CurrentSortField"] != null && grdView.Attributes["CurrentSortDirection"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    foreach (TableCell tableCell in e.Row.Cells)
                    {
                        if (tableCell.HasControls())
                        {
                            LinkButton sortLinkButton = null;
                            if (tableCell.Controls[0] is LinkButton)
                            {
                                sortLinkButton = (LinkButton)tableCell.Controls[0];
                            }

                            if (sortLinkButton != null && grdView.Attributes["CurrentSortField"] == sortLinkButton.CommandArgument)
                            {
                                Image image = new Image();
                                if (grdView.Attributes["CurrentSortDirection"] == "ASC")
                                {
                                    image.ImageUrl = "~/dist/Images/up_arrow.png";
                                }
                                else
                                {
                                    image.ImageUrl = "~/dist/Images/down_arrow.png";
                                }
                                // tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                                tableCell.Controls.Add(image);
                            }
                        }
                    }
                }
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdScenario.PageIndex = e.NewPageIndex;
            Bind_Scenario();
            txt_search_TextChanged(this, null);
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txt_search.Text = string.Empty;
        }

        private void ResetTopProgressBar()
        {
            Session["ProgressBarStatus"] = null;

            CreateScenario.Attributes["class"] = "";
            DownloadPBP.Attributes["class"] = "";
            UploadPBP.Attributes["class"] = "";
            ResultsReady.Attributes["class"] = "";

            LBScenarios.Enabled = false;
            LBPlans.Enabled = false;
            LBPlans_UploadPBP.Enabled = false;
            LBPlans_ResultsReady.Enabled = false;

        }
    }
}
