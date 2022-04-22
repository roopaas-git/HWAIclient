using System.Configuration;

namespace CommonUtility
{
    public static class Constants
    {
        #region General Constant Variables
        public const string iframeSRC = "src";
        public const string isTrue = "True";
        public const string isFalse = "False";
        public const string teganalytics = "teganalytics.com";
        public const string healthworksai = "healthworksai.com";
        public const string platinum = "Platinum";
        public const string silver = "Silver";
        public const string gold = "Gold";
        public const string accountNotActivatedFirst = "Click the button below to activate your HealthWorksAI account.";
        public const string activationCodeInvalid = "Invalid activation code. please contact your dedicated Account Manager or email our team at support@healthworksai.com.";
        public const string activationSuccess = "Your HealthWorksAI account is activated sucessfully,Click on the button below to login.";
        public const string accountNotActivated = "Your account is not activated.";
        public const string credentialsNotValid = "Credentials are not valid.";
        public const string accountNotExist = "Your account does not Exist.";
        public const string acccountLocked = "Your account is locked, Please Contact Administrator.";
        public const string successfull = "Successfully Inserted";
        public const string successfullyDeleted = "Successfully Deleted";
        public const string somethingWentWrong = "Something went wrong";

        //public const string sasLogFileLocation = "D:/Simulator/OOPC2020PlanFinder/OOPC";
        //public const string sasExeFileLocation = "D:/SASHome/SASFoundation/9.4/sas.exe";
        //public const string sasProgramFileLocation = "D:/Simulator/OOPC2020PlanFinder/programs/OOPCV1P.sas";


        public const string sasLogFileLocation = "E:/Simulator/OOPC2020PlanFinder/OOPC";
        public const string sasExeFileLocation = "E:/SAS/SASFoundation/9.4/sas.exe";
        public const string sasProgramFileLocation = "E:/Simulator/OOPC2020PlanFinder/programs/OOPCV1P.sas";

        public static string pythonExeFileLocation = ConfigurationManager.AppSettings["PythonExeFile"].ToString();
        public static string pythonProgramFile = ConfigurationManager.AppSettings["PythonFile"].ToString();
        public static string pythonAPICall = ConfigurationManager.AppSettings["PythonAPICall"].ToString();
        public static string PythonProviderDataCSV = ConfigurationManager.AppSettings["PythonProviderDataCSV"].ToString();

        public const string platinumDocumentsFolder = "https:/ibxhealthworks.analytics-hub.com";
        public const string goldDocumentsFolder = "https:/teghealthworks.analytics-hub.com";
        public const string platinumDocumentsFolderUpload = @"D:/inetpub/wwwroot/IbxHealthWorks/Documents/";
        public const string goldDocumentsFolderUpload = @"D:/inetpub/wwwroot/HealthWorksDemo/Documents/";

        #endregion


        #region Constant Variable for Email
        public const string fromAddress = "clientsuccess@healthworksai.com";
        public const string teamAddress = "teammedicare@teganalytics.com";
        public const string passWord = "HWai@123";
        public const string hostAddress = "smtp.office365.com";

        public const string passWordHeader = "Your Password Details";
        public const string activationHeader = "Account Activation";
        public const string sharePlanHeader = " has shared a plan with you.";
        public const string ticketHeader = "Ticket is Raised !";
        public const string ticketReOpenHeader = "Ticket is Re-Opened !";
        public const int portNumber = 587;
        #endregion

        #region Static Variables from Configuration file
        public static string tableauServerName = ConfigurationManager.AppSettings["TableauIP"].ToString();
        public static string tableauUser = ConfigurationManager.AppSettings["tabUser"].ToString();
        public static string tableauSiteName = ConfigurationManager.AppSettings["SiteName"].ToString();
        public static string tableauWhitepaperSiteName = ConfigurationManager.AppSettings["WhitepaperSiteName"].ToString();
        public static string httyType = ConfigurationManager.AppSettings["HttpType"].ToString();
        public static string TableauServerProtocol = httyType + "://" + tableauServerName;
        public static string inputFilesLocation = ConfigurationManager.AppSettings["inputFilesLocation"].ToString();
        public static string siteAddress = ConfigurationManager.AppSettings["siteAddress"].ToString();
        #endregion

        public static string Whitepaper1 = ConfigurationManager.AppSettings["WhitePaper1"].ToString();
        public static string Whitepaper2 = ConfigurationManager.AppSettings["WhitePaper2"].ToString();
        public static string TPVWhitepaper = ConfigurationManager.AppSettings["TPVPdf"].ToString();
        public static string PostAEPFindingsReport = ConfigurationManager.AppSettings["2021PostAEPFindingsReport"].ToString();

