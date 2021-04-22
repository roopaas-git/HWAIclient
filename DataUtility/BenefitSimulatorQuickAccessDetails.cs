using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtility
{
    public class BenefitSimulatorQuickAccessDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;     
        public DataSet dataSet;

        public DataSet GetBenefitSimulatorQuickAccessDB(int ScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorQuickAccess_GetQuickAccess", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Scenario_ID", ScenarioID);                
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter1 = sqlDataAdapter;
                sqlDataAdapter1.Fill(dataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseConnection();

            }
            return dataSet;
        }

        public DataSet GetBenefitSimulatorQuickAccessDownloadDB(int ScenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorQuickAccess_GetDownload", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Scenario_ID", ScenarioID);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter1 = sqlDataAdapter;
                sqlDataAdapter1.Fill(dataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.CloseConnection();

            }
            return dataSet;
        }
    }
}
