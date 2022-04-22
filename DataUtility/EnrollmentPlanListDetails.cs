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
    public class EnrollmentPlanListDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
        public DataTable dataTable;
        public DataSet dataSet;

        public DataSet GetDownloadEnrollmentResultDB(string BidId, string SelectedStates, string SelectedCounties, string SelectedPlanCategorys)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlanList_DownloadRawDataNew", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@BidId", BidId);
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", SelectedCounties);
                sqlCommand.Parameters.AddWithValue("@PlanCategoryIDs", SelectedPlanCategorys);
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

        public DataSet GetRawDataMappingDetailsDB()
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_RawDataMappingDetails", sqlHWAIEnrollmentSimulatorConnection);
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

        public DataSet GetBenefitsForSimulationDB(string BidId, int scenarioId)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulator_GetBenefitsForSimulation", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@BidId", BidId);
                sqlCommand.Parameters.AddWithValue("@ScenarioId", scenarioId);
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

        public DataSet GetFiltersForSimulationDB(int scenarioId, int clientId)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulator_GetFiltersForSimulation", sqlHWAIEnrollmentSimulatorConnection);               
                sqlCommand.Parameters.AddWithValue("@ScenarioId", scenarioId);
                sqlCommand.Parameters.AddWithValue("@ClientId", clientId);
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

        public DataTable GetEnrollmentMarketDB(int ClientId)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetMarket", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@ClientId", ClientId);
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

        public DataTable GetEnrollmentSubMarketDB(int ClientId, int MarketId)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetSubMarket", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@ClientId", ClientId);
                sqlCommand.Parameters.AddWithValue("@MarketID", MarketId);
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

        public DataTable GetEnrollmentStateDB(int ClientId, int SubMarketId)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetState", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@ClientId", ClientId);
                sqlCommand.Parameters.AddWithValue("@SubMarketID", SubMarketId);
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

        public DataTable GetSalesRegionDB(int ClientId, int SubMarketId, string SelectedStates)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetSalesRegion", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@ClientId", ClientId);
                sqlCommand.Parameters.AddWithValue("@SubMarketID", SubMarketId);
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
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

        public DataTable GetEnrollmentCountiesDB(int ClientId, int SubMarketId, string SelectedStates, string SelectedSalesRegions, string SelectedFootprints)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetCounty", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@ClientId", ClientId);
                sqlCommand.Parameters.AddWithValue("@SubMarketID", SubMarketId);
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
                sqlCommand.Parameters.AddWithValue("@SalesRegionIDs", SelectedSalesRegions);
                sqlCommand.Parameters.AddWithValue("@FootprintIDs", SelectedFootprints);
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

        public DataTable GetEnrollmentFootprintDB(int ClientId, int SubMarketId, string SelectedStates, string SelectedSalesRegions)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetFootprint", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ClientId", ClientId);
                sqlCommand.Parameters.AddWithValue("@SubMarketID", SubMarketId);
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
                sqlCommand.Parameters.AddWithValue("@SalesRegionIDs", SelectedSalesRegions);
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

        public DataTable GetEnrollmentPlanCategoryDB(string SelectedStates, string SelectedCounties)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetPlanCategory", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", SelectedCounties);
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
                
        public DataTable GetPremiumDB(string SelectedStates, string SelectedCounties, string SelectedPlanCategories)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetPremium", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;               
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", SelectedCounties);
                sqlCommand.Parameters.AddWithValue("@PlanCategoryIDs", SelectedPlanCategories);
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

        public DataTable GetEnrollmentPlanTypeDB(string SelectedStates, string SelectedCounties, string SelectedPlanCategories, string SelectedPremiums)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetPlanType", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", SelectedCounties);
                sqlCommand.Parameters.AddWithValue("@PlanCategoryIDs", SelectedPlanCategories);
                sqlCommand.Parameters.AddWithValue("@PremiumIDs", SelectedPremiums);
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

        public DataSet GetEnrollmentOrgViewDB(string SelectedStates, string SelectedCounties, string SelectedPlanCategories, string SelectedPremiums, string SelectedPlanTypes)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetOrganisationView", sqlHWAIEnrollmentSimulatorConnection);
                //sqlCommand.Parameters.AddWithValue("@MarketID", SelectedMarket);
                //sqlCommand.Parameters.AddWithValue("@SubMarketID", SelectedSubMarket);
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", SelectedCounties);
                //sqlCommand.Parameters.AddWithValue("@FootprintIDs", SelectedFootprintIDs);             
                sqlCommand.Parameters.AddWithValue("@PlanCategoryIDs", SelectedPlanCategories);
                sqlCommand.Parameters.AddWithValue("@PremiumIDs", SelectedPremiums);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIDs", SelectedPlanTypes);
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

        public DataTable GetEnrollmentCountyViewDB(string PlanBidID, string SelectedStates, string SelectedCounties, string SelectedPlanCategories, string SelectedPremiums, string SelectedPlanTypes)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorPlans_GetCountyView", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.Parameters.AddWithValue("@PlanBidID", PlanBidID);
                sqlCommand.Parameters.AddWithValue("@StateIDs", SelectedStates);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", SelectedCounties);
                sqlCommand.Parameters.AddWithValue("@PlanCategoryIDs", SelectedPlanCategories);
                sqlCommand.Parameters.AddWithValue("@PremiumIDs", SelectedPremiums);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIDs", SelectedPlanTypes);
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
