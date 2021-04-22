<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="UploadFiles.aspx.cs" Inherits="HealthWorks.Pages.UploadFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#SupportLi').find('i').removeClass('fa-angle-right').addClass('fa-angle-down')
        });
    </script>
     <script type="text/javascript">
        function callsucess() {
            alert("The File is uploaded sucessfully.");
        }
        function callwarning() {
            alert("This File is already existing, please delete and upload.");
        }
        function callfail1() {
            alert("This File name is already existing, please rename and upload.");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <asp:HiddenField Value="0" runat="server" ClientIDMode="Static" ID="hfFocus" />
    <asp:HiddenField Value="" runat="server" ClientIDMode="Static" ID="hfFocusStatus" />
   
    <div class="main-content">
        <div class="form-group row">
            <div class="col-md-12">
                <div class="widget">
                    <div class="widget-header">
                        <h3><i class="fas fa-file-upload"></i>Upload Files</h3>
                        <div class="btn-group widget-header-toolbar" id="div1" runat="server"
                            clientidmode="Static">
                            <a href="#" title="Focus" class="btn-borderless btn-focus" id="A1"
                                runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                    class="btn-borderless btn-toggle-expand" id="A2" runat="server" clientidmode="Static">
                                    <i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="widget-content">
                        <div class="form-group row firstrow">
                            <div class="col-md-2 control-label">
                                <asp:Label ID="Label3" runat="server" Text="Report Name :"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddl_PdfUpload" CssClass="custom-select">
                                    <asp:ListItem Text="Select" Value="Select" Selected="True" />
                                    <asp:ListItem Text="Benefit Competitive Grid" Value="Benefit Competitive Grid" />
                                    <asp:ListItem Text="Sample File" Value="Sample File" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Required"
                                    CssClass="form-control-label" ControlToValidate="ddl_PdfUpload" ForeColor="Red"
                                    ValidationGroup="upload" runat="server" InitialValue="Select" />
                            </div>
                            <div class="col-md-2 ">
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>


                        <div class="form-group row firstrow">
                            <div class="col-md-2 control-label">
                                <asp:Label ID="Label2" runat="server" Text="Choose File :"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:FileUpload ID="documentUpload" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-2 ">
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-2 control-label">
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btn_Documents" runat="server" OnClick="btn_Documents_Click" CssClass="btn form-control" Text="Upload" ValidationGroup="upload" />
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-2 ">
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group row">
            <div class="col-md-12">
                <div class="widget">
                    <div class="widget-header">
                        <h3>
                            <i class="fa  fa-files-o"></i>Uploaded Files</h3>
                        <div class="btn-group widget-header-toolbar" id="divUploaded" runat="server" clientidmode="Static">
                            <a href="#" title="Focus" class="btn-borderless btn-focus"><i class="fa fa-eye"></i>
                            </a><a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand"
                                id="lbUploaded" runat="server" clientidmode="Static"><i class="fa fa-chevron-up"></i></a><a href="#" title="Remove" class="btn-borderless btn-remove"><i class="fa fa-times"></i></a>
                        </div>
                    </div>
                    <div class="widget-content">
                        <div class="form-group row center-block">
                            <div class="col-xs-12 table-responsive" align="left">
                                <asp:GridView ID="GV_PageTracking" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                    BackColor="Transparent" GridLines="None" CssClass="grid-teg table table-hover table-bordered"
                                    OnRowDataBound="GV_PageTracking_RowDataBound" OnRowDeleting="GV_PageTracking_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LblId" runat="server" Text='<%# Bind("[FIleID]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFileName" runat="server" Text='<%# Bind("[FileName]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Page Name">
                                            <ItemTemplate>
                                                <asp:Label ID="LblPageName" runat="server" Text='<%# Bind("[PageName]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:BoundField DataField="UploadedDate" HeaderText="Uploaded Date" SortExpression="UploadedDate" />
                                        <asp:TemplateField HeaderText="Uploaded By">
                                            <ItemTemplate>
                                                <asp:Label ID="LblUploadedBy" runat="server" Text='<%# Bind("[UploadedBy]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField InsertVisible="False" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Deletebtn" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete"></asp:LinkButton><%-- onclientclick="return confirm('Are you Sure to Delete?');"--%>
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
