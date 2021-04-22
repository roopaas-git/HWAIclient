using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.HtmlChart;


namespace HealthWorks.Pages
{
    public partial class SimulatorResult : System.Web.UI.Page
    {
        public SqlConnection lobjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
        public SqlCommand lobjCmd;
        public DataSet lobjDS;
        public SqlDataAdapter lobjDA;
        PageTracking PT = new PageTracking();
        public int i;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["GetBidToHighlight"] = null;
                    Get_All_PlanFinder_Saved_Values();
                    Bind_Dropdowns();
                    Bind_LabelValues();
                    Bind_GridData();
                    Bind_UserList();
                    Bind_Graph_Bid();
                    Bind_Services();                  
                    Bind_Graph(ddlGraph.SelectedItem.Value);
                    if (Session["sName"] != null)
                    {
                        lblScenarioName.Text = Session["sName"].ToString();
                    }
                }
                this.Validate();
                Active_DeactiveLinks();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnk_ScenarioList_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("ScenarioList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ScenarioList.aspx");
                Response.Redirect("~/Pages/ScenarioList.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void lnk_Planfinder_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("PlanFinder", Session["SessionId"].ToString(), Session["UserName"].ToString(), "PlanFinder.aspx");
                Response.Redirect("~/Pages/PlanFinder.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void lnk_QuickAccess_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("QuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "QuickAccess.aspx");
                Response.Redirect("~/Pages/QuickAccess.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void lnk_Simulated_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("SimulatorResult", Session["SessionId"].ToString(), Session["UserName"].ToString(), "SimulatorResult.aspx");
                Response.Redirect("~/Pages/SimulatorResult.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ProductIntelC2L3li"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";

            }
         
        }

        private void Get_All_PlanFinder_Saved_Values()
        {
            SqlCommand lobjCmd = new SqlCommand("Get_PlanFinder_Save", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Convert.ToInt32(Session["sId"].ToString()));
            lobjCon.Open();
            SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
            DataSet ds = new DataSet();
            sd.Fill(ds);
            lobjCon.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["State"] = ds.Tables[0].Rows[0]["State"].ToString();
                Session["SelectedState"] = ds.Tables[0].Rows[0]["State"].ToString();
                Session["County"] = ds.Tables[0].Rows[0]["County"].ToString();
                Session["SelectedCounty"] = ds.Tables[0].Rows[0]["County"].ToString();
                Session["Drug_Coverage"] = ds.Tables[0].Rows[0]["Drug_Coverage"].ToString();
                Session["Plantype"] = ds.Tables[0].Rows[0]["Plan_Type"].ToString();
                Session["Persona"] = ds.Tables[0].Rows[0]["Persona"].ToString();
                Session["PlanName"] = ds.Tables[0].Rows[0]["PlanName"].ToString();
                Session["BidId"] = ds.Tables[0].Rows[0]["Bid_id"].ToString();
            }
        }

        private void SetSeriesItemsColors()
        {
            foreach (WaterfallSeries series in RadHtmlChart1.PlotArea.Series)
            {
                foreach (WaterfallSeriesItem item in series.SeriesItems)
                {
                    if (item.Y > 0)
                    {
                        item.BackgroundColor = Color.LightGreen;
                    }
                    if (item.Y < 0)
                    {
                        item.BackgroundColor = Color.Salmon;
                    }
                    if (item.Summary != SummaryType.Default)
                    {
                        item.BackgroundColor = Color.LightBlue;
                    }
                }
            }
        }

