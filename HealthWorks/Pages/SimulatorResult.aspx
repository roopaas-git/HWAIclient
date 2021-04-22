<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="SimulatorResult.aspx.cs" Inherits="HealthWorks.Pages.SimulatorResult" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <style>
        canvas
        {
            max-height: 250px !important;
        }
         .st0 
        {
            stroke: #5C276E !important;
        }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HiddenField ID="HfPageLink" Value="SimulatorResult.aspx" runat="server" />
    <div class="tab-nav-container">
        <nav class="tab-nav nav-light navbar">
            <ul class="nav">
                <li class="nav-item tab-nav-item plan-nav">
                    <a class="nav-link" href="#">Plan</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton  CssClass="nav-link" id="lnk_ScenarioList" runat="server"  Text="Scenario List" OnClick="lnk_ScenarioList_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton  CssClass="nav-link" id="lnk_Planfinder" runat="server"  Text="Plan List" OnClick="lnk_Planfinder_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Market Insight</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item design-nav">
                    <a class="nav-link" href="#">Design</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton  CssClass="nav-link active" id="lnk_QuickAccess" runat="server"  Text="Quick Access" OnClick="lnk_QuickAccess_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Detailed Screens</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item analyze-nav active">
                    <a class="nav-link" href="#">Analyze</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton  CssClass="nav-link active" id="lnk_Simulated" runat="server"  Text="Simulated Rank" OnClick="lnk_Simulated_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Market Summary</a>
                        </li>
                    </ul>
                </li>
            </ul>
        </nav>
    </div>
    <section class="content">
        <div class="scenario-table-outer-wrapper">
            <div class="scenario-table-inner-wrapper">
                <div class="scenario-table-header-wrapper">
                    <div class="scenario-table-header">
                        <h1><span class="header-label">Simulated Rank For Scenario: </span>
                            <asp:Label ID="lblScenarioName" Text="" runat="server" CssClass="header-text" />
                        </h1>
                    </div>
                    <div class="scenario-table-controls">
                        <div class="scenario-table-title" hidden>
                            <div class="choosen-title">
                                <asp:Label Text="" runat="server" ID="lblPlanName" />
                            </div>
                            <div class="choosen-title">
                                <asp:Label Text="" runat="server" ID="lblBid_Id" />
                            </div>
                        </div>
                        <div class="filter">
                            <asp:DropDownList runat="server" ID="ddlState" CssClass="custom-select state-select" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="ddlCounty" CssClass="custom-select state-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="ddlPersona" CssClass="custom-select country-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPersona_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="link-wrapper">
                            <asp:LinkButton ID="btnShare" Text="Share" runat="server" CssClass="control-link edit-link" OnClick="btnShare_Click" ToolTip="Share">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 14.05" fill="currentColor">
                                    <path class="cls-1" d="M15.83,3.76,12.22.17h0a.55.55,0,0,0-.63-.12.58.58,0,0,0-.36.54V2.18a4.7,4.7,0,0,0-2.82,1A5.47,5.47,0,0,0,6.45,8.13a.13.13,0,0,0,0,.06l.07.41A.56.56,0,0,0,7,9.07H7.1a.58.58,0,0,0,.5-.28l.22-.36a4.54,4.54,0,0,1,2.35-2.09,3.42,3.42,0,0,1,1.05-.19V7.81a.58.58,0,0,0,.36.54.61.61,0,0,0,.64-.13l3.61-3.63A.59.59,0,0,0,15.83,3.76ZM11.9,5a4.43,4.43,0,0,0-2.11.21A5.27,5.27,0,0,0,7.63,6.73l0-.16A4.25,4.25,0,0,1,9.18,4.06a4,4,0,0,1,2.58-.73.6.6,0,0,0,.63-.58V2L14.6,4.18,12.39,6.39V5.61A.58.58,0,0,0,11.9,5Z" />
                                    <path class="cls-1" d="M14.51,10.06a.59.59,0,0,0-.59.59V12.1a.78.78,0,0,1-.78.78H2a.78.78,0,0,1-.78-.78V4A.78.78,0,0,1,2,3.17H4.32A.59.59,0,0,0,4.32,2H2A2,2,0,0,0,0,4V12.1a2,2,0,0,0,2,2H13.14a2,2,0,0,0,1.95-2V10.65A.59.59,0,0,0,14.51,10.06Z" />
                                </svg>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="scenario-table-container">
                    <div class="table-responsive">
                        <asp:GridView ID="grdData" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                            BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                            OnRowDataBound="grdData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Bid ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblBidID" runat="server" Text='<%# Bind("[Bid_Id]")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblScenarioId" runat="server" Text='<%# Bind("[sId]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plan Name">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPlanName" runat="server" Text='<%# Bind("[Plan_Name]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OOPC Old">
                                    <ItemTemplate>
                                        <asp:Label ID="LblGrandTotal" runat="server" Text='<%# Bind("[OOPC Old]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OOPC New">
                                    <ItemTemplate>
                                        <asp:Label ID="LblGrandTotalNew" runat="server" Text='<%# Bind("[OOPC New]","{0:f0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank Old" SortExpression="[Rank Old]">
                                    <ItemTemplate>
                                        <asp:Label ID="LblRank" runat="server" Text='<%# Bind("[Rank Old]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank New" SortExpression="[Rank New]">
                                    <ItemTemplate>
                                        <asp:Label ID="LblRankNew" runat="server" Text='<%# Bind("[Rank New]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="bottom-controls">
                <asp:LinkButton ID="btnrevert" runat="server" CssClass="control-link edit-link" OnClick="btnrevert_Click" ToolTip="Revert">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left">
                        <polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                <asp:LinkButton ID="btnSaveAs" runat="server" CssClass="control-link edit-link" OnClick="btnSaveAs_Click" ToolTip="Save As">
                     <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor" width="14" height="14" stroke-width="2">
                     <g>
	<path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8"/>
	<path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z"/>
	<polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	"/>
	<polyline class="st0" points="7,3.4 7,7 9.4,7 	"/>