        //#region Market Intelligence Section Variables from Configuration file
        //public static string OpportunityAnalysis = ConfigurationManager.AppSettings["OpportunityAnalysis"].ToString();
        //public static string CompetitorAnalysis = ConfigurationManager.AppSettings["CompetitorAnalysis"].ToString();
        //public static string CompetitorAnalysis2020 = ConfigurationManager.AppSettings["CompetitorAnalysis2020"].ToString();
        //public static string MarketSnapshot = ConfigurationManager.AppSettings["MarketSnapshot"].ToString();
        //public static string ProviderNetworkAnalysis = ConfigurationManager.AppSettings["ProviderNetworkAnalysis"].ToString();
        //public static string ProviderNetworkFacilities = ConfigurationManager.AppSettings["ProviderNetworkFacilities"].ToString();

        //public static string OpportunityAnalysisPDF = ConfigurationManager.AppSettings["OpportunityAnalysisPDF"].ToString();
        //public static string CompetitorAnalysisPDF = ConfigurationManager.AppSettings["CompetitorAnalysisPDF"].ToString();
        //public static string CompetitorAnalysis2020PDF = ConfigurationManager.AppSettings["CompetitorAnalysis2020PDF"].ToString();
        //public static string MarketSnapshotPDF = ConfigurationManager.AppSettings["MarketSnapshotPDF"].ToString();
        //public static string ProviderNetworkAnalysisPDF = ConfigurationManager.AppSettings["ProviderNetworkAnalysisPDF"].ToString();
        //public static string ProviderNetworkFacilitiesPDF = ConfigurationManager.AppSettings["ProviderNetworkFacilitiesPDF"].ToString();

        //#endregion

        //#region Product Intelligence Section Variables from Configuration file        
        //public static string PlanComparisons = ConfigurationManager.AppSettings["PlanComparisons"].ToString();
        //public static string PlanComparisonsDownload = ConfigurationManager.AppSettings["PlanComparisonsDownload"].ToString();
        //public static string MRXPlanComparisons = ConfigurationManager.AppSettings["MRXPlanComparisons"].ToString();
        //public static string PlanComparisonsNY = ConfigurationManager.AppSettings["PlanComparisonsNY"].ToString();
        //public static string PlanComparisonsDownloadNY = ConfigurationManager.AppSettings["PlanComparisonsDownloadNY"].ToString();
        //public static string MRXPlanComparisonsNY = ConfigurationManager.AppSettings["MRXPlanComparisonsNY"].ToString();
        //public static string WinningPlans = ConfigurationManager.AppSettings["WinningPlans"].ToString();
        //public static string ProductInsights = ConfigurationManager.AppSettings["ProductInsights"].ToString();

        //public static string MRXPlanComparisonsPdf = ConfigurationManager.AppSettings["MRXPlanComparisonsPdf"].ToString();
        //public static string PlanComparisonsPdf = ConfigurationManager.AppSettings["PlanComparisonsPdf"].ToString();
        //public static string PlanComparisonsDownloadPdf = ConfigurationManager.AppSettings["PlanComparisonsDownloadPdf"].ToString();
        //public static string WinningPlansPDF = ConfigurationManager.AppSettings["WinningPlansPDF"].ToString();
        //public static string ProductInsightsPdf = ConfigurationManager.AppSettings["ProductInsightsPdf"].ToString();
        //#endregion

        //#region Enrollment Intelligence Section Variables from Configuration file
        //public static string EnrollmentTrends = ConfigurationManager.AppSettings["EnrollmentTrends"].ToString();
        //public static string EnrollmentForecastAEP = ConfigurationManager.AppSettings["EnrollmentForecastAEP"].ToString();
        //public static string SalesandTerminations = ConfigurationManager.AppSettings["SalesandTerminations"].ToString();
        //public static string YoYEnrollment = ConfigurationManager.AppSettings["YoYEnrollment"].ToString();
        //public static string LOBEnrollments = ConfigurationManager.AppSettings["LOBEnrollments"].ToString();

        //public static string EnrollmentTrendsPdf = ConfigurationManager.AppSettings["EnrollmentTrendsPdf"].ToString();
        //public static string EnrollmentForecastAEPPdf = ConfigurationManager.AppSettings["EnrollmentForecastAEPPdf"].ToString();
        //public static string SalesandTerminationsPdf = ConfigurationManager.AppSettings["SalesandTerminationsPdf"].ToString();
        //public static string YoYEnrollmentPdf = ConfigurationManager.AppSettings["YoYEnrollmentPdf"].ToString();
        //public static string LOBEnrollmentsPdf = ConfigurationManager.AppSettings["LOBEnrollmentsPdf"].ToString();
        //#endregion

