using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUtility;

namespace BusinessUtility
{
    public class EnrollmentPlanListMethods
    {
        EnrollmentPlanListDetails enrollmentPlanListDetails = new EnrollmentPlanListDetails();
        private DataTable dataTable;
        private DataSet dataSet;
                
        public DataTable GetMarket(int ClientId)
        {
            dataTable = enrollmentPlanListDetails.GetEnrollmentMarketDB(ClientId);
            return dataTable;
        }

        public DataTable GetSubMarket(int ClientId,int MarketId)
        {
            dataTable = enrollmentPlanListDetails.GetEnrollmentSubMarketDB(ClientId, MarketId);
            return dataTable;
        }

        public DataTable GetState(int ClientId, int SubMarketId)
        {
            dataTable = enrollmentPlanListDetails.GetEnrollmentStateDB(ClientId, SubMarketId);
            return dataTable;
        }

        public DataTable GetSalesRegion(int ClientId, int SubMarketId, string SelectedStates)
        {
            dataTable = enrollmentPlanListDetails.GetSalesRegionDB(ClientId, SubMarketId, SelectedStates);
            return dataTable;
        }
                          
        public DataTable GetCounties(int ClientId, int SubMarketId, string SelectedStates, string SelectedSalesRegion,  string SelectedFootprints)
        {
            dataTable = enrollmentPlanListDetails.GetEnrollmentCountiesDB(ClientId, SubMarketId, SelectedStates, SelectedSalesRegion, SelectedFootprints);
            return dataTable;
        }
        
        public DataTable GetFootprint(int ClientId, int SubMarketId, string SelectedStates, string SelectedSalesRegions)
        {
              dataTable = enrollmentPlanListDetails.GetEnrollmentFootprintDB(ClientId, SubMarketId, SelectedStates, SelectedSalesRegions);
            return dataTable;
        }

        public DataTable GetPlanCategory(string SelectedStates, string SelectedCounties)
        {
            dataTable = enrollmentPlanListDetails.GetEnrollmentPlanCategoryDB(SelectedStates, SelectedCounties);
            return dataTable;
        }

        public DataTable GetPremium(string SelectedStates, string SelectedCounties, string SelectedPlanCategories)
        {
            dataTable = enrollmentPlanListDetails.GetPremiumDB(SelectedStates, SelectedCounties, SelectedPlanCategories);
            return dataTable;
        }

        public DataTable GetPlanType(string SelectedStates, string SelectedCounties, string SelectedPlanCategories, string SelectedPremiums)
        {
            dataTable = enrollmentPlanListDetails.GetEnrollmentPlanTypeDB(SelectedStates, SelectedCounties, SelectedPlanCategories, SelectedPremiums);
            return dataTable;
        }

        public DataSet GetDownloadResult(string BidId, string SelectedStates, string SelectedCounties, string SelectedPlanCategorys)
        {
            dataSet = enrollmentPlanListDetails.GetDownloadEnrollmentResultDB(BidId, SelectedStates, SelectedCounties, SelectedPlanCategorys);
            return dataSet;
        }

        public DataSet GetRawDataMappingDetails()
        {
            dataSet = enrollmentPlanListDetails.GetRawDataMappingDetailsDB();
            return dataSet;
        }        

        public DataSet GetOrgView(string SelectedStates, string SelectedCounties, string SelectedPlanCategories, string SelectedPremiums, string SelectedPlanTypes)
        {
            dataSet = enrollmentPlanListDetails.GetEnrollmentOrgViewDB(SelectedStates, SelectedCounties, SelectedPlanCategories, SelectedPremiums, SelectedPlanTypes);
            return dataSet;
        }
    
        public DataTable GetCountyView(string PlanBidID, string SelectedStates, string SelectedCounties, string SelectedPlanCategories, string SelectedPremiums, string SelectedPlanTypes)
        {
            dataTable = enrollmentPlanListDetails.GetEnrollmentCountyViewDB(PlanBidID, SelectedStates, SelectedCounties, SelectedPlanCategories, SelectedPremiums, SelectedPlanTypes);
            return dataTable;
        }

        public DataSet GetBenefitsForSimulation(string bidId, int scenarioId)
        {
            dataSet = enrollmentPlanListDetails.GetBenefitsForSimulationDB(bidId, scenarioId);
            return dataSet;
        }

        public DataSet GetFiltersForSimulation(int scenarioId, int clientId)
        {
            dataSet = enrollmentPlanListDetails.GetFiltersForSimulationDB(scenarioId, clientId);
            return dataSet;
        }
    }
}
