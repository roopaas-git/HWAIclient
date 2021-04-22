<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="Reports.aspx.cs" Inherits="HealthWorks.Pages.Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#SupportLi').find('i').removeClass('fa-angle-right').addClass('fa-angle-down')
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          
            <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfFocus" />
            <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfFocusStatus" />
            <div class="content">
                <div class="main-header" runat="server" id="divMainHeader">
                    <h2>
                        <asp:Label Text="Reports" runat="server" ID="pageName" /></h2>
                    <em>
                        <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                </div>
                <br />
                <div class="main-content">
                    <div class="form-group row">
                        <div class="col-sm-3">
                            <span class="date-text">Start Date:</span>
                            <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtStartDate_CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtStartDate"></asp:CalendarExtender>
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtEndDate"
                                ValidationGroup="Search" ControlToValidate="txtStartDate" ErrorMessage="*" Operator="LessThanEqual"
                                Type="Date" Font-Bold="true" ForeColor="Red"></asp:CompareValidator>
                        </div>
                        <div class="col-sm-3">
                            <span class="date-text">End Date:</span>

                            <asp:TextBox ID="txtEndDate" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtEndDate_CalendarExtender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtEndDate"></asp:CalendarExtender>
                        </div>
                        <div class="col-sm-1">
                            <br />
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToCompare="txtStartDate"
                                ValidationGroup="Search" ControlToValidate="txtEndDate" ErrorMessage="*" Operator="GreaterThanEqual"
                                Type="Date" Font-Bold="true" ForeColor="#FF3300"></asp:CompareValidator>
                        </div>
                        <div class="col-sm-2">
                            <br />
                            <asp:Button Text="Search" runat="server" ID="btnSearch" CssClass="btn form-control"
                                ValidationGroup="Search" ClientIDMode="Static" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-sm-2">
                            <br />
                            <asp:Button Text="Download" runat="server" ID="btnDownload" CssClass="btn form-control"
                                OnClick="btnDownload_Click"></asp:Button>
                        </div>
                    </div>
                    <br />
                </div>
                <div class="form-group row">

                    <div class="col-sm-12">
                        <div class="widget widget-table">
                            <div class="widget-header">
                                <h3><i class="fas fa-ticket-alt"></i>Tickets</h3>
                                <div class="btn-group widget-header-toolbar" id="divReportsTicket" runat="server"
                                    clientidmode="Static">
                                    <a href="#" title="Focus" class="btn-borderless btn-focus" id="lbTicketReports" runat="server"
                                        clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                            class="btn-borderless btn-toggle-expand" id="lbReportsTicket" runat="server"
                                            clientidmode="Static"><i class="fa fa-chevron-up"></i></a>
                                </div>
                            </div>
                            <div class="widget-content table-responsive">
                                <div class="form-group row firstrow">
                                    <div class="col-md-12 ">
                                        <asp:GridView ID="Gvd_PastTicket" runat="server" AutoGenerateColumns="False" BackColor="Transparent"
                                            ClientIDMode="Static" GridLines="None" CssClass="grid-teg table table-hover table-bordered"
                                            AllowSorting="true" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true"
                                            EmptyDataText="No Record Found"
                                            OnSorting="Gvd_PastTicket_Sorting">
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="#585858"
                                                    SortExpression="FirstName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbName" runat="server" Text='<%#Bind("FirstName") %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="Priority" HeaderStyle-Font-Underline="true" HeaderStyle-ForeColor="#585858"
                                                    SortExpression="Ticket_Priority">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbPriority" runat="server" Text='<%#Bind("Ticket_Priority") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbStatus" runat="server" Text='<%#Bind("Ticket_Status") %>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
