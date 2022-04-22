using BusinessUtility;
using CommonUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HealthWorks.Pages
{
    public partial class ProviderNetworkData : System.Web.UI.Page
    {
        ProviderNetworkAnalysisMethods ProviderNetworkAnalysisMethods = new ProviderNetworkAnalysisMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Active_DeactiveLinks();
            }
            if (!IsPostBack)
            {
                BindFilters();
                BindGrid();
            }
        }
        private void BindCount(int RequestID, string lbNetwork_id)
        {
            LblBaseTotal.Text = ddlPlanA.SelectedValue.ToString() + " Total";
            LblCompareTotal.Text= ddlPlanB.SelectedValue.ToString() + " Total";
            LblBaseUnique.Text= ddlPlanA.SelectedValue.ToString() + " Unique";
            LblCompareUnique.Text = ddlPlanB.SelectedValue.ToString() + " Unique";

            DataTable DT = ProviderNetworkAnalysisMethods.GetResults(RequestID);
            string[] network = lbNetwork_id.Split(',');
            string networkid1 = network[0];
            string networkid2 = network[1];

            DataRow[] PCPA = DT.Select("NetworkID='" + networkid1 + "' and TypeFlag='PCP'");
            DataRow[] PCPB = DT.Select("NetworkID='" + networkid2 + "' and TypeFlag='PCP'");
            DataRow[] PCPC = DT.Select("NetworkID='Common' and TypeFlag='PCP'");

            DataRow[] SpecialistA = DT.Select("NetworkID='" + networkid1 + "' and TypeFlag='Specialist'");
            DataRow[] SpecialistB = DT.Select("NetworkID='" + networkid2 + "' and TypeFlag='Specialist'");
            DataRow[] SpecialistC = DT.Select("NetworkID='Common' and TypeFlag='Specialist'");

            DataRow[] HospitalA = DT.Select("NetworkID='" + networkid1 + "' and TypeFlag='Hospital'");
            DataRow[] HospitalB = DT.Select("NetworkID='" + networkid2 + "' and TypeFlag='Hospital'");
            DataRow[] HospitalC = DT.Select("NetworkID='Common' and TypeFlag='Hospital'");

            int num_PCPA = 0;
            int num_PCPB = 0;
            int num_PCPC = 0;

            int num_SpecialistA = 0;
            int num_SpecialistB = 0;
            int num_SpecialistC = 0;


            int num_HospitalA = 0;
            int num_HospitalB = 0;
            int num_HospitalC = 0;


            if (PCPA.Count() != 0)
            {
                num_PCPA = Convert.ToInt32(PCPA[0]["Count"].ToString());
            }
            if (PCPB.Count() != 0)
            {
                num_PCPB = Convert.ToInt32(PCPB[0]["Count"].ToString());
            }
            if (PCPC.Count() != 0)
            {
                num_PCPC = Convert.ToInt32(PCPC[0]["Count"].ToString());
            }

            lbPCPA.Text = String.Format("{0:N0}", num_PCPA);
            lbPCPB.Text = String.Format("{0:N0}", num_PCPB);
            lbPCPC.Text = String.Format("{0:N0}", num_PCPC);
            lbPCPATotal.Text = String.Format("{0:N0}", num_PCPA + num_PCPC);
            lbPCPBTotal.Text = String.Format("{0:N0}", num_PCPB + num_PCPC);

            if (SpecialistA.Count() != 0)
            {
                num_SpecialistA = Convert.ToInt32(SpecialistA[0]["Count"].ToString());
            }
            if (SpecialistB.Count() != 0)
            {
                num_SpecialistB = Convert.ToInt32(SpecialistB[0]["Count"].ToString());
            }
            if (SpecialistC.Count() != 0)
            {
                num_SpecialistC = Convert.ToInt32(SpecialistC[0]["Count"].ToString());
            }

            lbSpecialistA.Text = String.Format("{0:N0}", num_SpecialistA);
            lbSpecialistB.Text = String.Format("{0:N0}", num_SpecialistB);
            lbSpecialistC.Text = String.Format("{0:N0}", num_SpecialistC);
            lbSpecialistATotal.Text = String.Format("{0:N0}", num_SpecialistA + num_SpecialistC);
            lbSpecialistBTotal.Text = String.Format("{0:N0}", num_SpecialistB + num_SpecialistC);

            if (HospitalA.Count() != 0)
            {
                num_HospitalA = Convert.ToInt32(HospitalA[0]["Count"].ToString());
            }
            if (HospitalB.Count() != 0)
            {
                num_HospitalB = Convert.ToInt32(HospitalB[0]["Count"].ToString());
            }
            if (HospitalC.Count() != 0)
            {
                num_HospitalC = Convert.ToInt32(HospitalC[0]["Count"].ToString());
            }
            lbHospitalA.Text = String.Format("{0:N0}", num_HospitalA);
            lbHospitalB.Text = String.Format("{0:N0}", num_HospitalB);
            lbHospitalC.Text = String.Format("{0:N0}", num_HospitalC);
            lbHospitalATotal.Text = String.Format("{0:N0}", num_HospitalA + num_HospitalC);
            lbHospitalBTotal.Text = String.Format("{0:N0}", num_HospitalB + num_HospitalC);
        }
        private void BindCountZero()
        {
            LblBaseTotal.Text = ddlPlanA.SelectedValue.ToString() + " Total";
            LblCompareTotal.Text = ddlPlanB.SelectedValue.ToString() + " Total";
            LblBaseUnique.Text = ddlPlanA.SelectedValue.ToString() + " Unique";
            LblCompareUnique.Text = ddlPlanB.SelectedValue.ToString() + " Unique";

            lbPCPA.Text = "0";
            lbPCPB.Text = "0";
            lbPCPC.Text = "0";
            lbSpecialistA.Text = "0";
            lbSpecialistB.Text = "0";
            lbSpecialistC.Text = "0";
            lbHospitalA.Text = "0";
            lbHospitalB.Text = "0";
            lbHospitalC.Text = "0";
            lbPCPATotal.Text = "0";
            lbPCPBTotal.Text = "0";
            lbSpecialistATotal.Text = "0";
            lbSpecialistBTotal.Text = "0";
            lbHospitalATotal.Text = "0";
            lbHospitalBTotal.Text = "0";
        }
        private void BindGrid()
        {
            DataTable dataSet = ProviderNetworkAnalysisMethods.GetRequests(Session["UserName"].ToString());
            grdData.DataSource = dataSet;
            grdData.DataBind();

            grdData.SelectedIndex = 0;
            grdData_SelectedIndexChanged(grdData, new EventArgs());
            //GridViewRow firstRow = grdData.Rows[0];
            //Label lbBidId = (firstRow.FindControl("lbRequestid") as Label);
            //if (lbBidId != null)
            //{
            //    BindCount(Convert.ToInt32(lbBidId.Text));
            //}
        }
        protected void grdData_RowCommand1(object sender, GridViewCommandEventArgs e)
        {          

            if (e.CommandName == "Download")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = gvr.RowIndex;
                Label lbBid_id = (grdData.Rows[RowIndex].FindControl("lbBid_id") as Label);

                string x = "~/Documents/APIResponse/" + e.CommandArgument.ToString() + ".csv";
                string filename = lbBid_id.Text.Replace(",","_compare_")+".csv";
                string FName = Server.MapPath(x);
                Response.Clear();
                Response.ContentType = "application/*.*";
                Response.AppendHeader("Content-Disposition", "attachment; filename=\" " + filename + "\"");
                Response.TransmitFile(FName);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }
        }
        protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Label lbRequestid = (grdData.SelectedRow.FindControl("lbRequestid") as Label);
                Label lbNetwork_id = (grdData.SelectedRow.FindControl("lbNetwork_id") as Label);
                if (lbRequestid != null)
                {
                    BindCount(Convert.ToInt32(lbRequestid.Text), lbNetwork_id.Text);
                }

                foreach (GridViewRow row in grdData.Rows)
                {
                    if (row.RowIndex == grdData.SelectedIndex)
                    {
                        row.CssClass = "active";

                    }
                    else
                    {
                        row.CssClass = "inactive";

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void BindFilters()
        {
            DataTable dataTable = new DataTable();
            dataTable = ProviderNetworkAnalysisMethods.GetMaxRequests(Session["UserName"].ToString());
            if (dataTable.Rows.Count > 0)
            {
                Session["PNAMaxRequest"] = dataTable;
            }
            else
            {
                Session["PNAMaxRequest"] = null;
            }
            BindMarket();
        }
        private void BindMarket()
        {
            try
            {
                ddlMarket.DataSource = ProviderNetworkAnalysisMethods.GetMarket();
                ddlMarket.DataTextField = "Market";
                ddlMarket.DataValueField = "Market";
                ddlMarket.DataBind();
                if (Session["PNAMaxRequest"] != null)
                {
                    DataTable DT = new DataTable();
                    DT = (DataTable)Session["PNAMaxRequest"];
                    string SelectedMarket = DT.Rows[0]["Market"].ToString();
                    ddlMarket.SelectedValue = SelectedMarket;
                }
                else
                {
                    ddlMarket.SelectedIndex = 0;
                }
                ddlMarket_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }
        private void BindSubMarket()
        {
            ddlSubMarket.DataSource = ProviderNetworkAnalysisMethods.GetSubMarket(ddlMarket.SelectedValue);
            ddlSubMarket.DataTextField = "SubMarket";
            ddlSubMarket.DataValueField = "SubMarket";
            ddlSubMarket.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedSubmarket = DT.Rows[0]["SubMarket"].ToString();
                ddlSubMarket.SelectedValue = SelectedSubmarket;
            }
            else
            {
                ddlSubMarket.SelectedIndex = 0;
            }
            ddlSubMarket_SelectedIndexChanged(this, null);
        }
        private void BindState()
        {
            ddlState.DataSource = ProviderNetworkAnalysisMethods.GetState(ddlMarket.SelectedValue, ddlSubMarket.SelectedValue);
            ddlState.DataTextField = "State";
            ddlState.DataValueField = "Statecode";
            ddlState.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedState = DT.Rows[0]["State"].ToString();
                ddlState.SelectedValue = SelectedState;
            }
            else
            {
                ddlState.SelectedIndex = 0;
            }
            ddlState_SelectedIndexChanged(this, null);
        }
        private void BindCounty()
        {
            ddlCounty.DataSource = ProviderNetworkAnalysisMethods.GetCounty(ddlMarket.SelectedValue, ddlSubMarket.SelectedValue, ddlState.SelectedValue);
            ddlCounty.DataTextField = "County";
            ddlCounty.DataValueField = "County";
            ddlCounty.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedCounty = DT.Rows[0]["County"].ToString();
                ddlCounty.SelectedValue = SelectedCounty;
            }
            else
            {
                ddlCounty.SelectedIndex = 0;
            }
            ddlCounty_SelectedIndexChanged(this, null);
        }
        private void BindFootPrint()
        {
            ddlFootprint.DataSource = ProviderNetworkAnalysisMethods.GetFootprint(ddlMarket.SelectedValue, ddlSubMarket.SelectedValue, ddlState.SelectedValue, ddlCounty.SelectedValue);
            ddlFootprint.DataTextField = "Footprint";
            ddlFootprint.DataValueField = "Footprint";
            ddlFootprint.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedFootprint = DT.Rows[0]["Footprint"].ToString();
                ddlFootprint.SelectedValue = SelectedFootprint;
            }
            else
            {
                ddlFootprint.SelectedIndex = 0;
            }
            ddlFootprint_SelectedIndexChanged(this, null);
        }
        private void BindPlanCategory()
        {
            ddlPlanCategory.DataSource = ProviderNetworkAnalysisMethods.GetPlanCategory(ddlMarket.SelectedValue, ddlSubMarket.SelectedValue, ddlState.SelectedValue, ddlCounty.SelectedValue, ddlFootprint.SelectedValue);
            ddlPlanCategory.DataTextField = "PlanCategory";
            ddlPlanCategory.DataValueField = "PlanCategory";
            ddlPlanCategory.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedPlanType = DT.Rows[0]["PlanCategory"].ToString();
                ddlPlanCategory.SelectedValue = SelectedPlanType;
            }
            else
            {
                ddlPlanCategory.SelectedIndex = 0;
            }
            ddlPlanCategory_SelectedIndexChanged(this, null);

        }
        private void BindPlanType()
        {
            ddlPlanType.DataSource = ProviderNetworkAnalysisMethods.GetPlanType(ddlMarket.SelectedValue, ddlSubMarket.SelectedValue, ddlState.SelectedValue, ddlCounty.SelectedValue, ddlFootprint.SelectedValue, ddlPlanCategory.SelectedValue);
            ddlPlanType.DataTextField = "PlanType";
            ddlPlanType.DataValueField = "PlanType";
            ddlPlanType.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedPlanType = DT.Rows[0]["PlanType"].ToString();
                ddlPlanType.SelectedValue = SelectedPlanType;
            }
            else
            {
                ddlPlanType.SelectedIndex = 0;
            }
            ddlPlanType_SelectedIndexChanged(this, null);
        }
        private void BindPlanNames()
        {

            DataTable NetworkDT = ProviderNetworkAnalysisMethods.GetPlanNames(ddlMarket.SelectedValue, ddlSubMarket.SelectedValue, ddlState.SelectedValue, ddlCounty.SelectedValue, ddlFootprint.SelectedValue, ddlPlanCategory.SelectedValue, ddlPlanType.SelectedValue);

            ddlPlanA.DataSource = NetworkDT;
            ddlPlanA.DataTextField = "PlanName";
            ddlPlanA.DataValueField = "PlanID";
            ddlPlanA.DataBind();

            ddlPlanB.DataSource = NetworkDT;
            ddlPlanB.DataTextField = "PlanName";
            ddlPlanB.DataValueField = "PlanID";
            ddlPlanB.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedBid_Ids = DT.Rows[0]["Bid_Id"].ToString();
                string[] bid_ids = SelectedBid_Ids.Split(',');
                ddlPlanA.SelectedValue = bid_ids[0].ToString();
                ddlPlanB.SelectedValue = bid_ids[1].ToString();
            }
            else
            {
                ddlPlanA.SelectedIndex = 0;
                ddlPlanB.SelectedIndex = 0;
            }

            ddlPlanA_SelectedIndexChanged(this, null);
            ddlPlanB_SelectedIndexChanged(this, null);
        }
        private void BindNetworkA()
        {
            DataTable NetworkDT = ProviderNetworkAnalysisMethods.GetNetwork(ddlMarket.SelectedValue, ddlSubMarket.SelectedValue, ddlState.SelectedValue, ddlCounty.SelectedValue, ddlFootprint.SelectedValue, ddlPlanCategory.SelectedValue, ddlPlanType.SelectedValue, ddlPlanA.SelectedValue);

            ddlNetworkA.DataSource = NetworkDT;
            ddlNetworkA.DataTextField = "Network";
            ddlNetworkA.DataValueField = "NetworkID";
            ddlNetworkA.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedNetworks = DT.Rows[0]["Network_id"].ToString();
                string[] bid_ids = SelectedNetworks.Split(',');
                ddlNetworkA.SelectedValue = bid_ids[0].ToString();
                btnSubmit.Attributes.Add("class", "disabled");
                btnSubmit.Enabled = false;
            }
            else
            {
                ddlNetworkA.SelectedIndex = 0;
                btnSubmit.Enabled = true;
                btnSubmit.Attributes.Add("class", "btn");
                BindCountZero();
            }
            btnSubmit.Style[HtmlTextWriterStyle.Cursor] = btnSubmit.Enabled ? "pointer" : "default";
        }
        private void BindNetworkB()
        {

            DataTable NetworkDT = ProviderNetworkAnalysisMethods.GetNetwork(ddlMarket.SelectedValue, ddlSubMarket.SelectedValue, ddlState.SelectedValue, ddlCounty.SelectedValue, ddlFootprint.SelectedValue, ddlPlanCategory.SelectedValue, ddlPlanType.SelectedValue, ddlPlanB.SelectedValue);

            ddlNetworkB.DataSource = NetworkDT;
            ddlNetworkB.DataTextField = "Network";
            ddlNetworkB.DataValueField = "NetworkID";
            ddlNetworkB.DataBind();
            if (Session["PNAMaxRequest"] != null)
            {
                DataTable DT = new DataTable();
                DT = (DataTable)Session["PNAMaxRequest"];
                string SelectedNetworks = DT.Rows[0]["Network_id"].ToString();
                string[] bid_ids = SelectedNetworks.Split(',');
                ddlNetworkB.SelectedValue = bid_ids[1].ToString();
                btnSubmit.Attributes.Add("class", "btn disabled");
                btnSubmit.Enabled = false;
            }
            else
            {
                ddlNetworkB.SelectedIndex = 0;
                btnSubmit.Enabled = true;
                btnSubmit.Attributes.Add("class", "btn");
                BindCountZero();

            }
            btnSubmit.Style[HtmlTextWriterStyle.Cursor] = btnSubmit.Enabled ? "pointer" : "default";
            Session["PNAMaxRequest"] = null;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveUserRequest();
            //callAPI();
        }
        private string CallPythonScript(int Id)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += "Enter into Run API";
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = Server.MapPath("~/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }

            try
            {
                string state = ddlState.SelectedValue;
                string county = ddlCounty.SelectedValue;
                string Bid_Id1 = ddlPlanA.SelectedValue;
                string Bid_id2 = ddlPlanB.SelectedValue;
                string Network1 = ddlNetworkA.SelectedValue;
                string Network2 = ddlNetworkB.SelectedValue;

                ProcessStartInfo processStartInfo = new ProcessStartInfo(Constants.pythonExeFileLocation);
                processStartInfo.Arguments = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", Constants.PythonProviderDataCSV, state, '"' + county + '"', Bid_Id1, Bid_id2, Network1, Network2, Id, Session["UserName"].ToString());
                processStartInfo.UseShellExecute = false;
                processStartInfo.RedirectStandardOutput = true;
                Process p = Process.Start(processStartInfo);
                StreamReader s = p.StandardOutput;
                p.WaitForExit();
                int exitCode = p.ExitCode;
                return p.ExitCode.ToString();
            }
            catch (Exception ex)
            {
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += "Enter into Exception in API";
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.WriteLine(ex.Message);
                    writer.Close();
                }
                return "1";
            }
        }
        private void SaveUserRequest()
        {
            string SelectedNetworkIDs = ddlNetworkA.SelectedValue + "," + ddlNetworkB.SelectedValue;
            string SelectedPlanIDs = ddlPlanA.SelectedValue + "," + ddlPlanB.SelectedValue;
            string UserName = Session["UserName"].ToString();
            int Row = ProviderNetworkAnalysisMethods.SaveUserRequest(UserName, ddlMarket.SelectedValue, ddlSubMarket.SelectedValue, ddlState.SelectedValue, ddlCounty.SelectedValue, ddlFootprint.SelectedValue, ddlPlanCategory.SelectedValue, ddlPlanType.SelectedValue, SelectedPlanIDs, SelectedNetworkIDs);

            string Call = CallPythonScript(Row);
            ModalProgress.Hide();
            Response.Redirect("~/Pages/ProviderNetworkData.aspx", false);

            // BindGrid();
            //GridViewRow firstRow = grdData.Rows[0];
            //Label lbBidId = (firstRow.FindControl("lbRequestid") as Label);
            //if (lbBidId != null)
            //{
            //    BindCount(Convert.ToInt32(lbBidId.Text), SelectedNetworkIDs);
            //}

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your request has been submitted, you will recieve a notification email when data is ready to download.');", true);

        }
        //private void callAPI()
        //{
        //    var url = "https://api.vericred.com/providers/search";

        //    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        //    httpRequest.Method = "POST";

        //    httpRequest.Headers["Vericred-Api-Key"] = "88a8daf5130a2ee9c20965cb49293b98";
        //    httpRequest.ContentType = "application/json";

        //    string networkid = "[" + ddlNetworkA.SelectedValue + "," + ddlNetworkB.SelectedValue + "]";
        //    string pageno = "1";
        //    //   string radius = "100";
        //    //  string zipcode = "38017";
        //    string perpage = "200";

        //    string data = @"{""network_ids"":" + networkid + " , \"page\":" + pageno + ", \"per_page\":" + perpage + "}";
        //    //string data = @"{""network_ids"":" + networkid + " , \"page\":" + pageno + ", \"per_page\":" + perpage + " , \"radius\":" + radius + " , \"zip_code\":" + zipcode + "}";
        //    //var data = @"{""network_ids"": [203549,200229], ""per_page"": 200,  ""radius"": 100, ""zip_code"": ""38017""}";          

        //    using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        //    {
        //        streamWriter.Write(data);
        //    }

        //    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {
        //        if (httpResponse.StatusCode.ToString() == "OK")
        //        {
        //            var result = streamReader.ReadToEnd();
        //            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
        //            List<Provider> lstproviders = myDeserializedClass.providers.ToList();

        //            int pagecount = myDeserializedClass.meta.total / 200;

        //            for (int i = 1; i < pagecount + 1; i++)
        //            {
        //                Root PageResult = callAPIforPage(i + 1);
        //                List<Provider> lstproviders1 = new List<Provider>();
        //                if (PageResult.providers != null)
        //                {
        //                    lstproviders1 = PageResult.providers.ToList();
        //                    lstproviders.AddRange(lstproviders1);
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //            converttocsvList(lstproviders);
        //        }
        //    }

        //}
        //private void converttocsvList(List<Provider> lstproviders)
        //{
        //    List<CsvProvider> CsvProvider = new List<CsvProvider>();

        //    for (int i = 0; i < lstproviders.Count; i++)
        //    {

        //        for (int j = 0; j < lstproviders[i].network_ids.Count; j++)
        //        {
        //            for (int k = 0; k < lstproviders[i].npis.Count; k++)
        //            {
        //                CsvProvider csv = new CsvProvider();
        //                csv.city = lstproviders[i].city;
        //                csv.email = lstproviders[i].email;
        //                csv.gender = lstproviders[i].gender;
        //                if (lstproviders[i].first_name != null)
        //                    csv.first_name = lstproviders[i].first_name.Replace(',', ';');
        //                else
        //                    csv.first_name = lstproviders[i].first_name;
        //                csv.id = lstproviders[i].id;

        //                if (lstproviders[i].last_name != null)
        //                    csv.last_name = lstproviders[i].last_name.Replace(',', ';');
        //                else
        //                    csv.last_name = lstproviders[i].last_name;
        //                csv.latitude = lstproviders[i].latitude;
        //                csv.longitude = lstproviders[i].longitude;
        //                if (lstproviders[i].middle_name != null)
        //                    csv.middle_name = lstproviders[i].middle_name.Replace(',', ';');
        //                else
        //                    csv.middle_name = lstproviders[i].middle_name;
        //                if (lstproviders[i].organization_name != null)
        //                    csv.organization_name = lstproviders[i].organization_name.Replace(',', ';');
        //                else
        //                    csv.organization_name = lstproviders[i].organization_name;
        //                csv.phone = lstproviders[i].phone;
        //                if (lstproviders[i].presentation_name != null)
        //                    csv.presentation_name = lstproviders[i].presentation_name.Replace(',', ';');
        //                else
        //                    csv.presentation_name = lstproviders[i].presentation_name;
        //                if (lstproviders[i].specialty != null)
        //                    csv.specialty = lstproviders[i].specialty.Replace(',', ';');
        //                else
        //                    csv.specialty = lstproviders[i].specialty;
        //                csv.state = lstproviders[i].state;
        //                csv.state_id = lstproviders[i].state_id;
        //                if (lstproviders[i].street_line_1 != null)
        //                    csv.street_line_1 = lstproviders[i].street_line_1.Replace(',', ';');
        //                else
        //                    csv.street_line_1 = lstproviders[i].street_line_1;

        //                if (lstproviders[i].street_line_2 != null)
        //                    csv.street_line_2 = lstproviders[i].street_line_2.Replace(',', ';');
        //                else
        //                    csv.street_line_2 = lstproviders[i].street_line_2;
        //                csv.suffix = lstproviders[i].suffix;
        //                csv.title = lstproviders[i].title;
        //                csv.type = lstproviders[i].type;
        //                csv.zip_code = lstproviders[i].zip_code;
        //                csv.network_ids = lstproviders[i].network_ids[j];
        //                csv.npis = lstproviders[i].npis[k];
        //                CsvProvider.Add(csv);
        //            }
        //        }
        //    }

        //    int networkid1 = 203549;
        //    int networkid2 = 200229;


        //    var N1 = CsvProvider.Where(m => m.network_ids.Equals(networkid1)).Select(m => m.id).Distinct().ToList();
        //    var N2 = CsvProvider.Where(m => m.network_ids.Equals(networkid2)).Select(m => m.id).Distinct().ToList();

        //    List<Ids> test = new List<Ids>();
        //    foreach (var item in N1)
        //    {
        //        Ids t1 = new Ids();
        //        t1.id = item;
        //        test.Add(t1);
        //    }

        //    List<Ids> test1 = new List<Ids>();
        //    foreach (var item in N2)
        //    {
        //        Ids t2 = new Ids();
        //        t2.id = item;
        //        test1.Add(t2);
        //    }

        //    var query = from g in test
        //                join h in test1 on g.id equals h.id
        //                select new { g.id };

        //    //  Network1.Text = "Unique Count for 203549 :" + N1.Distinct().Count().ToString();
        //    //   Network2.Text = "Unique Count for 200229 :" + N2.Distinct().Count().ToString();
        //    //   Network3.Text = "Common Count :" + query.Distinct().Count().ToString();

        //    SaveToCsv(CsvProvider, @"E:\MyFolder\path1.csv");
        //}
        private void ConvertStringToList(RadComboBox MultiDDL, string SavedString)
        {
            string[] IDs = SavedString.ToString().Split(',');
            var getIDs = IDs;
            foreach (var item in getIDs)
            {
                RadComboBoxItem items = MultiDDL.FindItemByValue(item.ToString());
                if (items != null)
                {
                    items.Checked = true;
                }
            }
        }
        private void SaveToCsv<T>(List<T> reportData, string path)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            lines.Add(header);
            var valueLines = reportData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            File.WriteAllLines(path, lines.ToArray());
        }
        //private Root callAPIforPage(int page)
        //{
        //    var url = "https://api.vericred.com/providers/search";

        //    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        //    httpRequest.Method = "POST";

        //    httpRequest.Headers["Vericred-Api-Key"] = "88a8daf5130a2ee9c20965cb49293b98";
        //    httpRequest.ContentType = "application/json";

        //    string networkid = "[" + ddlNetworkA.SelectedValue + "," + ddlNetworkB.SelectedValue + "]";
        //    string pageno = page.ToString();
        //    string radius = "100";
        //    string zipcode = "38017";
        //    string perpage = "200";
        //    Root myDeserializedClass = new Root();
        //    string data = @"{""network_ids"":" + networkid + " , \"page\":" + pageno + ", \"per_page\":" + perpage + " }";
        //    //"{""network_ids"": ,""page"": """ + page + """,""per_page"": 200,""radius"": 100, ""zip_code"": ""38017""}";
        //    try
        //    {
        //        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        //        {
        //            streamWriter.Write(data);
        //        }

        //        using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        //        {
        //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //            {
        //                if (httpResponse.StatusCode.ToString() == "OK")
        //                {
        //                    var result = streamReader.ReadToEnd();
        //                    myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
        //                }

        //                return myDeserializedClass;
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
        //        return myDeserializedClass;
        //    }

        //}
        protected void ddlMarket_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubMarket();
        }
        protected void ddlSubMarket_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindState();
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounty();
        }
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindFootPrint();
        }
        protected void ddlFootprint_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPlanCategory();
        }
        protected void ddlPlanCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPlanType();
        }
        protected void ddlPlanA_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindNetworkA();
        }
        protected void ddlPlanB_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindNetworkB();
        }
        protected void ddlPlanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPlanNames();
        }
        private string ConvertListToString(RadComboBox MultiDDL)
        {
            string CheckedItems = string.Empty;
            if (MultiDDL.CheckedItems.Count > 0)
            {

                var CheckedItemsColelction = MultiDDL.CheckedItems;
                foreach (var item in CheckedItemsColelction)
                {
                    CheckedItems += item.Value + ",";
                }
                CheckedItems = CheckedItems.Substring(0, CheckedItems.Length - 1);
            }
            return CheckedItems;
        }
        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ProductIntelC2L7Li"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lbIscreated = ((Label)e.Row.FindControl("lbIscreated")).Text.ToString();
                LinkButton lnkDownload = ((LinkButton)e.Row.FindControl("lnkDownload"));

                if (lbIscreated.ToString() == "1")
                {
                    lnkDownload.Enabled = true;
                    lnkDownload.ForeColor = Color.Blue;
                }
                else
                {
                    lnkDownload.Enabled = false;
                }

            }
        }
    }
}