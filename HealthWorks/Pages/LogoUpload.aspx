<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="LogoUpload.aspx.cs" Inherits="HealthWorks.Pages.LogoUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#SupportLi').find('i').removeClass('fa-angle-right').addClass('fa-angle-down')
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfFocus" />
    <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfFocusStatus" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content">
                <div class="main-header" runat="server" id="divMainHeader">
                    <h2>
                        <asp:Label Text="Manage Image" runat="server" ID="pageName" /></h2>
                </div>
                <div class="main-header-description">
                    <em>
                        <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                </div>
                <div class="main-content">
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="widget">
                                <div class="widget-header">
                                    <h3><i class="fas fa-upload"></i>Upload Image</h3>
                                    <div class="btn-group widget-header-toolbar" id="divCreateTicket" runat="server"
                                        clientidmode="Static">
                                        <a href="#" title="Focus" class="btn-borderless btn-focus" id="lbCreateTicketFocus"
                                            runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                                class="btn-borderless btn-toggle-expand" id="lbCreateTicket" runat="server" clientidmode="Static">
                                                <i class="fa fa-chevron-up"></i></a>
                                    </div>
                                </div>
                                <div class="widget-content">
                                    <div class="form-group row firstrow">
                                        <div class="col-md-2 control-label">
                                            <asp:Label ID="Label1" runat="server" Text="Image :"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2 ">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="form-group row firstrow">
                                        <div class="col-md-2 control-label">
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn form-control" Text="Upload" OnClick="Upload" />
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2 ">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <%-- <asp:PostBackTrigger ControlID="btn_Documents" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <div class="main-content" hidden>
        <div class="form-group row">
            <div class="col-md-12">
                <div class="widget">
                    <div class="widget-header">
                        <h3><i class="fas fa-file-upload"></i>Upload Documents</h3>
                        <div class="btn-group widget-header-toolbar" id="div1" runat="server"
                            clientidmode="Static">
                            <a href="#" title="Focus" class="btn-borderless btn-focus" id="A1"
                                runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                    class="btn-borderless btn-toggle-expand" id="A2" runat="server" clientidmode="Static">
                                    <i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-content">
                        <div class="form-group row firstrow">
                            <div class="col-md-2 control-label">
                                <asp:Label ID="Label3" runat="server" Text="Document Name :"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddl_PdfUpload" CssClass="custom-select">
                                    <asp:ListItem Text="Select" Value="Select" Selected="True" />
                                    <asp:ListItem Text="Opportunity Analysis" Value="OpportunityAnalysis.pdf" />
                                    <asp:ListItem Text="Competitor Analysis" Value="CompetitorAnalysis.pdf" />
                                    <asp:ListItem Text="Market Snapshot" Value="MarketSnapshot.pdf" />
                                    <asp:ListItem Text="Winning Plans" Value="WinningPlans.pdf" />
                                    <asp:ListItem Text="Plan Comparison" Value="PlanComparison.pdf" />
                                    <asp:ListItem Text="MRx Plan Comparison" Value="MRxPlanComparison.pdf" />
                                    <asp:ListItem Text="Plan Comparisons Download" Value="PlanComparison.pdf" />
                                    <asp:ListItem Text="Product Insight" Value="ProductInsight.pdf" />
                                    <asp:ListItem Text="Sales Forecast" Value="SalesForecast.pdf" />
                                    <asp:ListItem Text="Territorry Planning" Value="TerritorryPlanning.pdf" />
                                    <asp:ListItem Text="Plannedvs Actuals" Value="PlannedvsActuals.pdf" />
                                    <asp:ListItem Text="Retrospective Report" Value="RetrospectiveReport.pdf" />
                                    <asp:ListItem Text="Sales and Terminations" Value="SalesandTerminations.pdf" />
                                    <asp:ListItem Text="Executive Summary" Value="ExecutiveSummary.pdf" />
                                    <asp:ListItem Text="Direct Marketing" Value="DirectMarketing.pdf" />
                                    <asp:ListItem Text="Site Performance" Value="SitePerformance.pdf" />
                                    <asp:ListItem Text="High Value Activity" Value="HighValueActivity.pdf" />
                                    <asp:ListItem Text="Enrollment Analysis" Value="EnrollmentAnalysis.pdf" />
                                    <asp:ListItem Text="Competitor Analysis Post" Value="CompetitorAnalysisPost.pdf" />
                                    <asp:ListItem Text="Regional Analysis" Value="RegionalAnalysis.pdf" />
                                    <asp:ListItem Text="Product Analysis" Value="ProductAnalysis.pdf" />
                                    <asp:ListItem Text="Hospital Compare" Value="HospitalCompare.pdf" />
                                    <asp:ListItem Text="Provider Scorecard" Value="ProviderScorecard.pdf" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Required"
                                    CssClass="form-control-label" ControlToValidate="ddl_PdfUpload" ForeColor="Red"
                                    ValidationGroup="upload" runat="server" InitialValue="Select" />
                            </div>
                            <div class="col-md-2 ">
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>


                        <div class="form-group row firstrow">
                            <div class="col-md-2 control-label">
                                <asp:Label ID="Label2" runat="server" Text="Document :"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:FileUpload ID="documentUpload" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-2 ">
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-2 control-label">
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btn_Documents" runat="server" OnClick="btn_Documents_Click" CssClass="btn form-control" Text="Upload" ValidationGroup="upload" />
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-2 ">
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
