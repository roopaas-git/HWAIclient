<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="ManageScenarioList.aspx.cs" Inherits="HealthWorks.Pages.ManageScenarioList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
    <div class="tab-nav-container" id="div_head" runat="server">
        <nav class="tab-nav nav-light navbar" id="id_nav" runat="server" style="pointer-events: none;">
            <ul class="nav">
                <li class="nav-item tab-nav-item plan-nav active">
                    <a class="nav-link" href="#">Plan</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBScenarios" runat="server" Text="Scenario List" OnClick="LBScenarios_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item ">
                            <asp:LinkButton CssClass="nav-link" ID="LBPlans" runat="server" Text="Plan List" OnClick="LBPlans_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Market Insight</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item design-nav" id="id_Quick" runat="server">
                    <a class="nav-link" href="#">Design</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBQuickAccess" runat="server" Text="Quick Access" OnClick="LBQuickAccess_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Detailed Screens</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item analyze-nav" id="id_Simulate" runat="server">
                    <a class="nav-link" href="#">Analyze</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBSimulatedOutput" runat="server" Text="Simulated Rank" OnClick="LBSimulatedOutput_Click"></asp:LinkButton>
                        </li>
                        <%-- <li class="nav-item">
                            <a class="nav-link not-active" href="#">Market Summary</a>
                        </li>--%>
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
                        <h1><span class="header-label">Scenario List </span>
                        </h1>
                        <div class="scenario-table-controls">
                            <div class="link-wrapper">
                                <asp:LinkButton Text="" runat="server" ToolTip="Create New Scenario" CssClass="external-link" ID="lbCreate" OnClick="lbCreate_Click">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-plus-circle">
                                    <circle cx="12" cy="12" r="10"></circle><line x1="12" y1="8" x2="12" y2="16"></line><line x1="8" y1="12" x2="16" y2="12"></line></svg>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="scenario-table-container">
                    <div class="table-responsive" id="dvScroll" onscroll="setScrollPosition(this.scrollTop);">
                        <asp:GridView ID="grdScenario" runat="server" AutoGenerateColumns="False" Width="100%"
                            BackColor="Transparent" GridLines="None" CssClass="table table-hover table-striped scenario-table"
                            OnSelectedIndexChanging="grdScenario_SelectedIndexChanging"
                            OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" OnRowDataBound="grdScenario_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rbSelect" runat="server" Text="" OnCheckedChanged="rbSelect_CheckedChanged" AutoPostBack="true" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:RadioButton ID="rbSelect" runat="server" Enabled="false" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("ScenarioId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Scenario" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="LblScenarioName" runat="server" Text='<%# Bind("ScenarioName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtScenarioName" runat="server" Text='<%# Eval("ScenarioName") %>'></asp:TextBox>
                                        <asp:Label ID="oldValue" runat="server" Visible="false" Text='<%# Bind("ScenarioName") %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="LblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shared By">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSharedBy" runat="server" Text='<%# Bind("SharedBy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shared Description">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSharedDescription" runat="server" Text='<%# Bind("SharedDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modified Date">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPlanName" runat="server" Text='<%# Bind("ModifiedDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        <asp:Label ID="LblShareCount" Visible="false" runat="server" Text='<%# Bind("ShareCount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" Text="" runat="server" CommandName="Edit" CausesValidation="False">
                                        <i class="fas fa-pen"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" Text="" runat="server" CommandName="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this scenario?');">
                                            <i class="far fa-trash-alt"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="Lnk_Share" Text="" runat="server" CommandName="Select" CausesValidation="False" OnClick="Share_Click">
                                           <i class="fa fa-share-alt" ></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" Text="" runat="server" CommandName="Update" CausesValidation="False">
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
        <div class="action-btn-wrapper">
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
                    <asp:TextBox ID="txtScenario" CssClass="form-control" placeholder="Scenario Name" runat="server"></asp:TextBox>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"  ForeColor="Red" ValidationGroup="Shareplan" InitialValue="0" ControlToValidate="ddlUser"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                    <asp:Label ID="lblMessage" CssClass="form-control-label" runat="server" Text="Message:"></asp:Label>
                </div>
                <div class="col-7">
                    <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Please provide Message"></asp:TextBox>

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

</asp:Content>
