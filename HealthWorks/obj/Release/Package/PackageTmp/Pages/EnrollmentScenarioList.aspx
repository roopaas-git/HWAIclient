<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="EnrollmentScenarioList.aspx.cs" Inherits="HealthWorks.Pages.EnrollmentScenarioList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <script type="text/javascript">   
        $(document).ready(function () {
            maintainScrollPosition();
             
        });
        function pageLoad() {
            maintainScrollPosition();            
        }
        function maintainScrollPosition() {
            $("#dvScroll").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
             
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }

        function openEnrollmentSimulatorOutput(controlId) {            
            setTimeout(document.getElementById(controlId).click(), 10000);         
        }
       

    </script>
    <style>
        .tr-align-right {
            text-align: right;
        }

         .custom-modal-grid-header
        {
            background-color: #5c276e !important; 
            border: 1px solid #4c4c4c !important;      
        }

        .close5 {   
            color: #fff !important; 
        }

        .card {
            z-index: 0;
            border: none;
            position: relative
        }

        #progressbar {
            overflow: hidden;
            color: lightgrey;
            margin-bottom: 0.5rem;
        }

            #progressbar .itemcolor {
                color: #c1c1c1;
            }

            #progressbar li.active > strong > a {
                color: #5c276e !important;
            }

            #progressbar li {
                list-style-type: none;
                font-size: 12px;
                width: 25%;
                float: left;
                position: relative
            }

            #progressbar #CreateScenario:before {
                content: "1";
                font-size: 15px;
                font-weight: 900;
            }

            #progressbar #DownloadPBP:before {
                content: "2";
                font-size: 15px;
                font-weight: 900;
            }

            #progressbar #UploadPBP:before {
                content: "3";
                font-size: 15px;
                font-weight: 900;
            }

            #progressbar #ResultsReady:before {
                content: "4";
                font-size: 15px;
                font-weight: 900;
            }

            #progressbar li:before {
                width: 32px;
                height: 32px;
                line-height: 28px;
                display: block;
                font-size: 18px;
                color: #ffffff;
                background: #c1c1c1;
                border-radius: 50%;
                margin: 0 auto 10px auto;
                padding: 2px;
            }

        #CreateScenario:after {
            content: '';
            height: 2px;
            background: lightgray;
            position: absolute;
            top: 16px;
            z-index: -1;
        }

        #DownloadPBP:after {
            content: '';
            width: 100%;
            height: 2px;
            background: lightgray;
            position: absolute;
            left: -50%;
            top: 16px;
            z-index: -1;
        }

        #UploadPBP:after {
            content: '';
            width: 100%;
            height: 2px;
            background: lightgray;
            position: absolute;
            left: -50%;
            top: 16px;
            z-index: -1;
        }

        #ResultsReady:after {
            content: '';
            width: 100%;
            height: 2px;
            background: lightgray;
            position: absolute;
            left: -50%;
            top: 16px;
            z-index: -1
        }

        #progressbar li.active:before, #progressbar li.active:after {
            background: #5c276e;
        }

        .ScenarioCustomHeader {
            vertical-align: middle !important;
        }

        .btn:disabled {
            background-color: gray !important;
        }

        .labeld {
            font-size: 14px;
        }

        .RadComboBox {
            height: 25px !important;
            width: 160px !important;
            margin-right: 25px !important;
        }

        .form-control {
            height: 25px !important;
            margin-right: 25px !important;
        }

        .btn {
            height: 25px !important;
        }

        select.form-control {
            height: 32px !important;
        }
        .RadComboBox table td.rcbInputCell {
            padding: 0 !important;
        }
    </style>
    <style type="text/css">
        .pager span {
            background-color: grey;
            color: #fff;
            padding: 3px 8px 3px 8px;
        }
    </style>

    <link href="../dist/css/CustomModal.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
    <div class="container-fluid" id="id_nav" runat="server" style="pointer-events: none;">
        <div class="row justify-content-center mt-0">
            <div class="col-md-12 text-center p-0 mt-3 mb-2">
                <div class="card px-0 pt-3 pb-0 mt-1 mb-1">
                    <div class="row">
                        <div class="col-md-12 mx-0">
                            <ul id="progressbar">
                                <li id="CreateScenario" runat="server" clientidmode="Static">
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBScenarios" runat="server" Text="Create Scenario" OnClick="LBScenarios_Click"></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="DownloadPBP" runat="server" clientidmode="Static">
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBPlans" runat="server" Text="Plan Selection" OnClick="LBPlans_Click"></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="UploadPBP" runat="server" clientidmode="Static">
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBPlans_UploadPBP" runat="server" Text="Upload PBP / Change Benefit(s)" OnClick="LBQuickAccess_Click"></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="ResultsReady" runat="server" clientidmode="Static">
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBPlans_ResultsReady" runat="server" Text="Results Ready"></asp:LinkButton>
                                    </strong>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="scenario-table-outer-wrapper">
            <div class="scenario-table-inner-wrapper">
                <div class="scenario-table-header-wrapper">
                    <div class="scenario-table-header">
                        <h1><span class="header-label">Scenario List</span>
                        </h1>

                        <div class="scenario-table-controls" style="padding-top: 15px;">
                            <div class="link-wrapper" style="width: 37%; text-align: right">
                                <%-- <asp:LinkButton Text="" runat="server" ToolTip="Create New Scenario" CssClass="external-link" ID="lbCreate" OnClick="lbCreate_Click">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-plus-circle">
                                    <circle cx="12" cy="12" r="10"></circle><line x1="12" y1="8" x2="12" y2="16"></line><line x1="8" y1="12" x2="16" y2="12"></line></svg>
                                </asp:LinkButton>--%>

                                <telerik:RadComboBox ID="ddlFilter" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" DropDownCssClass="textfont" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Scenario ID" Value="ScenarioId" />
                                        <telerik:RadComboBoxItem Text="Scenario Name" Value="ScenarioName" />
                                        <telerik:RadComboBoxItem Text="Bid ID" Value="BidId" />
                                        <telerik:RadComboBoxItem Text="Submitted By" Value="SubmittedBy" />
                                        <%--<telerik:RadComboBoxItem Text="Market" Value="Market" />--%>
                                        <telerik:RadComboBoxItem Text="Status" Value="Status" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:TextBox ID="txt_search" runat="server" CssClass="form-control" Width="160px" placeholder="Search" OnTextChanged="txt_search_TextChanged" AutoPostBack="true" />
                                <asp:Button Text="Create Scenario" runat="server" ToolTip="Create New Scenario" CssClass="btn" ID="Create" OnClick="lbCreate_Click"></asp:Button>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="scenario-table-container">
                    <div class="table-responsive" id="dvScroll" onscroll="setScrollPosition(this.scrollTop);">
                        <asp:GridView ID="grdScenario" runat="server" AutoGenerateColumns="False" Width="100%"
                            BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                            OnSelectedIndexChanging="grdScenario_SelectedIndexChanging"
                            OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting"
                            OnRowDataBound="grdScenario_RowDataBound" CurrentSortField="ScenarioId" EmptyDataText="No Data Found"
                            CurrentSortDirection="DESC" AllowSorting="true" PagerSettings-PageButtonCount="3" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging"
                            OnSorting="grdScenario_Sorting" OnRowCreated="grdScenario_RowCreated" PageSize="6" PagerStyle-HorizontalAlign="Center" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageImageUrl="~/dist/Images/last.png" PagerSettings-FirstPageImageUrl="~/dist/Images/first.png"
                            PagerStyle-BackColor="White" PagerStyle-CssClass="pager">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rbSelect" runat="server" Text="" OnCheckedChanged="rbSelect_CheckedChanged" AutoPostBack="true" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:RadioButton ID="RadioButton1" runat="server" Enabled="false" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" SortExpression="ScenarioId" ItemStyle-Width="4%" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("ScenarioId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scenario" SortExpression="ScenarioName" ItemStyle-Width="11%" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="LblScenarioName" runat="server" Text='<%# Bind("ScenarioName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtScenarioName" runat="server" Text='<%# Eval("ScenarioName") %>'></asp:TextBox>
                                        <asp:Label ID="oldValue" runat="server" Visible="false" Text='<%# Bind("ScenarioName") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" ItemStyle-Width="20%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created On" SortExpression="CreatedDate" ItemStyle-Width="8%" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="LblCreatedOn" runat="server" Text='<%# Bind("CreatedDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created By" Visible="false" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="LblCreatedBy" ToolTip='<%# Bind("CreatedBy") %>' runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Submitted By" SortExpression="SubmittedBy" ItemStyle-Width="16%" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSubmittedBy" ToolTip=<%# Bind("SubmittedBy") %> runat="server" Text='<%# Bind("SubmittedBy") %>'
                                            Style="width: 150px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Market" SortExpression="Market" ItemStyle-Width="9%" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="LblShareCount" Visible="false" runat="server" Text='<%# Bind("ShareCount") %>'></asp:Label>
                                        <asp:Label ID="LblMarket" runat="server" Text='<%# Bind("Market") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Bid ID" SortExpression="BidId" ItemStyle-Width="10%" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="LblBidID" runat="server" Text='<%# Bind("BidId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pre-AEP Enrollment" SortExpression="Pre_AEP_Enrolment" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%" HeaderStyle-CssClass="ScenarioCustomHeader tr-align-right">
                                    <ItemTemplate>
                                        <asp:Label ID="Pre_AEP_Enrolment" runat="server" Text='<%# Bind("Pre_AEP_Enrolment") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Post-AEP Enrollment" SortExpression="Post_AEP_Enrolment" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%" HeaderStyle-CssClass="ScenarioCustomHeader tr-align-right">
                                    <ItemTemplate>
                                        <asp:Label ID="Post_AEP_Enrolment" runat="server" Text='<%# Bind("Post_AEP_Enrolment") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Simulated Post-AEP Enrollment" SortExpression="Simulated_Results" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%" HeaderStyle-CssClass="ScenarioCustomHeader tr-align-right">
                                    <ItemTemplate>
                                        <asp:Label ID="Simulated_Results" runat="server" Text='<%# Bind("Simulated_Results") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="8%" SortExpression="ProcessStatus" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="LblStatus" runat="server" Text='<%# Bind("ProcessStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions" ItemStyle-Width="8%" HeaderStyle-CssClass="ScenarioCustomHeader">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" Text="" runat="server" Visible="false" CommandName="Edit" CausesValidation="False">
                                        <i class="fas fa-pen"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="Lnk_Delete" Text="" runat="server" CommandName="Delete" ToolTip="Delete Scenario" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this scenario?');">
                                            <i class="far fa-trash-alt"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="Lnk_Share" Text="" runat="server" CommandName="Select" ToolTip="Share Scenario" CausesValidation="False" OnClick="Share_Click">
                                           <i class="fa fa-share-alt" ></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="Lnk_output" Text="" runat="server" CommandName="Output" ToolTip="Simulation Output" CausesValidation="False" OnClick="Lnk_output">
                                        <i class="fa fa-clipboard-check"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" Text="" runat="server" CommandName="Update" CausesValidation="False">
                                         <svg id="Svg1" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 16 16" width="16" height="16">
                                             <path class="cls-1" d="M15.78,4.43a2.65,2.65,0,0,0-.54-.84L12.41.76a2.65,2.65,0,0,0-.84-.54,2.5,2.5,0,0,0-1-.22H1.21A1.2,1.2,0,0,0,0,1.21V14.79A1.2,1.2,0,0,0,1.21,16H14.79A1.2,1.2,0,0,0,16,14.79V5.41A2.5,2.5,0,0,0,15.78,4.43Zm-4.14,6.4v3.39H4.36V10.83ZM7,5.09V1.86A.07.07,0,0,1,7,1.8a.08.08,0,0,1,.06,0l2,0,0,3.29s0,0-.08.08H7L7,5.15A.07.07,0,0,1,7,5.09ZM13.06,9.4a1.18,1.18,0,0,0-.86-.35H3.8a1.2,1.2,0,0,0-1.21,1.21v4H1.78V1.78h.81v4A1.2,1.2,0,0,0,3.8,7H9.62a1.21,1.21,0,0,0,1.21-1.21V1.84l.06,0a.65.65,0,0,1,.26.15L14,4.85a.68.68,0,0,1,.15.27,1,1,0,0,1,.08.29v8.81h-.81v-4A1.21,1.21,0,0,0,13.06,9.4Z"/></svg>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton3" Text="" runat="server" CommandName="Cancel" CausesValidation="False">
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
        <div class="action-btn-wrapper">
            <%--<asp:Button Text="Upload Results" runat="server" CssClass="btn" ID="btnUpload" Enabled="false" OnClick="btnUpload_Click" />--%>

            <asp:Button Text="Load Scenario" runat="server" CssClass="btn" ID="btnLoad" Enabled="false" OnClick="btnLoad_Click" />
        </div>
    </section>

    <!--Radio Button content -->
    <script type="text/javascript">
        function SelectSingleRadiobutton(rbSelect) {
            var rdBtn = document.getElementById(rbSelect);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }
    </script>
    <div id="myModal2" class="custom-modal">
        <!-- Modal content -->
        <div class="custom-modal-header">
            <span>Create New Scenario</span>
            <span class="far fa-times-circle close2"></span>
        </div>
        <div class="custom-modal-content">
            <div class="row">
                <div class="col-7">
                    <asp:TextBox ID="txtScenario" CssClass="form-control" placeholder="Scenario Name" runat="server" MaxLength="16"></asp:TextBox>
                </div>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="requiredScenario" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Saveas" ControlToValidate="txtScenario"></asp:RequiredFieldValidator>
                </div>
                <div class="col-4">
                    <asp:Label ID="scenarioExists" runat="server" ValidationGroup="Saveas" Text="Scenario Name Already Exists" ForeColor="#CC0000" Visible="False"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-7">
                    <asp:TextBox ID="txtScenarioDesc" TextMode="MultiLine" CssClass="form-control" placeholder="Scenario Description" MaxLength="100" runat="server"></asp:TextBox>
                </div>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="requireddesc" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Saveas" ControlToValidate="txtScenarioDesc"></asp:RequiredFieldValidator>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-7">
                    <asp:DropDownList ID="ddlUserDetails" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="False">
                        <asp:ListItem Selected="True" Value="0">Select User</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="requiredUserDetails" runat="server" ControlToValidate="ddlUserDetails" ForeColor="Red" ValidationGroup="Saveas" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>

                    <%--<asp:RequiredFieldValidator ID="requiredUser" runat="server" EnableClientScript="true" Display="Dynamic" ErrorMessage="*" InitialValue="Select User" ForeColor="Red" ValidationGroup="Saveas" ControlToValidate="ddlUserDetails"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-4" style="text-align: left;">
                    <asp:Button ID="SaveAs" runat="server" OnClick="btnPopUpSave_Click" Text="Save" CssClass="btn form-control" ValidationGroup="Saveas" />
                </div>
            </div>
            <br />
        </div>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal2 = document.getElementById('myModal2');

        var divv = document.getElementById('#toptab');

        // Get the button that opens the modal
        var btn = document.getElementsByClassName("jsShare");

        // Get the <span> element that closes the modal
        var span2 = document.getElementsByClassName("close2")[0];

        // Get the <span> element that closes the modal
        var cancel = document.getElementsByClassName("cancelBtn")[0];

        // When the user clicks the button, open the modal

        function openModal2() {
            modal2.style.display = "block";

        }
        function closeModal2() {
            modal2.style.display = "none";

        }
        // When the user clicks on <span> (x), close the modal
        span2.onclick = function () {
            modal2.style.display = "none";
        }

    </script>

    <div id="myModal4" class="custom-modal">
        <!-- Modal content -->
        <div class="custom-modal-header">
            <span>Share Scenario</span>
            <span class="far fa-times-circle closeModal1"></span>
        </div>
        <div class="custom-modal-content">
            <div class="row">
                <div class="col-3">
                    <asp:Label ID="lblUsernamelist" CssClass="form-control-label" runat="server" Text="To:"></asp:Label>
                </div>
                <div class="col-7">
                    <asp:DropDownList ID="ddlUser" CssClass="custom-select state-select" runat="server" AppendDataBoundItems="True" AutoPostBack="False">
                        <asp:ListItem Selected="True" Value="0">Select User</asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="col-2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Shareplan" InitialValue="0" ControlToValidate="ddlUser"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                    <asp:Label ID="lblMessage" CssClass="form-control-label" runat="server" Text="Message:"></asp:Label>
                </div>
                <div class="col-7">
                    <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Message"></asp:TextBox>

                </div>
                <div class="col-2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Shareplan" ControlToValidate="txtMessage"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row" style="padding-bottom: 10px;">
                <div class="col-3">
                </div>
                <div class="col-7">
                    <asp:Button ID="btnSharePlan" CssClass="btn form-control" OnClick="btnPopUpShare_Click" Text="Share" runat="server" ValidationGroup="Shareplan" Style="margin-top: 10px !important" />
                </div>
                <div class="col-2">
                    <asp:Label ID="ScenarioNameS" runat="server" Text="Label" Visible="False"></asp:Label>
                    <asp:Label ID="ScenarioDescS" runat="server" Text="Label" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal4 = document.getElementById('myModal4');

        // Get the button that opens the modal
        var btn = document.getElementsByClassName("jsShare");

        // Get the <span> element that closes the modal
        var span4 = document.getElementsByClassName("closeModal1")[0];

        // Get the <span> element that closes the modal
        var cancel = document.getElementsByClassName("cancelBtn")[0];

        // When the user clicks the button, open the modal
        function openModal4() {
            modal4.style.display = "block";
        }
        // When the user clicks on <span> (x), close the modal
        span4.onclick = function () {
            modal4.style.display = "none";
        }
    </script>

    <div id="myModal5" class="custom-modal-grid">
        <!-- Modal content -->
        <div class="custom-modal-grid-header">
            <span>Simulation Result</span>
            <span class="far fa-times-circle close5"></span>
        </div>
        <div class="custom-modal-grid-content">
            <div class="scenario-table-container">
                <div class="table-responsive" id="DivOutput">
                    <asp:GridView ID="GrdOutput" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdOutput_RowDataBound"
                        BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table" OnRowCommand="GrdOutput_RowCommand"
                        EmptyDataText="No Data Found" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" RowStyle-CssClass="labelfont">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblScenarioId" runat="server" Text='<%# Bind("ScenarioId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HtmlEncode="False" DataField="ScenarioName"
                                HeaderText="Scenario Name"></asp:BoundField>
                            <asp:BoundField HtmlEncode="False" DataField="Description"
                                HeaderText="Description"></asp:BoundField>
                            <asp:BoundField HtmlEncode="False" DataField="PlanName"
                                HeaderText="Plan"></asp:BoundField>
                            <asp:TemplateField HeaderText="Pre-AEP Enrollment" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Pre_AEP_Enrolment" runat="server" Text='<%# Bind("Pre_AEP_Enrolment") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Post-AEP Enrollment" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Post_AEP_Enrolment" runat="server" Text='<%# Bind("Post_AEP_Enrolment") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Simulated Post-AEP Enrollment" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Simulated_Results" runat="server" Text='<%# Bind("Simulated_Results") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Simulated Change from Pre-AEP" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Simulated_Delta" runat="server" Text='<%# Bind("Simulated_Delta") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Simulated Change from Post-AEP" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="Growth" runat="server" Text='<%# Bind("Growth") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbIscreated" runat="server" Visible="false" Text='<%# Bind("[ScenarioId]") %>'></asp:Label>
                                    <div style="width: 120px; font-size: 12px; background: #F4AB1C !important; border-radius: 5px; height: 32px; line-height: 32px;">
                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("UploadedFilePath") %>'
                                            CommandName="Download" Text="Download Scenario" Style="color: #fff !important; padding: 0 !important;" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row" style="padding-bottom: 10px;">
            </div>
        </div>
    </div>

    <script type="text/javascript">
        // Get the modal
        var modal5 = document.getElementById('myModal5');

        var divv = document.getElementById('#toptab');

        // Get the button that opens the modal
        var btn = document.getElementsByClassName("jsShare");

        // Get the <span> element that closes the modal
        var span5 = document.getElementsByClassName("close5")[0];

        // Get the <span> element that closes the modal
        var cancel = document.getElementsByClassName("cancelBtn")[0];

        // When the user clicks the button, open the modal

        function openModal5() {
            modal5.style.display = "block";

        }
        function closeModal5() {
            modal5.style.display = "none";

        }
        // When the user clicks on <span> (x), close the modal
        span5.onclick = function () {
            modal5.style.display = "none";
        }
    </script>
</asp:Content>
