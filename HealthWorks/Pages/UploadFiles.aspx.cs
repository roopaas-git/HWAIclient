using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class UploadFiles : System.Web.UI.Page
    {
        SqlConnection lobjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "HealthWorksAI - Upload Files";
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                if (!IsPostBack)
                {
                    Active_DeactiveLinks();
                    BindGrid();
                }
                HideHelpLink();
            }
            catch (Exception)
            {

            }

        }

        private void BindGrid()
        {

            SqlCommand lobjCmd;
            lobjCmd = new SqlCommand("Select * from tbl_UploadedReports", lobjCon);
            lobjCon.Open();
            DataTable lobjs1 = new DataTable();
            SqlDataAdapter lobjDa = new SqlDataAdapter(lobjCmd);
            lobjCon.Close();
            lobjDa.Fill(lobjs1);
            if (lobjs1.Rows.Count > 0)
            {
                GV_PageTracking.DataSource = lobjs1;
                GV_PageTracking.DataBind();
            }
            else
            {
                GV_PageTracking.DataSource = null;
                GV_PageTracking.DataBind();
            }
        }

        protected void GV_PageTracking_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int row = e.RowIndex;
            Label lbl = (Label)GV_PageTracking.Rows[row].FindControl("LblId");
            Label LblFileName = (Label)GV_PageTracking.Rows[row].FindControl("LblFileName");
            LinkButton Lnk = (LinkButton)GV_PageTracking.Rows[row].FindControl("Deletebtn");
            if (Lnk != null)
            {
                string destPath = Server.MapPath("/Documents/UploadFiles");
                string deletefile = destPath + "\\" + LblFileName.Text.ToString();
                if (deletefile != null || deletefile != string.Empty)
                {
                    if ((System.IO.File.Exists(deletefile)))
                    {
                        System.IO.File.Delete(deletefile);
                    }
                }
            }
            if (lbl != null)
            {
                SqlCommand command1 = new SqlCommand("delete from tbl_UploadedReports where FIleID=@FileId", lobjCon);
                command1.CommandType = CommandType.Text;
                command1.Parameters.AddWithValue("@fileId", Convert.ToInt32(lbl.Text));
                lobjCon.Open();
                command1.ExecuteNonQuery();
                lobjCon.Close();
            }
            BindGrid();
        }

        protected void GV_PageTracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        public void HideHelpLink()
        {
            LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            lbFullScreen.Visible = false;
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("UploadFiles"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
            }
           
        }

        protected void btn_Documents_Click(object sender, EventArgs e)
        {
            if (documentUpload.HasFile)
            {
                string destPath = Server.MapPath("/Documents/UploadFiles");
                uploadfile(destPath, ddl_PdfUpload.SelectedValue);
            }
        }

        public void uploadfile(string destPath, string PageName)
        {

            string FileName = documentUpload.FileName.ToString();         

            SqlCommand lobjCmd = new SqlCommand("SELECT * FROM [tbl_UploadedReports] where PageName=@pagename", lobjCon);
            lobjCmd.Parameters.AddWithValue("@pagename", PageName);
            DataTable lobjds = new DataTable();
            SqlDataAdapter lobjDa = new SqlDataAdapter(lobjCmd);
            lobjDa.Fill(lobjds);

            if (lobjds.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "callwarning();", true);               
            }
            else
            {

                documentUpload.SaveAs(destPath + "\\" + documentUpload.FileName.Replace("'", ""));
                lobjCon.Open();
                SqlCommand command1 = new SqlCommand();
                command1.Connection = lobjCon;
                command1.CommandText = "insert into tbl_UploadedReports([PageName],[FileName],[UploadedBy])values ('" + PageName + "','" + FileName + "','" + Session["UserName"].ToString() + "')";
                command1.ExecuteNonQuery();
                lobjCon.Close();

                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "callsucess();", true);
                BindGrid();
            }
           
        }
    }
}