        private void Bind_Graph(string bid)
        {
            RadHtmlChart1.PlotArea.Series.Clear();
            RadHtmlChart1.PlotArea.XAxis.Items.Clear();
            // To Get Data for binding graph
            lobjCmd = new SqlCommand("Get_Simulated_Detailed_List_Test", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@Bid_Id", bid);
            lobjCmd.Parameters.AddWithValue("@Bid_Column", '[' + bid + ']');
            lobjCmd.Parameters.AddWithValue("@Flag", 1);
            lobjCmd.Parameters.AddWithValue("@sId", Convert.ToInt32(Session["sId"].ToString()));
            lobjCmd.Parameters.AddWithValue("@Persona", ddlPersona.SelectedValue);

            lobjDA = new SqlDataAdapter(lobjCmd);
            DataSet dataset1 = new DataSet();
            lobjDA.Fill(dataset1);
            string oldOOPC = string.Empty;

            for (int i = 0; i < grdData.Rows.Count; i++)
            {
                string abc = ((Label)grdData.Rows[i].FindControl("LblBidID")).Text;

                if (abc == bid)
                    oldOOPC = ((Label)grdData.Rows[i].FindControl("LblGrandTotal")).Text;
            }

            var plotArea = RadHtmlChart1.PlotArea;
            var xAxis = plotArea.XAxis;
            xAxis.AxisCrossingValue = 0;
            xAxis.Color = Color.Black;
            xAxis.MajorTickType = TickType.Outside;
            xAxis.MinorTickType = TickType.None;
            xAxis.Reversed = false;
            var axisItemCollection = xAxis.Items;
            axisItemCollection.AddRange(new List<AxisItem>()
            {
             new AxisItem("Old OOPC"),
             new AxisItem("All Other Service"),
             new AxisItem("Dental Services"),
             new AxisItem("Inpatient Care"),
             new AxisItem("Outpatient Care"),
             new AxisItem("Simulated OOPC"),
            });
            xAxis.LabelsAppearance.DataFormatString = "{0}";
            xAxis.LabelsAppearance.RotationAngle = 0;
            xAxis.TitleAppearance.Position = AxisTitlePosition.Center;
            xAxis.MajorGridLines.Width = 1;
            xAxis.MinorGridLines.Width = 1;

            var series = plotArea.Series;
            var barSeries = new WaterfallSeries();
            decimal all, Dental, Inpatient, Outpatient;

            if (dataset1.Tables[0].Rows[0]["All_Other_Services"].ToString() != null && dataset1.Tables[0].Rows[0]["All_Other_Services"].ToString() != "")
                all = Convert.ToDecimal(dataset1.Tables[0].Rows[0]["All_Other_Services"].ToString());
            else
                all = Convert.ToDecimal("0");

            if (dataset1.Tables[0].Rows[0]["Outpatient_Drugs"].ToString() != null && dataset1.Tables[0].Rows[0]["Outpatient_Drugs"].ToString() != "")
                Outpatient = Convert.ToDecimal(dataset1.Tables[0].Rows[0]["Outpatient_Drugs"].ToString());
            else
                Outpatient = Convert.ToDecimal("0");

            if (dataset1.Tables[0].Rows[0]["Dental_Services"].ToString() != null && dataset1.Tables[0].Rows[0]["Dental_Services"].ToString() != "")
            {
                Dental = Convert.ToDecimal(dataset1.Tables[0].Rows[0]["Dental_Services"].ToString());
            }
            else
                Dental = Convert.ToDecimal("0");

            if (dataset1.Tables[0].Rows[0]["Inpatient_Services"].ToString() != null && dataset1.Tables[0].Rows[0]["Inpatient_Services"].ToString() != "")
                Inpatient = Convert.ToDecimal(dataset1.Tables[0].Rows[0]["Inpatient_Services"].ToString());
            else
                Inpatient = Convert.ToDecimal("0");

            barSeries.SeriesItems.AddRange(new List<WaterfallSeriesItem>
            {
            new WaterfallSeriesItem(y:Convert.ToDecimal(oldOOPC.ToString())),
            new WaterfallSeriesItem(y:all),
            new WaterfallSeriesItem(y:Dental),
            new WaterfallSeriesItem(y:Inpatient),
            new WaterfallSeriesItem(y:Outpatient),
            new WaterfallSeriesItem(summary:SummaryType.Total),
            });


            barSeries.Appearance.FillStyle.BackgroundColor = Color.Orange;
            barSeries.LabelsAppearance.Visible = true;
            barSeries.LabelsAppearance.DataFormatString = "{0:0}";
            series.Add(barSeries);
            SetSeriesItemsColors();
        }

        private void Bind_GridData()
        {
            try
            {
                lobjCmd = new SqlCommand("SimulatorResultTest", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@state", Session["State"].ToString());
                lobjCmd.Parameters.AddWithValue("@county", Session["County"].ToString());
                lobjCmd.Parameters.AddWithValue("@Persona", Session["Persona"].ToString());
                lobjCmd.Parameters.AddWithValue("@planType", Session["PlanType"].ToString());
                lobjCmd.Parameters.AddWithValue("@drugCoverage", Session["Drug_Coverage"].ToString());
                lobjCmd.Parameters.AddWithValue("@sid", Convert.ToInt32(Session["sId"].ToString()));


                lobjDA = new SqlDataAdapter(lobjCmd);
                DataTable lobjDS = new DataTable();
                lobjDA.Fill(lobjDS);
                grdData.DataSource = lobjDS;
                grdData.DataBind();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private void Bind_Grid_After_Filter()
        {
            try
            {
                lobjCmd = new SqlCommand("SimulatorResultTest", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue);
                lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue);
                lobjCmd.Parameters.AddWithValue("@Persona", ddlPersona.SelectedValue);
                lobjCmd.Parameters.AddWithValue("@planType", Session["Plantype"].ToString());
                lobjCmd.Parameters.AddWithValue("@drugCoverage", Session["Drug_Coverage"].ToString());
                lobjCmd.Parameters.AddWithValue("@sid", Convert.ToInt32(Session["sId"].ToString()));

                lobjDA = new SqlDataAdapter(lobjCmd);
                DataTable lobjDS = new DataTable();
                lobjDA.Fill(lobjDS);
                grdData.DataSource = lobjDS;
                grdData.DataBind();
                ViewState["dirState"] = lobjDS;
                ViewState["sortdr"] = "Asc";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private void Bind_Services()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_Compare_OOPC", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue);
                lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue);
                lobjCmd.Parameters.AddWithValue("@Bid_Id", ddlGraph.SelectedValue);
                lobjCmd.Parameters.AddWithValue("@persona", ddlPersona.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@sId", Convert.ToInt32(Session["sId"].ToString()));

                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);

                int rowIndex = lobjDS.Tables[0].Rows.Count - 2;
                int rowIndexFinal = lobjDS.Tables[0].Rows.Count - 1;

                for (int i = 0; i < lobjDS.Tables[0].Columns.Count; i++)
                {
                    if (i != 0)
                    {
                        lobjDS.Tables[0].Rows[rowIndex][i] = Math.Round(Convert.ToDecimal(lobjDS.Tables[0].Rows[rowIndex][i].ToString()));
                        lobjDS.Tables[0].Rows[rowIndexFinal][i] = Math.Round(Convert.ToDecimal(lobjDS.Tables[0].Rows[rowIndexFinal][i].ToString()));
                    }
                }

                grd_Services.DataSource = lobjDS;
                grd_Services.DataBind();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private void Bind_LabelValues()
        {
            lblPlanName.Text = Session["PlanName"].ToString();
            lblBid_Id.Text = Session["BidId"].ToString();
        }

        private void Bind_State()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_Filter", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@Bid_id", Session["BidId"].ToString());
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@Id", 1);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlState.DataSource = lobjDS;
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "State";
                ddlState.DataBind();
                ddlState.Items.FindByText(Session["State"].ToString()).Selected = true;
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_County();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bind_UserList()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_UserList", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@CurrentUser", Session["UserName"].ToString());

                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlUser.DataSource = lobjDS;
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserName";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, "Select User");
            }
            catch (Exception Ex1)
            {

            }
        }

