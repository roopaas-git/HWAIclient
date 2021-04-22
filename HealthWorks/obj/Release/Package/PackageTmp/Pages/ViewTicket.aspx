<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="ViewTicket.aspx.cs" Inherits="HealthWorks.Pages.ViewTicket" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #ffffff;
            padding: 3px;
            width: 200px;
            height: auto;
        }

        .RadComboBox {
            background-color: #f9f9f9;
        }
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ShowModalPopup(ModalBehaviour) {
            $find(ModalBehaviour).show();
        }

        function HideModalPopup(ModalBehaviour) {
            $find(ModalBehaviour).hide();
        }

    </script>

    <script src="../dist/js/jquery-2.1.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        .textarea {
            height: 30px !important;
            resize: none;
            width: 230px;
            padding-left: 12px;
        }

        @media screen and (max-width:767px) {
            .textarea {
                height: auto;
                width: 100%;
            }

            .ddl {
                height: auto;
                width: auto;
                line-height: normal;
            }
        }

        @media screen and (min-width:992px) {
            .textarea {
                height: auto;
                width: 100%;
            }

            .lbl {
                margin-left: auto;
            }

            .ddl {
                height: auto;
                width: auto;
                line-height: normal;
            }
        }

        @media screen and (min-width:768px) {
            .textarea {
                height: auto;
                width: 100%;
            }

            .ddl {
                height: auto;
                width: auto;
                line-height: normal;
            }
        }

        @media screen and (min-width:1200px) {
        }

        .lbl {
            margin-left: -30px;
        }

        .btnSearch {
            height: 30px !important;
            text-align: center;
        }

        .ddl {
            height: 30px !important;
            width: 200px;
            line-height: 24px;
        }
    </style>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#SupportLi').find('i').removeClass('fa-angle-right').addClass('fa-angle-down')
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <script>
        function setFocusToTextBox() {
            document.getElementById("<%= btnUpdate.ClientID %>").focus();
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfSate" />
            <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfUpDown" />
            <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfFocus" />
            <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfFocusStatus" />
            <div class="content">
                <div class="main-header" runat="server" id="divMainHeader">
                    <h2>
                        <asp:Label Text="View Tickets" runat="server" ID="pageName" /></h2>
                    <em>
                        <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                </div>
                <br />
                <div class="main-content">
                    <div class="form-group row">
                        <div class="col-sm-3" align="left">
                            <span class="date-text">Ticket Status:</span>
                            <telerik:RadComboBox ID="rdStatus" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Skin="Test" EnableEmbeddedSkins="false"
                                ClientIDMode="Static" Width="100%" EmptyMessage="Select ticket status">
                                <Localization CheckAllString="Select ALL" AllItemsCheckedString="ALL Tickets Checked"
                                    ItemsCheckedString="Account(s) Selected" />
                            </telerik:RadComboBox>
                        </div>
                        <div class="col-sm-3">
                            <span class="date-text">Priority: </span>
                            <telerik:RadComboBox ID="rdPriority" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Skin="Test" EnableEmbeddedSkins="false"
                                ClientIDMode="Static" Width="100%" EmptyMessage="Select ticket Priority">
                                <Localization CheckAllString="Select ALL" AllItemsCheckedString="ALL Tickets Checked"
                                    ItemsCheckedString="Account(s) Selected" />
                            </telerik:RadComboBox>
                        </div>
                        <div class="col-sm-2">
                            <br />
                            <asp:Button ID="btnViewTickets" runat="server" Text="View Tickets" CssClass="btn"
                                OnClick="btnViewTickets_Click" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="widget">
                                <div class="widget-header">
                                    <h3><i class="fas fa-ticket-alt"></i>Tickets</h3>
                                    <div class="btn-group widget-header-toolbar" id="divViewTicket" runat="server" clientidmode="Static">
                                        <a href="#" title="Focus" class="btn-borderless btn-focus" id="lbViewTicketFocus"
                                            runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                                class="btn-borderless btn-toggle-expand" id="lbViewTicket" runat="server" clientidmode="Static">
                                                <i class="fa fa-chevron-up"></i></a>
                                    </div>
                                </div>
                                <div class="widget-content">
                                    <div class="form-group row firstrow">
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtID" runat="server" placeholder="Search by Name   or Issue" ClientIDMode="Static"
                                                OnTextChanged="txtID_TextChanged" EnableViewState="true" CssClass="form-control"
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 control-label" align="right">
                                            <asp:Label ID="lblTotal" Text="Total:" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                            <asp:LinkButton ID="lblTotalCount1" Font-Size="Small" OnClick="lblTotalCount1_Click"
                                                Font-Underline="true" Font-Bold="true" runat="server"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 control-label" align="right">
                                            <asp:Label ID="lblOpen" Text="Open:" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                            <asp:LinkButton ID="lblOpenCount1" Font-Size="Small" OnClick="lblOpenCount1_Click"
                                                Font-Underline="true" Font-Bold="true" runat="server"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 control-label " align="right">
                                            <asp:Label ID="lblReopen" Text="ReOpen:" Font-Size="Small" Font-Bold="true" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lblReopenCnt1" Font-Bold="true" Font-Size="Small" OnClick="lblReopenCnt1_Click"
                                                Font-Underline="true" runat="server"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 control-label" align="right">
                                            <asp:Label ID="lblWP" Text="Wip:" Font-Bold="true" Font-Size="Small" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lblWPCnt11" Font-Bold="true" Font-Size="Small" Font-Underline="true"
                                                OnClick="lblWPCnt1_Click" runat="server"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-2 control-label" align="right">
                                            <asp:Label ID="lbClosed" Text="Closed:" Font-Bold="true" Font-Size="Small" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lblClosedCnt11" Font-Bold="true" Font-Size="Small" Font-Underline="true"
                                                runat="server" OnClick="lblClosedCnt11_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-12 table-responsive" align="left">
                                            <asp:GridView ID="Gvd_ViewOpenTicket" runat="server" AutoGenerateColumns="False"
                                                BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                                AllowSorting="True" OnRowDataBound="Gvd_ViewOpenTicket_RowDataBound" OnSorting="Gvd_ViewOpenTicket_Sorting"
                                                ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" EmptyDataRowStyle-HorizontalAlign="Center"
                                                OnPageIndexChanging="Gvd_ViewOpenTicket_PageIndexChanging" PageSize="10" AllowPaging="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="TicketID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbTicketID" runat="server" Text='<%#Bind("TicketID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbName" runat="server" Text='<%#Bind("FirstName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UserName" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbUserName" runat="server" Text='<%#Bind("UserName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ticket Raised Date" HeaderStyle-Font-Underline="true"
                                                        HeaderStyle-ForeColor="#585858" SortExpression="Ticket_Raised_Date">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lbDategrdheader" ToolTip="click here to sort by Ticket Raised Date " runat="server" Text="Ticket Raised Date"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbDate" runat="server" Text='<%#Bind("Ticket_Raised_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbCategory" runat="server" Text='<%#Bind("Cat_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbSubCategory" runat="server" Text='<%#Bind("SubCat_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbIssue" runat="server" Text='<%#Bind("Issue") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Priority" SortExpression="Ticket_Priority" HeaderStyle-Font-Underline="true"
                                                        HeaderStyle-ForeColor="#585858">
                                                         <HeaderTemplate>
                                                            <asp:Label ID="lbDategrdheader123" ToolTip="click here to sort by Priority" runat="server" Text="Priority"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbPriority" runat="server" Text='<%#Bind("Ticket_Priority") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbStatus" runat="server" Text='<%#Bind("Ticket_Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" Font-Bold="true" Text="Select" OnClick="lnkSelect_Click">
                                                         
                                                            </asp:LinkButton>
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
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="widget">
                                <div class="widget-header">
                                    <h3><i class="fas fa-edit"></i>Update</h3>
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
                                            <asp:Label ID="lbTicketID" runat="server" Text="Ticket ID:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtTicketID" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lbName" runat="server" Text="Name:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label1" runat="server" Text="Category:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label3" runat="server" Text="Sub Category:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSubcategory" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="lbIssue" runat="server" Text="Issue:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtIssue" runat="server" CssClass="form-control"
                                                ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lbCreateDate" runat="server" Text="Created Date:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCreateDate" runat="server" CssClass="form-control" BackColor="#F2F2F2"
                                                ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="lbStatus0" runat="server" Text="Status:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="custom-select">
                                                <asp:ListItem Text="Select" Value="Select" Selected="True" />
                                                <asp:ListItem Text="WIP" Value="WIP"></asp:ListItem>
                                                <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Label ID="lbComment" runat="server" Text="Comment:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtComment" runat="server" BackColor="White" CssClass="form-control"
                                                TabIndex="5" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredStatus" runat="server" ControlToValidate="ddlStatus"
                                                ErrorMessage="Please change the status" Font-Bold="True" ForeColor="Red" InitialValue="Select" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-8">
                                            <asp:Label ID="lblUserEmail" Text="" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnUpdate" Enabled="false" runat="server" Text="Update" CssClass="btn form-control"
                                                OnClientClick="if(Page_ClientValidate('Update')) ShowModalPopup('pload');"
                                                OnClick="btnUpdate_Click" ClientIDMode="Static" ValidationGroup="Update" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnCancel" Enabled="false" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                CssClass="btn form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ModalPopupExtender runat="server" PopupControlID="PanLoad" ID="ModalProgress"
        TargetControlID="PanLoad" BackgroundCssClass="modalBackground" BehaviorID="pload">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanLoad" runat="server" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                    <ProgressTemplate>
                        <div align="center">
                            <br />
                            <img src="../dist/Images/animated_figure.gif" alt="loading" title="loading" /><br />
                            <b>Processing ... </b>
                            <br />
                            <br />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
