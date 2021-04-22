using LoggerUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataUtility
{
    public class UserFavorites : DBOperations
    {
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private SqlCommand sqlCommand;
        private string htmlString = string.Empty;
        CustomLogger customLogger = new CustomLogger();

        public string BindFavourites(string userName, string pageName)
        {
            try
            {
                this.OpenConnection();
                string sql = "select Isfavourite from [tbl_FavouritesTable_TEG] where UserID='" + userName + "' and ReportName ='" + pageName + "'";
                sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0][0].ToString() == "1")
                    {
                        htmlString = @"<i class='fas fa-star' aria-hidden='true'></i>";
                    }
                    else
                    {
                        htmlString = @"<i class='far fa-star' aria-hidden='true'></i>";
                    }
                }
                else
                {
                    htmlString = @"<i class='far fa-star' aria-hidden='true'></i>";
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }

            return htmlString;
        }

        public DataTable BindFavouriteByUser(string userName, int isFavourite=1)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("select * from [tbl_FavouritesTable_TEG] where UserID=@UserName and Isfavourite=@IsFavourite order by TabId desc", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@UserName", userName);
                sqlCommand.Parameters.AddWithValue("@IsFavourite", isFavourite);
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

        public void InsertUpdateFavourite(string pageName, string PageLink, string userName)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("Inset_Update_Favourates_TEG", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReportName", pageName);
                sqlCommand.Parameters.AddWithValue("@PageLink", PageLink);
                sqlCommand.Parameters.AddWithValue("@UserName", userName);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
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

        public void UpdateFavouriteById(int Id)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("Update_Favourates_By_Id", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", Id);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
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

        public DataTable GetPageNames()
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("SELECT * FROM [tbl_PageNames]", sqlConnection);
                dataTable = new DataTable();
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
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
    }
}
