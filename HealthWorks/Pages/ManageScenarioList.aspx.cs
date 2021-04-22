using CommonUtility;
using BusinessUtility;
using DataUtility;
using HealthWorks.Content.BO;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace HealthWorks.Pages
{
    public partial class ManageScenarioList : System.Web.UI.Page
    {
        CreateScenarioDetailMethods createScenarioDetailMethods;
        private DataTable dataTable = new DataTable();
        public static int isFirst = 0;
        PageTracking PT = new PageTracking();
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
                    if (!IsPostBack)
                    {
                        Bind_Scenario();
                        Bind_UserName();
                    }
                    Active_DeactiveLinks();
                    this.Validate();
                }
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
            createScenarioDetailMethods = new CreateScenarioDetailMethods();
            dataTable = createScenarioDetailMethods.LoadScenarios(UserName);
            grdScenario.DataSource = dataTable;
            grdScenario.DataBind();

        }
        public void Bind_UserName()
        {
            createScenarioDetailMethods = new CreateScenarioDetailMethods();
            dataTable = createScenarioDetailMethods.BindUserName(Session["UserName"].ToString());
            ddlUser.DataSource = dataTable;
            ddlUser.DataTextField = "UserName";

            ddlUser.DataBind();


        }
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            PT.InsertDataIntoDB("BenefitSimulatorPlanList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManagePlanList.aspx");
            Response.Redirect("~/Pages/ManagePlanList.aspx");
        }
        protected void rbSelect_CheckedChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = true;
            int rowIndex = Convert.ToInt32(((sender as RadioButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = grdScenario.Rows[rowIndex];

            RadioButton rb = (RadioButton)row.FindControl("rbSelect");
            if (rb.Checked)
            {
                id_nav.Style.Add("pointer-events", "auto");
                Session["BenefitSimulatorScenarioID"] = (row.FindControl("lblId") as Label).Text;
                Session["BenefitSimulatorScenarioName"] = (row.FindControl("LblScenarioName") as Label).Text;
                EnableTopMenuLinks(Convert.ToInt32(Session["BenefitSimulatorScenarioID"].ToString()));
            }

        }
        private void EnableTopMenuLinks(int ScenarioId)
        {
            PlanListMethods planListMethods = new PlanListMethods();
            DataTable DT = planListMethods.GetPages(ScenarioId);
            if (DT.Rows.Count > 0)
            {
                if (Convert.ToInt32(DT.Rows[0]["QuickAccessCount"].ToString()) > 0)
                {
                    id_Quick.Style.Add("pointer-events", "auto");
                }
                else
                {
                    id_Quick.Style.Add("pointer-events", "none");
                }

                if (Convert.ToInt32(DT.Rows[0]["SimResultCount"].ToString()) > 0)
                {
                    id_Simulate.Style.Add("pointer-events", "auto");
                }
                else
                {
                    id_Simulate.Style.Add("pointer-events", "none");
                }
            }

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            grdScenario.EditIndex = e.NewEditIndex;
            Bind_Scenario();
            if (Session["BenefitSimulatorScenarioID"] != null)
            {
                btnLoad.Enabled = false;
                Session["BenefitSimulatorScenarioID"] = null;
                Session["BenefitSimulatorScenarioName"] = null;
                id_nav.Style.Add("pointer-events", "none");
                id_Quick.Style.Add("pointer-events", "none");
                id_Simulate.Style.Add("pointer-events", "none");
            }
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            grdScenario.EditIndex = -1;
            Bind_Scenario();

            if (Session["BenefitSimulatorScenarioID"] != null)
            {
                btnLoad.Enabled = false;
                Session["BenefitSimulatorScenarioID"] = null;
                Session["BenefitSimulatorScenarioName"] = null;
                id_nav.Style.Add("pointer-events", "none");
                id_Quick.Style.Add("pointer-events", "none");
                id_Simulate.Style.Add("pointer-events", "none");
            }
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int alert = 0;
            createScenarioDetailMethods = new CreateScenarioDetailMethods();
            Label lblSid = (Label)grdScenario.Rows[e.RowIndex].FindControl("lblId");
            TextBox TxtScenarioName = (TextBox)grdScenario.Rows[e.RowIndex].FindControl("txtScenarioName");
            TextBox TxtDescription = (TextBox)grdScenario.Rows[e.RowIndex].FindControl("txtDescription");
            string oldValue = ((Label)grdScenario.Rows[e.RowIndex].FindControl("oldValue")).Text;
            if (TxtScenarioName.Text != oldValue)
            {
                alert = 1;
            }
            string CreatedBy = Session["UserName"].ToString();
            int result = createScenarioDetailMethods.UpdateScenario(Convert.ToInt32(lblSid.Text.ToString()), TxtScenarioName.Text.ToString(), TxtDescription.Text.ToString(), CreatedBy, alert);
            if (result == 1 && alert == 1)
            {
                string message = "Scenario name already exists.Please provide a different name.";
                string script = "window.onload = function(){ alert('"; script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", script, true);
            }

            grdScenario.EditIndex = -1;
            Bind_Scenario();

            if (Session["BenefitSimulatorScenarioID"] != null)
            {
                btnLoad.Enabled = false;
                Session["BenefitSimulatorScenarioID"] = null;
                Session["BenefitSimulatorScenarioName"] = null;
                id_nav.Style.Add("pointer-events", "none");
                id_Quick.Style.Add("pointer-events", "none");
                id_Simulate.Style.Add("pointer-events", "none");
            }
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            createScenarioDetailMethods = new CreateScenarioDetailMethods();
            int row = e.RowIndex;
            Label lbl = (Label)grdScenario.Rows[row].FindControl("LblId");
            Label lblScenarioName = (Label)grdScenario.Rows[row].FindControl("LblScenarioName");
            createScenarioDetailMethods.DeleteScenario(Convert.ToInt32(lbl.Text));
            Bind_Scenario();

            if (Session["BenefitSimulatorScenarioID"] != null)
            {
                btnLoad.Enabled = false;
                Session["BenefitSimulatorScenarioID"] = null;
                Session["BenefitSimulatorScenarioName"] = null;
                id_nav.Style.Add("pointer-events", "none");
                id_Quick.Style.Add("pointer-events", "none");
                id_Simulate.Style.Add("pointer-events", "none");
            }
        }
        protected void grdScenario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblShareCount = (Label)e.Row.FindControl("LblShareCount");
                LinkButton Lnk_Share = (LinkButton)e.Row.FindControl("Lnk_Share");
                if (LblShareCount != null && Lnk_Share != null)
                {
                    if (LblShareCount.Text != "0")
                    {
                        Lnk_Share.Enabled = true;
                    }
                    else
                    {
                        Lnk_Share.Enabled = false;
                        Lnk_Share.Style.Add("pointer-events", "none");
                        Lnk_Share.Style.Add("color", "gray");
                        Lnk_Share.Style.Add("opacity", "0.2");
                    }
                }
            }
        }
        protected void lbCreate_Click(object sender, EventArgs e)//that plus sign at the top right
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
            txtScenario.Text = "";
            txtScenarioDesc.Text = "";
            scenarioExists.Visible = false;
        }
        protected void btnPopUpSave_Click(object sender, EventArgs e)//willsave the things to database
        {
            createScenarioDetailMethods = new CreateScenarioDetailMethods();
            int result;
            string Scenario = txtScenario.Text.ToString();
            string ScenarioDesc = txtScenarioDesc.Text.ToString();
            string CreatedBy = Session["UserName"].ToString();
            result = createScenarioDetailMethods.InsertScenario(Scenario, ScenarioDesc, CreatedBy, "-", "-");

            if (result != 0)
            {
                scenarioExists.Visible = false;
                Session["BenefitSimulatorScenarioID"] = result;
                Session["BenefitSimulatorScenarioName"] = txtScenario.Text;
                txtScenario.Text = "";
                txtScenarioDesc.Text = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeModal2();", true);
                PT.InsertDataIntoDB("BenefitSimulatorPlanList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManagePlanList.aspx");
                Response.Redirect("~/Pages/ManagePlanList.aspx");
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
        protected void btnPopUpShare_Click(object sender, EventArgs e)
        {
            CreateScenarioDetailMethods createScenarioDetailMethods = new CreateScenarioDetailMethods();
            string SharedUser = ddlUser.SelectedValue.ToString();
            string SharedDesc = txtMessage.Text.ToString();
            string CreatedBy = Session["UserName"].ToString();
            string SName = ScenarioNameS.Text;
            createScenarioDetailMethods.RemoveAlreadySharedScenario(SharedUser, SName);
            int NewScenarioID = createScenarioDetailMethods.InsertScenario(ScenarioNameS.Text.ToString(), ScenarioDescS.Text.ToString(), SharedUser, CreatedBy, SharedDesc);
            if (NewScenarioID != 0)
            {
                if (Session["SharedScenario"] != null)
                {
                    int OldScenarioID = Convert.ToInt32(Session["SharedScenario"].ToString());
                    createScenarioDetailMethods.ShareScenario(OldScenarioID, NewScenarioID);
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
                PT.InsertDataIntoDB("BenefitSimulatorScenarioList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManageScenarioList.aspx");
                Response.Redirect("~/Pages/ManageScenarioList.aspx");
            }
            catch (Exception ex1)
            {

            }
        }
        protected void LBPlans_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("BenefitSimulatorPlanList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManagePlanList.aspx");
                Response.Redirect("~/Pages/ManagePlanList.aspx");
            }
            catch (Exception ex1)
            {

            }
        }
        protected void LBQuickAccess_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("BenefitSimulatorQuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManageQuickAccess.aspx");
                Response.Redirect("~/Pages/ManageQuickAccess.aspx");
            }
            catch (Exception ex1)
            {

            }
        }
        protected void LBSimulatedOutput_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("BenefitSimulatorResults", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManageSimulatedOutput.aspx");
                Response.Redirect("~/Pages/ManageSimulatedOutput.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

    }
}