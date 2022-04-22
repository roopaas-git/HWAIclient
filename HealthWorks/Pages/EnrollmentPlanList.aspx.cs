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
using System.Net.Mail;
using System.Net;
using ClosedXML.Excel;

namespace HealthWorks.Pages
{
    public partial class EnrollmentPlanList : System.Web.UI.Page
    {
        PageTracking PT = new PageTracking();
        EnrollmentPlanListMethods enrollmentPlanListMethods = new EnrollmentPlanListMethods();
        EnrollmentPlansUserInputsMethods enrollmentPlansUserInputsMethods = new EnrollmentPlansUserInputsMethods();
        Dictionary<string, string> stateDefaultValue;
        JsonFile jsonFile = new JsonFile();
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
                    if (!CheckIsUploadAllowedValidation())
                    {
                        Response.Redirect("~/Pages/EnrollmentScenarioList.aspx");
                    }

                    if (!IsPostBack)
                    {
                        //UploadFile.Attributes["onchange"] = "UploadFileChange(this)";
                        stateDefaultValue = jsonFile.ReadJsonFile();
                        var firstValue = stateDefaultValue.First();

                        if (Session["DefaultState"] == null)
                            Session["DefaultState"] = firstValue.Key;
                        if ((Session["EnrollmentSimulatorScenarioID"] != null) && (Session["EnrollmentSimulatorScenarioName"] != null))
                        {
                            lblScenarioName.Text = Session["EnrollmentSimulatorScenarioName"].ToString();
                            //EnableTopMenuLinks(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));
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

        private bool CheckIsUploadAllowedValidation()
        {
            EnrollmentScenarioDetailMethods enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
            DataTable dataTable = new DataTable();
            dataTable = enrollmentScenarioDetailMethods.GetEnrollmentScenario(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["ProcessStatus"].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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

            if (Session["ProgressBarStatus"] != null)
            {
                if (Convert.ToInt32(Session["ProgressBarStatus"]) == 2)
                {
                    DownloadPBP.Attributes["class"] = "active";
                    LBPlans_UploadPBP.Enabled = true;
                }
            }
        }
        
        private void BindFilters()
        {
            if (Session["EnrollmentSimulatorScenarioID"] != null)
            {
                DataTable dataTable = new DataTable();
                // EnrollmentFileUploadMethods objEnrollmentFileUpload = new EnrollmentFileUploadMethods();
                // dataTable = objEnrollmentFileUpload.GetEnrollmentFileUploadDetails(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));
                dataTable = enrollmentPlansUserInputsMethods.GetEnrollmentSimulatorSavedPlans(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));
                if (dataTable.Rows.Count > 0)
                {
                    Session["EnrollmentSimulatorSavedPlan"] = dataTable;
                }
                else
                {
                    Session["EnrollmentSimulatorSavedPlan"] = null;
                }
                BindMarket();
            }
        }

        private void BindMarket()
        {
            try
            {
                ddlMarket.DataSource = enrollmentPlanListMethods.GetMarket(Convert.ToInt32(Session["ClientId"]));
                ddlMarket.DataTextField = "Market";
                ddlMarket.DataValueField = "MarketId";
                ddlMarket.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedMarket = DT.Rows[0]["MarketId"].ToString();
                    ddlMarket.SelectedValue = SelectedMarket;
                }
                else
                {
                    ddlMarket.SelectedIndex = 0;
                }

                ddlMarket_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private void BindSubMarket()
        {
            try
            {
                ddlSubMarket.DataSource = enrollmentPlanListMethods.GetSubMarket(Convert.ToInt32(Session["ClientId"]),Convert.ToInt32(ddlMarket.SelectedValue));
                ddlSubMarket.DataTextField = "SubMarket";
                ddlSubMarket.DataValueField = "SubMarketId";
                ddlSubMarket.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedSubMarket = DT.Rows[0]["SubMarketId"].ToString();
                    ddlSubMarket.SelectedValue = SelectedSubMarket;
                }
                else
                {
                    ddlSubMarket.SelectedIndex = 0;
                }

                ddlSubMarket_SelectedIndexChanged(this, null);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private void BindState()
        {
            try
            {
                ddlState.DataSource = enrollmentPlanListMethods.GetState(Convert.ToInt32(Session["ClientId"]), Convert.ToInt32(ddlSubMarket.SelectedValue));
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "StateId";
                ddlState.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedState = DT.Rows[0]["StateIds"].ToString();
                    ConvertStringToList(ddlState, SelectedState);
                }
                else
                {
                    if (Session["DefaultState"] != null)
                    {
                        ConvertStringToList(ddlState, ddlState.Items.FindItemByText(Session["DefaultState"].ToString()).Value);
                    }
                    else
                    {
                        foreach (RadComboBoxItem item in ddlState.Items)
                        {
                            item.Checked = true;
                        }
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

        private void BindSalesRegion()
        {
            try
            {
                string SelectedStates = ConvertListToString(ddlState);

                ddlSalesRegion.DataSource = enrollmentPlanListMethods.GetSalesRegion(Convert.ToInt32(Session["ClientId"]), Convert.ToInt32(ddlSubMarket.SelectedValue), SelectedStates);
                ddlSalesRegion.DataTextField = "SalesRegion";
                ddlSalesRegion.DataValueField = "SalesRegionId";
                ddlSalesRegion.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedSalesRegion = DT.Rows[0]["SalesRegionIds"].ToString();
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

        private void BindCountry()
        {
            try
            {                
                string SelectedStates = ConvertListToString(ddlState);
                string SelectedSalesRegions = ConvertListToString(ddlSalesRegion);
                string SelectedFootprints = ConvertListToString(ddlFootprint);

                ddlCounty.DataSource = enrollmentPlanListMethods.GetCounties(Convert.ToInt32(Session["ClientId"]), Convert.ToInt32(ddlSubMarket.SelectedValue), SelectedStates, SelectedSalesRegions, SelectedFootprints);
                ddlCounty.DataTextField = "County";
                ddlCounty.DataValueField = "CountyId";
                ddlCounty.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedCounty = DT.Rows[0]["CountyIds"].ToString();
                    ConvertStringToList(ddlCounty, SelectedCounty);
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

        private void BindFootprint()
        {
            try
            {
                string SelectedStates = ConvertListToString(ddlState);
                string SelectedSalesRegions = ConvertListToString(ddlSalesRegion);
                
                ddlFootprint.DataSource = enrollmentPlanListMethods.GetFootprint(Convert.ToInt32(Session["ClientId"]), Convert.ToInt32(ddlSubMarket.SelectedValue), SelectedStates, SelectedSalesRegions);
                ddlFootprint.DataTextField = "Footprint";
                ddlFootprint.DataValueField = "FootprintId";
                ddlFootprint.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedFootprint = DT.Rows[0]["FootprintIds"].ToString();
                    ConvertStringToList(ddlFootprint, SelectedFootprint);
                }
                else
                {
                    foreach (RadComboBoxItem item in ddlFootprint.Items)
                    {
                        item.Checked = true;
                    }
                }

                ddlFootprint_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private void BindPlanCategory()
        {
            try
            {
                string SelectedStates = ConvertListToString(ddlState);
                string SelectedCounties = ConvertListToString(ddlCounty);
               
                ddlPlanCategory.DataSource = enrollmentPlanListMethods.GetPlanCategory(SelectedStates, SelectedCounties);
                ddlPlanCategory.DataTextField = "PlanCategory";
                ddlPlanCategory.DataValueField = "PlanCategoryId";
                ddlPlanCategory.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedPlanCategory = DT.Rows[0]["PlanCategoryIds"].ToString();
                    ConvertStringToList(ddlPlanCategory, SelectedPlanCategory);
                }
                else
                {
                    foreach (RadComboBoxItem item in ddlPlanCategory.Items)
                    {
                        item.Checked = true;
                    }
                }

                ddlPlanCategory_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private void BindPremium()
        {
            try
            {
                string SelectedStates = ConvertListToString(ddlState);
                string SelectedCounties = ConvertListToString(ddlCounty);                
                string SelectedPlanCategories = ConvertListToString(ddlPlanCategory);


                ddlPremium.DataSource = enrollmentPlanListMethods.GetPremium(SelectedStates, SelectedCounties, SelectedPlanCategories);
                ddlPremium.DataTextField = "Premium";
                ddlPremium.DataValueField = "PremiumId";
                ddlPremium.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedPremium = DT.Rows[0]["PremiumIds"].ToString();
                    ConvertStringToList(ddlPremium, SelectedPremium);
                }
                else
                {
                foreach (RadComboBoxItem item in ddlPremium.Items)
                {
                    item.Checked = true;
                }
                }

                ddlPremium_SelectedIndexChanged(this, null);
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
                string SelectedStates = ConvertListToString(ddlState);
                string SelectedCounties = ConvertListToString(ddlCounty);
                string SelectedPlanCategories = ConvertListToString(ddlPlanCategory);
                string SelectedPremiums = ConvertListToString(ddlPremium);

                ddlPlanType.DataSource = enrollmentPlanListMethods.GetPlanType(SelectedStates, SelectedCounties, SelectedPlanCategories, SelectedPremiums);
                ddlPlanType.DataTextField = "PlanType";
                ddlPlanType.DataValueField = "PlanTypeId";
                ddlPlanType.DataBind();

                if (Session["EnrollmentSimulatorSavedPlan"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                    string SelectedPlanType = DT.Rows[0]["PlanTypeIds"].ToString();
                    ConvertStringToList(ddlPlanType, SelectedPlanType);
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

        protected void ddlMarket_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindSubMarket();
        }

        protected void ddlSubMarket_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindState();
        }

        protected void ddlState_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindSalesRegion();           
        }

        protected void ddlSalesRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {           
            BindFootprint();
        }

        protected void ddlCounty_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindPlanCategory();
        }            

        protected void ddlFootprint_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindCountry();
        }

        protected void ddlPlanCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindPremium();
        }

        protected void ddlPremium_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindPlanType();
        }

        protected void ddlPlanType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindOrgView();
        }

        private void BindOrgView()
        {           
            string SelectedStates = ConvertListToString(ddlState);
            string SelectedCounties = ConvertListToString(ddlCounty);         
            string SelectedPlanCategories = ConvertListToString(ddlPlanCategory);
            string SelectedPremiums = ConvertListToString(ddlPremium);
            string SelectedPlanTypes = ConvertListToString(ddlPlanType);
            
            DataSet dataSet = enrollmentPlanListMethods.GetOrgView(SelectedStates, SelectedCounties, SelectedPlanCategories, SelectedPremiums, SelectedPlanTypes);

            grdOrgView.DataSource = dataSet.Tables[0];
            grdOrgView.DataBind();
            ViewState["grdOrgView"] = dataSet.Tables[0];
            ViewState["grdOrgViewSortDirection"] = "Asc";

            grdPlanView.DataSource = dataSet.Tables[1];
            grdPlanView.DataBind();

            ViewState["grdPlanView"] = dataSet.Tables[1];
            ViewState["grdPlanViewSortDirection"] = "Asc";

            if (Session["EnrollmentSimulatorSavedPlan"] != null)
            {
                grdCountyView.DataSource = null;
                grdCountyView.DataBind();

                DataTable DT = new DataTable();
                DT = (DataTable)Session["EnrollmentSimulatorSavedPlan"];
                string SelectedPlanName = DT.Rows[0]["PlanName"].ToString();
                string SelectedBidID = DT.Rows[0]["BidId"].ToString();
                string plans = SelectedPlanName + " (" + SelectedBidID + ")";
                foreach (GridViewRow row in grdPlanView.Rows)
                {
                    Session["EnrollmentSimulatorCheckSavedPlanExists"] = null;
                    Label LblPlans = row.FindControl("LblPlans") as Label;
                    if (LblPlans != null)
                    {
                        if (LblPlans.Text == plans)
                        {
                            ((RadioButton)row.FindControl("rbSelect")).Checked = true;
                            row.CssClass = "active";
                            Session["EnrollmentSimulatorCheckSavedPlanExists"] = "true";
                            Label LblBidID = row.FindControl("LblBidID") as Label;
                            Session["EnrollmentSimulator_BidID"] = LblBidID.Text;
                            Label LblPlanName = row.FindControl("LblPlanName") as Label;
                            Session["EnrollmentSimulator_PlanName"] = LblPlanName.Text;
                            BindCountyView(LblPlans.Text);
                            break;
                        }
                    }
                }
                if (Session["EnrollmentSimulatorCheckSavedPlanExists"] == null)
                {
                    if (grdPlanView.Rows.Count > 0)
                    {
                        GridViewRow row = (GridViewRow)grdPlanView.Rows[0];
                        ((RadioButton)row.FindControl("rbSelect")).Checked = true;
                        row.CssClass = "active";
                        Label LblPlans = row.FindControl("LblPlans") as Label;
                        if (LblPlans != null)
                        {
                            Label LblBidID = row.FindControl("LblBidID") as Label;
                            Session["EnrollmentSimulator_BidID"] = LblBidID.Text;
                            Label LblPlanName = row.FindControl("LblPlanName") as Label;
                            Session["EnrollmentSimulator_PlanName"] = LblPlanName.Text;
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
                        Label LblBidID = row.FindControl("LblBidID") as Label;
                        Session["EnrollmentSimulator_BidID"] = LblBidID.Text;
                        Label LblPlanName = row.FindControl("LblPlanName") as Label;
                        Session["EnrollmentSimulator_PlanName"] = LblPlanName.Text;
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
            string SelectedStates = ConvertListToString(ddlState);
            string SelectedCounties = ConvertListToString(ddlCounty);                  
            string SelectedPlanCategories = ConvertListToString(ddlPlanCategory);
            string SelectedPremiums = ConvertListToString(ddlPremium);
            string SelectedPlanTypes = ConvertListToString(ddlPlanType);
                       
            DataTable dataTable = enrollmentPlanListMethods.GetCountyView(PlanBidID, SelectedStates, SelectedCounties, SelectedPlanCategories, SelectedPremiums, SelectedPlanTypes);

            grdCountyView.DataSource = dataTable;
            grdCountyView.DataBind();
            ViewState["grdCountyView"] = dataTable;
            ViewState["grdCountyViewSortDirection"] = "Asc";
                        
            Session["EnrollmentSimulatorSavedPlan"] = null;
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
                Session["EnrollmentSimulatorCheckradioButton"] = null;
                RadioButton RB = ((RadioButton)row.FindControl("rbSelect"));
                if (RB != null && RB.Checked)
                {
                    Label LblPlans = row.FindControl("LblPlans") as Label;
                    if (LblPlans != null)
                    {
                        Session["EnrollmentSimulatorCheckradioButton"] = LblPlans.Text;
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

            if (Session["EnrollmentSimulatorCheckradioButton"] != null)
            {
                foreach (GridViewRow row in grdPlanView.Rows)
                {
                    Label LblPlans = row.FindControl("LblPlans") as Label;
                    if (LblPlans != null)
                    {
                        if (LblPlans.Text == Session["EnrollmentSimulatorCheckradioButton"].ToString())
                        {
                            ((RadioButton)row.FindControl("rbSelect")).Checked = true;
                            row.CssClass = "active";
                            Session["EnrollmentSimulatorCheckradioButton"] = null;
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

        private string ConvertListToStringValues(RadComboBox MultiDDL)
        {
            string CheckedItems = string.Empty;
            if (MultiDDL.CheckedItems.Count > 0)
            {

                var CheckedItemsColelction = MultiDDL.CheckedItems;
                foreach (var item in CheckedItemsColelction)
                {
                    CheckedItems += item.Text + ",";
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
                Label LblBidID = row.FindControl("LblBidID") as Label;
                Session["EnrollmentSimulator_BidID"] = LblBidID.Text;
                Label LblPlanName = row.FindControl("LblPlanName") as Label;
                Session["EnrollmentSimulator_PlanName"] = LblPlanName.Text;
                BindCountyView(LblPlans.Text);
            }
        }

        protected void lbRevert_Click(object sender, EventArgs e)
        {
            BindFilters();
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
                PT.InsertDataIntoDB("EnrollmentScenarioList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentScenarioList.aspx");
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
                PT.InsertDataIntoDB("EnrollmentPlanList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentPlanList.aspx");
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
                PT.InsertDataIntoDB("EnrollmentQuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentQuickAccessSimulation.aspx");
                nextStep();
               // Response.Redirect("~/Pages/EnrollmentQuickAccess.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void LBSimulatedOutput_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("ManageSimulatedOutput", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManageSimulatedOutput.aspx");
                Response.Redirect("~/Pages/ManageSimulatedOutput.aspx");
            }
            catch (Exception ex1)
            {

            }
        }
               
        protected void btnApplyStateFilter_Click(object sender, EventArgs e)
        {
            BindSalesRegion();
        }

        protected void btnApplySalesRegionFilter_Click(object sender, EventArgs e)
        {
            BindFootprint();
        }

        protected void btnApplyCountyFilter_Click(object sender, EventArgs e)
        {
            BindPlanCategory();
        }

        protected void btnApplyFootprintFilter_Click(object sender, EventArgs e)
        {
            BindCountry();
        }

        protected void btnApplyPlanCategoryFilter_Click(object sender, EventArgs e)
        {
            BindPremium();
        }

        protected void btnApplyPlanTypeFilter_Click(object sender, EventArgs e)
        {           
            BindOrgView();
        }

        protected void btnApplyPremiumFilter_Click(object sender, EventArgs e)
        {
            BindPlanType();
        }   

        protected void btnSimulate_Click(object sender, EventArgs e)
        {
            nextStep();
        }

        private void nextStep()
        {           
            Session["EnrollmentSimulatorSavedPlan"] = null;
            SavePlanFilters();
            PT.InsertDataIntoDB("EnrollmentQuickAccessSimulation", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentQuickAccessSimulation.aspx");
            Session["ProgressBarStatus"] = 2;
            Response.Redirect("~/Pages/EnrollmentQuickAccessSimulation.aspx");
        }

        private void SavePlanFilters()
        {
            EnrollmentPlansUserInputs objEnrollmentPlansUserInputs = new EnrollmentPlansUserInputs();

            objEnrollmentPlansUserInputs.MarketId = Convert.ToInt32(ddlMarket.SelectedValue);
            objEnrollmentPlansUserInputs.SubMarketId = Convert.ToInt32(ddlSubMarket.SelectedValue);
            objEnrollmentPlansUserInputs.StateIds = ConvertListToString(ddlState);
            objEnrollmentPlansUserInputs.SalesRegionIds = ConvertListToString(ddlSalesRegion);
            objEnrollmentPlansUserInputs.CountyIds = ConvertListToString(ddlCounty);
            objEnrollmentPlansUserInputs.FootprintIds = ConvertListToString(ddlFootprint);
            objEnrollmentPlansUserInputs.PlanCategoryIds = ConvertListToString(ddlPlanCategory);
            objEnrollmentPlansUserInputs.PremiumIds = ConvertListToString(ddlPremium);
            objEnrollmentPlansUserInputs.PlanTypeIds = ConvertListToString(ddlPlanType);

            objEnrollmentPlansUserInputs.ScenarioID = Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString());
            objEnrollmentPlansUserInputs.PlanName = string.Empty;
            objEnrollmentPlansUserInputs.BidId = string.Empty;

            foreach (GridViewRow row in grdPlanView.Rows)
            {
                RadioButton RB = ((RadioButton)row.FindControl("rbSelect"));
                if (RB.Checked)
                {
                    Label LblBidID = row.FindControl("LblBidID") as Label;
                    if (LblBidID != null)
                    {
                        objEnrollmentPlansUserInputs.BidId = LblBidID.Text;
                    }
                    Label LblPlanName = row.FindControl("LblPlanName") as Label;
                    if (LblPlanName != null)
                    {
                        objEnrollmentPlansUserInputs.PlanName = LblPlanName.Text;
                    }
                    break;
                }
            }

            DataTable dtrslt = ((DataTable)ViewState["grdCountyView"]).DefaultView.ToTable(false, "StateId", "CountyId");
            dtrslt.DefaultView.ToTable(false, "StateId", "CountyId");

            List<string> statesIds = new List<string>();
            List<string> countyIds = new List<string>();

            if (dtrslt.Rows.Count > 0)
            {
                foreach (DataRow row in dtrslt.Rows)
                {
                    if (statesIds.Where(x => x == row["StateId"].ToString()).Count() == 0)
                    {
                        statesIds.Add(row["StateId"].ToString());
                    }

                    if (countyIds.Where(x => x == row["CountyId"].ToString()).Count() == 0)
                    {
                        countyIds.Add(row["CountyId"].ToString());
                    }
                }
            }
            objEnrollmentPlansUserInputs.BidLevelStateIds = string.Join(",", statesIds);
            objEnrollmentPlansUserInputs.BidLevelCountyIds = string.Join(",", countyIds);

            enrollmentPlansUserInputsMethods.SaveEnrollmentSimulatorPlanUserInputs(objEnrollmentPlansUserInputs);
            Session["EnrollmentSimulatorSavedPlan"] = objEnrollmentPlansUserInputs;

        }
    }
}