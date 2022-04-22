using DataUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class EnrollmentFileUploadMethods
    {
        EnrollmentFileUploadDetails objEnrollmentSimulatorFileUpload = new EnrollmentFileUploadDetails();
        private DataTable dataTable;
        
        public bool Insert(EnrollmentFileUpload enrollmentFileUpload)
        {            
            if (objEnrollmentSimulatorFileUpload.InsertIntoDB(enrollmentFileUpload))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetEnrollmentFileUploadDetails(int ScenarioId)
        {
            dataTable = objEnrollmentSimulatorFileUpload.GetEnrollmentFileUploadDetailsDB(ScenarioId);
            return dataTable;
        }

        public bool UpdateEnrollmentFileUploadFilePath(int scenarioId, string filePath)
        {
            if (objEnrollmentSimulatorFileUpload.UpdateEnrollmentFileUploadFilePathDB(scenarioId, filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
