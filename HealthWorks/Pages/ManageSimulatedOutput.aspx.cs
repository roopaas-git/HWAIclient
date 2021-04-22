using BusinessUtility;
using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HealthWorks.Pages
{
    public partial class ManageSimulatedOutput : System.Web.UI.Page
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

                        if (Session["BenefitSimulatorScenarioName"] != null)
                        {
                            lblScenarioName.Text = Session["BenefitSimulatorScenarioName"].ToString();
                        }
                        BindFilters();
                    }
                    Active_DeactiveLinks();
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
        private void BindFilters()
        {
            if (Session["BenefitSimulatorScenarioID"] != null)
            {
                DataTable dataTable = new DataTable();
                dataTable = planListMethods.GetSavedPlans(Convert.ToInt32(Session["BenefitSimulatorScenarioID"].ToString()));
                if (dataTable.Rows.Count > 0)
                {
                    Session["BenefitSimulatorSavedPlan"] = dataTable;
                    lblBid_Id.Text = dataTable.Rows[0]["PlanName"].ToString() + " (" + dataTable.Rows[0]["BidID"].ToString() + ")";
                }
                else
                {
                    Session["BenefitSimulatorSavedPlan"] = null;
                    lblBid_Id.Text = "";
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

            //foreach (RadComboBoxItem rdItem in MultiDDL.Items)
            //{
            //    if (!rdItem.Checked)
            //    {
            //        rdItem.Attributes.Add("style", "color:gray;");
            //        rdItem.Enabled = false;
            //    }
            //}
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
            BindGridViews();
        }
        private void BindGridViews()
        {
            int SelectedState = Convert.ToInt32(ddlState.SelectedValue);
            string SelectedSalesRegionIDs = ConvertListToString(ddlSalesRegion);
            string SelectedCountyIDs = ConvertListToString(ddlCounty);
            string SelectedPlanTypeIDs = ConvertListToString(ddlPlanType);
            int ScenarioID = Convert.ToInt32(Session["BenefitSimulatorScenarioID"].ToString());

            DataSet dataSet = planListMethods.GetOutputResults(SelectedState, SelectedSalesRegionIDs, SelectedCountyIDs, SelectedPlanTypeIDs, ScenarioID);

            grdOrgView.DataSource = dataSet.Tables[2];
            grdOrgView.DataBind();

            ViewState["grdOrgView"] = dataSet.Tables[2];
            ViewState["grdOrgViewSortDirection"] = "Asc";

            grdPlanView.DataSource = dataSet.Tables[1];
            grdPlanView.DataBind();

            ViewState["grdPlanView"] = dataSet.Tables[1];
            ViewState["grdPlanViewSortDirection"] = "Asc";

            grdCountyView.DataSource = dataSet.Tables[0];
            grdCountyView.DataBind();

            ViewState["grdCountyView"] = dataSet.Tables[0];
            ViewState["grdCountyViewSortDirection"] = "Asc";
        }
        protected void grdPlanView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblHightlightRow = (Label)e.Row.FindControl("LblHightlightRow");
                if (LblHightlightRow != null)
                {
                    if (LblHightlightRow.Text == "1")
                    {
                        e.Row.CssClass = "active";
                    }
                }
            }
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