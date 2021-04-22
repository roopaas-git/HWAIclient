using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using DataUtility;
using LoggerUtility;

namespace HealthWorks.Pages
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        UserDetails userDetails;
        CustomLogger customLogger = new CustomLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "HealthWorksAI - Profile";
            try
            {
                if (Session["Username"] == null)
                    Response.Redirect("~/Default.aspx");
                else
                {
                    Active_DeactiveLinks();
                }

                if (!IsPostBack)
                {
                    txtPWD.Text = "";
                    txtNewPassword.Text = string.Empty;
                    txtConfirmPWD.Text = string.Empty;
                }
                HideHelpLink();

            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }
        public void HideHelpLink()
        {
            LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            if (lbFullScreen != null)
                lbFullScreen.Visible = false;
        }
        private void Active_DeactiveLinks()
        {
            LinkButton lb = (LinkButton)Master.FindControl("lbChangePassword");
            lb.CssClass = "dropdown-item active";
        }
        protected void btnChangePWD_Click(object sender, EventArgs e)
        {
            try
            {
                bool isvalid = Membership.ValidateUser(Session["Username"].ToString(), txtPWD.Text.ToString());

                if (isvalid)
                {
                    Get_And_Update_PassWord();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password is not correct');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "');", true);
                customLogger.Error(ex.Message);
            }
        }
        private void Get_And_Update_PassWord()
        {
            try
            {
                string strUserId = Get_UseID();
                UpdatePassword(strUserId);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }

        }
        private void UpdatePassword(string UserID)
        {
            try
            {
                userDetails = new UserDetails();
                int GetInsertId = userDetails.ChangePassword(UserID, txtNewPassword.Text.ToString());
                userDetails.check_User_Password(Session["UserName"].ToString(), txtNewPassword.Text.Trim());
                if (GetInsertId == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password updated Successfully');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "');", true);
                customLogger.Error(ex.Message);
            }
        }
        private string Get_UseID()
        {
            userDetails = new UserDetails();
            DataSet lobjDS = userDetails.GetUserId(Session["Username"].ToString()) as DataSet;
            return lobjDS.Tables[0].Rows[0][0].ToString();
        }
    }
}