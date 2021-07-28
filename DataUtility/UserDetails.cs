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
    public class UserDetails : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter SqlDataAdapter;
        private DataSet dataSet;
        private DataTable dataTable;
        private string lastLoginTime = string.Empty;
        private string firstName = string.Empty;
        private int insertedRecordId = 0;
        CustomLogger customLogger = new CustomLogger();

        public string Check_User_Activation(string UserName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("Select IsActive FROM Login_Details Where UserName = @user", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", UserName);
            string isActive = sqlCommand.ExecuteScalar().ToString();
            this.CloseConnection();
            return isActive;
        }

        public void check_User_Password(string UserName, string Password)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("Select PassWord FROM Login_Details Where UserName = @user", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", UserName);
            string isPassword = sqlCommand.ExecuteScalar().ToString();
            this.CloseConnection();

            if (isPassword != Password)
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("update Login_Details set Password=@pass Where UserName = @user", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@pass", Password);
                sqlCommand.Parameters.AddWithValue("@user", UserName);
                sqlCommand.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public string GetLastLoginTime(string userName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("Select LastLoginDate from aspnet_Membership Where UserId = (select UserId from aspnet_Users where UserName=@user)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", userName);
            SqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            SqlDataAdapter.Fill(dataSet);
            this.CloseConnection();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                lastLoginTime = dataSet.Tables[0].Rows[0]["LastLoginDate"].ToString();
            }
            else
            {
                lastLoginTime = string.Empty;
            }

            return lastLoginTime;
        }

        public DataSet GetUserDeatilsByEmail(string userName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("SELECT * FROM Login_Details Where UserName=@user", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", userName);
            SqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            SqlDataAdapter.Fill(dataSet);
            this.CloseConnection();

            return dataSet;
        }

        public string GetFirstName(string userName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("select FirstName from Login_Details Where UserName=@User", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", userName);
            SqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            SqlDataAdapter.Fill(dataSet);
            this.CloseConnection();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                firstName = dataSet.Tables[0].Rows[0]["FirstName"].ToString();
            }
            else
            {
                firstName = string.Empty;
            }

            return firstName;
        }

        public DataTable GetUsersList()
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("Select * from Login_Details order by ID desc", sqlConnection);
                dataTable = new DataTable();
                sqlCommand = new SqlCommand(sqlCommand.CommandText, sqlConnection);
                SqlDataAdapter lobjDa = new SqlDataAdapter(sqlCommand);
                lobjDa.Fill(dataTable);
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

        public int SaveLoginDetails(object key, string firstName, string lastName, string email, string password, string mobileNumber, string userRole, string JobTitle, string Department, int isActive = 0)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("Insert into Login_Details values(@UserID,@FirstName,@LastName,@UserName,@PassWord,@Mobile,@IsActive,@UserType,@JobTitle,@Department,null,null,0,null,null,null)", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@UserID", key);
                sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                sqlCommand.Parameters.AddWithValue("@UserName", email);
                sqlCommand.Parameters.AddWithValue("@PassWord", password);
                sqlCommand.Parameters.AddWithValue("@Mobile", mobileNumber);
                sqlCommand.Parameters.AddWithValue("@IsActive", isActive);
                sqlCommand.Parameters.AddWithValue("@UserType", userRole);
                sqlCommand.Parameters.AddWithValue("@JobTitle", JobTitle);
                sqlCommand.Parameters.AddWithValue("@Department", Department);
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


        public void DeleteRecord(int id)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("delete from Login_Details where ID=@id", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@id", id);
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

        public int ChangePassword(string userID, string password)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("update [aspnet_Membership] set [Password]=@Password,PasswordFormat=@format where UserId = @userId ", sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@userId", userID);
                sqlCommand.Parameters.AddWithValue("@Password", password);
                sqlCommand.Parameters.AddWithValue("@format", 0);
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

        public DataSet GetUserId(string username)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("Select UserID FROM aspnet_Users where UserName = @user ", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@user", username);
                SqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                SqlDataAdapter.Fill(dataSet);
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

        public void InsertLoginDownloadUserDetails(string UserName)
        {
            this.OpenHWConnection();
            sqlCommand = new SqlCommand("insert into tbl_LoginDownloadTracker(UserName) values (@UserName)", sqlHWConnection);
            sqlCommand.Parameters.AddWithValue("@UserName", UserName);
            sqlCommand.ExecuteNonQuery();
            this.CloseHWConnection();
        }

        public DataSet CheckUserAlreadyLoggedIn(string UserName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("SELECT * FROM Login_Details Where UserName=@user", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", UserName);
            SqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            SqlDataAdapter.Fill(dataSet);
            this.CloseConnection();
            return dataSet;
        }

        public void LogoutSession(string UserName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("update Login_Details set [IsLoggedIn]=0,LastLogoutDate=getdate() Where UserName = @user", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", UserName);
            sqlCommand.ExecuteNonQuery();
            this.CloseConnection();

        }
        public void LoginSession(string sessionid, string UserName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("update Login_Details set [SessionID]=@sessionid, [IsLoggedIn]=1,[LastLoginDate]=getdate(),[LastLogoutDate]=null Where UserName = @user", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@sessionid", sessionid);
            sqlCommand.Parameters.AddWithValue("@user", UserName);
            sqlCommand.ExecuteNonQuery();
            this.CloseConnection();

        }

        public void InsertIPDetails(string NetworkIP, string SessionId, string UserName)
        {
            string strInsert = "INSERT INTO tbl_LoginIPDetails values (@NetworkIP,@SessionId,@UserName,@LastVisited)";

            this.OpenConnection();
            SqlCommand cmd = new SqlCommand(strInsert, sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SessionId", SessionId);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@NetworkIP", NetworkIP);
            cmd.Parameters.AddWithValue("@LastVisited", DateTime.Now);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            this.CloseConnection();
        }

        public string GetSessionID(string userName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("select SessionID from Login_Details Where UserName=@User", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", userName);
            SqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            SqlDataAdapter.Fill(dataSet);
            this.CloseConnection();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                firstName = dataSet.Tables[0].Rows[0]["SessionID"].ToString();
            }
            else
            {
                firstName = string.Empty;
            }

            return firstName;
        }

        public DataTable Get_UserDetails(string activationCode)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("select Username,PassWord,[ResetPasswordExpireDate],[IsResetPasswordActive] from Login_Details where UserId=@userID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@userID", activationCode);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
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

        public void ResetPasswordStatus(string userId, int IsResetPasswordActive)
        {
            this.OpenConnection();
            if (IsResetPasswordActive == 0)
            {
                sqlCommand = new SqlCommand("update Login_Details set [IsResetPasswordActive]=1,[ResetPasswordExpireDate]=getdate() Where UserId = @UserId", sqlConnection);
            }
            else
            {
                sqlCommand = new SqlCommand("update Login_Details set [IsResetPasswordActive]=0,[ResetPasswordExpireDate]=null Where UserId = @UserId", sqlConnection);
            }
            sqlCommand.Parameters.AddWithValue("@UserId", userId);
            sqlCommand.ExecuteNonQuery();
            this.CloseConnection();

        }
    }
}
