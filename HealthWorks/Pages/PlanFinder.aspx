<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="PlanFinder.aspx.cs" Inherits="HealthWorks.Pages.PlanFinder" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <style>
        .bottom-controls .btn1 {
            background-color: #fff;
            color: #d9d9da;
            border-radius: 2px;
            width: 18%;
            margin-left: 15px;
            font-size: 14px;
            border-color: #858586;
            font-weight: 600;
            border-bottom-color: #d9d9da;
            box-shadow: none;
            border-width: 1px;
        }

        .GVFixedHeader {
            font-weight: bold;
            background-color: Green;
            position: relative;
            top: expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }

        .sim {
            margin-right: -510px;
        }

        @media only screen and (max-width: 680px) {
            .sim {
                float: right;
                margin-right: -100px;
            }
        }

        .st0 {
            stroke: #5C276E !important;
        }
    </style>

    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HfPageLink" Value="PlanFinder.aspx" runat="server" />
    <div class="tab-nav-container">
        <nav class="tab-nav nav-light navbar">
            <ul class="nav">
                <li class="nav-item tab-nav-item plan-nav active">
                    <a class="nav-link" href="#">Plan</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link" ID="lnk_ScenarioList" runat="server" Text="Scenario List" OnClick="lnk_ScenarioList_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item ">
                            <asp:LinkButton CssClass="nav-link active" ID="lnk_Planfinder" runat="server" Text="Plan List" OnClick="lnk_Planfinder_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link " href="#">Market Insight</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item design-nav" id="id_Quick" runat="server">
                    <a class="nav-link" href="#">Design</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="lnk_QuickAccess" runat="server" Text="Quick Access" OnClick="lnk_QuickAccess_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">Detailed Screens</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item analyze-nav" id="id_Simulate" runat="server">
                    <a class="nav-link" href="#">Analyze</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="lnk_Simulated" runat="server" Text="Simulated Rank" OnClick="lnk_Simulated_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Market Summary</a>
                        </li>
                    </ul>
                </li>
            </ul>
        </nav>
    </div>
    <section class="content">
        <div class="scenario-table-outer-wrapper">
            <div class="scenario-table-inner-wrapper">
                <div class="scenario-table-header-wrapper">
                    <div class="scenario-table-header">
                        <h1><span class="header-label">Plan List For Scenario: </span>
                            <asp:Label Text="H5012_012_3 Change" runat="server" CssClass="header-text" ID="lblScenarioName" />
                        </h1>
                    </div>
                    <div class="scenario-table-controls">
                        <div class="filter">

                            <telerik:RadComboBox ID="ddlState" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px" DropDownCssClass="textfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></telerik:RadComboBox>
                            <telerik:RadComboBox ID="ddlCounty" Filter="Contains" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px" DropDownCssClass="textfont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged"></telerik:RadComboBox>
                            <asp:DropDownList runat="server" ID="ddlDrugCoverage" CssClass="custom-select country-select" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDrugCoverage_SelectedIndexChanged">
                            </asp:DropDownList>
                            <telerik:RadComboBox ID="ddlPlanType" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPlanType_SelectedIndexChanged"
                                EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Plan Type" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="250px">
                                <Localization AllItemsCheckedString="All Plan type Selected" ItemsCheckedString="Plan type(s) Selected"
                                    CheckAllString="Select All" />
                            </telerik:RadComboBox>
                            <asp:DropDownList runat="server" ID="ddlPersona" CssClass="custom-select country-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPersona_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div class="dropdown">
                                <a class="dropdown-toggle not-active" data-toggle="dropdown" href="#" title="Drug List">Drug List
                                    <div class="dropdown-menu" id="divDrugs" runat="server">
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="scenario-table-container" id="grdData_Div">
                    <div class="table-responsive" id="dvScroll">
                        <asp:GridView ID="grdData" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Width="100%"
                            BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                            OnSorting="grdData_Sorting" AllowSorting="true" OnDataBound="grdData_DataBound" DataKeyNames="Rank"
                            OnRowDataBound="grdData_RowDataBound" OnSelectedIndexChanging="grdData_SelectedIndexChanged">
                            <HeaderStyle CssClass="GVFixedHeader" />
                            <Columns>
                                <asp:TemplateField HeaderText="Compare and Simulate" ControlStyle-CssClass="text-center" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" Text="" OnCheckedChanged="chkRow_CheckedChanged" AutoPostBack="true" ClientIDMode="Static" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract" ControlStyle-CssClass="text-center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblContract" runat="server" Text='<%# Bind("Contract") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" ControlStyle-CssClass="text-center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPlanID" runat="server" Text='<%# Bind("[Plan_ID]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bid ID">
                                    <ItemTemplate>
                                        <asp:Label ID="LblBidID" runat="server" Text='<%# Bind("[Bid_ID]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plan Name">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPlanName" runat="server" Text='<%# Bind("[Plan_Name]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parent Organization" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPO" runat="server" Text='<%# Bind("[Parent_Organization]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="	OOPC (PMPM)" ControlStyle-CssClass="text-center" SortExpression="OOPC">
                                    <ItemTemplate>
                                        <asp:Label ID="LblGrandTotal" runat="server" Text='<%# Bind("[OOPC]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank" ControlStyle-CssClass="text-center" SortExpression="Rank">
                                    <ItemTemplate>
                                        <asp:Label ID="LblRank" runat="server" Text='<%# Bind("Rank") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <input type="hidden" id="div_position" name="div_position" />
                </div>
            </div>
            <div class="bottom-controls">
                <asp:LinkButton Text="" runat="server" CssClass="control-link refresh-link" ID="lbRevert" OnClick="lbRevert_Click" ToolTip="Revert Changes">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left"><polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" CssClass="control-link edit-link" ID="lbSaveAs" OnClick="lbSaveAs_Click" ToolTip="Save As">
                   <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor"  width="14" height="14" stroke-width="2">
                     <g>
	<path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8"/>
	<path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z"/>
	<polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	"/>
	<polyline class="st0" points="7,3.4 7,7 9.4,7 	"/>
