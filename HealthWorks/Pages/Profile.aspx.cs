using DataUtility;
using LoggerUtility;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace HealthWorks.Pages
{
    public partial class Profile : System.Web.UI.Page
    {
        ProfileDetails ProfileDetails;
        UserDetails userDetails;
        CustomLogger customLogger = new CustomLogger();
        DataSet lobjDS;
        byte[] imageData = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "HealthWorksAI - Profile";
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
                    Bind_Data();
                    Bind_Recent_activity();
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
            LinkButton lb = (LinkButton)Master.FindControl("lbProfile");
            lb.CssClass = "dropdown-item active";
        }

        public void HideHelpLink()
        {
            LinkButton lbFullScreen = (LinkButton)Master.FindControl("lbFullScreen");
            if (lbFullScreen != null)
                lbFullScreen.Visible = false;
        }
        private void Bind_Data()
        {
            Bind_User_FirstName();
            lobjDS = new DataSet();

            ProfileDetails = new ProfileDetails();
            lobjDS = ProfileDetails.Get_User_Details(Session["UserName"].ToString());

            if (lobjDS.Tables[0].Rows.Count > 0)
            {
                if (lobjDS.Tables[0].Rows[0]["DOB"] != DBNull.Value)
                    lblDOB.Text = (Convert.ToDateTime(lobjDS.Tables[0].Rows[0]["DOB"].ToString())).ToString("MMM d, yyyy");
                else
                    lblDOB.Text = "";

                if (lobjDS.Tables[0].Rows[0]["Gender"] != DBNull.Value)
                    lblGender.Text = lobjDS.Tables[0].Rows[0]["Gender"].ToString();
                else
                    lblGender.Text = "";

                if (lobjDS.Tables[0].Rows[0]["DOJ"] != DBNull.Value)
                    lblDOJ.Text = (Convert.ToDateTime(lobjDS.Tables[0].Rows[0]["DOJ"].ToString())).ToString("MMM d, yyyy");
                else
                    lblDOJ.Text = "-";

                if (lobjDS.Tables[0].Rows[0]["Email"] != DBNull.Value)
                    lblEmail.Text = lobjDS.Tables[0].Rows[0]["UserID"].ToString();
                else
                    lblEmail.Text = lobjDS.Tables[0].Rows[0]["UserID"].ToString();

                if (lobjDS.Tables[0].Rows[0]["Mobile"] != DBNull.Value)
                    lblMobile.Text = lobjDS.Tables[0].Rows[0]["Mobile"].ToString();
                else
                    lblMobile.Text = "";

                if (lobjDS.Tables[0].Rows[0]["Address"] != DBNull.Value)
                    lblAddress.Text = lobjDS.Tables[0].Rows[0]["Address"].ToString();
                else
                    lblAddress.Text = "";

                if (lobjDS.Tables[0].Rows[0]["About"] != DBNull.Value)
                    lblAbout.Text = lobjDS.Tables[0].Rows[0]["About"].ToString();
                else
                    lblAbout.Text = "";

                Session["rowID"] = lobjDS.Tables[0].Rows[0]["TabID"].ToString();

                if (lobjDS.Tables[0].Rows[0]["Profile_PIC"] != DBNull.Value)
                    imageToDisplay.ImageUrl = String.Format("data:Image/jpeg;base64,{0}", Convert.ToBase64String((byte[])lobjDS.Tables[0].Rows[0]["Profile_PIC"]));
                else
                {
                    imageToDisplay.ImageUrl = "../Content/Images/Default.png";
                }
                Bind_Data_Update(lobjDS);
                Bind_Recent_activity();
            }
            else
            {
                imageToDisplay.ImageUrl = "../Content/Images/Default.png";
                Session["rowID"] = "0";
            }


            txtFirstName_Update.Text = lblUserNameProfile.Text;
            txtEmail_Update.Text = Session["Username"].ToString();

            DateTime dtNow = DateTime.Now;
            DateTime lastLoginDate = Convert.ToDateTime(Session["LastLogin"].ToString()).ToLocalTime();
            int diffhours = Convert.ToInt32(Math.Floor((dtNow - lastLoginDate).TotalHours));
            if (diffhours == 0)
            {
                int diffMinutes = Convert.ToInt32(Math.Floor((dtNow - lastLoginDate).TotalMinutes));
                if (diffMinutes == 0)
                {
                    int diffSec = Convert.ToInt32(Math.Floor((dtNow - lastLoginDate).TotalSeconds));
                    lblLastLogin.Text = diffSec + " Seconds ago";
                }
                else
                    lblLastLogin.Text = diffMinutes + " Minutes ago";
            }
            else if (diffhours > 24)
            {
                int days = diffhours / 24;
                lblLastLogin.Text = days + " days ago";
            }
            else
                lblLastLogin.Text = diffhours + " hours ago";
        }

        private void Bind_User_FirstName()
        {
            userDetails = new UserDetails();
            Session["FirstName"] = userDetails.GetFirstName(Session["UserName"].ToString());
            lblFullName.Text = Session["FirstName"].ToString();
            lblUserNameProfile.Text = Session["UserName"].ToString();
        }

        private void Bind_Recent_activity()
        {
            ProfileDetails = new ProfileDetails();
            lobjDS = ProfileDetails.Get_User_Activity(Session["UserName"].ToString()) as DataSet;
            LST_UserRecent.DataSource = lobjDS;
            LST_UserRecent.DataBind();
        }

        protected void LST_UserRecent_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblUser_Recent_Date = ((Label)e.Item.FindControl("lblUser_Recent_Date"));
                Label lblUser_Recent_Time = ((Label)e.Item.FindControl("lblUser_Recent_Time"));

                DateTime recntDateTime = Convert.ToDateTime(lblUser_Recent_Date.Text);
                DateTime dtNow = DateTime.Now;
                int diffhours = Convert.ToInt32(Math.Floor((dtNow - recntDateTime).TotalHours));

                if (diffhours == 0)
                {
                    int diffMinutes = Convert.ToInt32(Math.Floor((dtNow - recntDateTime).TotalMinutes));
                    if (diffMinutes == 0)
                    {
                        int diffSec = Convert.ToInt32(Math.Floor((dtNow - recntDateTime).TotalSeconds));
                        lblUser_Recent_Time.Text = diffSec + " Seconds ago";
                    }
                    else
                        lblUser_Recent_Time.Text = diffMinutes + " Minutes ago";
                }
                else if (diffhours > 24)
                {
                    int days = diffhours / 24;
                    lblUser_Recent_Time.Text = days + " days ago";
                }
                else
                    lblUser_Recent_Time.Text = diffhours + " hours ago";
            }
        }

        private void Bind_Data_Update(DataSet dsUserData)
        {
            if (dsUserData.Tables[0].Rows.Count > 0)
            {
                if (dsUserData.Tables[0].Rows[0]["DOB"] != DBNull.Value)
                    txtDOB_Update.Text = (Convert.ToDateTime(dsUserData.Tables[0].Rows[0]["DOB"].ToString())).ToString("dd-MMM-yyyy");
                else
                    txtDOB_Update.Text = "";

                if (dsUserData.Tables[0].Rows[0]["Gender"] != DBNull.Value)
                    ddlGender_Update.SelectedValue = dsUserData.Tables[0].Rows[0]["Gender"].ToString();
                else
                    ddlGender_Update.SelectedValue = "Select";

                if (dsUserData.Tables[0].Rows[0]["DOJ"] != DBNull.Value)
                    txtDOJ_Update.Text = (Convert.ToDateTime(dsUserData.Tables[0].Rows[0]["DOJ"].ToString())).ToString("dd-MMM-yyyy");
                else
                    txtDOJ_Update.Text = "";


                txtMobile_Update.Text = dsUserData.Tables[0].Rows[0]["Mobile"].ToString();
                txtAddress_Update.Text = dsUserData.Tables[0].Rows[0]["Address"].ToString();
                txtAboutMe_Update.Text = dsUserData.Tables[0].Rows[0]["About"].ToString();
                txtEmail_Update.Text = dsUserData.Tables[0].Rows[0]["UserID"].ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int rowID = Convert.ToInt32(Session["rowID"]);
                UpdatePanel1.Update();
                if (ImageUpload.HasFile)
                {
                    imageData = new byte[ImageUpload.PostedFile.ContentLength];
                    HttpPostedFile UploadedImage = ImageUpload.PostedFile;
                    UploadedImage.InputStream.Read(imageData, 0, (int)ImageUpload.PostedFile.ContentLength);
                }

                if (rowID == 0)
                {
                    Insert_User_Data(rowID, imageData);
                }
                else
                {
                    if (imageData != null)
                        Update_User_Data(rowID, imageData);
                    else
                        Update_User_Data(rowID);
                }

                Bind_Data();
                Response.Redirect(Request.Url.ToString(), false);
            }
            catch(Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        private void Insert_User_Data(int rowID, byte[] imageData)
        {
            ProfileDetails = new ProfileDetails();
            ProfileDetails.Insert_User_Details(Session["UserName"].ToString(), lblUserNameProfile.Text, txtDOB_Update.Text, ddlGender_Update.SelectedValue.ToString(), txtDOJ_Update.Text, txtEmail_Update.Text, txtMobile_Update.Text, txtAddress_Update.Text, imageData, txtAboutMe_Update.Text);
        }

        private void Update_User_Data(int rowID, byte[] imageData)
        {
            ProfileDetails = new ProfileDetails();
            ProfileDetails.Update_User_Details(Session["UserName"].ToString(), lblUserNameProfile.Text, txtDOB_Update.Text, ddlGender_Update.SelectedValue.ToString(), txtDOJ_Update.Text, txtEmail_Update.Text, txtMobile_Update.Text, txtAddress_Update.Text, imageData, txtAboutMe_Update.Text, rowID);
        }

        private void Update_User_Data(int rowID)
        {
            ProfileDetails = new ProfileDetails();
            ProfileDetails.Update_User_Details(Session["UserName"].ToString(), lblUserNameProfile.Text, txtDOB_Update.Text, ddlGender_Update.SelectedValue.ToString(), txtDOJ_Update.Text, txtEmail_Update.Text, txtMobile_Update.Text, txtAddress_Update.Text, imageData, txtAboutMe_Update.Text, rowID);
        }
    }
}