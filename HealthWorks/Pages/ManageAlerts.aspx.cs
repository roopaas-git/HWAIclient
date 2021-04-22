using CommonUtility;
using DataUtility;
using LoggerUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class ManageAlerts : System.Web.UI.Page
    {
        Dictionary<string, string> dictNames = new Dictionary<string, string>();
        TEGAlerts tegAlerts;
        CustomLogger customLogger = new CustomLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    Bind_Alert();
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
            lbFullScreen.Visible = false;
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ManageAlerts"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            tegAlerts = new TEGAlerts();
            tegAlerts.Insert_Alerts(txtAlert.Text.Trim(),Session["UserName"].ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Constants.successfull + "');", true);
            Bind_Alert();
            txtAlert.Text = "";
        }

        private void Bind_Alert()
        {
            tegAlerts = new TEGAlerts();
            DataTable dataTable = tegAlerts.Get_Alerts();

            if (dataTable.Rows.Count > 0)
            {
                GV_Alert.DataSource = dataTable;
                GV_Alert.DataBind();
            }
            else
            {
                GV_Alert.DataSource = null;
                GV_Alert.DataBind();
            }
        }

        protected void GV_Alert_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                tegAlerts = new TEGAlerts();
                int row = e.RowIndex;
                Label LblId = (Label)GV_Alert.Rows[row].FindControl("LblId");

                if (LblId != null)
                {
                    int result = tegAlerts.Delete_Alert(Convert.ToInt32(LblId.Text));

                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Constants.successfullyDeleted + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Constants.somethingWentWrong + "');", true);
                    }
                }

                Bind_Alert();
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }
    }
}