using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class BenefitSimulatorQuickAccessUserInput
    {
        public int ID { get; set; }
        public int ScenarioID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string bid_id { get; set; }
        public string Monthly_Consolidated_Premium_C_D { get; set; }
        public string In_network_MOOP_Amount { get; set; }
        public string Annual_Drug_Deductible { get; set; }
        public string Overall_Star_Rating { get; set; }
        public string Annual_Health_Deductible { get; set; }
        public string pbp_b4a_coins_pct_mc_min { get; set; }
        public string pbp_b4a_copay_amt_mc_min { get; set; }
        public string pbp_b7a_coins_pct_mc_min { get; set; }
        public string pbp_b7a_copay_amt_mc_min { get; set; }
        public string pbp_b8a_lab_copay_amt { get; set; }
        public string pbp_b8a_lab_copay_amt_max { get; set; }
        public string pbp_b8a_coins_pct_lab { get; set; }
        public string pbp_b8a_coins_pct_lab_max { get; set; }
        public string pbp_b8a_coins_pct_dmc { get; set; }
        public string pbp_b8a_coins_pct_dmc_max { get; set; }
        public string pbp_b8a_copay_min_dmc_amt { get; set; }
        public string pbp_b8a_copay_max_dmc_amt { get; set; }
        public string pbp_b8b_coins_pct_cmc { get; set; }
        public string pbp_b8b_coins_pct_cmc_max { get; set; }
        public string pbp_b8b_copay_mc_amt { get; set; }
        public string pbp_b8b_copay_mc_amt_max { get; set; }
        public string pbp_b8b_coins_pct_drs { get; set; }
        public string pbp_b8b_coins_pct_drs_max { get; set; }
        public string pbp_b8b_copay_amt_drs { get; set; }
        public string pbp_b8b_copay_amt_drs_max { get; set; }
        public string pbp_b8b_coins_pct_tmc { get; set; }
        public string pbp_b8b_coins_pct_tmc_max { get; set; }
        public string pbp_b8b_copay_amt_tmc { get; set; }
        public string pbp_b8b_copay_amt_tmc_max { get; set; }
        public string pbp_b7d_coins_pct_mc_min { get; set; }
        public string pbp_b7d_copay_amt_mc_min { get; set; }
        public string pbp_b4c_bendesc_yn { get; set; }
        public string pbp_b4c_wwc_maxplan_svcs_yn { get; set; }
        public string pbp_b4c_wwc_maxplan_amt { get; set; }
        public string pbp_b4c_copay_amt_wec_min { get; set; }
        public string pbp_b4c_copay_amt_wec_max { get; set; }
        public string pbp_b9d_bendesc_yn { get; set; }
        public string pbp_b9d_coins_yn { get; set; }
        public string pbp_b9d_coins_pct_mc_min { get; set; }
        public string pbp_b9d_coins_pct_mc_max { get; set; }
        public string pbp_b9d_copay_yn { get; set; }
        public string pbp_b9d_copay_mc_amt_min { get; set; }
        public string pbp_b9d_copay_mc_amt_max { get; set; }
        public string pbp_b13b_bendesc_otc { get; set; }
        public string pbp_b13b_maxplan_amt { get; set; }
        public string pbp_b13b_otc_maxplan_per { get; set; }
        public string pbp_b16a_bendesc_yn { get; set; }
        public string pbp_b16a_bendesc_ehc { get; set; }
        public string pbp_b16a_maxplan_amt { get; set; }
        public string pbp_b16a_maxplan_per { get; set; }
        public string pbp_b16a_bendesc_numv_oe { get; set; }
        public string pbp_b16a_bendesc_per_oe { get; set; }
        public string pbp_b16a_bendesc_numv_pc { get; set; }
        public string pbp_b16a_bendesc_per_pc { get; set; }
        public string pbp_b16a_bendesc_numv_ft { get; set; }
        public string pbp_b16a_bendesc_per_ft { get; set; }
        public string pbp_b16a_bendesc_numv_dx { get; set; }
        public string pbp_b16a_bendesc_per_dx { get; set; }
        public string pbp_b16b_bendesc_yn { get; set; }
        public string pbp_b16b_bendesc_ehc { get; set; }
        public string pbp_b16b_maxplan_yn { get; set; }
        public string pbp_b16b_maxbene_type { get; set; }
        public string pbp_b16b_maxplan_amt { get; set; }
        public string pbp_b16b_maxplan_per { get; set; }
        public string pbp_b17b_bendesc_yn { get; set; }
        public string pbp_b17b_bendesc_ehc { get; set; }
        public string pbp_b17b_maxplan_yn { get; set; }
        public string pbp_b17b_maxplan_type { get; set; }
        public string pbp_b17b_comb_maxplan_amt { get; set; }
        public string pbp_b17b_comb_maxplan_per { get; set; }
        public string pbp_b18b_bendesc_yn { get; set; }
        public string pbp_b18b_bendesc_ehc { get; set; }
        public string pbp_b18b_maxplan_yn { get; set; }
        public string pbp_b18b_maxplan_perear { get; set; }
        public string pbp_b18b_maxplan_type { get; set; }
        public string pbp_b18b_maxplan_amt { get; set; }
        public string pbp_b18b_maxplan_per { get; set; }
        public string pbp_b1a_bendesc_ad_up_nmcs { get; set; }
        public string pbp_b1a_bendesc_amt_ad { get; set; }
        public string pbp_b1a_copay_mcs_amt_t1 { get; set; }
        public string pbp_b1a_copay_mcs_amt_int1_t1 { get; set; }
        public string pbp_b1a_copay_mcs_bgnd_int1_t1 { get; set; }
        public string pbp_b1a_copay_mcs_endd_int1_t1 { get; set; }
        public string pbp_b1a_copay_mcs_amt_int2_t1 { get; set; }
        public string pbp_b1a_copay_mcs_bgnd_int2_t1 { get; set; }
        public string pbp_b1a_copay_mcs_endd_int2_t1 { get; set; }
        public string pbp_b1a_copay_mcs_amt_int3_t1 { get; set; }
        public string pbp_b1a_copay_mcs_bgnd_int3_t1 { get; set; }
        public string pbp_b1a_copay_mcs_endd_int3_t1 { get; set; }
    }
}

