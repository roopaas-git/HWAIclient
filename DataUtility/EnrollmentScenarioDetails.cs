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
    public class EnrollmentScenarioDetails : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private SqlDataReader sqlDataReader;
        private DataTable dataTable;
        private DataSet dataSet;
        CustomLogger customLogger = new CustomLogger();

        public DataTable LoadEnrollmentScenariosDB(string userName, int ClientId)
        {

            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_GetScenario", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserName", userName);
                sqlCommand.Parameters.AddWithValue("@ClientId", ClientId);
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
        public object DeleteEnrollmentScenarioDB(int sid)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_DeleteScenario", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Sid", sid);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();

            }

            return dataSet;
        }
        public object RemoveAlreadySharedEnrollmentScenarioDB(string SharedUser, string ScenarioName)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_RemoveSharedScenario", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure; ;
                sqlCommand.Parameters.AddWithValue("@SharedUser", SharedUser);
                sqlCommand.Parameters.AddWithValue("@ScenarioName", ScenarioName);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();

            }

            return dataSet;
        }
        public int UpdateEnrollmentScenarioDB(int sid, string scenarioName, string description, string createdBy)
        {
            int result = 0;
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_UpdateScenario", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Sid", sid);
                sqlCommand.Parameters.AddWithValue("@scenarioName", scenarioName);
                sqlCommand.Parameters.AddWithValue("@description", description);
                sqlCommand.Parameters.AddWithValue("@createdBy", createdBy);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                result = sqlDataAdapter.Fill(dataSet);
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
        public int InsertEnrollmentScenarioDB(string scenarioName, string description, string createdBy, string SharedBy, string SharedDesc,string UserName,int status, int clientId)
        {
            int result = 0;
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_InsertScenario", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ScenarioName", scenarioName);
                sqlCommand.Parameters.AddWithValue("@Description", description);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                sqlCommand.Parameters.AddWithValue("@SharedBy", SharedBy);
                sqlCommand.Parameters.AddWithValue("@SharedDesc", SharedDesc);
                sqlCommand.Parameters.AddWithValue("@UserName", UserName);
                sqlCommand.Parameters.AddWithValue("@status", status);
                sqlCommand.Parameters.AddWithValue("@ClientId", clientId);
                sqlCommand.Parameters.Add("@NewRowIdentity", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();
                result = Convert.ToInt32(sqlCommand.Parameters["@NewRowIdentity"].Value.ToString());

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
        public object ShareEnrollmentScenarioDB(int OldScenarioID, int NewScenarioID)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_ShareScenario", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@OldScenarioID", OldScenarioID);
                sqlCommand.Parameters.AddWithValue("@NewScenarioID", NewScenarioID);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();

            }
            return dataSet;

        }
        public DataTable BindUserNameEnrollmentDB(string UserName)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_GetUserName", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserName", UserName);
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
                this.CloseConnection();
            }
            return dataTable;

        }
        public DataTable BindUserDetailsEnrollmentDB()
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulator_GetUsers", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();

                sqlDataAdapter.Fill(dataTable);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseConnection();
            }
            return dataTable;

        }
        public bool UpdateEnrollmentScenarioProcessStatusDB(int sid, byte processStatus)
        {            
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_UpdateScenarioProcessStatus", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Sid", sid);
                sqlCommand.Parameters.AddWithValue("@processStatus", processStatus);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();

            }
        }
        public DataTable GetEnrollmentScenarioDB(int ScenarioId)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_GetScenarioById", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Sid", ScenarioId);
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
        public DataTable GetEnrollmentSimResultsDB(int ScenarioId)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorScenarios_GetSimResults", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Sid", ScenarioId);
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
