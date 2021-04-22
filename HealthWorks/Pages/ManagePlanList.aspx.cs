using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessUtility;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

namespace HealthWorks.Pages
{
    public partial class ManagePlanList : System.Web.UI.Page
    {
        PageTracking PT = new PageTracking();
        PlanListMethods planListMethods = new PlanListMethods();
        Dictionary<string, string> stateDefaultValue;
        JsonFile jsonFile = new JsonFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Session["UserName"] = "support@healthworksai.com";
                //Session["FirstName"] = "Katie";
                //Session["BenefitSimulatorScenarioID"] = "1";
                //Session["BenefitSimulatorScenarioName"] = "testT";
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        stateDefaultValue = jsonFile.ReadJsonFile();
                        var firstValue = stateDefaultValue.First();

                        if (Session["DefaultState"] == null)
                            Session["DefaultState"] = firstValue.Key;
                        if ((Session["BenefitSimulatorScenarioID"] != null) && (Session["BenefitSimulatorScenarioName"] != null))
                        {
                            lblScenarioName.Text = Session["BenefitSimulatorScenarioName"].ToString();
                            EnableTopMenuLinks(Convert.ToInt32(Session["BenefitSimulatorScenarioID"].ToString()));
                        }
                        BindFilters();
                    }
                    Active_DeactiveLinks();
                    // this.Validate();
                }
            }
            catch (Exception ex)
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
        private void BindFilters()
        {
            if (Session["BenefitSimulatorScenarioID"] != null)
            {
                DataTable dataTable = new DataTable();
                dataTable = planListMethods.GetSavedPlans(Convert.ToInt32(Session["BenefitSimulatorScenarioID"].ToString()));
                if (dataTable.Rows.Count > 0)
                {
                    Session["BenefitSimulatorSavedPlan"] = dataTable;
                }
                else
                {
                    Session["BenefitSimulatorSavedPlan"] = null;
                }
                BindState();
            }
        }
        private void BindState()
        {
            try
            {
                ddlState.DataSource = planListMethods.GetState();
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "StateID";
                ddlState.DataBind();

                if (Session["BenefitSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["BenefitSimulatorSavedPlan"];
                    string SelectedState = DT.Rows[0]["StateID"].ToString();
                    ddlState.SelectedValue = SelectedState;
                }
                else
                {
                    if (Session["DefaultState"] != null)
                    {
                        ddlState.SelectedIndex = ddlState.Items.FindItemIndexByText(Session["DefaultState"].ToString());
                    }
                    else
                    {
                        ddlState.SelectedIndex = 0;
                    }
                }
                foreach (RadComboBoxItem itm in ddlState.Items)
                {
                    if (itm.Text == Session["DefaultState"].ToString())
                    {
                        itm.Enabled = true;
                    }
                    else
                    {
                        itm.Attributes.Add("style", "color:gray;");
                        itm.Enabled = false;
                    }
                }
                ddlState_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }
        private void BindSalesregion()
        {
            try
            {
                ddlSalesRegion.DataSource = planListMethods.GetSalesregion(Convert.ToInt32(ddlState.SelectedValue));
                ddlSalesRegion.DataTextField = "SalesRegion";
                ddlSalesRegion.DataValueField = "SalesRegionID";
                ddlSalesRegion.DataBind();

                if (Session["BenefitSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["BenefitSimulatorSavedPlan"];
                    string SelectedSalesRegion = DT.Rows[0]["SalesRegionIDs"].ToString();
                    ConvertStringToList(ddlSalesRegion, SelectedSalesRegion);
                }
                else
                {
                    foreach (RadComboBoxItem item in ddlSalesRegion.Items)
                    {
                        item.Checked = true;
                    }
                }

                ddlSalesRegion_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }
        private void BindCounty()
        {
            try
            {

                string SelectedSalesRegionIDs = ConvertListToString(ddlSalesRegion);
                ddlCounty.DataSource = planListMethods.GetCounty(Convert.ToInt32(ddlState.SelectedValue), SelectedSalesRegionIDs);
                ddlCounty.DataTextField = "County";
                ddlCounty.DataValueField = "CountyID";
                ddlCounty.DataBind();

                if (Session["BenefitSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["BenefitSimulatorSavedPlan"];
                    string SelectedCountys = DT.Rows[0]["CountyIDs"].ToString();
                    ConvertStringToList(ddlCounty, SelectedCountys);
                }
                else
                {
                    foreach (RadComboBoxItem item in ddlCounty.Items)
                    {
                        item.Checked = true;
                    }
                }

                ddlCounty_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }

        }
        private void BindPlanType()
        {
            try
            {
                string SelectedSalesRegionIDs = ConvertListToString(ddlSalesRegion);
                string SelectedCountyIDs = ConvertListToString(ddlCounty);
                ddlPlanType.DataSource = planListMethods.GetPlanType(Convert.ToInt32(ddlState.SelectedValue), SelectedSalesRegionIDs, SelectedCountyIDs);
                ddlPlanType.DataTextField = "PlanType";
                ddlPlanType.DataValueField = "PlanTypeID";
                ddlPlanType.DataBind();

                if (Session["BenefitSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["BenefitSimulatorSavedPlan"];
                    string SelectedPlanTypes = DT.Rows[0]["PlanTypeIDs"].ToString();
                    ConvertStringToList(ddlPlanType, SelectedPlanTypes);
                }
                else
                {
                    foreach (RadComboBoxItem item in ddlPlanType.Items)
                    {
                        item.Checked = true;
                    }
                }

                ddlPlanType_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }
        private void BindOrgView()
        {
            int SelectedState = Convert.ToInt32(ddlState.SelectedValue);
            string SelectedSalesRegionIDs = ConvertListToString(ddlSalesRegion);
            string SelectedCountyIDs = ConvertListToString(ddlCounty);
            string SelectedPlanTypeIDs = ConvertListToString(ddlPlanType);
            int ScenarioID = Convert.ToInt32(Session["BenefitSimulatorScenarioID"].ToString());

            DataSet dataSet = planListMethods.GetOrgView(SelectedState, SelectedSalesRegionIDs, SelectedCountyIDs, SelectedPlanTypeIDs, ScenarioID);

            grdOrgView.DataSource = dataSet.Tables[0];
            grdOrgView.DataBind();
            ViewState["grdOrgView"] = dataSet.Tables[0];
            ViewState["grdOrgViewSortDirection"] = "Asc";

            grdPlanView.DataSource = dataSet.Tables[1];
            grdPlanView.DataBind();

            ViewState["grdPlanView"] = dataSet.Tables[1];
            ViewState["grdPlanViewSortDirection"] = "Asc";

            if (Session["BenefitSimulatorSavedPlan"] != null)
            {
                grdCountyView.DataSource = null;
                grdCountyView.DataBind();

                DataTable DT = new DataTable();
                DT = (DataTable)Session["BenefitSimulatorSavedPlan"];
                string SelectedPlanName = DT.Rows[0]["PlanName"].ToString();
                string SelectedBidID = DT.Rows[0]["BidID"].ToString();
                string plans = SelectedPlanName + " (" + SelectedBidID + ")";
                foreach (GridViewRow row in grdPlanView.Rows)
                {
                    Session["BenefitSimulatorCheckSavedPlanExists"] = null;
                    Label LblPlans = row.FindControl("LblPlans") as Label;
                    if (LblPlans != null)
                    {
                        if (LblPlans.Text == plans)
                        {
                            ((RadioButton)row.FindControl("rbSelect")).Checked = true;
                            row.CssClass = "active";
                            Session["BenefitSimulatorCheckSavedPlanExists"] = "true";
                            BindCountyView(LblPlans.Text);
                            break;
                        }
                    }
                }
                if (Session["BenefitSimulatorCheckSavedPlanExists"] == null)
                {
                    if (grdPlanView.Rows.Count > 0)
                    {
                        GridViewRow row = (GridViewRow)grdPlanView.Rows[0];
                        ((RadioButton)row.FindControl("rbSelect")).Checked = true;
                        row.CssClass = "active";
                        Label LblPlans = row.FindControl("LblPlans") as Label;
                        if (LblPlans != null)
                        {
                            BindCountyView(LblPlans.Text);
                        }
                    }
                }

            }
            else
            {
                if (grdPlanView.Rows.Count > 0)
                {
                    GridViewRow row = (GridViewRow)grdPlanView.Rows[0];
                    ((RadioButton)row.FindControl("rbSelect")).Checked = true;
                    row.CssClass = "active";
                    Label LblPlans = row.FindControl("LblPlans") as Label;
                    if (LblPlans != null)
                    {
                        BindCountyView(LblPlans.Text);
                    }
                }
                else
                {
                    grdCountyView.DataSource = null;
                    grdCountyView.DataBind();
                }
            }



        }
        private void BindCountyView(string PlanBidID)
        {
            DataTable dataTable = planListMethods.GetCountyView(PlanBidID);

            grdCountyView.DataSource = dataTable;
            grdCountyView.DataBind();
            ViewState["grdCountyView"] = dataTable;
            ViewState["grdCountyViewSortDirection"] = "Asc";


        }
        protected void grdOrgView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string sortField = string.Empty;

            SortGridview((GridView)sender, e, out sortDirection, out sortField);
            string strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";

            DataTable dtrslt = (DataTable)ViewState["grdOrgView"];
            dtrslt.DefaultView.Sort = e.SortExpression + " " + strSortDirection;
            dtrslt = dtrslt.DefaultView.ToTable();
            grdOrgView.DataSource = dtrslt;
            grdOrgView.DataBind();
        }
        protected void grdPlanView_Sorting(object sender, GridViewSortEventArgs e)
        {
            foreach (GridViewRow row in grdPlanView.Rows)
            {
                Session["BenefitSimulatorCheckradioButton"] = null;
                RadioButton RB = ((RadioButton)row.FindControl("rbSelect"));
                if (RB != null && RB.Checked)
                {
                    Label LblPlans = row.FindControl("LblPlans") as Label;
                    if (LblPlans != null)
                    {
                        Session["BenefitSimulatorCheckradioButton"] = LblPlans.Text;
                        break;
                    }
                }
            }

            SortDirection sortDirection = SortDirection.Ascending;
            string sortField = string.Empty;

            SortGridview((GridView)sender, e, out sortDirection, out sortField);
            string strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";

            DataTable dtrslt = (DataTable)ViewState["grdPlanView"];
            dtrslt.DefaultView.Sort = e.SortExpression + " " + strSortDirection;
            dtrslt = dtrslt.DefaultView.ToTable();
            grdPlanView.DataSource = dtrslt;
            grdPlanView.DataBind();

            if (Session["BenefitSimulatorCheckradioButton"] != null)
            {
                foreach (GridViewRow row in grdPlanView.Rows)
                {
                    Label LblPlans = row.FindControl("LblPlans") as Label;
                    if (LblPlans != null)
                    {
                        if (LblPlans.Text == Session["BenefitSimulatorCheckradioButton"].ToString())
                        {
                            ((RadioButton)row.FindControl("rbSelect")).Checked = true;
                            row.CssClass = "active";
                            Session["BenefitSimulatorCheckradioButton"] = null;
                            break;
                        }
                    }
                }
            }
        }
        protected void grdPlanView_RowCreated(object sender, GridViewRowEventArgs e)
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
        protected void grdCountyView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string sortField = string.Empty;

            SortGridview((GridView)sender, e, out sortDirection, out sortField);
            string strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";

            DataTable dtrslt = (DataTable)ViewState["grdCountyView"];
            dtrslt.DefaultView.Sort = e.SortExpression + " " + strSortDirection;
            dtrslt = dtrslt.DefaultView.ToTable();
            grdCountyView.DataSource = dtrslt;
            grdCountyView.DataBind();

        }
        private string ConvertListToString(RadComboBox MultiDDL)
        {
            string CheckedItems = string.Empty;
            if (MultiDDL.CheckedItems.Count > 0)
            {

                var CheckedItemsColelction = MultiDDL.CheckedItems;
                foreach (var item in CheckedItemsColelction)
                {
                    CheckedItems += item.Value + ",";
                }
                CheckedItems = CheckedItems.Substring(0, CheckedItems.Length - 1);
            }
            return CheckedItems;
        }
        private void ConvertStringToList(RadComboBox MultiDDL, string SavedString)
        {
            string[] IDs = SavedString.ToString().Split(',');
            var getIDs = IDs;
            foreach (var item in getIDs)
            {
                RadComboBoxItem items = MultiDDL.FindItemByValue(item.ToString());
                if (items != null)
                {
                    items.Checked = true;
                }
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSalesregion();
        }
        protected void ddlSalesRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounty();
        }
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPlanType();
        }
        protected void ddlPlanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindOrgView();
        }
        protected void rbSelect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow oldrow in grdPlanView.Rows)
            {
                ((RadioButton)oldrow.FindControl("rbSelect")).Checked = false;
                oldrow.CssClass = "inactive";
            }
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbSelect")).Checked = true;
            row.CssClass = "active";
            Label LblPlans = row.FindControl("LblPlans") as Label;
            if (LblPlans != null)
            {
                BindCountyView(LblPlans.Text);
            }
        }
        protected void btnsimulate_Click(object sender, EventArgs e)
        {
            try
            {
                Session["SimulateClick"] = "true";
                SavePlanFilters();
                Session["BenefitSimulatorSavedPlan"] = null;
                PT.InsertDataIntoDB("BenefitSimulatorQuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManageQuickAccess.aspx");
                Response.Redirect("~/Pages/ManageQuickAccess.aspx");
            }
            catch (Exception ex)
            {

            }
        }
        protected void lbRevert_Click(object sender, EventArgs e)
        {
            BindFilters();
        }
        protected void lbSaveAs_Click(object sender, EventArgs e)
        {
            txtScenario.Text = "";
            txtScenarioDesc.Text = "";
            scenarioExists.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
        }
        protected void lbSave_Click(object sender, EventArgs e)
        {
            SavePlanFilters();
        }
        protected void lbDownload_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }
        protected void BtnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                CreateScenarioDetailMethods createScenarioDetailMethods = new CreateScenarioDetailMethods();
                string Scenario = txtScenario.Text.ToString();
                string ScenarioDesc = txtScenarioDesc.Text.ToString();
                string CreatedBy = Session["UserName"].ToString();
                int result = createScenarioDetailMethods.InsertScenario(Scenario, ScenarioDesc, CreatedBy, "-", "-");
                if (result != 0)
                {
                    scenarioExists.Visible = false;
                    Session["BenefitSimulatorScenarioID"] = result;
                    Session["BenefitSimulatorScenarioName"] = txtScenario.Text;
                    lblScenarioName.Text = txtScenario.Text;
                    txtScenario.Text = "";
                    txtScenarioDesc.Text = "";

                    SavePlanFilters();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "closeModal2();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
                    scenarioExists.Visible = true;
                }
            }
            catch (Exception Ex1)
            {

            }
        }
        private void SavePlanFilters()
        {
            try
            {
                int SelectedState = Convert.ToInt32(ddlState.SelectedValue);
                string SelectedSalesRegionIDs = ConvertListToString(ddlSalesRegion);
                string SelectedCountyIDs = ConvertListToString(ddlCounty);
                string SelectedPlanTypeIDs = ConvertListToString(ddlPlanType);
                int ScenarioID = Convert.ToInt32(Session["BenefitSimulatorScenarioID"].ToString());
                string PlanName = string.Empty;
                string BidID = string.Empty;

                foreach (GridViewRow row in grdPlanView.Rows)
                {
                    RadioButton RB = ((RadioButton)row.FindControl("rbSelect"));
                    if (RB.Checked)
                    {
                        Label LblBidID = row.FindControl("LblBidID") as Label;
                        if (LblBidID != null)
                        {
                            BidID = LblBidID.Text;
                        }
                        Label LblPlanName = row.FindControl("LblPlanName") as Label;
                        if (LblPlanName != null)
                        {
                            PlanName = LblPlanName.Text;
                        }
                        break;
                    }
                }
                int SavedPlanID = planListMethods.SavePlanUserInputs(SelectedState, SelectedSalesRegionIDs, SelectedCountyIDs, SelectedPlanTypeIDs, BidID, PlanName, ScenarioID);
                if (Session["SimulateClick"] == null)
                {
                    if (SavedPlanID > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Selected user plan is saved sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Selected user plan is not saved.');", true);
                    }

                }
                else
                {
                    Session["SimulateClick"] = null;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ExportGridToExcel()
        {
            grdPlanView.Columns[0].Visible = false;

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "PlanList.xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            grdPlanView.GridLines = GridLines.Both;
            grdPlanView.HeaderStyle.Font.Bold = true;
            grdPlanView.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
            grdPlanView.Columns[0].Visible = true;
        }
        protected void lbExternal_Click(object sender, EventArgs e)
        {
            Button lbExternal = sender as Button;
          //  PT.InsertDataIntoDB(lbExternal.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbExternal.CommandName);
            Session["dashboardURL"] = null;
            Session["dashboardURL"] = lbExternal.CommandArgument;
            Session["ActiveLink"] = null;
            Session["ActiveLink"] = lbExternal.ID.ToString() + "Li";
            Response.Redirect("~/Pages/Dashboard.aspx");
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
    }
}