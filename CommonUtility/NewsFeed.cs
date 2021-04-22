using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonUtility
{
    public class NewsFeed
    {
        public DataTable FetchNewsFromRSS()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://news.google.com/news/rss/search/section/q/medicare%20advantage/medicare%20advantage?hl=en&gl=US&ned=us");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            XmlTextReader Reader1 = new XmlTextReader(response.GetResponseStream());
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(Reader1);
            DataTable dataTable = new DataTable();

            if (dataSet.Tables.Contains("item"))
            {
                if (dataSet.Tables["item"].Rows.Count >= 4)
                {
                    dataTable = SelectTopDataRow(dataSet.Tables["item"], 4);
                }
                else
                {
                    dataTable = dataSet.Tables["item"];
                }
            }
            else
            {
            }
            return dataTable;
        }

        public DataTable SelectTopDataRow(DataTable dt, int count)
        {
            DataTable dtn = dt.Clone();

            for (int i = 0; i < count; i++)
            {
                dtn.ImportRow(dt.Rows[i]);
            }
            return dtn;
        }
    }
}
