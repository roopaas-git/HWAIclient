using CommonUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace TableauUtility
{
    public class TableauTicket
    {
        public string tableauPath = string.Empty;

        public string GetTableauTicket(string FinalTabServer, string tabuser, string siteName)
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            string postData = "username=" + tabuser + "&target_site=" + siteName;
            byte[] data = enc.GetBytes(postData);
            string resString = string.Empty;

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(FinalTabServer + "/trusted");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;

                Stream outStream = req.GetRequestStream();
                outStream.Write(data, 0, data.Length);
                outStream.Close();

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader inStream = new StreamReader(res.GetResponseStream(), enc);
                resString = inStream.ReadToEnd();
                inStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resString = "-1";
            }
            return resString;
        }

        public string GetTableauURL(string FinalTabServer, string ticket, string viewPath)
        {
            tableauPath = FinalTabServer + "/trusted/" + ticket + "/t/" + Constants.tableauSiteName + "/views/" + viewPath + "?:embed=yes&:toolbar=yes";
            return tableauPath;
        }

        public string GetTableauParamURL(string FinalTabServer, string ticket, string viewPath,string querystring)
        {
            tableauPath = FinalTabServer + "/trusted/" + ticket + "/t/" + Constants.tableauSiteName + "/views/" + viewPath + "?"+ querystring + "&:embed=yes&:toolbar=yes";
            return tableauPath;
        }
        public string GetWhitepaperTableauURL(string FinalTabServer, string ticket, string viewPath)
        {
            tableauPath = FinalTabServer + "/trusted/" + ticket + "/t/" + Constants.tableauWhitepaperSiteName + "/views/" + viewPath + "?:embed=yes&:toolbar=yes";
            return tableauPath;
        }
    }
}
