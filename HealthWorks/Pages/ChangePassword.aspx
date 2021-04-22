<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HealthWorks.Pages.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HfPageName" Value="Change Password" runat="server" />
            <asp:HiddenField ID="HfPageLink" Value="ChangePassword.aspx" runat="server" />
            <asp:HiddenField ID="TabName" runat="server" ClientIDMode="Static" Value="settingstab" />
                       <div class="content">
                 <div class="row">
                    <div class="col-md-12">
                <div class="main-header" runat="server" id="divMainHeader">
                    <h2>
                        <asp:Label Text="Change PassWord" runat="server" ID="pageName" /></h2>
                    <em>
                        <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                </div>
                           </div>
                        </div>
                           <br />
                <div class="main-content">
                         <div class="form-group row">
                               <div class="col-sm-12">
                             <div class="widget">
                                <div class="widget-header">
                                    <h3><i class="fa fa-key" aria-hidden="true"></i>Change Password</h3>
                                    <div class="btn-group widget-header-toolbar" id="divCreateTicket" runat="server"
                                        clientidmode="Static">
                                        <a href="#" title="Focus" class="btn-borderless btn-focus" id="lbCreateTicketFocus"
                                            runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                                class="btn-borderless btn-toggle-expand" id="lbCreateTicket" runat="server" clientidmode="Static">
                                                <i class="fa fa-chevron-up"></i></a>
                                    </div>
                                </div>
                                <div class="widget-content">
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label1" Text="Password :" runat="server"  />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtPWD" CssClass="form-control" TextMode="Password"
                                                Text="" ClientIDMode="Static" EnableViewState="false" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="required"
                                                ControlToValidate="txtPWD" ValidationGroup="Check" ForeColor="Red" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label2" Text="New Password :" runat="server"  />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtNewPassword" CssClass="form-control" TextMode="Password" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="required"
                                                ControlToValidate="txtNewPassword" ValidationGroup="Check" ForeColor="Red" runat="server" />
                                        </div>
                                    </div>
                                     <div class="form-group row">
                                        <div class="col-sm-2">
                                            <asp:Label ID="Label3" Text="Confirm Password :" runat="server"  />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox runat="server" ID="txtConfirmPWD" CssClass="form-control" TextMode="Password" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="required"
                                                ControlToValidate="txtConfirmPWD" ValidationGroup="Check" ForeColor="Red" runat="server" />
                                        </div>
                                    </div>
                                   <div class="form-group row">
                                        <div class="col-sm-12 col-sm-2">
                                        </div>
                                        <div class="col-sm-12 col-sm-4">
                                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="txtNewPassword"
                                                ControlToValidate="txtConfirmPWD" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                                                ValidationGroup="ChangePassword1" CssClass="form-control-required" ForeColor="Red"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="Button1" Text="Change Password" runat="server" CssClass="btn form-control"
                                                ValidationGroup="Check" OnClick="btnChangePWD_Click" />
                                        </div>
                                        <div class="col-sm-2">
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
</asp:Content>

