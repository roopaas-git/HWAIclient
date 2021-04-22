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
    public partial class MPFSimulator : System.Web.UI.Page
    {
        public SqlConnection lobjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
        public SqlCommand lobjCmd;
        public DataSet lobjDS;
        public SqlDataAdapter lobjDA;
        public bool isUpdate;

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
                    Active_DeactiveLinks();
                }

                if (!IsPostBack)
                {
                    Session["SelectedBidId"] = null;
                    Session["isUpdate"] = "No";
                    BindAllDropDowns();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ddlState.Filter = RadComboBoxFilter.StartsWith;
            ddlCounty.Filter = RadComboBoxFilter.StartsWith;
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("MarketIntelC1L4Li"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";

            }

        }

        #region Bind Data to UI

        public void BindAllDropDowns()
        {
            BindPlanTypes();
            //BindStates();
            //BindCounties();
            //BindMPFSort();
            //BindPlansGrid();
        }

        public void BindStates()
        {
            ddlState.Enabled = true;
            try
            {
                lobjCmd = new SqlCommand("Get_MPF_States", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@planType", ddlPlanType.SelectedValue.ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlState.DataSource = lobjDS;
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "State";
                ddlState.DataBind();
                ddlState.SelectedIndex = 0;

                ddlState_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void BindCounties()
        {
            ddlCounty.Enabled = true;
            lobjCmd = new SqlCommand("Get_MPF_Counties", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@planType", ddlPlanType.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlCounty.DataSource = lobjDS;
            ddlCounty.DataTextField = "County";
            ddlCounty.DataValueField = "County";
            ddlCounty.DataBind();
            ddlCounty.SelectedIndex = 0;

            ddlCounty_SelectedIndexChanged(null, null);
        }

        public void BindPlanTypes()
        {
            lobjCmd = new SqlCommand("Get_MPF_PlanType", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlPlanType.DataSource = lobjDS;
            ddlPlanType.DataTextField = "PlanType";
            ddlPlanType.DataValueField = "PlanType";
            ddlPlanType.DataBind();
            ddlPlanType.SelectedIndex = 0;

            ddlPlanType_SelectedIndexChanged(null, null);
        }

        public void BindSnpType()
        {
            lobjCmd = new SqlCommand("Get_MPF_SNPType", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlSnpType.DataSource = lobjDS;
            ddlSnpType.DataTextField = "SNPType";
            ddlSnpType.DataValueField = "SNPType";
            ddlSnpType.DataBind();
            ddlSnpType.SelectedIndex = 0;
            ddlSnpType_SelectedIndexChanged(null, null);
        }

        public void BindMPFSort()
        {
            lobjCmd = new SqlCommand("Get_MPF_Sort", lobjCon);
            lobjCmd.Parameters.AddWithValue("@Value", ddlPlanType.SelectedValue);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            ddlMPFSort.DataSource = lobjDS;
            ddlMPFSort.DataTextField = "Name";
            ddlMPFSort.DataValueField = "Name";
            ddlMPFSort.DataBind();
            ddlMPFSort.SelectedIndex = 0;

            ddlMPFSort_SelectedIndexChanged(null, null);
        }

        public void BindPlansGrid()
        {
            ClearValues();
            string selectedSnptype = Get_Selected_SnpTypes();

            lobjCmd = new SqlCommand("Get_MPF_Plan_List", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@planType", ddlPlanType.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@state", ddlState.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@county", ddlCounty.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@SortedBy", ddlMPFSort.SelectedValue.ToString());
            lobjCmd.Parameters.AddWithValue("@SnpType", selectedSnptype);
            lobjCmd.Parameters.AddWithValue("@userName", Session["UserName"].ToString());
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            grdData.DataSource = lobjDS;
            grdData.DataBind();

            Session["PlansList"] = lobjDS;
        }

        public void InsertorUpdatePlans()
        {
            try
            {
                if (Session["SelectedBidId"] == null)
                {
                    Session["SelectedBidId"] = txtBidId.Text.ToString() + ",";
                }
                else
                {
                    Session["SelectedBidId"] = Session["SelectedBidId"].ToString() + txtBidId.Text.ToString() + ",";
                }

                lobjCon.Open();
                lobjCmd = new SqlCommand("Insert_MPF_UserInputs", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@State", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@County", ddlCounty.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@planType", ddlPlanType.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@BidId", txtBidId.Text.ToString());
                lobjCmd.Parameters.AddWithValue("@MarketingName", txtMarketName.Text.ToString());
                lobjCmd.Parameters.AddWithValue("@PlanName", txtPlanName.Text.ToString());

                if (txtPremium.Text != "" && txtPremium.Text != "_")
                {
                    lobjCmd.Parameters.AddWithValue("@Premium", Convert.ToDouble(txtPremium.Text));
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@Premium", DBNull.Value);
                }

                if (txtStarRating.Text != "_" && txtStarRating.Text != "")
                {
                    lobjCmd.Parameters.AddWithValue("@StarRating", Convert.ToDouble(txtStarRating.Text));
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@StarRating", DBNull.Value);
                }

                if (txtHealthDeductable.Text != "_" && txtHealthDeductable.Text != "")
                {
                    lobjCmd.Parameters.AddWithValue("@HealthDeductable", Convert.ToDouble(txtHealthDeductable.Text));
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@HealthDeductable", DBNull.Value);
                }

                if (txtDrugDeductible.Text != "" && txtDrugDeductible.Text != "_")
                {
                    lobjCmd.Parameters.AddWithValue("@DrugDeductable", Convert.ToDouble(txtDrugDeductible.Text));
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@DrugDeductable", DBNull.Value);
                }

                if (txtMoopInNetWork.Text != "_" && txtMoopInNetWork.Text != "")
                {
                    lobjCmd.Parameters.AddWithValue("@MOOPInnetwork", Convert.ToDouble(txtMoopInNetWork.Text));
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@MOOPInnetwork", DBNull.Value);
                }

                if (txtMoopInOut.Text != "" && txtMoopInOut.Text != "_")
                {
                    lobjCmd.Parameters.AddWithValue("@MOOPInout", Convert.ToDouble(txtMoopInOut.Text));
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@MOOPInout", DBNull.Value);
                }

                if (txtMoopOutNetWork.Text != "" && txtMoopOutNetWork.Text != "_")
                {
                    lobjCmd.Parameters.AddWithValue("@MOOPOON", Convert.ToDouble(txtMoopOutNetWork.Text));
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@MOOPOON", DBNull.Value);
                }

                lobjCmd.Parameters.AddWithValue("@SnpType", lblSnpType.Text);
                lobjCmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
                lobjCmd.ExecuteNonQuery();
                lobjCon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeletePlans()
        {
            try
            {
                lobjCon.Open();
                lobjCmd = new SqlCommand("delete_MPF_UserInputs", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@State", ddlState.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@County", ddlCounty.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@planType", ddlPlanType.SelectedValue.ToString());
                lobjCmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
                lobjCmd.ExecuteNonQuery();
                lobjCon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ClearValues()
        {
            txtBidId.ReadOnly = false;
            txtPlanName.ReadOnly = false;
            txtMarketName.ReadOnly = false;
            lblPlanHeader.Text = "Add Plan";

            txtBidId.Text = string.Empty;
            txtMarketName.Text = string.Empty;
            txtPlanName.Text = string.Empty;
            txtPremium.Text = string.Empty;
            txtStarRating.Text = string.Empty;
            txtHealthDeductable.Text = string.Empty;
            txtDrugDeductible.Text = string.Empty;
            txtMoopInNetWork.Text = string.Empty;
            txtMoopInOut.Text = string.Empty;
            txtMoopOutNetWork.Text = string.Empty;
            lblSnpType.Text = "0";

        }

        private string Get_Selected_SnpTypes()
        {
            string selectedSnpType = string.Empty;
            var snpTypeCollection = ddlSnpType.CheckedItems;
            foreach (var item in snpTypeCollection)
            {
                selectedSnpType += item.Text + ",";
            }

            if (snpTypeCollection.Count == 0)
            {
                selectedSnpType = "";
            }
            else
            {
                selectedSnpType = selectedSnpType.Substring(0, selectedSnpType.Length - 1);
            }

            return selectedSnpType;
        }

        #endregion

        #region dropdown Events

        protected void ddlPlanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPlanType.SelectedValue == "PDP")
                {
                    ddlSnpType.Enabled = false;
                }
                else
                {
                    ddlSnpType.Enabled = true;
                }
                BindStates();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindCounties();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindSnpType();
                BindMPFSort();
                BindPlansGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void ddlMPFSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPlansGrid();
            }
            catch (Exception)
            {

            }
        }

        protected void ddlSnpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindPlansGrid();
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region Grid Events

        protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["isUpdate"] = "Yes";
                lblPlanHeader.Text = "Update Plan";
                txtBidId.ReadOnly = true;
                txtMarketName.ReadOnly = true;
                txtPlanName.ReadOnly = true;

                Label lbBidId = (grdData.SelectedRow.FindControl("lbBidId") as Label);
                Label lbMarketingName = (grdData.SelectedRow.FindControl("lbMarketingName") as Label);
                Label lbPremium = (grdData.SelectedRow.FindControl("lbPremium") as Label);
                Label lbStartRating = (grdData.SelectedRow.FindControl("lbStartRating") as Label);
                Label lbHealthDeductable = (grdData.SelectedRow.FindControl("lbHealthDeductable") as Label);
                Label lbDrugDeductable = (grdData.SelectedRow.FindControl("lbDrugDeductable") as Label);
                Label lbMoopIn = (grdData.SelectedRow.FindControl("lbMoopIn") as Label);
                Label lbMoopInOut = (grdData.SelectedRow.FindControl("lbMoopInOut") as Label);
                Label lbMoopOON = (grdData.SelectedRow.FindControl("lbMoopOON") as Label);
                Label lbPlanName = (grdData.SelectedRow.FindControl("lbPlanName") as Label);
                Label lbSnpType = (grdData.SelectedRow.FindControl("lbSnpType") as Label);

                txtBidId.Text = lbBidId.Text;
                txtMarketName.Text = lbMarketingName.Text;
                txtPremium.Text = lbPremium.Text;
                txtStarRating.Text = lbStartRating.Text;
                txtHealthDeductable.Text = lbHealthDeductable.Text;
                txtDrugDeductible.Text = lbDrugDeductable.Text;
                txtMoopInNetWork.Text = lbMoopIn.Text;
                txtMoopInOut.Text = lbMoopInOut.Text;
                txtMoopOutNetWork.Text = lbMoopOON.Text;
                txtPlanName.Text = lbPlanName.Text;
                lblSnpType.Text = lbSnpType.Text;
            }
            catch (Exception)
            {

            }
        }

        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (Session["SelectedBidId"] != null)
                {
                    string strBidId = Session["SelectedBidId"].ToString();
                    List<string> bidIds = strBidId.Split(',').ToList();

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        string LblBidID = ((Label)e.Row.FindControl("lbBidId")).Text;

                        if (bidIds.Contains(LblBidID))
                        {
                            e.Row.CssClass = "active";
                        }
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.RowIndex == 0)
                    {
                        e.Row.Style.Add("height", "50px");
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string healthDeductible = ((Label)e.Row.FindControl("lbHealthDeductable")).Text;
                    string drugDeductible = ((Label)e.Row.FindControl("lbDrugDeductable")).Text;
                    string MoopINOut = ((Label)e.Row.FindControl("lbMoopInOut")).Text;
                    string MoopIn = ((Label)e.Row.FindControl("lbMoopIn")).Text;
                    string MoopOut = ((Label)e.Row.FindControl("lbMoopOON")).Text;

                    ((Label)e.Row.FindControl("lbHealthDeductable")).Text = String.IsNullOrEmpty(healthDeductible) ? "_" : healthDeductible;
                    ((Label)e.Row.FindControl("lbDrugDeductable")).Text = String.IsNullOrEmpty(drugDeductible) ? "_" : drugDeductible;
                    ((Label)e.Row.FindControl("lbMoopInOut")).Text = String.IsNullOrEmpty(MoopINOut) ? "_" : MoopINOut;
                    ((Label)e.Row.FindControl("lbMoopIn")).Text = String.IsNullOrEmpty(MoopIn) ? "_" : MoopIn;
                    ((Label)e.Row.FindControl("lbMoopOON")).Text = String.IsNullOrEmpty(MoopOut) ? "_" : MoopOut;
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region Click Events

        protected void lbRevert_Click(object sender, EventArgs e)
        {
            try
            {
                Session["SelectedBidId"] = null;
                DeletePlans();
                BindPlansGrid();
                string message = "Reset Successful";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "alert('" + message + "');", true);
            }
            catch (Exception)
            {

            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdData.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition",
                     "attachment;filename=MPFSimulatorPlanList.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    StringBuilder sb = new StringBuilder();
                    for (int k = 0; k < grdData.Columns.Count; k++)
                    {
                        sb.Append(grdData.Columns[k].HeaderText + ',');
                    }
                    sb.Append("\r\n");
                    for (int i = 0; i < grdData.Rows.Count; i++)
                    {
                        Label LblBidId = (Label)grdData.Rows[i].FindControl("lbBidId") as Label;
                        Label LblOrganizationName = (Label)grdData.Rows[i].FindControl("lbMarketingName") as Label;
                        Label LblSnpType = (Label)grdData.Rows[i].FindControl("lbSnpType") as Label;
                        Label LblPlanName = (Label)grdData.Rows[i].FindControl("lbPlanName") as Label;
                        Label LblPremium = (Label)grdData.Rows[i].FindControl("lbPremium") as Label;
                        Label LblStartRating = (Label)grdData.Rows[i].FindControl("lbStartRating") as Label;
                        Label LblHealthDeductable = (Label)grdData.Rows[i].FindControl("lbHealthDeductable") as Label;
                        Label LblDrugDeductable = (Label)grdData.Rows[i].FindControl("lbDrugDeductable") as Label;
                        Label LblMoopIn = (Label)grdData.Rows[i].FindControl("lbMoopIn") as Label;
                        Label LblMoopInOut = (Label)grdData.Rows[i].FindControl("lbMoopInOut") as Label;
                        Label LblMoopOON = (Label)grdData.Rows[i].FindControl("lbMoopOON") as Label;
                        Label LblRank = (Label)grdData.Rows[i].FindControl("lbOriginalRank") as Label;
                        Label LblSimulatedRank = (Label)grdData.Rows[i].FindControl("lbSimulatedRank") as Label;

                        if (LblBidId.Text.Contains(","))
                        {
                            LblBidId.Text.ToString().Replace(",", "");
                        }
                        if (LblOrganizationName.Text.Contains(","))
                        {
                            LblOrganizationName.Text.ToString().Replace(",", "");
                        }
                        if (LblSnpType.Text.Contains(","))
                        {
                            LblSnpType.Text.ToString().Replace(",", "");
                        }
                        if (LblPlanName.Text.Contains(","))
                        {
                            LblPlanName.Text.ToString().Replace(",", "");
                        }
                        if (LblPremium.Text.Contains(","))
                        {
                            LblPremium.Text.ToString().Replace(",", "");
                        }
                        if (LblStartRating.Text.Contains(","))
                        {
                            LblStartRating.Text.ToString().Replace(",", "");
                        }
                        if (LblHealthDeductable.Text.Contains(","))
                        {
                            LblHealthDeductable.Text.ToString().Replace(",", "");
                        }
                        if (LblDrugDeductable.Text.Contains(","))
                        {
                            LblDrugDeductable.Text.ToString().Replace(",", "");
                        }
                        if (LblMoopInOut.Text.Contains(","))
                        {
                            LblMoopInOut.Text.ToString().Replace(",", "");
                        }
                        if (LblMoopIn.Text.Contains(","))
                        {
                            LblMoopIn.Text.ToString().Replace(",", "");
                        }
                        if (LblMoopOON.Text.Contains(","))
                        {
                            LblMoopOON.Text.ToString().Replace(",", "");
                        }

                        if (LblRank.Text.Contains(","))
                        {
                            LblRank.Text.ToString().Replace(",", "");
                        }
                        if (LblSimulatedRank.Text.Contains(","))
                        {
                            LblSimulatedRank.Text.ToString().Replace(",", "");
                        }

                        sb.Append(LblBidId.Text + "," + LblOrganizationName.Text + "," + LblSnpType.Text + "," + LblPlanName.Text + "," + LblPremium.Text + "," + LblStartRating.Text + "," + LblHealthDeductable.Text + "," + LblDrugDeductable.Text + "," + LblMoopInOut.Text + "," + LblMoopIn.Text + "," + LblMoopOON.Text + "," + LblRank.Text + "," + LblSimulatedRank.Text + ",");
                        sb.Append("\r\n");
                    }
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                    grdData.Columns[1].Visible = true;
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnSimulate_Click(object sender, EventArgs e)
        {
            if (txtBidId.Text.Contains("-")) { return; }
            if (Convert.ToDouble(txtStarRating.Text) > 5 || Convert.ToDouble(txtStarRating.Text) < 0) { return; }

            if (Session["isUpdate"].ToString() == "Yes")
            {
                InsertorUpdatePlans();
                ClearValues();
                BindPlansGrid();
                Session["isUpdate"] = "No";
                string message = "Simulation completed";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "alert('" + message + "');", true);
            }
            else
            {
                DataSet dsResult = Session["PlansList"] as DataSet;
                DataTable dt = dsResult.Tables[0];
                string find = "BidId = '" + txtBidId.Text + "'";
                DataRow[] foundRows = dt.Select(find);

                if (foundRows.Length > 0)
                {
                    lblDuplicateBidId.Visible = true;
                }
                else
                {
                    lblDuplicateBidId.Visible = false;
                    InsertorUpdatePlans();
                    ClearValues();
                    BindPlansGrid();
                    string message = "Simulation completed";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "alert('" + message + "');", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        #endregion

        #region Change Events

        protected void txtBidId_TextChanged(object sender, EventArgs e)
        {
            lblDuplicateBidId.Visible = false;
        }

        protected void BidID_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (e.Value.Contains("-"))
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }

        protected void StarRating_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (Convert.ToDecimal(e.Value) > 5 || Convert.ToDecimal(e.Value) < 0)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
        #endregion
    }
}