using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DataUtility;

namespace HealthWorks.Content.BO
{
    public class PageTracking :DBOperations
    {
        public SqlCommand gobjCmdDD;

        public void InsertDataIntoDB(string pageName, string SessionId, string UserName, string PageLink)
        {
            int tableCount = 0;
            string strtableCount = "Select Count(*) from tbl_UserPageTrackingDetails where SessionId =@SessionId and UserName=@UserName and PageName=@pageName";
            this.OpenConnection();
            gobjCmdDD = new SqlCommand(strtableCount, sqlConnection);
            gobjCmdDD.CommandType = CommandType.Text;
            gobjCmdDD.Parameters.AddWithValue("@SessionId", SessionId);
            gobjCmdDD.Parameters.AddWithValue("@UserName", UserName);
            gobjCmdDD.Parameters.AddWithValue("@pageName", pageName);
            SqlDataReader dr = gobjCmdDD.ExecuteReader();
            while (dr.Read())
            {
                tableCount = dr.GetInt32(0);
            }
            dr.Close();
            this.CloseConnection();

            if (tableCount == 0)
            {
                InsertData(pageName, SessionId, UserName, PageLink);
            }
            else
            {
                UpdateData(pageName, SessionId, UserName);
            }
        }

        public void InsertData(string PageName, string SessionId, string UserName, string PageLink)
        {
            string strInsert = "INSERT INTO tbl_UserPageTrackingDetails values (@SessionId,@UserName,@PageName,@Visits,@LastVisited,@PageLink)";

            this.OpenConnection();
            SqlCommand cmd = new SqlCommand(strInsert, sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SessionId", SessionId);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@PageName", PageName);
            cmd.Parameters.AddWithValue("@Visits", 1);
            cmd.Parameters.AddWithValue("@LastVisited", DateTime.Now);
            cmd.Parameters.AddWithValue("@PageLink", PageLink);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            this.CloseConnection();
        }

        public void UpdateData(string PageName, string SessionId, string UserName)
        {
            int Get_Visits_Count = 0;
            string getQuery = "Select NoOfVisits from tbl_UserPageTrackingDetails where SessionId=@SessionId and UserName=@UserName and PageName=@PageName";

            this.OpenConnection();
            gobjCmdDD = new SqlCommand(getQuery, sqlConnection);
            gobjCmdDD.CommandType = CommandType.Text;
            gobjCmdDD.Parameters.AddWithValue("@SessionId", SessionId);
            gobjCmdDD.Parameters.AddWithValue("@UserName", UserName);
            gobjCmdDD.Parameters.AddWithValue("@PageName", PageName);
            SqlDataReader dr1 = gobjCmdDD.ExecuteReader();
            while (dr1.Read())
            {
                Get_Visits_Count = dr1.GetInt32(0);
            }
            dr1.Close();

            string updateQuery = "UPDATE tbl_UserPageTrackingDetails  set NoOfVisits=@Visits, LastVisitedDate=@LastVisited where SessionId =@SessionId and UserName=@UserName and PageName=@PageName";
            gobjCmdDD = new SqlCommand(updateQuery, sqlConnection);
            gobjCmdDD.CommandType = CommandType.Text;
            gobjCmdDD.Parameters.AddWithValue("@SessionId", SessionId);
            gobjCmdDD.Parameters.AddWithValue("@UserName", UserName);
            gobjCmdDD.Parameters.AddWithValue("@PageName", PageName);
            gobjCmdDD.Parameters.AddWithValue("@Visits", Get_Visits_Count + 1);
            gobjCmdDD.Parameters.AddWithValue("@LastVisited", DateTime.Now);
            gobjCmdDD.ExecuteNonQuery();
            gobjCmdDD.Parameters.Clear();
            this.CloseConnection();
        }

        public void InsertDownloadFileData(string PageName, string FileName, string UserName)
        {
            string strInsert = "INSERT INTO tbl_UserFileDownloadTracking values (@UserName,@FileName,@PageName,@DownloadedDateTime)";
            this.OpenConnection();
            gobjCmdDD = new SqlCommand(strInsert, sqlConnection);
            gobjCmdDD.CommandType = CommandType.Text;
            gobjCmdDD.Parameters.AddWithValue("@UserName", UserName);
            gobjCmdDD.Parameters.AddWithValue("@FileName", FileName);
            gobjCmdDD.Parameters.AddWithValue("@PageName", PageName);
            gobjCmdDD.Parameters.AddWithValue("@DownloadedDateTime", DateTime.Now);
            gobjCmdDD.ExecuteNonQuery();
            gobjCmdDD.Parameters.Clear();
            this.CloseConnection();
        }
    }
}