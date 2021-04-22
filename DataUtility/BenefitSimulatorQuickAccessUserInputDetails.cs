using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtility
{
   public class BenefitSimulatorQuickAccessUserInputDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
       
        public void InsertIntoDB(dynamic bSQAUI)
        {           
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorQuickAccessUserInputs_Save", sqlConnection);             
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ScenarioID", bSQAUI.ScenarioID);
                sqlCommand.Parameters.AddWithValue("@bid_id", bSQAUI.bid_id);
                               
                #region Plan_Features

                if(string.IsNullOrEmpty(bSQAUI.Monthly_Consolidated_Premium_C_D))
                {
                    sqlCommand.Parameters.AddWithValue("@Monthly_Consolidated_Premium_C_D", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@Monthly_Consolidated_Premium_C_D", bSQAUI.Monthly_Consolidated_Premium_C_D);
                }

                if (string.IsNullOrEmpty(bSQAUI.Annual_Health_Deductible))
                {
                    sqlCommand.Parameters.AddWithValue("@Annual_Health_Deductible", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@Annual_Health_Deductible", bSQAUI.Annual_Health_Deductible);
                }

                if (string.IsNullOrEmpty(bSQAUI.In_network_MOOP_Amount))
                {
                    sqlCommand.Parameters.AddWithValue("@In_network_MOOP_Amount", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@In_network_MOOP_Amount", bSQAUI.In_network_MOOP_Amount);
                }

                if (string.IsNullOrEmpty(bSQAUI.Annual_Drug_Deductible))
                {
                    sqlCommand.Parameters.AddWithValue("@Annual_Drug_Deductible", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@Annual_Drug_Deductible", bSQAUI.Annual_Drug_Deductible);
                }

                #endregion Plan_Features                                    

                #region Inpatient_Hospital   

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_bendesc_ad_up_nmcs))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_bendesc_ad_up_nmcs", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_bendesc_ad_up_nmcs", bSQAUI.pbp_b1a_bendesc_ad_up_nmcs);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_bendesc_amt_ad))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_bendesc_amt_ad", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_bendesc_amt_ad", bSQAUI.pbp_b1a_bendesc_amt_ad);
                }
                                             
                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_amt_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_t1", bSQAUI.pbp_b1a_copay_mcs_amt_t1);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_amt_int1_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int1_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int1_t1", bSQAUI.pbp_b1a_copay_mcs_amt_int1_t1);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_amt_int2_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int2_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int2_t1", bSQAUI.pbp_b1a_copay_mcs_amt_int2_t1);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_amt_int3_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int3_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_amt_int3_t1", bSQAUI.pbp_b1a_copay_mcs_amt_int3_t1);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_bgnd_int1_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int1_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int1_t1", bSQAUI.pbp_b1a_copay_mcs_bgnd_int1_t1);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_bgnd_int2_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int2_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int2_t1", bSQAUI.pbp_b1a_copay_mcs_bgnd_int2_t1);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_bgnd_int3_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int3_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_bgnd_int3_t1", bSQAUI.pbp_b1a_copay_mcs_bgnd_int3_t1);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_endd_int1_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int1_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int1_t1", bSQAUI.pbp_b1a_copay_mcs_endd_int1_t1);
                }


                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_endd_int2_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int2_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int2_t1", bSQAUI.pbp_b1a_copay_mcs_endd_int2_t1);
                }


                if (string.IsNullOrEmpty(bSQAUI.pbp_b1a_copay_mcs_endd_int3_t1))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int3_t1", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b1a_copay_mcs_endd_int3_t1", bSQAUI.pbp_b1a_copay_mcs_endd_int3_t1);
                }

                #endregion Inpatient_Hospital 

                #region PCP_Specialist_ER 

                if (string.IsNullOrEmpty(bSQAUI.pbp_b7a_coins_pct_mc_min))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b7a_coins_pct_mc_min", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b7a_coins_pct_mc_min", bSQAUI.pbp_b7a_coins_pct_mc_min);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b7a_copay_amt_mc_min))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b7a_copay_amt_mc_min", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b7a_copay_amt_mc_min", bSQAUI.pbp_b7a_copay_amt_mc_min);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b7d_coins_pct_mc_min))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b7d_coins_pct_mc_min", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b7d_coins_pct_mc_min", bSQAUI.pbp_b7d_coins_pct_mc_min);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b7d_copay_amt_mc_min))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b7d_copay_amt_mc_min", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b7d_copay_amt_mc_min", bSQAUI.pbp_b7d_copay_amt_mc_min);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b4a_coins_pct_mc_min))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b4a_coins_pct_mc_min", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b4a_coins_pct_mc_min", bSQAUI.pbp_b4a_coins_pct_mc_min);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b4a_copay_amt_mc_min))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b4a_copay_amt_mc_min", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b4a_copay_amt_mc_min", bSQAUI.pbp_b4a_copay_amt_mc_min);
                }

                #endregion PCP_Specialist_ER

                #region Outpatient_Medicare_Services  

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8a_coins_pct_lab))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8a_coins_pct_lab", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8a_coins_pct_lab", bSQAUI.pbp_b8a_coins_pct_lab);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8a_lab_copay_amt))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8a_lab_copay_amt", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8a_lab_copay_amt", bSQAUI.pbp_b8a_lab_copay_amt);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8a_coins_pct_dmc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8a_coins_pct_dmc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8a_coins_pct_dmc", bSQAUI.pbp_b8a_coins_pct_dmc);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8a_copay_min_dmc_amt))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8a_copay_min_dmc_amt", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8a_copay_min_dmc_amt", bSQAUI.pbp_b8a_copay_min_dmc_amt);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8b_coins_pct_cmc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_coins_pct_cmc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_coins_pct_cmc", bSQAUI.pbp_b8b_coins_pct_cmc);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8b_copay_mc_amt))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_copay_mc_amt", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_copay_mc_amt", bSQAUI.pbp_b8b_copay_mc_amt);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8b_coins_pct_drs))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_coins_pct_drs", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_coins_pct_drs", bSQAUI.pbp_b8b_coins_pct_drs);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8b_copay_amt_drs))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_copay_amt_drs", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_copay_amt_drs", bSQAUI.pbp_b8b_copay_amt_drs);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8b_coins_pct_tmc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_coins_pct_tmc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_coins_pct_tmc", bSQAUI.pbp_b8b_coins_pct_tmc);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b8b_copay_amt_tmc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_copay_amt_tmc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b8b_copay_amt_tmc", bSQAUI.pbp_b8b_copay_amt_tmc);
                }

                #endregion Outpatient_Medicare_Services

                #region Outpatient_Blood_Services   

                if (string.IsNullOrEmpty(bSQAUI.pbp_b9d_bendesc_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_bendesc_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_bendesc_yn", bSQAUI.pbp_b9d_bendesc_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b9d_coins_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_coins_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_coins_yn", bSQAUI.pbp_b9d_coins_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b9d_coins_pct_mc_min))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_coins_pct_mc_min", DBNull.Value); 
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_coins_pct_mc_min", bSQAUI.pbp_b9d_coins_pct_mc_min);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b9d_copay_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_copay_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_copay_yn", bSQAUI.pbp_b9d_copay_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b9d_copay_mc_amt_min))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_copay_mc_amt_min", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b9d_copay_mc_amt_min", bSQAUI.pbp_b9d_copay_mc_amt_min);
                }

                #endregion Outpatient_Blood_Services   

                #region OTC

                if (string.IsNullOrEmpty(bSQAUI.pbp_b13b_bendesc_otc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b13b_bendesc_otc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b13b_bendesc_otc", bSQAUI.pbp_b13b_bendesc_otc);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b13b_maxplan_amt))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b13b_maxplan_amt", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b13b_maxplan_amt", bSQAUI.pbp_b13b_maxplan_amt);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b13b_otc_maxplan_per))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b13b_otc_maxplan_per", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b13b_otc_maxplan_per", bSQAUI.pbp_b13b_otc_maxplan_per);
                }

                #endregion OTC

                #region PreventiveDental   

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_yn", bSQAUI.pbp_b16a_bendesc_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_ehc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_ehc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_ehc", bSQAUI.pbp_b16a_bendesc_ehc);
                }
                
                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_maxplan_amt))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_maxplan_amt", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_maxplan_amt", bSQAUI.pbp_b16a_maxplan_amt);
                }
                
                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_maxplan_per))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_maxplan_per", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_maxplan_per", bSQAUI.pbp_b16a_maxplan_per);
                }
                               
                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_numv_oe))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_oe", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_oe", bSQAUI.pbp_b16a_bendesc_numv_oe);
                }


                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_numv_dx))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_dx", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_dx", bSQAUI.pbp_b16a_bendesc_numv_dx);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_numv_pc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_pc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_pc", bSQAUI.pbp_b16a_bendesc_numv_pc);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_numv_ft))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_ft", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_numv_ft", bSQAUI.pbp_b16a_bendesc_numv_ft);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_per_oe))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_per_oe", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_per_oe", bSQAUI.pbp_b16a_bendesc_per_oe);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_per_dx))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_per_dx", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_per_dx", bSQAUI.pbp_b16a_bendesc_per_dx);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_per_ft))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_per_ft", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_per_ft", bSQAUI.pbp_b16a_bendesc_per_ft);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16a_bendesc_per_pc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_per_pc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16a_bendesc_per_pc", bSQAUI.pbp_b16a_bendesc_per_pc);
                }

                #endregion PreventiveDental   

                #region ComprehensiveDental   

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16b_bendesc_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_bendesc_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_bendesc_yn", bSQAUI.pbp_b16b_bendesc_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16b_bendesc_ehc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_bendesc_ehc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_bendesc_ehc", bSQAUI.pbp_b16b_bendesc_ehc);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16b_maxplan_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_maxplan_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_maxplan_yn", bSQAUI.pbp_b16b_maxplan_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16b_maxbene_type))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_maxbene_type", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_maxbene_type", bSQAUI.pbp_b16b_maxbene_type);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16b_maxplan_amt))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_maxplan_amt", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_maxplan_amt", bSQAUI.pbp_b16b_maxplan_amt);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b16b_maxplan_per))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_maxplan_per", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b16b_maxplan_per", bSQAUI.pbp_b16b_maxplan_per);
                }

                #endregion ComprehensiveDental   

                #region Eyewear   

                if (string.IsNullOrEmpty(bSQAUI.pbp_b17b_bendesc_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_bendesc_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_bendesc_yn", bSQAUI.pbp_b17b_bendesc_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b17b_bendesc_ehc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_bendesc_ehc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_bendesc_ehc", bSQAUI.pbp_b17b_bendesc_ehc);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b17b_maxplan_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_maxplan_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_maxplan_yn", bSQAUI.pbp_b17b_maxplan_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b17b_maxplan_type))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_maxplan_type", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_maxplan_type", bSQAUI.pbp_b17b_maxplan_type);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b17b_comb_maxplan_amt))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_comb_maxplan_amt", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_comb_maxplan_amt", bSQAUI.pbp_b17b_comb_maxplan_amt);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b17b_comb_maxplan_per))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_comb_maxplan_per", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b17b_comb_maxplan_per", bSQAUI.pbp_b17b_comb_maxplan_per);
                }

                #endregion Eyewear   

                #region Hearing Aid  

                if (string.IsNullOrEmpty(bSQAUI.pbp_b18b_bendesc_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_bendesc_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_bendesc_yn", bSQAUI.pbp_b18b_bendesc_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b18b_bendesc_ehc))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_bendesc_ehc", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_bendesc_ehc", bSQAUI.pbp_b18b_bendesc_ehc);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b18b_maxplan_yn))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_yn", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_yn", bSQAUI.pbp_b18b_maxplan_yn);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b18b_maxplan_perear))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_perear", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_perear", bSQAUI.pbp_b18b_maxplan_perear);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b18b_maxplan_type))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_type", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_type", bSQAUI.pbp_b18b_maxplan_type);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b18b_maxplan_amt))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_amt", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_amt", bSQAUI.pbp_b18b_maxplan_amt);
                }

                if (string.IsNullOrEmpty(bSQAUI.pbp_b18b_maxplan_per))
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_per", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@pbp_b18b_maxplan_per", bSQAUI.pbp_b18b_maxplan_per);
                }

                #endregion Hearing Aid   
                
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
