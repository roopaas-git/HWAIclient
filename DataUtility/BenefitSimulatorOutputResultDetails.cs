using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtility
{
    public class BenefitSimulatorOutputResultDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
        public DataSet dataSet;

        public object DeleteBenefitSimulatorOutputResultDB(int scenarioID)
        {
            try
            {
                this.OpenConnection();
                sqlCommand = new SqlCommand("spBenefitSimulatorOutputResults_Delete", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Scenario_Id", scenarioID);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
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
