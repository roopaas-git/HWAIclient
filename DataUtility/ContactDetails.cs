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
    public class ContactDetails : DBOperations
    {   
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;
        private DataSet dataSet;
        CustomLogger customLogger = new CustomLogger();

        public DataTable GetContactDetails()
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("select * from tbl_ContactUS order by ID", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
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

        public void InsertContactDetails(string Name, string Designation, string Email, string Contact, byte[] imageData, string City, string floor, string state)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("Insert into tbl_ContactUS values(@Profile_PIC, @Designation, @Email, @Mobile, @Name, @City, @floor, @state)", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Profile_PIC", imageData);
                sqlCommand.Parameters.AddWithValue("@Designation", Designation);
                sqlCommand.Parameters.AddWithValue("@Email", Email);
                sqlCommand.Parameters.AddWithValue("@Mobile", Contact);
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                sqlCommand.Parameters.AddWithValue("@City", City);
                if (floor != string.Empty)
                    sqlCommand.Parameters.AddWithValue("@floor", floor + ",");
                else
                    sqlCommand.Parameters.AddWithValue("@floor", floor);
                sqlCommand.Parameters.AddWithValue("@state", state);

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

        public void UpdateContactDetails(int id, string Name, string Designation, string Email, string Contact, byte[] imageData, string City)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("update tbl_ContactUS set Name = @Name, Designation = @Designation, Email=@Email, Mobile= @Mobile, Profile_PIC=@Profile_PIC, City=@City where ID=@id", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                sqlCommand.Parameters.AddWithValue("@Designation", Designation);
                sqlCommand.Parameters.AddWithValue("@Email", Email);
                sqlCommand.Parameters.AddWithValue("@Mobile", Contact);
                sqlCommand.Parameters.AddWithValue("@Profile_PIC", imageData);
                sqlCommand.Parameters.AddWithValue("@City", City);

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

        public DataSet BindCity()
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("Select City from [tbl_AddCity]", sqlConnection);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }

            return dataSet;
        }

        public DataTable GetCodeByCity(string cityName)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("Select Code from [tbl_AddCity] where City = @City", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@City", cityName);
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

        public void AddCity(string cityName, string code)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("insert into tbl_AddCity values(@city,@code)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@city", cityName);
                sqlCommand.Parameters.AddWithValue("@code", code);
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

        public void DeleteContact(int Id)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("delete from tbl_ContactUS where ID=@id", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", Id);
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
    }
}
