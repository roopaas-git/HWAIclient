using CommonUtility;
using DataUtility;
using LoggerUtility;
using System;
using System.Data;

namespace HealthWorks.Pages
{
    public partial class ActivatePage : System.Web.UI.Page
    {
        ActivatePageDetails activatePageDetails;
        private DataTable dataTable = new DataTable();
        CustomLogger customLogger = new CustomLogger();
        private static string activationCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                Get_Activation(activationCode);
            }
        }

        private void Get_Activation(string activationCode)
        {
            try
            {
                activatePageDetails = new ActivatePageDetails();
                dataTable = activatePageDetails.Get_ActivationDetails(activationCode);

                if (dataTable.Rows.Count > 0)
                {
                    lblUserName.Text = dataTable.Rows[0]["FirstName"].ToString();
                    if (dataTable.Rows[0]["IsActive"].ToString() == "True")
                    {
                        lbl_Activetext.Text = Constants.activationSuccess;
                        btnActivate.Visible = false;
                        btnLogin.Visible = true;
                    }
                    else
                    {
                        lbl_Activetext.Text = Constants.accountNotActivatedFirst;
                        btnActivate.Visible = true;
                        btnLogin.Visible = false;
                    }
                }
                else
                {
                    pageName.Visible = false;
                    lbl_Activetext.Text = Constants.activationCodeInvalid;
                    btnActivate.Visible = false;
                    btnLogin.Visible = false;
                }
            }
            catch
            {
                pageName.Visible = false;
                lbl_Activetext.Text = Constants.activationCodeInvalid;
                btnActivate.Visible = false;
                btnLogin.Visible = false;
            }
            finally
            {
            }
        }

        private void activateAccount(string activationCode, string Name)
        {
            activatePageDetails = new ActivatePageDetails();
            int rowsAffected = activatePageDetails.ActivateAccount(activationCode);
            if (rowsAffected == 1)
            {
                lblUserName.Text = "Hi " + Name;
                // ltMessage.Text = Constants.activationSuccess;
                //  dispLogin.Visible = true;
            }
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            activatePageDetails = new ActivatePageDetails();
            int rowsAffected = activatePageDetails.ActivateAccount(activationCode);

            lbl_Activetext.Text = Constants.activationSuccess;
            btnActivate.Visible = false;
            btnLogin.Visible = true;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}