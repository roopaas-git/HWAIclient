using DataUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class BenefitSimulatorPlansUserInputsMethods
    {
        BenefitSimulatorPlansUserInputsDetails BenefitSimulatorPlansUserInputs = new BenefitSimulatorPlansUserInputsDetails();
        private DataSet dataSet;

        public DataSet GetBenefitSimulatorPlansUserInputsPlanName(int ScenarioID)
        {
            dataSet = BenefitSimulatorPlansUserInputs.GetBenefitSimulatorPlansUserInputsPlanNameDB(ScenarioID);
            return dataSet;
        }

        public void InsertPlansUserInputsDetails(int oldScenarioID, int newScenarioID)
        {
            BenefitSimulatorPlansUserInputs.InsertIntoDB(oldScenarioID, newScenarioID);
        }
    }
}
