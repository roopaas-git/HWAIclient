using DataUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUtility
{
    public class UserMethods
    {
        UserProperties userProperties = new UserProperties();
        DataSet dataSet = new DataSet();
        UserDetails userDetails = new UserDetails();

        public UserProperties GetDetailsByEmail(string email)
        {
            dataSet = userDetails.GetUserDeatilsByEmail(email);

            foreach (DataRow item in dataSet.Tables[0].Rows)
            {
                userProperties.Id = Int32.Parse(item["ID"].ToString());
                userProperties.UserId = new Guid(item["UserId"].ToString());
                userProperties.FirstName = item["FirstName"].ToString();
                userProperties.LastName = item["LastName"].ToString();
                userProperties.UserName = item["UserName"].ToString();
                userProperties.PassWord = item["PassWord"].ToString();
                userProperties.Mobile = item["Mobile"].ToString();
                userProperties.IsActive = bool.Parse(item["IsActive"].ToString());
                userProperties.UserType = item["UserType"].ToString();
            }
            return userProperties;
        }
    }
}
