<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="ManageAlerts.aspx.cs" Inherits="HealthWorks.Pages.ManageAlerts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        textarea.txtHeight {
            min-height: 75px !important;
            overflow: auto;
        }
    </style>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#OthersLi').find('i').removeClass('fa-angle-right').addClass('fa-angle-down')
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfFocus" />
    <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfFocusStatus" />
    <div class="content">
        <div class="main-header" runat="server" id="divMainHeader">
            <h2>
                <asp:Label Text="Manage Alerts" runat="server" ID="pageName" /></h2>
        </div>
        <div class="main-header-description">
            <em>
                <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
        </div>
        <div class="main-content">
            <div class="row">
                <div class="col-12">
                    <div class="widget">
                        <div class="widget-header">
                            <h3>
                                <i class="fa fa-exclamation-triangle"></i>Add Alerts / Updates</h3>
                            <div class="btn-group widget-header-toolbar" id="divManageNews" runat="server" clientidmode="Static">
                                <a href="#"
                                    title="Expand/Collapse" class="btn-borderless btn-toggle-expand" id="lbManageNews"
                                    runat="server" clientidmode="Static"><i class="fa fa-chevron-up"></i></a>
                            </div>
                        </div>
                        <div class="widget-content user">
                            <div class="form-group row firstrow">
                                <div class="col-2 control-label">
                                    <asp:Label ID="Label4" Text="Alerts / Updates :" runat="server" />
                                </div>
                                <div class="col-4">
                                    <asp:TextBox runat="server" ID="txtAlert" CssClass="form-control " />
                                </div>
                                <div class="col-2 control-label">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Required"
                                        ControlToValidate="txtAlert" ForeColor="Red" ValidationGroup="upload" runat="server"
                                        CssClass="form-control-label" />
                                </div>
                                <div class="col-4">
                                </div>
                            </div>
                            <div class="form-group row ">
                                <div class="col-2 control-label">
                                </div>
                                <div class="col-4">
                                    <asp:Button Text="Save" runat="server" ID="btnSave" CssClass="btn form-control "
                                        ClientIDMode="Static" ValidationGroup="upload" OnClick="btnSave_Click" />
                                </div>
                                <div class="col-4 control-label">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">

                <div class="widget stories">
                    <div class="widget-header">
                        <h3>
                            <i class="fa fa-exclamation-triangle"></i>
                            <asp:Label Text="Manage Alerts / Updates" runat="server" ID="Label5" /></h3>
                        <div class="btn-group widget-header-toolbar" id="div1" runat="server" clientidmode="Static">
                            <a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand" id="A1"
                                runat="server" clientidmode="Static"><i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-content table-responsive user">
                        <div class="form-group row firstrow">
                            <div class="col-md-12">
                                <asp:GridView ID="GV_Alert" runat="server" AutoGenerateColumns="False" BackColor="Transparent"
                                    GridLines="None" CssClass="table table-hover table-striped scenario-table" OnRowDeleting="GV_Alert_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LblId" runat="server" Text='<%# Bind("[ID]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Main Content">
                                            <ItemTemplate>
                                                <asp:Label ID="LblMain_Content" runat="server" Text='<%# Bind("[Main_Content]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uploaded By">
                                            <ItemTemplate>
                                                <asp:Label ID="LblUploadedBy" runat="server" Text='<%# Bind("[UploadedBy]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uploaded Date">
                                            <ItemTemplate>
                                                <asp:Label ID="LblUploadedDate" runat="server" Text='<%# Bind("[UploadedDate]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField InsertVisible="False" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Deletebtn" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete"></asp:LinkButton>
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
    </div>
</asp:Content>
