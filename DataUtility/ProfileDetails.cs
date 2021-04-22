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
    public class ProfileDetails : DBOperations
    {
        private SqlCommand sqlCommand;
        private SqlDataAdapter SqlDataAdapter;
        private DataSet dataSet;
        CustomLogger customLogger = new CustomLogger();

        public DataSet Get_User_Details(string UserName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("Select * from tbl_UserDetails Where UserId=@user",sqlConnection);
            sqlCommand.Parameters.AddWithValue("@user", UserName);
            SqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            SqlDataAdapter.Fill(dataSet);
            this.CloseConnection();
            return dataSet;
        }

        public DataSet Get_User_Activity(string UserName)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("Profile_RecentActivity", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserName", UserName);
            SqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            SqlDataAdapter.Fill(dataSet);
            this.CloseConnection();
            return dataSet;
        }

        public void Insert_User_Details(string UserName, string FullName, string DOB, string Gender, string Doj, string EmailId, string Mobile, string Address, byte[] imageData, string AboutMe)
        {
            this.OpenConnection();
            sqlCommand = new SqlCommand("insert into tbl_UserDetails values(@UserID, @FullName,@DOB,@Gender,@DOJ,@Email,@Mobile,@Address,@Profile,@About, @Date)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@UserID", UserName);

            sqlCommand.Parameters.AddWithValue("@FullName", FullName);

            if (DOB.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@DOB", Convert.ToDateTime(DOB));
            else
                sqlCommand.Parameters.AddWithValue("@DOB", DBNull.Value);

            if (Gender.ToString() != "Select")
                sqlCommand.Parameters.AddWithValue("@Gender", Gender.ToString());
            else
                sqlCommand.Parameters.AddWithValue("@Gender", DBNull.Value);

            if (Doj.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@DOJ", Convert.ToDateTime(Doj));
            else
                sqlCommand.Parameters.AddWithValue("@DOJ", DBNull.Value);

            if (EmailId.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@Email", EmailId);
            else
                sqlCommand.Parameters.AddWithValue("@Email", DBNull.Value);

            if (Mobile.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@Mobile", Mobile);
            else
                sqlCommand.Parameters.AddWithValue("@Mobile", DBNull.Value);

            if (Address.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@Address", Address);
            else
                sqlCommand.Parameters.AddWithValue("@Address", DBNull.Value);

            if (imageData != null)
                sqlCommand.Parameters.AddWithValue("@Profile", imageData);
            else
            {
                SqlParameter imageParameter = new SqlParameter("@Profile", SqlDbType.Image);
                imageParameter.Value = DBNull.Value;
                sqlCommand.Parameters.Add(imageParameter);
            }

            if (AboutMe.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@About", AboutMe);
            else
                sqlCommand.Parameters.AddWithValue("@About", DBNull.Value);

            sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);

            sqlCommand.ExecuteNonQuery();
            this.CloseConnection();

        }

        public void Update_User_Details(string UserName, string FullName, string DOB, string Gender, string Doj, string EmailId, string Mobile, string Address, byte[] imageData, string AboutMe, int rowID)
        {
            this.OpenConnection();
            if (imageData != null)
            {
                sqlCommand = new SqlCommand("update tbl_UserDetails set FullName=@FullName, DOB=@DOB,Gender=@Gender,DOJ=@DOJ,Email=@Email,Mobile=@Mobile,Address=@Address,Profile_PIC=@Profile,About=@About,UpdatedDate=@Date where TabID=@TabID", sqlConnection);

                if (imageData != null)
                    sqlCommand.Parameters.AddWithValue("@Profile", imageData);
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@Profile", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    sqlCommand.Parameters.Add(imageParameter);
                }
            }
            else
            {
                sqlCommand = new SqlCommand("update tbl_UserDetails set FullName=@FullName, DOB=@DOB,Gender=@Gender,DOJ=@DOJ,Email=@Email,Mobile=@Mobile,Address=@Address,About=@About,UpdatedDate=@Date where TabID=@TabID", sqlConnection);
            }
                sqlCommand.Parameters.AddWithValue("@UserID", UserName);


            if (FullName.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@FullName", FullName);
            else
                sqlCommand.Parameters.AddWithValue("@FullName", DBNull.Value);

            if (DOB.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@DOB", Convert.ToDateTime(DOB));
            else
                sqlCommand.Parameters.AddWithValue("@DOB", DBNull.Value);

            if (Gender.ToString() != "Select")
                sqlCommand.Parameters.AddWithValue("@Gender", Gender);
            else
                sqlCommand.Parameters.AddWithValue("@Gender", DBNull.Value);

            if (Doj.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@DOJ", Convert.ToDateTime(Doj));
            else
                sqlCommand.Parameters.AddWithValue("@DOJ", DBNull.Value);

            if (EmailId.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@Email", EmailId);
            else
                sqlCommand.Parameters.AddWithValue("@Email", DBNull.Value);

            if (Mobile.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@Mobile", Mobile);
            else
                sqlCommand.Parameters.AddWithValue("@Mobile", DBNull.Value);

            if (Address.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@Address", Address);
            else
                sqlCommand.Parameters.AddWithValue("@Address", DBNull.Value);

            if (AboutMe.ToString() != "")
                sqlCommand.Parameters.AddWithValue("@About", AboutMe);
            else
                sqlCommand.Parameters.AddWithValue("@About", DBNull.Value);

            sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
            sqlCommand.Parameters.AddWithValue("@TabID", rowID);
            sqlCommand.ExecuteNonQuery();
            this.CloseConnection();
        }
    }
}
