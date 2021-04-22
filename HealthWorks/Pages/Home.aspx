<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMasterDashboard.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HealthWorks.Pages.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PostToNewWindow() {
            originalTarget = document.forms[0].target;
            document.forms[0].target = '_blank';
            window.setTimeout("document.forms[0].target=originalTarget;", 300);
            return true;
        }
    </script>
    <script type="text/javascript">       
        function callwarning() {
            alert("File does not exist,contact Account Manager.");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hw-pagecontent">
        <div class="hw-body">
            <div class="hw-dashboard-cards-container row">
                <div class="col-xs col-sm-6 col-md-4 col-xl-3">
                    <div class="card hw-dashboard-card on-hover-shadow">
                        <div class="card-header">
                            <h3>
                                <i class="fas fa-lightbulb d-inline-flex mr-2"></i>
                                Market Intelligence
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="hw-dashboard-links-wrapper">
                                <asp:LinkButton ID="MarketIntelC1L1" runat="server" CommandArgument="OpportunityAnalysis" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="Opportunity Analysis" />
                                <asp:LinkButton ID="MarketIntelC1L2" runat="server" CommandArgument="CompetitorAnalysis" CommandName="1200px" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="Competitor Analysis">Competitor Analysis<%--<span class="badge badge-pill badge-warning platinumicon"> NEW</span>--%></asp:LinkButton>
                                <asp:LinkButton ID="MarketIntelC1L3" runat="server" CommandArgument="MarketSnapshot" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="Market Snapshot" />
                                <asp:LinkButton ID="UnlimitedIntelC2L1" runat="server" CommandArgument="HospitalCompare" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Hospital Compare" />
                                <asp:LinkButton ID="UnlimitedIntelC2L2" runat="server" CommandArgument="ProviderScorecard" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Provider Scorecard" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs col-sm-6 col-md-4 col-xl-3">
                    <div class="card hw-dashboard-card on-hover-shadow">
                        <div class="card-header">
                            <h3>
                                <i class="fas fa-bullseye d-inline-flex mr-2"></i>
                                Product Intelligence
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="hw-dashboard-links-wrapper">
                                <asp:LinkButton ID="MarketIntelC2L1" runat="server" CommandArgument="PlanComparison2021" CommandName="3100px" OnClick="Dashboard_Click" CssClass="hw-dashboard-link">2021 Plan Comparison <span class="badge badge-pill badge-warning platinumicon"> NEW</span></asp:LinkButton>
                                <asp:LinkButton ID="MarketIntelC2L7" runat="server" CommandArgument="PlanComparison2020" CommandName="3100px" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="2020 Plan Comparison" />
                                <asp:LinkButton ID="MarketIntelC2L2" runat="server" CommandArgument="MRXPlanComparison2021" CommandName="3100px" OnClick="Dashboard_Click" CssClass="hw-dashboard-link">2021 MRX Plan Comparison <span class="badge badge-pill badge-warning platinumicon"> NEW</span></asp:LinkButton>
                                <asp:LinkButton ID="MarketIntelC2L8" runat="server" CommandArgument="MRXPlanComparison2020" CommandName="3100px" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="2020 MRX Plan Comparison" />
                                <asp:LinkButton ID="MarketIntelC2L3" runat="server" CommandArgument="WinningPlans" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="Winning Plans" />
                                <asp:LinkButton ID="MarketIntelC2L4" runat="server" CommandArgument="ProductInsights" CommandName="1400px" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="Product Insights" />


                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs col-sm-6 col-md-4 col-xl-3">
                    <div class="card hw-dashboard-card on-hover-shadow" id="divEnrollmentIntelligence" runat="server">
                        <div class="card-header">
                            <h3>
                                <i class="fas fa-list-alt d-inline-flex mr-2"></i>
                                Enrollment Intelligence
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="hw-dashboard-links-wrapper">
                                <asp:LinkButton ID="MarketIntelC3L1" runat="server" CommandArgument="EnrollmentTrends" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="Enrollment Trends" />
                                <asp:LinkButton ID="MarketIntelC3L2" runat="server" CommandArgument="EnrollmentForecastAEP" OnClick="Dashboard_Click" CssClass="hw-dashboard-link">Enrollment Forecast AEP <span class="badge badge-pill badge-warning platinumicon"> NEW</span></asp:LinkButton>
                                <asp:LinkButton ID="MarketIntelC3L3" runat="server" CommandArgument="SalesandTerminations" OnClick="Dashboard_Click" CssClass="hw-dashboard-link">Sales & Termination <span class="badge badge-pill badge-warning platinumicon"> DEMO</span></asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs col-sm-6 col-md-4 col-xl-3">
                    <div class="card hw-dashboard-card on-hover-shadow" id="divSimulator" runat="server">
                        <div class="card-header">
                            <h3>
                                <i class="fas fa-search-dollar d-inline-flex mr-2"></i>
                                Simulators
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="hw-dashboard-links-wrapper">
                                <asp:LinkButton ID="ProductIntelC2L3" runat="server" CommandName="ScenarioList.aspx" CommandArgument="OOPCSimulator" OnClick="lbExternal_Click" CssClass="hw-dashboard-link" Text="OOPC Simulator"></asp:LinkButton>
                                <asp:LinkButton ID="MarketIntelC1L4" runat="server" CommandArgument="MPFSimulator" CommandName="MPFSimulator.aspx" OnClick="lbExternal_Click" CssClass="hw-dashboard-link" Text="MPF Simulator" ClientIDMode="Static" />
                                <asp:LinkButton ID="MarketingIntelC1L5" runat="server" CommandArgument="MMMSimulator" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="" ClientIDMode="Static">MMM Simulator <span class="badge badge-pill badge-warning platinumicon"> DEMO</span></asp:LinkButton>
                               <%-- <asp:LinkButton ID="ProductIntelC2L5" runat="server" CommandName="ManageScenarioList.aspx" CommandArgument="BenefitSimulatorScenarioList" OnClick="lbExternal_Click" CssClass="hw-dashboard-link " Text="">Benefit Simulator <span class="badge badge-pill badge-warning platinumicon"> NEW</span></asp:LinkButton>--%>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs col-sm-6 col-md-4 col-xl-3">
                    <div class="card hw-dashboard-card on-hover-shadow" id="divProviderIntelligence" runat="server">
                        <div class="card-header">
                            <h3>
                                <i class="fas fa-clinic-medical d-inline-flex mr-2"></i>
                                HealthWorksAI Insights
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="hw-dashboard-links-wrapper">
                                <asp:LinkButton ID="UnlimitedIntelC1L5" runat="server" CommandArgument="WhitePaper3" CommandName="10100px" OnClick="Whitepaper_Click" CssClass="hw-dashboard-link" ClientIDMode="Static">Cost Structure Breakdown of Trending Benefits <span class="badge badge-pill badge-warning platinumicon">NEW</span></asp:LinkButton>
                                <asp:LinkButton ID="UnlimitedIntelC1L7" runat="server" CommandArgument="WhitePaper2_2021" CommandName="7800px" OnClick="Whitepaper_Click" CssClass="hw-dashboard-link" ClientIDMode="Static">Supplemental, Enhanced and SSBCI Benefit Trends (2021) <span class="badge badge-pill badge-warning platinumicon">NEW</span></asp:LinkButton>
                                <asp:LinkButton ID="UnlimitedIntelC1L6" runat="server" CommandArgument="2021PostAEPFindingsReport" CommandName="PostAEPFindingsReport.aspx" OnClick="lbExternal_Click" CssClass="hw-dashboard-link" ClientIDMode="Static">2021 Post AEP Findings Report <span class="badge badge-pill badge-warning platinumicon">NEW</span></asp:LinkButton>
                                <asp:LinkButton ID="UnlimitedIntelC1L1" runat="server" CommandName="https://www.healthworksai.com/2021-aep-findings-report/" CommandArgument="2021AEPFindingsReport" OnClientClick="PostToNewWindow();" OnClick="lbSocio_Click" CssClass="hw-dashboard-link" Text="2021 AEP Findings Report" />
                                <asp:LinkButton ID="UnlimitedIntelC1L2" runat="server" CommandArgument="WhitePaper2_2020" CommandName="7800px" OnClick="Whitepaper_Click" CssClass="hw-dashboard-link" Text="Supplemental, Enhanced and SSBCI Benefit Trends" ClientIDMode="Static">Supplemental, Enhanced and SSBCI Benefit Trends (2020)</asp:LinkButton>
                                <asp:LinkButton ID="UnlimitedIntelC1L3" runat="server" CommandArgument="WhitePaper1" CommandName="6200px" OnClick="Whitepaper_Click" CssClass="hw-dashboard-link" Text="Emergence of $0 Premium Plans" ClientIDMode="Static">Emergence of $0 Premium Plans</asp:LinkButton>
                                <asp:LinkButton ID="UnlimitedIntelC1L4" runat="server" CommandArgument="TruePlanValue(TPV)Methodology" CommandName="TPVWhitepaper.aspx" OnClick="lbExternal_Click" CssClass="hw-dashboard-link" Text="True Plan Value (TPV) Methodology" ClientIDMode="Static">True Plan Value (TPV) Methodology</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs col-sm-6 col-md-4 col-xl-3">
                    <div class="card hw-dashboard-card on-hover-shadow" id="divPerformanceIntelligence" runat="server">
                        <div class="card-header">
                            <h3>
                                <i class="fas fa-chart-line d-inline-flex mr-2"></i>
                                Marketing Intelligence
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="hw-dashboard-links-wrapper">
                                <asp:LinkButton ID="MarketingIntelC1L1" runat="server" CommandArgument="ExecutiveDashboard" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Executive Dashboard" />
                                <asp:LinkButton ID="MarketingIntelC1L2" runat="server" CommandArgument="MarketingPerformanceOverview" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Marketing Performance Overview" />
                                <asp:LinkButton ID="MarketingIntelC1L3" runat="server" CommandArgument="IntegratedChannelPerformance" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Integrated Channel Performance" />
                                <asp:LinkButton ID="MarketingIntelC1L4" runat="server" CommandArgument="IntegratedFinancialPerformance" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Integrated Financial Performance" />
                                <asp:LinkButton ID="MarketingIntelC2L1" runat="server" CommandArgument="DigitalOverview" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Digital Overview" />
                                <asp:LinkButton ID="MarketingIntelC2L2" runat="server" CommandArgument="Digital-HighValueActivity" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Digital - High Value Activity" />
                                <asp:LinkButton ID="MarketingIntelC2L3" runat="server" CommandArgument="Digital-PathtoPurchase" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Digital - Path to Purchase" />
                                <asp:LinkButton ID="MarketingIntelC3L1" runat="server" CommandArgument="EmailPerformance" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Email Performance" />
                                <asp:LinkButton ID="MarketingIntelC3L2" runat="server" CommandArgument="SocialMediaPerformance" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Social Media Performance" />
                                <asp:LinkButton ID="MarketingIntelC3L3" runat="server" CommandArgument="DirectMailPerformance" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Direct Mail Performance" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs col-sm-6 col-md-4 col-xl-3">
                    <div class="card hw-dashboard-card on-hover-shadow" id="divPostAEP" runat="server">
                        <div class="card-header">
                            <h3>
                                <i class="fas fa-calendar-week d-inline-flex mr-2"></i>
                                Post AEP Intelligence
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="hw-dashboard-links-wrapper">
                                <asp:LinkButton ID="ProductIntelC3L1" runat="server" CommandName="" CommandArgument="EnrollmentAnalysis" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Enrollment Analysis" />
                                <asp:LinkButton ID="ProductIntelC3L2" runat="server" CommandName="" CommandArgument="PostAEPCompetitorAnalysis" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Competitor Analysis" />
                                <asp:LinkButton ID="ProductIntelC3L3" runat="server" CommandName="" CommandArgument="RegionAnalysis" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Region Analysis" />
                                <asp:LinkButton ID="ProductIntelC3L4" runat="server" CommandName="" CommandArgument="ProductAnalysis" OnClick="Dashboard_Click" CssClass="hw-dashboard-link disabled" Text="Product Analysis" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs col-sm-6 col-md-4 col-xl-3">
                    <div class="card hw-dashboard-card on-hover-shadow">
                        <div class="card-header">
                            <h3>
                                <i class="fas fa-cubes d-inline-flex mr-2"></i>
                                Data & Download
                            </h3>
                        </div>
                        <div class="card-body">
                            <div class="hw-dashboard-links-wrapper">
                                <asp:LinkButton ID="UnlimitedIntelC3L1" runat="server" CommandName="BenefitCompetitiveGrid" CommandArgument="Benefit Competitive Grid" OnClick="lbDownload_Click" CssClass="hw-dashboard-link">Benefit Competitive Grid <span class="badge badge-pill badge-warning platinumicon">NEW</span></asp:LinkButton>
                                <asp:LinkButton ID="UnlimitedIntelC3L2" runat="server" CommandName="3100px" CommandArgument="PlanComparisonDownload2021" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" >2021 Plan Comparision (Download)<span class="badge badge-pill badge-warning platinumicon">NEW</span></asp:LinkButton>
                                <asp:LinkButton ID="UnlimitedIntelC3L6" runat="server" CommandName="3100px" CommandArgument="PlanComparisonDownload2020" OnClick="Dashboard_Click" CssClass="hw-dashboard-link" Text="2020 Plan Comparision (Download)" />
                                <asp:LinkButton ID="UnlimitedIntelC3L3" runat="server" CommandName="CompetitorCSV" CommandArgument="CompetitorAnalysisCube" OnClientClick="PostToNewWindow();" OnClick="lbDataCube_Click" CssClass="hw-dashboard-link" Text="Competitor Analysis" />
                                <asp:LinkButton ID="UnlimitedIntelC3L4" runat="server" CommandName="PopulationHealth" CommandArgument="PopulationHealthCube" OnClientClick="PostToNewWindow();" OnClick="lbDataCube_Click" CssClass="hw-dashboard-link" Text="Population Health" />
                                <asp:LinkButton ID="UnlimitedIntelC3L5" runat="server" CommandName="Census" CommandArgument="CensusCube" OnClientClick="PostToNewWindow();" OnClick="lbDataCube_Click" CssClass="hw-dashboard-link" Text="Census" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%-- <div id="myModal2" class="custom-modal">
        <!-- Modal content -->
       <%-- <div class="custom-modal-header">
            <span>Do you want to autoload data cube?</span>
            <span class="far fa-times-circle close2"></span>
        </div>
        <div class="custom-modal-content">
            <div class="row">                
                <div class="col-12">
                     <span>Your subscription has expired. Please <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Clickhere_Click">click here</asp:LinkButton> to renew or get more details.</span>                   
                </div>                
            </div>
            <div class="row">
                </br>
            </div>
             <div class="row">
                </br>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal2 = document.getElementById('myModal2');

        var divv = document.getElementById('#toptab');

        // Get the button that opens the modal
        var btn = document.getElementsByClassName("jsShare");

        // Get the <span> element that closes the modal
        var span2 = document.getElementsByClassName("close2")[0];

        // Get the <span> element that closes the modal
        var cancel = document.getElementsByClassName("cancelBtn")[0];

        // When the user clicks the button, open the modal
        function openModal2() {
            modal2.style.display = "block";

        }
        // When the user clicks on <span> (x), close the modal
        span2.onclick = function () {
            modal2.style.display = "none";
        }
    </script>--%>
</asp:Content>
