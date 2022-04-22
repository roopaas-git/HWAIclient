using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtility
{
    public class EnrollmentSimulatorPlansUserInputsDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
        public DataSet dataSet;
        public DataTable dataTable;

        public DataSet GetEnrollmentSimulatorPlansUserInputsPlanNameDB(int ScenarioID)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlansUserInputs_GetPlanName", sqlHWAIEnrollmentSimulatorConnection);
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
                this.CloseHWAIEnrollmentSimulatorConnection();

            }
            return dataSet;
        }

        public int SaveEnrollmentSimulatorPlanUserInputsDB(dynamic enrollmentPlansUserInputs)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlansUserInputs_SavePlan", sqlHWAIEnrollmentSimulatorConnection);

                sqlCommand.Parameters.AddWithValue("@MarketId", enrollmentPlansUserInputs.MarketId);
                sqlCommand.Parameters.AddWithValue("@SubMarketId", enrollmentPlansUserInputs.SubMarketId);
                sqlCommand.Parameters.AddWithValue("@StateIds", enrollmentPlansUserInputs.StateIds);               
                sqlCommand.Parameters.AddWithValue("@SalesRegionIds", enrollmentPlansUserInputs.SalesRegionIds);
                sqlCommand.Parameters.AddWithValue("@CountyIds", enrollmentPlansUserInputs.CountyIds);
                sqlCommand.Parameters.AddWithValue("@FootprintIds", enrollmentPlansUserInputs.FootprintIds);               
                sqlCommand.Parameters.AddWithValue("@PlanCategoryIds", enrollmentPlansUserInputs.PlanCategoryIds);
                sqlCommand.Parameters.AddWithValue("@PremiumIds", enrollmentPlansUserInputs.PremiumIds);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIds", enrollmentPlansUserInputs.PlanTypeIds);   
                sqlCommand.Parameters.AddWithValue("@BidID", enrollmentPlansUserInputs.BidId);
                sqlCommand.Parameters.AddWithValue("@PlanName", enrollmentPlansUserInputs.PlanName);
                sqlCommand.Parameters.AddWithValue("@ScenarioID", enrollmentPlansUserInputs.ScenarioID);

                sqlCommand.Parameters.AddWithValue("@BidLevelStateIds", enrollmentPlansUserInputs.BidLevelStateIds);
                sqlCommand.Parameters.AddWithValue("@BidLevelCountyIds", enrollmentPlansUserInputs.BidLevelCountyIds);
                
                sqlCommand.Parameters.Add("@NewRowIdentity", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                int NewRowIdentity = Convert.ToInt32(sqlCommand.Parameters["@NewRowIdentity"].Value.ToString());
                this.CloseHWAIEnrollmentSimulatorConnection();
                return NewRowIdentity;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();

            }
        }

        public DataTable GetEnrollmentSimulatorSavedPlansDB(int ScenarioID)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlansUserInputs_GetSavedPlans", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@ScenarioID", ScenarioID);
                sqlCommand.CommandType = CommandType.StoredProcedure;
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
