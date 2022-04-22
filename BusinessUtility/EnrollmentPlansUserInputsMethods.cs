using DataUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class EnrollmentPlansUserInputsMethods
    {
        EnrollmentSimulatorPlansUserInputsDetails enrollmentSimulatorPlansUserInputs = new EnrollmentSimulatorPlansUserInputsDetails();
        private DataTable dataTable;
        private DataSet dataSet;

        public DataSet GetEnrollmentSimulatorPlansUserInputsPlanName(int ScenarioID)
        {
            dataSet = enrollmentSimulatorPlansUserInputs.GetEnrollmentSimulatorPlansUserInputsPlanNameDB(ScenarioID);
            return dataSet;
        }

        public int SaveEnrollmentSimulatorPlanUserInputs(EnrollmentPlansUserInputs enrollmentPlansUserInputs)
        {
            int NewRowIdentity = enrollmentSimulatorPlansUserInputs.SaveEnrollmentSimulatorPlanUserInputsDB(enrollmentPlansUserInputs);
            return NewRowIdentity;
        }

        public DataTable GetEnrollmentSimulatorSavedPlans(int ScenarioID)
        {
            dataTable = enrollmentSimulatorPlansUserInputs.GetEnrollmentSimulatorSavedPlansDB(ScenarioID);
            return dataTable;
        }
    }
}