        //#region Simulator Section Variables from Configuration file
        //public static string MMMSimulator = ConfigurationManager.AppSettings["MMMSimulator"].ToString();
        //public static string SimExecutiveDashboard = ConfigurationManager.AppSettings["SimExecutiveDashboard"].ToString();

        //public static string MMMSimulatorPdf = ConfigurationManager.AppSettings["MMMSimulatorPdf"].ToString();
        //public static string SimExecutiveDashboardPdf = ConfigurationManager.AppSettings["SimExecutiveDashboardPdf"].ToString();
        //#endregion

        //#region Performance Management Variables from Configuration file
        //public static string ExecutiveDashboard = ConfigurationManager.AppSettings["ExecutiveDashboard"].ToString();
        //public static string DigitalMarketing = ConfigurationManager.AppSettings["DigitalMarketing"].ToString();
        //public static string DirectMarketing = ConfigurationManager.AppSettings["DirectMarketing"].ToString();
        //public static string SitePerformance = ConfigurationManager.AppSettings["SitePerformance"].ToString();
        //public static string HighValueActivity = ConfigurationManager.AppSettings["HighValueActivity"].ToString();
        //public static string ProductScore = ConfigurationManager.AppSettings["ProductScore"].ToString();
        //public static string ProductCompetitiveAnalysis = ConfigurationManager.AppSettings["ProductCompetitiveAnalysis"].ToString();


        //public static string ExecutiveDashboardPdf = ConfigurationManager.AppSettings["ExecutiveDashboardPdf"].ToString();
        //public static string DigitalMarketingPdf = ConfigurationManager.AppSettings["DigitalMarketingPdf"].ToString();
        //public static string DirectMarketingPdf = ConfigurationManager.AppSettings["DirectMarketingPdf"].ToString();
        //public static string SitePerformancePdf = ConfigurationManager.AppSettings["SitePerformancePdf"].ToString();
        //public static string HighValueActivityPdf = ConfigurationManager.AppSettings["HighValueActivityPdf"].ToString();
        //public static string ProductScorePdf = ConfigurationManager.AppSettings["ProductScorePdf"].ToString();
        //public static string ProductCompetitiveAnalysisPdf = ConfigurationManager.AppSettings["ProductCompetitiveAnalysisPdf"].ToString();

        //#endregion

        //#region Post AEP Intelligence Variables from Configuration file

        //public static string EnrollmentAnalysis = ConfigurationManager.AppSettings["EnrollmentAnalysis"].ToString();
        //public static string CompetitorAnalysisPost = ConfigurationManager.AppSettings["CompetitorAnalysisPost"].ToString();
        //public static string RegionalAnalysis = ConfigurationManager.AppSettings["RegionalAnalysis"].ToString();
        //public static string ProductAnalysis = ConfigurationManager.AppSettings["ProductAnalysis"].ToString();


        //public static string EnrollmentAnalysisPdf = ConfigurationManager.AppSettings["EnrollmentAnalysisPdf"].ToString();
        //public static string CompetitorAnalysisPostPdf = ConfigurationManager.AppSettings["CompetitorAnalysisPostPdf"].ToString();
        //public static string RegionalAnalysisPdf = ConfigurationManager.AppSettings["RegionalAnalysisPdf"].ToString();
        //public static string ProductAnalysisPdf = ConfigurationManager.AppSettings["ProductAnalysisPdf"].ToString();
        //#endregion

        //#region Provider Intelligence Section Variables from Configuration file
        //public static string HospitalCompare = ConfigurationManager.AppSettings["HospitalCompare"].ToString();
        //public static string ProviderScorecard = ConfigurationManager.AppSettings["ProviderScorecard"].ToString();
        //public static string EmailPerformance = ConfigurationManager.AppSettings["EmailPerformance"].ToString();
        //public static string EmailPerformancePDF = ConfigurationManager.AppSettings["EmailPerformancePDF"].ToString();

        //public static string HospitalComparePdf = ConfigurationManager.AppSettings["HospitalComparePdf"].ToString();
        //public static string ProviderScorecardPdf = ConfigurationManager.AppSettings["ProviderScorecardPdf"].ToString();
        //#endregion

        #region Ticket Section Variables from Configuration file
        public static string UserLogDashboard = ConfigurationManager.AppSettings["UserLogDashboard"].ToString();
        //public static string UserLogDashboardPdf = ConfigurationManager.AppSettings["UserLogDashboardPdf"].ToString();
        #endregion

        #region Datacube variable
        public static string DataCubeSiteName = ConfigurationManager.AppSettings["DataCubeSiteName"].ToString();
        #endregion

        #region Account Type
        public static string AccountType = ConfigurationManager.AppSettings["AccountType"].ToString();
        #endregion

        #region External Websites
        public static string Solutions => "https://www.healthworks.ai/";
        public static string Insights => "https://www.covid19india.org/";
        #endregion

    }
}
