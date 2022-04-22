using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class EnrollmentFileUpload
    {
        public int Id { get; set; }
        public int ScenarioID { get; set; }
        public string BidId { get; set; }
        public string UploadedFilePath { get; set; }

        //public int MarketId { get; set; }
        //public int SubMarketId { get; set; }
        //public string StateId { get; set; }
        //public string CountyId { get; set; }
        //public string FootprintId { get; set; }
        //public int PlanTypeId { get; set; }
        //public int PlanCategoryId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        //public string Market { get; set; }
        public string PlanName { get; set; }
    }
}
