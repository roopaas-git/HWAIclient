using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace DataUtility
{
    public class TicketDetails
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
        public SqlCommand lobjCmd;
        public DataSet lobjDS;
        public SqlDataAdapter lobjDA;
        public DataTable lobjDT;
        public string DBQuery;
        public DateTime now = DateTime.Now;
        public string Open = "Open";
       
        #region  CreateTicket

        public object GetCategoryDB()
        {
            DBQuery = "Select distinct Cat_Name from Category ";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            con.Close();
            return lobjDS;
        }

        public int insertTicketDB(string username, string getname, string issue, string priority, string status, DateTime raisedDate, string category, string subCategory)
        {
            con.Open();
            lobjCmd = new SqlCommand("Insert_RaisedTicket", con);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@UserName", username);
            lobjCmd.Parameters.AddWithValue("@FirstName", getname);
            lobjCmd.Parameters.AddWithValue("@Issue", issue);
            lobjCmd.Parameters.AddWithValue("@Ticket_Priority", priority);
            lobjCmd.Parameters.AddWithValue("@Ticket_Status", status);
            lobjCmd.Parameters.AddWithValue("@Cat_Name", category);
            lobjCmd.Parameters.AddWithValue("@SubCat_Name", subCategory);
            lobjCmd.Parameters.Add("@new_identity", SqlDbType.Int).Direction = ParameterDirection.Output;
            lobjCmd.ExecuteNonQuery();
            int num = Convert.ToInt32(lobjCmd.Parameters["@new_identity"].Value.ToString());
            con.Close();
            return num;
        }

        public object viewTicketDB(string username)
        {
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            string start = firstDay.Date.ToShortDateString();
            string end = DateTime.Now.ToShortDateString() + "  23:59:59";
            DBQuery = "select * from Ticket_Details where UserName= '" + username + "' and cast(Ticket_Raised_Date as date) between @StartDate and @EndDate order by Ticket_Raised_Date desc ";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjCmd.CommandType = CommandType.Text;
            lobjCmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(start));
            lobjCmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(end));
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            con.Close();
            return lobjDS;
        }

        public object GetSubCategoryDB(string Category)
        {
            DBQuery = "Select SubCat_Name from Category  where Cat_Name=@Cat_Name";
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjCmd.Parameters.AddWithValue("@Cat_Name", Category);
            con.Open();
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            con.Close();
            return lobjDS;
        }

        public int updateTicketDB(string issue, string priority, string status, int tickID, int num)
        {
            DBQuery = "Update Ticket_Details set Issue = '" + issue + "', Ticket_Priority= '" + priority + "', Ticket_Status ='" + status + "' where TicketID= '" + tickID + "' ";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            num = lobjCmd.ExecuteNonQuery();
            con.Close();
            return num;
        }

        public object filterDateDB(string start, string end, string username1)
        {
            DBQuery = "select * from Ticket_Details where UserName= '" + username1 + "' and cast(Ticket_Raised_Date as date) between @StartDate and @EndDate order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjCmd.CommandType = CommandType.Text;
            lobjCmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(start));
            lobjCmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(end));

            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            con.Close();
            return lobjDS;
        }


        #endregion

        #region  ViewTicket

        public object viewAllTickettDB(string username)
        {
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            string start = firstDay.Date.ToShortDateString();
            string end = DateTime.Now.ToShortDateString() + "  23:59:59";
            DBQuery = "select * from [Ticket_Details] order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjCmd.CommandType = CommandType.Text;
            lobjCmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(start));
            lobjCmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(end));
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            con.Close();
            return lobjDS;
        }

        public object ViewTicketByStatusDB(string status)
        {
            DBQuery = "select * from Ticket_Details where Ticket_Status = '" + status + "'  order by TicketID desc  ";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }

        public object AllCheckDB()
        {
            DBQuery = "select * from Ticket_Details order by Ticket_Raised_Date desc ";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }

        public object SearchTicketByName_ID_DB(string store)
        {
            int num1;
            if (int.TryParse(store, out num1) == true)
            {
                DBQuery = "select * from Ticket_Details where  ( TicketID = '" + store + "') order by TicketID desc  ";
                con.Open();
                lobjCmd = new SqlCommand(DBQuery, con);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDT = new DataTable();
                lobjDA.Fill(lobjDT);
                con.Close();
                return lobjDT;
            }
            else
            {
                DBQuery = "select * from Ticket_Details where  (FirstName Like '" + store + "%' or Issue Like '%" + store + "%') order by TicketID desc  ";
                con.Open();
                lobjCmd = new SqlCommand(DBQuery, con);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDT = new DataTable();
                lobjDA.Fill(lobjDT);
                con.Close();
                return lobjDT;
            }
        }

        public int updateOpenTicketDB(string status, int tickID, int num, string comment)
        {
            DBQuery = "Update Ticket_Details set  Ticket_Status ='" + status + "', Comments='" + comment + "' where TicketID= '" + tickID + "' ";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            num = lobjCmd.ExecuteNonQuery();
            con.Close();
            return num;
        }

        public object dropdownSearchDB(string TicketStatus, string TicketPriority, string search)
        {
            DBQuery = "select * from [Ticket_Details] where  Ticket_Status IN (" + TicketStatus + ") and Ticket_Priority IN (" + TicketPriority + ") and (FirstName Like '" + search + "%' or Issue Like '%" + search + "%') order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }

        public object TextSearchTicketPriorityDB(string TicketPriority, string search)
        {
            DBQuery = "select * from [Ticket_Details] where  Ticket_Priority IN (" + TicketPriority + ") and (FirstName Like '" + search + "%' or Issue Like '%" + search + "%') order by Ticket_Raised_Date desc ";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }

        public object TextSearchTicketStatusDB(string TicketStatus, string search)
        {
            DBQuery = "select * from [Ticket_Details] where  Ticket_Status IN (" + TicketStatus + ") and (FirstName Like '" + search + "%' or Issue Like '%" + search + "%') order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }


        public object TextSearchDB(string search)
        {
            DBQuery = "select * from [Ticket_Details] where FirstName Like '" + search + "%' or Issue Like '%" + search + "%' order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }

        public object BothdropdownSelectedDB(string TicketStatus, string TicketPriority)
        {
            DBQuery = "select * from Ticket_Details where  Ticket_Status IN (" + TicketStatus + ") and Ticket_Priority IN (" + TicketPriority + ") order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }

        public object DropdownPriorityDB(string TicketPriority)
        {
            DBQuery = "select * from [Ticket_Details] where  Ticket_Priority IN (" + TicketPriority + ") order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }

        public object DropdownStatusDB(string TicketStatus)
        {
            DBQuery = "select * from [Ticket_Details] where  Ticket_Status IN (" + TicketStatus + ") order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }
        #endregion

        #region Report


        public object viewPastTicketDB()
        {
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            string start = firstDay.Date.ToShortDateString();
            string end = DateTime.Now.ToShortDateString() + "  23:59:59";
            DBQuery = "select * from Ticket_Details where cast(Ticket_Raised_Date as date) between @StartDate and @EndDate order by Ticket_Raised_Date desc ";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjCmd.CommandType = CommandType.Text;
            lobjCmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(start));
            lobjCmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(end));
            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDS = new DataSet();
            lobjDA.Fill(lobjDS);
            con.Close();
            return lobjDS;
        }

        public object PastTicketfilterDateDB(string start, string end)
        {
            DBQuery = "select * from Ticket_Details where cast(Ticket_Raised_Date as date) between @StartDate and @EndDate order by Ticket_Raised_Date desc";
            con.Open();
            lobjCmd = new SqlCommand(DBQuery, con);
            lobjCmd.CommandType = CommandType.Text;
            lobjCmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(start));
            lobjCmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(end));

            lobjDA = new SqlDataAdapter(lobjCmd);
            lobjDT = new DataTable();
            lobjDA.Fill(lobjDT);
            con.Close();
            return lobjDT;
        }


        #endregion

    }
}
