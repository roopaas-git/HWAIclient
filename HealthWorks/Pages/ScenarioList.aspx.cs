using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class ScenarioList : System.Web.UI.Page
    {
        public SqlConnection lobjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
        public SqlCommand lobjCmd;
        public DataSet lobjDS;
        public SqlDataAdapter lobjDA;
        public static int isFirst = 0;
        PageTracking PT = new PageTracking();

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

                    if (!IsPostBack)
                    {
                        Bind_Data();
                        Bind_Scenario();
                        Bind_UserList();
                    }
                    Active_DeactiveLinks();
                    this.Validate();
                }
            }
            catch (Exception ex1)
            {

            }
        }

        protected void lnk_ScenarioList_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("ScenarioList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ScenarioList.aspx");
                Response.Redirect("~/Pages/ScenarioList.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void lnk_Planfinder_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("PlanFinder", Session["SessionId"].ToString(), Session["UserName"].ToString(), "PlanFinder.aspx");
                Response.Redirect("~/Pages/PlanFinder.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void lnk_QuickAccess_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("QuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "QuickAccess.aspx");
                Response.Redirect("~/Pages/QuickAccess.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void lnk_Simulated_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("SimulatorResult", Session["SessionId"].ToString(), Session["UserName"].ToString(), "SimulatorResult.aspx");
                Response.Redirect("~/Pages/SimulatorResult.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ProductIntelC2L3li"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
            }

        
        }

        public void Bind_Scenario()
        {
            if (Session["CheckedScenario"] != null)
            {
                foreach (GridViewRow row1 in grdScenario.Rows)
                {
                    RadioButton chkRow = (row1.Cells[0].FindControl("rbSelect") as RadioButton);
                    chkRow.Text = Session["CheckedScenario"].ToString();
                }
            }
        }

        public void Bind_Data()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_All_Scenarios", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@userName", Session["UserName"].ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                grdScenario.DataSource = lobjDS;
                grdScenario.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
              
            }
        }

        private void CheckQuickAccess()
        {
            SqlCommand lobjCmd = new SqlCommand("Get_QuickAccess1", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Convert.ToInt32(Session["sId"].ToString()));
            lobjCon.Open();
            SqlDataAdapter sd = new SqlDataAdapter(lobjCmd);
            DataSet ds = new DataSet();
            sd.Fill(ds);
            lobjCon.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                id_Quick.Style.Add("pointer-events", "auto");
                id_Simulate.Style.Add("pointer-events", "auto");
            }
            else
            {
                id_Quick.Style.Add("pointer-events", "none");
                id_Simulate.Style.Add("pointer-events", "none");
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (Session["sId"] != null)
            {
                try
                {
                    lobjCmd = new SqlCommand("Get_Saved_Pages", lobjCon);
                    lobjCmd.CommandType = CommandType.StoredProcedure;
                    lobjCmd.Parameters.AddWithValue("@sId", Convert.ToInt32(Session["sId"].ToString()));
                    lobjDA = new SqlDataAdapter(lobjCmd);
                    lobjDS = new DataSet();
                    lobjDA.Fill(lobjDS);

                    if (lobjDS.Tables[0].Rows.Count > 0)
                    {
                        PT.InsertDataIntoDB(lobjDS.Tables[0].Rows[0][0].ToString().Replace(".aspx", ""), Session["SessionId"].ToString(), Session["UserName"].ToString(), lobjDS.Tables[0].Rows[0][0].ToString());
                        Response.Redirect("~/Pages/" + lobjDS.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        PT.InsertDataIntoDB("PlanFinder", Session["SessionId"].ToString(), Session["UserName"].ToString(), "PlanFinder.aspx");
                        Response.Redirect("~/Pages/PlanFinder.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                }
            }
            else
            {
                PT.InsertDataIntoDB("PlanFinder", Session["SessionId"].ToString(), Session["UserName"].ToString(), "PlanFinder.aspx");
                Response.Redirect("~/Pages/PlanFinder.aspx");
            }
        }

        protected void rbSelect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow oldrow in grdScenario.Rows)
            {
                ((RadioButton)oldrow.FindControl("rbSelect")).Checked = false;
            }

            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbSelect")).Checked = true;

            btnLoad.Enabled = true;
            id_nav.Style.Add("pointer-events", "auto");

            foreach (GridViewRow row1 in grdScenario.Rows)
            {
                RadioButton chkRow = (row1.Cells[0].FindControl("rbSelect") as RadioButton);
                if (chkRow.Checked)
                {
                    Label LblBidID = row.FindControl("lblId") as Label;
                    Session["sName"] = ((Label)row.FindControl("LblScenarioName")).Text;
                    Session["sId"] = LblBidID.Text;
                    //Added
                    CheckQuickAccess();
                    Session["CheckedScenario"] = chkRow.Text;
                }
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            grdScenario.EditIndex = e.NewEditIndex;
            Bind_Data();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            grdScenario.EditIndex = -1;
            Bind_Data();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblSid = (Label)grdScenario.Rows[e.RowIndex].FindControl("lblId");
            TextBox txtScenarioName = (TextBox)grdScenario.Rows[e.RowIndex].FindControl("txtScenarioName");
            TextBox txtDescription = (TextBox)grdScenario.Rows[e.RowIndex].FindControl("txtDescription");

            lobjCon.Open();
            lobjCmd = new SqlCommand("Update_Scenario", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioName", txtScenarioName.Text.ToString().Trim());
            lobjCmd.Parameters.AddWithValue("@Description", txtDescription.Text.ToString().Trim());
            lobjCmd.Parameters.AddWithValue("@sId", Convert.ToInt32(lblSid.Text));
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
            grdScenario.EditIndex = -1;
            Bind_Data();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)grdScenario.Rows[e.RowIndex];
            Label lblSid = (Label)row.FindControl("lblId");
            lobjCon.Open();
            lobjCmd = new SqlCommand("Delete_Scenario", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@sId", Convert.ToInt32(lblSid.Text));
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();

            Bind_Data();
        }

        protected void grdScenario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbOOPC = (LinkButton)e.Row.FindControl("lbOOPCCheck");
                LinkButton Lnk_Share = (LinkButton)e.Row.FindControl("Lnk_Share");
                LinkButton lbDetailsCheck = (LinkButton)e.Row.FindControl("lbDetailsCheck");
                Label lblId = (Label)e.Row.FindControl("lblId");

                Label LblSharedBy = (Label)e.Row.FindControl("LblSharedBy");
                HtmlGenericControl divOOPCValue = (HtmlGenericControl)e.Row.FindControl("divOOPC");
                HtmlGenericControl divDeatilsValue = (HtmlGenericControl)e.Row.FindControl("divDeatils");
                int sid = Convert.ToInt32(((Label)e.Row.FindControl("lblId")).Text);
                lobjCmd = new SqlCommand("Get_Record_Count_Of_Plans", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@sid", sid);
                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                lobjCon.Close();

                int recValue = Convert.ToInt32(lobjDS.Tables[0].Rows[0]["Records"].ToString());

                if (LblSharedBy.Text == string.Empty || LblSharedBy.Text == null)
                {
                    LblSharedBy.Text = "-";
                    lbDetailsCheck.Style.Add("pointer-events", "none");
                    lbDetailsCheck.Style.Add("color", "gray");
                    lbDetailsCheck.Style.Add("opacity", "0.2");
                }
                else
                {
                    lbDetailsCheck.Enabled = true;
                    lobjCmd = new SqlCommand("Get_All_Scenarios_Description", lobjCon);
                    lobjCmd.CommandType = CommandType.StoredProcedure;
                    lobjCmd.Parameters.AddWithValue("@userName", Session["UserName"].ToString());
                    lobjCmd.Parameters.AddWithValue("@sid", sid);
                    lobjDA = new SqlDataAdapter(lobjCmd);
                    lobjDS = new DataSet();
                    lobjDA.Fill(lobjDS);

                    if (lobjDS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < lobjDS.Tables[0].Rows.Count; i++)
                        {
                            HtmlGenericControl span1 = new HtmlGenericControl("span");
                            span1.ID = lobjDS.Tables[0].Rows[i]["sId"].ToString() + "_" + i.ToString();
                            span1.InnerHtml = "Description : " + lobjDS.Tables[0].Rows[i]["SharedDescription"].ToString() + "</b>" + "<br/>" + "Modified By : " + lobjDS.Tables[0].Rows[i]["ModifiedBy"].ToString();
                            span1.Attributes["class"] = "dropdown-item-text";
                            divDeatilsValue.Controls.Add(span1);
                        }
                    }
                }

                if (recValue > 0)
                {
                    lbOOPC.Enabled = true;
                    Lnk_Share.Enabled = true;
                    lobjCmd = new SqlCommand("Get_All_Scenarios_Test", lobjCon);
                    lobjCmd.CommandType = CommandType.StoredProcedure;
                    lobjCmd.Parameters.AddWithValue("@userName", Session["UserName"].ToString());
                    lobjCmd.Parameters.AddWithValue("@sid", sid);
                    lobjDA = new SqlDataAdapter(lobjCmd);
                    lobjDS = new DataSet();
                    lobjDA.Fill(lobjDS);

                    if (lobjDS.Tables[0].Rows.Count > 0)
                    {
                        HtmlGenericControl table = new HtmlGenericControl("table");
                        HtmlGenericControl th1 = new HtmlGenericControl("td");
                        HtmlGenericControl th2 = new HtmlGenericControl("td");

                        th1.InnerHtml = "Plan Name";
                        th2.InnerHtml = "Score";
                        th1.Attributes.Add("style", "text-align:center;border:solid;border-width:1px; font-weight:bold; border-color:gray;height: 1px; background-color: #5C276E;color:#fff;");
                        th2.Attributes.Add("style", "text-align:center;border:solid;border-width:1px;  font-weight:bold;border-color:gray;height: 1px; background-color: #5C276E;color:#fff;");
                        HtmlGenericControl tr0 = new HtmlGenericControl("tr");
                        tr0.Attributes.Add("style", "height: 1px !important;");
                        tr0.Controls.Add(th1);
                        tr0.Controls.Add(th2);
                        table.Controls.Add(tr0);
                        divOOPCValue.Controls.Add(table);

                        for (int i = 0; i < lobjDS.Tables[0].Rows.Count; i++)
                        {
                            HtmlGenericControl tr = new HtmlGenericControl("tr");
                            HtmlGenericControl td1 = new HtmlGenericControl("td");
                            HtmlGenericControl td2 = new HtmlGenericControl("td");
                            table.ID = lobjDS.Tables[0].Rows[i]["Plan_Name"].ToString() + "_" + i.ToString();

                            td1.InnerText = lobjDS.Tables[0].Rows[i]["Plan_Name"].ToString();
                            td2.InnerText = Convert.ToString(Math.Round(Convert.ToDecimal(lobjDS.Tables[0].Rows[i]["OOPC Score"].ToString())));
                            td1.Attributes.Add("style", "text-align:center;border:solid;border-width:1px; border-color:gray;");
                            td2.Attributes.Add("style", "text-align:center;border:solid;border-width:1px; border-color:gray;");

                            tr.Controls.Add(td1);
                            tr.Controls.Add(td2);
                            table.Controls.Add(tr);
                            divOOPCValue.Controls.Add(table);
                        }
                    }
                }
                else
                {
                    lbOOPC.Style.Add("pointer-events", "none");
                    lbOOPC.Style.Add("color", "gray");
                    lbOOPC.Style.Add("opacity", "0.2");

                    Lnk_Share.Style.Add("pointer-events", "none");
                    Lnk_Share.Style.Add("color", "gray");
                    Lnk_Share.Style.Add("opacity", "0.2");
                }
            }
        }

        protected void lbCreate_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal2()", true);
        }

        protected void btnPopUpSave_Click(object sender, EventArgs e)
        {
            try
            {
                lobjCon.Open();
                lobjCmd = new SqlCommand("Get_All_Scenarios", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@userName ", Session["UserName"].ToString());
                lobjDA = new SqlDataAdapter(lobjCmd);
                DataTable dt = new DataTable();
                lobjDA.Fill(dt);
                var dr = dt.Select("ScenarioName = " + "'" + txtScenario.Text + "'");
                lobjCon.Close();
                if (dr.Count() > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Java", "AlreadyExist()", true);
                }
                else
                {
                    lobjCon.Open();
                    lobjCmd = new SqlCommand("Insert_Scenario", lobjCon);
                    lobjCmd.CommandType = CommandType.StoredProcedure;
                    lobjCmd.Parameters.AddWithValue("@scenarioName", txtScenario.Text);
                    lobjCmd.Parameters.AddWithValue("@description ", txtScenarioDesc.Text);
                    lobjCmd.Parameters.AddWithValue("@userName ", Session["UserName"].ToString());
                    lobjCmd.Parameters.AddWithValue("@SharedBy", "");
                    lobjCmd.Parameters.AddWithValue("@SharedDescription", "");

                    lobjCmd.Parameters.Add("@new_identity", SqlDbType.Int).Direction = ParameterDirection.Output;
                    lobjCmd.ExecuteNonQuery();
                    int id = Convert.ToInt32(lobjCmd.Parameters["@new_identity"].Value.ToString());
                    Session["sId"] = id;
                    Session["sName"] = txtScenario.Text;
                    lobjCon.Close();
                    txtScenario.Text = string.Empty;
                    txtScenarioDesc.Text = string.Empty;
                    Bind_Data();
                    PT.InsertDataIntoDB("PlanFinder", Session["SessionId"].ToString(), Session["UserName"].ToString(), "PlanFinder.aspx");
                    Response.Redirect("~/Pages/PlanFinder.aspx");
                }
            }
            catch (Exception Ex1)
            {

            }
        }

        protected void grdScenario_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            try
            {

                grdScenario.SelectedIndex = e.NewSelectedIndex;
                foreach (GridViewRow row in grdScenario.Rows)
                {
                    if (grdScenario.SelectedIndex.Equals(row.RowIndex))
                    {
                        Session["shareSid"] = null;
                        Session["shareScenarioName"] = null;
                        Session["shareScenarioDesc"] = null;
                        string lblId = ((Label)row.FindControl("lblId")).Text.ToString();
                        string LblScenarioName = ((Label)row.FindControl("LblScenarioName")).Text.ToString();
                        string LblDescription = ((Label)row.FindControl("LblDescription")).Text.ToString();
                        Session["shareSid"] = lblId;
                        Session["shareScenarioName"] = LblScenarioName;
                        Session["shareScenarioDesc"] = LblDescription;
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "openModal4()", true);

            }
            catch (Exception Ex1)
            {

            }
        }

        protected void btnPopUpShare_Click(object sender, EventArgs e)
        {
            try
            {
                lobjCon.Open();
                lobjCmd = new SqlCommand("Insert_Scenario", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@scenarioName", Session["shareScenarioName"].ToString());
                lobjCmd.Parameters.AddWithValue("@description", Session["shareScenarioDesc"].ToString());
                lobjCmd.Parameters.AddWithValue("@userName", ddlUser.SelectedItem.Text);
                lobjCmd.Parameters.AddWithValue("@SharedBy", Session["UserName"].ToString());
                lobjCmd.Parameters.AddWithValue("@SharedDescription", txtMessage.Text);

                lobjCmd.Parameters.Add("@new_identity", SqlDbType.Int).Direction = ParameterDirection.Output;
                lobjCmd.ExecuteNonQuery();
                int id = Convert.ToInt32(lobjCmd.Parameters["@new_identity"].Value.ToString());
                Session["newsId"] = id;
                lobjCon.Close();
                InsertCopyRecord();
                // To mention Scenarioname in mail
                Session["Passscenario"] = Session["shareScenarioName"].ToString();

                //SendMail();
                Bind_Data();
                //Response.Redirect("~/Pages/ScenarioList.aspx");

            }
            catch (Exception Ex1)
            {

            }
        }

        private void SendMail()
        {
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(Constants.fromAddress);
            Msg.To.Add(ddlUser.SelectedItem.Text);
            Msg.Subject = Session["Passscenario"].ToString() + " - Invitation to edit.";
            Msg.Body = "Hi,<br/><br/>Please find the shared plan : " + Session["Passscenario"].ToString() + "<br/><br/> " + txtMessage.Text + "<br/><br/>  Regards,<br/>" + Session["FirstName"].ToString() + ".";
            Msg.IsBodyHtml = true;
            Msg.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = Constants.hostAddress;
            smtp.EnableSsl = true;
            smtp.Port = Constants.portNumber;
            smtp.Credentials = new System.Net.NetworkCredential(Constants.fromAddress, Constants.passWord);
            smtp.Send(Msg);

        }

        private void InsertCopyRecord()
        {
            lobjCon.Open();
            lobjCmd = new SqlCommand("Insert_ShareCopyRecord", lobjCon);
            lobjCmd.CommandType = CommandType.StoredProcedure;
            lobjCmd.Parameters.AddWithValue("@ScenarioID", Session["shareSid"].ToString());
            lobjCmd.Parameters.AddWithValue("@NewScenarionID", Session["newsId"].ToString());
            lobjCmd.ExecuteNonQuery();
            lobjCon.Close();
        }

        private void Bind_UserList()
        {
            try
            {
                lobjCmd = new SqlCommand("Get_UserList", lobjCon);
                lobjCmd.CommandType = CommandType.StoredProcedure;
                lobjCmd.Parameters.AddWithValue("@CurrentUser", Session["UserName"].ToString());

                lobjDA = new SqlDataAdapter(lobjCmd);
                lobjDS = new DataSet();
                lobjDA.Fill(lobjDS);
                ddlUser.DataSource = lobjDS;
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserName";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, "Select User");
            }
            catch (Exception Ex1)
            {

            }
        }
    }
}