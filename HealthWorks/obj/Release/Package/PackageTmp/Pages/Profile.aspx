<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="HealthWorks.Pages.Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics4.Web.v14.1, Version=14.1.20141.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #lblAbout {
            min-height: 150px;
            width: 100%;
            overflow: hidden !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:HiddenField ID="TabName" runat="server" ClientIDMode="Static" Value="settingstab" />
            <div class="content">

                <div class="main-header" runat="server" id="divMainHeader">
                    <h2>
                        <asp:Label Text="Profile" runat="server" ID="pageName" /></h2>
                    <em>
                        <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                </div>
                <div class="main-content">
                    <br />
                    <nav class="tab-nav nav-light navbar">
                        <ul class="nav">
                            <li class="nav-item tab-nav-item plan-nav active"><a class="nav-link" href="#profile-tab" data-toggle="tab" aria-expanded="true"><i
                                class="fa fa-user"></i>&nbsp;Profile </a></li>
                            <li class="nav-item tab-nav-item design-nav"><a class="nav-link" href="#activity-tab" data-toggle="tab" aria-expanded="false"><i class="fa fa-rss"></i>&nbsp;Recent Activity </a></li>
                            <li class="nav-item tab-nav-item analyze-nav"><a class="nav-link" href="#update-tab" data-toggle="tab" aria-expanded="false"><i class="far fa-edit"></i>&nbsp;Update Content </a></li>
                        </ul>

                        <div class="tab-content profile-page">
                            <div class="tab-pane profile active" id="profile-tab">
                                <div class="row">
                                    <div class="col-3">
                                        <div class="user-info-left">
                                            <asp:Image ImageUrl="../Content/Images/Default.png" runat="server" CssClass="img-rounded img-thumbnail" ID="imageToDisplay" />
                                            <h5>
                                                <asp:Label Text="" runat="server" ID="lblFullName" />
                                            </h5>
                                        </div>
                                    </div>
                                    <div class="col-9">
                                        <div class="user-info-right">
                                            <div class="basic-info">
                                                <h3>
                                                    <i class="fa fa-square"></i>Basic Information
                                                </h3>
                                                <div class="row">
                                                    <div class="data-row col-3">
                                                        <span class="data-name">Email :</span>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label Text="" runat="server" ID="lblUserNameProfile" CssClass="form-control lblClass" />
                                                    </div>
                                                </div>
                                                <div class="row" hidden>
                                                    <div class="data-row col-3">
                                                        <span class="data-name">DOB :</span>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label Text="" runat="server" ID="lblDOB" CssClass="form-control lblClass" />
                                                    </div>
                                                </div>
                                                <div class="row" hidden>
                                                    <div class="data-row col-3">
                                                        <span class="data-name">Gender :</span>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label Text="" runat="server" ID="lblGender" CssClass="form-control lblClass" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="data-row col-3">
                                                        <span class="data-name">Last Login :</span>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label Text="" runat="server" ID="lblLastLogin" CssClass="form-control lblClass" />
                                                    </div>
                                                </div>
                                                <div class="row" hidden>
                                                    <div class="data-row col-3">
                                                        <span class="data-name">Date Joined :</span>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label Text="" runat="server" ID="lblDOJ" CssClass="form-control lblClass" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="contact_info">
                                                <h3>
                                                    <i class="fa fa-square"></i>Contact Information
                                                </h3>
                                                <div class="row" hidden>
                                                    <div class="data-row col-3">
                                                        <span class="data-name">Email :</span>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label Text="" runat="server" ID="lblEmail" CssClass="form-control lblClass" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="data-row col-3">
                                                        <span class="data-name">Mobile :</span>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label Text="" runat="server" ID="lblMobile" CssClass="form-control lblClass" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="data-row col-3">
                                                        <span class="data-name">Address :</span>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label Text="" runat="server" ID="lblAddress" CssClass="form-control lblClass" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="about">
                                                <h3>
                                                    <i class="fa fa-square"></i>About Me</h3>
                                                <p>
                                                    <ig:WebTextEditor ID="lblAbout" runat="server" Text="" TextMode="MultiLine" CssClass="lblClass"
                                                        ClientIDMode="Static">
                                                    </ig:WebTextEditor>
                                                    <%--<asp:Label ClientIDMode="Static" Text="" runat="server" ID="" CssClass="form-control lblClass" />--%>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane activity" id="activity-tab">
                                <asp:ListView ID="LST_UserRecent" runat="server" ClientIDMode="Static" OnItemDataBound="LST_UserRecent_ItemDataBound">
                                    <LayoutTemplate>
                                        <ul class="list-unstyled activity-list" id="ulTodo" runat="server" clientidmode="Static">
                                            <li runat="server" id="itemPlaceholder"></li>
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li><i class="fa fa-hand-o-right"></i>&nbsp;
                                        <asp:Label ID="lblUser_Recent" runat="server" Text='<%# Eval("Status") %>' />
                                            <asp:Label ID="lblUser_Recent_Date" runat="server" Text='<%# Eval("recentDate") %>'
                                                Visible="false" />
                                            <br />
                                            <asp:Label Text="-" runat="server" CssClass="lblGridClass" ID="lblUser_Recent_Time" />
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="tab-pane activity" id="update-tab">
                                <div class="col-6">
                                    <div class="row ">
                                        <div class="col-12 col-4">
                                            <asp:Label ID="Label1" Text="Full Name " runat="server" CssClass="form-control-label" />
                                        </div>
                                        <div class="col-12 col-8">
                                            <asp:TextBox runat="server" ID="txtFirstName_Update" CssClass="form-control textbox"
                                                disabled />
                                        </div>
                                    </div>
                                    <div class="row  hidden">
                                        <div class="col-12 col-4">
                                            <asp:Label ID="LabelDOB" Text="DOB " runat="server" CssClass="form-control-label" />
                                        </div>
                                        <div class="col-12 col-8">
                                            <asp:TextBox runat="server" ID="txtDOB_Update" CssClass="form-control textbox" />
                                            <asp:CalendarExtender ID="CalendarExtenderDOB" runat="server" TargetControlID="txtDOB_Update"
                                                Format="dd-MMM-yyyy" ClientIDMode="Static"></asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row hidden">
                                        <div class="col-12 col-4">
                                            <asp:Label ID="Label3" Text="Gender " runat="server" CssClass="form-control-label" />
                                        </div>
                                        <div class="col-12 col-8">
                                            <asp:DropDownList runat="server" ID="ddlGender_Update" CssClass="form-control textbox">
                                                <asp:ListItem Text="Select" Value="Select" />
                                                <asp:ListItem Text="Male" Value="Male" />
                                                <asp:ListItem Text="Female" Value="Female" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row hidden">
                                        <div class="col-12 col-md-4">
                                            <asp:Label ID="Label4" Text="Date Joined " runat="server" CssClass="form-control-label" />
                                        </div>
                                        <div class="col-12 col-md-8">
                                            <asp:TextBox runat="server" ID="txtDOJ_Update" CssClass="form-control textbox" />
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDOJ_Update"
                                                Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row hidden">
                                        <div class="col-12 col-md-4">
                                            <asp:Label ID="Label5" Text="Email " runat="server" />
                                        </div>
                                        <div class="col-12 col-md-8">
                                            <asp:TextBox runat="server" ID="txtEmail_Update" CssClass="form-control textbox"
                                                disabled />
                                        </div>
                                    </div>
                                    <div class="row ">
                                        <div class="col-12 col-4">
                                            <asp:Label ID="Label6" Text="Mobile " runat="server" />
                                        </div>
                                        <div class="col-12 col-8">
                                            <asp:TextBox runat="server" ID="txtMobile_Update" CssClass="form-control textbox" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-4">
                                            <asp:Label ID="Label7" Text="Address " runat="server" />
                                        </div>
                                        <div class="col-12 col-8">
                                            <asp:TextBox runat="server" ID="txtAddress_Update" CssClass="form-control textbox" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-4">
                                            <asp:Label ID="Label2" Text="Image " runat="server" CssClass="form-control-label" />
                                        </div>
                                        <div class="col-12 col-8">
                                            <asp:FileUpload ID="ImageUpload" runat="server" CssClass="form-control textbox" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-12 col-4">
                                        </div>
                                        <div class="col-12 col-8">
                                            <asp:Button Text="Update" runat="server" ID="btnUpdate" CssClass="btn form-control"
                                                ValidationGroup="Search" OnClick="btnUpdate_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="row">
                                        <div class="col-12 col-4">
                                            <asp:Label ID="Label8" Text="About Me " runat="server" />
                                        </div>
                                        <div class="col-12 col-10">
                                            <ig:WebTextEditor ID="txtAboutMe_Update" runat="server" Text="" TextMode="MultiLine"
                                                CssClass="form-control txtHeight textbox" ClientIDMode="Static">
                                            </ig:WebTextEditor>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
