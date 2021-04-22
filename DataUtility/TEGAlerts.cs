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
    public class TEGAlerts : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        CustomLogger customLogger;
        public int result = 0;

        public TEGAlerts()
        {
            customLogger = new CustomLogger();
        }

        public DataTable Get_Alerts()
        {
            try
            {
                this.OpenConnection();
                string sql = "Select * from tbl_Manage_Alert_TEG";
                sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
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

        public DataTable Get_Top_Alerts()
        {
            try
            {
                this.OpenConnection();
                string sql = "select top 4 * from tbl_Manage_Alert_TEG order by ID desc";
                sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
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

        public void Insert_Alerts(string mainContent, string userName)
        {
            try
            {
                this.OpenConnection();
                string strLogin = "Insert into tbl_Manage_Alert_TEG values(@Main_Content,@UploadedBy,@UploadedDate)";
                sqlCommand = new SqlCommand(strLogin, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Main_Content", mainContent);
                sqlCommand.Parameters.AddWithValue("@UploadedBy", userName);
                sqlCommand.Parameters.AddWithValue("@UploadedDate", DateTime.Now);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public int Delete_Alert(int Id)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("delete from tbl_Manage_Alert_TEG where ID=@id", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@id", Id);
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }
    }
}

