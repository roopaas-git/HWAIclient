using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using DataUtility;
using LoggerUtility;

namespace HealthWorks.Pages
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        UserDetails userDetails;
        CustomLogger customLogger = new CustomLogger();
        private static string UserId;
        private static string Password;
        private DataTable dataTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                UserId = !string.IsNullOrEmpty(Request.QueryString["UserId"]) ? Request.QueryString["UserId"] : Guid.Empty.ToString();
                GetDetails(UserId);
            }
        }

        private void GetDetails(string userId)
        {
            userDetails = new UserDetails();
            dataTable = userDetails.Get_UserDetails(UserId);

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["IsResetPasswordActive"].ToString() == "0" || DateTime.Now > Convert.ToDateTime(dataTable.Rows[0]["ResetPasswordExpireDate"].ToString()).AddDays(1))
                {
                   // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Link is expired');", true);
                    resetfailure.Visible = true;
                    resetsucess.Visible = false;
                    reset.Visible = false;
                    pageName.Visible = false;
                    // Response.Redirect("~/Default.aspx");
                }
                else
                {
                    txt_Email.Text = dataTable.Rows[0]["UserName"].ToString();
                    txtPWD.Text = dataTable.Rows[0]["PassWord"].ToString();
                    Password = dataTable.Rows[0]["PassWord"].ToString();
                    resetfailure.Visible = false;
                    resetsucess.Visible = false;
                    reset.Visible = true;
                    pageName.Visible = true;
                }
            }

        }

        protected void btnChangePWD_Click(object sender, EventArgs e)
        {
            try
            {
                bool isvalid = Membership.ValidateUser(txt_Email.Text.ToString(), Password);

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
                //  string strUserId = Get_UseID();
                UpdatePassword(UserId);
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
                userDetails.check_User_Password(txt_Email.Text, txtNewPassword.Text.Trim());
                if (GetInsertId == 0)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your password has been reset.');", true);
                    userDetails = new UserDetails();
                    userDetails.ResetPasswordStatus(UserId.ToString(), 1);
                    resetsucess.Visible = true;
                    reset.Visible = false;
                    pageName.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message + "');", true);
                customLogger.Error(ex.Message);
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {            
            Response.Redirect("~/Default.aspx");
        }
    }
}