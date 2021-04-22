using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace HealthWorks.Pages
{
    public partial class QuickAccess : System.Web.UI.Page
    {
        public SqlConnection lobjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
        public SqlCommand lobjCmd;
        public DataSet lobjDS;
        public SqlDataAdapter lobjDA;
        PageTracking PT = new PageTracking();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                if (!IsPostBack)
                {
                    Get_All_PlanFinder_Saved_Values();
                    Bind_BidId_Plans();
                    Bind_HospitalAcute();
                    Bind_Disabled();
                    Bind_Benefitperiod();
                    Bind_BenefitType();
                    Bind_BenefitCoverageType();
                    Bind_Data();
                    Bind_MRX_CST();
                    Bind_MRX_Ded_Tier();
                    Bind_MRX_PartD();
                    Bind_MRX();
                }
                lblScenarioName.Text = Session["sName"].ToString() + "-" + ddlBidId.SelectedValue;
                Active_DeactiveLinks();
                CheckSimulatedOutput();
                this.Validate();
            }
            catch (Exception)
            {

            }
        }

        #region Events

        protected void rd_HospitalAcute_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_HospitalAcute.SelectedValue == "Coinsurance")
                {
                    txtpbp_b1a_copay_mcs_amt_int1_t1.Enabled = false;
                    txtpbp_b1a_copay_mcs_amt_int2_t1.Enabled = false;
                    txtpbp_b1a_copay_mcs_amt_int3_t1.Enabled = false;

                    txtpbp_b1a_coins_mcs_pct_int1_t1.Enabled = true;
                    txtpbp_b1a_coins_mcs_pct_int2_t1.Enabled = true;
                    txtpbp_b1a_coins_mcs_pct_int3_t1.Enabled = true;

                    txtpbp_b1a_coins_mcs_pct_t1.Enabled = true;
                    txtpbp_b1a_copay_mcs_amt_t1.Enabled = false;
                }
                if (rd_HospitalAcute.SelectedValue == "Copayment")
                {
                    txtpbp_b1a_coins_mcs_pct_int1_t1.Enabled = false;
                    txtpbp_b1a_coins_mcs_pct_int2_t1.Enabled = false;
                    txtpbp_b1a_coins_mcs_pct_int3_t1.Enabled = false;

                    txtpbp_b1a_copay_mcs_amt_int1_t1.Enabled = true;
                    txtpbp_b1a_copay_mcs_amt_int2_t1.Enabled = true;
                    txtpbp_b1a_copay_mcs_amt_int3_t1.Enabled = true;

                    txtpbp_b1a_coins_mcs_pct_t1.Enabled = false;
                    txtpbp_b1a_copay_mcs_amt_t1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void chk_Surgery_coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Surgery_coin.Checked == true)
            {
                txtpbp_b9a_coins_ohs_pct_min.Enabled = true;
            }
            else
            {
                txtpbp_b9a_coins_ohs_pct_min.Enabled = false;
            }

        }

        protected void chk_Surgery_copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Surgery_copay.Checked == true)
            {
                txtpbp_b9a_copay_ohs_amt_min.Enabled = true;
            }
            else
            {
                txtpbp_b9a_copay_ohs_amt_min.Enabled = false;
            }
        }

        protected void chk_pbp_b9b_coins_yn_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_pbp_b9b_coins_yn.Checked == true)
            {
                txtpbp_b9b_coins_pct_mc.Enabled = true;

            }
            else
            {
                txtpbp_b9b_coins_pct_mc.Enabled = false;

            }
        }

        protected void chk_Ambulatory_copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Ambulatory_copay.Checked == true)
            {
                txtpbp_b9b_copay_mc_amt.Enabled = true;

            }
            else
            {
                txtpbp_b9b_copay_mc_amt.Enabled = false;

            }
        }

        protected void chck_Oral_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Oral_Coin.Checked == true)
            {
                txtpbp_b16a_coins_pct_oe.Enabled = true;

            }
            else
            {
                txtpbp_b16a_coins_pct_oe.Enabled = false;

            }
        }

        protected void chck_Oral_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Oral_Copay.Checked == true)
            {
                txtpbp_b16a_copay_amt_oemin.Enabled = true;

            }
            else
            {
                txtpbp_b16a_copay_amt_oemin.Enabled = false;

            }
        }

        protected void chck_Denatl_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Denatl_Coin.Checked == true)
            {
                txtpbp_b16a_coins_pct_dx.Enabled = true;

            }
            else
            {
                txtpbp_b16a_coins_pct_dx.Enabled = false;

            }
        }

        protected void chck_Denatl_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Denatl_Copay.Checked == true)
            {
                txtpbp_b16a_copay_amt_dxmin.Enabled = true;

            }
            else
            {
                txtpbp_b16a_copay_amt_dxmin.Enabled = false;

            }
        }

        protected void chck_Prophylaxis_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Prophylaxis_Coin.Checked == true)
            {
                txtpbp_b16a_coins_pct_pc.Enabled = true;

            }
            else
            {
                txtpbp_b16a_coins_pct_pc.Enabled = false;

            }
        }

        protected void chck_Prophylaxis_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Prophylaxis_Copay.Checked == true)
            {
                txtpbp_b16a_copay_amt_pcmin.Enabled = true;

            }
            else
            {
                txtpbp_b16a_copay_amt_pcmin.Enabled = false;

            }
        }

        protected void chk_Medicare_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Medicare_Coin.Checked == true)
            {
                txtPBP_B16B_COINS_PCT_MC_MIN.Enabled = true;

            }
            else
            {
                txtPBP_B16B_COINS_PCT_MC_MIN.Enabled = false;

            }
        }

        protected void chk_Medicare_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Medicare_Copay.Checked == true)
            {
                txtPBP_B16B_COPAY_AMT_MC_MIN.Enabled = true;

            }
            else
            {
                txtPBP_B16B_COPAY_AMT_MC_MIN.Enabled = false;

            }
        }

        protected void chk_Restorative_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Restorative_Coin.Checked == true)
            {
                txtpbp_b16b_coins_pct_rs_min.Enabled = true;

            }
            else
            {
                txtpbp_b16b_coins_pct_rs_min.Enabled = false;

            }
        }

        protected void chk_Restorative_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Restorative_Copay.Checked == true)
            {
                txtpbp_b16b_copay_amt_rs_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_copay_amt_rs_min.Enabled = false;
            }
        }

        protected void chk_Endodontics_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Endodontics_Coin.Checked == true)
            {
                txtpbp_b16b_coins_pct_end_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_coins_pct_end_min.Enabled = false;
            }
        }

        protected void chk_Endodontics_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Endodontics_Copay.Checked == true)
            {
                txtpbp_b16b_copay_amt_end_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_copay_amt_end_min.Enabled = false;
            }
        }

        protected void chk_Periodontics_coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Periodontics_coin.Checked == true)
            {
                txtpbp_b16b_coins_pct_peri_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_coins_pct_peri_min.Enabled = false;
            }
        }

        protected void chk_Periodontics_copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Periodontics_copay.Checked == true)
            {
                txtpbp_b16b_copay_amt_peri_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_copay_amt_peri_min.Enabled = false;

            }
        }

        protected void chk_Extractions_coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Extractions_coin.Checked == true)
            {
                txtpbp_b16b_coins_pct_ext_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_coins_pct_ext_min.Enabled = false;
            }
        }

        protected void chk_Extractions_copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Extractions_copay.Checked == true)
            {
                txtpbp_b16b_copay_amt_ext_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_copay_amt_ext_min.Enabled = false;
            }
        }

        protected void chk_Prosthodontics_coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Prosthodontics_coin.Checked == true)
            {
                txtpbp_b16b_coins_pct_poo_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_coins_pct_poo_min.Enabled = false;
            }
        }

        protected void chk_Prosthodontics_copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Prosthodontics_copay.Checked == true)
            {
                txtpbp_b16b_copay_amt_poo_min.Enabled = true;
            }
            else
            {
                txtpbp_b16b_copay_amt_poo_min.Enabled = false;
            }
        }

        protected void chck_XRay_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_XRay_Coin.Checked == true)
            {
                txtpbp_b8b_coins_pct_cmc.Enabled = true;
                txtpbp_b8b_coins_pct_cmc_max.Enabled = true;
            }
            else
            {
                txtpbp_b8b_coins_pct_cmc.Enabled = false;
                txtpbp_b8b_coins_pct_cmc_max.Enabled = false;
            }
        }

        protected void chck_XRay_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_XRay_Copay.Checked == true)
            {
                txtpbp_b8b_copay_mc_amt.Enabled = true;
                txtpbp_b8b_copay_mc_amt_max.Enabled = true;
            }
            else
            {
                txtpbp_b8b_copay_mc_amt.Enabled = false;
                txtpbp_b8b_copay_mc_amt_max.Enabled = false;
            }
        }

        protected void chck_Therapeutic_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Therapeutic_Coin.Checked == true)
            {
                txtpbp_b8b_coins_pct_tmc.Enabled = true;
                txtpbp_b8b_coins_pct_tmc_max.Enabled = true;
            }
            else
            {
                txtpbp_b8b_coins_pct_tmc.Enabled = false;
                txtpbp_b8b_coins_pct_tmc_max.Enabled = false;
            }
        }

        protected void chck_Therapeutic_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Therapeutic_Copay.Checked == true)
            {
                txtpbp_b8b_copay_amt_tmc.Enabled = true;
                txtpbp_b8b_copay_amt_tmc_max.Enabled = true;
            }
            else
            {
                txtpbp_b8b_copay_amt_tmc.Enabled = false;
                txtpbp_b8b_copay_amt_tmc_max.Enabled = false;
            }
        }

        protected void chck_Diagnostic_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Diagnostic_Coin.Checked == true)
            {
                txtpbp_b8b_coins_pct_drs.Enabled = true;
                txtpbp_b8b_coins_pct_drs_max.Enabled = true;
            }
            else
            {
                txtpbp_b8b_coins_pct_drs.Enabled = false;
                txtpbp_b8b_coins_pct_drs_max.Enabled = false;
            }
        }

        protected void chck_Diagnostic_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chck_Diagnostic_Copay.Checked == true)
            {
                txtpbp_b8b_copay_amt_drs.Enabled = true;
                txtpbp_b8b_copay_amt_drs_max.Enabled = true;
            }
            else
            {
                txtpbp_b8b_copay_amt_drs.Enabled = false;
                txtpbp_b8b_copay_amt_drs_max.Enabled = false;
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

        private void CheckSimulatedOutput()
        {
            SqlCommand lobjCmd = new SqlCommand("Get_SimulatedOutput", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Convert.ToInt32(Session["sId"].ToString()));
            lobjCon.Open();
            SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
            DataSet ds = new DataSet();
            sd.Fill(ds);
            lobjCon.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                id_Simulate.Style.Add("pointer-events", "auto");
            }
            else
            {
                id_Simulate.Style.Add("pointer-events", "none");
            }
        }

        private void Bind_BidId_Plans()
        {
            SqlCommand lobjCmd = new SqlCommand("Get_BidId_Plans", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Convert.ToInt32(Session["sId"].ToString()));
            lobjCon.Open();
            SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
            DataSet ds = new DataSet();
            sd.Fill(ds);
            lobjCon.Close();
            string GetAllBid = string.Empty;
            Session["All_BidIDs"] = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    Session["All_BidIDs"] += ds.Tables[0].Rows[i]["Bid_id"].ToString() + ",";
                }

                Session["All_BidIDs"] = Session["All_BidIDs"].ToString().Substring(0, Session["All_BidIDs"].ToString().Length - 1);
            }

            ddlBidId.DataSource = ds;
            ddlBidId.DataTextField = "PlanName";
            ddlBidId.DataValueField = "Bid_id";
            ddlBidId.DataBind();
            if (Session["ddl_BidId"] != null)
            {
                ddlBidId.SelectedValue = Session["ddl_BidId"].ToString();
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

                if (ds.Tables[0].Rows.Count > 0)
                {
                    id_Simulate.Style.Add("pointer-events", "auto");
                }
                else
                {
                    id_Simulate.Style.Add("pointer-events", "none");
                }

            }
        }

        private void Bind_MRX_CST()
        {
            try
            {
                SqlCommand lobjCmd = new SqlCommand("Get_MRX_CST", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
                DataSet ds = new DataSet();
                sd.Fill(ds);
                lobjCon.Close();

                ddl1mrx_tier_cstshr_struct_type_tier1.DataSource = ds;
                ddl1mrx_tier_cstshr_struct_type_tier1.DataTextField = "CST";
                ddl1mrx_tier_cstshr_struct_type_tier1.DataValueField = "Value";
                ddl1mrx_tier_cstshr_struct_type_tier1.DataBind();
                ddl1mrx_tier_cstshr_struct_type_tier1.Items.Insert(0, "Select");

                ddl1mrx_tier_cstshr_struct_type_tier2.DataSource = ds;
                ddl1mrx_tier_cstshr_struct_type_tier2.DataTextField = "CST";
                ddl1mrx_tier_cstshr_struct_type_tier2.DataValueField = "Value";
                ddl1mrx_tier_cstshr_struct_type_tier2.DataBind();
                ddl1mrx_tier_cstshr_struct_type_tier2.Items.Insert(0, "Select");

                ddl1mrx_tier_cstshr_struct_type_tier3.DataSource = ds;
                ddl1mrx_tier_cstshr_struct_type_tier3.DataTextField = "CST";
                ddl1mrx_tier_cstshr_struct_type_tier3.DataValueField = "Value";
                ddl1mrx_tier_cstshr_struct_type_tier3.DataBind();
                ddl1mrx_tier_cstshr_struct_type_tier3.Items.Insert(0, "Select");

                ddl1mrx_tier_cstshr_struct_type_tier4.DataSource = ds;
                ddl1mrx_tier_cstshr_struct_type_tier4.DataTextField = "CST";
                ddl1mrx_tier_cstshr_struct_type_tier4.DataValueField = "Value";
                ddl1mrx_tier_cstshr_struct_type_tier4.DataBind();
                ddl1mrx_tier_cstshr_struct_type_tier4.Items.Insert(0, "Select");

                ddl1mrx_tier_cstshr_struct_type_tier5.DataSource = ds;
                ddl1mrx_tier_cstshr_struct_type_tier5.DataTextField = "CST";
                ddl1mrx_tier_cstshr_struct_type_tier5.DataValueField = "Value";
                ddl1mrx_tier_cstshr_struct_type_tier5.DataBind();
                ddl1mrx_tier_cstshr_struct_type_tier5.Items.Insert(0, "Select");

                ddl1mrx_tier_cstshr_struct_type_tier6.DataSource = ds;
                ddl1mrx_tier_cstshr_struct_type_tier6.DataTextField = "CST";
                ddl1mrx_tier_cstshr_struct_type_tier6.DataValueField = "Value";
                ddl1mrx_tier_cstshr_struct_type_tier6.DataBind();
                ddl1mrx_tier_cstshr_struct_type_tier6.Items.Insert(0, "Select");


                lobjCmd = new SqlCommand("Get_MRX_TierIncludes", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                sd = new SqlDataAdapter(lobjCmd);
                ds = new DataSet();
                sd.Fill(ds);
                lobjCon.Close();

                ddlMRX_TIER_INCLUDES_tier1.DataSource = ds;
                ddlMRX_TIER_INCLUDES_tier1.DataTextField = "MRxTierIncludes";
                ddlMRX_TIER_INCLUDES_tier1.DataValueField = "ID";
                ddlMRX_TIER_INCLUDES_tier1.DataBind();
                ddlMRX_TIER_INCLUDES_tier1.Items.Insert(0, "Select");

                ddlMRX_TIER_INCLUDES_tier2.DataSource = ds;
                ddlMRX_TIER_INCLUDES_tier2.DataTextField = "MRxTierIncludes";
                ddlMRX_TIER_INCLUDES_tier2.DataValueField = "ID";
                ddlMRX_TIER_INCLUDES_tier2.DataBind();
                ddlMRX_TIER_INCLUDES_tier2.Items.Insert(0, "Select");

                ddlMRX_TIER_INCLUDES_tier3.DataSource = ds;
                ddlMRX_TIER_INCLUDES_tier3.DataTextField = "MRxTierIncludes";
                ddlMRX_TIER_INCLUDES_tier3.DataValueField = "ID";
                ddlMRX_TIER_INCLUDES_tier3.DataBind();
                ddlMRX_TIER_INCLUDES_tier3.Items.Insert(0, "Select");

                ddlMRX_TIER_INCLUDES_tier4.DataSource = ds;
                ddlMRX_TIER_INCLUDES_tier4.DataTextField = "MRxTierIncludes";
                ddlMRX_TIER_INCLUDES_tier4.DataValueField = "ID";
                ddlMRX_TIER_INCLUDES_tier4.DataBind();
                ddlMRX_TIER_INCLUDES_tier4.Items.Insert(0, "Select");

                ddlMRX_TIER_INCLUDES_tier5.DataSource = ds;
                ddlMRX_TIER_INCLUDES_tier5.DataTextField = "MRxTierIncludes";
                ddlMRX_TIER_INCLUDES_tier5.DataValueField = "ID";
                ddlMRX_TIER_INCLUDES_tier5.DataBind();
                ddlMRX_TIER_INCLUDES_tier5.Items.Insert(0, "Select");

                ddlMRX_TIER_INCLUDES_tier6.DataSource = ds;
                ddlMRX_TIER_INCLUDES_tier6.DataTextField = "MRxTierIncludes";
                ddlMRX_TIER_INCLUDES_tier6.DataValueField = "ID";
                ddlMRX_TIER_INCLUDES_tier6.DataBind();
                ddlMRX_TIER_INCLUDES_tier6.Items.Insert(0, "Select");

                lobjCmd = new SqlCommand("Get_MRX_CostShare", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                sd = new SqlDataAdapter(lobjCmd);
                ds = new DataSet();
                sd.Fill(ds);
                lobjCon.Close();

                ddlmrx_tier_gap_cost_share_tier1.DataSource = ds;
                ddlmrx_tier_gap_cost_share_tier1.DataTextField = "MRxTierGapCostShare";
                ddlmrx_tier_gap_cost_share_tier1.DataValueField = "ID";
                ddlmrx_tier_gap_cost_share_tier1.DataBind();
                ddlmrx_tier_gap_cost_share_tier1.Items.Insert(0, "Select");

                ddlmrx_tier_gap_cost_share_tier2.DataSource = ds;
                ddlmrx_tier_gap_cost_share_tier2.DataTextField = "MRxTierGapCostShare";
                ddlmrx_tier_gap_cost_share_tier2.DataValueField = "ID";
                ddlmrx_tier_gap_cost_share_tier2.DataBind();
                ddlmrx_tier_gap_cost_share_tier2.Items.Insert(0, "Select");

                ddlmrx_tier_gap_cost_share_tier2.DataSource = ds;
                ddlmrx_tier_gap_cost_share_tier2.DataTextField = "MRxTierGapCostShare";
                ddlmrx_tier_gap_cost_share_tier2.DataValueField = "ID";
                ddlmrx_tier_gap_cost_share_tier2.DataBind();
                ddlmrx_tier_gap_cost_share_tier2.Items.Insert(0, "Select");

                ddlmrx_tier_gap_cost_share_tier3.DataSource = ds;
                ddlmrx_tier_gap_cost_share_tier3.DataTextField = "MRxTierGapCostShare";
                ddlmrx_tier_gap_cost_share_tier3.DataValueField = "ID";
                ddlmrx_tier_gap_cost_share_tier3.DataBind();
                ddlmrx_tier_gap_cost_share_tier3.Items.Insert(0, "Select");

                ddlmrx_tier_gap_cost_share_tier4.DataSource = ds;
                ddlmrx_tier_gap_cost_share_tier4.DataTextField = "MRxTierGapCostShare";
                ddlmrx_tier_gap_cost_share_tier4.DataValueField = "ID";
                ddlmrx_tier_gap_cost_share_tier4.DataBind();
                ddlmrx_tier_gap_cost_share_tier4.Items.Insert(0, "Select");

                ddlmrx_tier_gap_cost_share_tier5.DataSource = ds;
                ddlmrx_tier_gap_cost_share_tier5.DataTextField = "MRxTierGapCostShare";
                ddlmrx_tier_gap_cost_share_tier5.DataValueField = "ID";
                ddlmrx_tier_gap_cost_share_tier5.DataBind();
                ddlmrx_tier_gap_cost_share_tier5.Items.Insert(0, "Select");

                ddlmrx_tier_gap_cost_share_tier6.DataSource = ds;
                ddlmrx_tier_gap_cost_share_tier6.DataTextField = "MRxTierGapCostShare";
                ddlmrx_tier_gap_cost_share_tier6.DataValueField = "ID";
                ddlmrx_tier_gap_cost_share_tier6.DataBind();
                ddlmrx_tier_gap_cost_share_tier6.Items.Insert(0, "Select");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bind_MRX_Ded_Tier()
        {
            try
            {
                SqlCommand lobjCmd = new SqlCommand("Get_MRX_Ded_Tier", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
                DataSet ds = new DataSet();
                sd.Fill(ds);
                lobjCon.Close();

                ddlmrx_alt_no_ded_tier.DataSource = ds;
                ddlmrx_alt_no_ded_tier.DataTextField = "Tier";
                ddlmrx_alt_no_ded_tier.DataValueField = "Value";
                ddlmrx_alt_no_ded_tier.DataBind();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bind_MRX_PartD()
        {
            try
            {
                SqlCommand lobjCmd = new SqlCommand("Get_MRX_PartD", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
                DataSet ds = new DataSet();
                sd.Fill(ds);
                lobjCon.Close();

                ddlmrx_benefit_type.DataSource = ds;
                ddlmrx_benefit_type.DataTextField = "BenefitType";
                ddlmrx_benefit_type.DataValueField = "Value";
                ddlmrx_benefit_type.DataBind();
                ddlmrx_benefit_type.Items.Insert(0, "Select");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bind_Data()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_QuickAccess", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@Persona", Session["Persona"].ToString());
                lobjCmd.Parameters.AddWithValue("@Bid_Id", ddlBidId.SelectedValue);
                lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());

                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                lobjCon.Close();
                Session["SelectedBidId"] = ddlBidId.SelectedValue;

                if (lobjDS.Tables[0].Rows.Count > 0)
                {
                    // Binding Inpatient Value
                    #region Inpatient Hospital Acute Services
                    if (lobjDS.Tables[0].Rows[0]["pbp_b1a_hosp_ben_period"].ToString() == "")
                    {
                        ddlpbp_b1a_hosp_ben_period.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b1a_hosp_ben_period.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b1a_hosp_ben_period"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b1a_cost_discharge_yn"].ToString() == "")
                    {
                        ddlpbp_b1a_cost_discharge_yn.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b1a_cost_discharge_yn.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b1a_cost_discharge_yn"].ToString();
                    }

                    txtpbp_b1a_coins_mcs_pct_t1.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_pct_t1"].ToString();
                    txtpbp_b1a_copay_mcs_amt_t1.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_amt_t1"].ToString();

                    txtpbp_b1a_coins_mcs_pct_int1_t1.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_pct_int1_t1"].ToString();
                    txtpbp_b1a_coins_mcs_pct_int2_t1.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_pct_int2_t1"].ToString();
                    txtpbp_b1a_coins_mcs_pct_int3_t1.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_pct_int3_t1"].ToString();

                    txtpbp_b1a_copay_mcs_amt_int1_t1.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_amt_int1_t1"].ToString();
                    txtpbp_b1a_copay_mcs_amt_int2_t1.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_amt_int2_t1"].ToString();
                    txtpbp_b1a_copay_mcs_amt_int3_t1.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_amt_int3_t1"].ToString();

                    if (lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_yn"].ToString() == "1")
                    {
                        rd_HospitalAcute.SelectedValue = "Coinsurance";

                        txtpbp_b1a_coins_mcs_pct_int1_t1.Enabled = true;
                        txtpbp_b1a_coins_mcs_pct_int2_t1.Enabled = true;
                        txtpbp_b1a_coins_mcs_pct_int3_t1.Enabled = true;

                        txtpbp_b1a_copay_mcs_amt_int1_t1.Enabled = false;
                        txtpbp_b1a_copay_mcs_amt_int2_t1.Enabled = false;
                        txtpbp_b1a_copay_mcs_amt_int3_t1.Enabled = false;

                        txtpbp_b1a_coins_mcs_pct_t1.Enabled = true;
                        txtpbp_b1a_copay_mcs_amt_t1.Enabled = false;


                        txtBegin1Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_bgnd_int1_t1"].ToString();
                        txtBegin2Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_bgnd_int2_t1"].ToString();
                        txtBegin3Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_bgnd_int3_t1"].ToString();

                        txtEnd1Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_endd_int1_t1"].ToString();
                        txtEnd2Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_endd_int2_t1"].ToString();
                        txtEnd3Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_coins_mcs_endd_int3_t1"].ToString();

                        if (lobjDS.Tables[0].Rows[0]["pbp_b1a_mc_coins_cstshr_yn_t1"].ToString() == "")
                        {
                            ddlpbp_b1a_mc_copay_cstshr_yn_t1.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlpbp_b1a_mc_copay_cstshr_yn_t1.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b1a_mc_coins_cstshr_yn_t1"].ToString();
                            if (lobjDS.Tables[0].Rows[0]["pbp_b1a_mc_coins_cstshr_yn_t1"].ToString() == "1")
                            {
                                ddlpbp_b1a_mc_copay_cstshr_yn_t1_SelectedIndexChanged(this, null);
                            }
                        }
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_yn"].ToString() == "1")
                    {

                        rd_HospitalAcute.SelectedValue = "Copayment";

                        txtpbp_b1a_coins_mcs_pct_int1_t1.Enabled = false;
                        txtpbp_b1a_coins_mcs_pct_int2_t1.Enabled = false;
                        txtpbp_b1a_coins_mcs_pct_int3_t1.Enabled = false;

                        txtpbp_b1a_copay_mcs_amt_int1_t1.Enabled = true;
                        txtpbp_b1a_copay_mcs_amt_int2_t1.Enabled = true;
                        txtpbp_b1a_copay_mcs_amt_int3_t1.Enabled = true;

                        txtpbp_b1a_coins_mcs_pct_t1.Enabled = false;
                        txtpbp_b1a_copay_mcs_amt_t1.Enabled = true;


                        txtBegin1Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_bgnd_int1_t1"].ToString();
                        txtBegin2Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_bgnd_int2_t1"].ToString();
                        txtBegin3Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_bgnd_int3_t1"].ToString();

                        txtEnd1Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_endd_int1_t1"].ToString();
                        txtEnd2Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_endd_int2_t1"].ToString();
                        txtEnd3Acute.Text = lobjDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_endd_int3_t1"].ToString();

                        if (lobjDS.Tables[0].Rows[0]["pbp_b1a_mc_copay_cstshr_yn_t1"].ToString() == "")
                        {
                            ddlpbp_b1a_mc_copay_cstshr_yn_t1.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlpbp_b1a_mc_copay_cstshr_yn_t1.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b1a_mc_copay_cstshr_yn_t1"].ToString();
                            if (lobjDS.Tables[0].Rows[0]["pbp_b1a_mc_copay_cstshr_yn_t1"].ToString() == "1")
                            {

                                ddlpbp_b1a_mc_copay_cstshr_yn_t1_SelectedIndexChanged(this, null);
                            }
                        }
                    }
                    #endregion

                    // Binding Outpatient Hospital Services
                    #region Outpatient Hospital Services
                    string pbp_b9a_coins_ehc = lobjDS.Tables[0].Rows[0]["pbp_b9a_coins_ehc"].ToString();

                    if (pbp_b9a_coins_ehc != String.Empty && pbp_b9a_coins_ehc.Length > 0)
                    {
                        char[] OutpatientCoin = pbp_b9a_coins_ehc.ToCharArray();

                        if (pbp_b9a_coins_ehc.Length >= 1)
                        {
                            if (OutpatientCoin[0] == '1')
                            {
                                chk_Observ_coin.Checked = true;
                            }
                            else
                            {
                                chk_Observ_coin.Checked = false;
                            }
                        }
                        if (pbp_b9a_coins_ehc.Length >= 2)
                        {
                            if (OutpatientCoin[1] == '1')
                            {
                                chk_Surgery_coin.Checked = true;
                                txtpbp_b9a_coins_ohs_pct_min.Enabled = true;
                            }
                            else
                            {
                                chk_Surgery_coin.Checked = false;
                                txtpbp_b9a_coins_ohs_pct_min.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        chk_Observ_coin.Checked = false;
                        chk_Surgery_coin.Checked = false;
                        txtpbp_b9a_coins_ohs_pct_min.Enabled = false;
                        txtpbp_b9a_coins_ohs_pct_min.Text = string.Empty;
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b9b_coins_yn"].ToString() == "1")
                    {
                        chk_pbp_b9b_coins_yn.Checked = true;
                        txtpbp_b9b_coins_pct_mc.Enabled = true;
                    }
                    else
                    {
                        chk_pbp_b9b_coins_yn.Checked = false;
                        txtpbp_b9b_coins_pct_mc.Enabled = false;
                    }


                    string pbp_b9a_copay_ehc = lobjDS.Tables[0].Rows[0]["pbp_b9a_copay_ehc"].ToString();
                    if (pbp_b9a_copay_ehc != String.Empty && pbp_b9a_copay_ehc.Length > 0)
                    {
                        char[] OutpatientCopay = pbp_b9a_copay_ehc.ToCharArray();

                        if (pbp_b9a_copay_ehc.Length >= 1)
                        {
                            if (OutpatientCopay[0] == '1')
                            {
                                chk_Observ_copay.Checked = true;
                            }
                            else
                            {
                                chk_Observ_copay.Checked = false;
                            }
                        }
                        if (pbp_b9a_copay_ehc.Length >= 2)
                        {
                            if (OutpatientCopay[1] == '1')
                            {
                                chk_Surgery_copay.Checked = true;
                                txtpbp_b9a_copay_ohs_amt_min.Enabled = true;
                            }
                            else
                            {
                                chk_Surgery_copay.Checked = false;
                                txtpbp_b9a_copay_ohs_amt_min.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        chk_Observ_copay.Checked = false;
                        chk_Surgery_copay.Checked = false;
                        txtpbp_b9a_copay_ohs_amt_min.Enabled = false;
                        txtpbp_b9a_copay_ohs_amt_min.Text = string.Empty;
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b9b_copay_yn"].ToString() == "1")
                    {
                        chk_Ambulatory_copay.Checked = true;
                        txtpbp_b9b_copay_mc_amt.Enabled = true;
                    }
                    else
                    {
                        chk_Ambulatory_copay.Checked = false;
                        txtpbp_b9b_copay_mc_amt.Enabled = false;
                    }

                    txtpbp_b9a_coins_ohs_pct_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b9a_coins_ohs_pct_min"].ToString();
                    txtpbp_b9b_coins_pct_mc.Text = lobjDS.Tables[0].Rows[0]["pbp_b9b_coins_pct_mc"].ToString();
                    txtpbp_b9a_copay_ohs_amt_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b9a_copay_ohs_amt_min"].ToString();
                    txtpbp_b9b_copay_mc_amt.Text = lobjDS.Tables[0].Rows[0]["pbp_b9b_copay_mc_amt"].ToString();
                    #endregion

                    //Preventive Dental Care
                    #region Preventive Dental Care
                    txtpbp_b16a_maxplan_amt.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_maxplan_amt"].ToString();

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16a_maxplan_per"].ToString() == "")
                    {
                        ddlpbp_b16a_maxplan_per.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16a_maxplan_per.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16a_maxplan_per"].ToString();
                    }

                    string pbp_b16a_coins_ehc = lobjDS.Tables[0].Rows[0]["pbp_b16a_coins_ehc"].ToString();

                    if (pbp_b16a_coins_ehc != String.Empty && pbp_b16a_coins_ehc.Length > 0)
                    {
                        char[] PreventiveCoin = pbp_b16a_coins_ehc.ToCharArray();
                        if (PreventiveCoin.Length >= 4)
                        {
                            Session["pbp_b16a_coins_ehc"] = PreventiveCoin[3].ToString();
                        }
                        else
                        {
                            Session["pbp_b16a_coins_ehc"] = 0;
                        }
                        if (pbp_b16a_coins_ehc.Length >= 1)
                        {
                            if (PreventiveCoin[0] == '1')
                            {
                                chck_Denatl_Coin.Checked = true;
                                txtpbp_b16a_coins_pct_dx.Enabled = true;
                            }
                            else
                            {
                                chck_Denatl_Coin.Checked = false;
                                txtpbp_b16a_coins_pct_dx.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Denatl_Coin.Checked = false;
                            txtpbp_b16a_coins_pct_dx.Enabled = false;
                            txtpbp_b16a_coins_pct_dx.Text = string.Empty;
                        }
                        if (pbp_b16a_coins_ehc.Length >= 2)
                        {
                            if (PreventiveCoin[1] == '1')
                            {
                                chck_Oral_Coin.Checked = true;
                                txtpbp_b16a_coins_pct_oe.Enabled = true;
                            }
                            else
                            {
                                chck_Oral_Coin.Checked = false;
                                txtpbp_b16a_coins_pct_oe.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Oral_Coin.Checked = false;
                            txtpbp_b16a_coins_pct_oe.Enabled = false;
                            txtpbp_b16a_coins_pct_oe.Text = string.Empty;
                        }

                        if (pbp_b16a_coins_ehc.Length >= 3)
                        {
                            if (PreventiveCoin[2] == '1')
                            {
                                chck_Prophylaxis_Coin.Checked = true;
                                txtpbp_b16a_coins_pct_pc.Enabled = true;
                            }
                            else
                            {
                                chck_Prophylaxis_Coin.Checked = false;
                                txtpbp_b16a_coins_pct_pc.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Prophylaxis_Coin.Checked = false;
                            txtpbp_b16a_coins_pct_pc.Enabled = false;
                            txtpbp_b16a_coins_pct_pc.Text = string.Empty;
                        }
                    }
                    else
                    {
                        chck_Denatl_Coin.Checked = false;
                        txtpbp_b16a_coins_pct_dx.Enabled = false;
                        txtpbp_b16a_coins_pct_dx.Text = string.Empty;

                        chck_Oral_Coin.Checked = false;
                        txtpbp_b16a_coins_pct_oe.Enabled = false;
                        txtpbp_b16a_coins_pct_oe.Text = string.Empty;

                        chck_Prophylaxis_Coin.Checked = false;
                        txtpbp_b16a_coins_pct_pc.Enabled = false;
                        txtpbp_b16a_coins_pct_pc.Text = string.Empty;
                        Session["pbp_b16a_coins_ehc"] = 0;
                    }
                    txtpbp_b16a_coins_pct_oe.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_coins_pct_oe"].ToString();
                    txtpbp_b16a_coins_pct_dx.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_coins_pct_dx"].ToString();
                    txtpbp_b16a_coins_pct_pc.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_coins_pct_pc"].ToString();

                    string pbp_b16a_copay_ehc = lobjDS.Tables[0].Rows[0]["pbp_b16a_copay_ehc"].ToString();
                    if (pbp_b16a_copay_ehc != String.Empty && pbp_b16a_copay_ehc.Length > 0)
                    {
                        char[] PreventiveCopay = pbp_b16a_copay_ehc.ToCharArray();
                        if (PreventiveCopay.Length >= 4)
                        {
                            Session["pbp_b16a_copay_ehc"] = PreventiveCopay[3].ToString();
                        }
                        else
                        {
                            Session["pbp_b16a_copay_ehc"] = 0;
                        }

                        if (pbp_b16a_copay_ehc.Length >= 1)
                        {
                            if (PreventiveCopay[0] == '1')
                            {
                                chck_Denatl_Copay.Checked = true;
                                txtpbp_b16a_copay_amt_dxmin.Enabled = true;
                            }
                            else
                            {
                                chck_Denatl_Copay.Checked = false;
                                txtpbp_b16a_copay_amt_dxmin.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Denatl_Copay.Checked = false;
                            txtpbp_b16a_copay_amt_dxmin.Enabled = false;
                            txtpbp_b16a_copay_amt_dxmin.Text = string.Empty;
                        }
                        if (pbp_b16a_copay_ehc.Length >= 2)
                        {
                            if (PreventiveCopay[1] == '1')
                            {
                                chck_Oral_Copay.Checked = true;
                                txtpbp_b16a_copay_amt_oemin.Enabled = true;
                            }
                            else
                            {
                                chck_Oral_Copay.Checked = false;
                                txtpbp_b16a_copay_amt_oemin.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Oral_Copay.Checked = false;
                            txtpbp_b16a_copay_amt_oemin.Enabled = false;
                            txtpbp_b16a_copay_amt_oemin.Text = string.Empty;
                        }

                        if (pbp_b16a_copay_ehc.Length >= 3)
                        {
                            if (PreventiveCopay[2] == '1')
                            {
                                chck_Prophylaxis_Copay.Checked = true;
                                txtpbp_b16a_copay_amt_pcmin.Enabled = true;
                            }
                            else
                            {
                                chck_Prophylaxis_Copay.Checked = false;
                                txtpbp_b16a_copay_amt_pcmin.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Prophylaxis_Copay.Checked = false;
                            txtpbp_b16a_copay_amt_pcmin.Enabled = false;
                            txtpbp_b16a_copay_amt_pcmin.Text = string.Empty;
                        }
                    }
                    else
                    {
                        chck_Denatl_Copay.Checked = false;
                        txtpbp_b16a_copay_amt_dxmin.Enabled = false;
                        txtpbp_b16a_copay_amt_dxmin.Text = string.Empty;

                        chck_Oral_Copay.Checked = false;
                        txtpbp_b16a_copay_amt_oemin.Enabled = false;
                        txtpbp_b16a_copay_amt_oemin.Text = string.Empty;

                        chck_Prophylaxis_Copay.Checked = false;
                        txtpbp_b16a_copay_amt_pcmin.Enabled = false;
                        txtpbp_b16a_copay_amt_pcmin.Text = string.Empty;
                        Session["pbp_b16a_copay_ehc"] = 0;

                    }
                    txtpbp_b16a_copay_amt_oemin.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_copay_amt_oemin"].ToString();
                    txtpbp_b16a_copay_amt_dxmin.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_copay_amt_dxmin"].ToString();
                    txtpbp_b16a_copay_amt_pcmin.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_copay_amt_pcmin"].ToString();

                    txtpbp_b16a_bendesc_numv_oe.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_numv_oe"].ToString();
                    txtpbp_b16a_bendesc_numv_dx.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_numv_dx"].ToString();
                    txtpbp_b16a_bendesc_numv_pc.Text = lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_numv_pc"].ToString();

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_oe"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_per_oe.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_per_oe.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_oe"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_dx"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_per_dx.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_per_dx.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_dx"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_pc"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_per_pc.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_per_pc.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_pc"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_amo_oe"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_amo_oe.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_amo_oe.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_amo_oe"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_amo_dx"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_amo_dx.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_amo_dx.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16a_bendesc_amo_dx"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["PBP_B16A_BENDESC_AMO_PC"].ToString() == "")
                    {
                        ddlPBP_B16A_BENDESC_AMO_PC.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlPBP_B16A_BENDESC_AMO_PC.SelectedValue = lobjDS.Tables[0].Rows[0]["PBP_B16A_BENDESC_AMO_PC"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16b_maxbene_type"].ToString() == "")
                    {
                        ddlpbp_b16b_maxbene_type.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16b_maxbene_type.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16b_maxbene_type"].ToString();
                    }
                    #endregion

                    //Comprehensive Dental Care
                    #region Comprehensive Dental Care
                    string pbp_b16b_coins_ehc = lobjDS.Tables[0].Rows[0]["pbp_b16b_coins_ehc"].ToString();

                    if (pbp_b16b_coins_ehc != String.Empty && pbp_b16b_coins_ehc.Length > 0)
                    {
                        char[] ComprehensiveCoin = pbp_b16b_coins_ehc.ToCharArray();

                        // 5th To Bind Prosthodontics, Other Oral/Maxillofacial
                        if (pbp_b16b_coins_ehc.Length >= 1)
                        {
                            if (ComprehensiveCoin[0] == '1')
                            {
                                chk_Prosthodontics_coin.Checked = true;
                                txtpbp_b16b_coins_pct_poo_min.Enabled = true;
                            }
                            else
                            {
                                chk_Prosthodontics_coin.Checked = false;
                                txtpbp_b16b_coins_pct_poo_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Prosthodontics_coin.Checked = false;
                            txtpbp_b16b_coins_pct_poo_min.Enabled = false;
                            txtpbp_b16b_coins_pct_poo_min.Text = string.Empty;
                        }

                        if (pbp_b16b_coins_ehc.Length >= 2)
                        {
                            if (ComprehensiveCoin[1] == '1')
                            {
                                chk_Medicare_Coin.Checked = true;
                                txtPBP_B16B_COINS_PCT_MC_MIN.Enabled = true;
                            }
                            else
                            {
                                chk_Medicare_Coin.Checked = false;
                                txtPBP_B16B_COINS_PCT_MC_MIN.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Medicare_Coin.Checked = false;
                            txtPBP_B16B_COINS_PCT_MC_MIN.Enabled = false;
                            txtPBP_B16B_COINS_PCT_MC_MIN.Text = string.Empty;
                        }

                        if (ComprehensiveCoin.Length >= 4)
                        {
                            Session["pbp_b16b_coins_ehc"] = ComprehensiveCoin[2].ToString() + ComprehensiveCoin[3].ToString();
                        }
                        else if (ComprehensiveCoin.Length >= 3)
                        {
                            Session["pbp_b16b_coins_ehc"] = ComprehensiveCoin[2].ToString() + "0";
                        }
                        else
                        {
                            Session["pbp_b16b_coins_ehc"] = null;
                        }

                        if (pbp_b16b_coins_ehc.Length >= 5)
                        {
                            if (ComprehensiveCoin[4] == '1')
                            {
                                chk_Restorative_Coin.Checked = true;
                                txtpbp_b16b_coins_pct_rs_min.Enabled = true;
                            }
                            else
                            {
                                chk_Restorative_Coin.Checked = false;
                                txtpbp_b16b_coins_pct_rs_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Restorative_Coin.Checked = false;
                            txtpbp_b16b_coins_pct_rs_min.Enabled = false;
                            txtpbp_b16b_coins_pct_rs_min.Text = string.Empty;
                        }
                        if (pbp_b16b_coins_ehc.Length >= 6)
                        {

                            if (ComprehensiveCoin[5] == '1')
                            {
                                chk_Endodontics_Coin.Checked = true;
                                txtpbp_b16b_coins_pct_end_min.Enabled = true;
                            }
                            else
                            {
                                chk_Endodontics_Coin.Checked = false;
                                txtpbp_b16b_coins_pct_end_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Endodontics_Coin.Checked = false;
                            txtpbp_b16b_coins_pct_end_min.Enabled = false;
                            txtpbp_b16b_coins_pct_end_min.Text = string.Empty;
                        }
                        if (pbp_b16b_coins_ehc.Length >= 7)
                        {
                            if (ComprehensiveCoin[6] == '1')
                            {
                                chk_Periodontics_coin.Checked = true;
                                txtpbp_b16b_coins_pct_peri_min.Enabled = true;
                            }
                            else
                            {
                                chk_Periodontics_coin.Checked = false;
                                txtpbp_b16b_coins_pct_peri_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Periodontics_coin.Checked = false;
                            txtpbp_b16b_coins_pct_peri_min.Enabled = false;
                            txtpbp_b16b_coins_pct_peri_min.Text = string.Empty;
                        }
                        if (pbp_b16b_coins_ehc.Length >= 8)
                        {
                            if (ComprehensiveCoin[7] == '1')
                            {
                                chk_Extractions_coin.Checked = true;
                                txtpbp_b16b_coins_pct_ext_min.Enabled = true;
                            }
                            else
                            {
                                chk_Extractions_coin.Checked = false;
                                txtpbp_b16b_coins_pct_ext_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Extractions_coin.Checked = false;
                            txtpbp_b16b_coins_pct_ext_min.Enabled = false;
                            txtpbp_b16b_coins_pct_ext_min.Text = string.Empty;
                        }
                    }
                    else
                    {
                        chk_Prosthodontics_coin.Checked = false;
                        txtpbp_b16b_coins_pct_poo_min.Enabled = false;
                        txtpbp_b16b_coins_pct_poo_min.Text = string.Empty;

                        chk_Medicare_Coin.Checked = false;
                        txtPBP_B16B_COINS_PCT_MC_MIN.Enabled = false;
                        txtPBP_B16B_COINS_PCT_MC_MIN.Text = string.Empty;

                        chk_Restorative_Coin.Checked = false;
                        txtpbp_b16b_coins_pct_rs_min.Enabled = false;
                        txtpbp_b16b_coins_pct_rs_min.Text = string.Empty;

                        chk_Endodontics_Coin.Checked = false;
                        txtpbp_b16b_coins_pct_end_min.Enabled = false;
                        txtpbp_b16b_coins_pct_end_min.Text = string.Empty;

                        chk_Periodontics_coin.Checked = false;
                        txtpbp_b16b_coins_pct_peri_min.Enabled = false;
                        txtpbp_b16b_coins_pct_peri_min.Text = string.Empty;

                        chk_Extractions_coin.Checked = false;
                        txtpbp_b16b_coins_pct_ext_min.Enabled = false;
                        txtpbp_b16b_coins_pct_ext_min.Text = string.Empty;

                        Session["pbp_b16b_coins_ehc"] = null;
                    }

                    txtPBP_B16B_COINS_PCT_MC_MIN.Text = lobjDS.Tables[0].Rows[0]["PBP_B16B_COINS_PCT_MC_MIN"].ToString();
                    txtpbp_b16b_coins_pct_rs_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_coins_pct_rs_min"].ToString();
                    txtpbp_b16b_coins_pct_end_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_coins_pct_end_min"].ToString();
                    txtpbp_b16b_coins_pct_peri_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_coins_pct_peri_min"].ToString();
                    txtpbp_b16b_coins_pct_ext_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_coins_pct_ext_min"].ToString();
                    txtpbp_b16b_coins_pct_poo_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_coins_pct_poo_min"].ToString();


                    string pbp_b16b_copay_ehc = lobjDS.Tables[0].Rows[0]["pbp_b16b_copay_ehc"].ToString();
                    if (pbp_b16b_copay_ehc != String.Empty && pbp_b16b_copay_ehc.Length > 0)
                    {
                        char[] ComprehensiveCopay = pbp_b16b_copay_ehc.ToCharArray();

                        if (pbp_b16b_copay_ehc.Length >= 1)
                        {
                            if (ComprehensiveCopay[0] == '1')
                            {
                                chk_Prosthodontics_copay.Checked = true;
                                txtpbp_b16b_copay_amt_poo_min.Enabled = true;
                            }
                            else
                            {
                                chk_Prosthodontics_copay.Checked = false;
                                txtpbp_b16b_copay_amt_poo_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Prosthodontics_copay.Checked = false;
                            txtpbp_b16b_copay_amt_poo_min.Enabled = false;
                            txtpbp_b16b_copay_amt_poo_min.Text = string.Empty;
                        }

                        if (pbp_b16b_copay_ehc.Length >= 2)
                        {
                            if (ComprehensiveCopay[1] == '1')
                            {
                                chk_Medicare_Copay.Checked = true;
                                txtPBP_B16B_COPAY_AMT_MC_MIN.Enabled = true;
                            }
                            else
                            {
                                chk_Medicare_Copay.Checked = false;
                                txtPBP_B16B_COPAY_AMT_MC_MIN.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Medicare_Copay.Checked = false;
                            txtPBP_B16B_COPAY_AMT_MC_MIN.Enabled = false;
                            txtPBP_B16B_COPAY_AMT_MC_MIN.Text = string.Empty;
                        }

                        if (ComprehensiveCopay.Length >= 4)
                        {
                            Session["getpbp_b16b_copay_ehc"] = ComprehensiveCopay[2].ToString() + ComprehensiveCopay[3].ToString();
                        }
                        else if (ComprehensiveCopay.Length >= 3)
                        {
                            Session["getpbp_b16b_copay_ehc"] = ComprehensiveCopay[2].ToString() + "0";
                        }
                        else
                        {
                            Session["getpbp_b16b_copay_ehc"] = null;
                        }

                        if (pbp_b16b_copay_ehc.Length >= 5)
                        {
                            if (ComprehensiveCopay[4] == '1')
                            {
                                chk_Restorative_Copay.Checked = true;
                                txtpbp_b16b_copay_amt_rs_min.Enabled = true;
                            }
                            else
                            {
                                chk_Restorative_Copay.Checked = false;
                                txtpbp_b16b_copay_amt_rs_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Restorative_Copay.Checked = false;
                            txtpbp_b16b_copay_amt_rs_min.Enabled = false;
                            txtpbp_b16b_copay_amt_rs_min.Text = string.Empty;
                        }

                        if (pbp_b16b_copay_ehc.Length >= 6)
                        {
                            if (ComprehensiveCopay[5] == '1')
                            {
                                chk_Endodontics_Copay.Checked = true;
                                txtpbp_b16b_copay_amt_end_min.Enabled = true;
                            }
                            else
                            {
                                chk_Endodontics_Copay.Checked = false;
                                txtpbp_b16b_copay_amt_end_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Endodontics_Copay.Checked = false;
                            txtpbp_b16b_copay_amt_end_min.Enabled = false;
                            txtpbp_b16b_copay_amt_end_min.Text = string.Empty;
                        }

                        if (pbp_b16b_copay_ehc.Length >= 7)
                        {
                            if (ComprehensiveCopay[6] == '1')
                            {
                                chk_Periodontics_copay.Checked = true;
                                txtpbp_b16b_copay_amt_peri_min.Enabled = true;
                            }
                            else
                            {
                                chk_Periodontics_copay.Checked = false;
                                txtpbp_b16b_copay_amt_peri_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Periodontics_copay.Checked = false;
                            txtpbp_b16b_copay_amt_peri_min.Enabled = false;
                            txtpbp_b16b_copay_amt_peri_min.Text = string.Empty;
                        }
                        if (pbp_b16b_copay_ehc.Length >= 8)
                        {
                            if (ComprehensiveCopay[7] == '1')
                            {
                                chk_Extractions_copay.Checked = true;
                                txtpbp_b16b_copay_amt_ext_min.Enabled = true;
                            }
                            else
                            {
                                chk_Extractions_copay.Checked = false;
                                txtpbp_b16b_copay_amt_ext_min.Enabled = false;
                            }
                        }
                        else
                        {
                            chk_Extractions_copay.Checked = false;
                            txtpbp_b16b_copay_amt_ext_min.Enabled = false;
                            txtpbp_b16b_copay_amt_ext_min.Text = string.Empty;
                        }
                    }
                    else
                    {
                        chk_Prosthodontics_copay.Checked = false;
                        txtpbp_b16b_copay_amt_poo_min.Enabled = false;
                        txtpbp_b16b_copay_amt_poo_min.Text = string.Empty;

                        chk_Medicare_Copay.Checked = false;
                        txtPBP_B16B_COPAY_AMT_MC_MIN.Enabled = false;
                        txtPBP_B16B_COPAY_AMT_MC_MIN.Text = string.Empty;

                        chk_Restorative_Copay.Checked = false;
                        txtpbp_b16b_copay_amt_rs_min.Enabled = false;
                        txtpbp_b16b_copay_amt_rs_min.Text = string.Empty;

                        chk_Endodontics_Copay.Checked = false;
                        txtpbp_b16b_copay_amt_end_min.Enabled = false;
                        txtpbp_b16b_copay_amt_end_min.Text = string.Empty;

                        chk_Periodontics_copay.Checked = false;
                        txtpbp_b16b_copay_amt_peri_min.Enabled = false;
                        txtpbp_b16b_copay_amt_peri_min.Text = string.Empty;

                        chk_Extractions_copay.Checked = false;
                        txtpbp_b16b_copay_amt_ext_min.Enabled = false;
                        txtpbp_b16b_copay_amt_ext_min.Text = string.Empty;

                        Session["getpbp_b16b_copay_ehc"] = null;
                    }

                    txtPBP_B16B_COPAY_AMT_MC_MIN.Text = lobjDS.Tables[0].Rows[0]["PBP_B16B_COPAY_AMT_MC_MIN"].ToString();
                    txtpbp_b16b_copay_amt_rs_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_copay_amt_rs_min"].ToString();
                    txtpbp_b16b_copay_amt_end_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_copay_amt_end_min"].ToString();
                    txtpbp_b16b_copay_amt_peri_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_copay_amt_peri_min"].ToString();
                    txtpbp_b16b_copay_amt_ext_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_copay_amt_ext_min"].ToString();
                    txtpbp_b16b_copay_amt_poo_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_copay_amt_poo_min"].ToString();

                    txtpbp_b16b_bendesc_numv_rs.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_numv_rs"].ToString();
                    txtpbp_b16b_bendesc_num_end.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_num_end"].ToString();
                    txtpbp_b16b_bendesc_num_peri.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_num_peri"].ToString();
                    txtpbp_b16b_bendesc_num_ext.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_num_ext"].ToString();
                    txtpbp_b16b_bendesc_numv_poo.Text = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_numv_poo"].ToString();

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_rs"].ToString() == "")
                    {
                        ddlpbp_b16b_bendesc_per_rs.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16b_bendesc_per_rs.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_rs"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_end"].ToString() == "")
                    {
                        ddlpbp_b16b_bendesc_per_end.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16b_bendesc_per_end.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_end"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_peri"].ToString() == "")
                    {
                        ddlpbp_b16b_bendesc_per_peri.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16b_bendesc_per_peri.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_peri"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_ext"].ToString() == "")
                    {
                        ddlpbp_b16b_bendesc_per_ext.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16b_bendesc_per_ext.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_ext"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_poo"].ToString() == "")
                    {
                        ddlpbp_b16b_bendesc_per_poo.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b16b_bendesc_per_poo.SelectedValue = lobjDS.Tables[0].Rows[0]["pbp_b16b_bendesc_per_poo"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_RS"].ToString() == "")
                    {
                        ddlPBP_B16B_BENDESC_AMO_RS.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlPBP_B16B_BENDESC_AMO_RS.SelectedValue = lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_RS"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_END"].ToString() == "")
                    {
                        ddlPBP_B16B_BENDESC_AMO_END.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlPBP_B16B_BENDESC_AMO_END.SelectedValue = lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_END"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_PERI"].ToString() == "")
                    {
                        ddlPBP_B16B_BENDESC_AMO_PERI.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlPBP_B16B_BENDESC_AMO_PERI.SelectedValue = lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_PERI"].ToString();
                    }

                    if (lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_EXT"].ToString() == "")
                    {
                        ddlPBP_B16B_BENDESC_AMO_EXT.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlPBP_B16B_BENDESC_AMO_EXT.SelectedValue = lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_EXT"].ToString();
                    }


                    if (lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_POO"].ToString() == "")
                    {
                        ddlPBP_B16B_BENDESC_AMO_POO.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlPBP_B16B_BENDESC_AMO_POO.SelectedValue = lobjDS.Tables[0].Rows[0]["PBP_B16B_BENDESC_AMO_POO"].ToString();
                    }

                    #endregion
                    // Emergency Room

                    txtpbp_b4a_max_visit.Text = lobjDS.Tables[0].Rows[0]["pbp_b4a_max_visit"].ToString();
                    txtpbp_b4a_coins_pct_mc_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b4a_coins_pct_mc_min"].ToString();
                    txtpbp_b4a_copay_amt_mc_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b4a_copay_amt_mc_min"].ToString();

                    // Binding Speciality Care Physician

                    txtpbp_b7d_coins_pct_mc_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b7d_coins_pct_mc_min"].ToString();
                    txtpbp_b7d_copay_amt_mc_min.Text = lobjDS.Tables[0].Rows[0]["pbp_b7d_copay_amt_mc_min"].ToString();

                    // Binding Radioalogy
                    #region Radiology
                    if (lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_max_yn"].ToString() == "1")
                    {
                        rd_pbp_b8b_copay_max_yn.SelectedValue = "Yes";
                    }
                    else if (lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_max_yn"].ToString() == "2")
                    {
                        rd_pbp_b8b_copay_max_yn.SelectedValue = "No";
                    }
                    else
                    {
                        rd_pbp_b8b_copay_max_yn.SelectedValue = "";
                    }
                    string pbp_b8b_coins_ehc = lobjDS.Tables[0].Rows[0]["pbp_b8b_coins_ehc"].ToString();
                    if (pbp_b8b_coins_ehc != String.Empty && pbp_b8b_coins_ehc.Length > 0)
                    {
                        char[] RadiologyCoin = pbp_b8b_coins_ehc.ToCharArray();
                        if (pbp_b8b_coins_ehc.Length >= 1)
                        {
                            if (RadiologyCoin[0] == '1')
                            {
                                chck_XRay_Coin.Checked = true;
                                txtpbp_b8b_coins_pct_cmc.Enabled = true;
                                txtpbp_b8b_coins_pct_cmc_max.Enabled = true;
                            }
                            else
                            {
                                chck_XRay_Coin.Checked = false;
                                txtpbp_b8b_coins_pct_cmc.Enabled = false;
                                txtpbp_b8b_coins_pct_cmc_max.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_XRay_Coin.Checked = false;
                            txtpbp_b8b_coins_pct_cmc.Enabled = false;
                            txtpbp_b8b_coins_pct_cmc_max.Enabled = false;
                            txtpbp_b8b_coins_pct_cmc.Text = string.Empty;
                            txtpbp_b8b_coins_pct_cmc_max.Text = string.Empty;
                        }

                        if (pbp_b8b_coins_ehc.Length >= 2)
                        {
                            if (RadiologyCoin[1] == '1')
                            {
                                chck_Diagnostic_Coin.Checked = true;
                                txtpbp_b8b_coins_pct_drs.Enabled = true;
                                txtpbp_b8b_coins_pct_drs_max.Enabled = true;
                            }
                            else
                            {
                                chck_Diagnostic_Coin.Checked = false;
                                txtpbp_b8b_coins_pct_drs.Enabled = false;
                                txtpbp_b8b_coins_pct_drs_max.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Diagnostic_Coin.Checked = false;
                            txtpbp_b8b_coins_pct_drs.Enabled = false;
                            txtpbp_b8b_coins_pct_drs_max.Enabled = false;
                            txtpbp_b8b_coins_pct_drs.Text = string.Empty;
                            txtpbp_b8b_coins_pct_drs_max.Text = string.Empty;
                        }
                        if (pbp_b8b_coins_ehc.Length >= 3)
                        {
                            if (RadiologyCoin[2] == '1')
                            {
                                chck_Therapeutic_Coin.Checked = true;
                                txtpbp_b8b_coins_pct_tmc.Enabled = true;
                                txtpbp_b8b_coins_pct_tmc_max.Enabled = true;
                            }
                            else
                            {
                                chck_Therapeutic_Coin.Checked = false;
                                txtpbp_b8b_coins_pct_tmc.Enabled = false;
                                txtpbp_b8b_coins_pct_tmc_max.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Therapeutic_Coin.Checked = false;
                            txtpbp_b8b_coins_pct_tmc.Enabled = false;
                            txtpbp_b8b_coins_pct_tmc_max.Enabled = false;
                            txtpbp_b8b_coins_pct_tmc.Text = string.Empty;
                            txtpbp_b8b_coins_pct_tmc_max.Text = string.Empty;
                        }
                    }
                    else
                    {
                        chck_XRay_Coin.Checked = false;
                        txtpbp_b8b_coins_pct_cmc.Enabled = false;
                        txtpbp_b8b_coins_pct_cmc_max.Enabled = false;
                        txtpbp_b8b_coins_pct_cmc.Text = string.Empty;
                        txtpbp_b8b_coins_pct_cmc_max.Text = string.Empty;

                        chck_Diagnostic_Coin.Checked = false;
                        txtpbp_b8b_coins_pct_drs.Enabled = false;
                        txtpbp_b8b_coins_pct_drs_max.Enabled = false;
                        txtpbp_b8b_coins_pct_drs.Text = string.Empty;
                        txtpbp_b8b_coins_pct_drs_max.Text = string.Empty;

                        chck_Therapeutic_Coin.Checked = false;
                        txtpbp_b8b_coins_pct_tmc.Enabled = false;
                        txtpbp_b8b_coins_pct_tmc_max.Enabled = false;
                        txtpbp_b8b_coins_pct_tmc.Text = string.Empty;
                        txtpbp_b8b_coins_pct_tmc_max.Text = string.Empty;
                    }
                    txtpbp_b8b_coins_pct_cmc.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_cmc"].ToString();
                    txtpbp_b8b_coins_pct_tmc.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_tmc"].ToString();
                    txtpbp_b8b_coins_pct_drs.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_drs"].ToString();

                    txtpbp_b8b_coins_pct_cmc_max.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_cmc_max"].ToString();
                    txtpbp_b8b_coins_pct_tmc_max.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_tmc_max"].ToString();
                    txtpbp_b8b_coins_pct_drs_max.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_drs_max"].ToString();

                    string pbp_b8b_copay_ehc = lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_ehc"].ToString();
                    if (pbp_b8b_copay_ehc != String.Empty && pbp_b8b_copay_ehc.Length > 0)
                    {
                        char[] RadiologyCopay = pbp_b8b_copay_ehc.ToCharArray();

                        if (pbp_b8b_copay_ehc.Length >= 1)
                        {
                            if (RadiologyCopay[0] == '1')
                            {
                                chck_XRay_Copay.Checked = true;
                                txtpbp_b8b_copay_mc_amt.Enabled = true;
                                txtpbp_b8b_copay_mc_amt_max.Enabled = true;
                            }
                            else
                            {
                                chck_XRay_Copay.Checked = false;
                                txtpbp_b8b_copay_mc_amt.Enabled = false;
                                txtpbp_b8b_copay_mc_amt_max.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_XRay_Copay.Checked = false;
                            txtpbp_b8b_copay_mc_amt.Enabled = false;
                            txtpbp_b8b_copay_mc_amt_max.Enabled = false;
                            txtpbp_b8b_copay_mc_amt.Text = string.Empty;
                            txtpbp_b8b_copay_mc_amt_max.Text = string.Empty;
                        }
                        if (pbp_b8b_copay_ehc.Length >= 2)
                        {
                            if (RadiologyCopay[1] == '1')
                            {
                                chck_Diagnostic_Copay.Checked = true;
                                txtpbp_b8b_copay_amt_drs.Enabled = true;
                                txtpbp_b8b_copay_amt_drs_max.Enabled = true;
                            }
                            else
                            {
                                chck_Diagnostic_Copay.Checked = false;
                                txtpbp_b8b_copay_amt_drs.Enabled = false;
                                txtpbp_b8b_copay_amt_drs_max.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Diagnostic_Copay.Checked = false;
                            txtpbp_b8b_copay_amt_drs.Enabled = false;
                            txtpbp_b8b_copay_amt_drs_max.Enabled = false;
                            txtpbp_b8b_copay_amt_drs.Text = string.Empty;
                            txtpbp_b8b_copay_amt_drs_max.Text = string.Empty;
                        }
                        if (pbp_b8b_copay_ehc.Length >= 3)
                        {
                            if (RadiologyCopay[2] == '1')
                            {
                                chck_Therapeutic_Copay.Checked = true;
                                txtpbp_b8b_copay_amt_tmc.Enabled = true;
                                txtpbp_b8b_copay_amt_tmc_max.Enabled = true;
                            }
                            else
                            {
                                chck_Therapeutic_Copay.Checked = false;
                                txtpbp_b8b_copay_amt_tmc.Enabled = false;
                                txtpbp_b8b_copay_amt_tmc_max.Enabled = false;
                            }
                        }
                        else
                        {
                            chck_Therapeutic_Copay.Checked = false;
                            txtpbp_b8b_copay_amt_tmc.Enabled = false;
                            txtpbp_b8b_copay_amt_tmc_max.Enabled = false;
                            txtpbp_b8b_copay_amt_tmc.Text = string.Empty;
                            txtpbp_b8b_copay_amt_tmc_max.Text = string.Empty;
                        }
                    }
                    else
                    {
                        chck_XRay_Copay.Checked = false;
                        txtpbp_b8b_copay_mc_amt.Enabled = false;
                        txtpbp_b8b_copay_mc_amt_max.Enabled = false;
                        txtpbp_b8b_copay_mc_amt.Text = string.Empty;
                        txtpbp_b8b_copay_mc_amt_max.Text = string.Empty;

                        chck_Diagnostic_Copay.Checked = false;
                        txtpbp_b8b_copay_amt_drs.Enabled = false;
                        txtpbp_b8b_copay_amt_drs_max.Enabled = false;
                        txtpbp_b8b_copay_amt_drs.Text = string.Empty;
                        txtpbp_b8b_copay_amt_drs_max.Text = string.Empty;

                        chck_Therapeutic_Copay.Checked = false;
                        txtpbp_b8b_copay_amt_tmc.Enabled = false;
                        txtpbp_b8b_copay_amt_tmc_max.Enabled = false;
                        txtpbp_b8b_copay_amt_tmc.Text = string.Empty;
                        txtpbp_b8b_copay_amt_tmc_max.Text = string.Empty;
                    }
                    txtpbp_b8b_copay_mc_amt.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_mc_amt"].ToString();
                    txtpbp_b8b_copay_amt_tmc.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_amt_tmc"].ToString();
                    txtpbp_b8b_copay_amt_drs.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_amt_drs"].ToString();

                    txtpbp_b8b_copay_mc_amt_max.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_mc_amt_max"].ToString();
                    txtpbp_b8b_copay_amt_tmc_max.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_amt_tmc_max"].ToString();
                    txtpbp_b8b_copay_amt_drs_max.Text = lobjDS.Tables[0].Rows[0]["pbp_b8b_copay_amt_drs_max"].ToString();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Bind_MRX()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_MRX", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@BidId", ddlBidId.SelectedValue);
                lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());

                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);

                if (lobjDS.Tables[0].Rows.Count > 0)
                {
                    if (lobjDS.Tables[0].Rows[0]["mrx_benefit_type"].ToString() == "")
                    {
                        ddlmrx_benefit_type.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlmrx_benefit_type.SelectedValue = lobjDS.Tables[0].Rows[0]["mrx_benefit_type"].ToString();
                    }

                    txtmrx_alt_cov_lmt_amt.Text = lobjDS.Tables[0].Rows[0]["mrx_alt_cov_lmt_amt"].ToString();
                    txtmrx_alt_ded_amount.Text = lobjDS.Tables[0].Rows[0]["mrx_alt_ded_amount"].ToString();

                    string mrx_alt_no_ded_tier = lobjDS.Tables[0].Rows[0]["mrx_alt_no_ded_tier"].ToString();
                    if (mrx_alt_no_ded_tier != String.Empty && mrx_alt_no_ded_tier.Length > 0)
                    {
                        char[] charArr = mrx_alt_no_ded_tier.ToCharArray();
                        RadComboBoxItem item6 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 6");
                        RadComboBoxItem item1 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 1");
                        RadComboBoxItem item2 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 2");
                        RadComboBoxItem item3 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 3");
                        RadComboBoxItem item4 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 4");
                        RadComboBoxItem item5 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 5");

                        if (mrx_alt_no_ded_tier.Length >= 1)
                        {
                            if (charArr[0] == '1')
                            {
                                item6.Checked = true;
                            }
                            else
                            {
                                item6.Checked = false;
                            }
                        }
                        else
                        {
                            item6.Checked = false;
                        }
                        if (mrx_alt_no_ded_tier.Length >= 2)
                        {

                            if (charArr[1] == '1')
                            {
                                item1.Checked = true;
                            }
                            else
                            {
                                item1.Checked = false;
                            }
                        }
                        else
                        {
                            item1.Checked = false;
                        }
                        if (mrx_alt_no_ded_tier.Length >= 3)
                        {

                            if (charArr[2] == '1')
                            {
                                item2.Checked = true;
                            }
                            else
                            {
                                item2.Checked = false;
                            }
                        }
                        else
                        {
                            item2.Checked = false;
                        }
                        if (mrx_alt_no_ded_tier.Length >= 4)
                        {

                            if (charArr[3] == '1')
                            {
                                item3.Checked = true;
                            }
                            else
                            {
                                item3.Checked = false;
                            }
                        }
                        else
                        {
                            item3.Checked = false;
                        }
                        if (mrx_alt_no_ded_tier.Length >= 5)
                        {

                            if (charArr[4] == '1')
                            {
                                item4.Checked = true;
                            }
                            else
                            {
                                item4.Checked = false;
                            }
                        }
                        else
                        {
                            item4.Checked = false;
                        }
                        if (mrx_alt_no_ded_tier.Length >= 6)
                        {

                            if (charArr[5] == '1')
                            {
                                item5.Checked = true;
                            }
                            else
                            {
                                item5.Checked = false;
                            }
                        }
                        else
                        {
                            item5.Checked = false;
                        }
                    }

                    else
                    {

                        RadComboBoxItem item6 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 6");
                        item6.Checked = false;

                        RadComboBoxItem item1 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 1");
                        item1.Checked = false;

                        RadComboBoxItem item2 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 2");
                        item2.Checked = false;

                        RadComboBoxItem item3 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 3");
                        item3.Checked = false;

                        RadComboBoxItem item4 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 4");
                        item4.Checked = false;

                        RadComboBoxItem item5 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 5");
                        item5.Checked = false;

                    }
                }
                else
                {
                    ddlmrx_benefit_type.SelectedValue = "Select";
                    txtmrx_alt_cov_lmt_amt.Text = string.Empty;
                    txtmrx_alt_ded_amount.Text = string.Empty;
                    RadComboBoxItem item6 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 6");
                    item6.Checked = false;

                    RadComboBoxItem item1 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 1");
                    item1.Checked = false;

                    RadComboBoxItem item2 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 2");
                    item2.Checked = false;

                    RadComboBoxItem item3 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 3");
                    item3.Checked = false;

                    RadComboBoxItem item4 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 4");
                    item4.Checked = false;

                    RadComboBoxItem item5 = ddlmrx_alt_no_ded_tier.FindItemByText("Tier 5");
                    item5.Checked = false;
                }

                if (lobjDS.Tables[1].Rows.Count > 0)
                {
                    if (lobjDS.Tables[1].Rows.Count >= 1)
                    {
                        if (lobjDS.Tables[1].Rows[0]["mrx_tier_cstshr_struct_type"].ToString() == "")
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier1.SelectedValue = "Select";
                        }
                        else
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier1.SelectedValue = lobjDS.Tables[1].Rows[0]["mrx_tier_cstshr_struct_type"].ToString();
                        }

                        txtmrx_tier_rstd_coins_1m_tier1.Text = lobjDS.Tables[1].Rows[0]["mrx_tier_rstd_coins_1m"].ToString();
                        txtmrx_tier_rstd_copay_1m_tier1.Text = lobjDS.Tables[1].Rows[0]["mrx_tier_rstd_copay_1m"].ToString();
                        txtmrx_tier_rsstd_coins_1m_tier1.Text = lobjDS.Tables[1].Rows[0]["mrx_tier_rsstd_coins_1m"].ToString();
                        txtmrx_tier_rsstd_copay_1m_tier1.Text = lobjDS.Tables[1].Rows[0]["mrx_tier_rsstd_copay_1m"].ToString();
                        txtmrx_tier_rspfd_coins_1m_tier1.Text = lobjDS.Tables[1].Rows[0]["mrx_tier_rspfd_coins_1m"].ToString();
                        txtmrx_tier_rspfd_copay_1m_tier1.Text = lobjDS.Tables[1].Rows[0]["mrx_tier_rspfd_copay_1m"].ToString();

                        txtmrx_tier_oonp_coins_1m_tier1.Text = lobjDS.Tables[1].Rows[0]["mrx_tier_oonp_coins_1m"].ToString();
                        txtmrx_tier_oonp_copay_1m_tier1.Text = lobjDS.Tables[1].Rows[0]["mrx_tier_oonp_copay_1m"].ToString();

                        if (lobjDS.Tables[1].Rows[0]["mrx_tier_gap_cost_share"].ToString() == "")
                        {
                            ddlmrx_tier_gap_cost_share_tier1.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlmrx_tier_gap_cost_share_tier1.SelectedValue = lobjDS.Tables[1].Rows[0]["mrx_tier_gap_cost_share"].ToString();
                        }

                        if (lobjDS.Tables[1].Rows[0]["MRX_TIER_INCLUDES"].ToString() == "")
                        {
                            ddlMRX_TIER_INCLUDES_tier1.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlMRX_TIER_INCLUDES_tier1.SelectedValue = lobjDS.Tables[1].Rows[0]["MRX_TIER_INCLUDES"].ToString();
                        }

                    }
                    else
                    {
                        ddl1mrx_tier_cstshr_struct_type_tier1.SelectedValue = "Select";
                        txtmrx_tier_rstd_coins_1m_tier1.Text = string.Empty;
                        txtmrx_tier_rstd_copay_1m_tier1.Text = string.Empty;
                        txtmrx_tier_rsstd_coins_1m_tier1.Text = string.Empty;
                        txtmrx_tier_rsstd_copay_1m_tier1.Text = string.Empty;
                        txtmrx_tier_rspfd_coins_1m_tier1.Text = string.Empty;
                        txtmrx_tier_rspfd_copay_1m_tier1.Text = string.Empty;
                        txtmrx_tier_oonp_coins_1m_tier1.Text = string.Empty;
                        txtmrx_tier_oonp_copay_1m_tier1.Text = string.Empty;
                        ddlmrx_tier_gap_cost_share_tier1.SelectedValue = "Select";
                        ddlMRX_TIER_INCLUDES_tier1.SelectedValue = "Select";
                    }

                    if (lobjDS.Tables[1].Rows.Count >= 2)
                    {
                        if (lobjDS.Tables[1].Rows[1]["mrx_tier_cstshr_struct_type"].ToString() == "")
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier2.SelectedValue = "Select";
                        }
                        else
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier2.SelectedValue = lobjDS.Tables[1].Rows[1]["mrx_tier_cstshr_struct_type"].ToString();
                        }
                        txtmrx_tier_rstd_coins_1m_tier2.Text = lobjDS.Tables[1].Rows[1]["mrx_tier_rstd_coins_1m"].ToString();
                        txtmrx_tier_rstd_copay_1m_tier2.Text = lobjDS.Tables[1].Rows[1]["mrx_tier_rstd_copay_1m"].ToString();
                        txtmrx_tier_rsstd_coins_1m_tier2.Text = lobjDS.Tables[1].Rows[1]["mrx_tier_rsstd_coins_1m"].ToString();
                        txtmrx_tier_rsstd_copay_1m_tier2.Text = lobjDS.Tables[1].Rows[1]["mrx_tier_rsstd_copay_1m"].ToString();
                        txtmrx_tier_rspfd_coins_1m_tier2.Text = lobjDS.Tables[1].Rows[1]["mrx_tier_rspfd_coins_1m"].ToString();
                        txtmrx_tier_rspfd_copay_1m_tier2.Text = lobjDS.Tables[1].Rows[1]["mrx_tier_rspfd_copay_1m"].ToString();

                        txtmrx_tier_oonp_coins_1m_tier2.Text = lobjDS.Tables[1].Rows[1]["mrx_tier_oonp_coins_1m"].ToString();
                        txtmrx_tier_oonp_copay_1m_tier2.Text = lobjDS.Tables[1].Rows[1]["mrx_tier_oonp_copay_1m"].ToString();

                        if (lobjDS.Tables[1].Rows[1]["mrx_tier_gap_cost_share"].ToString() == "")
                        {
                            ddlmrx_tier_gap_cost_share_tier2.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlmrx_tier_gap_cost_share_tier2.SelectedValue = lobjDS.Tables[1].Rows[1]["mrx_tier_gap_cost_share"].ToString();
                        }

                        if (lobjDS.Tables[1].Rows[1]["MRX_TIER_INCLUDES"].ToString() == "")
                        {
                            ddlMRX_TIER_INCLUDES_tier2.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlMRX_TIER_INCLUDES_tier2.SelectedValue = lobjDS.Tables[1].Rows[1]["MRX_TIER_INCLUDES"].ToString();
                        }
                    }
                    else
                    {
                        ddl1mrx_tier_cstshr_struct_type_tier2.SelectedValue = "Select";
                        txtmrx_tier_rstd_coins_1m_tier2.Text = string.Empty;
                        txtmrx_tier_rstd_copay_1m_tier2.Text = string.Empty;
                        txtmrx_tier_rsstd_coins_1m_tier2.Text = string.Empty;
                        txtmrx_tier_rsstd_copay_1m_tier2.Text = string.Empty;
                        txtmrx_tier_rspfd_coins_1m_tier2.Text = string.Empty;
                        txtmrx_tier_rspfd_copay_1m_tier2.Text = string.Empty;
                        txtmrx_tier_oonp_coins_1m_tier2.Text = string.Empty;
                        txtmrx_tier_oonp_copay_1m_tier2.Text = string.Empty;
                        ddlmrx_tier_gap_cost_share_tier2.SelectedValue = "Select";
                        ddlMRX_TIER_INCLUDES_tier2.SelectedValue = "Select";
                    }

                    if (lobjDS.Tables[1].Rows.Count >= 3)
                    {
                        if (lobjDS.Tables[1].Rows[2]["mrx_tier_cstshr_struct_type"].ToString() == "")
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier3.SelectedValue = "Select";
                        }
                        else
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier3.SelectedValue = lobjDS.Tables[1].Rows[2]["mrx_tier_cstshr_struct_type"].ToString();
                        }

                        txtmrx_tier_rstd_coins_1m_tier3.Text = lobjDS.Tables[1].Rows[2]["mrx_tier_rstd_coins_1m"].ToString();
                        txtmrx_tier_rstd_copay_1m_tier3.Text = lobjDS.Tables[1].Rows[2]["mrx_tier_rstd_copay_1m"].ToString();
                        txtmrx_tier_rsstd_coins_1m_tier3.Text = lobjDS.Tables[1].Rows[2]["mrx_tier_rsstd_coins_1m"].ToString();
                        txtmrx_tier_rsstd_copay_1m_tier3.Text = lobjDS.Tables[1].Rows[2]["mrx_tier_rsstd_copay_1m"].ToString();
                        txtmrx_tier_rspfd_coins_1m_tier3.Text = lobjDS.Tables[1].Rows[2]["mrx_tier_rspfd_coins_1m"].ToString();
                        txtmrx_tier_rspfd_copay_1m_tier3.Text = lobjDS.Tables[1].Rows[2]["mrx_tier_rspfd_copay_1m"].ToString();

                        txtmrx_tier_oonp_coins_1m_tier3.Text = lobjDS.Tables[1].Rows[2]["mrx_tier_oonp_coins_1m"].ToString();
                        txtmrx_tier_oonp_copay_1m_tier3.Text = lobjDS.Tables[1].Rows[2]["mrx_tier_oonp_copay_1m"].ToString();

                        if (lobjDS.Tables[1].Rows[2]["mrx_tier_gap_cost_share"].ToString() == "")
                        {
                            ddlmrx_tier_gap_cost_share_tier3.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlmrx_tier_gap_cost_share_tier3.SelectedValue = lobjDS.Tables[1].Rows[2]["mrx_tier_gap_cost_share"].ToString();
                        }

                        if (lobjDS.Tables[1].Rows[2]["MRX_TIER_INCLUDES"].ToString() == "")
                        {
                            ddlMRX_TIER_INCLUDES_tier3.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlMRX_TIER_INCLUDES_tier3.SelectedValue = lobjDS.Tables[1].Rows[2]["MRX_TIER_INCLUDES"].ToString();
                        }

                    }
                    else
                    {
                        ddl1mrx_tier_cstshr_struct_type_tier3.SelectedValue = "Select";
                        txtmrx_tier_rstd_coins_1m_tier3.Text = string.Empty;
                        txtmrx_tier_rstd_copay_1m_tier3.Text = string.Empty;
                        txtmrx_tier_rsstd_coins_1m_tier3.Text = string.Empty;
                        txtmrx_tier_rsstd_copay_1m_tier3.Text = string.Empty;
                        txtmrx_tier_rspfd_coins_1m_tier3.Text = string.Empty;
                        txtmrx_tier_rspfd_copay_1m_tier3.Text = string.Empty;
                        txtmrx_tier_oonp_coins_1m_tier3.Text = string.Empty;
                        txtmrx_tier_oonp_copay_1m_tier3.Text = string.Empty;
                        ddlmrx_tier_gap_cost_share_tier3.SelectedValue = "Select";
                        ddlMRX_TIER_INCLUDES_tier3.SelectedValue = "Select";
                    }

                    if (lobjDS.Tables[1].Rows.Count >= 4)
                    {
                        if (lobjDS.Tables[1].Rows[3]["mrx_tier_cstshr_struct_type"].ToString() == "")
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier4.SelectedValue = "Select";
                        }
                        else
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier4.SelectedValue = lobjDS.Tables[1].Rows[3]["mrx_tier_cstshr_struct_type"].ToString();
                        }
                        txtmrx_tier_rstd_coins_1m_tier4.Text = lobjDS.Tables[1].Rows[3]["mrx_tier_rstd_coins_1m"].ToString();
                        txtmrx_tier_rstd_copay_1m_tier4.Text = lobjDS.Tables[1].Rows[3]["mrx_tier_rstd_copay_1m"].ToString();
                        txtmrx_tier_rsstd_coins_1m_tier4.Text = lobjDS.Tables[1].Rows[3]["mrx_tier_rsstd_coins_1m"].ToString();
                        txtmrx_tier_rsstd_copay_1m_tier4.Text = lobjDS.Tables[1].Rows[3]["mrx_tier_rsstd_copay_1m"].ToString();
                        txtmrx_tier_rspfd_coins_1m_tier4.Text = lobjDS.Tables[1].Rows[3]["mrx_tier_rspfd_coins_1m"].ToString();
                        txtmrx_tier_rspfd_copay_1m_tier4.Text = lobjDS.Tables[1].Rows[3]["mrx_tier_rspfd_copay_1m"].ToString();

                        txtmrx_tier_oonp_coins_1m_tier4.Text = lobjDS.Tables[1].Rows[3]["mrx_tier_oonp_coins_1m"].ToString();
                        txtmrx_tier_oonp_copay_1m_tier4.Text = lobjDS.Tables[1].Rows[3]["mrx_tier_oonp_copay_1m"].ToString();

                        if (lobjDS.Tables[1].Rows[3]["mrx_tier_gap_cost_share"].ToString() == "")
                        {
                            ddlmrx_tier_gap_cost_share_tier4.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlmrx_tier_gap_cost_share_tier4.SelectedValue = lobjDS.Tables[1].Rows[3]["mrx_tier_gap_cost_share"].ToString();
                        }

                        if (lobjDS.Tables[1].Rows[3]["MRX_TIER_INCLUDES"].ToString() == "")
                        {
                            ddlMRX_TIER_INCLUDES_tier4.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlMRX_TIER_INCLUDES_tier4.SelectedValue = lobjDS.Tables[1].Rows[3]["MRX_TIER_INCLUDES"].ToString();
                        }
                    }
                    else
                    {
                        ddl1mrx_tier_cstshr_struct_type_tier4.SelectedValue = "Select";
                        txtmrx_tier_rstd_coins_1m_tier4.Text = string.Empty;
                        txtmrx_tier_rstd_copay_1m_tier4.Text = string.Empty;
                        txtmrx_tier_rsstd_coins_1m_tier4.Text = string.Empty;
                        txtmrx_tier_rsstd_copay_1m_tier4.Text = string.Empty;
                        txtmrx_tier_rspfd_coins_1m_tier4.Text = string.Empty;
                        txtmrx_tier_rspfd_copay_1m_tier4.Text = string.Empty;
                        txtmrx_tier_oonp_coins_1m_tier4.Text = string.Empty;
                        txtmrx_tier_oonp_copay_1m_tier4.Text = string.Empty;
                        ddlmrx_tier_gap_cost_share_tier4.SelectedValue = "Select";
                        ddlMRX_TIER_INCLUDES_tier4.SelectedValue = "Select";
                    }

                    if (lobjDS.Tables[1].Rows.Count >= 5)
                    {
                        if (lobjDS.Tables[1].Rows[4]["mrx_tier_cstshr_struct_type"].ToString() == "")
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier5.SelectedValue = "Select";
                        }
                        else
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier5.SelectedValue = lobjDS.Tables[1].Rows[4]["mrx_tier_cstshr_struct_type"].ToString();
                        }
                        txtmrx_tier_rstd_coins_1m_tier5.Text = lobjDS.Tables[1].Rows[4]["mrx_tier_rstd_coins_1m"].ToString();
                        txtmrx_tier_rstd_copay_1m_tier5.Text = lobjDS.Tables[1].Rows[4]["mrx_tier_rstd_copay_1m"].ToString();
                        txtmrx_tier_rsstd_coins_1m_tier5.Text = lobjDS.Tables[1].Rows[4]["mrx_tier_rsstd_coins_1m"].ToString();
                        txtmrx_tier_rsstd_copay_1m_tier5.Text = lobjDS.Tables[1].Rows[4]["mrx_tier_rsstd_copay_1m"].ToString();
                        txtmrx_tier_rspfd_coins_1m_tier5.Text = lobjDS.Tables[1].Rows[4]["mrx_tier_rspfd_coins_1m"].ToString();
                        txtmrx_tier_rspfd_copay_1m_tier5.Text = lobjDS.Tables[1].Rows[4]["mrx_tier_rspfd_copay_1m"].ToString();

                        txtmrx_tier_oonp_coins_1m_tier5.Text = lobjDS.Tables[1].Rows[4]["mrx_tier_oonp_coins_1m"].ToString();
                        txtmrx_tier_oonp_copay_1m_tier5.Text = lobjDS.Tables[1].Rows[4]["mrx_tier_oonp_copay_1m"].ToString();

                        if (lobjDS.Tables[1].Rows[4]["mrx_tier_gap_cost_share"].ToString() == "")
                        {
                            ddlmrx_tier_gap_cost_share_tier5.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlmrx_tier_gap_cost_share_tier5.SelectedValue = lobjDS.Tables[1].Rows[4]["mrx_tier_gap_cost_share"].ToString();
                        }

                        if (lobjDS.Tables[1].Rows[4]["MRX_TIER_INCLUDES"].ToString() == "")
                        {
                            ddlMRX_TIER_INCLUDES_tier5.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlMRX_TIER_INCLUDES_tier5.SelectedValue = lobjDS.Tables[1].Rows[4]["MRX_TIER_INCLUDES"].ToString().Trim();
                        }
                    }
                    else
                    {
                        ddl1mrx_tier_cstshr_struct_type_tier5.SelectedValue = "Select";
                        txtmrx_tier_rstd_coins_1m_tier5.Text = string.Empty;
                        txtmrx_tier_rstd_copay_1m_tier5.Text = string.Empty;
                        txtmrx_tier_rsstd_coins_1m_tier5.Text = string.Empty;
                        txtmrx_tier_rsstd_copay_1m_tier5.Text = string.Empty;
                        txtmrx_tier_rspfd_coins_1m_tier5.Text = string.Empty;
                        txtmrx_tier_rspfd_copay_1m_tier5.Text = string.Empty;
                        txtmrx_tier_oonp_coins_1m_tier5.Text = string.Empty;
                        txtmrx_tier_oonp_copay_1m_tier5.Text = string.Empty;
                        ddlmrx_tier_gap_cost_share_tier5.SelectedValue = "Select";
                        ddlMRX_TIER_INCLUDES_tier5.SelectedValue = "Select";

                    }

                    if (lobjDS.Tables[1].Rows.Count >= 6)
                    {
                        if (lobjDS.Tables[1].Rows[5]["mrx_tier_cstshr_struct_type"].ToString() == "")
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier6.SelectedValue = "Select";
                        }
                        else
                        {
                            ddl1mrx_tier_cstshr_struct_type_tier6.SelectedValue = lobjDS.Tables[1].Rows[5]["mrx_tier_cstshr_struct_type"].ToString();
                        }
                        txtmrx_tier_rstd_coins_1m_tier6.Text = lobjDS.Tables[1].Rows[5]["mrx_tier_rstd_coins_1m"].ToString();
                        txtmrx_tier_rstd_copay_1m_tier6.Text = lobjDS.Tables[1].Rows[5]["mrx_tier_rstd_copay_1m"].ToString();
                        txtmrx_tier_rsstd_coins_1m_tier6.Text = lobjDS.Tables[1].Rows[5]["mrx_tier_rsstd_coins_1m"].ToString();
                        txtmrx_tier_rsstd_copay_1m_tier6.Text = lobjDS.Tables[1].Rows[5]["mrx_tier_rsstd_copay_1m"].ToString();
                        txtmrx_tier_rspfd_coins_1m_tier6.Text = lobjDS.Tables[1].Rows[5]["mrx_tier_rspfd_coins_1m"].ToString();
                        txtmrx_tier_rspfd_copay_1m_tier6.Text = lobjDS.Tables[1].Rows[5]["mrx_tier_rspfd_copay_1m"].ToString();

                        txtmrx_tier_oonp_coins_1m_tier6.Text = lobjDS.Tables[1].Rows[5]["mrx_tier_oonp_coins_1m"].ToString();
                        txtmrx_tier_oonp_copay_1m_tier6.Text = lobjDS.Tables[1].Rows[5]["mrx_tier_oonp_copay_1m"].ToString();

                        if (lobjDS.Tables[1].Rows[5]["mrx_tier_gap_cost_share"].ToString() == "")
                        {
                            ddlmrx_tier_gap_cost_share_tier6.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlmrx_tier_gap_cost_share_tier6.SelectedValue = lobjDS.Tables[1].Rows[5]["mrx_tier_gap_cost_share"].ToString();
                        }

                        if (lobjDS.Tables[1].Rows[5]["MRX_TIER_INCLUDES"].ToString() == "")
                        {
                            ddlMRX_TIER_INCLUDES_tier6.SelectedValue = "Select";
                        }
                        else
                        {
                            ddlMRX_TIER_INCLUDES_tier6.SelectedValue = lobjDS.Tables[1].Rows[5]["MRX_TIER_INCLUDES"].ToString();
                        }

                    }
                    else
                    {
                        ddl1mrx_tier_cstshr_struct_type_tier6.SelectedValue = "Select";
                        txtmrx_tier_rstd_coins_1m_tier6.Text = string.Empty;
                        txtmrx_tier_rstd_copay_1m_tier6.Text = string.Empty;
                        txtmrx_tier_rsstd_coins_1m_tier6.Text = string.Empty;
                        txtmrx_tier_rsstd_copay_1m_tier6.Text = string.Empty;
                        txtmrx_tier_rspfd_coins_1m_tier6.Text = string.Empty;
                        txtmrx_tier_rspfd_copay_1m_tier6.Text = string.Empty;
                        txtmrx_tier_oonp_coins_1m_tier6.Text = string.Empty;
                        txtmrx_tier_oonp_copay_1m_tier6.Text = string.Empty;
                        ddlmrx_tier_gap_cost_share_tier6.SelectedValue = "Select";
                        ddlMRX_TIER_INCLUDES_tier6.SelectedValue = "Select";
                    }
                }
                else
                {
                    ddl1mrx_tier_cstshr_struct_type_tier1.SelectedValue = "Select";
                    txtmrx_tier_rstd_coins_1m_tier1.Text = string.Empty;
                    txtmrx_tier_rstd_copay_1m_tier1.Text = string.Empty;
                    txtmrx_tier_rsstd_coins_1m_tier1.Text = string.Empty;
                    txtmrx_tier_rsstd_copay_1m_tier1.Text = string.Empty;
                    txtmrx_tier_rspfd_coins_1m_tier1.Text = string.Empty;
                    txtmrx_tier_rspfd_copay_1m_tier1.Text = string.Empty;
                    txtmrx_tier_oonp_coins_1m_tier1.Text = string.Empty;
                    txtmrx_tier_oonp_copay_1m_tier1.Text = string.Empty;
                    ddlmrx_tier_gap_cost_share_tier1.SelectedValue = "Select";
                    ddlMRX_TIER_INCLUDES_tier1.SelectedValue = "Select";


                    ddl1mrx_tier_cstshr_struct_type_tier2.SelectedValue = "Select";
                    txtmrx_tier_rstd_coins_1m_tier2.Text = string.Empty;
                    txtmrx_tier_rstd_copay_1m_tier2.Text = string.Empty;
                    txtmrx_tier_rsstd_coins_1m_tier2.Text = string.Empty;
                    txtmrx_tier_rsstd_copay_1m_tier2.Text = string.Empty;
                    txtmrx_tier_rspfd_coins_1m_tier2.Text = string.Empty;
                    txtmrx_tier_rspfd_copay_1m_tier2.Text = string.Empty;
                    txtmrx_tier_oonp_coins_1m_tier2.Text = string.Empty;
                    txtmrx_tier_oonp_copay_1m_tier2.Text = string.Empty;
                    ddlmrx_tier_gap_cost_share_tier2.SelectedValue = "Select";
                    ddlMRX_TIER_INCLUDES_tier2.SelectedValue = "Select";


                    ddl1mrx_tier_cstshr_struct_type_tier3.SelectedValue = "Select";
                    txtmrx_tier_rstd_coins_1m_tier3.Text = string.Empty;
                    txtmrx_tier_rstd_copay_1m_tier3.Text = string.Empty;
                    txtmrx_tier_rsstd_coins_1m_tier3.Text = string.Empty;
                    txtmrx_tier_rsstd_copay_1m_tier3.Text = string.Empty;
                    txtmrx_tier_rspfd_coins_1m_tier3.Text = string.Empty;
                    txtmrx_tier_rspfd_copay_1m_tier3.Text = string.Empty;
                    txtmrx_tier_oonp_coins_1m_tier3.Text = string.Empty;
                    txtmrx_tier_oonp_copay_1m_tier3.Text = string.Empty;
                    ddlmrx_tier_gap_cost_share_tier3.SelectedValue = "Select";
                    ddlMRX_TIER_INCLUDES_tier3.SelectedValue = "Select";


                    ddl1mrx_tier_cstshr_struct_type_tier4.SelectedValue = "Select";
                    txtmrx_tier_rstd_coins_1m_tier4.Text = string.Empty;
                    txtmrx_tier_rstd_copay_1m_tier4.Text = string.Empty;
                    txtmrx_tier_rsstd_coins_1m_tier4.Text = string.Empty;
                    txtmrx_tier_rsstd_copay_1m_tier4.Text = string.Empty;
                    txtmrx_tier_rspfd_coins_1m_tier4.Text = string.Empty;
                    txtmrx_tier_rspfd_copay_1m_tier4.Text = string.Empty;
                    txtmrx_tier_oonp_coins_1m_tier4.Text = string.Empty;
                    txtmrx_tier_oonp_copay_1m_tier4.Text = string.Empty;
                    ddlmrx_tier_gap_cost_share_tier4.SelectedValue = "Select";
                    ddlMRX_TIER_INCLUDES_tier4.SelectedValue = "Select";

                    ddl1mrx_tier_cstshr_struct_type_tier5.SelectedValue = "Select";
                    txtmrx_tier_rstd_coins_1m_tier5.Text = string.Empty;
                    txtmrx_tier_rstd_copay_1m_tier5.Text = string.Empty;
                    txtmrx_tier_rsstd_coins_1m_tier5.Text = string.Empty;
                    txtmrx_tier_rsstd_copay_1m_tier5.Text = string.Empty;
                    txtmrx_tier_rspfd_coins_1m_tier5.Text = string.Empty;
                    txtmrx_tier_rspfd_copay_1m_tier5.Text = string.Empty;
                    txtmrx_tier_oonp_coins_1m_tier5.Text = string.Empty;
                    txtmrx_tier_oonp_copay_1m_tier5.Text = string.Empty;
                    ddlmrx_tier_gap_cost_share_tier5.SelectedValue = "Select";
                    ddlMRX_TIER_INCLUDES_tier5.SelectedValue = "Select";


                    ddl1mrx_tier_cstshr_struct_type_tier6.SelectedValue = "Select";
                    txtmrx_tier_rstd_coins_1m_tier6.Text = string.Empty;
                    txtmrx_tier_rstd_copay_1m_tier6.Text = string.Empty;
                    txtmrx_tier_rsstd_coins_1m_tier6.Text = string.Empty;
                    txtmrx_tier_rsstd_copay_1m_tier6.Text = string.Empty;
                    txtmrx_tier_rspfd_coins_1m_tier6.Text = string.Empty;
                    txtmrx_tier_rspfd_copay_1m_tier6.Text = string.Empty;
                    txtmrx_tier_oonp_coins_1m_tier6.Text = string.Empty;
                    txtmrx_tier_oonp_copay_1m_tier6.Text = string.Empty;
                    ddlmrx_tier_gap_cost_share_tier6.SelectedValue = "Select";
                    ddlMRX_TIER_INCLUDES_tier6.SelectedValue = "Select";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bind_Disabled()
        {
            txtpbp_b1a_copay_mcs_amt_int1_t1.Enabled = false;
            txtpbp_b1a_copay_mcs_amt_int2_t1.Enabled = false;
            txtpbp_b1a_copay_mcs_amt_int3_t1.Enabled = false;

            txtpbp_b1a_coins_mcs_pct_int1_t1.Enabled = false;
            txtpbp_b1a_coins_mcs_pct_int2_t1.Enabled = false;
            txtpbp_b1a_coins_mcs_pct_int3_t1.Enabled = false;

            txtpbp_b9a_coins_ohs_pct_min.Enabled = false;
            txtpbp_b9a_copay_ohs_amt_min.Enabled = false;
            txtpbp_b9b_coins_pct_mc.Enabled = false;
            txtpbp_b9b_copay_mc_amt.Enabled = false;

            txtpbp_b16a_coins_pct_oe.Enabled = false;
            txtpbp_b16a_copay_amt_oemin.Enabled = false;
            txtpbp_b16a_coins_pct_dx.Enabled = false;
            txtpbp_b16a_copay_amt_dxmin.Enabled = false;
            txtpbp_b16a_coins_pct_pc.Enabled = false;
            txtpbp_b16a_copay_amt_pcmin.Enabled = false;

            txtpbp_b16b_coins_pct_rs_min.Enabled = false;
            txtpbp_b16b_copay_amt_rs_min.Enabled = false;
            txtpbp_b16b_coins_pct_end_min.Enabled = false;
            txtpbp_b16b_copay_amt_end_min.Enabled = false;
            txtpbp_b16b_coins_pct_peri_min.Enabled = false;
            txtpbp_b16b_copay_amt_peri_min.Enabled = false;
            txtpbp_b16b_coins_pct_ext_min.Enabled = false;
            txtpbp_b16b_copay_amt_ext_min.Enabled = false;
            txtpbp_b16b_coins_pct_poo_min.Enabled = false;
            txtpbp_b16b_copay_amt_poo_min.Enabled = false;

            txtpbp_b8b_coins_pct_cmc.Enabled = false;
            txtpbp_b8b_coins_pct_cmc_max.Enabled = false;
            txtpbp_b8b_copay_mc_amt.Enabled = false;
            txtpbp_b8b_copay_mc_amt_max.Enabled = false;
            txtpbp_b8b_coins_pct_tmc.Enabled = false;
            txtpbp_b8b_coins_pct_tmc_max.Enabled = false;
            txtpbp_b8b_copay_amt_tmc.Enabled = false;
            txtpbp_b8b_copay_amt_tmc_max.Enabled = false;
            txtpbp_b8b_coins_pct_drs.Enabled = false;
            txtpbp_b8b_coins_pct_drs_max.Enabled = false;
            txtpbp_b8b_copay_amt_drs.Enabled = false;
            txtpbp_b8b_copay_amt_drs_max.Enabled = false;
        }

        private void Bind_Benefitperiod()
        {
            try
            {
                SqlCommand lobjCmd = new SqlCommand("Get_BenefitPeriod", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
                DataSet ds = new DataSet();
                sd.Fill(ds);

                lobjCon.Close();

                ddlpbp_b16a_maxplan_per.DataSource = ds;
                ddlpbp_b16a_maxplan_per.DataTextField = "Periodicity";
                ddlpbp_b16a_maxplan_per.DataValueField = "BenefitID";
                ddlpbp_b16a_maxplan_per.DataBind();
                ddlpbp_b16a_maxplan_per.Items.Insert(0, "Select");

                ddlpbp_b16a_bendesc_per_oe.DataSource = ds;
                ddlpbp_b16a_bendesc_per_oe.DataTextField = "Periodicity";
                ddlpbp_b16a_bendesc_per_oe.DataValueField = "BenefitID";
                ddlpbp_b16a_bendesc_per_oe.DataBind();
                ddlpbp_b16a_bendesc_per_oe.Items.Insert(0, "Select");

                ddlpbp_b16a_bendesc_per_dx.DataSource = ds;
                ddlpbp_b16a_bendesc_per_dx.DataTextField = "Periodicity";
                ddlpbp_b16a_bendesc_per_dx.DataValueField = "BenefitID";
                ddlpbp_b16a_bendesc_per_dx.DataBind();
                ddlpbp_b16a_bendesc_per_dx.Items.Insert(0, "Select");

                ddlpbp_b16a_bendesc_per_pc.DataSource = ds;
                ddlpbp_b16a_bendesc_per_pc.DataTextField = "Periodicity";
                ddlpbp_b16a_bendesc_per_pc.DataValueField = "BenefitID";
                ddlpbp_b16a_bendesc_per_pc.DataBind();
                ddlpbp_b16a_bendesc_per_pc.Items.Insert(0, "Select");

                ddlpbp_b16b_bendesc_per_rs.DataSource = ds;
                ddlpbp_b16b_bendesc_per_rs.DataTextField = "Periodicity";
                ddlpbp_b16b_bendesc_per_rs.DataValueField = "BenefitID";
                ddlpbp_b16b_bendesc_per_rs.DataBind();
                ddlpbp_b16b_bendesc_per_rs.Items.Insert(0, "Select");

                ddlpbp_b16b_bendesc_per_end.DataSource = ds;
                ddlpbp_b16b_bendesc_per_end.DataTextField = "Periodicity";
                ddlpbp_b16b_bendesc_per_end.DataValueField = "BenefitID";
                ddlpbp_b16b_bendesc_per_end.DataBind();
                ddlpbp_b16b_bendesc_per_end.Items.Insert(0, "Select");

                ddlpbp_b16b_bendesc_per_peri.DataSource = ds;
                ddlpbp_b16b_bendesc_per_peri.DataTextField = "Periodicity";
                ddlpbp_b16b_bendesc_per_peri.DataValueField = "BenefitID";
                ddlpbp_b16b_bendesc_per_peri.DataBind();
                ddlpbp_b16b_bendesc_per_peri.Items.Insert(0, "Select");

                ddlpbp_b16b_bendesc_per_ext.DataSource = ds;
                ddlpbp_b16b_bendesc_per_ext.DataTextField = "Periodicity";
                ddlpbp_b16b_bendesc_per_ext.DataValueField = "BenefitID";
                ddlpbp_b16b_bendesc_per_ext.DataBind();
                ddlpbp_b16b_bendesc_per_ext.Items.Insert(0, "Select");

                ddlpbp_b16b_bendesc_per_poo.DataSource = ds;
                ddlpbp_b16b_bendesc_per_poo.DataTextField = "Periodicity";
                ddlpbp_b16b_bendesc_per_poo.DataValueField = "BenefitID";
                ddlpbp_b16b_bendesc_per_poo.DataBind();
                ddlpbp_b16b_bendesc_per_poo.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bind_BenefitType()
        {
            try
            {
                SqlCommand lobjCmd = new SqlCommand("Get_BenefitType", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
                DataSet ds = new DataSet();
                sd.Fill(ds);

                lobjCon.Close();

                ddlpbp_b16a_bendesc_amo_oe.DataSource = ds;
                ddlpbp_b16a_bendesc_amo_oe.DataTextField = "BenefitType";
                ddlpbp_b16a_bendesc_amo_oe.DataValueField = "BenefitTypeID";
                ddlpbp_b16a_bendesc_amo_oe.DataBind();
                ddlpbp_b16a_bendesc_amo_oe.Items.Insert(0, "Select");

                ddlpbp_b16a_bendesc_amo_dx.DataSource = ds;
                ddlpbp_b16a_bendesc_amo_dx.DataTextField = "BenefitType";
                ddlpbp_b16a_bendesc_amo_dx.DataValueField = "BenefitTypeID";
                ddlpbp_b16a_bendesc_amo_dx.DataBind();
                ddlpbp_b16a_bendesc_amo_dx.Items.Insert(0, "Select");

                ddlPBP_B16A_BENDESC_AMO_PC.DataSource = ds;
                ddlPBP_B16A_BENDESC_AMO_PC.DataTextField = "BenefitType";
                ddlPBP_B16A_BENDESC_AMO_PC.DataValueField = "BenefitTypeID";
                ddlPBP_B16A_BENDESC_AMO_PC.DataBind();
                ddlPBP_B16A_BENDESC_AMO_PC.Items.Insert(0, "Select");

                ddlPBP_B16B_BENDESC_AMO_RS.DataSource = ds;
                ddlPBP_B16B_BENDESC_AMO_RS.DataTextField = "BenefitType";
                ddlPBP_B16B_BENDESC_AMO_RS.DataValueField = "BenefitTypeID";
                ddlPBP_B16B_BENDESC_AMO_RS.DataBind();
                ddlPBP_B16B_BENDESC_AMO_RS.Items.Insert(0, "Select");


                ddlPBP_B16B_BENDESC_AMO_END.DataSource = ds;
                ddlPBP_B16B_BENDESC_AMO_END.DataTextField = "BenefitType";
                ddlPBP_B16B_BENDESC_AMO_END.DataValueField = "BenefitTypeID";
                ddlPBP_B16B_BENDESC_AMO_END.DataBind();
                ddlPBP_B16B_BENDESC_AMO_END.Items.Insert(0, "Select");

                ddlPBP_B16B_BENDESC_AMO_PERI.DataSource = ds;
                ddlPBP_B16B_BENDESC_AMO_PERI.DataTextField = "BenefitType";
                ddlPBP_B16B_BENDESC_AMO_PERI.DataValueField = "BenefitTypeID";
                ddlPBP_B16B_BENDESC_AMO_PERI.DataBind();
                ddlPBP_B16B_BENDESC_AMO_PERI.Items.Insert(0, "Select");


                ddlPBP_B16B_BENDESC_AMO_EXT.DataSource = ds;
                ddlPBP_B16B_BENDESC_AMO_EXT.DataTextField = "BenefitType";
                ddlPBP_B16B_BENDESC_AMO_EXT.DataValueField = "BenefitTypeID";
                ddlPBP_B16B_BENDESC_AMO_EXT.DataBind();
                ddlPBP_B16B_BENDESC_AMO_EXT.Items.Insert(0, "Select");


                ddlPBP_B16B_BENDESC_AMO_POO.DataSource = ds;
                ddlPBP_B16B_BENDESC_AMO_POO.DataTextField = "BenefitType";
                ddlPBP_B16B_BENDESC_AMO_POO.DataValueField = "BenefitTypeID";
                ddlPBP_B16B_BENDESC_AMO_POO.DataBind();
                ddlPBP_B16B_BENDESC_AMO_POO.Items.Insert(0, "Select");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Bind_BenefitCoverageType()
        {
            try
            {
                SqlCommand lobjCmd = new SqlCommand("Get_BenefitCoverageType", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
                DataSet ds = new DataSet();
                sd.Fill(ds);
                lobjCon.Close();
                ddlpbp_b16b_maxbene_type.DataSource = ds;
                ddlpbp_b16b_maxbene_type.DataTextField = "BenefitcoverageType";
                ddlpbp_b16b_maxbene_type.DataValueField = "BenefitCoverageTypeID";
                ddlpbp_b16b_maxbene_type.DataBind();
                ddlpbp_b16b_maxbene_type.Items.Insert(0, "Select");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bind_HospitalAcute()
        {
            try
            {
                SqlCommand lobjCmd = new SqlCommand("Get_Inpatient", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCon.Open();
                SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
                DataSet ds = new DataSet();
                sd.Fill(ds);
                ddlpbp_b1a_hosp_ben_period.DataSource = ds;
                ddlpbp_b1a_hosp_ben_period.DataTextField = "Inpatient";
                ddlpbp_b1a_hosp_ben_period.DataValueField = "InpatientID";
                ddlpbp_b1a_hosp_ben_period.DataBind();
                ddlpbp_b1a_hosp_ben_period.Items.Insert(0, "Select");
                lobjCon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void ExportFiles()
        {
            string usrname = Session["UserName"].ToString();
            usrname = usrname.Split('@')[0];
            usrname = usrname.Replace(".", "");
            Session["usrName"] = usrname;
            string Quick = "/QuickAccess_Inputs_" + usrname + ".txt";
            string mrx = "/MRX_Inputs_" + usrname + ".txt";
            string mrxtier = "/MRX_Tier_Inputs_" + usrname + ".txt";

            lobjCmd = new SqlCommand("Download_QuickAccess", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@sId ", Convert.ToInt32(Session["sId"].ToString()));
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);

            string dirPath = "~/Files";
            string strFilePathQA = Server.MapPath(dirPath + Quick);
            string strFilePathMRX = Server.MapPath(dirPath + mrx);
            string strFilePathMRXTier = Server.MapPath(dirPath + mrxtier);


            if (File.Exists(strFilePathQA))
            {
                File.Delete(strFilePathQA);
            }

            if (File.Exists(strFilePathMRX))
            {
                File.Delete(strFilePathMRX);
            }

            if (File.Exists(strFilePathMRXTier))
            {
                File.Delete(strFilePathMRXTier);
            }

            DataTable dt1 = lobjDS.Tables[0];
            StringBuilder sb1 = new StringBuilder();
            DataTable dt2 = lobjDS.Tables[1];
            StringBuilder sb2 = new StringBuilder();
            DataTable dt3 = lobjDS.Tables[2];
            StringBuilder sb3 = new StringBuilder();

            // Write into  Csv for QuickAccess
            if (!File.Exists(strFilePathQA))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(dirPath));
                IEnumerable<string> columnNames1 = dt1.Columns.Cast<DataColumn>().
                                             Select(column => "\"" + column.ColumnName + "\"");

                sb1.AppendLine(string.Join("\t", columnNames1));
                File.WriteAllText(strFilePathQA, sb1.ToString());
                sb1.Clear();
            }

            foreach (DataRow row in dt1.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => "\"" + field.ToString() + "\"");
                sb1.AppendLine(string.Join("\t", fields));
            }
            File.AppendAllText(strFilePathQA, sb1.ToString());

            // Write into  Csv for MRX
            if (!File.Exists(strFilePathMRX))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(dirPath));
                IEnumerable<string> columnNames2 = dt2.Columns.Cast<DataColumn>().
                                           Select(column => "\"" + column.ColumnName + "\"");
                sb2.AppendLine(string.Join("\t", columnNames2));
                File.WriteAllText(strFilePathMRX, sb2.ToString());
                sb2.Clear();
            }
            foreach (DataRow row in dt2.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => "\"" + field.ToString() + "\"");
                sb2.AppendLine(string.Join("\t", fields));
            }
            File.AppendAllText(strFilePathMRX, sb2.ToString());

            // Write into  Csv for MRXTier
            if (!File.Exists(strFilePathMRXTier))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(dirPath));
                IEnumerable<string> columnNames3 = dt3.Columns.Cast<DataColumn>().
                                            Select(column => "\"" + column.ColumnName + "\"");
                sb3.AppendLine(string.Join("\t", columnNames3));
                File.WriteAllText(strFilePathMRXTier, sb3.ToString());
                sb3.Clear();
            }
            foreach (DataRow row in dt3.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => "\"" + field.ToString() + "\"");
                sb3.AppendLine(string.Join("\t", fields));
            }
            File.AppendAllText(strFilePathMRXTier, sb3.ToString());
        }

        private void SaveData()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_QuickAccess", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@bid_id", Session["SelectedBidId"].ToString());
            //Inpatient Hospital Acute Services 
            if (ddlpbp_b1a_hosp_ben_period.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_hosp_ben_period", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_hosp_ben_period", ddlpbp_b1a_hosp_ben_period.SelectedValue);
            }

            if (ddlpbp_b1a_cost_discharge_yn.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_cost_discharge_yn", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_cost_discharge_yn", ddlpbp_b1a_cost_discharge_yn.SelectedValue);
            }


            if (rd_HospitalAcute.SelectedValue == "Coinsurance")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_yn", 1);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_bgnd_int1_t1", txtBegin1Acute.Text);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_bgnd_int2_t1", txtBegin2Acute.Text);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_bgnd_int3_t1", txtBegin3Acute.Text);

                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_endd_int1_t1", txtEnd1Acute.Text);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_endd_int2_t1", txtEnd2Acute.Text);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_endd_int3_t1", txtEnd3Acute.Text);
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_yn", 2);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_bgnd_int1_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_bgnd_int2_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_bgnd_int3_t1", "");

                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_endd_int1_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_endd_int2_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_endd_int3_t1", "");
            }

            if (rd_HospitalAcute.SelectedValue == "Copayment")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_yn", 1);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int1_t1", txtBegin1Acute.Text);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int2_t1", txtBegin2Acute.Text);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int3_t1", txtBegin3Acute.Text);

                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int1_t1", txtEnd1Acute.Text);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int2_t1", txtEnd2Acute.Text);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int3_t1", txtEnd3Acute.Text);
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_yn", 2);
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int1_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int2_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int3_t1", "");

                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int1_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int2_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int3_t1", "");
            }

            lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_pct_int1_t1", txtpbp_b1a_coins_mcs_pct_int1_t1.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_pct_int2_t1", txtpbp_b1a_coins_mcs_pct_int2_t1.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_pct_int3_t1", txtpbp_b1a_coins_mcs_pct_int3_t1.Text);

            lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int1_t1", txtpbp_b1a_copay_mcs_amt_int1_t1.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int2_t1", txtpbp_b1a_copay_mcs_amt_int2_t1.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int3_t1", txtpbp_b1a_copay_mcs_amt_int3_t1.Text);

            lobjCmd.Parameters.AddWithValue("@pbp_b1a_coins_mcs_pct_t1", txtpbp_b1a_coins_mcs_pct_t1.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_t1", txtpbp_b1a_copay_mcs_amt_t1.Text);

            if (ddlpbp_b1a_mc_copay_cstshr_yn_t1.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_mc_coins_cstshr_yn_t1", "");
                lobjCmd.Parameters.AddWithValue("@pbp_b1a_mc_copay_cstshr_yn_t1", "");
            }
            else
            {
                if (rd_HospitalAcute.SelectedValue == "Coinsurance")
                {
                    lobjCmd.Parameters.AddWithValue("@pbp_b1a_mc_coins_cstshr_yn_t1", ddlpbp_b1a_mc_copay_cstshr_yn_t1.SelectedValue);
                    lobjCmd.Parameters.AddWithValue("@pbp_b1a_mc_copay_cstshr_yn_t1", "");
                }
                else
                {
                    lobjCmd.Parameters.AddWithValue("@pbp_b1a_mc_coins_cstshr_yn_t1", "");
                    lobjCmd.Parameters.AddWithValue("@pbp_b1a_mc_copay_cstshr_yn_t1", ddlpbp_b1a_mc_copay_cstshr_yn_t1.SelectedValue);
                }
            }

            //Outpatient Hospital Services 

            List<String> OutPatientCoin = new List<string>();

            if (chk_Observ_coin.Checked == true)
            {
                OutPatientCoin.Add("1");
            }
            else
            {
                OutPatientCoin.Add("0");
            }

            if (chk_Surgery_coin.Checked == true)
            {
                OutPatientCoin.Add("1");
            }
            else
            {
                OutPatientCoin.Add("0");
            }

            string selectedOutPatientCoin = string.Empty;
            var CoinCollection = OutPatientCoin;
            foreach (var item in CoinCollection)
            {
                selectedOutPatientCoin += item.ToString();
            }

            if (selectedOutPatientCoin == "00")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b9a_coins_ehc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b9a_coins_ehc", selectedOutPatientCoin);
            }

            if (chk_pbp_b9b_coins_yn.Checked == true)
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b9b_coins_yn", 1);
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b9b_coins_yn", 2);
            }


            lobjCmd.Parameters.AddWithValue("@pbp_b9a_coins_ohs_pct_min", txtpbp_b9a_coins_ohs_pct_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b9b_coins_pct_mc", txtpbp_b9b_coins_pct_mc.Text);

            List<String> OutPatientCopay = new List<string>();

            if (chk_Observ_copay.Checked == true)
            {
                OutPatientCopay.Add("1");
            }
            else
            {
                OutPatientCopay.Add("0");
            }
            if (chk_Surgery_copay.Checked == true)
            {
                OutPatientCopay.Add("1");
            }
            else
            {
                OutPatientCopay.Add("0");
            }

            string selectedOutPatientCopay = string.Empty;
            var CopayCollection = OutPatientCopay;
            foreach (var item1 in CopayCollection)
            {
                selectedOutPatientCopay += item1.ToString();
            }

            if (selectedOutPatientCopay == "00")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b9a_copay_ehc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b9a_copay_ehc", selectedOutPatientCopay);
            }

            if (chk_Ambulatory_copay.Checked == true)
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b9b_copay_yn", 1);
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b9b_copay_yn", 2);
            }

            lobjCmd.Parameters.AddWithValue("@pbp_b9a_copay_ohs_amt_min", txtpbp_b9a_copay_ohs_amt_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b9b_copay_mc_amt", txtpbp_b9b_copay_mc_amt.Text);

            //Preventive Dental Service

            lobjCmd.Parameters.AddWithValue("@pbp_b16a_maxplan_amt", txtpbp_b16a_maxplan_amt.Text);

            if (ddlpbp_b16a_maxplan_per.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_maxplan_per", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_maxplan_per", ddlpbp_b16a_maxplan_per.SelectedValue);
            }


            List<String> PreventiveCoin = new List<string>();

            if (chck_Denatl_Coin.Checked == true)
            {
                PreventiveCoin.Add("1");
            }
            else
            {
                PreventiveCoin.Add("0");
            }
            if (chck_Oral_Coin.Checked == true)
            {
                PreventiveCoin.Add("1");
            }
            else
            {
                PreventiveCoin.Add("0");
            }

            if (chck_Prophylaxis_Coin.Checked == true)
            {
                PreventiveCoin.Add("1");
            }
            else
            {
                PreventiveCoin.Add("0");
            }

            if (Session["pbp_b16a_coins_ehc"] == null)
            {
                PreventiveCoin.Add("0");
            }
            else
            {
                string getprevcoin = Session["pbp_b16a_coins_ehc"].ToString();
                PreventiveCoin.Add(getprevcoin);
            }

            string selectedPreventiveCoin = string.Empty;
            var PreventiveCoincollection = PreventiveCoin;
            foreach (var item1 in PreventiveCoincollection)
            {
                selectedPreventiveCoin += item1.ToString();
            }

            if (selectedPreventiveCoin == "0000")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_coins_ehc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_coins_ehc", selectedPreventiveCoin);
            }

            lobjCmd.Parameters.AddWithValue("@pbp_b16a_coins_pct_oe", txtpbp_b16a_coins_pct_oe.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16a_coins_pct_dx", txtpbp_b16a_coins_pct_dx.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16a_coins_pct_pc", txtpbp_b16a_coins_pct_pc.Text);

            List<String> PreventiveCopay = new List<string>();

            if (chck_Denatl_Copay.Checked == true)
            {
                PreventiveCopay.Add("1");
            }
            else
            {
                PreventiveCopay.Add("0");
            }
            if (chck_Oral_Copay.Checked == true)
            {
                PreventiveCopay.Add("1");
            }
            else
            {
                PreventiveCopay.Add("0");
            }

            if (chck_Prophylaxis_Copay.Checked == true)
            {
                PreventiveCopay.Add("1");
            }
            else
            {
                PreventiveCopay.Add("0");
            }

            if (Session["pbp_b16a_copay_ehc"] == null)
            {
                PreventiveCopay.Add("0");
            }
            else
            {
                string getprevcopay = Session["pbp_b16a_copay_ehc"].ToString();
                PreventiveCopay.Add(getprevcopay);
            }
            string selectedPreventiveCopay = string.Empty;
            var PreventiveCopaycollection = PreventiveCopay;
            foreach (var item1 in PreventiveCopaycollection)
            {
                selectedPreventiveCopay += item1.ToString();
            }

            if (selectedPreventiveCopay == "0000")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_copay_ehc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_copay_ehc", selectedPreventiveCopay);
            }

            lobjCmd.Parameters.AddWithValue("@pbp_b16a_copay_amt_oemin", txtpbp_b16a_copay_amt_oemin.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16a_copay_amt_dxmin", txtpbp_b16a_copay_amt_dxmin.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16a_copay_amt_pcmin", txtpbp_b16a_copay_amt_pcmin.Text);

            lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_oe", txtpbp_b16a_bendesc_numv_oe.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_dx", txtpbp_b16a_bendesc_numv_dx.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_pc", txtpbp_b16a_bendesc_numv_pc.Text);

            if (ddlpbp_b16a_bendesc_per_oe.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_per_oe", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_per_oe", ddlpbp_b16a_bendesc_per_oe.SelectedValue);
            }

            if (ddlpbp_b16a_bendesc_per_dx.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_per_dx", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_per_dx", ddlpbp_b16a_bendesc_per_dx.SelectedValue);
            }

            if (ddlpbp_b16a_bendesc_per_pc.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_per_pc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_per_pc", ddlpbp_b16a_bendesc_per_pc.SelectedValue);
            }

            if (ddlpbp_b16a_bendesc_amo_oe.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_amo_oe", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_amo_oe", ddlpbp_b16a_bendesc_amo_oe.SelectedValue);
            }

            if (ddlpbp_b16a_bendesc_amo_dx.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_amo_dx", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16a_bendesc_amo_dx", ddlpbp_b16a_bendesc_amo_dx.SelectedValue);
            }

            if (ddlPBP_B16A_BENDESC_AMO_PC.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16A_BENDESC_AMO_PC", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16A_BENDESC_AMO_PC", ddlPBP_B16A_BENDESC_AMO_PC.SelectedValue);
            }


            if (ddlpbp_b16b_maxbene_type.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_maxbene_type", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_maxbene_type", ddlpbp_b16b_maxbene_type.SelectedValue);
            }


            //Comprehensive Dental Service

            List<String> ComprehensiveCoin = new List<string>();

            if (chk_Prosthodontics_coin.Checked == true)
            {
                ComprehensiveCoin.Add("1");
            }
            else
            {
                ComprehensiveCoin.Add("0");
            }


            if (chk_Medicare_Coin.Checked == true)
            {
                ComprehensiveCoin.Add("1");
            }
            else
            {
                ComprehensiveCoin.Add("0");
            }

            if (Session["pbp_b16b_coins_ehc"] == null)
            {
                ComprehensiveCoin.Add("00");
            }
            else
            {
                string getcoin = Session["pbp_b16b_coins_ehc"].ToString();
                ComprehensiveCoin.Add(getcoin);
            }


            if (chk_Restorative_Coin.Checked == true)
            {
                ComprehensiveCoin.Add("1");
            }
            else
            {
                ComprehensiveCoin.Add("0");
            }

            if (chk_Endodontics_Coin.Checked == true)
            {
                ComprehensiveCoin.Add("1");
            }
            else
            {
                ComprehensiveCoin.Add("0");
            }

            if (chk_Periodontics_coin.Checked == true)
            {
                ComprehensiveCoin.Add("1");
            }
            else
            {
                ComprehensiveCoin.Add("0");
            }

            if (chk_Extractions_coin.Checked == true)
            {
                ComprehensiveCoin.Add("1");
            }
            else
            {
                ComprehensiveCoin.Add("0");
            }

            string selectedComprehensiveCoin = string.Empty;
            var Comprehensivecollection = ComprehensiveCoin;
            foreach (var item1 in Comprehensivecollection)
            {
                selectedComprehensiveCoin += item1.ToString();
            }

            if (selectedComprehensiveCoin == "00000000")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_coins_ehc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_coins_ehc", selectedComprehensiveCoin);
            }

            lobjCmd.Parameters.AddWithValue("@PBP_B16B_COINS_PCT_MC_MIN", txtPBP_B16B_COINS_PCT_MC_MIN.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_coins_pct_rs_min", txtpbp_b16b_coins_pct_rs_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_coins_pct_end_min", txtpbp_b16b_coins_pct_end_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_coins_pct_peri_min", txtpbp_b16b_coins_pct_peri_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_coins_pct_ext_min", txtpbp_b16b_coins_pct_ext_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_coins_pct_poo_min", txtpbp_b16b_coins_pct_poo_min.Text);

            List<String> ComprehensiveCopay = new List<string>();

            if (chk_Prosthodontics_copay.Checked == true)
            {
                ComprehensiveCopay.Add("1");
            }
            else
            {
                ComprehensiveCopay.Add("0");
            }
            if (chk_Medicare_Copay.Checked == true)
            {
                ComprehensiveCopay.Add("1");
            }
            else
            {
                ComprehensiveCopay.Add("0");
            }
            if (Session["getpbp_b16b_copay_ehc"] == null)
            {
                ComprehensiveCopay.Add("00");
            }
            else
            {
                string getcopay = Session["getpbp_b16b_copay_ehc"].ToString();
                ComprehensiveCopay.Add(getcopay.ToString());
            }
            if (chk_Restorative_Copay.Checked == true)
            {
                ComprehensiveCopay.Add("1");
            }
            else
            {
                ComprehensiveCopay.Add("0");
            }

            if (chk_Endodontics_Copay.Checked == true)
            {
                ComprehensiveCopay.Add("1");
            }
            else
            {
                ComprehensiveCopay.Add("0");
            }

            if (chk_Periodontics_copay.Checked == true)
            {
                ComprehensiveCopay.Add("1");
            }
            else
            {
                ComprehensiveCopay.Add("0");
            }

            if (chk_Extractions_copay.Checked == true)
            {
                ComprehensiveCopay.Add("1");
            }
            else
            {
                ComprehensiveCopay.Add("0");
            }



            string selectedComprehensiveCopay = string.Empty;
            var ComprehensiveCopaycollection = ComprehensiveCopay;
            foreach (var item1 in ComprehensiveCopaycollection)
            {
                selectedComprehensiveCopay += item1.ToString();
            }

            if (selectedComprehensiveCopay == "00000000")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_copay_ehc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_copay_ehc", selectedComprehensiveCopay);
            }

            lobjCmd.Parameters.AddWithValue("@PBP_B16B_COPAY_AMT_MC_MIN", txtPBP_B16B_COPAY_AMT_MC_MIN.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_copay_amt_rs_min", txtpbp_b16b_copay_amt_rs_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_copay_amt_end_min", txtpbp_b16b_copay_amt_end_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_copay_amt_peri_min", txtpbp_b16b_copay_amt_peri_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_copay_amt_ext_min", txtpbp_b16b_copay_amt_ext_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_copay_amt_poo_min", txtpbp_b16b_copay_amt_poo_min.Text);

            lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_numv_rs", txtpbp_b16b_bendesc_numv_rs.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_num_end", txtpbp_b16b_bendesc_num_end.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_num_peri", txtpbp_b16b_bendesc_num_peri.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_num_ext", txtpbp_b16b_bendesc_num_ext.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_numv_poo", txtpbp_b16b_bendesc_numv_poo.Text);

            if (ddlpbp_b16b_bendesc_per_rs.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_rs", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_rs", ddlpbp_b16b_bendesc_per_rs.SelectedValue);
            }

            if (ddlpbp_b16b_bendesc_per_end.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_end", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_end", ddlpbp_b16b_bendesc_per_end.SelectedValue);
            }

            if (ddlpbp_b16b_bendesc_per_peri.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_peri", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_peri", ddlpbp_b16b_bendesc_per_peri.SelectedValue);
            }

            if (ddlpbp_b16b_bendesc_per_ext.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_ext", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_ext", ddlpbp_b16b_bendesc_per_ext.SelectedValue);
            }

            if (ddlpbp_b16b_bendesc_per_poo.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_poo", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b16b_bendesc_per_poo", ddlpbp_b16b_bendesc_per_poo.SelectedValue);
            }


            if (ddlPBP_B16B_BENDESC_AMO_RS.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_RS", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_RS", ddlPBP_B16B_BENDESC_AMO_RS.SelectedValue);
            }

            if (ddlPBP_B16B_BENDESC_AMO_END.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_END", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_END", ddlPBP_B16B_BENDESC_AMO_END.SelectedValue);
            }

            if (ddlPBP_B16B_BENDESC_AMO_PERI.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_PERI", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_PERI", ddlPBP_B16B_BENDESC_AMO_PERI.SelectedValue);
            }

            if (ddlPBP_B16B_BENDESC_AMO_EXT.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_EXT", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_EXT", ddlPBP_B16B_BENDESC_AMO_EXT.SelectedValue);
            }

            if (ddlPBP_B16B_BENDESC_AMO_POO.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_POO", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@PBP_B16B_BENDESC_AMO_POO", ddlPBP_B16B_BENDESC_AMO_POO.SelectedValue);
            }

            //Emergency Room  

            lobjCmd.Parameters.AddWithValue("@pbp_b4a_max_visit", txtpbp_b4a_max_visit.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b4a_coins_pct_mc_min", txtpbp_b4a_coins_pct_mc_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b4a_copay_amt_mc_min", txtpbp_b4a_copay_amt_mc_min.Text);

            // Speciality Care Physician

            lobjCmd.Parameters.AddWithValue("@pbp_b7d_coins_pct_mc_min", txtpbp_b7d_coins_pct_mc_min.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b7d_copay_amt_mc_min", txtpbp_b7d_copay_amt_mc_min.Text);

            // Radiology 

            if (rd_pbp_b8b_copay_max_yn.SelectedValue == "Yes")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_max_yn", 1);
            }
            else if (rd_pbp_b8b_copay_max_yn.SelectedValue == "No")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_max_yn", 2);
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_max_yn", "");
            }

            List<String> XRayCoin = new List<string>();

            if (chck_XRay_Coin.Checked == true)
            {
                XRayCoin.Add("1");
            }
            else
            {
                XRayCoin.Add("0");
            }

            if (chck_Diagnostic_Coin.Checked == true)
            {
                XRayCoin.Add("1");
            }
            else
            {
                XRayCoin.Add("0");
            }

            if (chck_Therapeutic_Coin.Checked == true)
            {
                XRayCoin.Add("1");
            }
            else
            {
                XRayCoin.Add("0");
            }


            string selectedXRayCoin = string.Empty;
            var XRayCoincollection = XRayCoin;
            foreach (var item1 in XRayCoincollection)
            {
                selectedXRayCoin += item1.ToString();
            }

            if (selectedXRayCoin == "000")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b8b_coins_ehc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b8b_coins_ehc", selectedXRayCoin);
            }

            lobjCmd.Parameters.AddWithValue("@pbp_b8b_coins_pct_cmc", txtpbp_b8b_coins_pct_cmc.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b8b_coins_pct_tmc", txtpbp_b8b_coins_pct_tmc.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b8b_coins_pct_drs", txtpbp_b8b_coins_pct_drs.Text);

            lobjCmd.Parameters.AddWithValue("@pbp_b8b_coins_pct_cmc_max", txtpbp_b8b_coins_pct_cmc_max.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b8b_coins_pct_tmc_max", txtpbp_b8b_coins_pct_tmc_max.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b8b_coins_pct_drs_max", txtpbp_b8b_coins_pct_drs_max.Text);

            List<String> XRayCopay = new List<string>();

            if (chck_XRay_Copay.Checked == true)
            {
                XRayCopay.Add("1");
            }
            else
            {
                XRayCopay.Add("0");
            }
            if (chck_Diagnostic_Copay.Checked == true)
            {
                XRayCopay.Add("1");
            }
            else
            {
                XRayCopay.Add("0");
            }

            if (chck_Therapeutic_Copay.Checked == true)
            {
                XRayCopay.Add("1");
            }
            else
            {
                XRayCopay.Add("0");
            }


            string selectedXRayCopay = string.Empty;
            var selectedXRayCopaycollection = XRayCopay;
            foreach (var item1 in selectedXRayCopaycollection)
            {
                selectedXRayCopay += item1.ToString();
            }

            if (selectedXRayCopay == "000")
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_ehc", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_ehc", selectedXRayCopay);
            }

            lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_mc_amt", txtpbp_b8b_copay_mc_amt.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_amt_tmc", txtpbp_b8b_copay_amt_tmc.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_amt_drs", txtpbp_b8b_copay_amt_drs.Text);

            lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_mc_amt_max", txtpbp_b8b_copay_mc_amt_max.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_amt_tmc_max", txtpbp_b8b_copay_amt_tmc_max.Text);
            lobjCmd.Parameters.AddWithValue("@pbp_b8b_copay_amt_drs_max", txtpbp_b8b_copay_amt_drs_max.Text);
            lobjCmd.Parameters.AddWithValue("@ScenarioId", Session["sId"].ToString());

            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();


        }

        private void Insert_PlanFider_Values()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_SaveAsScenario", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Convert.ToInt32(Session["OldSId"].ToString()));
            lobjCmd.Parameters.AddWithValue("@NewScenarionID", Convert.ToInt32(Session["sId"].ToString()));
            lobjCmd.Parameters.AddWithValue("@Flag", 1);
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        private void Insert_MRX()
        {
            lobjCmd = new SqlCommand("Insert_MRX", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCon.Open();
            lobjCmd.Parameters.AddWithValue("@BidId", Session["SelectedBidId"].ToString());
            if (ddlmrx_benefit_type.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_benefit_type", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_benefit_type", ddlmrx_benefit_type.SelectedValue);
            }

            lobjCmd.Parameters.AddWithValue("@mrx_alt_cov_lmt_amt", txtmrx_alt_cov_lmt_amt.Text);
            lobjCmd.Parameters.AddWithValue("@mrx_alt_ded_amount", txtmrx_alt_ded_amount.Text);

            string tiercollection = "000000";
            char[] ch = tiercollection.ToCharArray();

            if (ddlmrx_alt_no_ded_tier.Items[5].Checked == true)
            {
                ch[0] = '1';
            }
            if (ddlmrx_alt_no_ded_tier.Items[0].Checked == true)
            {
                ch[1] = '1';
            }
            if (ddlmrx_alt_no_ded_tier.Items[1].Checked == true)
            {
                ch[2] = '1';
            }
            if (ddlmrx_alt_no_ded_tier.Items[2].Checked == true)
            {
                ch[3] = '1';
            }
            if (ddlmrx_alt_no_ded_tier.Items[3].Checked == true)
            {
                ch[4] = '1';
            }
            if (ddlmrx_alt_no_ded_tier.Items[4].Checked == true)
            {
                ch[5] = '1';
            }

            string selectedTier = string.Empty;
            var TierCollection = ch;
            foreach (var item in TierCollection)
            {
                selectedTier += item.ToString();
            }
            lobjCmd.Parameters.AddWithValue("@mrx_alt_no_ded_tier", selectedTier);

            if (ddl1mrx_tier_cstshr_struct_type_tier1.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier1", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier1", ddl1mrx_tier_cstshr_struct_type_tier1.SelectedValue);
            }

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_coins_1m_tier1", txtmrx_tier_rstd_coins_1m_tier1.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_copay_1m_tier1", txtmrx_tier_rstd_copay_1m_tier1.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_coins_1m_tier1", txtmrx_tier_rsstd_coins_1m_tier1.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_copay_1m_tier1", txtmrx_tier_rsstd_copay_1m_tier1.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_coins_1m_tier1", txtmrx_tier_rspfd_coins_1m_tier1.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_copay_1m_tier1", txtmrx_tier_rspfd_copay_1m_tier1.Text);

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_coins_1m_tier1", txtmrx_tier_oonp_coins_1m_tier1.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_copay_1m_tier1", txtmrx_tier_oonp_copay_1m_tier1.Text);

            if (ddlmrx_tier_gap_cost_share_tier1.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier1", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier1", ddlmrx_tier_gap_cost_share_tier1.SelectedValue);
            }

            if (ddlMRX_TIER_INCLUDES_tier1.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier1", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier1", ddlMRX_TIER_INCLUDES_tier1.SelectedValue);
            }

            if (ddl1mrx_tier_cstshr_struct_type_tier2.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier2", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier2", ddl1mrx_tier_cstshr_struct_type_tier2.SelectedValue);
            }

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_coins_1m_tier2", txtmrx_tier_rstd_coins_1m_tier2.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_copay_1m_tier2", txtmrx_tier_rstd_copay_1m_tier2.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_coins_1m_tier2", txtmrx_tier_rsstd_coins_1m_tier2.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_copay_1m_tier2", txtmrx_tier_rsstd_copay_1m_tier2.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_coins_1m_tier2", txtmrx_tier_rspfd_coins_1m_tier2.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_copay_1m_tier2", txtmrx_tier_rspfd_copay_1m_tier2.Text);

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_coins_1m_tier2", txtmrx_tier_oonp_coins_1m_tier2.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_copay_1m_tier2", txtmrx_tier_oonp_copay_1m_tier2.Text);


            if (ddlmrx_tier_gap_cost_share_tier2.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier2", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier2", ddlmrx_tier_gap_cost_share_tier2.SelectedValue);
            }

            if (ddlMRX_TIER_INCLUDES_tier2.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier2", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier2", ddlMRX_TIER_INCLUDES_tier2.SelectedValue);
            }

            if (ddl1mrx_tier_cstshr_struct_type_tier3.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier3", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier3", ddl1mrx_tier_cstshr_struct_type_tier3.SelectedValue);
            }

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_coins_1m_tier3", txtmrx_tier_rstd_coins_1m_tier3.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_copay_1m_tier3", txtmrx_tier_rstd_copay_1m_tier3.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_coins_1m_tier3", txtmrx_tier_rsstd_coins_1m_tier3.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_copay_1m_tier3", txtmrx_tier_rsstd_copay_1m_tier3.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_coins_1m_tier3", txtmrx_tier_rspfd_coins_1m_tier3.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_copay_1m_tier3", txtmrx_tier_rspfd_copay_1m_tier3.Text);

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_coins_1m_tier3", txtmrx_tier_oonp_coins_1m_tier3.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_copay_1m_tier3", txtmrx_tier_oonp_copay_1m_tier3.Text);

            if (ddlmrx_tier_gap_cost_share_tier3.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier3", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier3", ddlmrx_tier_gap_cost_share_tier3.SelectedValue);
            }

            if (ddlMRX_TIER_INCLUDES_tier3.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier3", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier3", ddlMRX_TIER_INCLUDES_tier3.SelectedValue);
            }

            if (ddl1mrx_tier_cstshr_struct_type_tier4.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier4", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier4", ddl1mrx_tier_cstshr_struct_type_tier4.SelectedValue);
            }

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_coins_1m_tier4", txtmrx_tier_rstd_coins_1m_tier4.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_copay_1m_tier4", txtmrx_tier_rstd_copay_1m_tier4.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_coins_1m_tier4", txtmrx_tier_rsstd_coins_1m_tier4.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_copay_1m_tier4", txtmrx_tier_rsstd_copay_1m_tier4.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_coins_1m_tier4", txtmrx_tier_rspfd_coins_1m_tier4.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_copay_1m_tier4", txtmrx_tier_rspfd_copay_1m_tier4.Text);

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_coins_1m_tier4", txtmrx_tier_oonp_coins_1m_tier4.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_copay_1m_tier4", txtmrx_tier_oonp_copay_1m_tier4.Text);


            if (ddlmrx_tier_gap_cost_share_tier4.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier4", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier4", ddlmrx_tier_gap_cost_share_tier4.SelectedValue);
            }

            if (ddlMRX_TIER_INCLUDES_tier4.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier4", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier4", ddlMRX_TIER_INCLUDES_tier4.SelectedValue);
            }

            if (ddl1mrx_tier_cstshr_struct_type_tier5.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier5", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier5", ddl1mrx_tier_cstshr_struct_type_tier5.SelectedValue);
            }

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_coins_1m_tier5", txtmrx_tier_rstd_coins_1m_tier5.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_copay_1m_tier5", txtmrx_tier_rstd_copay_1m_tier5.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_coins_1m_tier5", txtmrx_tier_rsstd_coins_1m_tier5.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_copay_1m_tier5", txtmrx_tier_rsstd_copay_1m_tier5.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_coins_1m_tier5", txtmrx_tier_rspfd_coins_1m_tier5.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_copay_1m_tier5", txtmrx_tier_rspfd_copay_1m_tier5.Text);

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_coins_1m_tier5", txtmrx_tier_oonp_coins_1m_tier5.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_copay_1m_tier5", txtmrx_tier_oonp_copay_1m_tier5.Text);

            if (ddlmrx_tier_gap_cost_share_tier5.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier5", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier5", ddlmrx_tier_gap_cost_share_tier5.SelectedValue);
            }

            if (ddlMRX_TIER_INCLUDES_tier5.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier5", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier5", ddlMRX_TIER_INCLUDES_tier5.SelectedValue.Trim());
            }

            if (ddl1mrx_tier_cstshr_struct_type_tier6.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier6", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddl1mrx_tier_cstshr_struct_type_tier6", ddl1mrx_tier_cstshr_struct_type_tier6.SelectedValue);
            }

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_coins_1m_tier6", txtmrx_tier_rstd_coins_1m_tier6.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rstd_copay_1m_tier6", txtmrx_tier_rstd_copay_1m_tier6.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_coins_1m_tier6", txtmrx_tier_rsstd_coins_1m_tier6.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rsstd_copay_1m_tier6", txtmrx_tier_rsstd_copay_1m_tier6.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_coins_1m_tier6", txtmrx_tier_rspfd_coins_1m_tier6.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_rspfd_copay_1m_tier6", txtmrx_tier_rspfd_copay_1m_tier6.Text);

            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_coins_1m_tier6", txtmrx_tier_oonp_coins_1m_tier6.Text);
            lobjCmd.Parameters.AddWithValue("@txtmrx_tier_oonp_copay_1m_tier6", txtmrx_tier_oonp_copay_1m_tier6.Text);


            if (ddlmrx_tier_gap_cost_share_tier6.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier6", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlmrx_tier_gap_cost_share_tier6", ddlmrx_tier_gap_cost_share_tier6.SelectedValue);
            }

            if (ddlMRX_TIER_INCLUDES_tier6.SelectedValue == "Select")
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier6", "");
            }
            else
            {
                lobjCmd.Parameters.AddWithValue("@ddlMRX_TIER_INCLUDES_tier6", ddlMRX_TIER_INCLUDES_tier6.SelectedValue);
            }

            lobjCmd.Parameters.AddWithValue("@ScenarioId", Session["sId"].ToString());

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

        private void Insert_Pages_Simulate()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_Pages", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@PageName", "SimulatorResult.aspx");
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["sId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        private void RunSimulate()
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += "Enter into Run Simulator";
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = Server.MapPath("~/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }

            try
            {
                string inputParams = Session["sId"].ToString() + "," + Session["usrName"].ToString() + "," + Constants.inputFilesLocation;
                string LogPath = Constants.sasLogFileLocation + Session["usrName"].ToString() + ".log";
                int exitCode = 0;
                ProcessStartInfo info = new ProcessStartInfo(Constants.sasExeFileLocation);
                info.Arguments = " -sysin " + Constants.sasProgramFileLocation + " -LOG " + LogPath + " -sysparm " + "\"" + inputParams + "\"";
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                Process p = Process.Start(info);
                p.WaitForExit();
                exitCode = p.ExitCode;

                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += "Exited Code " + exitCode;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }

                if (exitCode != 0)
                {

                }
            }
            catch (Exception)
            {
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += "Enter into Exception in Run Simlator";
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
        }

        private void Save_All_BidId()
        {
            string GetBidID = Session["All_BidIDs"].ToString();
            lobjCmd = new SqlCommand("Insert_All_BidIds", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCon.Open();
            lobjCmd.Parameters.AddWithValue("@bid_id", GetBidID);
            lobjCmd.Parameters.AddWithValue("@ScenarioId", Session["sId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        #endregion

        #region Button Events

        protected void lnk_ScenarioList_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("ScenarioList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ScenarioList.aspx");
                Response.Redirect("~/Pages/ScenarioList.aspx");
            }
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {

            }
        }

        protected void btnSimulate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                Insert_MRX();
                Insert_Pages_Simulate();
                Save_All_BidId();
                ExportFiles();
                RunSimulate();
                ModalProgress.Hide();
                Session["ddl_BidId"] = ddlBidId.SelectedItem.Value;
                PT.InsertDataIntoDB("SimulatorResult", Session["SessionId"].ToString(), Session["UserName"].ToString(), "SimulatorResult.aspx");
                Response.Redirect("~/Pages/SimulatorResult.aspx", false);
            }
            catch (Exception)
            {
                ModalProgress.Hide();
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                string path = Server.MapPath("~/ErrorLog.txt");
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += "Enter into Exception button Click Simulator";
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
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
                    Session["OldSId"] = Session["sId"].ToString();
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
                    lblScenarioName.Text = txtScenario.Text;
                    Session["sName"] = txtScenario.Text;
                    Insert_PlanFider_Values();
                    SaveData();
                    Insert_MRX();
                    Insert_Pages();
                }
            }
            catch (Exception)
            {

            }

        }

        protected void lbRevert_Click(object sender, EventArgs e)
        {
            try
            {
                Bind_Data();
                Bind_MRX();
            }
            catch (Exception)
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
                SaveData();
                Insert_MRX();
                Insert_Pages();
            }
            catch (Exception)
            {

            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {
            try
            {
                lobjCmd = new SqlCommand("Download_QuickAccess", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@sId", Session["sId"].ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                DataTable lobjDS = new DataTable();
                lobjDA.Fill(lobjDS);

                if (lobjDS.Rows.Count > 0)
                {
                    string attachment = "attachment; filename=QuickAccess.csv";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "text/csv";
                    string tab = "";
                    foreach (DataColumn dc in lobjDS.Columns)
                    {
                        Response.Write(tab + dc.ColumnName);
                        tab = ",";
                    }
                    Response.Write("\n");
                    int i;
                    foreach (DataRow dr in lobjDS.Rows)
                    {
                        tab = "";
                        for (i = 0; i < lobjDS.Columns.Count; i++)
                        {
                            Response.Write(tab + dr[i].ToString());
                            tab = ",";
                        }
                        Response.Write("\n");
                    }
                    Response.End();
                }
            }
            catch (Exception)
            {


            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                Insert_MRX();
                Insert_Pages();
                PT.InsertDataIntoDB("PlanFinder", Session["SessionId"].ToString(), Session["UserName"].ToString(), "PlanFinder.aspx");
                Response.Redirect("~/Pages/PlanFinder.aspx");
            }
            catch (Exception)
            {

            }
        }

        protected void ddlBidId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                Insert_MRX();
                Insert_Pages();
                Bind_Data();
                Bind_MRX();
            }
            catch (Exception)
            {

            }
        }

        #endregion

        protected void ddlpbp_b1a_mc_copay_cstshr_yn_t1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlpbp_b1a_mc_copay_cstshr_yn_t1.SelectedItem.Text == "Yes")
            {
                txtpbp_b1a_copay_mcs_amt_t1.Enabled = false;
                txtpbp_b1a_coins_mcs_pct_t1.Enabled = false;
                txtpbp_b1a_coins_mcs_pct_int1_t1.Enabled = false;
                txtpbp_b1a_coins_mcs_pct_int2_t1.Enabled = false;
                txtpbp_b1a_coins_mcs_pct_int3_t1.Enabled = false;

                txtpbp_b1a_copay_mcs_amt_int1_t1.Enabled = false;
                txtpbp_b1a_copay_mcs_amt_int2_t1.Enabled = false;
                txtpbp_b1a_copay_mcs_amt_int3_t1.Enabled = false;

                txtBegin1Acute.Enabled = false;
                txtBegin2Acute.Enabled = false;
                txtBegin3Acute.Enabled = false;

                txtEnd1Acute.Enabled = false;
                txtEnd2Acute.Enabled = false;
                txtEnd3Acute.Enabled = false;

                rd_HospitalAcute.Enabled = false;
            }
            else
            {

                rd_HospitalAcute_SelectedIndexChanged(this, null);

                txtBegin1Acute.Enabled = true;
                txtBegin2Acute.Enabled = true;
                txtBegin3Acute.Enabled = true;

                txtEnd1Acute.Enabled = true;
                txtEnd2Acute.Enabled = true;
                txtEnd3Acute.Enabled = true;

                rd_HospitalAcute.Enabled = true;
            }
        }
    }
}