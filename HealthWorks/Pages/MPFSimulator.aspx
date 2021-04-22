<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="MPFSimulator.aspx.cs" Inherits="HealthWorks.Pages.MPFSimulator" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <style type="text/css">
        .table tr:first-child th {
            font-size: 12px !important;
        }
    </style>
    <script type="text/javascript">
        function Validate() {
           <%-- var txtStarRating = document.getElementById("<%=txtStarRating.ClientID%>").value;
            var BidId = document.getElementById("<%=txtBidId.ClientID%>").value;


            if (txtStarRating > 5) {
                return false;
            }--%>
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="scenario-table-outer-wrapper">
            <div class="scenario-table-inner-wrapper">
                <div class="scenario-table-header-wrapper">
                    <div class="scenario-table-header">
                        <h1><span class="header-label">MPF Ranking Simulator </span>
                        </h1>
                    </div>
                    <div class="scenario-table-controls">
                        <div class="filter">
                            <asp:DropDownList runat="server" ID="ddlPlanType" CssClass="custom-select country-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPlanType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <telerik:RadComboBox ID="ddlState" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px" DropDownCssClass="textfont" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></telerik:RadComboBox>
                            <telerik:RadComboBox ID="ddlCounty" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px" DropDownCssClass="textfont" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged"></telerik:RadComboBox>
                            <telerik:RadComboBox ID="ddlSnpType" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSnpType_SelectedIndexChanged"
                                EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select SNP Type" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px">
                                <Localization AllItemsCheckedString="All SNP type Selected" ItemsCheckedString="SNP type(s) Selected"
                                    CheckAllString="Select All" />
                            </telerik:RadComboBox>
                            <asp:DropDownList runat="server" ID="ddlMPFSort" CssClass="custom-select country-select" AutoPostBack="true" Width="25%" OnSelectedIndexChanged="ddlMPFSort_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="scenario-table-container" id="grdData_Div">
                    <div class="table-responsive" id="dvScroll">
                        <asp:GridView ID="grdData" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" Width="100%"
                            BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                            AutoGenerateSelectButton="true" OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowDataBound="grdData_RowDataBound">
                            <HeaderStyle CssClass="GVFixedHeader" />
                            <Columns>
                                <asp:TemplateField HeaderText="Bid Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbBidId" runat="server" Text='<%# Bind("BidId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Organization Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMarketingName" runat="server" Text='<%# Bind("[Marketing Name]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SNP Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSnpType" runat="server" Text='<%# Bind("[SNPType]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plan Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPlanName" runat="server" Text='<%# Bind("Plan_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Premium" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPremium" runat="server" Text='<%# Bind("Premium") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Star Rating" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbStartRating" runat="server" Text='<%# Bind("Star_Rating") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Health Deductible" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbHealthDeductable" runat="server" Text='<%# Bind("Health_Deductable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Drug Deductible" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDrugDeductable" runat="server" Text='<%# Bind("Drug_Deductable") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MOOP-IN&OUT" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMoopInOut" runat="server" Text='<%# Bind("MOOP_InOut") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MOOP-INN" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMoopIn" runat="server" Text='<%# Bind("MOOP_InNetwork") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MOOP-OON" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMoopOON" runat="server" Text='<%# Bind("MOOP_OON") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbOriginalRank" runat="server" Text='<%# Bind("OriginalRank") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Simulated Rank" ControlStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSimulatedRank" runat="server" Text='<%# Bind("SimulatedRank") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="bottom-controls">
                <asp:LinkButton Text="" runat="server" CssClass="control-link refresh-link" ID="lbRevert" OnClick="lbRevert_Click" ToolTip="Reset Changes">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left"><polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" CssClass="control-link edit-link not-active" ID="lbSaveAs" ToolTip="Save As">
                   <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor" width="14" height="14" stroke-width="2">
                        <g>
                            <path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8" />
                            <path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z" />
                            <polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	" />
                            <polyline class="st0" points="7,3.4 7,7 9.4,7 	" />
                        </g>
                    </svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" CssClass="control-link edit-link not-active" ID="lbSave" ToolTip="Save">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save"><path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" CssClass="control-link download-link" ID="lbDownload" OnClick="lbDownload_Click" ToolTip="Download">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
                </asp:LinkButton>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="widget">
                    <div class="widget-header">
                        <h3><i class="fas fa-edit"></i>
                            <asp:Label Text="Add Plan" runat="server" ID="lblPlanHeader" /></h3>
                        <div class="btn-group widget-header-toolbar" id="divViewTicket_Update" runat="server"
                            clientidmode="Static">
                            <a href="#" title="Focus" class="btn-borderless btn-focus" id="lbViewTicketUpdateFocus"
                                runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                    class="btn-borderless btn-toggle-expand" id="lbViewTicket_Update" runat="server"
                                    clientidmode="Static"><i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-content">
                        <div class="form-group row firstrow">
                            <div class="col-sm-2">
                                <asp:Label ID="lblBidId" runat="server" Text="Bid Id:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtBidId" runat="server" CssClass="form-control" OnTextChanged="txtBidId_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBidId"
                                    ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup="Simulate"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="customBidIdValidator" ErrorMessage="use _ not -" Font-Size="10px" ForeColor="Red" ControlToValidate="txtBidId" runat="server" OnServerValidate="BidID_ServerValidate" ValidationGroup="Simulate" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="lblMarketName" runat="server" Text="Organization Name:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtMarketName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMarketName"
                                    ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup="Simulate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label2" runat="server" Text="Plan Name:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtPlanName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPlanName"
                                    ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup="Simulate"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="Label1" runat="server" Text="Premium:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtPremium" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPremium"
                                    ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup="Simulate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtPremium" ValidationExpression="^(?:\d{1,9})?(?:\.\d{1,9})?$"
                                    Display="Static" EnableClientScript="true" ErrorMessage="Numbers only" ValidationGroup="Simulate" runat="server" ForeColor="Red" Font-Size="12px" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-2">
                                <asp:Label ID="Label3" runat="server" Text="Star Rating:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtStarRating" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStarRating"
                                    ErrorMessage="*" Font-Bold="True" ForeColor="Red" ValidationGroup="Simulate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtStarRating" ValidationExpression="^(?:\d{1,9})?(?:\.\d{1,9})?_?$"
                                    Display="Static" EnableClientScript="true" ErrorMessage="Enter from 0 to 5" runat="server" ValidationGroup="Simulate" ForeColor="Red" Font-Size="12px" />
                                <asp:CustomValidator ID="CustomeValidationStarRating" ErrorMessage="Enter from 0 to 5" ForeColor="Red" ControlToValidate="txtStarRating" runat="server" OnServerValidate="StarRating_ServerValidate" ValidationGroup="Simulate" Font-Size="8px" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="lblHealthDeductable" runat="server" Text="Health Deductible:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtHealthDeductable" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtHealthDeductable" ValidationExpression="^(?:\d{1,9})?(?:\.\d{1,9})?_?$"
                                    Display="Static" EnableClientScript="true" ErrorMessage="Numbers only" ValidationGroup="Simulate" runat="server" ForeColor="Red" Font-Size="12px" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-2">
                                <asp:Label ID="lblDrugDeductible" runat="server" Text="Drug Deductible:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtDrugDeductible" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtDrugDeductible" ValidationExpression="^(?:\d{1,9})?(?:\.\d{1,9})?_?$"
                                    Display="Static" EnableClientScript="true" ErrorMessage="Numeric only" runat="server" ValidationGroup="Simulate" ForeColor="Red" Font-Size="12px" />
                            </div>

                            <div class="col-sm-2">
                                <asp:Label ID="lblMoopInOut" runat="server" Text="MOOP-IN&OUT:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtMoopInOut" runat="server" CssClass="form-control"
                                    ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMoopInOut" ValidationExpression="^(?:\d{1,9})?(?:\.\d{1,9})?_?$"
                                    Display="Static" EnableClientScript="true" ErrorMessage="Numeric only" runat="server" ForeColor="Red" Font-Size="12px" ValidationGroup="Simulate" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-2">
                                <asp:Label ID="lblMoopInNetWork" runat="server" Text="MOOP-INN:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtMoopInNetWork" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtMoopInNetWork"
                                    ValidationExpression="^(?:\d{1,9})?(?:\.\d{1,9})?_?$" Display="Static" EnableClientScript="true" ErrorMessage="Numeric only"
                                    runat="server" ValidationGroup="Simulate" ForeColor="Red" Font-Size="12px" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="Label4" runat="server" Text="MOOP-OON:"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtMoopOutNetWork" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtMoopOutNetWork" ValidationExpression="^(?:\d{1,9})?(?:\.\d{1,9})?_?$"
                                    Display="Static" EnableClientScript="true" ErrorMessage="Numeric only" runat="server" ValidationGroup="Simulate" ForeColor="Red" Font-Size="12px" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-2">
                            </div>
                            <div class="col-sm-4">
                                <asp:Label ID="lblSnpType" runat="server" Visible="false" Text="0"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                            </div>
                            <div class="col-sm-4">
                                <asp:Label Text="Bid Id all ready Present!" runat="server" Font-Bold="true" ForeColor="Red" Visible="false" ID="lblDuplicateBidId" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-8">
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnSimulate" runat="server" Text="Simulate" CssClass="btn form-control"
                                    ClientIDMode="Static" ValidationGroup="Simulate" OnClick="btnSimulate_Click" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    CssClass="btn form-control" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
