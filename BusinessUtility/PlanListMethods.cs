using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUtility;

namespace BusinessUtility
{
    public class PlanListMethods
    {
        PlanListDetails PlanListDetails = new PlanListDetails();
        private DataTable dataTable;
        private DataSet dataSet;
        public DataTable GetState()
        {
            dataTable = PlanListDetails.GetStateDB();
            return dataTable;
        }
        public DataTable GetSalesregion(int StateID)
        {
            dataTable = PlanListDetails.GetSalesRegionDB(StateID);
            return dataTable;
        }
        public DataTable GetCounty(int StateID, string SalesRegionIDs)
        {
            dataTable = PlanListDetails.GetCountyDB(StateID, SalesRegionIDs);
            return dataTable;
        }
        public DataTable GetPlanType(int StateID, string SalesRegionIDs, string CountyIDs)
        {
            dataTable = PlanListDetails.GetPlanTypeDB(StateID, SalesRegionIDs, CountyIDs);
            return dataTable;
        }
        public DataSet GetOrgView(int StateID, string SalesRegionIDs, string CountyIDs, string PlanTypeIDs,int ScenarioID)
        {
            dataSet = PlanListDetails.GetOrgViewDB(StateID, SalesRegionIDs, CountyIDs, PlanTypeIDs, ScenarioID);
            return dataSet;
        }
        public DataTable GetCountyView(string PlanBidID)
        {
            dataTable = PlanListDetails.GetCountyViewDB(PlanBidID);
            return dataTable;
        }
        public DataTable GetSavedPlans(int ScenarioID)
        {
            dataTable = PlanListDetails.GetSavedPlansDB(ScenarioID);
            return dataTable;
        }
        public int SavePlanUserInputs(int StateID, string SalesRegionIDs, string CountyIDs, string PlanTypeIDs, string BidID, string PlanName, int ScenarioID)
        {
            int NewRowIdentity = PlanListDetails.SavePlanUserInputsDB(StateID, SalesRegionIDs, CountyIDs, PlanTypeIDs, BidID, PlanName, ScenarioID);
            return NewRowIdentity;
        }
        public DataTable GetPages(int ScenarioID)
        {
            dataTable = PlanListDetails.GetPagesDB(ScenarioID);
            return dataTable;
        }
        public DataSet GetOutputResults(int StateID, string SalesRegionIDs, string CountyIDs, string PlanTypeIDs,int ScenarioID)
        {
            dataSet = PlanListDetails.GetOutputResultsDB(StateID, SalesRegionIDs, CountyIDs, PlanTypeIDs, ScenarioID);
            return dataSet;
        }
    }
}