</g>
                 </svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" CssClass="control-link edit-link" ID="lbSave" OnClick="lbSave_Click" ToolTip="Save">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save"><path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" CssClass="control-link download-link" ID="lbDownload" OnClick="lbDownload_Click" ToolTip="Download">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
                </asp:LinkButton>

                <asp:Button Text="Simulate" runat="server" ID="btnsimulate" OnClick="btnsimulate_Click" CssClass="sim btn ml-auto download-link" />
                <asp:Button Text="Market Insights" runat="server" ID="btnMarketInsights" CssClass="btn1  ml-auto download-link" disabled />

            </div>
        </div>
        <div class="scenario-table-outer-wrapper">
            <div class="scenario-table-inner-wrapper">
                <div class="scenario-table-header-wrapper">
                    <div class="scenario-table-header">
                        <h1><span class="header-label">Plan Comparision</span></h1>
                    </div>
                </div>
                <div class="scenario-table-container">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="grdData1" ClientIDMode="Static" AutoGenerateColumns="true" Font-Size="12px" Width="100%"
                            BackColor="Transparent" GridLines="None" CssClass="grid-teg table table-hover table-bordered table-dark-header table-striped"
                            OnRowDataBound="grdData1_RowDataBound">
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="bottom-controls">
                <asp:LinkButton Text="" runat="server" CssClass="control-link refresh-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left"><polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                <a href="#" class="control-link edit-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor" width="14" height="14" stroke-width="2">
                        <g>
                            <path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8" />
                            <path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z" />
                            <polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	" />
                            <polyline class="st0" points="7,3.4 7,7 9.4,7 	" />
                        </g>
                    </svg>
                </a>
                <a href="#" class="control-link save-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save">
                        <path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </a>
                <asp:LinkButton Text="" runat="server" CssClass="control-link download-link" ID="lbDownload1" ToolTip="Download" OnClick="lbDownload1_Click">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
                </asp:LinkButton>
                <asp:Button Text="Detailed Plan Comparision" runat="server" ID="btnDetailed" CssClass="btn control-link ml-auto download-link" OnClick="btnDetailed_Click" />
            </div>
        </div>
    </section>


    <div id="modalSaveAS" class="custom-modal">
        <!-- Modal content -->
        <div class="custom-modal-header">
            <span>Save Scenario As</span>
            <span class="far fa-times-circle closeModal1"></span>
        </div>
        <div class="custom-modal-content">
            <div class="row">
                <div class="col-7">
                    <asp:TextBox ID="txtScenario" CssClass="form-control" placeholder="Scenario Name" runat="server"></asp:TextBox>
                </div>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="requiredScenario" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Saveas" ControlToValidate="txtScenario"></asp:RequiredFieldValidator>
                </div>
                <div class="col-4">
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-7">
                    <asp:TextBox ID="txtScenarioDesc" TextMode="MultiLine" CssClass="form-control" placeholder="Scenario Description" runat="server"></asp:TextBox>
                </div>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="requireddesc" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Saveas" ControlToValidate="txtScenarioDesc"></asp:RequiredFieldValidator>
                </div>
                <div class="col-4">
                    <asp:Button ID="btnPopUpSave" runat="server" OnClick="btnPopUpSave_Click" Text="Save" CssClass="btn form-control" ValidationGroup="Saveas" />
                </div>
            </div>
        </div>
        <script type="text/javascript">
            // Get the modal
            var modal2 = document.getElementById('modalSaveAS');

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
            // When the user clicks on <span> (x), close the modal

            span2.onclick = function () {
                modal2.style.display = "none";
            }
        </script>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal2 = document.getElementById('modalSaveAS');

        // Get the <span> element that closes the modal
        var span2 = document.getElementsByClassName("closeModal1")[0];

        // Get the <span> element that closes the modal
        var cancel = document.getElementsByClassName("cancelBtn")[0];

        // When the user clicks the button, open the modal
        function openModal2() {
            modal2.style.display = "block";
        }
        // When the user clicks on <span> (x), close the modal
        span2.onclick = function () {
            modal2.style.display = "none";
        }
    </script>
    <div id="modalGrid" class="custom-modal">
        <!-- Modal content -->
        <div class="custom-modal-header1">
            <span>Estimated OOPC per Service Category</span>
            <span class="far fa-times-circle close2"></span>
        </div>
        <div class="custom-modal-content1">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="Gv_All_Services" ClientIDMode="Static" runat="server" AutoGenerateColumns="true" Font-Size="12px" Width="100%"
                    BackColor="Transparent" GridLines="None" CssClass="grid-teg table table-hover table-bordered table-dark-header table-striped"
                    OnRowDataBound="grdData1_RowDataBound">
                </asp:GridView>
            </div>
            <div class="bottom-controls">
                <asp:LinkButton ID="LinkButton1" Text="" runat="server" CssClass="control-link refresh-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left"><polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                <a href="#" class="control-link edit-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor" width="14" height="14" stroke-width="2">
                        <g>
                            <path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8" />
                            <path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z" />
                            <polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	" />
                            <polyline class="st0" points="7,3.4 7,7 9.4,7 	" />
                        </g>
                    </svg>
                </a>
                <a href="#" class="control-link save-link not-active">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save">
                        <path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </a>
                <asp:LinkButton Text="" runat="server" CssClass="control-link download-link" ID="lblPDownload" ToolTip="Download" OnClick="lblPDownload_Click">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal1 = document.getElementById('modalGrid');

        // Get the <span> element that closes the modal
        var span1 = document.getElementsByClassName("close2")[0];

        // Get the <span> element that closes the modal
        var cancel = document.getElementsByClassName("cancelBtn")[0];

        // When the user clicks the button, open the modal
        function openModal1() {
            modal1.style.display = "block";
        }
        // When the user clicks on <span> (x), close the modal
        span1.onclick = function () {
            modal1.style.display = "none";
        }
    </script>
</asp:Content>
