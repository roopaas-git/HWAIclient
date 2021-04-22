using BusinessUtility;
using CommonUtility;
using DataUtility;
using LoggerUtility;
using System;
using System.Collections.Generic;
using System.Web.Security;

using HealthWorks.Content.BO;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Data;

namespace HealthWorks
{
    public partial class Default : System.Web.UI.Page
    {
        EmailServices emailServices;
        UserDetails userDetails;
        UserMethods userMethods;
        private string userName = string.Empty;
        ICustomLogger _logger = new CustomLogger();

        ////https://bit.analytics-hub.com/#/site/Aetna/views/SalesTerms/SalesTerms?County=Henry,Clay&SNP=Non-SNP&:iid=10
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    txtUserID.Text = Request.Cookies["UserName"].Value;
                    txtPassWord.Attributes["value"] = Request.Cookies["Password"].Value;
                    chkRemember.Checked = true;
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessage.Visible = false;
                userDetails = new UserDetails();
                Session["LastLogin"] = userDetails.GetLastLoginTime(txtUserID.Text.Trim());
                Hdn_username.Value = txtUserID.Text.Trim();
                DataSet ds = userDetails.CheckUserAlreadyLoggedIn(txtUserID.Text.Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["IsLoggedIn"].ToString() == "1")
                    {
                        BtnLogOut.Visible = true;
                        LblLogoutMsg.Visible = true;
                        btnLogin.Visible = false;
                    }
                    else
                    {
                        BtnLogOut.Visible = false;
                        LblLogoutMsg.Visible = false;
                        btnLogin.Visible = true;
                        if (Membership.GetUser(txtUserID.Text) != null)
                        {
                            if (Membership.GetUser(txtUserID.Text).IsLockedOut == true)
                            {
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = Constants.acccountLocked;
                            }
                            else if (Membership.ValidateUser(txtUserID.Text, txtPassWord.Text))
                            {
                                userDetails.check_User_Password(txtUserID.Text.Trim(), txtPassWord.Text.Trim());
                                string isActiveUser = userDetails.Check_User_Activation(txtUserID.Text.ToString());

                                if (isActiveUser == Constants.isTrue)
                                {
                                    Session["UserName"] = txtUserID.Text.ToString();
                                    Session["sessionID"] = Guid.NewGuid();
                                    Save_credentials();
                                    string ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                                    if (string.IsNullOrEmpty(ipAdd))
                                    {
                                        ipAdd = Request.ServerVariables["REMOTE_ADDR"];
                                    }

                                    userDetails.LoginSession(Session["sessionID"].ToString(), txtUserID.Text.ToString());
                                    userDetails.InsertIPDetails(ipAdd, Session["SessionId"].ToString(), Session["UserName"].ToString());

                                    Response.Redirect("~/Pages/Home.aspx");
                                }
                                else
                                {
                                    lblErrorMessage.Visible = true;
                                    lblErrorMessage.Text = Constants.accountNotActivated;
                                }
                            }
                            else
                            {
                                if (txtUserID.Text != "")
                                {
                                    lblErrorMessage.Visible = true;
                                    lblErrorMessage.Text = Constants.credentialsNotValid;
                                }
                                else
                                {
                                    lblErrorMessage.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = Constants.accountNotExist;
                        }

                    }
                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = Constants.accountNotExist;
                }


            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            userDetails = new UserDetails();
            userDetails.LogoutSession(Hdn_username.Value);
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
        private void SaveIPDetails()
        {
            //PageTracking PT = new PageTracking();
            //string ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //if (string.IsNullOrEmpty(ipAdd))
            //{
            //    ipAdd = Request.ServerVariables["REMOTE_ADDR"];
            //}
            //string ipAdd1 = HttpContext.Current.Request.UserHostAddress.ToString();
            //string ipAdd2=string.Empty;
            //var host = Dns.GetHostEntry(Dns.GetHostName());
            //ipAdd2 = host.AddressList[1].ToString();

            // PT.InsertIPDetails(ipAdd, Session["SessionId"].ToString(), Session["UserName"].ToString());
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                emailServices = new EmailServices();
                userMethods = new UserMethods();
                UserProperties userProperties = userMethods.GetDetailsByEmail(txtEmail.Text.Trim());
                emailServices.SendPasswordDetails(userProperties.UserName, userProperties.FirstName, userProperties.PassWord);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
        private void Save_credentials()
        {
            try
            {
                if (chkRemember.Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                }
                Response.Cookies["UserName"].Value = txtUserID.Text.Trim();
                Response.Cookies["Password"].Value = txtPassWord.Text.Trim();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
        protected void txtUserID_TextChanged(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }
        protected void txtPassWord_TextChanged(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }
        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                UserDetails userDetails = new UserDetails();
                userDetails.InsertLoginDownloadUserDetails(txtEmailDownload.Text);

                Response.ContentType = "Application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=Right to Win Analysis Overview.pdf");
                Response.TransmitFile(Server.MapPath("~/Documents/Right to Win Analysis Overview.pdf"));
                Response.End();

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

    }
}