using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataUtility
{
    public class EnrollmentQuickAccessSimulationDetails : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private SqlDataReader sqlDataReader;
        private DataTable dataTable;
        private DataSet dataSet;
        public int SaveChangedResultsDB(DataTable dt)
        {
            int result = 0;
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulator_SaveRawDataUserInputs", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@changedValueTbl", dt);
                result = sqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();
            }
            return result;
        }
    }
}
