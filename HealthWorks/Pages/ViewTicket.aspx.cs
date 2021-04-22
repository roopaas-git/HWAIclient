using BusinessUtility;
using CommonUtility;
using LoggerUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HealthWorks.Pages
{
    public partial class ViewTicket : System.Web.UI.Page
    {
        List<string> CheckedItems = new List<string>();
        TicketDetailMethods ticketDetailMethods = new TicketDetailMethods();
        EmailServices emailServices = new EmailServices();
        CustomLogger customLogger = new CustomLogger();

        public DataTable lobjDT;
        public string Statuslist;
        public string Prioritylist;
        public string TicketStatus;
        public string TicketPriority;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "HealthWorksAI - Ticket";
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    Active_DeactiveLinks();
                }

                DisableControls();

                if (!IsPostBack)
                {
                    BindData();
                    BindCount();
                    Session["SortedView"] = null;
                    Bind_Dropdowns();
                }
                HideHelpLink();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        private void Active_DeactiveLinks()
        {
            LinkButton lb = (LinkButton)Master.FindControl("lbViewTicket");
            lb.CssClass = "dropdown-item active";
        }

        public void HideHelpLink()
        {
            LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            if (lbFullScreen != null)
                lbFullScreen.Visible = false;
        }

        public void Bind_Dropdowns()
        {
            RadComboBoxItem item1 = new RadComboBoxItem();
            item1.Text = "Open";
            item1.Value = "0";
            rdStatus.Items.Add(item1);
            RadComboBoxItem item2 = new RadComboBoxItem();
            item2.Text = "ReOpen";
            item2.Value = "1";
            rdStatus.Items.Add(item2);
            RadComboBoxItem item3 = new RadComboBoxItem();
            item3.Text = "WIP";
            item3.Value = "2";
            rdStatus.Items.Add(item3);
            RadComboBoxItem item4 = new RadComboBoxItem();
            item4.Text = "Closed";
            item4.Value = "3";
            rdStatus.Items.Add(item4);


            RadComboBoxItem rditem1 = new RadComboBoxItem();
            rditem1.Text = "High";
            rditem1.Value = "0";
            rdPriority.Items.Add(rditem1);
            RadComboBoxItem rditem2 = new RadComboBoxItem();
            rditem2.Text = "Medium";
            rditem2.Value = "1";
            rdPriority.Items.Add(rditem2);
            RadComboBoxItem rditem3 = new RadComboBoxItem();
            rditem3.Text = "Low";
            rditem3.Value = "2";
            rdPriority.Items.Add(rditem3);
        }

        public void DisableControls()
        {
            txtComment.Enabled = true;
            txtTicketID.Enabled = false;
            txtName.Enabled = false;
            txtCategory.Enabled = false;
            txtSubcategory.Enabled = false;
            txtIssue.Enabled = false;
            ddlStatus.Enabled = false;
            txtCreateDate.Enabled = false;
        }

        public void BindData()
        {
            Gvd_ViewOpenTicket.DataSource = ticketDetailMethods.viewAllTicket(Session["UserName"].ToString());
            Gvd_ViewOpenTicket.DataBind();

            Session["GridBind"] = null;
            lobjDT = ticketDetailMethods.viewAllTicket(Session["UserName"].ToString()) as DataTable;
            Session["GridBind"] = lobjDT;
        }

        public void BindCount()
        {
            lobjDT = ticketDetailMethods.ViewTicketByStatus("Open") as DataTable;
            lblOpenCount1.Text = lobjDT.Rows.Count.ToString();

            lobjDT = ticketDetailMethods.ViewTicketByStatus("ReOpen") as DataTable;
            lblReopenCnt1.Text = lobjDT.Rows.Count.ToString();

            lobjDT = ticketDetailMethods.ViewTicketByStatus("WIP") as DataTable;
            lblWPCnt11.Text = lobjDT.Rows.Count.ToString();

            lobjDT = ticketDetailMethods.ViewTicketByStatus("Closed") as DataTable;
            lblClosedCnt11.Text = lobjDT.Rows.Count.ToString();

            int total = Convert.ToInt32(lblOpenCount1.Text) + Convert.ToInt32(lblReopenCnt1.Text) + Convert.ToInt32(lblWPCnt11.Text) + Convert.ToInt32(lblClosedCnt11.Text);
            lblTotalCount1.Text = total.ToString();
        }

        protected void btnViewTickets_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.Text = string.Empty;
                SelectionBasedBind();
                Session["SortedView"] = null;

                lobjDT = ticketDetailMethods.AllCheck() as DataTable;
                lblTotalCount1.Text = lobjDT.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void TextBasedBind()
        {
            string search = txtID.Text;
            List<string> lst1 = new List<string>();
            if (rdStatus.CheckedItems.Count > 0)
            {
                for (int i = 0; i < rdStatus.Items.Count; i++)
                {
                    if (rdStatus.Items[i].Checked)
                        lst1.Add(rdStatus.Items[i].Text);
                    if (i == 0)
                    {
                        if (rdStatus.Items[i].Checked)
                        {
                            lblOpenCount1.Enabled = true;
                        }
                        else
                        {
                            lblOpenCount1.Text = "0";
                            lblOpenCount1.Enabled = false;
                        }
                    }
                    if (i == 1)
                    {
                        if (rdStatus.Items[i].Checked)
                        {
                            lblReopenCnt1.Enabled = true;
                        }
                        else
                        {
                            lblReopenCnt1.Text = "0";
                            lblReopenCnt1.Enabled = false;
                        }
                    }
                    if (i == 2)
                    {
                        if (rdStatus.Items[i].Checked)
                        {
                            lblWPCnt11.Enabled = true;
                        }
                        else
                        {
                            lblWPCnt11.Text = "0";
                            lblWPCnt11.Enabled = false;
                        }
                    }
                    if (i == 3)
                    {
                        if (rdStatus.Items[i].Checked)
                        {
                            lblClosedCnt11.Enabled = true;
                        }
                        else
                        {
                            lblClosedCnt11.Text = "0";
                            lblClosedCnt11.Enabled = false;
                        }
                    }
                }
            }
            List<string> lst2 = new List<string>();
            for (int i = 0; i < rdPriority.Items.Count; i++)
            {
                if (rdPriority.Items[i].Checked)
                    lst2.Add(rdPriority.Items[i].Text);
            }

            Statuslist = "";
            for (int i = 0; i < lst1.Count; i++)
            {
                Statuslist += "\'" + lst1[i] + "\'" + ",";
            }
            Prioritylist = "";
            for (int i = 0; i < lst2.Count; i++)
            {
                Prioritylist += "\'" + lst2[i] + "\'" + ",";
            }

            if (rdStatus.CheckedItems.Count != 0 && rdPriority.CheckedItems.Count != 0 && txtID.Text != string.Empty) // Both Selected
            {
                TicketStatus = Statuslist.Substring(0, Statuslist.Length - 1);
                TicketPriority = Prioritylist.Substring(0, Prioritylist.Length - 1);
                lobjDT = ticketDetailMethods.dropdownSearch(TicketStatus, TicketPriority, search) as DataTable;
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;
                Gvd_ViewOpenTicket.DataSource = lobjDT;
                Gvd_ViewOpenTicket.DataBind();
                BindStatusLabelCount(lobjDT);
            }

            if (rdStatus.CheckedItems.Count == 0 && rdPriority.CheckedItems.Count != 0 && txtID.Text != string.Empty) // Status Null  Priority selected
            {
                TicketPriority = Prioritylist.Substring(0, Prioritylist.Length - 1);
                lobjDT = ticketDetailMethods.TextSearchTicketPriority(TicketPriority, search) as DataTable;
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;
                Gvd_ViewOpenTicket.DataSource = lobjDT;
                Gvd_ViewOpenTicket.DataBind();
                BindStatusLabelCount(lobjDT);
            }

            if (rdStatus.CheckedItems.Count != 0 && rdPriority.CheckedItems.Count == 0 && txtID.Text != string.Empty) // Status Selected Priority null
            {
                TicketStatus = Statuslist.Substring(0, Statuslist.Length - 1);
                lobjDT = ticketDetailMethods.TextSearchTicketStatus(TicketPriority, search) as DataTable;
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;
                Gvd_ViewOpenTicket.DataSource = lobjDT;
                Gvd_ViewOpenTicket.DataBind();
                BindStatusLabelCount(lobjDT);
            }

            if (rdStatus.CheckedItems.Count == 0 && rdPriority.CheckedItems.Count == 0 && txtID.Text != string.Empty) //Both  Status  Priority null
            {
                lobjDT = ticketDetailMethods.TextSearch(search) as DataTable;
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;
                Gvd_ViewOpenTicket.DataSource = lobjDT;
                Gvd_ViewOpenTicket.DataBind();
                BindStatusLabelCount(lobjDT);
            }
            if (rdStatus.CheckedItems.Count == 0 && rdPriority.CheckedItems.Count == 0 && txtID.Text == string.Empty) // All Blank
            {
                lobjDT = ticketDetailMethods.AllCheck() as DataTable;
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;

                Gvd_ViewOpenTicket.DataSource = lobjDT;
                Gvd_ViewOpenTicket.DataBind();
                BindStatusLabelCount(lobjDT);
            }

            if (txtID.Text != "")
            {
                CountPerEmployee();
            }
        }

        public void BindStatusLabelCount(DataTable lobjDT)
        {
            var rows = from row in lobjDT.AsEnumerable()
                       where row.Field<string>("Ticket_Status") == "Open"
                       select row;
            lblOpenCount1.Text = rows.Count().ToString();

            var rows1 = from row1 in lobjDT.AsEnumerable()
                        where row1.Field<string>("Ticket_Status") == "ReOpen"
                        select row1;
            lblReopenCnt1.Text = rows1.Count().ToString();

            var rows2 = from row2 in lobjDT.AsEnumerable()
                        where row2.Field<string>("Ticket_Status") == "WIP"
                        select row2;
            lblWPCnt11.Text = rows2.Count().ToString();

            var rows3 = from row3 in lobjDT.AsEnumerable()
                        where row3.Field<string>("Ticket_Status") == "Closed"
                        select row3;
            lblClosedCnt11.Text = rows3.Count().ToString();
        }

        public void SelectionBasedBind()
        {
            List<string> lst1 = new List<string>();
            if (rdStatus.CheckedItems.Count > 0)
            {
                for (int i = 0; i < rdStatus.Items.Count; i++)
                {
                    if (rdStatus.Items[i].Checked)
                        lst1.Add(rdStatus.Items[i].Text);
                    if (i == 0)
                    {
                        if (rdStatus.Items[i].Checked)
                        {
                            lblOpenCount1.Enabled = true;
                        }
                        else
                        {
                            lblOpenCount1.Text = "0";
                            lblOpenCount1.Enabled = false;
                        }
                    }
                    if (i == 1)
                    {
                        if (rdStatus.Items[i].Checked)
                        {
                            lblReopenCnt1.Enabled = true;
                        }
                        else
                        {
                            lblReopenCnt1.Text = "0";
                            lblReopenCnt1.Enabled = false;
                        }
                    }
                    if (i == 2)
                    {
                        if (rdStatus.Items[i].Checked)
                        {
                            lblWPCnt11.Enabled = true;
                        }
                        else
                        {
                            lblWPCnt11.Text = "0";
                            lblWPCnt11.Enabled = false;
                        }
                    }
                    if (i == 3)
                    {
                        if (rdStatus.Items[i].Checked)
                        {
                            lblClosedCnt11.Enabled = true;
                        }
                        else
                        {
                            lblClosedCnt11.Text = "0";
                            lblClosedCnt11.Enabled = false;
                        }
                    }
                }
            }
            List<string> lst2 = new List<string>();
            for (int i = 0; i < rdPriority.Items.Count; i++)
            {
                if (rdPriority.Items[i].Checked)
                    lst2.Add(rdPriority.Items[i].Text);
            }

            Statuslist = "";
            for (int i = 0; i < lst1.Count; i++)
            {
                Statuslist += "\'" + lst1[i] + "\'" + ",";
            }
            Prioritylist = "";
            for (int i = 0; i < lst2.Count; i++)
            {
                Prioritylist += "\'" + lst2[i] + "\'" + ",";
            }


            if (rdStatus.CheckedItems.Count != 0 && rdPriority.CheckedItems.Count != 0 && txtID.Text == string.Empty) // Both Selected
            {
                TicketStatus = Statuslist.Substring(0, Statuslist.Length - 1);
                TicketPriority = Prioritylist.Substring(0, Prioritylist.Length - 1);
                lobjDT = ticketDetailMethods.BothdropdownSelected(TicketStatus, TicketPriority) as DataTable;
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;
                Gvd_ViewOpenTicket.DataSource = lobjDT;
                Gvd_ViewOpenTicket.DataBind();
                BindStatusLabelCount(lobjDT);

            }

            if (rdStatus.CheckedItems.Count == 0 && rdPriority.CheckedItems.Count != 0 && txtID.Text == string.Empty) // Status Null  Priority selected
            {
                TicketPriority = Prioritylist.Substring(0, Prioritylist.Length - 1);
                lobjDT = ticketDetailMethods.DropdownPriority(TicketPriority) as DataTable;
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;
                Gvd_ViewOpenTicket.DataSource = lobjDT;
                Gvd_ViewOpenTicket.DataBind();
                BindStatusLabelCount(lobjDT);
            }

            if (rdStatus.CheckedItems.Count != 0 && rdPriority.CheckedItems.Count == 0 && txtID.Text == string.Empty) // Status Selected Priority null
            {
                TicketStatus = Statuslist.Substring(0, Statuslist.Length - 1);
                lobjDT = ticketDetailMethods.DropdownStatus(TicketStatus) as DataTable;
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;

                Gvd_ViewOpenTicket.DataSource = lobjDT;
                Gvd_ViewOpenTicket.DataBind();
                BindStatusLabelCount(lobjDT);
            }

            if (rdStatus.CheckedItems.Count == 0 && rdPriority.CheckedItems.Count == 0 && txtID.Text == string.Empty) // Status Selected Priority null
            {
                BindData();
            }
        }

        public void txtID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int num1;
                if (int.TryParse(txtID.Text, out num1) == false)
                {
                    TextBasedBind();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void lblOpenCount1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RadComboBoxItem item in rdStatus.Items)
                {
                    rdStatus.Items[0].Checked = true;
                    rdStatus.Items[1].Checked = false;
                    rdStatus.Items[2].Checked = false;
                    rdStatus.Items[3].Checked = false;
                }
                // To disable other linkbutton

                lblReopenCnt1.Text = "0";
                lblReopenCnt1.Enabled = false;

                lblClosedCnt11.Text = "0";
                lblClosedCnt11.Enabled = false;

                lblWPCnt11.Text = "0";
                lblWPCnt11.Enabled = false;

                if (txtID.Text == "")
                {
                    SelectionBasedBind();
                }
                else
                {
                    TextBasedBind();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void lblReopenCnt1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RadComboBoxItem item in rdStatus.Items)
                {
                    rdStatus.Items[0].Checked = false;
                    rdStatus.Items[1].Checked = true;
                    rdStatus.Items[2].Checked = false;
                    rdStatus.Items[3].Checked = false;
                }

                lblOpenCount1.Text = "0";
                lblOpenCount1.Enabled = false;

                lblClosedCnt11.Text = "0";
                lblClosedCnt11.Enabled = false;

                lblWPCnt11.Text = "0";
                lblWPCnt11.Enabled = false;

                if (txtID.Text == "")
                {
                    SelectionBasedBind();
                }
                else
                {
                    TextBasedBind();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void lblWPCnt1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RadComboBoxItem item in rdStatus.Items)
                {
                    rdStatus.Items[0].Checked = false;
                    rdStatus.Items[1].Checked = false;
                    rdStatus.Items[2].Checked = true;
                    rdStatus.Items[3].Checked = false;
                }

                lblOpenCount1.Text = "0";
                lblOpenCount1.Enabled = false;

                lblReopenCnt1.Text = "0";
                lblReopenCnt1.Enabled = false;

                lblClosedCnt11.Text = "0";
                lblClosedCnt11.Enabled = false;

                if (txtID.Text == "")
                {
                    SelectionBasedBind();
                }
                else
                {
                    TextBasedBind();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void lblClosedCnt11_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RadComboBoxItem item in rdStatus.Items)
                {
                    rdStatus.Items[0].Checked = false;
                    rdStatus.Items[1].Checked = false;
                    rdStatus.Items[2].Checked = false;
                    rdStatus.Items[3].Checked = true;
                }

                lblOpenCount1.Text = "0";
                lblOpenCount1.Enabled = false;

                lblReopenCnt1.Text = "0";
                lblReopenCnt1.Enabled = false;

                lblWPCnt11.Text = "0";
                lblWPCnt11.Enabled = false;

                if (txtID.Text == "")
                {
                    SelectionBasedBind();
                }
                else
                {
                    TextBasedBind();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void lblTotalCount1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RadComboBoxItem item in rdStatus.Items)
                {
                    item.Checked = true;
                }

                foreach (RadComboBoxItem item1 in rdPriority.Items)
                {
                    item1.Checked = true;
                }

                if (txtID.Text == "")
                {
                    lobjDT = ticketDetailMethods.AllCheck() as DataTable;
                    Gvd_ViewOpenTicket.DataSource = lobjDT;
                    Gvd_ViewOpenTicket.DataBind();
                }

                if (txtID.Text != "")
                {
                    lobjDT = ticketDetailMethods.SearchTicketByName_ID(txtID.Text) as DataTable;
                    Gvd_ViewOpenTicket.DataSource = lobjDT;
                    Gvd_ViewOpenTicket.DataBind();
                    BindStatusLabelCount(lobjDT);
                }
                Session["GridBind"] = null;
                Session["GridBind"] = lobjDT;
                if (txtID.Text == "")
                {
                    BindCount();
                }
                else
                {
                    CountPerEmployee();
                }

                lblOpenCount1.Enabled = true;
                lblReopenCnt1.Enabled = true;
                lblWPCnt11.Enabled = true;
                lblClosedCnt11.Enabled = true;
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void CountPerEmployee()
        {
            DataTable dtE = ticketDetailMethods.SearchTicketByName_ID(txtID.Text) as DataTable;
            string Total = dtE.Rows.Count.ToString();
            lblTotalCount1.Text = Total;
        }

        protected void Gvd_ViewOpenTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbStatus = (Label)e.Row.FindControl("lbStatus") as Label;

                    Label lbDate = (Label)e.Row.FindControl("lbDate") as Label;
                    LinkButton lnkSelect = (LinkButton)e.Row.FindControl("lnkSelect") as LinkButton;

                    DateTime Date = DateTime.Parse(lbDate.Text);
                    string Date1 = Date.ToString("MM/dd/yyyy");
                    lbDate.Text = Date1;

                    if (lbStatus.Text == "Closed")
                    {
                        lnkSelect.Enabled = false;
                        lnkSelect.ForeColor = Color.LightGray;
                        lnkSelect.Font.Bold = false;
                    }
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in Gvd_ViewOpenTicket.Rows)
                {
                    if ((sender as LinkButton).ClientID == (row.FindControl("lnkSelect") as LinkButton).ClientID)
                    {

                        Label lbTicketID = row.FindControl("lbTicketID") as Label;
                        Label lbName = row.FindControl("lbName") as Label;
                        Label lbCategory = row.FindControl("lbCategory") as Label;
                        Label lbSubCategory = row.FindControl("lbSubCategory") as Label;
                        Label lbIssue = row.FindControl("lbIssue") as Label;
                        Label lbPriority = row.FindControl("lbPriority") as Label;
                        Label lbDate = row.FindControl("lbDate") as Label;
                        Label lbUserEmail = row.FindControl("lbUserName") as Label;

                        txtTicketID.Text = lbTicketID.Text;
                        txtName.Text = lbName.Text;
                        txtIssue.Text = lbIssue.Text;
                        txtCategory.Text = lbCategory.Text;
                        txtSubcategory.Text = lbSubCategory.Text;
                        txtCreateDate.Text = lbDate.Text;
                        lblUserEmail.Text = lbUserEmail.Text;

                        ddlStatus.Enabled = true;
                        txtComment.Enabled = true;
                        btnCancel.Enabled = true;
                        btnUpdate.Enabled = true;
                       
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtTicketID.Text = string.Empty;
                txtName.Text = string.Empty;
                txtCategory.Text = string.Empty;
                txtSubcategory.Text = string.Empty;
                txtIssue.Text = string.Empty;
                txtCreateDate.Text = string.Empty;
                txtComment.Text = string.Empty;
                ddlStatus.SelectedIndex = 0;
                ddlStatus.Enabled = false;
                txtComment.Enabled = false;
                btnUpdate.Enabled = false;
                btnCancel.Enabled = false;
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                int returnvalue = ticketDetailMethods.updateOpenTicket(ddlStatus.SelectedValue, Convert.ToInt32(txtTicketID.Text), num, txtComment.Text);
                if (returnvalue == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Ticket Status is updated');", true);

                    int ticketId = Convert.ToInt32(txtTicketID.Text);
                    emailServices.SendTicketStatus(txtName.Text, Convert.ToInt16(txtTicketID.Text), ddlStatus.SelectedValue, txtComment.Text, lblUserEmail.Text.Trim());

                    txtTicketID.Text = " ";
                    txtName.Text = " ";
                    txtIssue.Text = " ";
                    txtCategory.Text = " ";
                    txtSubcategory.Text = " ";
                    txtCreateDate.Text = " ";
                    txtComment.Text = " ";
                    lblUserEmail.Text = "";
                    ddlStatus.SelectedIndex = 0;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Ticket Status is not updated');", true);
                }

                if (txtID.Text == " " || txtID.Text == "" || txtID.Text == string.Empty)
                {
                    SelectionBasedBind();
                }
                else
                {
                    TextBasedBind();
                }

                ddlStatus.SelectedIndex = 0;
                ddlStatus.Enabled = false;
                txtComment.Enabled = false;
                btnUpdate.Enabled = false;
                btnCancel.Enabled = false;
                ModalProgress.Hide();
            }
            catch (Exception ex)
            {
                ModalProgress.Hide();
                customLogger.Error(ex.Message);
            }

        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Descending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void Gvd_ViewOpenTicket_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                string sortingDirection = string.Empty;
                if (direction == SortDirection.Descending)
                {
                    direction = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }
                else
                {
                    direction = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                lobjDT = Session["GridBind"] as DataTable;

                if (lobjDT.Rows.Count != 0)
                {
                    DataView sortedView = new DataView(lobjDT);
                    sortedView.Sort = e.SortExpression + " " + sortingDirection;
                    Session["SortedView"] = sortedView;
                    Gvd_ViewOpenTicket.DataSource = sortedView;
                    Gvd_ViewOpenTicket.DataBind();

                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void Gvd_ViewOpenTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                Gvd_ViewOpenTicket.PageIndex = e.NewPageIndex;
                if (Session["SortedView"] != null)
                {
                    Gvd_ViewOpenTicket.DataSource = Session["SortedView"];
                    Gvd_ViewOpenTicket.DataBind();
                }
                else
                {
                    SelectionBasedBind();
                    TextBasedBind();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }
    }
}