</g>
                 </svg>
                </asp:LinkButton>
                <asp:LinkButton ID="btnSaveFilters" Text="Save" runat="server" CssClass="control-link edit-link" OnClick="btnSaveFilters_Click" ToolTip="Save">
                     <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save">
                        <path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </asp:LinkButton>
                <asp:LinkButton ID="lnk_dwn" runat="server" CssClass="control-link edit-link" OnClick="lnk_dwn_Click" ToolTip="Download">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download">
                        <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
                </asp:LinkButton>
            </div>
        </div>

        <div class="scenario-table-outer-wrapper">
            <div class="scenario-table-inner-wrapper">
                <div class="scenario-table-header-wrapper">
                    <div class="scenario-table-header">
                        <h1><span class="header-label">Select Bid Id to Compare</span></h1>
                    </div>
                    <div class="row">
                        <div class="col-4">
                            <asp:DropDownList runat="server" ID="ddlGraph" CssClass="custom-select state-select" AutoPostBack="true" OnSelectedIndexChanged="ddlGraph_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-7">
                        </div>
                    </div>
                </div>
                <div class="scenario-table-container">
                    <div class="row">
                        <div class="col-6" hidden>
                            <telerik:RadHtmlChart ID="RadHtmlChart1" runat="server" Width="100%" Height="279px" CssClass="RadHtmlChart1" >
                                <PlotArea>
                                    <Series>
                                        <telerik:WaterfallSeries>
                                            <SeriesItems >
                                            </SeriesItems>
                                            <LabelsAppearance Visible="true"></LabelsAppearance>
                                        </telerik:WaterfallSeries>
                                    </Series>
                                    <XAxis>
                                        <LabelsAppearance>
                                            <TextStyle FontSize="10px" Padding="11px" />
                                        </LabelsAppearance>
                                        <MinorGridLines Visible="false" />
                                        <MajorGridLines Color="#c1c1c1" />
                                        <TitleAppearance Position="Left"></TitleAppearance>
                                        <Items>
                                        </Items>
                                    </XAxis>
     
                                    <YAxis MajorTickSize="1"   MinValue="0" MaxValue="800" Step="200" >
                                        <MinorGridLines Visible="false" />
                                        <MajorGridLines Color="#c1c1c1" />
                                    </YAxis>
                                </PlotArea>
                            </telerik:RadHtmlChart>
                        </div>
                        <div class="col-12">
                            <div class="table-responsive servicegrd">
                                <asp:GridView ID="grd_Services" ClientIDMode="Static" runat="server" AutoGenerateColumns="true" Height="5px" OnRowDataBound="grd_Services_RowDataBound"
                                  BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"  >
                                </asp:GridView>
                            </div>
                        </div>
                </div>
                </div>
            </div>
            <div class="bottom-controls">
                <a href="#" class="control-link refresh-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left">
                        <polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </a>
                <a href="#" class="control-link edit-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor" width="14" height="14" stroke-width="2">
                        <g>
                            <path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8" />
                            <path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z" />
                            <polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	" />
                            <polyline class="st0" points="7,3.4 7,7 9.4,7 	" />
                        </g>
                    </svg>
                </a>
                <a href="#" class="control-link save-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save">
                        <path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </a>
                <asp:LinkButton ID="lnk_service" runat="server" OnClick="lnk_service_Click" CssClass="control-link download-link" ToolTip="Download Data">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download">
                        <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
                </asp:LinkButton>
            </div>

        </div>

    </section>
    <div id="myModal3" class="custom-modal">
        <div class="custom-modal-header">
            <span>Save Scenario As</span>
            <span class="far fa-times-circle close2"></span>
        </div>
        <div class="custom-modal-content">
            <div class="row">
                <div class="col-7">
                    <asp:TextBox ID="txtScenario" CssClass="form-control" placeholder="Scenario Name" runat="server"></asp:TextBox>
                </div>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="requiredScenario" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Saveas" ControlToValidate="txtScenario"></asp:RequiredFieldValidator>
                </div>
                <div class="col-4">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-7">
                    <asp:TextBox ID="txtScenarioDesc" TextMode="MultiLine" CssClass="form-control" placeholder="Scenario Description" runat="server"></asp:TextBox>

                </div>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="requireddesc" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Saveas" ControlToValidate="txtScenarioDesc"></asp:RequiredFieldValidator>
                </div>
                <div class="col-4">
                    <asp:Button ID="btnPopUpSave" runat="server" OnClick="btnPopUpSave_Click" ValidationGroup="Saveas" Text="Save" CssClass="btn form-control" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal3 = document.getElementById('myModal3');

        // Get the button that opens the modal
        var btn = document.getElementsByClassName("jsShare");

        // Get the <span> element that closes the modal
        var span3 = document.getElementsByClassName("close2")[0];

        // Get the <span> element that closes the modal
        var cancel = document.getElementsByClassName("cancelBtn")[0];

        // When the user clicks the button, open the modal
        function openModal3() {
            modal3.style.display = "block";
        }
        // When the user clicks on <span> (x), close the modal
        span3.onclick = function () {
            modal3.style.display = "none";
        }
    </script>

    <div id="myModal4" class="custom-modal">
        <!-- Modal content -->
        <div class="custom-modal-header">
            <span>Share Scenario</span>
            <span class="far fa-times-circle closeModal1"></span>
        </div>
        <div class="custom-modal-content">
            <div class="row">
                <div class="col-3">
                    <asp:Label ID="lblUsernamelist" CssClass="form-control-label" runat="server" Text="To:"></asp:Label>
                </div>
                <div class="col-7">
                    <asp:DropDownList ID="ddlUser" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                </div>
                <div class="col-2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Shareplan" InitialValue="Select User" ControlToValidate="ddlUser"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                    <asp:Label ID="lblMessage" CssClass="form-control-label" runat="server" Text="Message:"></asp:Label>
                </div>
                <div class="col-7">
                    <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Please provide Message"></asp:TextBox>

                </div>
                <div class="col-2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Shareplan" ControlToValidate="txtMessage"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row" style="padding-bottom: 10px;">
                <div class="col-3">
                </div>
                <div class="col-7">
                    <asp:Button ID="btnSharePlan" CssClass="btn form-control" OnClick="btnPopUpShare_Click" Text="Share" runat="server" ValidationGroup="Shareplan" Style="margin-top: 10px !important" />
                </div>
                <div class="col-2">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal4 = document.getElementById('myModal4');

        // Get the button that opens the modal
        var btn = document.getElementsByClassName("jsShare");

        // Get the <span> element that closes the modal
        var span4 = document.getElementsByClassName("closeModal1")[0];

        // Get the <span> element that closes the modal
        var cancel = document.getElementsByClassName("cancelBtn")[0];

        // When the user clicks the button, open the modal
        function openModal4() {
            modal4.style.display = "block";
        }
        // When the user clicks on <span> (x), close the modal
        span4.onclick = function () {
            modal4.style.display = "none";
        }
    </script>
</asp:Content>