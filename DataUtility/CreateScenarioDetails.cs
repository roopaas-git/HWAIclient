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
    public class CreateScenarioDetails : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private SqlDataReader sqlDataReader;
        private DataTable dataTable;
        private DataSet dataSet;
        CustomLogger customLogger = new CustomLogger();
        public DataTable LoadScenariosDB(string userName)
        {

            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorScenarios_GetScenario", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserName", userName);
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
        public object DeleteScenarioDB(int sid)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorScenarios_DeleteScenario", sqlConnection);
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
                this.CloseConnection();

            }

            return dataSet;
        }
        public object RemoveAlreadySharedScenarioDB(string SharedUser, string ScenarioName)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorScenarios_RemoveSharedScenario", sqlConnection);
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
                this.CloseConnection();

            }

            return dataSet;
        }
        public int UpdateScenarioDB(int sid, string scenarioName, string description, string createdBy)
        {
            int result = 0;
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorScenarios_UpdateScenario", sqlConnection);
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
                this.CloseConnection();

            }
            return result;
        }
        public int InsertScenarioDB(string scenarioName, string description, string createdBy,string SharedBy,string SharedDesc)
        {
            int result = 0;
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorScenarios_InsertScenario", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ScenarioName", scenarioName);
                sqlCommand.Parameters.AddWithValue("@Description", description);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", createdBy);
                sqlCommand.Parameters.AddWithValue("@SharedBy", SharedBy);
                sqlCommand.Parameters.AddWithValue("@SharedDesc", SharedDesc);
                sqlCommand.Parameters.Add("@NewRowIdentity", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();
                result = Convert.ToInt32(sqlCommand.Parameters["@NewRowIdentity"].Value.ToString());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }
        public object ShareScenarioDB(int OldScenarioID,int NewScenarioID)
        {            
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorScenarios_ShareScenario", sqlConnection);
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
                this.CloseConnection();

            }
            return dataSet;

        }
        public DataTable BindUserNameDB(string UserName)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorScenarios_GetUserName", sqlConnection);
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
    }
}
