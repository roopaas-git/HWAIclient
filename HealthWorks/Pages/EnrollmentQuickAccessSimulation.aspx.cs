using CommonUtility;
using HealthWorks.Content.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessUtility;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using ClosedXML.Excel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections;

namespace HealthWorks.Pages
{
    public partial class EnrollmentQuickAccessSimulation : System.Web.UI.Page
    {
        PageTracking PT = new PageTracking();
        EnrollmentPlansUserInputsMethods enrollmentPlansUserInputsMethods = new EnrollmentPlansUserInputsMethods();
        EnrollmentPlanListMethods enrollmentPlanListMethods = new EnrollmentPlanListMethods();
        EnrollmentQuickAccessSimulationMethods enrollmentQuickAccessSimulationMethods = new EnrollmentQuickAccessSimulationMethods();
        EnrollmentScenarioDetailMethods enrollmentScenarioDetailMethods = new EnrollmentScenarioDetailMethods();
        private DataTable resultDataTable = new DataTable();
        private DataTable finalDataTable = new DataTable();
        private DataTable rawDataMappingDT = new DataTable();
        protected bool IsChecked = false;
        private bool ExcelUploadValidationStatus;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    if (!CheckIsUploadAllowedValidation())
                    {
                        Response.Redirect("~/Pages/EnrollmentScenarioList.aspx");
                    }

