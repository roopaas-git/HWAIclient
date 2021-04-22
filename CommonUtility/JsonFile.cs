using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonUtility
{
    public class JsonFile
    {
        public Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        public Dictionary<string, string> ReadJsonFile()
        {
            string filePath = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "TEGConfiguration.json";
            var r = new StreamReader(filePath);
            var myJson = r.ReadToEnd();
            keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(myJson);

            return keyValuePairs;
        }
    }
}
