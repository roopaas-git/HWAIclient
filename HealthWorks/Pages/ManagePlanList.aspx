<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="ManagePlanList.aspx.cs" EnableEventValidation="false" Inherits="HealthWorks.Pages.ManagePlanList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <script type="text/javascript">
        function PostToNewWindow() {
            originalTarget = document.forms[0].target;
            document.forms[0].target = '_blank';
            window.setTimeout("document.forms[0].target=originalTarget;", 300);
            return true;
        }
    </script>
    <style>
        .validator {
            color: red;
            margin-right: 30px;
        }
    </style>
    <script type="text/javascript">   
        $(document).ready(function () {
            maintainScrollPosition();
        });
        function pageLoad() {
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#divPlanView").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
    <div class="tab-nav-container">
        <nav class="tab-nav nav-light navbar">
            <ul class="nav">
                <li class="nav-item tab-nav-item plan-nav active">
                    <a class="nav-link" href="#">Plan</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBScenarios" runat="server" Text="Scenario List" OnClick="LBScenarios_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item ">
                            <asp:LinkButton CssClass="nav-link active" ID="LBPlans" runat="server" Text="Plan List" OnClick="LBPlans_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Market Insight</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item design-nav" id="id_Quick" runat="server">
                    <a class="nav-link" href="#">Design</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBQuickAccess" runat="server" Text="Quick Access" OnClick="LBQuickAccess_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Detailed Screens</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item analyze-nav" id="id_Simulate" runat="server">
                    <a class="nav-link" href="#">Analyze</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBSimulatedOutput" runat="server" Text="Simulated Rank" OnClick="LBSimulatedOutput_Click"></asp:LinkButton>
                        </li>
                        <%-- <li class="nav-item">
                            <a class="nav-link not-active" href="#">Market Summary</a>
                        </li>--%>
                    </ul>
                </li>
            </ul>
        </nav>
    </div>
    <section class="content">
        <div class="scenario-table-outer-wrapper">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="scenario-table-inner-wrapper">
                        <div class="scenario-table-header-wrapper">
                            <div class="scenario-table-header">
                                <h1><span class="header-label">Plan List For Scenario: </span>
                                    <asp:Label Text="Change" runat="server" CssClass="header-text" ID="lblScenarioName" />
                                </h1>
                            </div>
                            <div class="scenario-table-controls">
                                <div class="filter">
                                    <telerik:RadComboBox ID="ddlState" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px" DropDownCssClass="textfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></telerik:RadComboBox>
                                    <telerik:RadComboBox ID="ddlSalesRegion" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesRegion_SelectedIndexChanged"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Sales Region" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px">
                                        <Localization AllItemsCheckedString="All Sales Region(s) Selected" ItemsCheckedString="Sales Region(s) Selected"
                                            CheckAllString="Select All" />
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        ControlToValidate="ddlSalesRegion" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                    <telerik:RadComboBox ID="ddlCounty" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select County" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px">
                                        <Localization AllItemsCheckedString="All County(s) Selected" ItemsCheckedString="County(s) Selected"
                                            CheckAllString="Select All" />
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        ControlToValidate="ddlCounty" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                    <telerik:RadComboBox ID="ddlPlanType" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPlanType_SelectedIndexChanged"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Plan Type" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px">
                                        <Localization AllItemsCheckedString="All Plan Type(s) Selected" ItemsCheckedString="Plan Type(s) Selected"
                                            CheckAllString="Select All" />
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqCountry"
                                        ControlToValidate="ddlPlanType" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="scenario-table-container" id="grdData_Div">
                            <div class="row">
                                <div class="col-4">
                                    Organization View                            
                            <div class="table-responsive" style="margin-top: 5px;" id="divOrgView">
                                <asp:GridView ID="grdOrgView" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                    DataKeyNames="Organization" CurrentSortField="Enrollment"
                                    CurrentSortDirection="ASC" AllowSorting="true" OnSorting="grdOrgView_Sorting" OnRowCreated="grdPlanView_RowCreated" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:BoundField HtmlEncode="False" DataField="Organization"
                                            SortExpression="Organization" HeaderText="Organization"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="AEPGrowth"
                                            SortExpression="AEPGrowth" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="25%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" DataField="Enrollment"
                                            SortExpression="Enrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="25%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" Visible="false" DataFormatString="{0:P1}" DataField="YoYGrowth" ItemStyle-HorizontalAlign="Right"
                                            SortExpression="YoYGrowth" HeaderText="YoY Growth"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                </div>
                                <div class="col-4">
                                    Plan View
                            <div class="table-responsive" style="margin-top: 5px;" id="divPlanView" onscroll="setScrollPosition(this.scrollTop);">
                                <asp:GridView ID="grdPlanView" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                    DataKeyNames="Plans" CurrentSortField="Enrollment"
                                    CurrentSortDirection="ASC" AllowSorting="true" OnSorting="grdPlanView_Sorting" OnRowCreated="grdPlanView_RowCreated" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rbSelect" runat="server" OnCheckedChanged="rbSelect_CheckedChanged" AutoPostBack="true" ClientIDMode="Static" />
                                                <asp:Label ID="LblPlans" Text='<%# Bind("Plans") %>' Visible="false" runat="server" />
                                                <asp:Label ID="LblBidID" Text='<%# Bind("BidID") %>' Visible="false" runat="server" />
                                                <asp:Label ID="LblPlanName" Text='<%# Bind("PlanName") %>' Visible="false" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HtmlEncode="False" DataField="Plans"
                                            SortExpression="Plans" HeaderText="Plan"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="AEPGrowth"
                                            SortExpression="AEPGrowth" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="25%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" DataField="Enrollment"
                                            SortExpression="Enrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="25%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:P1}" DataField="YoYGrowth" ItemStyle-HorizontalAlign="Right"
                                            SortExpression="YoYGrowth" Visible="false" HeaderText="YoY Growth"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                </div>
                                <div class="col-4">
                                    County View
                             <div class="table-responsive" style="margin-top: 5px;" id="divCountyView">
                                 <asp:GridView ID="grdCountyView" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                                     BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                     DataKeyNames="County" CurrentSortField="Enrollment"
                                     CurrentSortDirection="ASC" AllowSorting="true" OnSorting="grdCountyView_Sorting" OnRowCreated="grdPlanView_RowCreated" EmptyDataText="No Data Found">
                                     <HeaderStyle CssClass="GVFixedHeader" />
                                     <Columns>
                                         <asp:BoundField HtmlEncode="False" DataField="County"
                                             SortExpression="County" HeaderText="County"></asp:BoundField>
                                         <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="AEPGrowth"
                                             SortExpression="AEPGrowth" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="25%"></asp:BoundField>
                                         <asp:BoundField HtmlEncode="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" DataField="Enrollment"
                                             SortExpression="Enrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="25%"></asp:BoundField>
                                         <asp:BoundField HtmlEncode="False" DataFormatString="{0:P1}" ItemStyle-HorizontalAlign="Right" DataField="YoYGrowth"
                                             SortExpression="YoYGrowth" Visible="false" HeaderText="YoY Growth"></asp:BoundField>
                                     </Columns>
                                 </asp:GridView>
                             </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="bottom-controls">
                <asp:LinkButton runat="server" CssClass="control-link refresh-link" ID="lbRevert" OnClick="lbRevert_Click" ToolTip="Revert Changes">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left"><polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="control-link edit-link" ID="lbSaveAs" OnClick="lbSaveAs_Click" ToolTip="Save As">
                   <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor"  width="14" height="14" stroke-width="2">
                     <g>
	<path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8"/>
	<path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z"/>
	<polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	"/>
	<polyline class="st0" points="7,3.4 7,7 9.4,7 	"/>
