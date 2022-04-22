<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="EnrollmentPlanList.aspx.cs" EnableEventValidation="false" Inherits="HealthWorks.Pages.EnrollmentPlanList" %>

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

        .tr-align-right {
            text-align: right;
        }
        /*.validator {
            color: red;
            margin-right: 30px;
        }*/

        /*Enrollment*/
                  
.card {
    z-index: 0;border: none;position: relative
}

#progressbar {   
    overflow: hidden;color: lightgrey;margin-bottom: 0.5rem;
}

#progressbar .itemcolor {
    color: #c1c1c1;
}

#progressbar li.active > strong > a {
    color: #5c276e !important;
}

#progressbar li {
    list-style-type: none;font-size: 12px;width: 25%;float: left;position: relative
}

#progressbar #CreateScenario:before {  
   content: "1";font-size: 15px;font-weight: 900;
}

#progressbar #DownloadPBP:before {   
   content: "2";font-size: 15px;font-weight: 900;
}

#progressbar #UploadPBP:before {   
    content: "3";font-size: 15px;font-weight: 900;
}

#progressbar #ResultsReady:before {   
    content: "4";font-size: 15px;font-weight: 900;
}

#progressbar li:before {
    width: 32px;height: 32px;line-height: 28px;display: block;font-size: 18px;color: #ffffff;background: #c1c1c1;border-radius: 50%;margin: 0 auto 10px auto;padding: 2px;
}
 #CreateScenario:after {
    content: '';height: 2px;background: lightgray;position: absolute;top: 16px;z-index: -1;
}

  #DownloadPBP:after {
    content: '';width: 100%;height: 2px;background: lightgray;position: absolute;left: -50%;top: 16px;z-index: -1;
}

   #UploadPBP:after {
    content: '';width: 100%;height: 2px;background: lightgray;position: absolute;left: -50%;top: 16px;z-index: -1;
}

    #ResultsReady:after {
    content: '';width: 100%;height: 2px;background: lightgray;position: absolute;left: -50%;top: 16px;z-index: -1
}

        #progressbar li.active:before, #progressbar li.active:after {
            background: #5c276e;
        }
        .validator {
            color: red;
            margin-right: 10px;
            margin-left: -5px;
        }

        .applyBtnFilter {
            width: 158px;
            font-size: 14px;
            color: #FFF;
            background: #5C276E !important;
        }

            .applyBtnFilter:hover {
                border-color: #5C276E !important;
                box-shadow: 0 3px 6px #5C276E;
                color: #fff;
            }

        .RadComboBoxDropDown .rcbHeader, .RadComboBoxDropDown .rcbFooter {
            padding: 0 !important;
        }

        .scenario-table-outer-wrapper .scenario-table-controls .filter {
            width: 96% !important;
        }

        .RadComboBox .rcbEmptyMessage {
            font-size: 12px !important;
        }

        .label {
            color: gray;
            font-size: smaller;
            margin-left: 1px;
            text-align: left;
            line-height: 13px;
        }

        .labeld {
            font-size: 14px;
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

        //function UploadFileChange(fileUpload) {
        //    if (fileUpload.value != '') {
        //        document.getElementById("Upload").click();
        //    }
        //}

        //function InitializeUploadDialog() {
        //    $("#UploadFile").click();
        //}

        //function changeCSS()
        //{
        //    $("#DownloadPBP").addClass("active");    
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
    <div class="container-fluid">
        <div class="row justify-content-center mt-0">
            <div class="col-md-12 text-center p-0 mt-3 mb-2">
                <div class="card px-0 pt-3 pb-0 mt-1 mb-1">
                    <div class="row">
                        <div class="col-md-12 mx-0">
                            <ul id="progressbar">
                                <li id="CreateScenario" class="active">
                                    <strong>
                                        <asp:LinkButton class="itemcolor" ID="LBScenarios" runat="server" Text="Create Scenario" OnClick="LBScenarios_Click"></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="DownloadPBP" runat="server" clientidmode="Static">
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBPlans" runat="server" Text="Plan Selection"></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="UploadPBP">
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBPlans_UploadPBP" runat="server" Text="UPLOAD PBP / CHANGE BENEFIT(S)" OnClick="LBQuickAccess_Click"></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="ResultsReady">
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBPlans_ResultsReady" runat="server" Text="Results Ready"></asp:LinkButton>
                                    </strong>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <section class="content">
        <div class="scenario-table-outer-wrapper">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="scenario-table-inner-wrapper">
                        <div class="scenario-table-header-wrapper">
                            <div class="scenario-table-header">
                                <h1><span class="header-label">Plan List For Scenario : </span>
                                    <asp:Label Text="Change" runat="server" CssClass="header-text" ID="lblScenarioName" />
                                </h1>
                            </div>
                            <div class="scenario-table-controls">
                                <div class="filter">
                                    <div visible="false" runat="server" >
                                    <asp:Label ID="Label1" runat="server" Text="Market" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlMarket" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMarket_SelectedIndexChanged"></telerik:RadComboBox>
                                    
                                    <asp:Label ID="Label2" runat="server" Text="Sub-Market" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlSubMarket" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubMarket_SelectedIndexChanged"></telerik:RadComboBox>
                                    </div>
                                    <asp:Label ID="Label3" runat="server" Text="State" CssClass="label"></asp:Label>
                                    <%--OnSelectedIndexChanged="ddlState_SelectedIndexChanged"--%>
                                    <telerik:RadComboBox ID="ddlState" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select State" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px">
                                        <Localization AllItemsCheckedString="All" ItemsCheckedString="State(s) Selected"
                                            CheckAllString="All" />
                                        <FooterTemplate>
                                            <asp:Button ID="btnApplyStateFilter" CssClass="applyBtnFilter" runat="server" Text="Apply" OnClick="btnApplyStateFilter_Click" />
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        ControlToValidate="ddlState" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                                                       
                                    <asp:Label ID="Label8" runat="server" Text="Sales Region" CssClass="label"></asp:Label>                                  
                                    <telerik:RadComboBox ID="ddlSalesRegion" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Sales Region" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px">
                                        <Localization AllItemsCheckedString="All" ItemsCheckedString="Sales Region(s) Selected"
                                            CheckAllString="All" />
                                        <FooterTemplate>
                                            <asp:Button ID="btnApplySalesRegionFilter" CssClass="applyBtnFilter" runat="server" Text="Apply" OnClick="btnApplySalesRegionFilter_Click" />
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        ControlToValidate="ddlSalesRegion" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                    
                                    
                                    <asp:Label ID="Label4" runat="server" Text="County" CssClass="label"></asp:Label>
                                    <%--OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged"--%>
                                    <telerik:RadComboBox ID="ddlCounty" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select County" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px">
                                        <Localization AllItemsCheckedString="All" ItemsCheckedString="County(s) Selected" CheckAllString="All" />
                                        <FooterTemplate>
                                            <asp:Button ID="btnApplyCountyFilter" CssClass="applyBtnFilter" runat="server" Text="Apply" OnClick="btnApplyCountyFilter_Click" />
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        ControlToValidate="ddlCounty" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>

                                    <div visible="false" runat="server" >  
                                    <asp:Label ID="Label5" runat="server" Text="Footprint" CssClass="label"></asp:Label>
                                    <%--OnSelectedIndexChanged="ddlFootprint_SelectedIndexChanged"--%>
                                    <telerik:RadComboBox ID="ddlFootprint" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Footprint" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px">
                                        <Localization AllItemsCheckedString="All" ItemsCheckedString="Footprint(s) Selected" CheckAllString="All" />
                                        <FooterTemplate>
                                            <asp:Button ID="btnApplyFootprintFilter" CssClass="applyBtnFilter" runat="server" Text="Apply" OnClick="btnApplyFootprintFilter_Click" />
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        ControlToValidate="ddlFootprint" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                                </div>                                                         
                                    <asp:Label ID="Label6" runat="server" Text="Plan Category" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlPlanCategory" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Plan Category" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px">
                                        <Localization AllItemsCheckedString="All" ItemsCheckedString="Plan Category(s) Selected" CheckAllString="All" />
                                        <FooterTemplate>
                                            <asp:Button ID="btnApplyPlanCategoryFilter" CssClass="applyBtnFilter" runat="server" Text="Apply" OnClick="btnApplyPlanCategoryFilter_Click" />
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        ControlToValidate="ddlPlanCategory" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                                   <div visible="false" runat="server" >                                      
                                    <asp:Label ID="Label9" runat="server" Text="Premium" CssClass="label"></asp:Label>
                                     <telerik:RadComboBox ID="ddlPremium" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Premium" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px">
                                        <Localization AllItemsCheckedString="All" ItemsCheckedString="Premium(s) Selected" CheckAllString="All" />
                                        <FooterTemplate>
                                            <asp:Button ID="btnApplyPremiumFilter" CssClass="applyBtnFilter" runat="server" Text="Apply" OnClick="btnApplyPremiumFilter_Click" />
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        ControlToValidate="ddlPremium" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>
                                                </div>                  
                                    <asp:Label ID="Label7" runat="server" Text="Plan Type" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlPlanType" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Plan Type" EnableEmbeddedSkins="false" Skin="Test" Style="width: 150px; white-space: normal;" DropDownWidth="160px">
                                        <Localization AllItemsCheckedString="All" ItemsCheckedString="Plan Type(s) Selected" CheckAllString="All" />
                                        <FooterTemplate>
                                            <asp:Button ID="btnApplyPlanTypeFilter" CssClass="applyBtnFilter" runat="server" Text="Apply" OnClick="btnApplyPlanTypeFilter_Click" />
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        ControlToValidate="ddlPlanType" runat="server" ValidationGroup="Simulate" CssClass="validator"
                                        ErrorMessage="*">  
                                    </asp:RequiredFieldValidator>        
                                </div>
                            </div>
                        </div>
                        <div class="scenario-table-container" id="grdData_Div">
                            <div class="row">
                                <div class="col-4">
                                    <%--Organization View        --%>
                                    <div class="table-responsive" style="margin-top: 5px;" id="divOrgView">
                                        <asp:GridView ID="grdOrgView" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                                            BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                            DataKeyNames="Organization" CurrentSortField="Enrollment"
                                            CurrentSortDirection="ASC" AllowSorting="true" OnSorting="grdOrgView_Sorting" OnRowCreated="grdPlanView_RowCreated" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:BoundField HtmlEncode="False" DataField="Organization"
                                                    SortExpression="Organization" HeaderText="Organization"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="AEPGrowth"
                                                    SortExpression="AEPGrowth" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="25%" HeaderStyle-CssClass="tr-align-right"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" DataField="Enrollment"
                                                    SortExpression="Enrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="25%" HeaderStyle-CssClass="tr-align-right"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" Visible="false" DataFormatString="{0:P1}" DataField="YoYGrowth" ItemStyle-HorizontalAlign="Right"
                                                    SortExpression="YoYGrowth" HeaderText="YoY Growth"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <%--Plan View--%>
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
                                                    SortExpression="AEPGrowth" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="25%" HeaderStyle-CssClass="tr-align-right"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" DataField="Enrollment"
                                                    SortExpression="Enrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="25%" HeaderStyle-CssClass="tr-align-right"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:P1}" DataField="YoYGrowth" ItemStyle-HorizontalAlign="Right"
                                                    SortExpression="YoYGrowth" Visible="false" HeaderText="YoY Growth" HeaderStyle-CssClass="tr-align-right"></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <%--County View--%>
                                    <div class="table-responsive" style="margin-top: 5px;" id="divCountyView">
                                        <asp:GridView ID="grdCountyView" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                                            BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                            DataKeyNames="County" CurrentSortField="Enrollment"
                                            CurrentSortDirection="ASC" AllowSorting="true" OnSorting="grdCountyView_Sorting" OnRowCreated="grdPlanView_RowCreated" EmptyDataText="No Data Found">
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <Columns>
                                                <asp:BoundField HtmlEncode="False" DataField="State"
                                                    SortExpression="State" HeaderText="State"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataField="County"
                                                    SortExpression="County" HeaderText="County"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" DataField="AEPGrowth"
                                                    SortExpression="AEPGrowth" HeaderText="Pre-AEP Enrollment" ItemStyle-Width="25%" HeaderStyle-CssClass="tr-align-right"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" DataField="Enrollment"
                                                    SortExpression="Enrollment" HeaderText="Post-AEP Enrollment" ItemStyle-Width="25%" HeaderStyle-CssClass="tr-align-right"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:P1}" ItemStyle-HorizontalAlign="Right" DataField="YoYGrowth"
                                                    SortExpression="YoYGrowth" Visible="false" HeaderText="YoY Growth" HeaderStyle-CssClass="tr-align-right"></asp:BoundField>
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
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button Text="Next" style=" width: 75px; " runat="server" ID="btnSimulate" ValidationGroup="Simulate" OnClick="btnSimulate_Click" CssClass="sim btn ml-auto download-link"  ClientIDMode="Static" />

                <%--
                <asp:Button Text="Plan Comparison" runat="server" CommandArgument="PlanComparisonDownload2021" ID="UnlimitedIntelC3L2" OnClientClick="PostToNewWindow();" OnClick="lbExternal_Click" CssClass="sim simbtn ml-auto download-link" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button Text="Benefit Scorecard" runat="server" CommandArgument="BenefitScorecard" ID="ScoreboardC1L1" OnClientClick="PostToNewWindow();" OnClick="lbExternal_Click" CssClass="sim simbtn ml-auto download-link" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              
                <asp:FileUpload ID="UploadFile" ClientIDMode="Static" Style="display: none" runat="server" CssClass="sim simbtn ml-auto download-link" />               
                <asp:Button Text="Download PBP" runat="server" ID="Download" ValidationGroup="Simulate" CssClass="sim btn ml-auto download-link"  OnClick="Download_Click" ClientIDMode="Static"  OnClientClick="changeCSS();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;       
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:Button Text="Upload PBP" ClientIDMode="Static" runat="server" OnClick="InitializeUpload_Click" ID="InitializeUpload" ValidationGroup="Simulate" CssClass="sim btn ml-auto download-link" />
                        <asp:Button Text="Trigger Upload" ClientIDMode="Static" Style="display: none" runat="server" ID="Upload" OnClick="Upload_Click" CssClass="sim btn ml-auto download-link" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Upload" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </section>
</asp:Content>



