using LoggerUtility;
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
    public class EnrollmentQuickAccessDetails : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private SqlDataReader sqlDataReader;
        private DataTable dataTable;
        private DataSet dataSet;
        public int SaveSimulationDB(int ScenarioId, string BidId, int preAEP, int postAEP, int simulatedresults)
        {
            int result = 0;
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulator_SaveSimulation", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ScenarioId", ScenarioId);
                sqlCommand.Parameters.AddWithValue("@BidId", BidId);
                sqlCommand.Parameters.AddWithValue("@preAEP", preAEP);
                sqlCommand.Parameters.AddWithValue("@postAEP", postAEP);
                sqlCommand.Parameters.AddWithValue("@simulatedresults", simulatedresults);       
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
        public string GetBidIdDB(int ScenarioId)
        {
            string result="";
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorQuickAccess_GetBidID", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ScenarioId", ScenarioId);


                result = (string)sqlCommand.ExecuteScalar();

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
        public DataTable UpdateStatusDB(int ScenarioId,int Status)
        {
         
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_UpdateScenarioProcessStatus", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Sid", ScenarioId);
                sqlCommand.Parameters.AddWithValue("@processStatus", Status);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter1 = sqlDataAdapter;
                sqlDataAdapter1.Fill(dataTable);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();
            }
            return dataTable;
        }
    }
}
