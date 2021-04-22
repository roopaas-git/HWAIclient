<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="CreateTicket.aspx.cs" Inherits="HealthWorks.Pages.CreateTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        .textarea.form-control {
            height:300px !important;
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
    <style type="text/css">
        .textarea {
            height: 30px !important;
            resize: none;
            width: 230px;
            padding-left: 11px;
        }

        .textarea1 {
            height: 30px !important;
            resize: none;
            width: 315px;
        }


        @media screen and (max-width:767px) {
            .textarea {
                height: auto;
                width: 100%;
            }

            .textarea1 {
                height: auto;
                width: 100%;
            }
        }

        @media screen and (min-width:992px) {
            .textarea {
                height: auto;
                width: 100%;
            }

            .textarea1 {
                height: auto;
                width: 100%;
            }
        }

        @media screen and (min-width:768px) {
            .textarea {
                height: auto;
                width: 100%;
            }

            .textarea1 {
                height: auto;
                width: 100%;
            }
        }

        @media screen and (min-width:1200px) {
        }
    </style>
    <style type="text/css">
        .lblLoading {
            margin-left: 34px;
        }

        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 5;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin-left: 450px;
            margin-top: 170px;
            margin-bottom: 630px;
            padding: 6px;
            height: 120px;
            width: 160px;
            background-color: Gray;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                margin-left: 25px;
                height: 100px;
                width: 100px;
            }
    </style>
    <script src="../dist/js/jquery-2.1.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#SupportLi').find('i').removeClass('fa-angle-right').addClass('fa-angle-down')
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <script type="text/javascript">
        function setFocusToTextBox() {
            document.getElementById("<%= btnReopen.ClientID %>").focus();
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
        <ContentTemplate>

            <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfSate" />
            <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfUpDown" />
            <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfFocus" />
            <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfFocusStatus" />
            <div class="content">
                <div class="main-header" runat="server" id="divMainHeader">
                    <h2>
                        <asp:Label Text="Create Tickets" runat="server" ID="pageName" /></h2>
                    <em>
                        <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                </div>
                <br />
                <div class="main-content">
                    <div class="form-group row">
                        <div class="col-sm-12">
                            <div class="widget">
                                <div class="widget-header">
                                    <h3><i class="fas fa-ticket-alt"></i>Create Tickets</h3>
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
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label1" runat="server" Text="Category:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="custom-select" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory"
                                                ErrorMessage="*" Font-Bold="True" ForeColor="Red" InitialValue="Select" ValidationGroup="Create"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class=" form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label2" runat="server" Text="Sub Category:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="custom-select" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSubCategory"
                                                ErrorMessage="*" Font-Bold="True" ForeColor="Red" InitialValue="Select" ValidationGroup="Create"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-2"></div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="lbIssue" runat="server" Text="Issue:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <%--<asp:TextBox ID="txtIssue" runat="server" CssClass="form-control">
                                            </asp:TextBox>--%>
                                            <ig:WebTextEditor ID="txtIssue" runat="server" Text="" TextMode="MultiLine"
                                                CssClass="form-control txtHeight textbox" ClientIDMode="Static">
                                            </ig:WebTextEditor>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIssue"
                                                ErrorMessage="*" ForeColor="Red" Font-Bold="True" ValidationGroup="Create"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-2"></div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="lbPriority" runat="server" Text="Priority:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlPriority" runat="server" CssClass="custom-select">
                                                <asp:ListItem Selected="True" Text="Select" Value="Select" />
                                                <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                                <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                                                <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPriority"
                                                ErrorMessage="*" Font-Bold="True" ForeColor="Red" InitialValue="Select" ValidationGroup="Create"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-2"></div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button Text="Submit" runat="server" ID="btnSubmit" CssClass="btn form-control"
                                                OnClientClick="if(Page_ClientValidate('Create')) ShowModalPopup('pload');" ClientIDMode="Static"
                                                ValidationGroup="Create" OnClick="btnSubmit_Click" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button Text="Cancel" runat="server" ID="btnCancel" CssClass="btn form-control"
                                                OnClick="btnCancel_Click" ClientIDMode="Static" />
                                        </div>
                                        <div class="col-sm-1"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" hidden>
                        <div class="col-sm-12">
                            <div class="widget">
                                <div class="widget-header">
                                    <h3><i class="fas fa-search"></i>Search Tickets</h3>
                                    <div class="btn-group widget-header-toolbar" id="divSearchTickets" runat="server"
                                        clientidmode="Static">
                                        <a href="#" title="Focus" class="btn-borderless btn-focus" id="lbSearchTicketFocus"
                                            runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                                class="btn-borderless btn-toggle-expand" id="lbSearchTickets" runat="server"
                                                clientidmode="Static"><i class="fa fa-chevron-up"></i></a>
                                    </div>
                                </div>
                                <div class="widget-content">
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <span class="date-text">Start Date:</span>
                                            <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate"></asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="date-text">End Date:</span>
                                            <asp:TextBox ID="txtEndDate" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate"></asp:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1">
                                        </div>
                                        <div class="col-sm-3">
                                            <br />
                                            <asp:Button Text="Search" runat="server" ID="Button1" CssClass="btn form-control"
                                                ValidationGroup="Search" ClientIDMode="Static" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-4">
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtEndDate"
                                                ValidationGroup="Search" ControlToValidate="txtStartDate" ErrorMessage="*" Operator="LessThanEqual"
                                                Type="Date" Font-Bold="true" ForeColor="Red"></asp:CompareValidator>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToCompare="txtStartDate"
                                                ValidationGroup="Search" ControlToValidate="txtEndDate" ErrorMessage="*" Operator="GreaterThanEqual"
                                                Type="Date" Font-Bold="true" ForeColor="#FF3300"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-12 table-responsive" align="left">
                                            <asp:GridView ID="Gvd_ViewTicket" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                                                BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                                OnRowDataBound="Gvd_ViewTicket_RowDataBound" AllowPaging="True" OnPageIndexChanging="Gvd_ViewTicket_PageIndexChanging"
                                                PageSize="10" AllowSorting="True" OnSorting="Gvd_ViewTicket_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="TicketID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbTicketID" runat="server" Text='<%#Bind("TicketID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbName" runat="server" Text='<%#Bind("FirstName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ticket Raised Date" SortExpression="Ticket_Raised_Date"
                                                        HeaderStyle-ForeColor="#585858" HeaderStyle-Font-Underline="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbDategrd" runat="server" Text='<%#Bind("Ticket_Raised_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbCategory" runat="server" Text='<%#Bind("Cat_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbSubCategory" runat="server" Text='<%#Bind("SubCat_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbIssuegrd" runat="server" Text='<%#Bind("Issue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Priority">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lborioritygrd" runat="server" Text='<%#Bind("Ticket_Priority")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-ForeColor="#585858" SortExpression="Ticket_Status"
                                                        HeaderStyle-Font-Underline="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbStatus" runat="server" Text='<%#Bind("Ticket_Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" Font-Bold="true" runat="server" OnClick="lnkSelect_Click"
                                                                Text="Select"></asp:LinkButton>
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
                    <div class="row" hidden>
                        <div class="col-sm-12">
                            <div class="widget" id="upd" runat="server">
                                <div class="widget-header">
                                    <h3><i class="fas fa-edit"></i>Update</h3>
                                    <div class="btn-group widget-header-toolbar" id="div2" runat="server" clientidmode="Static">
                                        <a href="#" title="Focus" class="btn-borderless btn-focus" id="lbUpdateTicketFocus"
                                            runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                                class="btn-borderless btn-toggle-expand" id="A2" runat="server" clientidmode="Static">
                                                <i class="fa fa-chevron-up"></i></a>
                                    </div>
                                </div>
                                <div class="widget-content">
                                    <br />
                                    <div class="form-group row ">
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label5" runat="server" Text="Ticket ID:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtTicketID" CssClass="form-control" runat="server" ReadOnly="false"
                                                ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="lbName1" runat="server" Text="Name:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtName1" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label6" runat="server" Text="Category:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCategory" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label7" runat="server" Text="Sub Category:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtSubcategory" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label8" runat="server" Text="Priority:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPriority" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="lblCreateDate" runat="server" Text="Created Date:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCreatedate" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label9" runat="server" Text="Status:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlReopen" runat="server" OnSelectedIndexChanged="ddlReopen_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="form-control" ClientIDMode="Static">
                                                <asp:ListItem Text="Select" Value="Select" Selected="True" />
                                                <asp:ListItem Text="ReOpen" Value="Reopen"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label10" runat="server" Text="Issue:"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtIssue1" runat="server" CssClass="form-control" BackColor="White"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="requiredReOpen" runat="server" ControlToValidate="ddlReopen"
                                                ErrorMessage="*" Font-Bold="True" ForeColor="Red" InitialValue="Select" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Update"
                                                ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtIssue1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row ">
                                        <div class="col-sm-8">
                                        </div>
                                        <div class="col-sm-2">

                                            <asp:Button Text="Submit" runat="server" ID="btnReopen" CssClass="btn form-control"
                                                OnClientClick="if(Page_ClientValidate('Update')) ShowModalPopup('pload');"
                                                ClientIDMode="Static" ValidationGroup="Update" OnClick="btnReopen_Click" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button Text="Cancel" runat="server" ID="btnCancel2" CssClass="btn form-control"
                                                OnClick="btnCancel2_Click" ClientIDMode="Static" />
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
                            <img src="../dist1/Images/animated_figure.gif" alt="loading" title="loading" /><br />
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

