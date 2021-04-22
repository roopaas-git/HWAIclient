﻿using LoggerUtility;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace CommonUtility
{
    public class EmailServices
    {
        public string readURL = string.Empty;
        public string path = string.Empty;
        MailMessage mailMessage = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        CustomLogger customLogger = new CustomLogger();

        public void SendPasswordDetails(string emailId, string firstName, string passWord)
        {
            try
            {
                mailMessage.From = new MailAddress(Constants.fromAddress);
                mailMessage.To.Add(emailId);
                mailMessage.Subject = Constants.passWordHeader;
                mailMessage.Body = "Hi " + firstName + ",<br/><br/>Please find the Login Details below:<br/><br/>Username: " + emailId + "<br/><br/>Password: " + passWord + "<br/><br/>" + "Regards," + "<br/>" + "HealthWorksAI";
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Host = Constants.hostAddress;
                smtpClient.EnableSsl = true;
                smtpClient.Port = Constants.portNumber;
                smtpClient.Credentials = new System.Net.NetworkCredential(Constants.fromAddress, Constants.passWord);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void SendActivationMail(Guid key, string emailId, string firstName, string passWord)
        {
            try
            {
                readURL = ConfigurationManager.AppSettings["siteAddress"].ToString();
                path = readURL + "Pages/ActivatePage.aspx?ActivationCode=" + key;

                mailMessage.From = new MailAddress(Constants.fromAddress);
                mailMessage.To.Add(emailId);
                mailMessage.Subject = Constants.activationHeader;
                string supportemail = "mailto:support@healthworksai.com";

                //  string body = "Hello " + firstName + ",";
                string body = "";
                body += "<br />Welcome to HealthWorksAI! A self-service data visualization platform providing you immediate access to market analytics, powerful simulators, and up-to-date metrics for strategic decision making.";
                body += "<br /><br />A new account has been created for you upon request. Please click the link below to be connected to the platform and login using the username & temporary password found below.";
                body += "<br /><br /><a href = '" + path + "'>" + Constants.siteAddress + "</a>";
                body += "<br />User Name: " + emailId;
                body += "<br />Temporary Password: " + passWord;
                body += "<br /><br />We recommend that you change your password after signing in for the first time.";
                body += "<br /><br />If you require any assistance, please contact your dedicated Account Manager or email our team at <a href='" + supportemail + "'>support@healthworksai.com</a>.";
                body += "<br /><br />Thank you,";
                body += "<br />HealthWorksAI";

                mailMessage.AlternateViews.Add(Mail_Body(body));
                // mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Host = Constants.hostAddress;
                smtpClient.EnableSsl = true;
                smtpClient.Port = Constants.portNumber;
                smtpClient.Credentials = new System.Net.NetworkCredential(Constants.fromAddress, Constants.passWord);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        private AlternateView Mail_Body(string body)
        {
            string Imgpath = HttpContext.Current.Server.MapPath(@"~/dist/Images/healthworks_ai-logo_mail.jpg");
            LinkedResource Img = new LinkedResource(Imgpath, MediaTypeNames.Image.Jpeg);
            Img.ContentId = "MyImage";
            string str = @"  
                <table>
                    <tr>  
                        <td>  
                          <img src=cid:MyImage  id='img'/>   
                        </td>  
                    </tr>
                    <tr>  
                        <td> " + body + @"
                        </td>  
                    </tr>  
                </table>  
                ";
            AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add(Img);
            return AV;
        }

        public void SharedPlanDetails(string fromAddress, string toAddress, string userName, string scenarioName, string message)
        {
            try
            {
                mailMessage.From = new MailAddress(Constants.fromAddress);
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = userName + Constants.sharePlanHeader;
                mailMessage.Body = "Hi,<br/><br/>Please find the shared plan" + userName + "<br/><br/> " + message + "<br/><br/>  Regards," + userName + "<br/>" + ".";
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Host = Constants.hostAddress;
                smtpClient.EnableSsl = true;
                smtpClient.Port = Constants.portNumber;
                smtpClient.Credentials = new System.Net.NetworkCredential(Constants.fromAddress, Constants.passWord);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void SendTicketDetails(string userName, int ticketId, string priority, string header, string fromUser, string comment, string dashboardName)
        {
            try
            {
                mailMessage.From = new MailAddress(Constants.fromAddress);
                mailMessage.To.Add(Constants.fromAddress);
                //mailMessage.CC.Add(fromUser);
                mailMessage.Subject = header;
                mailMessage.Body = "Hi,<br/><br/>This is " + userName + ".<br/>My Ticket ID is (" + ticketId + "). <br/>" + "SiteName : " + Constants.tableauSiteName + "<br/>DashboardName : " + dashboardName + "<br/>Comment : " + comment + "<br/>Please resolve the issue.<br/><br/>  Regards,<br/>" + userName + "<br/>" + ".";
                mailMessage.IsBodyHtml = true;

                if (priority == "High")
                {
                    mailMessage.Priority = MailPriority.High;
                }
                else if (priority == "Medium")
                {
                    mailMessage.Priority = MailPriority.Normal;
                }
                else
                {
                    mailMessage.Priority = MailPriority.Low;
                }

                mailMessage.Priority = MailPriority.High;
                smtpClient.Host = Constants.hostAddress;
                smtpClient.EnableSsl = true;
                smtpClient.Port = Constants.portNumber;
                smtpClient.Credentials = new System.Net.NetworkCredential(Constants.fromAddress, Constants.passWord);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void SendTicketStatus(string userName, int ticketId, string status, string comment, string email)
        {
            try
            {
                mailMessage.From = new MailAddress(Constants.fromAddress);
                mailMessage.To.Add(email);
                mailMessage.CC.Add(Constants.teamAddress);
                mailMessage.Subject = "Ticket is " + status;
                mailMessage.Body = "Hi " + userName + ",<br/> Your Ticket (" + ticketId + ") is " + status + ".<br/><br/>" + comment + "<br/><br/>  Regards,<br/> HealthWorksAI";
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Host = Constants.hostAddress;
                smtpClient.EnableSsl = true;
                smtpClient.Port = Constants.portNumber;
                smtpClient.Credentials = new System.Net.NetworkCredential(Constants.fromAddress, Constants.passWord);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void SendRenewSubscriptiondetails(string emailId, string firstName)
        {
            try
            {
                mailMessage.From = new MailAddress(Constants.fromAddress);
                string[] multiids = emailId.Split(',');
                foreach (string ids in multiids)
                {
                    mailMessage.To.Add(new MailAddress(ids));
                }               
                mailMessage.Subject = Constants.tableauSiteName + " Subscription Renewal";
                mailMessage.Body = "Hi,<br/>" + firstName + " from "+ Constants.tableauSiteName +" has clicked on the link on the portal. Please reach out asap.<br/><br/>" + "Regards," + "<br/>" + "HealthWorksAI" + "<br/>";
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Host = Constants.hostAddress;
                smtpClient.EnableSsl = true;
                smtpClient.Port = Constants.portNumber;
                smtpClient.Credentials = new System.Net.NetworkCredential(Constants.fromAddress, Constants.passWord);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }
    }
}