<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="HealthWorks.Pages.ManageUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../dist1/css/style1.css" rel="stylesheet" />
    <style>
        .table tbody tr.active,
        .table tbody tr:hover {
            background-color: #ffe0d0;
        }

        .custom-modal {
            display: none;
            position: fixed;
            z-index: 3;
            padding-top: 0px;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgb(0, 0, 0);
            background-color: rgba(0, 0, 0, 0.7);
        }

        .custom-modal-content .form-group {
            float: right !important;
            width: auto !important;
            margin-bottom: 0;
            margin-top: -20px;
        }

        .custom-modal-content .modal-title {
            font-weight: 300;
            font-size: 26px;
            color: #777;
            line-height: normal;
            margin-bottom: 30px;
            padding-right: 30px;
        }

        .custom-modal-content .form-group label {
            position: static !important;
            margin-top: -5px;
        }


        .custom-modal-content {
            background-color: #fefefe;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            /*width: 200px;*/
            position: relative;
            top: 20%;
            text-align: left;
            width: 30% !important;
            padding-top: 18px;
            padding-left: 20px;
            padding-right: 10px;
            padding-bottom: 18px;
        }
    </style>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#SupportLi').find('i').removeClass('fa-angle-right').addClass('fa-angle-down')
        });
    </script>
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
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfFocus" />
            <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfFocusStatus" />
            <div class="row hidden">
                <div class="col-md-4">
                    <ul class="breadcrumb">
                        <li><i class="fa fa-home"></i><a href="Home.aspx">Home</a></li>
                        <li class="active">Admin</li>
                        <li class="active">ManageUser</li>
                    </ul>
                </div>
            </div>
            <div class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="main-header" id="divMainHeader" runat="server">
                            <h2>
                                <asp:Label Text="Manage User" runat="server" ID="pageName" /></h2>
                        </div>
                        <div class="main-header-description">
                            <em>
                                <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                        </div>
                    </div>
                </div>
                <div class="main-content">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="widget userCreation">
                                <div class="widget-header">
                                    <h3>
                                        <i class="fa fa-user-plus"></i>
                                        <asp:Label Text="User Creation" runat="server" ID="lblHeader" /></h3>
                                    <div class="btn-group widget-header-toolbar" id="divCreateUser" runat="server" clientidmode="Static">
                                        <a href="#" title="Expand/Collapse"
                                            class="btn-borderless btn-toggle-expand" id="liCreateUser" runat="server" clientidmode="Static">
                                            <i class="fa fa-chevron-up"></i></a><a href="#" title="Remove" class="btn-borderless btn-remove">
                                                <i class="fa fa-times"></i></a>
                                    </div>
                                </div>
                                <div class="widget-content">
                                    <div class="form-group row firstrow">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label2" Text="First Name :" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required"
                                                ControlToValidate="txtFirstName" ForeColor="Red" ValidationGroup="upload" runat="server"
                                                CssClass="form-control-label" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label3" Text="Last Name :" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2 control-label">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required"
                                                CssClass="form-control-label" ControlToValidate="txtLastName" ForeColor="Red"
                                                ValidationGroup="upload" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label7" Text="Phone No :" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label6" Text="Email ID :" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Required"
                                                CssClass="form-control-label" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="upload"
                                                runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label4" Text="Job Title :" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtJobTitle" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Required"
                                                CssClass="form-control-label" ControlToValidate="txtJobTitle" ForeColor="Red" ValidationGroup="upload"
                                                runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label8" Text="Department :" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtDepartment" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2 control-label">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Required"
                                                CssClass="form-control-label" ControlToValidate="txtDepartment" ForeColor="Red"
                                                ValidationGroup="upload" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label9" Text="Password :" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control disabled" Text="HWai@123"/>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="Required"
                                                CssClass="form-control-label" ControlToValidate="txtPassword" ForeColor="Red"
                                                ValidationGroup="upload" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2 control-label">
                                            <asp:Label ID="Label5" Text="User Type :" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:DropDownList runat="server" ID="ddlUserType" CssClass="custom-select">
                                                <asp:ListItem Text="Select" Value="Select" Selected="True" />
                                                <asp:ListItem Text="Admin" Value="Admin" />
                                                <asp:ListItem Text="Business User" Value="Business User" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Required"
                                                CssClass="form-control-label" ControlToValidate="ddlUserType" ForeColor="Red"
                                                ValidationGroup="upload" runat="server" InitialValue="Select" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2 control-label">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button Text="Register" runat="server" ID="btnRegister" CssClass="btn form-control"
                                                ClientIDMode="Static" ValidationGroup="upload" OnClientClick="if(Page_ClientValidate('upload')) ShowModalPopup('pload');" OnClick="btnRegister_Click" />
                                        </div>
                                        <div class="col-sm-4 control-label">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <br />
                            <div class="widget ">
                                <div class="widget-header">
                                    <h3>
                                        <i class="fa fa-users"></i>
                                        <asp:Label Text="Manage Users" runat="server" ID="Label1" /></h3>
                                    <div class="btn-group widget-header-toolbar" id="div1" runat="server" clientidmode="Static">
                                        <a href="#" title="Expand/Collapse"
                                            class="btn-borderless btn-toggle-expand" id="A1" runat="server" clientidmode="Static">
                                            <i class="fa fa-chevron-up"></i></a><a href="#" title="Remove" class="btn-borderless btn-remove">
                                                <i class="fa fa-times"></i></a>
                                    </div>
                                </div>
                                <div class="widget-content table-responsive user">
                                    <div class="form-group row firstrow">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GV_Users" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                                BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                                                OnRowDeleting="GV_Users_RowDeleting" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblId" runat="server" Text='<%# Bind("[ID]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblUserId" runat="server" Text='<%# Bind("[UserId]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="First Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblFirstName" runat="server" Text='<%# Bind("[FirstName]") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblLastName" runat="server" Text='<%# Bind("[LastName]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblJobTitle" runat="server" Text='<%# Bind("[JobTitle]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblDepartment" runat="server" Text='<%# Bind("[Department]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblUserName" runat="server" Text='<%# Bind("[UserName]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblMobile" runat="server" Text='<%# Bind("[Mobile]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblIsActive" runat="server" Text='<%# Bind("[IsActive]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtIsActive" runat="server" Text='<%# Eval("IsActive") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="IsActive" HeaderText="Status" SortExpression="IsActive" />--%>
                                                    <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Editbtn" Text="" runat="server" CommandName="Edit" CausesValidation="False" OnClientClick="ShowModalPopup('pload');"><i class="fas fa-pen"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="Deletebtn" runat="server" CausesValidation="False" CommandName="Delete" CssClass="deleteUser" OnClientClick="ShowModalPopup('pload');"><i class="far fa-trash-alt"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" Text="" runat="server" CommandName="Update" CausesValidation="False" OnClientClick="ShowModalPopup('pload');">
                                         <svg id="Svg1" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 16 16" width="16" height="16">
                                             <path class="cls-1" d="M15.78,4.43a2.65,2.65,0,0,0-.54-.84L12.41.76a2.65,2.65,0,0,0-.84-.54,2.5,2.5,0,0,0-1-.22H1.21A1.2,1.2,0,0,0,0,1.21V14.79A1.2,1.2,0,0,0,1.21,16H14.79A1.2,1.2,0,0,0,16,14.79V5.41A2.5,2.5,0,0,0,15.78,4.43Zm-4.14,6.4v3.39H4.36V10.83ZM7,5.09V1.86A.07.07,0,0,1,7,1.8a.08.08,0,0,1,.06,0l2,0,0,3.29s0,0-.08.08H7L7,5.15A.07.07,0,0,1,7,5.09ZM13.06,9.4a1.18,1.18,0,0,0-.86-.35H3.8a1.2,1.2,0,0,0-1.21,1.21v4H1.78V1.78h.81v4A1.2,1.2,0,0,0,3.8,7H9.62a1.21,1.21,0,0,0,1.21-1.21V1.84l.06,0a.65.65,0,0,1,.26.15L14,4.85a.68.68,0,0,1,.15.27,1,1,0,0,1,.08.29v8.81h-.81v-4A1.21,1.21,0,0,0,13.06,9.4Z"/></svg>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton2" Text="" runat="server" CommandName="Cancel" CausesValidation="False">
                                        <svg id="Svg2" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 16 16" width="16" height="16">
                                            <path class="cls-1" d="M8,0a8,8,0,1,0,8,8A8,8,0,0,0,8,0ZM8,14.32A6.32,6.32,0,1,1,14.32,8,6.33,6.33,0,0,1,8,14.32Z"/><path class="cls-1" d="M11.93,4.07a.84.84,0,0,0-1.19,0L8,6.83,5.25,4.11A.84.84,0,1,0,4.06,5.3L6.78,8,4.1,10.71a.84.84,0,0,0,0,1.19.86.86,0,0,0,.6.24.84.84,0,0,0,.59-.24L8,9.22l2.72,2.72a.84.84,0,0,0,.59.24.86.86,0,0,0,.6-.24.84.84,0,0,0,0-1.19L9.17,8l2.76-2.76A.84.84,0,0,0,11.93,4.07Z"/></svg>
                                                            </asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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

