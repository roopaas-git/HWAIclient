using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUtility;

namespace BusinessUtility
{
    public class EnrollmentQuickAccessSimulationMethods
    {
        EnrollmentQuickAccessSimulationDetails enrollmentQuickAccessDetails = new EnrollmentQuickAccessSimulationDetails();
        int result;
        public int SaveChangedResults(DataTable dt)
        {
            result = enrollmentQuickAccessDetails.SaveChangedResultsDB(dt);

            return result;
        }
    }
}