                    if (!IsPostBack)
                    {
                        UploadFile.Attributes["onchange"] = "UploadFileChange(this)";

                        if ((Session["EnrollmentSimulatorScenarioID"] != null) && (Session["EnrollmentSimulatorScenarioName"] != null))
                        {
                            lblScenarioName.Text = Session["EnrollmentSimulatorScenarioName"].ToString();
                            //EnableTopMenuLinks(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));
                            BindEnrollmentSimulatorPlansUserInputsPlanName(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]));
                            BindEnrollmentQuickAccessData(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]));
                        }
                    }

                    Active_DeactiveLinks();

                    // this.Validate();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool CheckIsUploadAllowedValidation()
        {
            DataTable dataTable = new DataTable();
            dataTable = enrollmentScenarioDetailMethods.GetEnrollmentScenario(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["ProcessStatus"].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Active_DeactiveLinks()
        {
            HtmlControl htmlctl = ((HtmlControl)Master.FindControl("ProductIntelC2L5li"));
            if (htmlctl != null)
            {
                htmlctl.Attributes["class"] = "active";
                ((HtmlGenericControl)htmlctl.Parent).Style["display"] = "block";
            }

            if (Session["ProgressBarStatus"] != null)
            {
                if (Convert.ToInt32(Session["ProgressBarStatus"]) == 2)
                {
                    DownloadPBP.Attributes["class"] = "active";
                }
            }

            if(chkBenefitChangeOption.Checked)
            {
                excelUpload.Style.Add("display", "block");
                dynamicUpload.Style.Add("display", "none");
                btnSimulate.Style.Add("display", "none");
                lbRevert.Style.Add("display", "none");
            }
            else
            {
                excelUpload.Style.Add("display", "none");
                dynamicUpload.Style.Add("display", "block");
                btnSimulate.Style.Add("display", "block");
                lbRevert.Style.Add("display", "block");
            }
        }
        
        protected void lbRevert_Click(object sender, EventArgs e)
        {
            BindEnrollmentQuickAccessData(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]));
        }

        protected void LBScenarios_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("EnrollmentScenarioList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentScenarioList.aspx");
                Response.Redirect("~/Pages/EnrollmentScenarioList.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void LBPlans_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("EnrollmentPlanList", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentPlanList.aspx");
                Response.Redirect("~/Pages/EnrollmentPlanList.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void LBQuickAccess_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("EnrollmentQuickAccess", Session["SessionId"].ToString(), Session["UserName"].ToString(), "EnrollmentQuickAccess.aspx");
                Response.Redirect("~/Pages/EnrollmentQuickAccess.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void LBSimulatedOutput_Click(object sender, EventArgs e)
        {
            try
            {
                PT.InsertDataIntoDB("ManageSimulatedOutput", Session["SessionId"].ToString(), Session["UserName"].ToString(), "ManageSimulatedOutput.aspx");
                Response.Redirect("~/Pages/ManageSimulatedOutput.aspx");
            }
            catch (Exception ex1)
            {

            }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            InitializeUpload.Enabled = false;

            EnrollmentFileUploadMethods objEnrollmentFileUpload = new EnrollmentFileUploadMethods();

            DataTable downloadPBPDataTable = new DataTable();
            downloadPBPDataTable = objEnrollmentFileUpload.GetEnrollmentFileUploadDetails(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]));

            if (downloadPBPDataTable.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowHideDynamicUploadDiv();", true);

                if (!(UploadFile.FileName.Contains(downloadPBPDataTable.Rows[0]["BidId"].ToString())))
                {
                    string msg = "alert('Invalid file uploaded. Please upload the PBP file for " + Session["EnrollmentSimulator_BidID"].ToString() + ".')";
                    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alertMessage", msg, true);
                }

                if (((UploadFile.FileName.Contains(downloadPBPDataTable.Rows[0]["BidId"].ToString()))) && (!UploadFile.FileName.Contains(Session["EnrollmentSimulator_BidID"].ToString())))
                {
                    string msg = "alert('The Bid ID you are trying to upload for (" + Path.GetFileNameWithoutExtension(UploadFile.FileName) + "), does not match the selection in the table (" + Session["EnrollmentSimulator_BidID"].ToString() + "). Please check the selection or the uploaded file.')";
                    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alertMessage", msg, true);
                }
            }

            if (string.IsNullOrEmpty(Session["EnrollmentSimulator_BidID"].ToString()))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error while uploading!');", true);
            }
            else if (UploadFile.HasFile)
            {
                List<string> allowedExtensions = new List<string>() { ".xlsx" };
                var extension = UploadFile.FileName.Substring(UploadFile.FileName.LastIndexOf('.')).ToLower();

                if (allowedExtensions.Contains(extension))
                {
                    if (UploadFile.FileName.Contains(Session["EnrollmentSimulator_BidID"].ToString()))
                    {
                        ExcelUploadValidationStatus = true;
                        int count = 1;
                        var postedFileName = Path.GetFileName(UploadFile.FileName).Replace(" ", "_");
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year));
                        }

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month));
                        }

                        var filePath = HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + UploadFile.FileName.Replace(" ", "_"));

                        while (File.Exists(filePath))
                        {
                            string tempFileName = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(postedFileName), count++);
                            postedFileName = tempFileName + extension;
                            filePath = Path.Combine(Path.GetDirectoryName(filePath), postedFileName);
                        }
                        
                        rawDataMappingDT = enrollmentPlanListMethods.GetRawDataMappingDetails().Tables[0];

                        finalDataTable = ImportExceltoDatatable(Session["EnrollmentSimulator_BidID"].ToString(), Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]));

                        if (ExcelUploadValidationStatus == false)
                        {
                            ModalProgress.Hide();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Validation failed, please enter valid benefit value(s)!')", true);                            
                        }
                        else
                        {                            

                            if (finalDataTable.Rows.Count < 1)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('For Simulation, You need to change atleast one benefit value!');", true);
                            }
                            else
                            {
                                if (downloadPBPDataTable.Rows.Count > 0)
                                {
                                    UploadFile.SaveAs(filePath);
                                    string uploadedFilePath = "EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + postedFileName.Replace(" ", "_");
                                    if (!objEnrollmentFileUpload.UpdateEnrollmentFileUploadFilePath(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]), uploadedFilePath))
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error in uploading, Please contact administrator!');", true);
                                    }
                                }
                                else
                                {
                                    EnrollmentFileUpload enrollmentFileUpload = new EnrollmentFileUpload();
                                    enrollmentFileUpload.ScenarioID = Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]);
                                    enrollmentFileUpload.UploadedFilePath = "EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + postedFileName.Replace(" ", "_");

                                    EnrollmentPlansUserInputs objEnrollmentPlansUserInputs = new EnrollmentPlansUserInputs();
                                    objEnrollmentPlansUserInputs = (EnrollmentPlansUserInputs)Session["EnrollmentSimulatorSavedPlan"];

                                    enrollmentFileUpload.BidId = objEnrollmentPlansUserInputs.BidId;
                                    enrollmentFileUpload.PlanName = objEnrollmentPlansUserInputs.PlanName;
                                    enrollmentFileUpload.CreatedBy = Session["UserName"].ToString();
                                    enrollmentFileUpload.CreatedDate = DateTime.Now;

                                    if (!objEnrollmentFileUpload.Insert(enrollmentFileUpload))
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error in uploading, Please contact administrator!');", true);
                                    }
                                }

                                if (enrollmentScenarioDetailMethods.UpdateEnrollmentScenarioProcessStatus(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]), 2))
                                {
                                    if (CompleteSaveOption(2))
                                    {
                                        Session["EnrollmentSimulatorPlanListScenarioID"] = Session["EnrollmentSimulatorScenarioID"].ToString();
                                        Response.AddHeader("REFRESH", "1;URL=EnrollmentScenarioList.aspx");
                                    }
                                    else
                                    {
                                        ModalProgress.Hide();
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OOPS, There was an error!", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OOPS, There was an error!", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        string msg = "alert('Invalid file uploaded. Please upload the PBP file for " + Session["EnrollmentSimulator_BidID"].ToString() + ".')";
                        ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alertMessage", msg, true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Allowed file format (.xlsx) only!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OOPS, There was an error!", true);
            }

            InitializeUpload.Enabled = true;
        }

        protected void Download_Click(object sender, EventArgs e)
        {
            try
            {
                string sc = Session["EnrollmentSimulator_BidID"].ToString();

                EnrollmentPlansUserInputs objEnrollmentPlansUserInputs = new EnrollmentPlansUserInputs();
                objEnrollmentPlansUserInputs = (EnrollmentPlansUserInputs)Session["EnrollmentSimulatorSavedPlan"];

                DataSet planListDS = enrollmentPlanListMethods.GetDownloadResult(sc, objEnrollmentPlansUserInputs.BidLevelStateIds, objEnrollmentPlansUserInputs.BidLevelCountyIds, objEnrollmentPlansUserInputs.PlanCategoryIds);
                string attachment = "attachment; Filename=\" "+ sc + ".xlsx\"";
                DataTable dt = new DataTable();

                planListDS.Tables[0].Columns.Remove("IsComma");
                planListDS.Tables[0].Columns.Remove("Benefit Data Type");
                planListDS.Tables[0].Columns.Remove("Range");
                dt = planListDS.Tables[0];
                if (planListDS.Tables[0].Rows.Count > 0)
                {
                    XLWorkbook wb = new XLWorkbook();
                    //wb.Worksheets.Add(dt, sc);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.ContentType = "application/vnd.ms-excel";

                    Response.AddHeader("content-disposition", attachment);
                    IXLWorksheet ws = wb.Worksheets.Add(dt, sc);
                    ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    ws.Row(1).InsertRowsAbove(8);

                    ws.Cell(1, 1).Value = "Instructions:";
                    ws.Cell(2, 1).Value = "Step 1:";
                    ws.Cell(2, 2).Value = "Apply Filters on Row No 9";
                    ws.Cell(3, 1).Value = "Step 2:";
                    ws.Cell(3, 2).Value = "Select the benefit(s) to be simulated";
                    ws.Cell(3, 3).Value = "PBP Dictionary reference";
                    //ws.Cell(3, 4).Value = "https://tegbusiness-my.sharepoint.com/:x:/g/personal/anirudh_bhutani_healthworksai_com/Ec6C3Xm1bLROs4NT5CsmRWABrHDAgSI3ufmrJmulf4dVHA?e=xlcEqJ";
                    ws.Cell(3, 4).Value = "https://tegbusiness-my.sharepoint.com/:x:/g/personal/aditya_kumar_healthworksai_com/EUyvIZaewRdJve3vY0ST4T8B5ISoj67f3wFieESMlWcvYw?rtime=obuj_IEc2kg";
                    ws.Cell(4, 1).Value = "Step 3:";
                    ws.Cell(4, 2).Value = "Add the new benefit value(s) in Column E (New Value) for simulation";
                    ws.Cell(5, 1).Value = "Step 4:";
                    ws.Cell(5, 2).Value = "Save & Upload the PDP file with the same file name (bid_id).";
                    ws.Cell(7, 1).Value = "Plan Name";
                    ws.Cell(7, 2).Value = Session["EnrollmentSimulator_PlanName"].ToString();
                    ws.Columns("A").Width = 35;
                    ws.Columns("B").Width = 35;
                    ws.Columns("C").Width = 71;
                    ws.Columns("D").Width = 14;
                    ws.Columns("E").Width = 14;

                    //ws.Cell(1, 1).Style.Font.SetBold();

                    ws.Columns("D").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    ws.Columns("E").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    ws.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(9, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(9, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(9, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(9, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(9, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                    }

                    if (IsPostBack)
                    {
                        Session["Check"] = "true";
                        Session["ProgressBarStatus"] = 2;
                    }
                }

                EnrollmentFileUpload enrollmentFileUpload = new EnrollmentFileUpload();
                EnrollmentFileUploadMethods objEnrollmentFileUpload = new EnrollmentFileUploadMethods();             
                objEnrollmentPlansUserInputs = (EnrollmentPlansUserInputs)Session["EnrollmentSimulatorSavedPlan"];

                enrollmentFileUpload.ScenarioID = Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]);
                enrollmentFileUpload.BidId = objEnrollmentPlansUserInputs.BidId;             
                enrollmentFileUpload.PlanName = objEnrollmentPlansUserInputs.PlanName;
                enrollmentFileUpload.CreatedBy = Session["UserName"].ToString();
                enrollmentFileUpload.CreatedDate = DateTime.Now;

                if (!objEnrollmentFileUpload.Insert(enrollmentFileUpload))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error in downloading, Please contact administrator!');", true);
                }
                //  Response.End();
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.                
                //chkBenefitChangeOption.Checked = true;
            }
            catch (Exception ew)
            {
            }
        }

        protected void InitializeUpload_Click(object sender, EventArgs e)
        {
            chkBenefitChangeOption.Checked = true;
            UpdatePanel2.Update();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openFileUpload", "InitializeUploadDialog()", true);
        }

        private void BindEnrollmentSimulatorPlansUserInputsPlanName(int ScenarioID)
        {
            DataSet enrollmentPlansUserInputsDS = enrollmentPlansUserInputsMethods.GetEnrollmentSimulatorPlansUserInputsPlanName(ScenarioID);
            Session["EnrollmentSimulator_BidID"] = enrollmentPlansUserInputsDS.Tables[0].Rows[0]["BidID"];
            ddlBidId.DataSource = enrollmentPlansUserInputsDS;
            ddlBidId.DataTextField = "PlanName";
            ddlBidId.DataValueField = "BidID";
            ddlBidId.DataBind();

            bidIdFileName.InnerHtml = "*Upload PBP file name should be " + Session["EnrollmentSimulator_BidID"].ToString();
        }

        private void BindEnrollmentQuickAccessData(int ScenarioID)
        {
            string sc = Session["EnrollmentSimulator_BidID"].ToString();
            
            EnrollmentPlansUserInputs objEnrollmentPlansUserInputs = new EnrollmentPlansUserInputs();

            objEnrollmentPlansUserInputs = (EnrollmentPlansUserInputs)Session["EnrollmentSimulatorSavedPlan"];

            DataSet enrollmentQuickAccessDS = enrollmentPlanListMethods.GetDownloadResult(sc, objEnrollmentPlansUserInputs.BidLevelStateIds, objEnrollmentPlansUserInputs.BidLevelCountyIds, objEnrollmentPlansUserInputs.PlanCategoryIds);
            if (enrollmentQuickAccessDS.Tables[0].Rows.Count > 0)
            {
                rptBenefits.DataSource = enrollmentQuickAccessDS;
                rptBenefits.DataBind();
            }
        }

        private DataTable SaveData()
        {
            DataTable changedValueDT = new DataTable();

            changedValueDT.Columns.Add(new DataColumn("BenefitGrp"));
            changedValueDT.Columns.Add(new DataColumn("Benefits"));
            changedValueDT.Columns.Add(new DataColumn("BenefitDesc"));
            changedValueDT.Columns.Add(new DataColumn("Current"));
            changedValueDT.Columns.Add(new DataColumn("ChangedVal"));
            changedValueDT.Columns.Add(new DataColumn("SessionId"));

            resultDataTable.Columns.Add(new DataColumn("Benefit Group"));
            resultDataTable.Columns.Add(new DataColumn("Benefits"));
            resultDataTable.Columns.Add(new DataColumn("Benefit Description"));
            resultDataTable.Columns.Add(new DataColumn("Current Value"));
            resultDataTable.Columns.Add(new DataColumn("New Value"));
            
            foreach (RepeaterItem ri in rptBenefits.Items)
            {
                TextBox ChangedVal = ri.FindControl("lblChanged") as TextBox;

                Label lblBenefitGrp = ri.FindControl("lblBenefitGrp") as Label;

                Label lblBenefits = ri.FindControl("lblBenefits") as Label;

                Label lblBenefitDesc = ri.FindControl("lblBenefitDesc") as Label;

                Label lblCurrent = ri.FindControl("lblCurrent") as Label;

                DataRow dr = resultDataTable.NewRow();

                dr[0] = lblBenefitGrp.Text;
                dr[1] = lblBenefits.Text;
                dr[2] = lblBenefitDesc.Text;
                dr[3] = lblCurrent.Text;
                dr[4] = ChangedVal.Text;
                
                resultDataTable.Rows.Add(dr);
                if (ChangedVal.Text != lblCurrent.Text)
                {
                    DataRow cdr = changedValueDT.NewRow();

                    cdr[0] = lblBenefitGrp.Text;
                    cdr[1] = lblBenefits.Text;
                    cdr[2] = lblBenefitDesc.Text;
                    cdr[3] = lblCurrent.Text;
                    cdr[4] = ChangedVal.Text;
                    cdr[5] = Session["EnrollmentSimulatorScenarioID"];

                    changedValueDT.Rows.Add(cdr);
                }
            }
            return changedValueDT;
        }

        public DataTable ImportExceltoDatatable(string sheetName, int enrollmentSimulatorScenarioID)
        {            
            // Open the Excel file using ClosedXML.
            // Keep in mind the Excel file cannot be open when trying to read it
            using (XLWorkbook workBook = new XLWorkbook(UploadFile.PostedFile.InputStream))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                bool firstRow = true;

                foreach (IXLRow row in workSheet.Rows())
                {
                    if (row.RowNumber() < 9)
                    {
                        // do nothing
                    }
                    else
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells(1, 5))
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            dt.Columns.Add("ScenarioID");
                            firstRow = false;
                        }
                        else
                        {
                            // if (string.IsNullOrEmpty(row.Cell("5").Value.ToString()))
                            if (row.Cell("4").Value.ToString() == row.Cell("5").Value.ToString())
                            {
                                //no nothing
                            }
                            else
                            {
                                
                                int i = 0;
                                if(ValidateNewValueRow(row.Cell("1").Value.ToString(), row.Cell("2").Value.ToString(), row.Cell("4").Value.ToString(), row.Cell("5").GetFormattedString().ToString()))
                                {
                                    //Add rows to DataTable.
                                     dt.Rows.Add();
                                    //foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                                    foreach (IXLCell cell in row.Cells(1, dt.Columns.Count))
                                    {
                                        

                                        //if (cell.Address.ColumnLetter == "E")
                                        //{
                                        //    //if (row.Cell("4").Value.ToString().Contains("$"))
                                        //    //{
                                        //    //    var tempBenfitNewValue = row.Cell("5").GetFormattedString().ToString().Replace("$", "");
                                        //    //    tempBenfitNewValue = tempBenfitNewValue.Replace(",", "");

                                        //    //    dt.Rows[dt.Rows.Count - 1][i] = "$"+ Convert.ToInt32(tempBenfitNewValue).ToString("N0") ;
                                        //    //}
                                        //    //else
                                        //    //{
                                        //    //    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                        //    //}
                                        //}
                                        //else
                                        //{
                                        //    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                        //}
                                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                        i++;
                                    }
                                    dt.Rows[dt.Rows.Count - 1][i - 1] = enrollmentSimulatorScenarioID;
                                }
                                else
                                {
                                    ExcelUploadValidationStatus = false;
                                    dt.Clear();                       
                                    return dt;
                                }                                
                            }
                        }
                    }
                }

                return dt;
            }
        }

        // get mapping data
        // save to Datatable
        
        private bool ValidateNewValueRow(string BenefitGroup, string Benefit, string benfitCurrentValue, string benfitNewValue)
        {
            bool isValid = false;

            var row = rawDataMappingDT.AsEnumerable().Where(r => r.Field<string>("Benefit Group") == BenefitGroup && r.Field<string>("Benefit") == Benefit).First();

            string dtType = row["Benefit Data Type"].ToString().ToLower();
            string benfitRange_Min = row["Range"].ToString().Split('|')[0];
            string benfitRange_Max = row["Range"].ToString().Split('|')[1];
            int maxlength = benfitRange_Max.Count();

            if(benfitRange_Min.Contains("/"))
            {
                benfitRange_Min = benfitRange_Min.Split('/')[1];
            }
            
            switch (dtType)
            {
                case "int":

                    if (benfitNewValue.Count() > maxlength)
                    {
                        isValid = false;
                    }
                    else
                    {
                        bool isAllDigitsAllowedPer = !benfitNewValue.Any(ch => ch < '0' || ch > '9');

                        if (isAllDigitsAllowedPer)
                        {

                            if ((Convert.ToInt32(benfitNewValue) > Convert.ToInt32(benfitRange_Max) || (Convert.ToInt32(benfitNewValue) < Convert.ToInt32(benfitRange_Min))))
                            {
                                isValid = false;
                            }
                            else
                            {
                                isValid = true;
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }

                    break;

                case "percentage":

                    //if (!benfitNewValue.Contains("%"))
                    //{
                    //    isValid = false;
                    //}
                    //else
                    //{
                        if (benfitNewValue.Count() > (maxlength + 1))
                        {
                            isValid = false;
                        }
                        else
                        {
                            var tempBenfitNewValuePer = benfitNewValue.Replace("%", "");

                            bool isAllDigitsAllowedPer = !tempBenfitNewValuePer.Any(ch => ch < '0' || ch > '9');

                            if (isAllDigitsAllowedPer)
                            {
                                if ((Convert.ToInt32(tempBenfitNewValuePer) > Convert.ToInt32(benfitRange_Max) || (Convert.ToInt32(tempBenfitNewValuePer) < Convert.ToInt32(benfitRange_Min))))
                                {
                                    isValid = false;
                                }
                                else
                                {
                                    isValid = true;
                                }
                            }
                            else
                            {
                                isValid = false;
                            }
                        }
                   // }

                    break;

                case "money":

                    //if (!benfitNewValue.Contains("$"))
                    //{
                    //    isValid = false;
                    //}
                    //else
                    //{
                        var tempBenfitNewValue = benfitNewValue.Replace("$", "");
                        tempBenfitNewValue = tempBenfitNewValue.Replace(",", "");

                        bool isAllDigitsAllowed = !tempBenfitNewValue.Any(ch => ch < '0' || ch > '9');

                        if (isAllDigitsAllowed)
                        {
                            if ((Convert.ToInt32(tempBenfitNewValue) > Convert.ToInt32(benfitRange_Max) || (Convert.ToInt32(tempBenfitNewValue) < Convert.ToInt32(benfitRange_Min))))
                            {
                                isValid = false;
                            }
                            else
                            {
                                isValid = true;
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    //}

                    break;

                case "boolean":

                    if (benfitNewValue.Count() > maxlength)
                    {
                        isValid = false;
                    }
                    else
                    {
                        if ((benfitNewValue.ToLower() == "yes") || (benfitNewValue.ToLower() == "no"))
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    break;

                case "float":

                    if (benfitNewValue.Count() > maxlength + 2)
                    {
                        isValid = false;
                    }
                    else
                    {

                        int rangeInt;
                        bool isNumerical = int.TryParse(benfitNewValue, out rangeInt);

                        float rangeFloat;
                        bool isfloat = float.TryParse(benfitNewValue, out rangeFloat);


                        if (!(isNumerical || isfloat))
                        {
                            isValid = false;
                        }
                        else
                        {
                            var validRange = new ArrayList();
                            var loopRange = ((int.Parse(benfitRange_Max) - int.Parse(benfitRange_Min)) + (int.Parse(benfitRange_Max) - int.Parse(benfitRange_Min))) + 1;
                            dynamic startRange = benfitRange_Min;
                            validRange.Add(int.Parse(startRange));
                            for (var i = 1; i < loopRange; i++)
                            {
                                dynamic temp = null;
                                if (i % 2 != 0)
                                {
                                    temp = (float.Parse((int.Parse(startRange) + .5).ToString()));
                                }
                                else
                                {
                                    temp = (int.Parse((float.Parse(startRange) + .5).ToString()));
                                }
                                validRange.Add(temp);
                                startRange = temp.ToString();
                            }

                            if(isNumerical)
                            {
                                if (!(validRange.Contains(int.Parse(benfitNewValue))))
                                {
                                    isValid = false;
                                }
                                else
                                {
                                    isValid = true;
                                }
                            }
                            else
                            {
                                if (!(validRange.IndexOf(float.Parse(benfitNewValue)) != -1))
                                {
                                    isValid = false;
                                }
                                else
                                {
                                    isValid = true;
                                }
                            }
                            
                        }

                        //float f;
                        //int i;
                        //if (int.TryParse(benfitNewValue))
                        //{
                        //}
                        //else if (float.TryParse(benfitNewValue))
                        //{


                        //  benfitNewValue.All(char.is);
                        //stringTest.All(char.IsDigit);
                        //bool isAllDigitsAllowed = benfitNewValue.All(char.IsNumber);

                        //int myInt;
                        //bool isNumerical = int.TryParse(benfitNewValue, out myInt);

                        //float myfloat;
                        //bool isfloat = float.TryParse(benfitNewValue, out myfloat);

                        ////bool isAllDigitsAlloweds = benfitNewValue.All(char.IsDigit);

                        //if (isAllDigitsAllowed)
                        //{
                        //    if (((dynamic)benfitNewValue > (dynamic)benfitRange_Max) || ((dynamic)benfitNewValue < (dynamic)benfitRange_Min))
                        //    {
                        //        isValid = false;
                        //    }
                        //    else
                        //    {
                        //        isValid = true;
                        //    }

                        //}
                    }  
                             
                    break;


                case "string_tickcross":

                    if (benfitNewValue.Count() > maxlength)
                    {
                        isValid = false;
                    }
                    else
                    {
                        bool isAllDigitsAllowedST = !benfitNewValue.Any(ch => ch < '0' || ch > '1');

                        if (isAllDigitsAllowedST)
                        {
                            if ((Convert.ToInt32(benfitNewValue) > Convert.ToInt32(benfitRange_Max) || (Convert.ToInt32(benfitNewValue) < Convert.ToInt32(benfitRange_Min))))
                            {
                                isValid = false;
                            }
                            else
                            {
                                isValid = true;
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }

                break;
                                  


                case "string_nc_money":

                    //if (!benfitNewValue.Contains("$"))
                    //{
                    //    isValid = false;
                    //}
                    //else
                    //{
                    var tempBenfitNewValueNCM = benfitNewValue.Replace("$", "");
                    tempBenfitNewValue = tempBenfitNewValueNCM.Replace(",", "");

                    bool isAllDigitsAllowedNCM = !tempBenfitNewValue.Any(ch => ch < '0' || ch > '9');

                    if (isAllDigitsAllowedNCM)
                    {
                        if ((Convert.ToInt32(tempBenfitNewValueNCM) > Convert.ToInt32(benfitRange_Max) || (Convert.ToInt32(tempBenfitNewValueNCM) < Convert.ToInt32(benfitRange_Min))))
                        {
                            isValid = false;
                        }
                        else
                        {
                            isValid = true;
                        }
                    }
                    else if (benfitNewValue.ToLower() == "nc")
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                    //}

                    break;

                case "string_nc_percentage":

                    //if (!benfitNewValue.Contains("%"))
                    //{
                    //    isValid = false;
                    //}
                    //else
                    //{
                    if (benfitNewValue.Count() > (maxlength + 1))
                    {
                        isValid = false;
                    }
                    else
                    {
                        var tempBenfitNewValuePer = benfitNewValue.Replace("%", "");

                        bool isAllDigitsAllowedPer = !tempBenfitNewValuePer.Any(ch => ch < '0' || ch > '9');

                        if (isAllDigitsAllowedPer)
                        {
                            if ((Convert.ToInt32(tempBenfitNewValuePer) > Convert.ToInt32(benfitRange_Max) || (Convert.ToInt32(tempBenfitNewValuePer) < Convert.ToInt32(benfitRange_Min))))
                            {
                                isValid = false;
                            }
                            else
                            {
                                isValid = true;
                            }
                        }
                        else if (benfitNewValue.ToLower() == "nc")
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    // }

                    break;

            }

            return isValid;
        }


        protected void btnSimulate_Click(object sender, EventArgs e)
        {
            finalDataTable = SaveData();
            if (finalDataTable.Rows.Count < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('For Simulation, You need to change atleast one benefit value!');", true);
            }
            else
            {
                if (enrollmentScenarioDetailMethods.UpdateEnrollmentScenarioProcessStatus(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]), 2))
                {
                    if (CompleteSaveOption(1))                    {                        
                        Session["EnrollmentSimulatorPlanListScenarioID"] = Session["EnrollmentSimulatorScenarioID"].ToString();                        
                        Response.AddHeader("REFRESH", "1;URL=EnrollmentScenarioList.aspx");
                    }
                    else
                    {
                        ModalProgress.Hide();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OOPS, There was an error!", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OOPS, There was an error!", true);
                }
            }
        }

        private bool SaveChangesToDB()
        {
            bool isSaved = false;
            if (enrollmentQuickAccessSimulationMethods.SaveChangedResults(finalDataTable) != 0)
            {
                isSaved = true;
            }
            return isSaved;
        }
        
        public class RequestInputParams
        {
            public PlanFilters Filters { get; set; }            
            public object BenefitList { get; set; }
        }
               
        public class PlanFilters
        {
            public string BidId { get; set; }
            public string ScenarioId { get; set; }
            public List<StatesCounties> StatesCounties { get; set; }
            public string PlanCategories { get; set; }
            public string Premiums { get; set; }
            public string PlanTypes { get; set; }
        }

        public class StatesCounties
        {
            public string State { get; set; }
            public string Counties { get; set; }
        }


        private bool CallExternalSimulationApi(DataTable inputDT)
        {
            bool isApiSuccess = false;
            
            RequestInputParams requestInputParams = new RequestInputParams();
            PlanFilters planFilters = new PlanFilters();
            List<StatesCounties> StatesCounties = new List<StatesCounties>();

            DataSet filtersForSimulation = enrollmentPlanListMethods.GetFiltersForSimulation(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()), Convert.ToInt32(Session["ClientId"].ToString()));
            planFilters.BidId = Session["EnrollmentSimulator_BidID"].ToString();
            planFilters.ScenarioId = Session["EnrollmentSimulatorScenarioID"].ToString();

            var state = string.Empty;
            var counties = string.Empty;
            var PlanCategory = string.Empty;
            var Premium = string.Empty;
            var PlanType = string.Empty;

            if (filtersForSimulation.Tables[0].Rows.Count > 0)
            {
                string previousState = string.Empty;

                for (int i = 0; i < filtersForSimulation.Tables[0].Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(previousState))
                    {
                        previousState = filtersForSimulation.Tables[0].Rows[i]["State"].ToString();

                        state = filtersForSimulation.Tables[0].Rows[i]["State"].ToString();
                        counties += filtersForSimulation.Tables[0].Rows[i]["County"].ToString() + ",";
                    }
                    else if (previousState == filtersForSimulation.Tables[0].Rows[i]["State"].ToString())
                    {
                        counties += filtersForSimulation.Tables[0].Rows[i]["County"].ToString() + ",";
                    }
                    else
                    {
                        StatesCounties statesCounties = new StatesCounties();
                        statesCounties.State = state;                        
                        counties = counties.TrimEnd(',');
                        statesCounties.Counties = counties;
                        StatesCounties.Add(statesCounties);

                        state = string.Empty;
                        counties = string.Empty;

                        state = filtersForSimulation.Tables[0].Rows[i]["State"].ToString();
                        counties += filtersForSimulation.Tables[0].Rows[i]["County"].ToString() + ",";
                    }

                    previousState = filtersForSimulation.Tables[0].Rows[i]["State"].ToString();
                   
                    if (!PlanCategory.Contains(filtersForSimulation.Tables[0].Rows[i]["PlanCategory"].ToString()))
                    {
                        PlanCategory += filtersForSimulation.Tables[0].Rows[i]["PlanCategory"].ToString() + ",";
                    }

                    if (!Premium.Contains(filtersForSimulation.Tables[0].Rows[i]["Premium"].ToString()))
                    {
                        Premium += filtersForSimulation.Tables[0].Rows[i]["Premium"].ToString() + ",";
                    }

                    if (!PlanType.Contains(filtersForSimulation.Tables[0].Rows[i]["PlanType"].ToString()))
                    {
                        PlanType += filtersForSimulation.Tables[0].Rows[i]["PlanType"].ToString() + ",";
                    }

                    if (i == (filtersForSimulation.Tables[0].Rows.Count)-1)
                    {
                        StatesCounties statesCounties = new StatesCounties();
                        statesCounties.State = state;
                        counties = counties.TrimEnd(',');
                        statesCounties.Counties = counties;
                        StatesCounties.Add(statesCounties);
                    }
                }
            }

            planFilters.StatesCounties = StatesCounties;

            PlanCategory = PlanCategory.TrimEnd(',');
            Premium = Premium.TrimEnd(',');
            PlanType = PlanType.TrimEnd(',');

            planFilters.PlanCategories = PlanCategory;
            planFilters.Premiums = Premium;
            planFilters.PlanTypes = PlanType;

            DataSet benefitsForSimulation = enrollmentPlanListMethods.GetBenefitsForSimulation(Session["EnrollmentSimulator_BidID"].ToString(), Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"].ToString()));

            requestInputParams.Filters = planFilters;
            requestInputParams.BenefitList = benefitsForSimulation.Tables[0];

            using (var client = new HttpClient())
            {
                string uri = ConfigurationManager.AppSettings["SimulationApiBaseURL"] + "simulation";
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(requestInputParams);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(uri, stringContent).Result;

                string result = response.Content.ReadAsStringAsync().Result;
                               
                if(response.StatusCode.ToString() == "NOT FOUND")
                {
                    return false;
                }

                if (response.IsSuccessStatusCode)
                {
                    isApiSuccess = true;
                }
                else
                {    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('We are unable to process your simulation request, please contact the Client Success team (clientsuccess@healthworksai.com) with the necessary details!')", true);
                    Response.AddHeader("REFRESH", "2;URL=EnrollmentScenarioList.aspx");
                    isApiSuccess = false;                   
                }
            }
            return isApiSuccess;
        }
        
        //Type 1 = UI
        //Type 2 = Upload
        private bool CompleteSaveOption(int Type)
        {
            bool isSaved = false;

            if (SaveChangesToDB())
            {
                if (Type == 1)
                {
                    try
                    {
                        string sc = Session["EnrollmentSimulator_BidID"].ToString();

                        EnrollmentPlansUserInputs objEnrollmentPlansUserInputs = new EnrollmentPlansUserInputs();
                        objEnrollmentPlansUserInputs = (EnrollmentPlansUserInputs)Session["EnrollmentSimulatorSavedPlan"];


                        DataTable planListDS = resultDataTable;
                        string attachment = "attachment; Filename=" + sc + ".xlsx";

                        if (planListDS.Rows.Count > 0)
                        {
                            XLWorkbook wb = new XLWorkbook();
                            //Response.Clear();
                            //Response.Buffer = true;
                            //Response.Charset = "";
                            //Response.ContentType = "application/vnd.ms-excel";
                            //Response.AddHeader("content-disposition", attachment);
                            IXLWorksheet ws = wb.Worksheets.Add(planListDS, sc);
                            ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                            ws.Row(1).InsertRowsAbove(8);

                            ws.Cell(1, 1).Value = "Instructions:";
                            ws.Cell(2, 1).Value = "Step 1:";
                            ws.Cell(2, 2).Value = "Apply Filters on Row No 9";
                            ws.Cell(3, 1).Value = "Step 2:";
                            ws.Cell(3, 2).Value = "Select the benefit(s) to be simulated";
                            ws.Cell(3, 3).Value = "PBP Dictionary reference";
                            //ws.Cell(3, 4).Value = "https://tegbusiness-my.sharepoint.com/:x:/g/personal/anirudh_bhutani_healthworksai_com/Ec6C3Xm1bLROs4NT5CsmRWABrHDAgSI3ufmrJmulf4dVHA?e=xlcEqJ";
                            ws.Cell(3, 4).Value = "https://tegbusiness-my.sharepoint.com/:x:/g/personal/aditya_kumar_healthworksai_com/EUyvIZaewRdJve3vY0ST4T8B5ISoj67f3wFieESMlWcvYw?rtime=obuj_IEc2kg";
                            ws.Cell(4, 1).Value = "Step 3:";
                            ws.Cell(4, 2).Value = "Add the new benefit value(s) in Column E (New Value) for simulation";
                            ws.Cell(5, 1).Value = "Step 4:";
                            ws.Cell(5, 2).Value = "Save & Upload the PDP file with the same file name (bid_id).";
                            ws.Cell(7, 1).Value = "Plan Name";
                            ws.Cell(7, 2).Value = Session["EnrollmentSimulator_PlanName"].ToString();
                            ws.Columns("A").Width = 35;
                            ws.Columns("B").Width = 35;
                            ws.Columns("C").Width = 71;
                            ws.Columns("D").Width = 14;
                            ws.Columns("E").Width = 14;

                            ws.Columns("D").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                            ws.Columns("E").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                            ws.Cell(3, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            ws.Cell(9, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(9, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(9, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(9, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            ws.Cell(9, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            //using (MemoryStream MyMemoryStream = new MemoryStream())
                            //{
                            //    wb.SaveAs(MyMemoryStream);
                            //    MyMemoryStream.WriteTo(Response.OutputStream);
                            //    Response.Flush();
                            //}                           


                            EnrollmentFileUpload enrollmentFileUpload = new EnrollmentFileUpload();
                            EnrollmentFileUploadMethods objEnrollmentFileUpload = new EnrollmentFileUploadMethods();
                            objEnrollmentPlansUserInputs = (EnrollmentPlansUserInputs)Session["EnrollmentSimulatorSavedPlan"];

                            enrollmentFileUpload.ScenarioID = Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]);
                            enrollmentFileUpload.BidId = objEnrollmentPlansUserInputs.BidId;
                            enrollmentFileUpload.PlanName = objEnrollmentPlansUserInputs.PlanName;
                            enrollmentFileUpload.CreatedBy = Session["UserName"].ToString();
                            enrollmentFileUpload.CreatedDate = DateTime.Now;

                           

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year));
                            }

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month));
                            }

                            int count = 1;
                            var postedFileName = sc.Replace(" ", "_");
                            var filePath = HttpContext.Current.Server.MapPath("~/EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + sc.Replace(" ", "_")+ ".xlsx");

                            

                            string uploadedFilePath = "EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + sc.Replace(" ", "_") + ".xlsx";


                            while (File.Exists(filePath))
                            {
                                string tempFileName = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(postedFileName), count++);
                                postedFileName = tempFileName + ".xlsx";
                                filePath = Path.Combine(Path.GetDirectoryName(filePath), postedFileName);

                                uploadedFilePath = "EnrollmentUploadedInputFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + postedFileName.Replace(" ", "_");
                            }


                           

                            wb.SaveAs(filePath);


                            

                            enrollmentFileUpload.UploadedFilePath = uploadedFilePath;

                            if (!objEnrollmentFileUpload.Insert(enrollmentFileUpload))
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error in downloading, Please contact administrator!');", true);
                            }


                           // Response.Flush();
                          //  HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                          //  HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                          //  HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.                
                            //chkBenefitChangeOption.Checked = true;
                        }
                    }
                    catch (Exception ew)
                    {
                    }



                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Changes for Scenario " + Session["EnrollmentSimulatorScenarioName"].ToString() + " is successful, you will be notified when the results are ready!');", true);     
                }
                else
                {
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload for Scenario " + Session["EnrollmentSimulatorScenarioName"].ToString() 
                }

                if (CallExternalSimulationApi(finalDataTable))
                {
                   // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Session["EnrollmentSimulatorScenarioName"].ToString() + " is successful');", true);

                    if (enrollmentScenarioDetailMethods.UpdateEnrollmentScenarioProcessStatus(Convert.ToInt32(Session["EnrollmentSimulatorScenarioID"]), 3))
                    {
                        //Session["EnrollmentSimulatorPlanListScenarioID"] = Session["EnrollmentSimulatorScenarioID"].ToString();
                        //ModalProgress.Hide();
                        //Response.AddHeader("REFRESH", "1;URL=EnrollmentScenarioList.aspx");
                        isSaved = true;
                    }
                    else
                    {
                        //ModalProgress.Hide();                        
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OOPS, There was an error!", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OOPS, Error in Simulation API!", true);
                }
            }

            return isSaved;
        }

        protected void rptBenefits_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string CurrentValue = string.Empty;

                RepeaterItem item = e.Item;
                Label lblCurrent = item.FindControl("lblCurrent") as Label;
                if (lblCurrent.Text != "")
                    lblCurrent.Text = Convert.ToInt32(lblCurrent.Text).ToString("N0");
                    CurrentValue = lblCurrent.Text;



            }
        }

        protected void rptBenefits_ItemCreated(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}