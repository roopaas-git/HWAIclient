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
    public class ProviderNetworkAnalysisDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
        public DataTable dataTable;
        public DataSet dataSet;

        public DataTable GetMarketDB()
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetMarket", sqlHWConnection);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetSubMarketDB(string MarketIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetSubMarket", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetStateDB(string MarketIDs, string SubMarketIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetState", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubMarketIDs);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetCountyDB(string MarketIDs, string SubMarketIDs, string StateIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetCounty", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubMarketIDs);
                sqlCommand.Parameters.AddWithValue("@StateIDs", StateIDs);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetZipcodeDB(string MarketIDs, string SubMarketIDs, string StateIDs, string CountyIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetZipcode", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubMarketIDs);
                sqlCommand.Parameters.AddWithValue("@StateIDs", StateIDs);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetFootprintDB(string MarketIDs, string SubMarketIDs, string StateIDs, string CountyIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetFootprint", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubMarketIDs);
                sqlCommand.Parameters.AddWithValue("@StateIDs", StateIDs);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetPlanCategoryDB(string MarketIDs, string SubMarketIDs, string StateIDs, string CountyIDs, string FootprintIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetPlanCategory", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubMarketIDs);
                sqlCommand.Parameters.AddWithValue("@StateIDs", StateIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
                sqlCommand.Parameters.AddWithValue("@FootprintIDs", FootprintIDs);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetPlanTypeDB(string MarketIDs, string SubMarketIDs, string StateIDs, string CountyIDs, string FootprintIDs, string PlanCategory)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetPlanType", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubMarketIDs);
                sqlCommand.Parameters.AddWithValue("@StateIDs", StateIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
                sqlCommand.Parameters.AddWithValue("@FootprintIDs", FootprintIDs);
                sqlCommand.Parameters.AddWithValue("@PlanCategory", PlanCategory);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetPlanNamesDB(string MarketIDs, string SubMarketIDs, string StateIDs, string CountyIDs, string FootprintIDs, string PlanCategory, string PlanTypeIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetPlanNames", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubMarketIDs);
                sqlCommand.Parameters.AddWithValue("@StateIDs", StateIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
                sqlCommand.Parameters.AddWithValue("@FootprintIDs", FootprintIDs);
                sqlCommand.Parameters.AddWithValue("@PlanCategory", PlanCategory);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIDs", PlanTypeIDs);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetNetworkDB(string MarketIDs, string SubMarketIDs, string StateIDs, string CountyIDs, string FootprintIDs, string PlanCategory, string PlanTypeIDs, string PlanNameIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetNetwork", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubMarketIDs);
                sqlCommand.Parameters.AddWithValue("@StateIDs", StateIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
                sqlCommand.Parameters.AddWithValue("@FootprintIDs", FootprintIDs);
                sqlCommand.Parameters.AddWithValue("@PlanCategory", PlanCategory);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIDs", PlanTypeIDs);
                sqlCommand.Parameters.AddWithValue("@PlanNameIDs", PlanNameIDs);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public int SaveUserRequestDB(string UserName, string MarketIDs, string SubmarketIDs, string StateIDs, string CountyIDs, string FootprintIDs, string PlanCategory, string PlanTypeIDs, string PlanNameIDs, string NetwrokIDs)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_SaveRequest", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@UserName", UserName);
                sqlCommand.Parameters.AddWithValue("@MarketIDs", MarketIDs);
                sqlCommand.Parameters.AddWithValue("@SubMarketIDs", SubmarketIDs);
                sqlCommand.Parameters.AddWithValue("@StateIDs", StateIDs);
                sqlCommand.Parameters.AddWithValue("@CountyIDs", CountyIDs);
                sqlCommand.Parameters.AddWithValue("@FootprintIDs", FootprintIDs);
                sqlCommand.Parameters.AddWithValue("@PlanCategory", PlanCategory);
                sqlCommand.Parameters.AddWithValue("@PlanTypeIDs", PlanTypeIDs);
                sqlCommand.Parameters.AddWithValue("@PlanNameIDs", PlanNameIDs);
                sqlCommand.Parameters.AddWithValue("@NetwrokIDs", NetwrokIDs);
                sqlCommand.Parameters.Add("@NewRowIdentity", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                int NewRowIdentity = Convert.ToInt32(sqlCommand.Parameters["@NewRowIdentity"].Value.ToString());
                this.CloseHWConnection();
                return NewRowIdentity;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                this.CloseHWConnection();

            }

        }
        public DataTable GetRequestsDB(string UserName)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetRequests", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@UserName", UserName);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetResultsDB(int RequestID)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetResults", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@RequestID", RequestID);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }
        public DataTable GetMaxRequestsDB(string UserName)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spProviderNetworkAnalysis_GetMaxRequests", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@UserName", UserName);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }

        public DataTable GetNPIStateDB()
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spNPISearch_GetState", sqlHWConnection);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }

        public DataTable GetNPICountyDB(string state)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spNPISearch_GetCounty", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@state", state);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }

        public DataTable GetNPIZipcodeDB(string state, string county)
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spNPISearch_GetZipcode", sqlHWConnection);
                sqlCommand.Parameters.AddWithValue("@state", state);
                sqlCommand.Parameters.AddWithValue("@county", county);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }

        public DataTable GetNPISpecialtyGroupDB()
        {
            try
            {
                this.OpenHWConnection();
                sqlCommand = new SqlCommand("spNPISearch_GetSpecialtyGroup", sqlHWConnection);
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
                this.CloseHWConnection();

            }
            return dataTable;
        }

        public DataTable GetNPISearchDataDB(int requestid)
        {
            try
            {
                this.OpenHWAIConnection();
                sqlCommand = new SqlCommand("spNPISearh", sqlHWAIConnection);
                sqlCommand.Parameters.AddWithValue("@requestid", requestid);                
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
                this.CloseHWAIConnection();

            }
            return dataTable;
        }

        public int SaveUserSearchCriteriaDB(string npi, string type, string specialty, string firstname, string lastname, string orgname, string state, string county, string zipcode,string username)
        {
            try
            {
                this.OpenHWAIConnection();
                sqlCommand = new SqlCommand("spNPISearch_SaveRequest", sqlHWAIConnection);              
                sqlCommand.Parameters.AddWithValue("@npi", npi);
                sqlCommand.Parameters.AddWithValue("@npitype", type);
                sqlCommand.Parameters.AddWithValue("@specialtygroup", specialty);
                sqlCommand.Parameters.AddWithValue("@firstname", firstname);
                sqlCommand.Parameters.AddWithValue("@lastname", lastname);
                sqlCommand.Parameters.AddWithValue("@organization", orgname);
                sqlCommand.Parameters.AddWithValue("@state", state);
                sqlCommand.Parameters.AddWithValue("@county", county);
                sqlCommand.Parameters.AddWithValue("@zipcode", zipcode);
                sqlCommand.Parameters.AddWithValue("@username", username);
                sqlCommand.Parameters.Add("@NewRowIdentity", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                int NewRowIdentity = Convert.ToInt32(sqlCommand.Parameters["@NewRowIdentity"].Value.ToString());
                this.CloseHWAIConnection();
                return NewRowIdentity;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                this.CloseHWAIConnection();

            }

        }

        public DataTable GetNPISearchLevel2DataDB(int requestid, string presentation_name, string specialty, string flag)
        {
            try
            {
                this.OpenHWAIConnection();
                sqlCommand = new SqlCommand("spNPISearhLevel", sqlHWAIConnection);
                sqlCommand.Parameters.AddWithValue("@requestid", requestid);
                sqlCommand.Parameters.AddWithValue("@presentation_name", presentation_name);
                sqlCommand.Parameters.AddWithValue("@specialtyname", specialty);
                sqlCommand.Parameters.AddWithValue("@flag", flag);
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
                this.CloseHWAIConnection();
            }
            return dataTable;
        }
    }
}
