using BusinessUtility;
using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class ManageQuickAccess : System.Web.UI.Page
    {
        PageTracking PT = new PageTracking();
        BenefitSimulatorQuickAccessMethods benefitSimulatorQuickAccessMethods = new BenefitSimulatorQuickAccessMethods();
        BenefitSimulatorPlansUserInputsMethods benefitSimulatorPlansUserInputsMethods = new BenefitSimulatorPlansUserInputsMethods();
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
                    if ((Session["BenefitSimulatorScenarioID"] != null) && (Session["BenefitSimulatorScenarioName"] != null))
                    {
                        lblScenarioName.Text = Session["BenefitSimulatorScenarioName"].ToString();
                        BindQuickAccessData(Convert.ToInt32(Session["BenefitSimulatorScenarioID"]));
                        BindBenefitSimulatorPlansUserInputsPlanName(Convert.ToInt32(Session["BenefitSimulatorScenarioID"]));
                    }
                    else
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                Active_DeactiveLinks();
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

        protected void chck_Diagnostic_Coin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpbp_b9d_coins_yn.Checked)
            {
                txtpbp_b9d_coins_pct_mc_min.Enabled = true;
            }
            else
            {
                txtpbp_b9d_coins_pct_mc_min.Text = string.Empty;
                txtpbp_b9d_coins_pct_mc_min.Enabled = false;
            }
        }

        protected void chck_Diagnostic_Copay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpbp_b9d_copay_yn.Checked)
            {
                txtpbp_b9d_copay_mc_amt_min.Enabled = true;
            }
            else
            {
                txtpbp_b9d_copay_mc_amt_min.Text = string.Empty;
                txtpbp_b9d_copay_mc_amt_min.Enabled = false;
            }
        }

        private void BindBenefitSimulatorPlansUserInputsPlanName(int ScenarioID)
        {
            DataSet benefitSimulatorPlansUserInputsDS = benefitSimulatorPlansUserInputsMethods.GetBenefitSimulatorPlansUserInputsPlanName(ScenarioID);

            ddlBidId.DataSource = benefitSimulatorPlansUserInputsDS;
            ddlBidId.DataTextField = "PlanName";
            ddlBidId.DataValueField = "BidID";
            ddlBidId.DataBind();
        }

        private void BindQuickAccessData(int ScenarioID)
        {
            DataSet quickAccessDS = benefitSimulatorQuickAccessMethods.GetBenefitSimulatorQuickAccess(ScenarioID);

            if (quickAccessDS.Tables[0].Rows.Count > 0)
            {
                #region Plan_Features     
                txtMonthly_Consolidated_Premium_C_D.Text = quickAccessDS.Tables[0].Rows[0]["Monthly_Consolidated_Premium_C_D"].ToString();
                txtAnnual_Health_Deductible.Text = quickAccessDS.Tables[0].Rows[0]["Annual_Health_Deductible"].ToString();
                txtIn_network_MOOP_Amount.Text = quickAccessDS.Tables[0].Rows[0]["In_network_MOOP_Amount"].ToString();
                txtAnnual_Drug_Deductible.Text = quickAccessDS.Tables[0].Rows[0]["Annual_Drug_Deductible"].ToString();
                #endregion Plan_Features     

                #region Inpatient_Hospital
                LoadInpatientHospitalEnhancedBenefits(quickAccessDS);
                txtpbp_b1a_bendesc_amt_ad.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_bendesc_amt_ad"].ToString();
                txtpbp_b1a_copay_mcs_amt_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_amt_t1"].ToString();
                txtpbp_b1a_copay_mcs_amt_int1_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_amt_int1_t1"].ToString();
                txtpbp_b1a_copay_mcs_bgnd_int1_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_bgnd_int1_t1"].ToString();
                txtpbp_b1a_copay_mcs_endd_int1_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_endd_int1_t1"].ToString();
                txtpbp_b1a_copay_mcs_amt_int2_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_amt_int2_t1"].ToString();
                txtpbp_b1a_copay_mcs_bgnd_int2_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_bgnd_int2_t1"].ToString();
                txtpbp_b1a_copay_mcs_endd_int2_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_endd_int2_t1"].ToString();
                txtpbp_b1a_copay_mcs_amt_int3_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_amt_int3_t1"].ToString();
                txtpbp_b1a_copay_mcs_bgnd_int3_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_bgnd_int3_t1"].ToString();
                txtpbp_b1a_copay_mcs_endd_int3_t1.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_copay_mcs_endd_int3_t1"].ToString();
                #endregion Inpatient_Hospital  

                #region PCP_Specialist_ER
                txtpbp_b7a_coins_pct_mc_min.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b7a_coins_pct_mc_min"].ToString();
                txtpbp_b7a_copay_amt_mc_min.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b7a_copay_amt_mc_min"].ToString();
                txtpbp_b7d_coins_pct_mc_min.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b7d_coins_pct_mc_min"].ToString();
                txtpbp_b7d_copay_amt_mc_min.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b7d_copay_amt_mc_min"].ToString();
                txtpbp_b4a_coins_pct_mc_min.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b4a_coins_pct_mc_min"].ToString();
                txtpbp_b4a_copay_amt_mc_min.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b4a_copay_amt_mc_min"].ToString();
                #endregion PCP_Specialist_ER

                #region Outpatient_Medicare_Services
                txtpbp_b8a_coins_pct_lab.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8a_coins_pct_lab"].ToString();
                txtpbp_b8a_lab_copay_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8a_lab_copay_amt"].ToString();
                txtpbp_b8a_coins_pct_dmc.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8a_coins_pct_dmc"].ToString();
                txtpbp_b8a_copay_min_dmc_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8a_copay_min_dmc_amt"].ToString();
                txtpbp_b8b_coins_pct_cmc.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_cmc"].ToString();
                txtpbp_b8b_copay_mc_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8b_copay_mc_amt"].ToString();
                txtpbp_b8b_coins_pct_drs.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_drs"].ToString();
                txtpbp_b8b_copay_amt_drs.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8b_copay_amt_drs"].ToString();
                txtpbp_b8b_coins_pct_tmc.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8b_coins_pct_tmc"].ToString();
                txtpbp_b8b_copay_amt_tmc.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b8b_copay_amt_tmc"].ToString();
                #endregion Outpatient_Medicare_Services

                #region Outpatient_Blood_Services   
                if (quickAccessDS.Tables[0].Rows[0]["pbp_b9d_bendesc_yn"].ToString() == "1")
                {
                    rdpbp_b9d_bendesc_yn.SelectedValue = "Yes";
                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b9d_bendesc_yn"].ToString() == "2")
                {
                    rdpbp_b9d_bendesc_yn.SelectedValue = "No";
                }
                else
                {
                    rdpbp_b9d_bendesc_yn.SelectedValue = "";
                }

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b9d_coins_yn"].ToString() == "1")
                {
                    chkpbp_b9d_coins_yn.Checked = true;
                    txtpbp_b9d_coins_pct_mc_min.Enabled = true;
                    txtpbp_b9d_coins_pct_mc_min.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b9d_coins_pct_mc_min"].ToString();
                }
                else
                {
                    chkpbp_b9d_coins_yn.Checked = false;
                    txtpbp_b9d_coins_pct_mc_min.Enabled = false;
                    txtpbp_b9d_coins_pct_mc_min.Text = "";
                }

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b9d_copay_yn"].ToString() == "1")
                {
                    chkpbp_b9d_copay_yn.Checked = true;
                    txtpbp_b9d_copay_mc_amt_min.Enabled = true;
                    txtpbp_b9d_copay_mc_amt_min.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b9d_copay_mc_amt_min"].ToString();
                }
                else
                {
                    chkpbp_b9d_copay_yn.Checked = false;
                    txtpbp_b9d_copay_mc_amt_min.Enabled = false;
                    txtpbp_b9d_copay_mc_amt_min.Text = "";
                }

                #endregion Outpatient_Blood_Services

                #region OTC
                if (quickAccessDS.Tables[0].Rows[0]["pbp_b13b_bendesc_otc"].ToString() == "1")
                {
                    rdpbp_b13b_bendesc_otc.SelectedValue = "Yes";

                    txtpbp_b13b_maxplan_amt.Enabled = true;
                    ddlpbp_b13b_otc_maxplan_per.Enabled = true;

                    txtpbp_b13b_maxplan_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b13b_maxplan_amt"].ToString();
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b13b_otc_maxplan_per"].ToString() == "")
                    {
                        ddlpbp_b13b_otc_maxplan_per.SelectedValue = "Select";
                    }
                    else
                    {
                        ddlpbp_b13b_otc_maxplan_per.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b13b_otc_maxplan_per"].ToString();
                    }
                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b13b_bendesc_otc"].ToString() == "2")
                {
                    rdpbp_b13b_bendesc_otc.SelectedValue = "No";

                    txtpbp_b13b_maxplan_amt.Enabled = false;
                    ddlpbp_b13b_otc_maxplan_per.Enabled = false;

                    txtpbp_b13b_maxplan_amt.Text = "";
                }
                else
                {
                    rdpbp_b13b_bendesc_otc.SelectedValue = "";

                    txtpbp_b13b_maxplan_amt.Enabled = false;
                    ddlpbp_b13b_otc_maxplan_per.Enabled = false;

                    txtpbp_b13b_maxplan_amt.Text = "";
                }

                #endregion OTC

                #region PreventiveDental    
                LoadPreventiveDentalEnhancedBenefits(quickAccessDS);
                if (quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_yn"].ToString() == "1")
                {
                    rdpbp_b16a_bendesc_yn.SelectedValue = "Yes";
                    ddlpbp_b16a_bendesc_ehc.Enabled = true;
                    txtpbp_b16a_maxplan_amt.Enabled = true;
                    ddlpbp_b16a_maxplan_per.Enabled = true;
                    txtpbp_b16a_bendesc_numv_oe.Enabled = true;
                    ddlpbp_b16a_bendesc_per_oe.Enabled = true;
                    txtpbp_b16a_bendesc_numv_dx.Enabled = true;
                    ddlpbp_b16a_bendesc_per_dx.Enabled = true;
                    txtpbp_b16a_bendesc_numv_pc.Enabled = true;
                    ddlpbp_b16a_bendesc_per_pc.Enabled = true;
                    txtpbp_b16a_bendesc_numv_ft.Enabled = true;
                    ddlpbp_b16a_bendesc_per_ft.Enabled = true;

                    txtpbp_b16a_maxplan_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_maxplan_amt"].ToString();

                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b16a_maxplan_per"].ToString() == "")
                    {
                        ddlpbp_b16a_maxplan_per.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b16a_maxplan_per.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_maxplan_per"].ToString();
                    }

                    txtpbp_b16a_bendesc_numv_oe.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_numv_oe"].ToString();
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_oe"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_per_oe.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_per_oe.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_oe"].ToString();
                    }

                    txtpbp_b16a_bendesc_numv_dx.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_numv_dx"].ToString();
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_dx"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_per_dx.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_per_dx.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_dx"].ToString();
                    }

                    txtpbp_b16a_bendesc_numv_pc.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_numv_pc"].ToString();
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_pc"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_per_pc.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_per_pc.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_pc"].ToString();
                    }

                    txtpbp_b16a_bendesc_numv_ft.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_numv_ft"].ToString();
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_ft"].ToString() == "")
                    {
                        ddlpbp_b16a_bendesc_per_ft.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b16a_bendesc_per_ft.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_per_ft"].ToString();
                    }

                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_yn"].ToString() == "2")
                {
                    rdpbp_b16a_bendesc_yn.SelectedValue = "No";

                    // ddlpbp_b16a_bendesc_ehc.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_ehc.Enabled = false;
                    txtpbp_b16a_maxplan_amt.Text = string.Empty;
                    txtpbp_b16a_maxplan_amt.Enabled = false;
                    ddlpbp_b16a_maxplan_per.SelectedValue = "0";
                    ddlpbp_b16a_maxplan_per.Enabled = false;

                    txtpbp_b16a_bendesc_numv_oe.Text = string.Empty;
                    txtpbp_b16a_bendesc_numv_oe.Enabled = false;
                    ddlpbp_b16a_bendesc_per_oe.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_per_oe.Enabled = false;

                    txtpbp_b16a_bendesc_numv_dx.Text = string.Empty;
                    txtpbp_b16a_bendesc_numv_dx.Enabled = false;
                    ddlpbp_b16a_bendesc_per_dx.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_per_dx.Enabled = false;

                    txtpbp_b16a_bendesc_numv_pc.Text = string.Empty;
                    txtpbp_b16a_bendesc_numv_pc.Enabled = false;
                    ddlpbp_b16a_bendesc_per_pc.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_per_pc.Enabled = false;

                    txtpbp_b16a_bendesc_numv_ft.Text = string.Empty;
                    txtpbp_b16a_bendesc_numv_ft.Enabled = false;
                    ddlpbp_b16a_bendesc_per_ft.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_per_ft.Enabled = false;
                }
                else
                {
                    rdpbp_b16a_bendesc_yn.SelectedValue = "";

                    // ddlpbp_b16a_bendesc_ehc.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_ehc.Enabled = false;
                    txtpbp_b16a_maxplan_amt.Text = string.Empty;
                    txtpbp_b16a_maxplan_amt.Enabled = false;
                    ddlpbp_b16a_maxplan_per.SelectedValue = "0";
                    ddlpbp_b16a_maxplan_per.Enabled = false;

                    txtpbp_b16a_bendesc_numv_oe.Text = string.Empty;
                    txtpbp_b16a_bendesc_numv_oe.Enabled = false;
                    ddlpbp_b16a_bendesc_per_oe.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_per_oe.Enabled = false;

                    txtpbp_b16a_bendesc_numv_dx.Text = string.Empty;
                    txtpbp_b16a_bendesc_numv_dx.Enabled = false;
                    ddlpbp_b16a_bendesc_per_dx.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_per_dx.Enabled = false;

                    txtpbp_b16a_bendesc_numv_pc.Text = string.Empty;
                    txtpbp_b16a_bendesc_numv_pc.Enabled = false;
                    ddlpbp_b16a_bendesc_per_pc.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_per_pc.Enabled = false;

                    txtpbp_b16a_bendesc_numv_ft.Text = string.Empty;
                    txtpbp_b16a_bendesc_numv_ft.Enabled = false;
                    ddlpbp_b16a_bendesc_per_ft.SelectedValue = "0";
                    ddlpbp_b16a_bendesc_per_ft.Enabled = false;
                }

                #endregion PreventiveDental

                #region ComprehensiveDental

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b16b_bendesc_yn"].ToString() == "1")
                {
                    rdpbp_b16b_bendesc_yn.SelectedValue = "Yes";
                    ddlpbp_b16b_bendesc_ehc.Enabled = true;
                    rdpbp_b16b_maxplan_yn.Enabled = true;
                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b16b_bendesc_yn"].ToString() == "2")
                {
                    rdpbp_b16b_bendesc_yn.SelectedValue = "No";
                    ddlpbp_b16b_bendesc_ehc.Enabled = false;
                    rdpbp_b16b_maxplan_yn.Enabled = false;
                }
                else
                {
                    rdpbp_b16b_bendesc_yn.SelectedValue = "";
                    ddlpbp_b16b_bendesc_ehc.Enabled = false;
                    rdpbp_b16b_maxplan_yn.Enabled = false;
                }

                LoadComprehensiveDentalEnhancedBenefits(quickAccessDS);

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b16b_maxplan_yn"].ToString() == "1")
                {
                    rdpbp_b16b_maxplan_yn.SelectedValue = "Yes";
                    ddlpbp_b16b_maxbene_type.Enabled = true;

                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b16b_maxbene_type"].ToString() == "")
                    {
                        ddlpbp_b16b_maxbene_type.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b16b_maxbene_type.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b16b_maxbene_type"].ToString();
                    }

                    txtpbp_b16b_maxplan_amt.Enabled = true;
                    txtpbp_b16b_maxplan_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b16b_maxplan_amt"].ToString();

                    ddlpbp_b16b_maxplan_per.Enabled = true;
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b16b_maxplan_per"].ToString() == "")
                    {
                        ddlpbp_b16b_maxplan_per.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b16b_maxplan_per.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b16b_maxplan_per"].ToString();
                    }
                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b16b_maxplan_yn"].ToString() == "2")
                {
                    rdpbp_b16b_maxplan_yn.SelectedValue = "No";
                    ddlpbp_b16b_maxbene_type.SelectedValue = "0";
                    ddlpbp_b16b_maxbene_type.Enabled = false;
                    txtpbp_b16b_maxplan_amt.Enabled = false;
                    txtpbp_b16b_maxplan_amt.Text = string.Empty;
                    ddlpbp_b16b_maxplan_per.SelectedValue = "0";
                    ddlpbp_b16b_maxplan_per.Enabled = false;
                }
                else
                {
                    rdpbp_b16b_maxplan_yn.SelectedValue = "";
                    ddlpbp_b16b_maxbene_type.SelectedValue = "0";
                    ddlpbp_b16b_maxbene_type.Enabled = false;
                    txtpbp_b16b_maxplan_amt.Enabled = false;
                    txtpbp_b16b_maxplan_amt.Text = string.Empty;
                    ddlpbp_b16b_maxplan_per.SelectedValue = "0";
                    ddlpbp_b16b_maxplan_per.Enabled = false;
                }

                #endregion ComprehensiveDental

                #region Eyewear
                if (quickAccessDS.Tables[0].Rows[0]["pbp_b17b_bendesc_yn"].ToString() == "1")
                {
                    rdpbp_b17b_bendesc_yn.SelectedValue = "Yes";
                    ddlpbp_b17b_bendesc_ehc.Enabled = true;
                    rdpbp_b17b_maxplan_yn.Enabled = true;
                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b17b_bendesc_yn"].ToString() == "2")
                {
                    rdpbp_b17b_bendesc_yn.SelectedValue = "No";
                    ddlpbp_b17b_bendesc_ehc.Enabled = false;
                    rdpbp_b17b_maxplan_yn.Enabled = false;
                }
                else
                {
                    rdpbp_b17b_bendesc_yn.SelectedValue = "";
                    ddlpbp_b17b_bendesc_ehc.Enabled = false;
                    rdpbp_b17b_maxplan_yn.Enabled = false;
                }

                LoadEyewearEnhancedBenefits(quickAccessDS);

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b17b_maxplan_yn"].ToString() == "1")
                {
                    rdpbp_b17b_maxplan_yn.SelectedValue = "Yes";
                    ddlpbp_b17b_maxplan_type.Enabled = true;

                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b17b_maxplan_type"].ToString() == "")
                    {
                        ddlpbp_b17b_maxplan_type.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b17b_maxplan_type.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b17b_maxplan_type"].ToString();
                    }

                    txtpbp_b17b_comb_maxplan_amt.Enabled = true;
                    ddlpbp_b17b_comb_maxplan_per.Enabled = true;
                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b17b_maxplan_yn"].ToString() == "2")
                {
                    rdpbp_b17b_maxplan_yn.SelectedValue = "No";
                    ddlpbp_b17b_maxplan_type.SelectedValue = "0";
                    ddlpbp_b17b_maxplan_type.Enabled = false;
                    txtpbp_b17b_comb_maxplan_amt.Enabled = false;
                    txtpbp_b17b_comb_maxplan_amt.Text = string.Empty;
                    ddlpbp_b17b_comb_maxplan_per.SelectedValue = "0";
                    ddlpbp_b17b_comb_maxplan_per.Enabled = false;
                }
                else
                {
                    rdpbp_b17b_maxplan_yn.SelectedValue = "";
                    ddlpbp_b17b_maxplan_type.SelectedValue = "0";
                    ddlpbp_b17b_maxplan_type.Enabled = false;
                    txtpbp_b17b_comb_maxplan_amt.Enabled = false;
                    txtpbp_b17b_comb_maxplan_amt.Text = string.Empty;
                    ddlpbp_b17b_comb_maxplan_per.SelectedValue = "0";
                    ddlpbp_b17b_comb_maxplan_per.Enabled = false;
                }

                txtpbp_b17b_comb_maxplan_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b17b_comb_maxplan_amt"].ToString();

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b17b_comb_maxplan_per"].ToString() == "")
                {
                    ddlpbp_b17b_comb_maxplan_per.SelectedValue = "0";
                }
                else
                {
                    ddlpbp_b17b_comb_maxplan_per.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b17b_comb_maxplan_per"].ToString();
                }

                #endregion Eyewear

                #region HearingAid

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b18b_bendesc_yn"].ToString() == "1")
                {
                    rdpbp_b18b_bendesc_yn.SelectedValue = "Yes";
                    ddlpbp_b18b_bendesc_ehc.Enabled = true;
                    rdpbp_b18b_maxplan_yn.Enabled = true;
                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b18b_bendesc_yn"].ToString() == "2")
                {
                    rdpbp_b18b_bendesc_yn.SelectedValue = "No";
                    ddlpbp_b18b_bendesc_ehc.Enabled = false;
                    rdpbp_b18b_maxplan_yn.Enabled = false;
                }
                else
                {
                    rdpbp_b18b_bendesc_yn.SelectedValue = "";
                    ddlpbp_b18b_bendesc_ehc.Enabled = false;
                    rdpbp_b18b_maxplan_yn.Enabled = false;
                }

                LoadHearingAidEnhancedBenefits(quickAccessDS);

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_yn"].ToString() == "1")
                {
                    rdpbp_b18b_maxplan_yn.SelectedValue = "Yes";

                    ddlpbp_b18b_maxplan_perear.Enabled = true;
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_perear"].ToString() == "")
                    {
                        ddlpbp_b18b_maxplan_perear.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b18b_maxplan_perear.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_perear"].ToString();
                    }

                    ddlpbp_b18b_maxplan_type.Enabled = true;
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_type"].ToString() == "")
                    {
                        ddlpbp_b18b_maxplan_type.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b18b_maxplan_type.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_type"].ToString();
                    }

                    txtpbp_b18b_maxplan_amt.Enabled = true;
                    txtpbp_b18b_maxplan_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_amt"].ToString();

                    ddlpbp_b18b_maxplan_per.Enabled = true;
                    if (quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_per"].ToString() == "")
                    {
                        ddlpbp_b18b_maxplan_per.SelectedValue = "0";
                    }
                    else
                    {
                        ddlpbp_b18b_maxplan_per.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_per"].ToString();
                    }
                }
                else if (quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_yn"].ToString() == "2")
                {
                    rdpbp_b18b_maxplan_yn.SelectedValue = "No";
                    ddlpbp_b18b_maxplan_perear.SelectedValue = "0";
                    ddlpbp_b18b_maxplan_perear.Enabled = false;
                    ddlpbp_b18b_maxplan_type.SelectedValue = "0";
                    ddlpbp_b18b_maxplan_type.Enabled = false;
                    txtpbp_b18b_maxplan_amt.Enabled = false;
                    txtpbp_b18b_maxplan_amt.Text = string.Empty;
                    ddlpbp_b18b_maxplan_per.SelectedValue = "0";
                    ddlpbp_b18b_maxplan_per.Enabled = false;
                }
                else
                {
                    rdpbp_b18b_maxplan_yn.SelectedValue = "";
                    ddlpbp_b18b_maxplan_perear.SelectedValue = "0";
                    ddlpbp_b18b_maxplan_perear.Enabled = false;
                    ddlpbp_b18b_maxplan_type.SelectedValue = "0";
                    ddlpbp_b18b_maxplan_type.Enabled = false;
                    txtpbp_b18b_maxplan_amt.Enabled = false;
                    txtpbp_b18b_maxplan_amt.Text = string.Empty;
                    ddlpbp_b18b_maxplan_per.SelectedValue = "0";
                    ddlpbp_b18b_maxplan_per.Enabled = false;
                }

                txtpbp_b18b_maxplan_amt.Text = quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_amt"].ToString();

                if (quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_per"].ToString() == "")
                {
                    ddlpbp_b18b_maxplan_per.SelectedValue = "0";
                }
                else
                {
                    ddlpbp_b18b_maxplan_per.SelectedValue = quickAccessDS.Tables[0].Rows[0]["pbp_b18b_maxplan_per"].ToString();
                }
                #endregion HearingAid
            }
        }

        protected void rdpbp_b13b_bendesc_otc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdpbp_b13b_bendesc_otc.SelectedItem.Text == "Yes")
            {
                txtpbp_b13b_maxplan_amt.Enabled = true;
                ddlpbp_b13b_otc_maxplan_per.Enabled = true;
            }
            else
            {
                txtpbp_b13b_maxplan_amt.Text = string.Empty;
                txtpbp_b13b_maxplan_amt.Enabled = false;
                ddlpbp_b13b_otc_maxplan_per.SelectedValue = "0";
                ddlpbp_b13b_otc_maxplan_per.Enabled = false;
            }
        }

        protected void rdpbp_b16a_bendesc_yn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdpbp_b16a_bendesc_yn.SelectedItem.Text == "Yes")
            {
                ddlpbp_b16a_bendesc_ehc.Enabled = true;
                txtpbp_b16a_maxplan_amt.Enabled = true;
                ddlpbp_b16a_maxplan_per.Enabled = true;
                txtpbp_b16a_bendesc_numv_oe.Enabled = true;
                ddlpbp_b16a_bendesc_per_oe.Enabled = true;
                txtpbp_b16a_bendesc_numv_dx.Enabled = true;
                ddlpbp_b16a_bendesc_per_dx.Enabled = true;
                txtpbp_b16a_bendesc_numv_pc.Enabled = true;
                ddlpbp_b16a_bendesc_per_pc.Enabled = true;
                txtpbp_b16a_bendesc_numv_ft.Enabled = true;
                ddlpbp_b16a_bendesc_per_ft.Enabled = true;
            }
            else
            {
                ddlpbp_b16a_bendesc_ehc.SelectedValue = "0";
                ddlpbp_b16a_bendesc_ehc.Enabled = false;
                txtpbp_b16a_maxplan_amt.Text = string.Empty;
                txtpbp_b16a_maxplan_amt.Enabled = false;
                ddlpbp_b16a_maxplan_per.SelectedValue = "0";
                ddlpbp_b16a_maxplan_per.Enabled = false;

                txtpbp_b16a_bendesc_numv_oe.Text = string.Empty;
                txtpbp_b16a_bendesc_numv_oe.Enabled = false;
                ddlpbp_b16a_bendesc_per_oe.SelectedValue = "0";
                ddlpbp_b16a_bendesc_per_oe.Enabled = false;

                txtpbp_b16a_bendesc_numv_dx.Text = string.Empty;
                txtpbp_b16a_bendesc_numv_dx.Enabled = false;
                ddlpbp_b16a_bendesc_per_dx.SelectedValue = "0";
                ddlpbp_b16a_bendesc_per_dx.Enabled = false;

                txtpbp_b16a_bendesc_numv_pc.Text = string.Empty;
                txtpbp_b16a_bendesc_numv_pc.Enabled = false;
                ddlpbp_b16a_bendesc_per_pc.SelectedValue = "0";
                ddlpbp_b16a_bendesc_per_pc.Enabled = false;

                txtpbp_b16a_bendesc_numv_ft.Text = string.Empty;
                txtpbp_b16a_bendesc_numv_ft.Enabled = false;
                ddlpbp_b16a_bendesc_per_ft.SelectedValue = "0";
                ddlpbp_b16a_bendesc_per_ft.Enabled = false;
            }
        }

        protected void rdpbp_b16b_bendesc_yn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdpbp_b16b_bendesc_yn.SelectedItem.Text == "Yes")
            {
                ddlpbp_b16b_bendesc_ehc.Enabled = true;
                rdpbp_b16b_maxplan_yn.Enabled = true;
            }
            else
            {
                ddlpbp_b16b_bendesc_ehc.SelectedValue = "0";
                ddlpbp_b16b_bendesc_ehc.Enabled = false;
                rdpbp_b16b_maxplan_yn.SelectedValue = "";
                rdpbp_b16b_maxplan_yn.Enabled = false;
                ddlpbp_b16b_maxbene_type.SelectedValue = "0";
                ddlpbp_b16b_maxbene_type.Enabled = false;
                txtpbp_b16b_maxplan_amt.Enabled = false;
                txtpbp_b16b_maxplan_amt.Text = string.Empty;
                ddlpbp_b16b_maxplan_per.SelectedValue = "0";
                ddlpbp_b16b_maxplan_per.Enabled = false;
            }
        }

        protected void rdpbp_b16b_maxplan_yn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdpbp_b16b_maxplan_yn.SelectedItem.Text == "Yes")
            {
                ddlpbp_b16b_maxbene_type.Enabled = true;
                txtpbp_b16b_maxplan_amt.Enabled = true;
                ddlpbp_b16b_maxplan_per.Enabled = true;
            }
            else
            {
                ddlpbp_b16b_maxbene_type.SelectedValue = "0";
                ddlpbp_b16b_maxbene_type.Enabled = false;
                txtpbp_b16b_maxplan_amt.Enabled = false;
                txtpbp_b16b_maxplan_amt.Text = string.Empty;
                ddlpbp_b16b_maxplan_per.SelectedValue = "0";
                ddlpbp_b16b_maxplan_per.Enabled = false;
            }
        }

        protected void rdpbp_b17b_bendesc_yn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdpbp_b17b_bendesc_yn.SelectedItem.Text == "Yes")
            {
                ddlpbp_b17b_bendesc_ehc.Enabled = true;
                rdpbp_b17b_maxplan_yn.Enabled = true;
            }
            else
            {
                ddlpbp_b17b_bendesc_ehc.SelectedValue = "0";
                ddlpbp_b17b_bendesc_ehc.Enabled = false;
                rdpbp_b17b_maxplan_yn.SelectedValue = "";
                rdpbp_b17b_maxplan_yn.Enabled = false;
                ddlpbp_b17b_maxplan_type.SelectedValue = "0";
                ddlpbp_b17b_maxplan_type.Enabled = false;
                txtpbp_b17b_comb_maxplan_amt.Enabled = false;
                txtpbp_b17b_comb_maxplan_amt.Text = string.Empty;
                ddlpbp_b17b_comb_maxplan_per.SelectedValue = "0";
                ddlpbp_b17b_comb_maxplan_per.Enabled = false;
            }
        }

        protected void rdpbp_b17b_maxplan_yn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdpbp_b17b_maxplan_yn.SelectedItem.Text == "Yes")
            {
                ddlpbp_b17b_maxplan_type.Enabled = true;
                txtpbp_b17b_comb_maxplan_amt.Enabled = true;
                ddlpbp_b17b_comb_maxplan_per.Enabled = true;
            }
            else
            {
                ddlpbp_b17b_maxplan_type.SelectedValue = "0";
                ddlpbp_b17b_maxplan_type.Enabled = false;
                txtpbp_b17b_comb_maxplan_amt.Enabled = false;
                txtpbp_b17b_comb_maxplan_amt.Text = string.Empty;
                ddlpbp_b17b_comb_maxplan_per.SelectedValue = "0";
                ddlpbp_b17b_comb_maxplan_per.Enabled = false;
            }
        }

        protected void rdpbp_b18b_bendesc_yn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdpbp_b18b_bendesc_yn.SelectedItem.Text == "Yes")
            {
                ddlpbp_b18b_bendesc_ehc.Enabled = true;
                rdpbp_b18b_maxplan_yn.Enabled = true;
            }
            else
            {
                ddlpbp_b18b_bendesc_ehc.SelectedValue = "0";
                ddlpbp_b18b_bendesc_ehc.Enabled = false;
                rdpbp_b18b_maxplan_yn.SelectedValue = "";
                rdpbp_b18b_maxplan_yn.Enabled = false;
                ddlpbp_b18b_maxplan_perear.SelectedValue = "0";
                ddlpbp_b18b_maxplan_perear.Enabled = false;
                ddlpbp_b18b_maxplan_type.SelectedValue = "0";
                ddlpbp_b18b_maxplan_type.Enabled = false;
                txtpbp_b18b_maxplan_amt.Enabled = false;
                txtpbp_b18b_maxplan_amt.Text = string.Empty;
                ddlpbp_b18b_maxplan_per.SelectedValue = "0";
                ddlpbp_b18b_maxplan_per.Enabled = false;
            }
        }

        protected void rdpbp_b18b_maxplan_yn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdpbp_b18b_maxplan_yn.SelectedItem.Text == "Yes")
            {
                ddlpbp_b18b_maxplan_perear.Enabled = true;
                ddlpbp_b18b_maxplan_type.Enabled = true;
                txtpbp_b18b_maxplan_amt.Enabled = true;
                ddlpbp_b18b_maxplan_per.Enabled = true;
            }
            else
            {
                ddlpbp_b18b_maxplan_perear.SelectedValue = "0";
                ddlpbp_b18b_maxplan_perear.Enabled = false;
                ddlpbp_b18b_maxplan_type.SelectedValue = "0";
                ddlpbp_b18b_maxplan_type.Enabled = false;
                txtpbp_b18b_maxplan_amt.Enabled = false;
                txtpbp_b18b_maxplan_amt.Text = string.Empty;
                ddlpbp_b18b_maxplan_per.SelectedValue = "0";
                ddlpbp_b18b_maxplan_per.Enabled = false;
            }
        }

        public void LoadInpatientHospitalEnhancedBenefits(DataSet quickAccessDS)
        {
            ddlpbp_b1a_bendesc_ad_up_nmcs.Items.Clear();
            //ddlpbp_b1a_bendesc_ad_up_nmcs.Items.Insert(0, "Select");
            ddlpbp_b1a_bendesc_ad_up_nmcs.Items.Insert(0, "Additional Days");
            ddlpbp_b1a_bendesc_ad_up_nmcs.Items.Insert(1, "Non-Medicare-covered Stay");
            ddlpbp_b1a_bendesc_ad_up_nmcs.Items.Insert(2, "Upgrades");
            ddlpbp_b1a_bendesc_ad_up_nmcs.Visible = true;

            if (quickAccessDS.Tables[0].Rows.Count > 0)
            {
                string val = quickAccessDS.Tables[0].Rows[0]["pbp_b1a_bendesc_ad_up_nmcs"].ToString();
                if (val != null)
                {
                    char[] individualValues = val.ToCharArray();
                    for (int i = 0; i < individualValues.Length; i++)
                    {
                        if (individualValues[i].ToString() == "1")
                        {
                            ddlpbp_b1a_bendesc_ad_up_nmcs.Items[i].Checked = true;
                        }
                        else
                        {
                            ddlpbp_b1a_bendesc_ad_up_nmcs.Items[i].Checked = false;
                        }
                    }
                }
            }
        }

        public void LoadPreventiveDentalEnhancedBenefits(DataSet quickAccessDS)
        {
            ddlpbp_b16a_bendesc_ehc.Items.Clear();
            //ddlpbp_b16a_bendesc_ehc.Items.Insert(0, "Select");
            ddlpbp_b16a_bendesc_ehc.Items.Insert(0, "Oral Exams");
            ddlpbp_b16a_bendesc_ehc.Items.Insert(1, "Prophylaxis (Cleaning)");
            ddlpbp_b16a_bendesc_ehc.Items.Insert(2, "Fluoride Treatment");
            ddlpbp_b16a_bendesc_ehc.Items.Insert(3, "Dental X-Rays");
            ddlpbp_b16a_bendesc_ehc.DataBind();
            ddlpbp_b16a_bendesc_ehc.Visible = true;

            if (quickAccessDS.Tables[0].Rows.Count > 0)
            {
                string val = quickAccessDS.Tables[0].Rows[0]["pbp_b16a_bendesc_ehc"].ToString();
                if (val != null)
                {
                    char[] individualValues = val.ToCharArray();
                    for (int i = 0; i < individualValues.Length; i++)
                    {
                        if (individualValues[i].ToString() == "1")
                        {
                            ddlpbp_b16a_bendesc_ehc.Items[i].Checked = true;
                        }
                        else
                        {
                            ddlpbp_b16a_bendesc_ehc.Items[i].Checked = false;
                        }
                    }
                }
            }
        }

        public void LoadComprehensiveDentalEnhancedBenefits(DataSet quickAccessDS)
        {
            ddlpbp_b16b_bendesc_ehc.Items.Clear();
            //ddlpbp_b16b_bendesc_ehc.Items.Insert(0, "Select");
            ddlpbp_b16b_bendesc_ehc.Items.Insert(0, "Prosthodontics, Other Oral/Maxillofacial Surgery, Other Services");
            ddlpbp_b16b_bendesc_ehc.Items.Insert(1, "Non-routine Services");
            ddlpbp_b16b_bendesc_ehc.Items.Insert(2, "Diagnostic Services");
            ddlpbp_b16b_bendesc_ehc.Items.Insert(3, "Restorative Services");
            ddlpbp_b16b_bendesc_ehc.Items.Insert(4, "Endodontics");
            ddlpbp_b16b_bendesc_ehc.Items.Insert(5, "Periodontics");
            ddlpbp_b16b_bendesc_ehc.Items.Insert(6, "Extractions");
            ddlpbp_b16b_bendesc_ehc.DataBind();
            ddlpbp_b16b_bendesc_ehc.Visible = true;

            if (quickAccessDS.Tables[0].Rows.Count > 0)
            {
                string val = quickAccessDS.Tables[0].Rows[0]["pbp_b16b_bendesc_ehc"].ToString();
                if (val != null)
                {
                    char[] individualValues = val.ToCharArray();
                    for (int i = 0; i < individualValues.Length; i++)
                    {
                        if (individualValues[i].ToString() == "1")
                        {
                            ddlpbp_b16b_bendesc_ehc.Items[i].Checked = true;
                        }
                        else
                        {
                            ddlpbp_b16b_bendesc_ehc.Items[i].Checked = false;
                        }
                    }
                }
            }
        }

        public void LoadEyewearEnhancedBenefits(DataSet quickAccessDS)
        {
            ddlpbp_b17b_bendesc_ehc.Items.Clear();
            //ddlpbp_b17b_bendesc_ehc.Items.Insert(0, "Select");
            ddlpbp_b17b_bendesc_ehc.Items.Insert(0, "Upgrades");
            ddlpbp_b17b_bendesc_ehc.Items.Insert(1, "Contact lenses");
            ddlpbp_b17b_bendesc_ehc.Items.Insert(2, "Eyeglasses (lenses and frames)");
            ddlpbp_b17b_bendesc_ehc.Items.Insert(3, "Eyeglass lenses");
            ddlpbp_b17b_bendesc_ehc.Items.Insert(4, "Eyeglass frames");
            ddlpbp_b17b_bendesc_ehc.Visible = true;

            if (quickAccessDS.Tables[0].Rows.Count > 0)
            {
                string val = quickAccessDS.Tables[0].Rows[0]["pbp_b17b_bendesc_ehc"].ToString();
                if (val != null)
                {
                    char[] individualValues = val.ToCharArray();
                    for (int i = 0; i < individualValues.Length; i++)
                    {
                        if (individualValues[i].ToString() == "1")
                        {
                            ddlpbp_b17b_bendesc_ehc.Items[i].Checked = true;
                        }
                        else
                        {
                            ddlpbp_b17b_bendesc_ehc.Items[i].Checked = false;
                        }
                    }
                }
            }
        }

        public void LoadHearingAidEnhancedBenefits(DataSet quickAccessDS)
        {
            ddlpbp_b18b_bendesc_ehc.Items.Clear();
            //ddlpbp_b18b_bendesc_ehc.Items.Insert(0, "Select");
            ddlpbp_b18b_bendesc_ehc.Items.Insert(0, "Hearing Aids (all types)");
            ddlpbp_b18b_bendesc_ehc.Items.Insert(1, "Hearing Aids - Inner Ear");
            ddlpbp_b18b_bendesc_ehc.Items.Insert(2, "Hearing Aids - Outer Ear");
            ddlpbp_b18b_bendesc_ehc.Items.Insert(3, "Hearing Aids - Over the Ear");

            ddlpbp_b18b_bendesc_ehc.DataBind();
            ddlpbp_b18b_bendesc_ehc.Visible = true;

            if (quickAccessDS.Tables[0].Rows.Count > 0)
            {
                string val = quickAccessDS.Tables[0].Rows[0]["pbp_b18b_bendesc_ehc"].ToString();
                if (val != null)
                {
                    char[] individualValues = val.ToCharArray();
                    for (int i = 0; i < individualValues.Length; i++)
                    {
                        if (individualValues[i].ToString() == "1")
                        {
                            ddlpbp_b18b_bendesc_ehc.Items[i].Checked = true;
                        }
                        else
                        {
                            ddlpbp_b18b_bendesc_ehc.Items[i].Checked = false;
                        }
                    }
                }
            }
        }

        private void SaveData()
        {
            BenefitSimulatorQuickAccessUserInput bSQAUI = new BenefitSimulatorQuickAccessUserInput();

            bSQAUI.ScenarioID = Convert.ToInt32(Session["BenefitSimulatorScenarioID"]);
            bSQAUI.bid_id = ddlBidId.SelectedValue;

            #region Plan_Features
            bSQAUI.Monthly_Consolidated_Premium_C_D = txtMonthly_Consolidated_Premium_C_D.Text;
            bSQAUI.Annual_Health_Deductible = txtAnnual_Health_Deductible.Text;
            bSQAUI.In_network_MOOP_Amount = txtIn_network_MOOP_Amount.Text;
            bSQAUI.Annual_Drug_Deductible = txtAnnual_Drug_Deductible.Text;
            #endregion Plan_Features                                    

            #region Inpatient_Hospital
            string inpatientHospitalEnhancedBenefits = string.Empty;

            for (int i = 0; i < ddlpbp_b1a_bendesc_ad_up_nmcs.Items.Count; i++)
            {
                if (ddlpbp_b1a_bendesc_ad_up_nmcs.Items[i].Checked)
                {
                    inpatientHospitalEnhancedBenefits = inpatientHospitalEnhancedBenefits + "1";
                }
                else
                {
                    inpatientHospitalEnhancedBenefits = inpatientHospitalEnhancedBenefits + "0";
                }
            }

            bSQAUI.pbp_b1a_bendesc_ad_up_nmcs = inpatientHospitalEnhancedBenefits;
            bSQAUI.pbp_b1a_bendesc_amt_ad = txtpbp_b1a_bendesc_amt_ad.Text;
            bSQAUI.pbp_b1a_copay_mcs_amt_t1 = txtpbp_b1a_copay_mcs_amt_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_amt_int1_t1 = txtpbp_b1a_copay_mcs_amt_int1_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_amt_int2_t1 = txtpbp_b1a_copay_mcs_amt_int2_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_amt_int3_t1 = txtpbp_b1a_copay_mcs_amt_int3_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_bgnd_int1_t1 = txtpbp_b1a_copay_mcs_bgnd_int1_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_bgnd_int2_t1 = txtpbp_b1a_copay_mcs_bgnd_int2_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_bgnd_int3_t1 = txtpbp_b1a_copay_mcs_bgnd_int3_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_endd_int1_t1 = txtpbp_b1a_copay_mcs_endd_int1_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_endd_int2_t1 = txtpbp_b1a_copay_mcs_endd_int2_t1.Text;
            bSQAUI.pbp_b1a_copay_mcs_endd_int3_t1 = txtpbp_b1a_copay_mcs_endd_int3_t1.Text;
            #endregion Inpatient_Hospital 

            #region PCP_Specialist_ER         
            bSQAUI.pbp_b7a_coins_pct_mc_min = txtpbp_b7a_coins_pct_mc_min.Text;
            bSQAUI.pbp_b7d_coins_pct_mc_min = txtpbp_b7d_coins_pct_mc_min.Text;
            bSQAUI.pbp_b4a_coins_pct_mc_min = txtpbp_b4a_coins_pct_mc_min.Text;
            bSQAUI.pbp_b7a_copay_amt_mc_min = txtpbp_b7a_copay_amt_mc_min.Text;
            bSQAUI.pbp_b7d_copay_amt_mc_min = txtpbp_b7d_copay_amt_mc_min.Text;
            bSQAUI.pbp_b4a_copay_amt_mc_min = txtpbp_b4a_copay_amt_mc_min.Text;
            #endregion PCP_Specialist_ER

            #region Outpatient_Medicare_Services        
            bSQAUI.pbp_b8a_coins_pct_lab = txtpbp_b8a_coins_pct_lab.Text;
            bSQAUI.pbp_b8a_coins_pct_dmc = txtpbp_b8a_coins_pct_dmc.Text;
            bSQAUI.pbp_b8b_coins_pct_cmc = txtpbp_b8b_coins_pct_cmc.Text;
            bSQAUI.pbp_b8b_coins_pct_drs = txtpbp_b8b_coins_pct_drs.Text;
            bSQAUI.pbp_b8b_coins_pct_tmc = txtpbp_b8b_coins_pct_tmc.Text;
            bSQAUI.pbp_b8a_lab_copay_amt = txtpbp_b8a_lab_copay_amt.Text;
            bSQAUI.pbp_b8a_copay_min_dmc_amt = txtpbp_b8a_copay_min_dmc_amt.Text;
            bSQAUI.pbp_b8b_copay_mc_amt = txtpbp_b8b_copay_mc_amt.Text;
            bSQAUI.pbp_b8b_copay_amt_drs = txtpbp_b8b_copay_amt_drs.Text;
            bSQAUI.pbp_b8b_copay_amt_tmc = txtpbp_b8b_copay_amt_tmc.Text;
            #endregion Outpatient_Medicare_Services

            #region Outpatient_Blood_Services   

            if (rdpbp_b9d_bendesc_yn.SelectedValue == "Yes")
            {
                bSQAUI.pbp_b9d_bendesc_yn = "1";
            }
            else if (rdpbp_b9d_bendesc_yn.SelectedValue == "No")
            {
                bSQAUI.pbp_b9d_bendesc_yn = "2";
            }
            else
            {
                bSQAUI.pbp_b9d_bendesc_yn = "";
            }

            if (chkpbp_b9d_coins_yn.Checked == true)
            {
                bSQAUI.pbp_b9d_coins_yn = "1";
                bSQAUI.pbp_b9d_coins_pct_mc_min = txtpbp_b9d_coins_pct_mc_min.Text;
            }
            else
            {
                bSQAUI.pbp_b9d_coins_yn = "2";
                bSQAUI.pbp_b9d_coins_pct_mc_min = "";
            }

            if (chkpbp_b9d_copay_yn.Checked == true)
            {
                bSQAUI.pbp_b9d_copay_yn = "1";
                bSQAUI.pbp_b9d_copay_mc_amt_min = txtpbp_b9d_copay_mc_amt_min.Text;
            }
            else
            {
                bSQAUI.pbp_b9d_copay_yn = "2";
                bSQAUI.pbp_b9d_copay_mc_amt_min = "";
            }

            #endregion Outpatient_Blood_Services   

            #region OTC

            if (rdpbp_b13b_bendesc_otc.SelectedValue == "Yes")
            {
                bSQAUI.pbp_b13b_bendesc_otc = "1";
                bSQAUI.pbp_b13b_maxplan_amt = txtpbp_b13b_maxplan_amt.Text;

                if (ddlpbp_b13b_otc_maxplan_per.SelectedValue == "Select")
                {
                    bSQAUI.pbp_b13b_otc_maxplan_per = "";
                }
                else
                {
                    bSQAUI.pbp_b13b_otc_maxplan_per = ddlpbp_b13b_otc_maxplan_per.SelectedValue;
                }

            }
            else if (rdpbp_b13b_bendesc_otc.SelectedValue == "No")
            {
                bSQAUI.pbp_b13b_bendesc_otc = "2";
                bSQAUI.pbp_b13b_maxplan_amt = "";
                bSQAUI.pbp_b13b_otc_maxplan_per = "";
            }
            else
            {
                bSQAUI.pbp_b13b_bendesc_otc = "";
                bSQAUI.pbp_b13b_maxplan_amt = "";
                bSQAUI.pbp_b13b_otc_maxplan_per = "";
            }

            #endregion OTC

            #region PreventiveDental   

            if (rdpbp_b16a_bendesc_yn.SelectedValue == "Yes")
            {
                bSQAUI.pbp_b16a_bendesc_yn = "1";

                string preventiveDentalEnhancedBenefits = string.Empty;

                for (int i = 0; i < ddlpbp_b16a_bendesc_ehc.Items.Count; i++)
                {
                    if (ddlpbp_b16a_bendesc_ehc.Items[i].Checked)
                    {
                        preventiveDentalEnhancedBenefits = preventiveDentalEnhancedBenefits + "1";
                    }
                    else
                    {
                        preventiveDentalEnhancedBenefits = preventiveDentalEnhancedBenefits + "0";
                    }
                }

                bSQAUI.pbp_b16a_bendesc_ehc = preventiveDentalEnhancedBenefits;
                bSQAUI.pbp_b16a_maxplan_amt = txtpbp_b16a_maxplan_amt.Text;

                if (ddlpbp_b13b_otc_maxplan_per.SelectedValue == "Select")
                {
                    bSQAUI.pbp_b16a_maxplan_per = "";
                }
                else
                {
                    bSQAUI.pbp_b16a_maxplan_per = ddlpbp_b16a_maxplan_per.SelectedValue;
                }

                bSQAUI.pbp_b16a_bendesc_numv_oe = txtpbp_b16a_bendesc_numv_oe.Text;
                bSQAUI.pbp_b16a_bendesc_numv_dx = txtpbp_b16a_bendesc_numv_dx.Text;
                bSQAUI.pbp_b16a_bendesc_numv_pc = txtpbp_b16a_bendesc_numv_pc.Text;
                bSQAUI.pbp_b16a_bendesc_numv_ft = txtpbp_b16a_bendesc_numv_ft.Text;

                if (ddlpbp_b16a_bendesc_per_oe.SelectedValue == "Select")
                {
                    bSQAUI.pbp_b16a_bendesc_per_oe = "";
                }
                else
                {
                    bSQAUI.pbp_b16a_bendesc_per_oe = ddlpbp_b16a_bendesc_per_oe.SelectedValue;
                }

                if (ddlpbp_b16a_bendesc_per_dx.SelectedValue == "Select")
                {
                    bSQAUI.pbp_b16a_bendesc_per_dx = "";
                }
                else
                {
                    bSQAUI.pbp_b16a_bendesc_per_dx = ddlpbp_b16a_bendesc_per_dx.SelectedValue;
                }

                if (ddlpbp_b16a_bendesc_per_pc.SelectedValue == "Select")
                {
                    bSQAUI.pbp_b16a_bendesc_per_pc = "";
                }
                else
                {
                    bSQAUI.pbp_b16a_bendesc_per_pc = ddlpbp_b16a_bendesc_per_pc.SelectedValue;
                }

                if (ddlpbp_b16a_bendesc_per_ft.SelectedValue == "Select")
                {
                    bSQAUI.pbp_b16a_bendesc_per_ft = "";
                }
                else
                {
                    bSQAUI.pbp_b16a_bendesc_per_ft = ddlpbp_b16a_bendesc_per_ft.SelectedValue;
                }
            }
            else if (rdpbp_b16a_bendesc_yn.SelectedValue == "No")
            {
                bSQAUI.pbp_b16a_bendesc_yn = "2";

                bSQAUI.pbp_b16a_bendesc_ehc = "";
                bSQAUI.pbp_b16a_maxplan_amt = "";
                bSQAUI.pbp_b16a_maxplan_per = "";
                bSQAUI.pbp_b16a_bendesc_numv_oe = "";
                bSQAUI.pbp_b16a_bendesc_numv_dx = "";
                bSQAUI.pbp_b16a_bendesc_numv_pc = "";
                bSQAUI.pbp_b16a_bendesc_numv_ft = "";
                bSQAUI.pbp_b16a_bendesc_per_oe = "";
                bSQAUI.pbp_b16a_bendesc_per_dx = "";
                bSQAUI.pbp_b16a_bendesc_per_pc = "";
                bSQAUI.pbp_b16a_bendesc_per_ft = "";
            }
            else
            {
                bSQAUI.pbp_b16a_bendesc_yn = "";

                bSQAUI.pbp_b16a_bendesc_ehc = "";
                bSQAUI.pbp_b16a_maxplan_amt = "";
                bSQAUI.pbp_b16a_maxplan_per = "";
                bSQAUI.pbp_b16a_bendesc_numv_oe = "";
                bSQAUI.pbp_b16a_bendesc_numv_dx = "";
                bSQAUI.pbp_b16a_bendesc_numv_pc = "";
                bSQAUI.pbp_b16a_bendesc_numv_ft = "";
                bSQAUI.pbp_b16a_bendesc_per_oe = "";
                bSQAUI.pbp_b16a_bendesc_per_dx = "";
                bSQAUI.pbp_b16a_bendesc_per_pc = "";
                bSQAUI.pbp_b16a_bendesc_per_ft = "";
            }

            #endregion PreventiveDental   

            #region ComprehensiveDental   

            if (rdpbp_b16b_bendesc_yn.SelectedValue == "Yes")
            {
                bSQAUI.pbp_b16b_bendesc_yn = "1";

                string comprehensiveDentalEnhancedBenefits = string.Empty;

                for (int i = 0; i < ddlpbp_b16b_bendesc_ehc.Items.Count; i++)
                {
                    if (ddlpbp_b16b_bendesc_ehc.Items[i].Checked)
                    {
                        comprehensiveDentalEnhancedBenefits = comprehensiveDentalEnhancedBenefits + "1";
                    }
                    else
                    {
                        comprehensiveDentalEnhancedBenefits = comprehensiveDentalEnhancedBenefits + "0";
                    }
                }

                bSQAUI.pbp_b16b_bendesc_ehc = comprehensiveDentalEnhancedBenefits;

                if (rdpbp_b16b_maxplan_yn.SelectedValue == "Yes")
                {
                    bSQAUI.pbp_b16b_maxplan_yn = "1";
                    if (ddlpbp_b16b_maxbene_type.SelectedValue == "Select")
                    {
                        bSQAUI.pbp_b16b_maxbene_type = "";
                    }
                    else
                    {
                        bSQAUI.pbp_b16b_maxbene_type = ddlpbp_b16b_maxbene_type.SelectedValue;
                    }

                    bSQAUI.pbp_b16b_maxplan_amt = txtpbp_b16b_maxplan_amt.Text;

                    if (ddlpbp_b16b_maxplan_per.SelectedValue == "Select")
                    {
                        bSQAUI.pbp_b16b_maxplan_per = "";
                    }
                    else
                    {
                        bSQAUI.pbp_b16b_maxplan_per = ddlpbp_b16b_maxplan_per.SelectedValue;
                    }
                }
                else if (rdpbp_b16b_maxplan_yn.SelectedValue == "No")
                {
                    bSQAUI.pbp_b16b_maxplan_yn = "2";

                    bSQAUI.pbp_b16b_maxbene_type = "";
                    bSQAUI.pbp_b16b_maxplan_amt = "";
                    bSQAUI.pbp_b16b_maxplan_per = "";
                }
                else
                {
                    bSQAUI.pbp_b16b_maxplan_yn = "";

                    bSQAUI.pbp_b16b_maxbene_type = "";
                    bSQAUI.pbp_b16b_maxplan_amt = "";
                    bSQAUI.pbp_b16b_maxplan_per = "";
                }
            }
            else if (rdpbp_b16b_bendesc_yn.SelectedValue == "No")
            {
                bSQAUI.pbp_b16b_bendesc_yn = "2";
                bSQAUI.pbp_b16b_bendesc_ehc = "";

                bSQAUI.pbp_b16b_maxplan_yn = "";

                bSQAUI.pbp_b16b_maxbene_type = "";
                bSQAUI.pbp_b16b_maxplan_amt = "";
                bSQAUI.pbp_b16b_maxplan_per = "";
            }
            else
            {
                bSQAUI.pbp_b16b_bendesc_yn = "";
                bSQAUI.pbp_b16b_bendesc_ehc = "";

                bSQAUI.pbp_b16b_maxplan_yn = "";

                bSQAUI.pbp_b16b_maxbene_type = "";
                bSQAUI.pbp_b16b_maxplan_amt = "";
                bSQAUI.pbp_b16b_maxplan_per = "";
            }
            #endregion ComprehensiveDental   

            #region Eyewear   

            if (rdpbp_b17b_bendesc_yn.SelectedValue == "Yes")
            {
                bSQAUI.pbp_b17b_bendesc_yn = "1";

                string eyewearEnhancedBenefits = string.Empty;

                for (int i = 0; i < ddlpbp_b17b_bendesc_ehc.Items.Count; i++)
                {
                    if (ddlpbp_b17b_bendesc_ehc.Items[i].Checked)
                    {
                        eyewearEnhancedBenefits = eyewearEnhancedBenefits + "1";
                    }
                    else
                    {
                        eyewearEnhancedBenefits = eyewearEnhancedBenefits + "0";
                    }
                }

                bSQAUI.pbp_b17b_bendesc_ehc = eyewearEnhancedBenefits;

                if (rdpbp_b17b_maxplan_yn.SelectedValue == "Yes")
                {
                    bSQAUI.pbp_b17b_maxplan_yn = "1";

                    if (ddlpbp_b17b_maxplan_type.SelectedValue == "Select")
                    {
                        bSQAUI.pbp_b17b_maxplan_type = "";
                    }
                    else
                    {
                        bSQAUI.pbp_b17b_maxplan_type = ddlpbp_b17b_maxplan_type.SelectedValue;
                    }

                    bSQAUI.pbp_b17b_comb_maxplan_amt = txtpbp_b17b_comb_maxplan_amt.Text;

                    if (ddlpbp_b17b_comb_maxplan_per.SelectedValue == "Select")
                    {
                        bSQAUI.pbp_b17b_comb_maxplan_per = "";
                    }
                    else
                    {
                        bSQAUI.pbp_b17b_comb_maxplan_per = ddlpbp_b17b_comb_maxplan_per.SelectedValue;
                    }
                }
                else if (rdpbp_b17b_maxplan_yn.SelectedValue == "No")
                {
                    bSQAUI.pbp_b17b_maxplan_yn = "2";

                    bSQAUI.pbp_b17b_maxplan_type = "";
                    bSQAUI.pbp_b17b_comb_maxplan_amt = "";
                    bSQAUI.pbp_b17b_comb_maxplan_per = "";
                }
                else
                {
                    bSQAUI.pbp_b17b_maxplan_yn = "";

                    bSQAUI.pbp_b17b_maxplan_type = "";
                    bSQAUI.pbp_b17b_comb_maxplan_amt = "";
                    bSQAUI.pbp_b17b_comb_maxplan_per = "";
                }
            }
            else if (rdpbp_b17b_bendesc_yn.SelectedValue == "No")
            {
                bSQAUI.pbp_b17b_bendesc_yn = "2";

                bSQAUI.pbp_b17b_bendesc_ehc = "";

                bSQAUI.pbp_b17b_maxplan_yn = "";

                bSQAUI.pbp_b17b_maxplan_type = "";
                bSQAUI.pbp_b17b_comb_maxplan_amt = "";
                bSQAUI.pbp_b17b_comb_maxplan_per = "";
            }
            else
            {
                bSQAUI.pbp_b17b_bendesc_yn = "";

                bSQAUI.pbp_b17b_bendesc_ehc = "";

                bSQAUI.pbp_b17b_maxplan_yn = "";

                bSQAUI.pbp_b17b_maxplan_type = "";
                bSQAUI.pbp_b17b_comb_maxplan_amt = "";
                bSQAUI.pbp_b17b_comb_maxplan_per = "";

            }
            #endregion Eyewear   

            #region Hearing Aid   

            if (rdpbp_b18b_bendesc_yn.SelectedValue == "Yes")
            {
                bSQAUI.pbp_b18b_bendesc_yn = "1";

                string hearingAidEnhancedBenefits = string.Empty;

                for (int i = 0; i < ddlpbp_b18b_bendesc_ehc.Items.Count; i++)
                {
                    if (ddlpbp_b18b_bendesc_ehc.Items[i].Checked)
                    {
                        hearingAidEnhancedBenefits = hearingAidEnhancedBenefits + "1";
                    }
                    else
                    {
                        hearingAidEnhancedBenefits = hearingAidEnhancedBenefits + "0";
                    }
                }

                bSQAUI.pbp_b18b_bendesc_ehc = hearingAidEnhancedBenefits;

                if (rdpbp_b18b_maxplan_yn.SelectedValue == "Yes")
                {
                    bSQAUI.pbp_b18b_maxplan_yn = "1";
                    if (ddlpbp_b18b_maxplan_perear.SelectedValue == "Select")
                    {
                        bSQAUI.pbp_b18b_maxplan_perear = "";
                    }
                    else
                    {
                        bSQAUI.pbp_b18b_maxplan_perear = ddlpbp_b18b_maxplan_perear.SelectedValue;
                    }

                    if (ddlpbp_b18b_maxplan_type.SelectedValue == "Select")
                    {
                        bSQAUI.pbp_b18b_maxplan_type = "";
                    }
                    else
                    {
                        bSQAUI.pbp_b18b_maxplan_type = ddlpbp_b18b_maxplan_type.SelectedValue;
                    }

                    bSQAUI.pbp_b18b_maxplan_amt = txtpbp_b18b_maxplan_amt.Text;

                    if (ddlpbp_b18b_maxplan_per.SelectedValue == "Select")
                    {
                        bSQAUI.pbp_b18b_maxplan_per = "";
                    }
                    else
                    {
                        bSQAUI.pbp_b18b_maxplan_per = ddlpbp_b18b_maxplan_per.SelectedValue;
                    }
                }
                else if (rdpbp_b18b_maxplan_yn.SelectedValue == "No")
                {
                    bSQAUI.pbp_b18b_maxplan_yn = "2";
                    bSQAUI.pbp_b18b_maxplan_perear = "";
                    bSQAUI.pbp_b18b_maxplan_type = "";
                    bSQAUI.pbp_b18b_maxplan_amt = "";
                    bSQAUI.pbp_b18b_maxplan_per = "";
                }
                else
                {
                    bSQAUI.pbp_b18b_maxplan_yn = "";
                    bSQAUI.pbp_b18b_maxplan_perear = "";
                    bSQAUI.pbp_b18b_maxplan_type = "";
                    bSQAUI.pbp_b18b_maxplan_amt = "";
                    bSQAUI.pbp_b18b_maxplan_per = "";
                }
            }
            else if (rdpbp_b18b_bendesc_yn.SelectedValue == "No")
            {
                bSQAUI.pbp_b18b_bendesc_yn = "2";

                bSQAUI.pbp_b18b_bendesc_ehc = "";

                bSQAUI.pbp_b18b_maxplan_yn = "";

                bSQAUI.pbp_b18b_bendesc_ehc = "";
                bSQAUI.pbp_b18b_maxplan_perear = "";
                bSQAUI.pbp_b18b_maxplan_type = "";
                bSQAUI.pbp_b18b_maxplan_amt = "";
                bSQAUI.pbp_b18b_maxplan_per = "";
            }
            else
            {
                bSQAUI.pbp_b18b_bendesc_yn = "";

                bSQAUI.pbp_b18b_bendesc_ehc = "";

                bSQAUI.pbp_b18b_maxplan_yn = "";

                bSQAUI.pbp_b18b_bendesc_ehc = "";
                bSQAUI.pbp_b18b_maxplan_perear = "";
                bSQAUI.pbp_b18b_maxplan_type = "";
                bSQAUI.pbp_b18b_maxplan_amt = "";
                bSQAUI.pbp_b18b_maxplan_per = "";
            }
            #endregion Hearing Aid   

            BenefitSimulatorQuickAccessUserInputMethods objBSQAUIM = new BenefitSimulatorQuickAccessUserInputMethods();
            objBSQAUIM.Insert(bSQAUI);
        }

        private string RunSimulate()
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
                ProcessStartInfo processStartInfo = new ProcessStartInfo(Constants.pythonExeFileLocation);
                processStartInfo.Arguments = string.Format("{0} {1} {2}", Constants.pythonProgramFile, Session["BenefitSimulatorScenarioID"].ToString(),Constants.tableauSiteName);
                processStartInfo.UseShellExecute = false;
                processStartInfo.RedirectStandardOutput = true;
                Process p = Process.Start(processStartInfo);
                StreamReader s = p.StandardOutput;
                p.WaitForExit();
                int exitCode = p.ExitCode;


                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += "Exited Code " + exitCode + " , ID=" + Session["BenefitSimulatorScenarioID"].ToString();
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }

                return p.ExitCode.ToString();

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
                return "1";
            }           
        }

        protected void btnSimulate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                var output = RunSimulate();

                if (output != "0")
                {
                    BenefitSimulatorOutputResultMethods benefitSimulatorOutputResultMethods = new BenefitSimulatorOutputResultMethods();
                    benefitSimulatorOutputResultMethods.DeleteBenefitSimulatorOutputResult(Convert.ToInt32(Session["BenefitSimulatorScenarioID"]));
                }

                ModalProgress.Hide();
                PT.InsertDataIntoDB("BenefitSimulatorOutput", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManageSimulatedOutput.aspx");
                Response.Redirect("~/Pages/ManageSimulatedOutput.aspx", false);

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
                CreateScenarioDetailMethods createScenarioDetailMethods = new CreateScenarioDetailMethods();
                string Scenario = txtScenario.Text.ToString();
                string ScenarioDesc = txtScenarioDesc.Text.ToString();
                string CreatedBy = Session["UserName"].ToString();
                var oldScenarioID = Convert.ToInt32(Session["BenefitSimulatorScenarioID"]);
                int result = createScenarioDetailMethods.InsertScenario(Scenario, ScenarioDesc, CreatedBy, "-", "-");
                if (result != 0)
                {
                    scenarioExists.Visible = false;
                    Session["BenefitSimulatorScenarioID"] = result;
                    Session["BenefitSimulatorScenarioName"] = txtScenario.Text;
                    lblScenarioName.Text = txtScenario.Text;
                    txtScenario.Text = "";
                    txtScenarioDesc.Text = "";

                    benefitSimulatorPlansUserInputsMethods.InsertPlansUserInputsDetails(oldScenarioID, Convert.ToInt32(Session["BenefitSimulatorScenarioID"]));
                    SaveData();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The scenario is saved sucessfully.');", true);
                }
                else
                {
                    scenarioExists.Visible = true;
                }
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void lbRevert_Click(object sender, EventArgs e)
        {
            BindQuickAccessData(Convert.ToInt32(Session["BenefitSimulatorScenarioID"]));
            BindBenefitSimulatorPlansUserInputsPlanName(Convert.ToInt32(Session["BenefitSimulatorScenarioID"]));
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            SaveData();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Saved sucessfully.');", true);
        }

        protected void lbSaveAs_Click(object sender, EventArgs e)
        {
            txtScenario.Text = "";
            txtScenarioDesc.Text = "";
            scenarioExists.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet quickAccessDownloadDS = benefitSimulatorQuickAccessMethods.GetBenefitSimulatorQuickAccessDownload(Convert.ToInt32(Session["BenefitSimulatorScenarioID"]));

                if (quickAccessDownloadDS.Tables[0].Rows.Count > 0)
                {
                    string attachment = "attachment; filename=QuickAccess.csv";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "text/csv";
                    string tab = "";
                    foreach (DataColumn dc in quickAccessDownloadDS.Tables[0].Columns)
                    {
                        Response.Write(tab + dc.ColumnName);
                        tab = ",";
                    }
                    Response.Write("\n");
                    int i;
                    foreach (DataRow dr in quickAccessDownloadDS.Tables[0].Rows)
                    {
                        tab = "";
                        for (i = 0; i < quickAccessDownloadDS.Tables[0].Columns.Count; i++)
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
    }
}