        private void Bind_County()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_Filter", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@Bid_id", Session["BidId"].ToString());
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@Id", 2);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlCounty.DataSource = lobjDS;
                ddlCounty.DataTextField = "County";
                ddlCounty.DataValueField = "County";
                ddlCounty.DataBind();
                ddlCounty.Items.FindByText(Session["County"].ToString()).Selected = true;

            }
            catch (Exception Ex1)
            {

            }
        }

        private void Bind_Persona()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_Filter", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@Bid_id", Session["BidId"].ToString());
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@Id", 3);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlPersona.DataSource = lobjDS;
                ddlPersona.DataTextField = "Persona";
                ddlPersona.DataValueField = "Persona";
                ddlPersona.DataBind();
                ddlPersona.Items.FindByText(Session["Persona"].ToString()).Selected = true;

            }
            catch (Exception Ex1)
            {

            }
        }

        private void Bind_Dropdowns()
        {
            Bind_State();
            Bind_County();
            Bind_Persona();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("QuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "QuickAccess.aspx");
                Response.Redirect("~/Pages/QuickAccess.aspx");
            }
            catch (Exception Ex1)
            {
            }
        }

        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label LblGrandTotal = (Label)e.Row.FindControl("LblGrandTotal");
                    Label LblGrandTotalNew = (Label)e.Row.FindControl("LblGrandTotalNew");
                    Label LblRankRankNew = (Label)e.Row.FindControl("LblRankRankNew");
                    Label LblBidID = (Label)e.Row.FindControl("LblBidID");
                    Label LblScenarioId = (Label)e.Row.FindControl("LblScenarioId");

                    double val = Convert.ToDouble(LblGrandTotal.Text);
                    double valnew = Convert.ToDouble(LblGrandTotalNew.Text);
                    string displayed_value = val.ToString("N0");
                    LblGrandTotal.Text = displayed_value;
                    LblGrandTotalNew.Text = valnew.ToString("N0");

                    string sid = Session["sId"].ToString();
                    if (sid == LblScenarioId.Text)
                    {
                        if (LblGrandTotal.Text != LblGrandTotalNew.Text)
                        {
                            e.Row.CssClass = "active";
                            if (Session["GetBidToHighlight"] == null)
                            {
                                Session["GetBidToHighlight"] = LblBidID.Text;
                            }
                        }
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void ddlPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Grid_After_Filter();
                Bind_Graph(ddlGraph.SelectedItem.Value);
                Bind_Services();
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Grid_After_Filter();
                RadHtmlChart1.DataSource = null;
                RadHtmlChart1.DataBind();
                Bind_Graph(ddlGraph.SelectedItem.Value);
            }
            catch (Exception Ex1)
            {

            }
        }

        private void Insert_SimulatorOutput_Save()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_SimulatorOutput_Save", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@BidId", Session["BidId"].ToString());
            lobjCmd.Parameters.AddWithValue("@State", ddlState.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@Persona", ddlPersona.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        private void Insert_SimulatorOutput_SaveAS()
        {
            lobjCon.Open();

            lobjCmd = new SqlCommand("Insert_SaveAsScenario", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Convert.ToInt32(Session["OldSId"].ToString()));
            lobjCmd.Parameters.AddWithValue("@NewScenarionID", Convert.ToInt32(Session["sId"].ToString()));
            lobjCmd.Parameters.AddWithValue("@Flag", 2);
            lobjCmd.ExecuteNonQuery();

            lobjCmd = new SqlCommand("Insert_SimulatorOutput_Save", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@BidId", Session["BidId"].ToString());
            lobjCmd.Parameters.AddWithValue("@State", ddlState.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@Persona", ddlPersona.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        private void Insert_Pages()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_Pages", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@PageName", HfPageLink.Value);
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        private void Insert_Scenario()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_Scenario", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@scenarioName", txtScenario.Text);
            lobjCmd.Parameters.AddWithValue("@description ", txtScenarioDesc.Text);
            lobjCmd.Parameters.AddWithValue("@userName ", Session["UserName"].ToString());
            lobjCmd.Parameters.AddWithValue("@SharedBy", "");
            lobjCmd.Parameters.AddWithValue("@SharedDescription", "");
            lobjCmd.Parameters.Add("@new_identity", SqlDbType.Int).Direction = ParameterDirection.Output;
            lobjCmd.ExecuteNonQuery();
            int id = Convert.ToInt32(lobjCmd.Parameters["@new_identity"].Value.ToString());
            Session["sId"] = id;

            lobjCon.Close();
        }

        private void Get_SimulatorSave()
        {
            lobjCmd = new SqlCommand("Get_SimulatorOutput_Save", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCon.Open();
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjCmd.Parameters.AddWithValue("@Bid_Id", lblBid_Id.Text);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            lobjCon.Close();

            if (lobjDS.Tables[0].Rows.Count > 0)
            {
                string State = lobjDS.Tables[0].Rows[0]["State"].ToString();
                string county = lobjDS.Tables[0].Rows[0]["County"].ToString();
                string Persona = lobjDS.Tables[0].Rows[0]["Persona"].ToString();

                lobjCmd = new SqlCommand("Get_Filter", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@Bid_id", Session["BidId"].ToString());
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@Id", 1);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlState.DataSource = lobjDS;
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "State";
                ddlState.DataBind();
                ddlState.Items.FindByText(State).Selected = true;

                lobjCmd = new SqlCommand("Get_Filter", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@Bid_id", Session["BidId"].ToString());
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@Id", 2);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlCounty.DataSource = lobjDS;
                ddlCounty.DataTextField = "County";
                ddlCounty.DataValueField = "County";
                ddlCounty.DataBind();
                ddlCounty.Items.FindByText(county).Selected = true;

                lobjCmd = new SqlCommand("Get_Filter", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@Bid_id", Session["BidId"].ToString());
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@Id", 3);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlPersona.DataSource = lobjDS;
                ddlPersona.DataTextField = "Persona";
                ddlPersona.DataValueField = "Persona";
                ddlPersona.DataBind();
                ddlPersona.Items.FindByText(Persona).Selected = true;

                Bind_Grid_After_Filter();
            }
            else
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }

        }

        protected void btnrevert_Click(object sender, EventArgs e)
        {
            try
            {
                Get_SimulatorSave();
                Bind_Graph(ddlGraph.SelectedItem.Value);
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void btnSaveAs_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal3()", true);
            Bind_Graph(ddlGraph.SelectedItem.Value);
        }

        protected void btnSaveFilters_Click(object sender, EventArgs e)
        {
            try
            {
                Insert_SimulatorOutput_Save();
                Insert_Pages();
                Bind_Graph(ddlGraph.SelectedItem.Value);
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void lnk_service_Click(object sender, EventArgs e)
        {
            try
            {
                if (grd_Services.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition",
                     "attachment;filename=PlanComparison.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    StringBuilder columnbind1 = new StringBuilder();
                    for (int k = 0; k < grd_Services.HeaderRow.Cells.Count; k++)
                    {
                        columnbind1.Append(grd_Services.HeaderRow.Cells[k].Text + ',');
                    }

                    columnbind1.Append("\r\n");
                    for (int i = 0; i < grd_Services.Rows.Count; i++)
                    {
                        for (int k = 0; k < grd_Services.HeaderRow.Cells.Count; k++)
                        {
                            if (grd_Services.Rows[i].Cells[k].Text.Contains(","))
                                columnbind1.Append(grd_Services.Rows[i].Cells[k].Text.ToString().Replace(",", "") + ',');
                            else
                                columnbind1.Append(grd_Services.Rows[i].Cells[k].Text + ',');
                        }

                        columnbind1.Append("\r\n");
                    }
                    Response.Output.Write(columnbind1.ToString());
                    Response.Flush();
                    Response.End();
                    Bind_Graph(ddlGraph.SelectedItem.Value);
                }
            }
            catch (Exception ex2)
            {


            }
        }

        protected void lnk_dwn_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdData.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition",
                     "attachment;filename=SimulatedOOPC.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";

                    StringBuilder sb = new StringBuilder();
                    for (int k = 0; k < grdData.Columns.Count; k++)
                    {
                        //add separator
                        if (k != 0)
                            sb.Append(grdData.Columns[k].HeaderText + ',');

                    }
                    //append new line
                    sb.Append("\r\n");
                    for (int i = 0; i < grdData.Rows.Count; i++)
                    {
                        Label lblPlanName = (Label)grdData.Rows[i].FindControl("LblPlanName");
                        Label LblGrandTotal = (Label)grdData.Rows[i].FindControl("LblGrandTotal");
                        Label LblGrandTotalNew = (Label)grdData.Rows[i].FindControl("LblGrandTotalNew");
                        Label LblRank = (Label)grdData.Rows[i].FindControl("LblRank");
                        Label LblRankNew = (Label)grdData.Rows[i].FindControl("LblRankNew");

                        sb.Append(lblPlanName.Text + "," + LblGrandTotal.Text + "," + LblGrandTotalNew.Text + "," + LblRank.Text + "," + LblRankNew.Text + ",");
                        sb.Append("\r\n");
                    }
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex2)
            {

            }

        }

        protected void btnPopUpSave_Click(object sender, EventArgs e)
        {
            try
            {
                lobjCon.Open();
                lobjCmd = new SqlCommand("Get_All_Scenarios", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@userName ", Session["UserName"].ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                DataTable dt = new DataTable();
                lobjDA.Fill(dt);
                var dr = dt.Select("ScenarioName = " + "'" + txtScenario.Text + "'");
                lobjCon.Close();
                if (dr.Count() > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Java", "AlreadyExist()", true);
                }
                else
                {
                    Session["OldSId"] = Session["sId"].ToString();
                    Insert_Scenario();
                    txtScenario.Text = "";
                    txtScenarioDesc.Text = "";
                    Insert_SimulatorOutput_SaveAS();
                    Insert_Pages();
                }
                Bind_Graph(ddlGraph.SelectedItem.Value);
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);
                Bind_Graph(ddlGraph.SelectedItem.Value);
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void btnPopUpShare_Click(object sender, EventArgs e)
        {
            try
            {
                lobjCmd = new SqlCommand("Get_All_Scenarios", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@userName ", Session["UserName"].ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                DataTable dt = new DataTable();
                lobjDA.Fill(dt);

                var Sessionid = dt.Select("sId = " + Convert.ToInt32(Session["sId"].ToString()));
                foreach (var item in Sessionid)
                {
                    lobjCon.Open();
                    lobjCmd = new SqlCommand("Insert_Scenario", lobjCon);
                    lobjCmd.CommandType = CommandType.StoredProcedure;
                    lobjCmd.Parameters.AddWithValue("@scenarioName", item["ScenarioName"].ToString());
                    lobjCmd.Parameters.AddWithValue("@description ", item["Description"].ToString());
                    lobjCmd.Parameters.AddWithValue("@userName ", ddlUser.SelectedValue);
                    lobjCmd.Parameters.AddWithValue("@SharedBy", Session["UserName"].ToString());
                    lobjCmd.Parameters.AddWithValue("@SharedDescription", txtMessage.Text);
                    lobjCmd.Parameters.Add("@new_identity", SqlDbType.Int).Direction = ParameterDirection.Output;
                    lobjCmd.ExecuteNonQuery();
                    int id = Convert.ToInt32(lobjCmd.Parameters["@new_identity"].Value.ToString());
                    Session["newsId"] = id;
                    lobjCon.Close();
                    InsertCopyRecord();
                    // To mention Scenarioname in mail
                    Session["Passscenario"] = item["ScenarioName"].ToString();
                }
                SendMail();
                Bind_Graph(ddlGraph.SelectedItem.Value);
            }
            catch (Exception Ex1)
            {

            }
        }

        private void InsertCopyRecord()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_ShareCopyRecord", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjCmd.Parameters.AddWithValue("@NewScenarionID", Session["newsId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();

        }

        private void SendMail()
        {
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(Constants.fromAddress);
            Msg.To.Add(ddlUser.SelectedValue);
            Msg.Subject = Session["UserName"].ToString() + " has shared a plan with you.";
            Msg.Body = "Hi,<br/><br/>Please find the shared plan" + Session["Passscenario"].ToString() + "<br/><br/> " + txtMessage.Text + "<br/><br/>  Regards, <br/>" + Session["UserName"].ToString() + "<br/>" + ".";
            Msg.IsBodyHtml = true;
            Msg.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Constants.hostAddress;
            smtp.EnableSsl = true;
            smtp.Port = Constants.portNumber;
            smtp.Credentials = new System.Net.NetworkCredential(Constants.fromAddress, Constants.passWord);
            smtp.Send(Msg);
        }

        protected void grd_Services_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {

                        if (e.Row.Cells[i].Text != "&nbsp;")
                        {
                            if (e.Row.Cells[i].Text != string.Empty && e.Row.Cells[i].Text != "")
                            {
                                e.Row.Cells[i].Text = String.Format("{0:n}", Convert.ToDecimal(e.Row.Cells[i].Text));
                            }
                        }
                    }
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        if (e.Row.Cells[i].Text != "&nbsp;")
                        {
                            if (i == 1)
                            {
                                Double a = 0, b = 0;

                                if (e.Row.Cells[i].Text != null)
                                {
                                    a = Convert.ToDouble(e.Row.Cells[i].Text);
                                }
                                if (e.Row.Cells[i + 1].Text != "&nbsp;")
                                {
                                    b = Convert.ToDouble(e.Row.Cells[i + 1].Text);
                                }

                                if (a < b)
                                {
                                    e.Row.Cells[i + 1].ForeColor = Color.Red;
                                }
                                else if (a > b)
                                {
                                    e.Row.Cells[i + 1].ForeColor = Color.Green;
                                }
                                else
                                {
                                    e.Row.Cells[i + 1].ForeColor = Color.Black;
                                }
                            }
                        }
                    }

                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        if (e.Row.Cells[i].Text != "&nbsp;")
                        {
                            if (e.Row.Cells[i].Text.Length > 1)
                                e.Row.Cells[i].Text = "$" + e.Row.Cells[i].Text.TrimEnd('0');
                            else
                                e.Row.Cells[i].Text = "$" + e.Row.Cells[i].Text;
                            if (e.Row.Cells[i].Text.ToString().EndsWith("."))
                                e.Row.Cells[i].Text = e.Row.Cells[i].Text.TrimEnd('.');
                        }
                        else
                        {
                            e.Row.Cells[i].Text = "_";
                        }
                        e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("_", " ");
                    }
                }
            }
            catch (Exception Ex1)
            {

            }
        }

        private void Bind_Graph_Bid()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_BId_Graph", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@sId", Convert.ToInt32(Session["sId"].ToString()));
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlGraph.DataSource = lobjDS;
                ddlGraph.DataTextField = "Plan_Name";
                ddlGraph.DataValueField = "bid_id";
                ddlGraph.DataBind();
                string bididofoopc = null;
                if (Session["GetBidToHighlight"] != null)
                {
                    bididofoopc = Session["GetBidToHighlight"].ToString();
                }
                if (bididofoopc != null)
                {
                    ddlGraph.Items.FindByValue(bididofoopc).Selected = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        protected void ddlGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Graph(ddlGraph.SelectedValue.ToString());
                Bind_Services();

            }
            catch (Exception Ex1)
            {
            }
        }
    }
}