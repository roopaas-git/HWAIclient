using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerUtility
{
    public class CustomLogger : ICustomLogger
    {
        private static ILogger _logger;

        public CustomLogger()
        {
            IConfigurationBuilder builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            string sampleFile = ConfigurationManager.AppSettings["fileLocation"].ToString();
            string outputFormatt = ConfigurationManager.AppSettings["OutputTemplate"].ToString();

            _logger = new LoggerConfiguration()
             .Enrich.FromLogContext()
             .WriteTo.File(sampleFile)
             .MinimumLevel.Debug()
             .CreateLogger();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Information(string message)
        {
            _logger.Information(message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }
    }
}
