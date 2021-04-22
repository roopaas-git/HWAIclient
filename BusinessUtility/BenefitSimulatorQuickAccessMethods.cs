using DataUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class BenefitSimulatorQuickAccessMethods
    {
        BenefitSimulatorQuickAccessDetails benefitSimulatorQuickAccessDetails = new BenefitSimulatorQuickAccessDetails();        
        private DataSet dataSet;

        public DataSet GetBenefitSimulatorQuickAccess(int ScenarioID)
        {
            dataSet = benefitSimulatorQuickAccessDetails.GetBenefitSimulatorQuickAccessDB(ScenarioID);
            return dataSet;
        }

        public DataSet GetBenefitSimulatorQuickAccessDownload(int ScenarioID)
        {
            dataSet = benefitSimulatorQuickAccessDetails.GetBenefitSimulatorQuickAccessDownloadDB(ScenarioID);
            return dataSet;
        }
    }
}
