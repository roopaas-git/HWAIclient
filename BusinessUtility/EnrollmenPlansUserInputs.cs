using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class EnrollmentPlansUserInputs
    {
        public int Id { get; set; }
        public int ScenarioID { get; set; }
        public string BidId { get; set; }
       
        public int MarketId { get; set; }
        public int SubMarketId { get; set; }
        public string StateIds { get; set; }
        public string SalesRegionIds { get; set; }
        public string CountyIds { get; set; }
        public string FootprintIds { get; set; }
        public string PlanCategoryIds { get; set; }
        public string PremiumIds { get; set; }
        public string PlanTypeIds { get; set; }

        public string BidLevelStateIds { get; set; }
        public string BidLevelCountyIds { get; set; }

        public string PlanName { get; set; }
    }
}
