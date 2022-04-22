<%@ Page Title="HealthWorksAI - Provider Network Data Pull" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="ProviderNetworkData.aspx.cs" Inherits="HealthWorks.Pages.ProviderNetworkData" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <style>
        .col-sm-3 {
            -ms-flex: 0 0 25%;
            flex: 0 0 25%;
            max-width: 25%;
        }

        .label {
            color: gray;
            font-size: smaller;
            margin-left: 1px;
            text-align: left;
        }


        table {
            border-collapse: collapse;
        }

        td, th {
            border: 1px solid black;
            padding: 3px;
        }

        .label1 {
            font-size: 14px;
        }

        .labeld {
            font-size: 14px;
            font-weight: Bold;
        }

        .label2 {
            font-size: 14px;
            font-weight: Bold;
        }

        .btn {
            background-color: #9776A2 !important;
            font-weight: 500;
            border-radius: 2px;
            color: #fff;
            transition: all ease 0.3s;
            padding: 0px 5px;
            font-size: 12px;
            color: Black;
            font-weight: Bold;
        }

            .btn:hover {
                /* background-color: #E5EDD5 !important; */
                border-color: #9776A2 !important;
                box-shadow: 0 3px 6px #5c276e;
                color: #fff;
            }

            .btn.disabled, .btn:disabled {
                opacity: .65;
                box-shadow: 0 0px 0px #5c276e;
                color: Black;
            }
    </style>
    <script language="javascript" type="text/javascript">
        function ShowModalPopup(ModalBehaviour) {
            $('a').css({
                color: 'black'
            });
            $find(ModalBehaviour).show();
        }

        function HideModalPopup(ModalBehaviour) {
            $find(ModalBehaviour).hide();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="scenario-table-outer-wrapper">
            <div class="scenario-table-inner-wrapper">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="scenario-table-header-wrapper">

                            <div class="scenario-table-header">
                                <h1><span class="header-label">Provider Network Comparison</span>
                                </h1>
                            </div>
                            <div class="scenario-table-controls">
                                <div class="filter">
                                    <asp:Label ID="Label1" runat="server" Text="Market" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlMarket" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 90px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlMarket_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <asp:Label ID="Label2" runat="server" Text="Sub-Market" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlSubMarket" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 90px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSubMarket_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <asp:Label ID="Label3" runat="server" Text="State" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlState" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 90px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <asp:Label ID="Label4" runat="server" Text="County" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlCounty" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 90px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <%--<asp:Label ID="Label5" runat="server" Text="Zipcode" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlZipcode" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Style="width: 90px; white-space: normal;" AutoPostBack="true" OnSelectedIndexChanged="ddlZipcode_SelectedIndexChanged"
                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Zipcode" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="160px">
                                        <Localization AllItemsCheckedString="All Zipcode(s) Selected" ItemsCheckedString="Zipcode(s) Selected"
                                            CheckAllString="Select All" />
                                    </telerik:RadComboBox>--%>
                                    <asp:Label ID="Label6" runat="server" Text="Footprint" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlFootprint" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 90px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFootprint_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <asp:Label ID="Label5" runat="server" Text="Plan Category" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlPlanCategory" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 90px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPlanCategory_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <asp:Label ID="Label7" runat="server" Text="Plan Type" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlPlanType" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 90px; white-space: normal;" DropDownWidth="160px" DropDownCssClass="textfont" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPlanType_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="filter" style="margin-left: 8%;">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label8" runat="server" Text="Base Plan Id" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlPlanA" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 300px; white-space: normal;" DropDownCssClass="textfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPlanA_SelectedIndexChanged"></telerik:RadComboBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label9" runat="server" Text="Comparison Plan Id" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlPlanB" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 300px; white-space: normal;" DropDownCssClass="textfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPlanB_SelectedIndexChanged"></telerik:RadComboBox>
                                </div>
                                <div class="filter" style="margin-left: 8%;">
                                    <asp:Label ID="Label10" runat="server" Text="Base Network Id" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlNetworkA" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 300px; white-space: normal;" DropDownCssClass="textfont" runat="server" AutoPostBack="true"></telerik:RadComboBox>
                                    <asp:Label ID="Label11" runat="server" Text="Comparison Network Id" CssClass="label"></asp:Label>
                                    <telerik:RadComboBox ID="ddlNetworkB" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" Style="width: 300px; white-space: normal;" DropDownCssClass="textfont" runat="server" AutoPostBack="true"></telerik:RadComboBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit Request" Height="30px" CssClass="btn"
                                        ClientIDMode="Static" Enabled="false" OnClientClick="ShowModalPopup('pload');" OnClick="btnSubmit_Click" />
                                </div>
                            </div>

                        </div>
                        <div class="scenario-table-container" id="grdCount">
                            <div class="row" style="text-align: center">
                                <table style="width: 70%; margin-left: 12%;">
                                    <tr>
                                        <td style="width: 15%; height: 40px;"></td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="LblBaseTotal" runat="server" Text="" CssClass="label2"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="LblCompareTotal" runat="server" Text="Comparison Plan Total" CssClass="label2"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="LblBaseUnique" runat="server" Text="Base Plan Unique" CssClass="label2"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="LblCompareUnique" runat="server" Text="Comparison Plan Unique" CssClass="label2"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="Label16" runat="server" Text="Common" CssClass="label2"></asp:Label>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td style="width: 15%; height: 40px; text-align: left; padding-left: 20px;">
                                            <asp:Label ID="Label14" runat="server" Text="PCP Count" CssClass="label2"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="lbPCPATotal" runat="server" CssClass="label1"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="lbPCPBTotal" runat="server" CssClass="label1"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="lbPCPA" runat="server" CssClass="label1"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="lbPCPB" runat="server" CssClass="label1"></asp:Label>
                                        </td>
                                        <td style="width: 15%; height: 40px;">
                                            <asp:Label ID="lbPCPC" runat="server" CssClass="label1"></asp:Label>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td style="width: 15%; height: 40px; text-align: left; padding-left: 20px;">
                                            <asp:Label ID="Label13" runat="server" Text="Specialist Count" CssClass="label2"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbSpecialistATotal" runat="server" CssClass="label1"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbSpecialistBTotal" runat="server" CssClass="label1"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbSpecialistA" runat="server" CssClass="label1"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbSpecialistB" runat="server" CssClass="label1"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbSpecialistC" runat="server" CssClass="label1"></asp:Label>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td style="width: 15%; height: 40px; text-align: left; padding-left: 20px;">
                                            <asp:Label ID="Label12" runat="server" Text="Hospital Count" CssClass="label2"></asp:Label></td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbHospitalATotal" runat="server" CssClass="label1"></asp:Label></td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbHospitalBTotal" runat="server" CssClass="label1"></asp:Label></td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbHospitalA" runat="server" CssClass="label1"></asp:Label></td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbHospitalB" runat="server" CssClass="label1"></asp:Label></td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lbHospitalC" runat="server" CssClass="label1"></asp:Label></td>


                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="scenario-table-container" id="grdData_Div">
                    <br />
                    <br />
                    <span class="labeld">Download Data</span>
                    <br />
                    <br />
                    <div class="table-responsive" id="dvScroll">
                        <asp:GridView ID="grdData" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="RequestId"
                            BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                            OnSelectedIndexChanged="grdData_SelectedIndexChanged" OnRowDataBound="grdData_RowDataBound" OnRowCommand="grdData_RowCommand1" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                            <Columns>
                                <asp:CommandField SelectText="Select" ControlStyle-ForeColor="Blue" ShowSelectButton="True" />
                                <asp:TemplateField HeaderText="State" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRequestid" runat="server" Text='<%# Bind("[RequestID]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lbState" runat="server" Text='<%# Bind("[State]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="County">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPlanName" runat="server" Text='<%# Bind("County") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Network Ids" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNetwork_id" runat="server" Text='<%# Bind("[Network_id]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Base Organization">
                                    <ItemTemplate>
                                        <asp:Label ID="lbBaseOrganization" runat="server" Text='<%# Bind("[BaseOrganization]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comparison Organization">
                                    <ItemTemplate>
                                        <asp:Label ID="lbComparisonOrganization" runat="server" Text='<%# Bind("[ComparisonOrganization]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bid Ids">
                                    <ItemTemplate>
                                        <asp:Label ID="lbBid_id" runat="server" Text='<%# Bind("[Bid_id]") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Zipcode">
                                    <ItemTemplate>
                                        <div style="word-wrap: break-word; width: 400px;">
                                            <asp:Label ID="lbZipcode" runat="server" Text='<%# Bind("Zipcode") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Date Time EST">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRequestDate" runat="server" Text='<%# Bind("RequestDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lbIscreated" runat="server" Visible="false" Text='<%# Bind("[IsCreated]") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkDownload" runat="server" ForeColor="Gray" CausesValidation="False" CommandArgument='<%# Eval("RequestId") %>'
                                            CommandName="Download" Text="Download" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="bottom-controls">
            </div>
        </div>
    </section>
    <asp:ModalPopupExtender runat="server" PopupControlID="PanLoad" ID="ModalProgress"
        TargetControlID="PanLoad" BackgroundCssClass="modalBackground" BehaviorID="pload">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanLoad" runat="server" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div align="center">
                    <br />
                    <img src="../dist/img/animated_figure.gif" alt="loading" title="loading" /><br />
                    <b>Processing ... </b>
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
