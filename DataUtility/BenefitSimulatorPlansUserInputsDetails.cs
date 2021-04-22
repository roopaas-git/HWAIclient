using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtility
{
    public class BenefitSimulatorPlansUserInputsDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
        public DataSet dataSet;

        public DataSet GetBenefitSimulatorPlansUserInputsPlanNameDB(int ScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlansUserInputs_GetPlanName", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Scenario_ID", ScenarioID);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter1 = sqlDataAdapter;
                sqlDataAdapter1.Fill(dataSet);
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

        // used only when new Scenario is created from Quick Access page
        public void InsertIntoDB(int oldScenarioID, int newScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorQuickAccess_Saveas", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@oldScenarioID", oldScenarioID);
                sqlCommand.Parameters.AddWithValue("@newScenarioID", newScenarioID);                             
                
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
