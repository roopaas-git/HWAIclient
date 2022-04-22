using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUtility;

namespace BusinessUtility
{
    public class EnrollmentScenarioDetailMethods
    {
        EnrollmentScenarioDetails enrollmentScenarioDetails = new EnrollmentScenarioDetails();
        private DataTable dataTable;
        int result;
        public DataTable LoadEnrollmentScenarios(string userName, int ClientId)
        {
            dataTable = enrollmentScenarioDetails.LoadEnrollmentScenariosDB(userName, ClientId);
            return dataTable;
        }

        public object DeleteEnrollmentScenario(int sid)
        {
            return enrollmentScenarioDetails.DeleteEnrollmentScenarioDB(sid);
        }

        public object RemoveAlreadySharedEnrollmentScenario(string SharedUser, string ScenarioName)
        {
            return enrollmentScenarioDetails.RemoveAlreadySharedEnrollmentScenarioDB(SharedUser, ScenarioName);
        }

        public int UpdateEnrollmentScenario(int sid, string scenarioName, string description, string createdBy, int alert, int ClientId)
        {
            scenarioName = scenarioName.Trim();
            DataTable check = new DataTable();
            check = LoadEnrollmentScenarios(createdBy, ClientId);
            DataRow[] found = check.Select("ScenarioName = '" + scenarioName + "'");
            if (scenarioName != "")
            {
                if (alert == 1)
                {
                    if (found.Length == 0)
                    {
                        result = enrollmentScenarioDetails.UpdateEnrollmentScenarioDB(sid, scenarioName, description, createdBy);
                    }
                    else
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = enrollmentScenarioDetails.UpdateEnrollmentScenarioDB(sid, scenarioName, description, createdBy);
                }
            }
            return result;
        }

        public int InsertEnrollmentScenario(string scenarioName, string description, string createdBy, string SharedBy, string SharedDesc,string UserName,int status,int shareflag, int ClientId)
        {
            scenarioName = scenarioName.Trim();
            DataTable check = new DataTable();
            check = LoadEnrollmentScenarios(createdBy, ClientId);
            DataRow[] found = check.Select("ScenarioName = '" + scenarioName + "'");
            if (found.Length == 0 && shareflag==0)
            {
                result = enrollmentScenarioDetails.InsertEnrollmentScenarioDB(scenarioName, description, createdBy, SharedBy, SharedDesc,UserName,status, ClientId);
            }
            if (shareflag == 1)
            {
                result = enrollmentScenarioDetails.InsertEnrollmentScenarioDB(scenarioName, description, createdBy, SharedBy, SharedDesc, UserName, status, ClientId);
            }
            return result;
        }

        public object ShareEnrollmentScenario(int OldScenarioID, int NewScenarioID)
        {
            return enrollmentScenarioDetails.ShareEnrollmentScenarioDB(OldScenarioID, NewScenarioID);
        }

        public DataTable BindEnrollmentUserName(string UserName)
        {
            dataTable = enrollmentScenarioDetails.BindUserNameEnrollmentDB(UserName);
            return dataTable;
        }
        public DataTable BindEnrollmentUserDetails()
        {
            dataTable = enrollmentScenarioDetails.BindUserDetailsEnrollmentDB();
            return dataTable;
        }

        public bool UpdateEnrollmentScenarioProcessStatus(int sid, byte processStatus)
        {
            if (enrollmentScenarioDetails.UpdateEnrollmentScenarioProcessStatusDB(sid, processStatus))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetEnrollmentScenario(int ScenarioId)
        {
            dataTable = enrollmentScenarioDetails.GetEnrollmentScenarioDB(ScenarioId);
            return dataTable;
        }

        public DataTable GetEnrollmentSimResults(int ScenarioId)
        {
            dataTable = enrollmentScenarioDetails.GetEnrollmentSimResultsDB(ScenarioId);
            return dataTable;
        }
    }
}
