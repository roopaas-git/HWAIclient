using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HealthWorks.Pages
{
    public partial class PlanFinder : System.Web.UI.Page
    {
        public SqlConnection lobjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
        public SqlCommand lobjCmd;
        public DataSet lobjDS;
        public SqlDataAdapter lobjDA;
        Dictionary<string, string> returnValue;
        JsonFile jsonFile = new JsonFile();
        string stateName = string.Empty;
        string countyName = string.Empty;
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
                    returnValue = jsonFile.ReadJsonFile();
                    var first = returnValue.First();
                    string stateName = first.Key;
                    string countyName = first.Value;

                    if (Session["SelectedState"] == null)
                        Session["SelectedState"] = stateName;

                    if (Session["SelectedCounty"] == null)
                        Session["SelectedCounty"] = countyName;
                }

                if (!IsPostBack)
                {
                    CheckQuickAccess();
                    lblScenarioName.Text = Session["sName"].ToString();
                    Check_And_Bind_Plan_List();
                }
                Active_DeactiveLinks();
                this.Validate();
                ddlState.Filter = RadComboBoxFilter.StartsWith;
                ddlCounty.Filter = RadComboBoxFilter.StartsWith;
            }
            catch (Exception ex)
            {

            }
        }

        #region Dropdown Events

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCounty.Enabled = true;
            ddlPlanType.Items.Clear();
            ddlDrugCoverage.Items.Clear();
            ddlPlanType.Enabled = false;
            ddlDrugCoverage.Enabled = false;
            grdData.DataSource = null;
            grdData.DataBind();
            try
            {
                Bind_County();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDrugCoverage.Enabled = true;
            try
            {
                Bind_DrugCoverage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void ddlDrugCoverage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPlanType.Enabled = true;
            try
            {
                Bind_PlanType();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void ddlPlanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Bind_Persona();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void ddlPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iValue = 0;
            Bind_DrugList();
            Bind_Top_Grid();
            Bind_Bottom_Grid(iValue);
            Bind_All_Service_Data();
        }

        #endregion

        #region Click Events

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

        protected void btnsimulate_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in grdData.Rows)
                {
                    CheckBox chkcheck = (CheckBox)row.FindControl("chkRow");
                    if (chkcheck.Checked == true)
                    {
                        Insert_Save();
                        Insert_Save_PageNames();
                        PT.InsertDataIntoDB("QuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "QuickAccess.aspx");
                        Response.Redirect("~/Pages/QuickAccess.aspx");
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void lbRevert_Click(object sender, EventArgs e)
        {
            try
            {
                Check_And_Bind_Plan_List();
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void lbSaveAs_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PlanName"] != null)
                    Session["PlanName"] = null;
                Insert_Save();
                Insert_Save_PageNames();
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdData.Rows.Count > 0)
                {
                    grdData.Columns[0].Visible = false;
                    grdData.Columns[1].Visible = false;

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition",
                     "attachment;filename=PlanFinderPlanList.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    StringBuilder sb = new StringBuilder();
                    for (int k = 0; k < grdData.Columns.Count; k++)
                    {
                        if (k != 0 && k != 1 && k != 4)
                        {
                            sb.Append(grdData.Columns[k].HeaderText + ',');
                        }
                    }
                    sb.Append("\r\n");
                    for (int i = 0; i < grdData.Rows.Count; i++)
                    {
                        Label LblContract = (Label)grdData.Rows[i].FindControl("LblContract");
                        Label LblPlanID = (Label)grdData.Rows[i].FindControl("LblPlanID");
                        Label LblPlanName = (Label)grdData.Rows[i].FindControl("LblPlanName");
                        Label LblPO = (Label)grdData.Rows[i].FindControl("LblPO");
                        Label LblGrandTotal = (Label)grdData.Rows[i].FindControl("LblGrandTotal");
                        Label LblRank = (Label)grdData.Rows[i].FindControl("LblRank");

                        if (LblPlanName.Text.Contains(","))
                        {
                            LblPlanName.Text.ToString().Replace(",", "");
                        }
                        if (LblPO.Text.Contains(","))
                        {
                            LblPO.Text.ToString().Replace(",", "");
                        }
                        if (LblGrandTotal.Text.Contains(","))
                        {
                            LblGrandTotal.Text.ToString().Replace(",", "");
                        }
                        if (LblRank.Text.Contains(","))
                        {
                            LblRank.Text.ToString().Replace(",", "");
                        }
                        sb.Append(LblContract.Text + "," + LblPlanID.Text + "," + LblPlanName.Text + "," + LblPO.Text + "," + LblGrandTotal.Text + "," + LblRank.Text + ",");
                        sb.Append("\r\n");
                    }
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                    grdData.Columns[0].Visible = true;
                    grdData.Columns[1].Visible = true;
                }
            }
            catch (Exception ex2)
            {
            }
        }

        protected void lbDownload1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdData1.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition",
                     "attachment;filename=PlanComparison.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    StringBuilder columnbind = new StringBuilder();
                    for (int k = 0; k < grdData1.HeaderRow.Cells.Count; k++)
                    {
                        columnbind.Append(grdData1.HeaderRow.Cells[k].Text + ',');
                    }

                    columnbind.Append("\r\n");
                    for (int i = 0; i < grdData1.Rows.Count; i++)
                    {
                        for (int k = 0; k < grdData1.HeaderRow.Cells.Count; k++)
                        {
                            if (grdData1.Rows[i].Cells[k].Text.Contains(","))
                                columnbind.Append(grdData1.Rows[i].Cells[k].Text.ToString().Replace(",", "") + ',');
                            else
                                columnbind.Append(grdData1.Rows[i].Cells[k].Text + ',');
                        }

                        columnbind.Append("\r\n");
                    }
                    Response.Output.Write(columnbind.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex2)
            {
            }
        }

        protected void lblPDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Gv_All_Services.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition",
                     "attachment;filename=PlanComparison.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    StringBuilder columnbind = new StringBuilder();
                    for (int k = 0; k < Gv_All_Services.HeaderRow.Cells.Count; k++)
                    {
                        columnbind.Append(Gv_All_Services.HeaderRow.Cells[k].Text + ',');
                    }

                    columnbind.Append("\r\n");
                    for (int i = 0; i < Gv_All_Services.Rows.Count; i++)
                    {
                        for (int k = 0; k < Gv_All_Services.HeaderRow.Cells.Count; k++)
                        {
                            if (Gv_All_Services.Rows[i].Cells[k].Text.Contains(","))
                                columnbind.Append(Gv_All_Services.Rows[i].Cells[k].Text.ToString().Replace(",", "") + ',');
                            else
                                columnbind.Append(Gv_All_Services.Rows[i].Cells[k].Text + ',');
                        }

                        columnbind.Append("\r\n");
                    }
                    Response.Output.Write(columnbind.ToString());
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
                    lblScenarioName.Text = txtScenario.Text;
                    Session["sName"] = txtScenario.Text;
                    lobjCon.Close();

                    txtScenario.Text = "";
                    txtScenarioDesc.Text = "";

                    Insert_Save();
                    Insert_Save_PageNames();
                }
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void btnDetailed_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal1()", true);
        }

        #endregion

        #region GridView Events

        protected void grdData_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            int indexValue = e.NewSelectedIndex;
            Label lblBid = grdData.Rows[indexValue].FindControl("LblBidID") as Label;
            Label lblPlanName = grdData.Rows[indexValue].FindControl("LblPlanName") as Label;
            Label LblPO = grdData.Rows[indexValue].FindControl("LblPO") as Label;
            string Bid_Id = lblBid.Text;
            Session["BidId"] = Bid_Id;
            Session["PlanName"] = lblPlanName.Text;

            Insert_Save();
            Insert_Save_PageNames();
            Response.Redirect("~/Pages/QuickAccess.aspx");
        }

        protected void grdData1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text != "&nbsp;")
                    {
                        e.Row.Cells[i].Text = String.Format("{0:n}", Convert.ToDecimal(e.Row.Cells[i].Text));
                        if (e.Row.Cells[i].Text.Length > 1)
                            e.Row.Cells[i].Text = "$" + e.Row.Cells[i].Text.TrimEnd('0');
                        else
                            e.Row.Cells[i].Text = "$" + e.Row.Cells[i].Text;
                        if (e.Row.Cells[i].Text.ToString().EndsWith("."))
                            e.Row.Cells[i].Text = e.Row.Cells[i].Text.TrimEnd('.');
                    }
                    else
                        e.Row.Cells[i].Text = "_";
                }
                e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("_", " ");
            }
        }

        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblGrandTotal = (Label)e.Row.FindControl("LblGrandTotal");
                if (LblGrandTotal.Text != null && LblGrandTotal.Text != string.Empty)
                {
                    double val = Convert.ToDouble(LblGrandTotal.Text);
                    string displayed_value = val.ToString("N0");
                    LblGrandTotal.Text = "$" + displayed_value;
                }
                else
                {
                    LblGrandTotal.Text = "_";
                }
            }
        }

        protected Dictionary<int, CheckBoxState> checkBoxState;

        protected void grdData_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                this.checkBoxState = new Dictionary<int, CheckBoxState>();
                foreach (GridViewRow row in grdData.Rows)
                {
                    CheckBox chkRow = (CheckBox)row.FindControl("chkRow");
                    if (chkRow.Checked)
                    {
                        int key = Int32.Parse(grdData.DataKeys[row.RowIndex].Value.ToString());

                        // Save CheckBox state.
                        CheckBoxState state = new CheckBoxState(chkRow.Checked);
                        this.checkBoxState.Add(key, state);
                    }
                }
                DataTable dtrslt = (DataTable)ViewState["dirState"];
                if (dtrslt.Rows.Count > 0)
                {
                    if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                    {
                        dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                        ViewState["sortdr"] = "Desc";
                    }
                    else
                    {
                        dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                        ViewState["sortdr"] = "Asc";
                    }
                    grdData.DataSource = dtrslt;
                    grdData.DataBind();
                }

            }
            catch (Exception Ex1)
            {

            }
        }

        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {

            string selectedBidID = string.Empty;
            string selectedcolumn = string.Empty;
            int iCounter = 0;

            foreach (GridViewRow row in grdData.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    iCounter += 1;
                    Label LblBidID = row.FindControl("LblBidID") as Label;
                    selectedBidID += LblBidID.Text + "'," + "'";
                    selectedcolumn += LblBidID.Text + "]," + "[";
                }
            }

            if (selectedBidID != string.Empty)
            {
                selectedBidID = selectedBidID.Substring(0, selectedBidID.Length - 3);
                selectedBidID = selectedBidID.ToString();
                Session["BidID"] = selectedBidID;

                selectedcolumn = "[" + selectedcolumn.Substring(0, selectedcolumn.Length - 2);
                Session["BidColumn"] = selectedcolumn;
                Bind_Bottom_Grid(iCounter);
                Bind_All_Service_Data1();
            }
            else
            {
                Bind_Bottom_Grid(iCounter);
            }
        }

        protected void grdData_DataBound(object sender, EventArgs e)
        {

            if (checkBoxState != null)
            {
                foreach (GridViewRow row in grdData.Rows)
                {
                    int key = Int32.Parse(grdData.DataKeys[row.RowIndex].Value.ToString());
                    CheckBoxState state;

                    if (checkBoxState.TryGetValue(key, out state))
                    {
                        CheckBox chkRow = (CheckBox)row.FindControl("chkRow");

                        // Restore state
                        chkRow.Checked = state.checkRow;
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ProductIntelC2L3li"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
            }

        }

        private void CheckQuickAccess()
        {
            SqlCommand lobjCmd = new SqlCommand("Get_QuickAccess1", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Convert.ToInt32(Session["sId"].ToString()));
            lobjCon.Open();
            SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
            DataSet ds = new DataSet();
            sd.Fill(ds);
            lobjCon.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                id_Quick.Style.Add("pointer-events", "auto");
                id_Simulate.Style.Add("pointer-events", "auto");
            }
            else
            {
                id_Quick.Style.Add("pointer-events", "none");
                id_Simulate.Style.Add("pointer-events", "none");
            }
        }

        private void Check_And_Bind_Plan_List()
        {
            lobjCmd = new SqlCommand("Get_PlanFinder_Save", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);

            if (lobjDS.Tables[0].Rows.Count > 0)
            {
                // More Plan Update
                Session["GetSavedBidId"] = lobjDS.Tables[0].Rows[0]["Bid_id"].ToString();
                Bind_All_Filters(lobjDS);
            }
            else
            {
                Bind_State();
            }
        }

        private void Bind_All_Filters(DataSet ds)
        {
            ddlCounty.Enabled = true;
            ddlDrugCoverage.Enabled = true;
            ddlPlanType.Enabled = true;
            ddlPersona.Enabled = true;

            Bind_State_Values(ds.Tables[0].Rows[0]["State"].ToString());
            Bind_County_Values(ds.Tables[0].Rows[0]["County"].ToString());
            Bind_DrugCoverage_Values(ds.Tables[0].Rows[0]["Drug_Coverage"].ToString());
            Bind_PlanType_Values(ds.Tables[0].Rows[0]["Plan_Type"].ToString());
            Bind_Persona_Values(ds.Tables[0].Rows[0]["Persona"].ToString());
        }

        private void Bind_State_Values(string selectedState)
        {
            try
            {
                lobjCmd = new SqlCommand("Get_States", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlState.DataSource = lobjDS;
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "State";
                ddlState.DataBind();
                ddlState.SelectedValue = selectedState;

                foreach (RadComboBoxItem itm in ddlState.Items)
                {
                    if (returnValue.ContainsKey(itm.Value))
                        Console.WriteLine("Key :" + itm.Value + " is present");
                    else
                    {
                        itm.Attributes.Add("style", "color:gray;");
                        itm.Attributes.Add("disabled", "true");
                    }
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

        private void Bind_County_Values(string selectedCounty)
        {
            lobjCmd = new SqlCommand("Get_Counties", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlCounty.DataSource = lobjDS;
            ddlCounty.DataTextField = "County";
            ddlCounty.DataValueField = "County";
            ddlCounty.DataBind();
            ddlCounty.SelectedValue = selectedCounty;
        }

        private void Bind_DrugCoverage_Values(string selectedCoverage)
        {
            lobjCmd = new SqlCommand("Get_DrugCoverage", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlDrugCoverage.DataSource = lobjDS;
            ddlDrugCoverage.DataTextField = "Drug_Coverage";
            ddlDrugCoverage.DataValueField = "Drug_Coverage";
            ddlDrugCoverage.DataBind();
            ddlDrugCoverage.Items.FindByText(selectedCoverage).Selected = true;
        }

        private void Bind_PlanType_Values(string selectedPlanType)
        {
            lobjCmd = new SqlCommand("Get_PlanTypes", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@drugCoverage", ddlDrugCoverage.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlPlanType.DataSource = lobjDS;
            ddlPlanType.DataTextField = "PlanType";
            ddlPlanType.DataValueField = "PlanType";
            ddlPlanType.DataBind();

            string[] Plantypelist = selectedPlanType.ToString().Split(',');
            var getplan = Plantypelist;
            foreach (var item in getplan)
            {
                RadComboBoxItem items = ddlPlanType.FindItemByText(item.ToString());
                items.Checked = true;
            }
        }

        private void Bind_Persona_Values(string selectedPersona)
        {
            try
            {
                lobjCmd = new SqlCommand("Get_Persona", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlPersona.DataSource = lobjDS;
                ddlPersona.DataTextField = "Persona";
                ddlPersona.DataValueField = "Persona";
                ddlPersona.DataBind();
                ddlPersona.Items.FindByText(selectedPersona).Selected = true;
                ddlPersona.Items.Insert(0, "Select Persona");
                ddlPersona_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private void Bind_State()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_States", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlState.DataSource = lobjDS;
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "State";
                ddlState.DataBind();
                ddlState.SelectedValue = Session["SelectedState"].ToString();

                foreach (RadComboBoxItem itm in ddlState.Items)
                {
                    if (returnValue.ContainsKey(itm.Value))
                        Console.WriteLine("Key :" + itm.Value + " is present");
                    else
                    {
                        itm.Attributes.Add("style", "color:gray;");
                        itm.Attributes.Add("disabled", "true");
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

        private void Bind_County()
        {
            lobjCmd = new SqlCommand("Get_Counties", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlCounty.DataSource = lobjDS;
            ddlCounty.DataTextField = "County";
            ddlCounty.DataValueField = "County";
            ddlCounty.DataBind();


            if (returnValue.ContainsKey(ddlState.SelectedValue.ToString()))
            {
                ddlCounty.SelectedValue = returnValue[ddlState.SelectedValue.ToString()];
                ddlPersona.Enabled = true;
                ddlCounty_SelectedIndexChanged(this, null);
            }
            else
            {
                ddlCounty.Enabled = false;
                // ddlDrugCoverage.Enabled = false;
                ddlPlanType.SelectedIndex = 0;
                ddlPlanType.Enabled = false;
                ddlPersona.Enabled = false;
                ddlPersona.SelectedIndex = 0;
            }

        }

        private void Bind_DrugCoverage()
        {
            lobjCmd = new SqlCommand("Get_DrugCoverage", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlDrugCoverage.DataSource = lobjDS;
            ddlDrugCoverage.DataTextField = "Drug_Coverage";
            ddlDrugCoverage.DataValueField = "Drug_Coverage";
            ddlDrugCoverage.DataBind();


            if (returnValue.ContainsKey(ddlState.SelectedValue.ToString()))
            {
                ddlDrugCoverage.Items.FindByText("MAPD").Selected = true;
                ddlDrugCoverage_SelectedIndexChanged(null, null);
            }
        }

        private void Bind_PlanType()
        {
            lobjCmd = new SqlCommand("Get_PlanTypes", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@drugCoverage", ddlDrugCoverage.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlPlanType.DataSource = lobjDS;
            ddlPlanType.DataTextField = "PlanType";
            ddlPlanType.DataValueField = "PlanType";
            ddlPlanType.DataBind();

            foreach (RadComboBoxItem itm in ddlPlanType.Items)
            {
                itm.Checked = true;
            }

            if (returnValue.ContainsKey(ddlState.SelectedValue.ToString()))
            {
                ddlPlanType_SelectedIndexChanged(this, null);
            }
        }

        private void Bind_Persona()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_Persona", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlPersona.DataSource = lobjDS;
                ddlPersona.DataTextField = "Persona";
                ddlPersona.DataValueField = "Persona";
                ddlPersona.DataBind();
                ddlPersona.Items.FindByText("Very Good Health").Selected = true;
                ddlPersona.Items.Insert(0, "Select Persona");

                if (returnValue.ContainsKey(ddlState.SelectedValue.ToString()))
                {
                    ddlPersona_SelectedIndexChanged(this, null);
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

        private void Bind_DrugList()
        {
            lobjCmd = new SqlCommand("Get_Drugs", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@Persona", ddlPersona.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);

            if (lobjDS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < lobjDS.Tables[0].Rows.Count; i++)
                {
                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.ID = lobjDS.Tables[0].Rows[i]["DrugName"].ToString() + "_" + i.ToString();
                    span.InnerHtml = lobjDS.Tables[0].Rows[i]["DrugName"].ToString();
                    span.Attributes["class"] = "dropdown-item-text";
                    divDrugs.Controls.Add(span);
                }
            }
        }

        public void Bind_Top_Grid()
        {
            try
            {
                string selectedPlantype = Get_Selected_PlanTypes();

                lobjCmd = new SqlCommand("Get_Plan_List1", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@drugCover", ddlDrugCoverage.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@planType", selectedPlantype);
                lobjCmd.Parameters.AddWithValue("@persona", ddlPersona.SelectedValue.ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                DataTable lobjDS = new DataTable();
                lobjDA.Fill(lobjDS);
                grdData.DataSource = lobjDS;
                grdData.DataBind();
                ViewState["dirState"] = lobjDS;
                ViewState["sortdr"] = "Asc";
                // More Plan Update
                if (Session["GetSavedBidId"] != null)
                {
                    string BidID = Session["GetSavedBidId"].ToString();
                    string[] GetBidID = BidID.ToString().Split(',');
                    foreach (GridViewRow row in grdData.Rows)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        Label LblBidID = (row.Cells[0].FindControl("LblBidID") as Label);

                        var GetBidIDlist = GetBidID;
                        foreach (var item in GetBidIDlist)
                        {
                            if (LblBidID.Text == item.ToString())
                            {
                                chkRow.Checked = true;
                            }
                        }
                    }
                }
                else
                {
                    if (lobjDS.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (GridViewRow row in grdData.Rows)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            i++;
                            chkRow.Checked = true;
                            if (i == 3)
                            {
                                break;
                            }

                        }
                    }
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

        public void Bind_Bottom_Grid(int counter)
        {
            try
            {
                string planTypes = Get_Selected_PlanTypes();

                lobjCmd = new SqlCommand("Get_Services_Data", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@planType", planTypes);
                lobjCmd.Parameters.AddWithValue("@drugCoverage", ddlDrugCoverage.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@persona", ddlPersona.SelectedValue.ToString());

                if (Session["GetSavedBidId"] != null)
                {
                    string BidID = Session["GetSavedBidId"].ToString();
                    string formstring = string.Empty;
                    string[] GetBidID = BidID.ToString().Split(',');
                    var GetBidIDlist = GetBidID;
                    foreach (var item in GetBidIDlist)
                    {
                        formstring += item + "'," + "'";
                    }
                    formstring = formstring.Substring(0, formstring.Length - 3);

                    Session["BidID"] = formstring;
                }

                int flag = 2;
                if (Session["BidID"] != null)
                    lobjCmd.Parameters.AddWithValue("@Bid_Id", Session["BidID"].ToString());
                else
                    lobjCmd.Parameters.AddWithValue("@Bid_Id", string.Empty);
                if (Session["BidColumn"] != null)
                    lobjCmd.Parameters.AddWithValue("@Bid_Column", Session["BidColumn"].ToString());
                else
                    lobjCmd.Parameters.AddWithValue("@Bid_Column", string.Empty);
                if (Session["GetSavedBidId"] != null)
                {
                    lobjCmd.Parameters.AddWithValue("@isFirst", flag);
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@isFirst", counter);
                }

                Session["GetSavedBidId"] = null;

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

                grdData1.DataSource = lobjDS;
                grdData1.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        public void Bind_All_Service_Data()
        {
            try
            {
                string planTypes = Get_Selected_PlanTypes();

                lobjCmd = new SqlCommand("Get_All_Services_Data_Default", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@planType", planTypes);
                lobjCmd.Parameters.AddWithValue("@drugCoverage", ddlDrugCoverage.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@persona", ddlPersona.SelectedValue.ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                Gv_All_Services.DataSource = lobjDS;
                Gv_All_Services.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        public void Bind_All_Service_Data1()
        {
            try
            {
                string planTypes = Get_Selected_PlanTypes();

                lobjCmd = new SqlCommand("Get_All_Services_Data_Selected", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@planType", planTypes);
                lobjCmd.Parameters.AddWithValue("@drugCoverage", ddlDrugCoverage.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@persona", ddlPersona.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@Bid_Id", Session["BidID"].ToString());
                lobjCmd.Parameters.AddWithValue("@Bid_Column", Session["BidColumn"].ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                Gv_All_Services.DataSource = lobjDS;
                Gv_All_Services.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private string Get_Selected_PlanTypes()
        {
            string selectedPlantype = string.Empty;
            var PlantypeCollection = ddlPlanType.CheckedItems;
            foreach (var item in PlantypeCollection)
            {
                selectedPlantype += item.Text + ",";
            }
            selectedPlantype = selectedPlantype.Substring(0, selectedPlantype.Length - 1);

            return selectedPlantype;
        }

        public void Insert_Save()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_PlanFinder_Save", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            string selectedPlantype = Get_Selected_PlanTypes();

            string selectedBidID = string.Empty;
            string selectedPlan = string.Empty;

            foreach (GridViewRow row in grdData.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    Label LblBidID = row.FindControl("LblBidID") as Label;
                    Label LblPlanName = row.FindControl("LblPlanName") as Label;

                    selectedBidID += LblBidID.Text + ",";
                    selectedPlan += LblPlanName.Text + ",";
                }
            }
            if (selectedBidID.Length > 1)
            {
                selectedBidID = selectedBidID.Substring(0, selectedBidID.Length - 1);
            }
            else
            {
                selectedBidID = "";
            }

            if (selectedPlan.Length > 1)
            {
                selectedPlan = selectedPlan.Substring(0, selectedPlan.Length - 1);
            }
            else
            {
                selectedPlan = "";
            }

            lobjCmd.Parameters.AddWithValue("@State", ddlState.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@County", ddlCounty.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@drugCoverage", ddlDrugCoverage.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@Plan_Type", selectedPlantype);
            lobjCmd.Parameters.AddWithValue("@Persona", ddlPersona.SelectedValue);
            lobjCmd.Parameters.AddWithValue("@PlanName", selectedPlan);
            lobjCmd.Parameters.AddWithValue("@Bid_id", selectedBidID);
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        private void Insert_Save_PageNames()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_Pages", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@PageName", HfPageLink.Value);
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        public class CheckBoxState
        {
            public CheckBoxState(bool checkRow)
            {
                this._checkRow = checkRow;
            }

            private bool _checkRow;
            public bool checkRow
            {
                get { return _checkRow; }
                set { _checkRow = value; }
            }
        }

        #endregion
    }
}