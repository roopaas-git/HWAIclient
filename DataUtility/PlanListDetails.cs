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
    public class PlanListDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
        public DataTable dataTable;
        public DataSet dataSet;
        public DataTable GetStateDB()
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlans_GetState", sqlConnection);
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
                this.CloseConnection();

            }
            return dataTable;
        }

        public DataTable GetSalesRegionDB(int StateID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlans_GetSalesRegion", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@stateID",StateID);
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
                this.CloseConnection();

            }
            return dataTable;
        }       

        public DataTable GetCountyDB(int StateID,string SalesRegionIDs)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlans_GetCounty", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@StateID", StateID);
                sqlCommand.Parameters.AddWithValue("@SalesRegionIDs", SalesRegionIDs);
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
                this.CloseConnection();

            }
            return dataTable;
        }

        public DataTable GetPlanTypeDB(int StateID, string SalesRegionIDs,string CountyIDs)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlans_GetPlanType", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@StateID", StateID);
                sqlCommand.Parameters.AddWithValue("@SalesRegionIDs", SalesRegionIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
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
                this.CloseConnection();

            }
            return dataTable;
        }

        public DataSet GetOrgViewDB(int StateID, string SalesRegionIDs, string CountyIDs,string PlanTypeIDs, int ScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlans_GetOrganisationView", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@StateID", StateID);
                sqlCommand.Parameters.AddWithValue("@SalesRegionIDs", SalesRegionIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIDs", PlanTypeIDs);
                sqlCommand.Parameters.AddWithValue("@ScenarioID", ScenarioID);
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

        public DataTable GetCountyViewDB( string PlanBidID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlans_GetCountyView", sqlConnection);               
                sqlCommand.Parameters.AddWithValue("@PlanBidID", PlanBidID);
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
                this.CloseConnection();

            }
            return dataTable;
        }

        public DataTable GetSavedPlansDB(int ScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlansUserInputs_GetSavedPlans", sqlConnection);
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
                this.CloseConnection();

            }
            return dataTable;
        }

        public int SavePlanUserInputsDB(int StateID, string SalesRegionIDs, string CountyIDs, string PlanTypeIDs, string BidID, string PlanName, int ScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorPlansUserInputs_SavePlan", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@StateID", StateID);
                sqlCommand.Parameters.AddWithValue("@SalesRegionIDs", SalesRegionIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIDs", PlanTypeIDs);
                sqlCommand.Parameters.AddWithValue("@BidID", BidID);
                sqlCommand.Parameters.AddWithValue("@PlanName", PlanName);                
                sqlCommand.Parameters.AddWithValue("@ScenarioID", ScenarioID);
                sqlCommand.Parameters.Add("@NewRowIdentity", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                int NewRowIdentity = Convert.ToInt32(sqlCommand.Parameters["@NewRowIdentity"].Value.ToString());
                this.CloseConnection();
                return NewRowIdentity;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                this.CloseConnection();

            }
           
        }

        public DataTable GetPagesDB(int ScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulator_GetPages", sqlConnection);
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
                this.CloseConnection();

            }
            return dataTable;
        }

        public DataSet GetOutputResultsDB(int StateID, string SalesRegionIDs, string CountyIDs, string PlanTypeIDs,int ScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorOutputResults_GetOutputResults", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@StateID", StateID);
                sqlCommand.Parameters.AddWithValue("@SalesRegionIDs", SalesRegionIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIDs", PlanTypeIDs);
                sqlCommand.Parameters.AddWithValue("@ScenarioID", ScenarioID);
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
    }
}
