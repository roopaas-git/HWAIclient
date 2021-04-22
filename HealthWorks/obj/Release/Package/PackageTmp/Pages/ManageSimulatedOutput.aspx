<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ManageSimulatedOutput.aspx.cs" Inherits="HealthWorks.Pages.ManageSimulatedOutput" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <style>
        .validator {
            color: red;
            margin-right: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="tab-nav-container">
        <nav class="tab-nav nav-light navbar">
            <ul class="nav">
                <li class="nav-item tab-nav-item plan-nav">
                    <a class="nav-link" href="#">Plan</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link  active" ID="LBScenarios" runat="server" Text="Scenario List" OnClick="LBScenarios_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBPlans" runat="server" Text="Plan List" OnClick="LBPlans_Click"></asp:LinkButton>
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
                            <asp:LinkButton CssClass="nav-link active" ID="LBQuickAccess" runat="server" Text="Quick Access" OnClick="LBQuickAccess_Click"></asp:LinkButton>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="content">
                <div class="scenario-table-outer-wrapper">
                    <div class="scenario-table-inner-wrapper">
                        <div class="scenario-table-header-wrapper">
                            <div class="scenario-table-header">
                                <h1><span class="header-label">Simulated Enrollments For Scenario: </span>
                                    <asp:Label Text="Change" runat="server" CssClass="header-text" ID="lblScenarioName" />
                                </h1>
                            </div>
                            <div class="scenario-table-controls">
                                <div class="scenario-table-title">
                                    <div class="choosen-title">
                                        <span class="header-label">Simulated For Plan: </span>
                                        <asp:Label Text="" CssClass="header-text" runat="server" ID="lblBid_Id" />
                                    </div>
                                </div>
                                <div class="filter">
                                    <telerik:RadComboBox ID="ddlState" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px" DropDownCssClass="textfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></telerik:RadComboBox>
                                    <telerik:RadComboBox ID="ddlSalesRegion" runat="server" CausesValidation="true" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesRegion_SelectedIndexChanged"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Sales Region" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px">
                                        <Localization AllItemsCheckedString="All Sales Region(s) Selected" ItemsCheckedString="Sales Region(s) Selected"
                                            CheckAllString="Select All" />
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Simulate"
                                        ControlToValidate="ddlSalesRegion" runat="server" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                    <telerik:RadComboBox ID="ddlCounty" runat="server" CheckBoxes="true" CausesValidation="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select County" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px">
                                        <Localization AllItemsCheckedString="All County(s) Selected" ItemsCheckedString="County(s) Selected"
                                            CheckAllString="Select All" />
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Simulate"
                                        ControlToValidate="ddlCounty" runat="server" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                    <telerik:RadComboBox ID="ddlPlanType" runat="server" CheckBoxes="true" CausesValidation="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPlanType_SelectedIndexChanged"
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
                                    County View
                             <div class="table-responsive" style="margin-top: 5px;" id="divCountyView">
                                 <asp:GridView ID="grdCountyView" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                                     BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                     DataKeyNames="County" CurrentSortField="SimulatedEnrollment"
                                     CurrentSortDirection="ASC" AllowSorting="true" OnSorting="grdCountyView_Sorting" OnRowCreated="grdPlanView_RowCreated" EmptyDataText="No Data Found">
                                     <Columns>
                                         <asp:BoundField HtmlEncode="False" DataField="County"
                                             SortExpression="County" HeaderText="County"></asp:BoundField>
                                         <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="PreAEPEnrollment"
                                             SortExpression="PreAEPEnrollment" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                         <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="CurrentEnrollment"
                                             SortExpression="CurrentEnrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                         <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="SimulatedEnrollment"
                                             SortExpression="SimulatedEnrollment" HeaderText="Simulated Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                     </Columns>
                                 </asp:GridView>
                             </div>
                                </div>
                                <div class="col-4">
                                    Plan View
                            <div class="table-responsive" style="margin-top: 5px;" id="divPlanView">
                                <asp:GridView ID="grdPlanView" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                    DataKeyNames="Plans" CurrentSortField="SimulatedEnrollment"
                                    CurrentSortDirection="ASC" AllowSorting="true" OnSorting="grdPlanView_Sorting" OnRowCreated="grdPlanView_RowCreated" EmptyDataText="No Data Found" OnRowDataBound="grdPlanView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HtmlEncode="False" DataField="Plans"
                                            SortExpression="Plans" HeaderText="Plan"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="PreAEPEnrollment"
                                            SortExpression="PreAEPEnrollment" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="CurrentEnrollment"
                                            SortExpression="CurrentEnrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="SimulatedEnrollment"
                                            SortExpression="SimulatedEnrollment" HeaderText="Simulated Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LblHightlightRow" runat="server" Visible="false" Text='<%# Bind("HightlightRow") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                </div>
                                <div class="col-4">
                                    Organization View                            
                            <div class="table-responsive" style="margin-top: 5px;" id="divOrgView">
                                <asp:GridView ID="grdOrgView" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                    DataKeyNames="Organization" CurrentSortField="SimulatedEnrollment"
                                    CurrentSortDirection="ASC" AllowSorting="true" OnSorting="grdOrgView_Sorting" OnRowCreated="grdPlanView_RowCreated" EmptyDataText="No Data Found" OnRowDataBound="grdPlanView_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HtmlEncode="False" DataField="Organization"
                                            SortExpression="Organization" HeaderText="Organization"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="PreAEPEnrollment"
                                            SortExpression="PreAEPEnrollment" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="CurrentEnrollment"
                                            SortExpression="CurrentEnrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="SimulatedEnrollment"
                                            SortExpression="SimulatedEnrollment" HeaderText="Simulated Enrollment" ItemStyle-Width="20%"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LblHightlightRow" runat="server" Visible="false" Text='<%# Bind("HightlightRow") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
