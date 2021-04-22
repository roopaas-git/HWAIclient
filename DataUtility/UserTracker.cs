using LoggerUtility;
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
    public class UserTracker : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter SqlDataAdapter;
        private DataTable dataTable;
        CustomLogger customLogger = new CustomLogger();

        public DataTable GetPortalUsageStats(DateTime startDate, DateTime endDate)
        {
            try
            {
                this.OpenConnection();
                string strtableCount = "Select Id,[UserName],[PageName],[NoOfVisits],[LastVisitedDate] from tbl_UserPageTrackingDetails where UserName not like '%teganalytics%' and cast(LastVisitedDate as date) between @StartDate and @EndDate order by LastVisitedDate desc";
                sqlCommand = new SqlCommand(strtableCount, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                SqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }

            return dataTable;
        }
    }
}
