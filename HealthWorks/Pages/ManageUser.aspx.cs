using CommonUtility;
using DataUtility;
using LoggerUtility;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace HealthWorks.Pages
{
    public partial class ManageUser : System.Web.UI.Page
    {
        CustomLogger customLogger = new CustomLogger();
        UserDetails userDetails;
        EmailServices emailServices;
        public SqlCommand lobjCmd;
        public DataSet lobjDS;
        public SqlDataAdapter lobjDA;
        public string message = string.Empty;

        SqlConnection lobjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());


        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "HealthWorksAI - User";
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

                if (!IsPostBack)
                {
                    Bind_User_Details();
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
           
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ManageUser"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
            }
        }

        private void Bind_User_Details()
        {
            userDetails = new UserDetails();
            DataTable dataTable = userDetails.GetUsersList();

            if (dataTable.Rows.Count > 0)
            {
                GV_Users.DataSource = dataTable;
                GV_Users.DataBind();
            }
            else
            {
                GV_Users.DataSource = null;
                GV_Users.DataBind();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterUser();
            Bind_User_Details();

        }

        private void RegisterUser()
        {
            userDetails = new UserDetails();
            object key = new Guid();
            int userId = 0;

            MembershipCreateStatus status;
            MembershipUser user = Membership.Provider.CreateUser(txtEmail.Text.ToString(), txtPassword.Text.ToString(), txtEmail.Text.ToString(), null, null, true, null, out status);
            if (status == MembershipCreateStatus.Success)
            {
                key = user.ProviderUserKey;
            }
            else
            {
                userId = -3;
            }

            if (user != null)
            {
                userId = userDetails.SaveLoginDetails(key, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPassword.Text, txtMobile.Text, ddlUserType.SelectedValue, txtJobTitle.Text, txtDepartment.Text);
            }

            switch (userId)
            {
                case -1:
                    message = "Username already exists.\\nPlease choose a different username.";
                    break;
                case -2:
                    message = "Supplied email address has already been used.";
                    break;
                case -3:
                    message = "Username already exists.\\nPlease choose a different username.";
                    break;
                default:
                    message = "Registration successful. Activation email has been sent.";
                    SendActivationEmail((Guid)key);
                    break;
            }
            ModalProgress.Hide();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "');", true);

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMobile.Text = "";
            txtDepartment.Text = "";
            txtEmail.Text = "";
            txtJobTitle.Text = "";
            ddlUserType.SelectedIndex = 0;

        }

        private void SendActivationEmail(Guid key)
        {
            try
            {
                emailServices = new EmailServices();
                emailServices.SendActivationMail(key, txtEmail.Text.Trim(), txtFirstName.Text.Trim(), txtPassword.Text);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        protected void GV_Users_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            userDetails = new UserDetails();
            int row = e.RowIndex;
            Label lbl = (Label)GV_Users.Rows[row].FindControl("LblId");
            Label lblUserName = (Label)GV_Users.Rows[row].FindControl("LblUserName");
            Membership.DeleteUser(lblUserName.Text.ToString());

            if (lbl != null)
            {
                userDetails.DeleteRecord(Convert.ToInt32(lbl.Text));
            }
            Bind_User_Details();

            message = "User Deleted.";
            ModalProgress.Hide();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "');", true);
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_Users.EditIndex = e.NewEditIndex;
            Bind_User_Details();
            ModalProgress.Hide();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GV_Users.EditIndex = -1;
            Bind_User_Details();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblSid = (Label)GV_Users.Rows[e.RowIndex].FindControl("LblId");
            TextBox txtStstus = (TextBox)GV_Users.Rows[e.RowIndex].FindControl("txtIsActive");

            lobjCon.Open();
            lobjCmd = new SqlCommand("Update_userstatus ", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@Status", txtStstus.Text.ToString());
            lobjCmd.Parameters.AddWithValue("@Id", lblSid.Text.ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
            GV_Users.EditIndex = -1;
            Bind_User_Details();

            message = "User Activated sucessfully.";
            ModalProgress.Hide();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + message + "');", true);
        }
    }
}