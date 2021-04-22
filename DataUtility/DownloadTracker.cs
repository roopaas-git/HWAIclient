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
    public class DownloadTracker : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter SqlDataAdapter;
        private DataTable dataTable;
        CustomLogger customLogger = new CustomLogger();

        public DataTable GetUploadedReports(string ReportName)
        {
            try
            {
                this.OpenConnection();
                string strtableCount = "Select FileName from tbl_UploadedReports where PageName = @pagename";
                sqlCommand = new SqlCommand(strtableCount, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@pagename", ReportName);
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
