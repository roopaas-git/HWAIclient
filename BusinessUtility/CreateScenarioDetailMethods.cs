using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUtility;

namespace BusinessUtility
{
    public class CreateScenarioDetailMethods
    {
        CreateScenarioDetails createScenarioDetails = new CreateScenarioDetails();
        private DataTable dataTable;
        int result;
        public DataTable LoadScenarios(string userName)
        {
            dataTable = createScenarioDetails.LoadScenariosDB(userName);
            return dataTable;
        }

        public object DeleteScenario(int sid)
        {
            return createScenarioDetails.DeleteScenarioDB(sid);
        }

        public object RemoveAlreadySharedScenario(string SharedUser, string ScenarioName)
        {
            return createScenarioDetails.RemoveAlreadySharedScenarioDB(SharedUser, ScenarioName);
        }

        public int UpdateScenario(int sid, string scenarioName, string description, string createdBy, int alert)
        {
            scenarioName = scenarioName.Trim();
            DataTable check = new DataTable();
            check = LoadScenarios(createdBy);
            DataRow[] found = check.Select("ScenarioName = '" + scenarioName + "'");
            if (scenarioName != "")
            {
                if (alert == 1)
                {
                    if (found.Length == 0)
                    {
                        result = createScenarioDetails.UpdateScenarioDB(sid, scenarioName, description, createdBy);
                    }
                    else
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = createScenarioDetails.UpdateScenarioDB(sid, scenarioName, description, createdBy);
                }
            }
            return result;
        }

        public int InsertScenario(string scenarioName, string description, string createdBy, string SharedBy, string SharedDesc)
        {
            scenarioName = scenarioName.Trim();
            DataTable check = new DataTable();
            check = LoadScenarios(createdBy);
            DataRow[] found = check.Select("ScenarioName = '" + scenarioName + "'");
            if (found.Length == 0)
            {
                result = createScenarioDetails.InsertScenarioDB(scenarioName, description, createdBy, SharedBy, SharedDesc);
            }
            return result;
        }

        public object ShareScenario(int OldScenarioID, int NewScenarioID)
        {
            return createScenarioDetails.ShareScenarioDB(OldScenarioID, NewScenarioID);
        }

        public DataTable BindUserName(string UserName)
        {
            dataTable = createScenarioDetails.BindUserNameDB(UserName);
            return dataTable;
        }

    }
}
