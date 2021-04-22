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
    public class ActivatePageDetails : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        CustomLogger customLogger = new CustomLogger();
        private int insertedRecordId = 0;

        public DataTable Get_ActivationDetails(string activationCode)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("select FirstName,IsActive from Login_Details where UserId=@userID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@userID", activationCode);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
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

        public int ActivateAccount(string activationCode)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("update Login_Details set IsActive=@IsActive where UserId=@userID", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@IsActive", 1);
                sqlCommand.Parameters.AddWithValue("@userID", activationCode);
                insertedRecordId = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return insertedRecordId;
        }
    }
}