</g>
                 </svg>
                </asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="control-link edit-link" ID="lbSave" OnClick="lbSave_Click" ToolTip="Save">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save"><path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="control-link download-link" ID="lbDownload" OnClick="lbDownload_Click" ToolTip="Download">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <asp:Button Text="Plan Comparison" runat="server" CommandArgument="PlanComparison2021" ID="MarketIntelC2L1" OnClientClick="PostToNewWindow();" OnClick="lbExternal_Click" CssClass="sim simbtn ml-auto download-link" />
                <asp:Button Text="Significant Benefits" runat="server" CommandArgument="SSBCI" Enabled="false" ID="ProductIntelC3L2" OnClientClick="PostToNewWindow();" OnClick="lbExternal_Click" CssClass="sim simbtn ml-auto download-link" />
                <asp:Button Text="Simulate" runat="server" ID="BtnSimulate" ValidationGroup="Simulate" OnClick="btnsimulate_Click" CssClass="sim btn ml-auto download-link" />
            </div>
        </div>
    </section>
    <div id="modalSaveAS" class="custom-modal">
        <!-- Modal content -->
        <div class="custom-modal-header">
            <span>Save Scenario As</span>
            <span class="far fa-times-circle closeModal1"></span>
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
                    <asp:Label ID="scenarioExists" runat="server" ValidationGroup="Saveas" Text="Scenario Name Already Exists" ForeColor="#CC0000" Visible="False"></asp:Label>
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
                    <asp:Button ID="BtnSaveAs" runat="server" OnClick="BtnSaveAs_Click" Text="Save" CssClass="btn form-control" ValidationGroup="Saveas" />
                </div>
            </div>
        </div>
        <script type="text/javascript">
            // Get the modal
            var modal2 = document.getElementById('modalSaveAS');

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
        </script>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal2 = document.getElementById('modalSaveAS');

        // Get the <span> element that closes the modal
        var span2 = document.getElementsByClassName("closeModal1")[0];

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
    </script>

</asp:Content>
