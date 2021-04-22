using DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class TicketDetailMethods
    {
        public TicketDetails  ticketDetails = new TicketDetails();

        #region  CreateTicket

        public int insertTicket(string username, string getname, string issue, string priority, string status, DateTime raisedDate, string category, string subCategory)
        {
            return ticketDetails.insertTicketDB(username, getname, issue, priority, status, raisedDate, category, subCategory);
        }

        public object viewTicket(string username)
        {
            return ticketDetails.viewTicketDB(username);
        }

        public object GetCategory()
        {
            return ticketDetails.GetCategoryDB();
        }

        public object GetSubCategory(string Category)
        {
            return ticketDetails.GetSubCategoryDB(Category);
        }

        public int updateTicket(string issue, string priority, string status, int tickID, int num)
        {
            return ticketDetails.updateTicketDB(issue, priority, status, tickID, num);
        }

        public object filterDisplay(string start, string end, string username1)
        {
            return ticketDetails.filterDateDB(start, end, username1);
        }

        #endregion

        #region  ViewTicket

        public object viewAllTicket(string username)
        {
            return ticketDetails.viewAllTickettDB(username);
        }

        public object ViewTicketByStatus(string status)
        {
            return ticketDetails.ViewTicketByStatusDB(status);
        }

        public object AllCheck()
        {
            return ticketDetails.AllCheckDB();
        }

        public object SearchTicketByName_ID(string store)
        {
            return ticketDetails.SearchTicketByName_ID_DB(store);
        }

        public int updateOpenTicket(string status, int tickID, int num, string comment)
        {
            return ticketDetails.updateOpenTicketDB(status, tickID, num, comment);
        }

        public object dropdownSearch(string TicketStatus, string TicketPriority, string search)
        {
            return ticketDetails.dropdownSearchDB(TicketStatus, TicketPriority, search);
        }

        public object TextSearchTicketPriority(string TicketPriority, string search)
        {
            return ticketDetails.TextSearchTicketPriorityDB(TicketPriority, search);
        }

        public object TextSearchTicketStatus(string TicketStatus, string search)
        {
            return ticketDetails.TextSearchTicketStatusDB(TicketStatus, search);
        }

        public object TextSearch(string search)
        {
            return ticketDetails.TextSearchDB(search);
        }

        public object BothdropdownSelected(string TicketStatus, string TicketPriority)
        {
            return ticketDetails.BothdropdownSelectedDB(TicketStatus, TicketPriority);
        }

        public object DropdownPriority(string TicketPriority)
        {
            return ticketDetails.DropdownPriorityDB(TicketPriority);
        }

        public object DropdownStatus(string TicketStatus)
        {
            return ticketDetails.DropdownStatusDB(TicketStatus);
        }

        #endregion

        #region  Report

        public object viewPastTicket()
        {
            return ticketDetails.viewPastTicketDB();
        }

        public object PastTicketfilterDisplay(string start, string end)
        {
            return ticketDetails.PastTicketfilterDateDB(start, end);
        }

        #endregion

    }
}

