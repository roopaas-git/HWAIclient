using DataUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class BenefitSimulatorQuickAccessUserInputMethods
    {       
        public void Insert(BenefitSimulatorQuickAccessUserInput benefitSimulatorQuickAccessUserInput)
        {
            BenefitSimulatorQuickAccessUserInputDetails objBSQAUID = new BenefitSimulatorQuickAccessUserInputDetails();
            objBSQAUID.InsertIntoDB(benefitSimulatorQuickAccessUserInput);            
        }
    }
}
