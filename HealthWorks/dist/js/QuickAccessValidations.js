
       function checkCompare1(getcurrenttxt)
       {
            var currentTextbox = document.getElementById(getcurrenttxt.id);
            var txtBegin1 = document.getElementById("txtBegin1Acute");
            var txtEnd1 = document.getElementById("txtEnd1Acute");
            var txtBegin2 = document.getElementById("txtBegin2Acute");
            var txtEnd2 = document.getElementById("txtEnd2Acute");
            var txtBegin3 = document.getElementById("txtBegin3Acute");
            var txtEnd3 = document.getElementById("txtEnd3Acute");

    if (parseFloat(txtBegin1.value) > parseFloat(txtEnd1.value))
            {
        alert('Begin Day Interval can not be lesser than End Day Interval');
    currentTextbox.value = '';
            }
         else if (parseFloat(txtEnd1.value) > parseFloat(txtBegin2.value))
            {
        alert('Begin Day Interval can not be lesser than End Day Interval');
    currentTextbox.value = '';
}
            else if (parseFloat(txtBegin2.value) > parseFloat(txtEnd2.value)) {
        alert('Begin Day Interval can not be lesser than End Day Interval');
    currentTextbox.value = '';
}
            else if (parseFloat(txtEnd2.value) > parseFloat(txtBegin3.value)) {
        alert('Begin Day Interval can not be lesser than End Day Interval');
    currentTextbox.value = '';
}
            else if (parseFloat(txtBegin3.value) > parseFloat(txtEnd3.value)) {
        alert('Begin Day Interval can not be lesser than End Day Interval');
    currentTextbox.value = '';
}
}

       function checkOutpatientCoins(outpatientcoinstxtid)
            {
            var Outpatientcoins = document.getElementById(outpatientcoinstxtid.id);
            var txtsurcoinsmin = document.getElementById("txtpbp_b9a_coins_ohs_pct_min");
            var txtsurcoinsmax = document.getElementById("txtpbp_b9a_coins_ohs_pct_max");
            var txtobscoinsmin = document.getElementById("txtpbp_b9a_coins_obs_pct_min");
            var txtobscoinsmax = document.getElementById("txtpbp_b9a_coins_obs_pct_max");
            var txtAmbcoins = document.getElementById("txtpbp_b9b_coins_pct_mc");
 
            if (parseFloat(txtsurcoinsmin.value) > parseFloat(txtsurcoinsmax.value))
            {
              alert('Surgery Coinsurance Minimum value cant be greater than Coinsurance Maximum value');
              Outpatientcoins.value = '';
            }
            else if (parseFloat(txtobscoinsmin.value) > parseFloat(txtobscoinsmax.value))
             {
               alert('Observation Coinsurance Minimum value cant be greater than Coinsurance Maximum value');
               Outpatientcoins.value = '';
             }
         }

       function checkOutpatientCopay(outpatientcopaytxtid)
    {

        var Outpatientcopay = document.getElementById(outpatientcopaytxtid.id);
        var txtsurcopaymin =      document.getElementById("txtpbp_b9a_copay_ohs_amt_min");
        var txtsurcopaymax = document.getElementById("txtpbp_b9a_copay_ohs_amt_max");
        var txtobscopaymin = document.getElementById("txtpbp_b9a_copay_obs_amt_min");
        var txtobscopaymax = document.getElementById("txtpbp_b9a_copay_obs_amt_max");
        var txtAmbcopay = document.getElementById("txtpbp_b9b_copay_mc_amt");

        if (parseFloat(txtsurcopaymin.value) > parseFloat(txtsurcopaymax.value))
        {
        alert('Surgery Copayment Minimum value cant be greater than Copayment Maximum value');
        Outpatientcopay.value = '';
        }
        else if (parseFloat(txtobscopaymin.value) > parseFloat(txtobscopaymax.value))
        {
        alert('Observation Copayment Minimum value cant be greater than Copayment Maximum value');
        Outpatientcopay.value = '';
        }
    }

       function checkradiologyCoins(Radiologycoinstxtid)
       {

            var Radiologycoins = document.getElementById(Radiologycoinstxtid.id);

            var txtxraycoinsmin = document.getElementById("txtpbp_b8b_coins_pct_cmc");
            var txtxraycoinsmax = document.getElementById("txtpbp_b8b_coins_pct_cmc_max");
            var txtTherapcoinsmin = document.getElementById("txtpbp_b8b_coins_pct_tmc");
            var txtTherapcoinsmax = document.getElementById("txtpbp_b8b_coins_pct_tmc_max");
            var txtDiagcoinsmin = document.getElementById("txtpbp_b8b_coins_pct_drs");
            var txtDiagcoinsmax = document.getElementById("txtpbp_b8b_coins_pct_drs_max");

           if (parseFloat(txtxraycoinsmin.value) > parseFloat(txtxraycoinsmax.value))
           {
               alert('X-Ray Coinsurance Minimum value cant be greater than Coinsurance Maximum value');
               Radiologycoins.value = '';
           }
           else if (parseFloat(txtTherapcoinsmin.value) > parseFloat(txtTherapcoinsmax.value))
           {
               alert('Therapeutic Radiology Coinsurance Minimum value cant be greater than Coinsurance Maximum value');
               Radiologycoins.value = '';
           }
           else if (parseFloat(txtDiagcoinsmin.value) > parseFloat(txtDiagcoinsmax.value))
           {
               alert('Diagnostic Radiology Coinsurance Minimum value cant be greater than Coinsurance Maximum value');
               Radiologycoins.value = '';
           }
       }

       function checkradiologyCopay(Radiologycopaytxtid)
          {
            var Radiologycopay = document.getElementById(Radiologycopaytxtid.id);
            var txtxraycopaymin = document.getElementById("txtpbp_b8b_copay_mc_amt");
            var txtxraycopaymax = document.getElementById("txtpbp_b8b_copay_mc_amt_max");
            var txtTherapcopaymin = document.getElementById("txtpbp_b8b_copay_amt_tmc");
            var txtTherapcopaymax = document.getElementById("txtpbp_b8b_copay_amt_tmc_max");
            var txtDiagcopaymin = document.getElementById("txtpbp_b8b_copay_amt_drs");
            var txtDiagcopaymax = document.getElementById("txtpbp_b8b_copay_amt_drs_max");

             if (parseFloat(txtxraycopaymin.value) > parseFloat(txtxraycopaymax.value))
             {
               alert('X-Ray Copayment Minimum value cant be greater than Copayment Maximum value');
               Radiologycopay.value = '';
             }
             else if (parseFloat(txtTherapcopaymin.value) > parseFloat(txtTherapcopaymax.value))
             {
               alert('Therapeutic Radiology Copayment Minimum value cant be greater than Copayment Maximum value');
               Radiologycopay.value = '';
             }
             else if (parseFloat(txtDiagcopaymin.value) > parseFloat(txtDiagcopaymax.value))
             {
               alert('Diagnostic Radiology Copayment Minimum value cant be greater than Copayment Maximum value');
               Radiologycopay.value = '';
             }
            }

