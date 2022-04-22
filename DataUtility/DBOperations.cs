using LoggerUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtility
{
    public class DBOperations
    {
        protected SqlConnection sqlConnection = null;
        protected SqlConnection sqlHWConnection = null;
        protected SqlConnection sqlHWAIConnection = null;
        protected SqlConnection sqlHWAIEnrollmentSimulatorConnection = null;
        CustomLogger customLogger;

        public DBOperations() : base()
        {
            customLogger = new CustomLogger();
        }

        public void OpenConnection()
        {
            try
            {
                if (sqlConnection == null)
                {
                    sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
                    sqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                sqlConnection = null;
            }
        }

        public void OpenHWConnection()
        {
            try
            {
                if (sqlHWConnection == null)
                {
                    sqlHWConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyAspNetDB"].ToString());
                    sqlHWConnection.Open();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void CloseHWConnection()
        {
            try
            {
                if (sqlHWConnection != null)
                {
                    sqlHWConnection.Close();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                sqlHWConnection = null;
            }
        }

        public void OpenHWAIConnection()
        {
            try
            {
                if (sqlHWAIConnection == null)
                {
                    sqlHWAIConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ProviderNetworkDB"].ToString());
                    sqlHWAIConnection.Open();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void CloseHWAIConnection()
        {
            try
            {
                if (sqlHWAIConnection != null)
                {
                    sqlHWAIConnection.Close();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                sqlHWAIConnection = null;
            }
        }

        public void OpenHWAIEnrollmentSimulatorConnection()
        {
            try
            {
                if (sqlHWAIEnrollmentSimulatorConnection == null)
                {
                    sqlHWAIEnrollmentSimulatorConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EnrollmentSimulatorDB"].ToString());
                    sqlHWAIEnrollmentSimulatorConnection.Open();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
        }

        public void CloseHWAIEnrollmentSimulatorConnection()
        {
            try
            {
                if (sqlHWAIEnrollmentSimulatorConnection != null)
                {
                    sqlHWAIEnrollmentSimulatorConnection.Close();
                }
            }
            catch (Exception ex)
            {
                customLogger.Error(ex.Message);
            }
            finally
            {
                sqlHWAIEnrollmentSimulatorConnection = null;
            }
        }


    }
}
