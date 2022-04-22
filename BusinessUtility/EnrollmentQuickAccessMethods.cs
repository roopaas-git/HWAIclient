using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUtility;

namespace BusinessUtility
{
    public class EnrollmentQuickAccessMethods
    {
        EnrollmentQuickAccessDetails enrollmentQuickAccessDetails = new EnrollmentQuickAccessDetails();
        int result;
        public int SaveSimulation(int ScenarioId,string BidId,int preAEP,int postAEP,int simulatedresults)
        {
            
                result = enrollmentQuickAccessDetails.SaveSimulationDB(ScenarioId, BidId, preAEP, postAEP, simulatedresults);
            
            return result;
        }
        public string GetBidId(int ScenarioId)
        {
            string result = enrollmentQuickAccessDetails.GetBidIdDB(ScenarioId);
            return result;
        }
        public DataTable UpdateStatus(int ScenarioId, int Status)
        {
            DataTable dt = enrollmentQuickAccessDetails.UpdateStatusDB(ScenarioId, Status);

            return dt;
        }
    }
}
