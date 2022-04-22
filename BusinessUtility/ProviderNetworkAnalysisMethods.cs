using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUtility;

namespace BusinessUtility
{
    public class ProviderNetworkAnalysisMethods
    {
        ProviderNetworkAnalysisDetails ProviderNetworkAnalysisDetails = new ProviderNetworkAnalysisDetails();
        private DataTable dataTable;
        private DataSet dataSet;
        public DataTable GetMarket()
        {
            dataTable = ProviderNetworkAnalysisDetails.GetMarketDB();
            return dataTable;
        }
        public DataTable GetSubMarket(string MarketIDs)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetSubMarketDB(MarketIDs);
            return dataTable;
        }
        public DataTable GetState(string MarketIDs, string SubmarketIDs)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetStateDB(MarketIDs, SubmarketIDs);
            return dataTable;
        }
        public DataTable GetCounty(string MarketIDs, string SubmarketIDs, string StateIDs)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetCountyDB(MarketIDs, SubmarketIDs, StateIDs);
            return dataTable;
        }
        public DataTable GetZipcode(string MarketIDs, string SubmarketIDs, string StateIDs, string CountyIDs)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetZipcodeDB(MarketIDs, SubmarketIDs, StateIDs, CountyIDs);
            return dataTable;
        }
        public DataTable GetFootprint(string MarketIDs, string SubmarketIDs, string StateIDs, string CountyIDs)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetFootprintDB(MarketIDs, SubmarketIDs, StateIDs, CountyIDs);
            return dataTable;
        }
        public DataTable GetPlanCategory(string MarketIDs, string SubmarketIDs, string StateIDs, string CountyIDs, string FootprintIDs)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetPlanCategoryDB(MarketIDs, SubmarketIDs, StateIDs, CountyIDs, FootprintIDs);
            return dataTable;
        }
        public DataTable GetPlanType(string MarketIDs, string SubmarketIDs, string StateIDs, string CountyIDs, string FootprintIDs, string PlanCategory)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetPlanTypeDB(MarketIDs, SubmarketIDs, StateIDs, CountyIDs, FootprintIDs, PlanCategory);
            return dataTable;
        }
        public DataTable GetPlanNames(string MarketIDs, string SubmarketIDs, string StateIDs, string CountyIDs, string FootprintIDs, string PlanCategory, string PlanTypeIDs)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetPlanNamesDB(MarketIDs, SubmarketIDs, StateIDs, CountyIDs, FootprintIDs, PlanCategory, PlanTypeIDs);
            return dataTable;
        }
        public DataTable GetNetwork(string MarketIDs, string SubmarketIDs, string StateIDs, string CountyIDs, string FootprintIDs, string PlanCategory, string PlanTypeIDs, string PlanNameIDs)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetNetworkDB(MarketIDs, SubmarketIDs, StateIDs, CountyIDs,  FootprintIDs, PlanCategory, PlanTypeIDs, PlanNameIDs);
            return dataTable;
        }
        public int SaveUserRequest(string UserName, string MarketIDs, string SubmarketIDs, string StateIDs, string CountyIDs, string FootprintIDs, string PlanCategory, string PlanTypeIDs, string PlanNameIDs, string NetwrokIDs)
        {
            int NewRowIdentity = ProviderNetworkAnalysisDetails.SaveUserRequestDB(UserName, MarketIDs, SubmarketIDs, StateIDs, CountyIDs, FootprintIDs, PlanCategory, PlanTypeIDs, PlanNameIDs, NetwrokIDs);
            return NewRowIdentity;
        }
        public DataTable GetRequests(string UserName)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetRequestsDB(UserName);
            return dataTable;
        }
        public DataTable GetResults(int RequestID)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetResultsDB(RequestID);
            return dataTable;
        }
        public DataTable GetMaxRequests(string UserName)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetMaxRequestsDB(UserName);
            return dataTable;
        }

        public DataTable GetNPIState()
        {
            dataTable = ProviderNetworkAnalysisDetails.GetNPIStateDB();
            return dataTable;
        }

        public DataTable GetNPICounty(string state)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetNPICountyDB(state);
            return dataTable;
        }

        public DataTable GetNPIZipcode(string state,string county)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetNPIZipcodeDB(state,county);
            return dataTable;
        }

        public DataTable GetNPISpecialtyGroup()
        {
            dataTable = ProviderNetworkAnalysisDetails.GetNPISpecialtyGroupDB();
            return dataTable;
        }
        public DataTable GetNPISearchData(int requestid)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetNPISearchDataDB(requestid);
            return dataTable;
        }

        public int SaveUserSearchCriteria(string npi, string type, string specialty, string firstname, string lastname, string orgname, string state, string county, string zipcode,string username)
        {
            int NewRowIdentity = ProviderNetworkAnalysisDetails.SaveUserSearchCriteriaDB(npi,type,specialty,firstname,lastname,orgname,state,county,zipcode,username);
            return NewRowIdentity;
        }

        public DataTable GetNPISearchLevel2Data(int requestid,string presentation_name,string specialty,string flag)
        {
            dataTable = ProviderNetworkAnalysisDetails.GetNPISearchLevel2DataDB(requestid, presentation_name, specialty, flag);
            return dataTable;
        }
    }
}
