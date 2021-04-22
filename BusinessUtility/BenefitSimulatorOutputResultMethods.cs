using DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class BenefitSimulatorOutputResultMethods
    {
        BenefitSimulatorOutputResultDetails benefitSimulatorOutputResultDetails = new BenefitSimulatorOutputResultDetails();
        public object DeleteBenefitSimulatorOutputResult(int scenarioID)
        {
            return benefitSimulatorOutputResultDetails.DeleteBenefitSimulatorOutputResultDB(scenarioID);
        }
    }
}
