using CommonUtility;
using DataUtility;
using HealthWorks.Content.BO;
using System;
using System.Data;
using System.Web.UI.WebControls;
namespace HealthWorks.Pages
{
    public partial class HealthMasterDashboard : System.Web.UI.MasterPage
    {
        #region variables
        UserDetails userDetails;
        PageTracking PT = new PageTracking();
        NewsFeed newsFeed = new NewsFeed();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                userDetails = new UserDetails();
                string SessionID = userDetails.GetSessionID(Session["UserName"].ToString());
                Hdn_username.Value = Session["UserName"].ToString();
                if (SessionID != null && SessionID != Session["SessionID"].ToString())
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Bind_User_FirstName();
                        EnableDisableLinks();
                        BindNewsArticles();
                    }
                }
            }
        }

        private void Bind_User_FirstName()
        {
            userDetails = new UserDetails();
            Session["FirstName"] = userDetails.GetFirstName(Session["UserName"].ToString());
            lblUserName.Text = Session["FirstName"].ToString();
        }

        private void EnableDisableLinks()
        {
            string domainName = Session["UserName"].ToString().Split('@')[1];

            if (domainName != Constants.teganalytics && domainName != Constants.healthworksai)
            {
                lbViewTicket.Visible = false;
                lbTicketHistory.Visible = false;
            }
        }

        protected void lbExternal_Click(object sender, EventArgs e)
        {
            LinkButton lbExternal = sender as LinkButton;
            PT.InsertDataIntoDB(lbExternal.CommandArgument, Session["SessionId"].ToString(), Session["UserName"].ToString(), lbExternal.CommandName);
            string redirectToPage = string.Empty;
            redirectToPage = "~/Pages/" + lbExternal.CommandName;
            Response.Redirect(redirectToPage);
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.LogoutSession(Session["UserName"].ToString());
            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }

        public void BindNewsArticles()
        {
            try
            {
                DataTable dataTable = newsFeed.FetchNewsFromRSS();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        lblNewsFirst.Text = dataTable.Rows[i]["title"].ToString() + ";";
                        lblNewsFirst.NavigateUrl = dataTable.Rows[i]["link"].ToString();
                    }

                    if (i == 1)
                    {
                        // lblNewsSecond.Text = dataTable.Rows[i]["description"].ToString() + ";";
                        lblNewsSecond.Text = dataTable.Rows[i]["title"].ToString() + ";";
                        lblNewsSecond.NavigateUrl = dataTable.Rows[i]["link"].ToString();
                    }

                    if (i == 2)
                    {
                        // lblNewsThird.Text = dataTable.Rows[i]["description"].ToString() + ";";
                        lblNewsThird.Text = dataTable.Rows[i]["title"].ToString() + ";";
                        lblNewsThird.NavigateUrl = dataTable.Rows[i]["link"].ToString();
                    }

                    if (i == 3)
                    {
                        //lblNewsFourth.Text = dataTable.Rows[i]["description"].ToString() + ";";
                        lblNewsFourth.Text = dataTable.Rows[i]["title"].ToString() + ";";
                        lblNewsFourth.NavigateUrl = dataTable.Rows[i]["link"].ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}