<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="TrackPageUsage.aspx.cs" Inherits="HealthWorks.Pages.TrackPageUsage" %>

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
            <div class="content">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="main-header" id="divMainHeader" runat="server">
                            <h2>
                                <asp:Label Text="Track Page Usage" runat="server" ID="pageName" /></h2>
                        </div>
                        <div class="main-header-description">
                            <em>
                                <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                        </div>
                    </div>
                </div>
                <div class="main-content">
                    <br />
                    <div class="row">
                        <div class="col-sm-1 ">
                            <asp:LinkButton ID="lbImagBtn" runat="server" OnClick="imgBtnExport_Click">
                               <i class="fas fa-file-excel fa-3x" ></i>
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-4">
                            <span class="date-text">Start Date </span>
                            <asp:TextBox ID="txtStartDate" runat="server" class="form-control startDate " ClientIDMode="Static"
                                required></asp:TextBox>
                            <asp:CalendarExtender ID="dpCalender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate"
                                PopupButtonID="calenderimage"></asp:CalendarExtender>
                        </div>
                        <div class="col-sm-4">
                            <span class="date-text">End Date </span>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control textbox endDate" ClientIDMode="Static"
                                required></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                                PopupButtonID="calenderimage1"></asp:CalendarExtender>
                        </div>
                        <div class="col-sm-3">
                            <br />
                            <asp:Button ID="BtnSearch" runat="server" Text="View Report" OnClick="BtnSearch_Click"
                                class="btn form-control" ClientIDMode="Static" ValidationGroup="Search"
                                BorderStyle="None" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="End Date can't be less than Start Date."
                                ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Font-Bold="False"
                                ForeColor="Red" Operator="GreaterThan" Type="Date" ValidationGroup="Search"></asp:CompareValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="widget GridReport">
                                <div class="widget-header">
                                    <h3>
                                        <i class="fa fa-table"></i><span>Users actions</span></h3>
                                    <div class="btn-group widget-header-toolbar" id="divKPIList" runat="server" clientidmode="Static">
                                        <a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand"
                                            id="lbKPIList" runat="server" clientidmode="Static"><i class="fa fa-chevron-up"></i></a>
                                    </div>
                                </div>
                                <div class="widget-content table-responsive">
                                    <div class="form-group row firstrow">
                                        <div class="col-md-12 ">
                                            <asp:GridView ID="GV_PageTracking" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                                BackColor="Transparent" GridLines="None" CssClass="grid-teg table table-hover table-bordered table-dark-header table-striped ">
                                                <Columns>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                                                    <asp:TemplateField HeaderText="Page Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblPageName" runat="server" Text='<%# Bind("[PageName]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NoOfVisits" HeaderText="No Of Visits" SortExpression="NoOfVisits" />
                                                    <asp:BoundField DataField="LastVisitedDate" HeaderText="Last Visited" SortExpression="LastVisitedDate" />
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
</asp:Content>
