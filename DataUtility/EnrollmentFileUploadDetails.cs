using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtility
{
   public class EnrollmentFileUploadDetails : DBOperations
    {
        public SqlCommand sqlCommand;
        public SqlDataAdapter sqlDataAdapter;
        private DataTable dataTable;

        public bool InsertIntoDB(dynamic objBSUD)
        {           
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorFileUpload_Insert", sqlHWAIEnrollmentSimulatorConnection);             
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ScenarioID", objBSUD.ScenarioID);
                sqlCommand.Parameters.AddWithValue("@BidId", objBSUD.BidId);
                //sqlCommand.Parameters.AddWithValue("@MarketId", objBSUD.MarketId);
                //sqlCommand.Parameters.AddWithValue("@SubMarketId", objBSUD.SubMarketId);
                //sqlCommand.Parameters.AddWithValue("@StateId", objBSUD.StateId);
                //sqlCommand.Parameters.AddWithValue("@CountyId", objBSUD.CountyId);
                //sqlCommand.Parameters.AddWithValue("@FootprintId", objBSUD.FootprintId);
                //sqlCommand.Parameters.AddWithValue("@PlanTypeId", objBSUD.PlanTypeId);
                //sqlCommand.Parameters.AddWithValue("@PlanCategoryId", objBSUD.PlanCategoryId);

                if (string.IsNullOrEmpty(objBSUD.UploadedFilePath))
                {
                    sqlCommand.Parameters.AddWithValue("@UploadedFilePath", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@UploadedFilePath", objBSUD.UploadedFilePath);
                }
                
                //sqlCommand.Parameters.AddWithValue("@Market", objBSUD.Market);
                sqlCommand.Parameters.AddWithValue("@PlanName", objBSUD.PlanName);

                sqlCommand.Parameters.AddWithValue("@CreatedBy", objBSUD.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@CreatedDate", objBSUD.CreatedDate);
                
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();
            }
        }
    
        public DataTable GetEnrollmentFileUploadDetailsDB(int ScenarioId)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorFileUpload_GetFileUploadDetails", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Sid", ScenarioId);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                SqlDataAdapter sqlDataAdapter1 = sqlDataAdapter;
                sqlDataAdapter1.Fill(dataTable);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();
            }
            return dataTable;
        }

        public bool UpdateEnrollmentFileUploadFilePathDB(int scenarioId, string filePath)
        {
            try
            {
                this.OpenHWAIEnrollmentSimulatorConnection();
                sqlCommand = new SqlCommand("spEnrollmentSimulatorFileUpload_UpdateFilePath", sqlHWAIEnrollmentSimulatorConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@scenarioID", scenarioId);
                sqlCommand.Parameters.AddWithValue("@uploadedFilePath", filePath);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                this.CloseHWAIEnrollmentSimulatorConnection();

            }
        }
    }
}
