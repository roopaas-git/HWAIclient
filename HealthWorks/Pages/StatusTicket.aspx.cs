using BusinessUtility;
using CommonUtility;
using LoggerUtility;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class StatusTicket : System.Web.UI.Page
    {
        TicketDetailMethods ticketDetailMethods = new TicketDetailMethods();
        CustomLogger customLogger = new CustomLogger();
        EmailServices emailServices = new EmailServices();
        public DateTime now = DateTime.Now;
        public DataTable lobjDT;

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
                    Session["Count"] = 0;
                    Session["CountSort"] = 0;
                    Session["SortedView"] = null;
                    BindTicketDetails();
                    Bind_Dates();
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
            LinkButton lb = (LinkButton)Master.FindControl("lbStatus");
            lb.CssClass = "dropdown-item active";
        }

        public void HideHelpLink()
        {
            ((HtmlControl)Master.FindControl("liFullScreen")).Attributes["class"] = "hidden";
        }

        public void Bind_Dates()
        {
            DateTime today = DateTime.Now;
            DateTime firstday = new DateTime(today.Year, today.Month, 1);
            txtStartDate.Text = firstday.Date.ToShortDateString();
            txtEndDate.Text = today.ToShortDateString();
            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }

        public void DisableControls()
        {
            txtTicketID.Enabled = false;
            txtName1.Enabled = false;
            txtCategory.Enabled = false;
            txtSubcategory.Enabled = false;
            txtPriority.Enabled = false;
            txtIssue1.Enabled = false;
            ddlReopen.Enabled = false;
            txtCreatedate.Enabled = false;

            txtStartDate.Attributes.Add("readonly", "readonly");
            txtEndDate.Attributes.Add("readonly", "readonly");
        }

        public void BindTicketDetails()
        {
            Gvd_ViewTicket.DataSource = ticketDetailMethods.viewTicket(Session["UserName"].ToString());
            Gvd_ViewTicket.DataBind();
        }

        protected void lnkSelect_Click(object sender, System.EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in Gvd_ViewTicket.Rows)
                {
                    if ((sender as LinkButton).ClientID == (row.FindControl("lnkSelect") as LinkButton).ClientID)
                    {
                        Label lbTicketID = row.FindControl("lbTicketID") as Label;
                        Label lbName = row.FindControl("lbName") as Label;
                        Label lbCategory = row.FindControl("lbCategory") as Label;
                        Label lbSubCategory = row.FindControl("lbSubCategory") as Label;
                        Label lbIssuegrd = row.FindControl("lbIssuegrd") as Label;
                        Label lborioritygrd = row.FindControl("lborioritygrd") as Label;
                        Label lbDategrd = row.FindControl("lbDategrd") as Label;
                        txtTicketID.Text = lbTicketID.Text;
                        txtName1.Text = lbName.Text;
                        txtIssue1.Text = lbIssuegrd.Text;
                        txtCategory.Text = lbCategory.Text;
                        txtSubcategory.Text = lbSubCategory.Text;
                        txtPriority.Text = lborioritygrd.Text;

                        txtCreatedate.Text = lbDategrd.Text; ;
                        btnReopen.Enabled = true;
                        btnCancel2.Enabled = true;
                       // ddlReopen.Enabled = true;
                        btnReopen.Focus();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void ddlReopen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlReopen.Enabled = true;
            txtIssue1.Enabled = true;
        }

        protected void Gvd_ViewTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbStatus = (Label)e.Row.FindControl("lbStatus") as Label;
                    Label lbDategrd = (Label)e.Row.FindControl("lbDategrd") as Label;
                    LinkButton lnkSelect = (LinkButton)e.Row.FindControl("lnkSelect") as LinkButton;
                    if (lbStatus.Text != "Closed")
                    {
                        lnkSelect.Enabled = false;
                        lnkSelect.ForeColor = Color.LightGray;
                        lnkSelect.Font.Bold = false;
                    }
                    DateTime Date = DateTime.Parse(lbDategrd.Text);
                    string Date1 = Date.ToString("MM/dd/yyyy");
                    lbDategrd.Text = Date1;
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void btnReopen_Click(object sender, System.EventArgs e)
        {
            try
            {
                int num = 0;
                int returnvalue = ticketDetailMethods.updateTicket(txtIssue1.Text, txtPriority.Text, ddlReopen.SelectedItem.Text, Convert.ToInt32(txtTicketID.Text), num);

                if (returnvalue == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Ticket is Re-Opened');", true);
                    emailServices.SendTicketDetails(Session["FirstName"].ToString(), returnvalue, txtPriority.Text, "Ticket is Re-Opened !", Session["UserName"].ToString(), txtIssue1.Text, txtSubcategory.Text);
                    ModalProgress.Hide();
                    Clear_Textboxes();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Ticket is not Re-Opened');", true);
                }

                Session["SortedView1"] = null;
                Gvd_ViewTicket.DataSource = ticketDetailMethods.filterDisplay(txtStartDate.Text, txtEndDate.Text.ToString(), Session["UserName"].ToString());
                Gvd_ViewTicket.DataBind();
                ddlReopen.SelectedIndex = 0;
                txtPriority.Text = string.Empty;
                
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            ModalProgress.Hide();

        }

        public void Clear_Textboxes()
        {
            txtTicketID.Text = string.Empty;
            txtName1.Text = string.Empty;
            txtIssue1.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtSubcategory.Text = string.Empty;
            txtCreatedate.Text = string.Empty;
            txtPriority.Text = string.Empty;
            btnCancel2.Enabled = false;
            btnReopen.Enabled = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int filter = 0;
                for (int n = 0; n < 1; n++)
                {
                    filter++;
                }
                Session["Count"] = filter;
                SearchTicketsUsingDates();
                Session["SortedView1"] = null;
                Clear_Textboxes();
                ddlReopen.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void SearchTicketsUsingDates()
        {
            Gvd_ViewTicket.DataSource = ticketDetailMethods.filterDisplay(txtStartDate.Text, txtEndDate.Text, Session["UserName"].ToString());
            Gvd_ViewTicket.DataBind();
        }

        protected void Gvd_ViewTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                Gvd_ViewTicket.PageIndex = e.NewPageIndex;
                int n = Convert.ToInt16(Session["Count"].ToString());  //Enters search process
                int m = Convert.ToInt16(Session["CountSort"].ToString());  //Enter Sorting process
                if (n != 0)
                {
                    if (m == 0)
                    {
                        Session["SortedView1"] = null;
                        Gvd_ViewTicket.DataSource = ticketDetailMethods.filterDisplay(txtStartDate.Text, txtEndDate.Text.ToString(), Session["UserName"].ToString());
                        Gvd_ViewTicket.DataBind();
                    }
                    if (m == 1)
                    {
                        if (Session["SortedView1"] != null)
                        {
                            Gvd_ViewTicket.DataSource = Session["SortedView1"];
                            Gvd_ViewTicket.DataBind();
                        }
                        else
                        {
                            Gvd_ViewTicket.DataSource = ticketDetailMethods.filterDisplay(txtStartDate.Text, txtEndDate.Text.ToString(), Session["UserName"].ToString());
                            Gvd_ViewTicket.DataBind();
                        }
                    }
                }
                else
                {
                    if (Session["SortedView"] != null)
                    {
                        Gvd_ViewTicket.DataSource = Session["SortedView"];
                        Gvd_ViewTicket.DataBind();
                    }
                    else
                    {
                        Gvd_ViewTicket.DataSource = ticketDetailMethods.viewTicket(Session["UserName"].ToString());
                        Gvd_ViewTicket.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void Gvd_ViewTicket_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                int filter = 0;
                for (int m = 0; m < 1; m++)
                {
                    filter++;
                }
                Session["CountSort"] = filter;

                string sortingDirection = string.Empty;
                if (direction == SortDirection.Ascending)
                {
                    direction = SortDirection.Descending;
                    sortingDirection = "Desc";
                }
                else
                {
                    direction = SortDirection.Ascending;
                    sortingDirection = "Asc";
                }

                int n = Convert.ToInt16(Session["Count"].ToString());
                if (n != 0)
                {
                    Gvd_ViewTicket.DataSource = ticketDetailMethods.filterDisplay(txtStartDate.Text, txtEndDate.Text.ToString(), Session["UserName"].ToString());
                    lobjDT = (Gvd_ViewTicket.DataSource as DataSet).Tables[0];
                    if (lobjDT != null)
                    {
                        DataView sortedView1 = new DataView(lobjDT);
                        sortedView1.Sort = e.SortExpression + " " + sortingDirection;
                        Session["SortedView1"] = sortedView1;
                        Gvd_ViewTicket.DataSource = sortedView1;
                        Gvd_ViewTicket.DataBind();
                    }
                }
                else
                {
                    Gvd_ViewTicket.DataSource = ticketDetailMethods.viewTicket(Session["UserName"].ToString());
                    lobjDT = (Gvd_ViewTicket.DataSource as DataSet).Tables[0];
                    if (lobjDT != null)
                    {
                        DataView sortedView = new DataView(lobjDT);
                        sortedView.Sort = e.SortExpression + " " + sortingDirection;
                        Session["SortedView"] = sortedView;
                        Gvd_ViewTicket.DataSource = sortedView;
                        Gvd_ViewTicket.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void btnCancel2_Click(object sender, System.EventArgs e)
        {
            try
            {
                txtTicketID.Text = string.Empty;
                txtName1.Text = string.Empty;
                txtCategory.Text = string.Empty;
                txtSubcategory.Text = string.Empty;
                txtIssue1.Text = string.Empty;
                txtPriority.Text = string.Empty;
                ddlReopen.SelectedIndex = 0;
                txtCreatedate.Text = string.Empty;
                btnReopen.Enabled = false;
                btnCancel2.Enabled = false;
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }

        }
    }
}