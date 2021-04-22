using CommonUtility;
using System;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class LogoUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "HealthWorksAI - Logo";
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                if (!IsPostBack)
                {
                    Active_DeactiveLinks();
                }
                HideHelpLink();
            }
            catch (Exception)
            {

            }

        }

        public void HideHelpLink()
        {
            LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            lbFullScreen.Visible = false;
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ManageLogo"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
            }

        }

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/dist/Images/") + "Default.png");
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void btn_Documents_Click(object sender, EventArgs e)
        {
            if (documentUpload.HasFile)
            {
                string savedocuments = string.Empty;
                if (Constants.AccountType != Constants.platinum)
                {
                    savedocuments = Constants.goldDocumentsFolderUpload + Server.HtmlEncode(ddl_PdfUpload.SelectedValue);
                    documentUpload.SaveAs(savedocuments);
                }
                else
                {
                    savedocuments = Constants.platinumDocumentsFolderUpload + Server.HtmlEncode(ddl_PdfUpload.SelectedValue);
                    documentUpload.SaveAs(savedocuments);
                }
            }
        }
    }
}