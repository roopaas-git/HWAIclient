<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="QuickAccess.aspx.cs" Inherits="HealthWorks.Pages.QuickAccess" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <script src="../dist/js/QuickAccessValidations.js"></script>
    <style type="text/css">
        table {
            border-collapse: collapse;
        }

        .btn-warning {
            background-color: #f0ad4e;
            border: 2px solid;
            border-radius: 5px;
            width: 125px;
            height: 25px;
            cursor: pointer;
        }

            .btn-warning:hover {
                color: Black;
                background-color: #ec971f;
                border-color: #d58512;
            }

        .HeaderCSS {
            height: 40px;
            background-repeat: no-repeat;
            background-image: url('../dist/Images/Plus-black.png');
            background-position: 5px;
            border-color: #D2DFEF;
            color: Black;
            padding: 5px;
            text-align: left;
            vertical-align: middle;
            cursor: pointer;
            background-color: rgba(0,0,0,.03) !important;
            font-size: 14px;
        }

        .HeaderSelectedCSS {
            height: 40px;
            text-align: left;
            padding: 5px;
            background-image: url('../dist/Images/Minus-purple.png');
            background-repeat: no-repeat;
            background-position: 5px;
            color: #5c276e;
            vertical-align: middle;
            border-color: #D2DFEF;
            background-color: rgba(0,0,0,.03) !important;
            font-size: 14px;
            cursor: pointer;
            font-weight: bold;
        }

        .ContentCSS {
            padding-left: 1.25rem;
            padding-right: 1.25rem;
            padding-top: 1.25rem;
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

        .close3 {
            float: right;
            font-size: 21px;
            font-weight: 700;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #fff;
            filter: alpha(opacity=20);
            opacity: .2;
            cursor: pointer;
        }

        #UpdatePanel1 {
            width: 100% !important;
        }

        .accordianHeading {
            padding: 5px;
        }

        .accordian {
            border-radius: calc(.25rem - 1px) calc(.25rem - 1px) 0 0;
            margin-bottom: 2px;
        }

        .accordianIcon {
            padding-left: 5px;
            font-size: 14px;
        }

        .accordianHeading span {
            padding: 10px;
            padding-left: 20px;
        }

        .rbFonts {
            font-size: 14px !important;
        }

        .RadComboBox {
            width: 100% !important;
        }

        .rbFonts label {
            display: inline-block;
            padding: 4px 11px;
        }

        .st0 {
            stroke: #5C276E !important;

        }

        .txtalgn {
            text-align: center;
        }
    </style>
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
    <asp:HiddenField ID="HfPageLink" Value="QuickAccess.aspx" runat="server" />
    <div class="tab-nav-container">
        <nav class="tab-nav nav-light navbar">
            <ul class="nav">
                <li class="nav-item tab-nav-item plan-nav">
                    <a class="nav-link" href="#">Plan</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link" ID="lnk_ScenarioList" runat="server" Text="Scenario List" OnClick="lnk_ScenarioList_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link" ID="lnk_Planfinder" runat="server" Text="Plan List" OnClick="lnk_Planfinder_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link not-active" href="#">Market Insight</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item design-nav active">
                    <a class="nav-link" href="#">Design</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="lnk_QuickAccess" runat="server" Text="Quick Access" OnClick="lnk_QuickAccess_Click"></asp:LinkButton>
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
                            <asp:LinkButton CssClass="nav-link" ID="lnk_Simulated" runat="server" Text="Simulated Rank" OnClick="lnk_Simulated_Click"></asp:LinkButton>
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
                <div class="scenario-table-header">
                    <div class="row">
                        <div class="col-7">
                            <h1><span class="header-label">Scenario: </span>
                                <asp:Label ID="lblScenarioName" Text="" runat="server" CssClass="header-text" />
                            </h1>
                        </div>
                        <div class="col-1">
                        </div>
                        <div class="col-4">
                            <asp:DropDownList ID="ddlBidId" Style="float: left;" runat="server" AutoPostBack="true" CssClass="custom-select state-select" OnSelectedIndexChanged="ddlBidId_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-2" hidden>
                            <asp:LinkButton Text="" runat="server" ID="btnback" CssClass="control-link refresh-link" OnClick="btnback_Click" ToolTip="Back">
                  <svg xmlns="http://www.w3.org/2000/svg" style="float: right; margin-top: 0px; color:#f26b26;" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left-circle"><circle cx="12" cy="12" r="10"></circle><polyline points="12 8 8 12 12 16"></polyline><line x1="16" y1="12" x2="8" y2="12"></line></svg>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="scenario-table-container">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                        <ContentTemplate>
                            <asp:Accordion ID="Accordion1" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                <Panes>
                                    <asp:AccordionPane runat="server" ID="AccordionPane1">
                                        <Header>
                                            <div id="div3" class="accordianHeading" runat="server">
                                                <a style="text-decoration: none"><%--<i class="fas fa-plus accordianIcon"></i>--%><span>Inpatient Hospital Acute Services</span></a>
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label9" Text="Inpatient Hospital-Acute benefit period :" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b1a_hosp_ben_period" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label32" Text="Do you charge cost sharing on the day of discharge? " runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b1a_cost_discharge_yn" CssClass="custom-select state-select">
                                                        <asp:ListItem Text="Select" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:RadioButtonList ID="rd_HospitalAcute" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rd_HospitalAcute_SelectedIndexChanged"
                                                        AutoPostBack="True" ValidationGroup="Search" CssClass="rbFonts">
                                                        <asp:ListItem Text="Coinsurance" Value="Coinsurance"></asp:ListItem>
                                                        <asp:ListItem Text="Copayment" Value="Copayment"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                                <div class="col-2">
                                                    <asp:Label ID="Label1" Text="Medicare Defined" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b1a_mc_copay_cstshr_yn_t1" CssClass="custom-select state-select" OnSelectedIndexChanged="ddlpbp_b1a_mc_copay_cstshr_yn_t1_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Select" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-6">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label78" Text="Coins percentage for Medicare covered stay" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b1a_coins_mcs_pct_t1" runat="server" CssClass="form-control"> </asp:TextBox>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label79" Text="Copay amount for Medicare covered stay" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b1a_copay_mcs_amt_t1" runat="server" CssClass="form-control"> </asp:TextBox>

                                                </div>
                                                <div class="col-3">
                                                </div>
                                                <div class="col-3">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label2" Text="Coinsurance % Interval 1" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b1a_coins_mcs_pct_int1_t1" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b1a_coins_mcs_pct_int1_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label3" Text="Copayment Amt Interval 1" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b1a_copay_mcs_amt_int1_t1" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b1a_copay_mcs_amt_int1_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>

                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label4" Text="Begin Day Interval 1:" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtBegin1Acute" runat="server" CssClass="form-control" ClientIDMode="Static" onChange="checkCompare1(this)"> </asp:TextBox>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="Insert"
                                                        ControlToValidate="txtBegin1Acute" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label5" Text="End Day Interval 1:" runat="server" CssClass="form-control-label" />

                                                    <asp:TextBox ID="txtEnd1Acute" runat="server" CssClass="form-control" ClientIDMode="Static" onChange="checkCompare1(this)"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="Insert"
                                                        ControlToValidate="txtEnd1Acute" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label6" Text="Coinsurance % Interval 2" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b1a_coins_mcs_pct_int2_t1" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b1a_coins_mcs_pct_int2_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label7" Text="Copayment Amt Interval 2" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b1a_copay_mcs_amt_int2_t1" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b1a_copay_mcs_amt_int2_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label8" Text="Begin Day Interval 2:" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtBegin2Acute" runat="server" CssClass="form-control" ClientIDMode="Static" onChange="checkCompare1(this)"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="Insert"
                                                        ControlToValidate="txtBegin2Acute" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label10" Text="End Day Interval 2:" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtEnd2Acute" runat="server" CssClass="form-control" ClientIDMode="Static" onChange="checkCompare1(this)"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="Insert"
                                                        ControlToValidate="txtEnd2Acute" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label11" Text="Coinsurance % Interval 3" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b1a_coins_mcs_pct_int3_t1" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b1a_coins_mcs_pct_int3_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label12" Text="Copayment Amt Interval 3" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b1a_copay_mcs_amt_int3_t1" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b1a_copay_mcs_amt_int3_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label13" Text="Begin Day Interval 3:" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtBegin3Acute" runat="server" CssClass="form-control" ClientIDMode="Static" onChange="checkCompare1(this)"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ValidationGroup="Insert"
                                                        ControlToValidate="txtBegin3Acute" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                    <br />
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label14" Text="End Day Interval 3:" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtEnd3Acute" runat="server" CssClass="form-control" ClientIDMode="Static" onChange="checkCompare1(this)"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="Insert"
                                                        ControlToValidate="txtEnd3Acute" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                    <br />
                                                </div>
                                            </div>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                            <asp:Accordion ID="Accordion2" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                <Panes>
                                    <asp:AccordionPane runat="server" ID="AccordionPane2">
                                        <Header>
                                            <div id="div1" class="accordianHeading" runat="server">
                                                <a style="text-decoration: none"><span>Outpatient Hospital Services</span></a>
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="row">
                                                <div class="col-4">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label27" Text="Coinsurance" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label28" Text="Copayment" runat="server" CssClass="form-control-label" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label29" Text="Minimum" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>

                                                <div class="col-2">
                                                    <%--  <asp:Label ID="Label31" Text="Maximum" runat="server" CssClass="form-control-label lblTeg" />--%>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label30" Text="Minimum" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>
                                                <div class="col-2">
                                                    <%--  <asp:Label ID="Label32" Text="Maximum" runat="server" CssClass="form-control-label lblTeg" />--%>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label37" Text="Surgery" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Surgery_coin" runat="server" AutoPostBack="true"
                                                        OnCheckedChanged="chk_Surgery_coin_CheckedChanged"></asp:CheckBox>
                                                    <asp:TextBox ID="txtpbp_b9a_coins_ohs_pct_min" CssClass="textbx" runat="server" onChange="checkOutpatientCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator24" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9a_coins_ohs_pct_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <%--  <asp:TextBox ID="txtpbp_b9a_coins_ohs_pct_max" CssClass="textbx" runat="server" onChange="checkOutpatientCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9a_coins_ohs_pct_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>--%>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Surgery_copay" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Surgery_copay_CheckedChanged" />
                                                    <asp:TextBox ID="txtpbp_b9a_copay_ohs_amt_min" CssClass="textbx" runat="server" onChange="checkOutpatientCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9a_copay_ohs_amt_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>

                                                </div>
                                                <div class="col-2">
                                                    <%--  <asp:TextBox ID="txtpbp_b9a_copay_ohs_amt_max" CssClass="textbx" runat="server" onChange="checkOutpatientCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9a_copay_ohs_amt_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>--%>
                                                </div>
                                            </div>
                                            <div class="row" hidden>
                                                <div class="col-3">
                                                    <asp:Label ID="Label33" Text="Observation" runat="server"
                                                        CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Observ_coin" runat="server"></asp:CheckBox>
                                                    <asp:TextBox ID="txtpbp_b9a_coins_obs_pct_min" CssClass="textbx" runat="server" onChange="checkOutpatientCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9a_coins_obs_pct_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b9a_coins_obs_pct_max" CssClass="textbx" runat="server" onChange="checkOutpatientCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9a_coins_obs_pct_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Observ_copay" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b9a_copay_obs_amt_min" CssClass="textbx" runat="server" onChange="checkOutpatientCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9a_copay_obs_amt_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b9a_copay_obs_amt_max" CssClass="textbx" runat="server" onChange="checkOutpatientCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9a_copay_obs_amt_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-3">
                                                    <asp:Label ID="Label34" Text="Ambulatory Surgical Center" runat="server"
                                                        CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_pbp_b9b_coins_yn" runat="server" AutoPostBack="true"
                                                        OnCheckedChanged="chk_pbp_b9b_coins_yn_CheckedChanged"></asp:CheckBox>
                                                    <asp:TextBox ID="txtpbp_b9b_coins_pct_mc" CssClass="textbx" runat="server" onChange="checkOutpatientCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9b_coins_pct_mc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <%-- <asp:TextBox ID="txt_Ambulatory_Coin__Max" CssClass="textbx" runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" ValidationGroup="Insert"
                                                        ControlToValidate="txt_Ambulatory_Coin__Max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>--%>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Ambulatory_copay" runat="server" AutoPostBack="true"
                                                        OnCheckedChanged="chk_Ambulatory_copay_CheckedChanged" />
                                                    <asp:TextBox ID="txtpbp_b9b_copay_mc_amt" CssClass="textbx" runat="server" onChange="checkOutpatientCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b9b_copay_mc_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <%-- <asp:TextBox ID="txt_Ambulatory_copay_Max" CssClass="textbx" runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" ValidationGroup="Insert"
                                                        ControlToValidate="txt_Ambulatory_copay_Max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>--%>
                                                </div>
                                            </div>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                            <asp:Accordion ID="Accordion3" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                <Panes>
                                    <asp:AccordionPane runat="server" ID="AccordionPane3">
                                        <Header>
                                            <div id="div2" class="accordianHeading" runat="server">
                                                <a style="text-decoration: none"><span>Preventive Dental Services</span></a>
                                            </div>
                                        </Header>
                                        <Content>
                                            <%--  <div class="row" hidden>
                                                <div class="col-3">
                                                    <asp:Label ID="Label35" Text="Deductible amount:" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:TextBox ID="txtpbp_b16a_ded_amt" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator25" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_ded_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                </div>
                                                <div class="col-3">
                                                </div>
                                            </div>--%>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label38" Text="Plan benefit coverage amount and periodicity" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:TextBox ID="txtpbp_b16a_maxplan_amt" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator26" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_maxplan_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16a_maxplan_per" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-3">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label36" Text="Coinsurance" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label39" Text="Copayment" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label40" Text="Visits" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label41" Text="Periodicity" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label80" Text="Types of Benefit" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label46" Text="Oral Exam" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chck_Oral_Coin" AutoPostBack="true" OnCheckedChanged="chck_Oral_Coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16a_coins_pct_oe" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_coins_pct_oe" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chck_Oral_Copay" AutoPostBack="true" OnCheckedChanged="chck_Oral_Copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16a_copay_amt_oemin" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator28" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_copay_amt_oemin" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b16a_bendesc_numv_oe" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator29" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_bendesc_numv_oe" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_per_oe" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_amo_oe" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label42" Text="Dental X-Rays" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chck_Denatl_Coin" AutoPostBack="true" OnCheckedChanged="chck_Denatl_Coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16a_coins_pct_dx" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator30" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_coins_pct_dx" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chck_Denatl_Copay" AutoPostBack="true" OnCheckedChanged="chck_Denatl_Copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16a_copay_amt_dxmin" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator31" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_copay_amt_dxmin" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b16a_bendesc_numv_dx" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator32" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_bendesc_numv_dx" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_per_dx" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_amo_dx" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label43" Text="Prophylaxis (Cleaning)" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chck_Prophylaxis_Coin" AutoPostBack="true" OnCheckedChanged="chck_Prophylaxis_Coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16a_coins_pct_pc" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator33" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_coins_pct_pc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chck_Prophylaxis_Copay" AutoPostBack="true" OnCheckedChanged="chck_Prophylaxis_Copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16a_copay_amt_pcmin" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator34" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_copay_amt_pcmin" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b16a_bendesc_numv_pc" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator35" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16a_bendesc_numv_pc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_per_pc" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlPBP_B16A_BENDESC_AMO_PC" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                            <asp:Accordion ID="Accordion4" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                <Panes>
                                    <asp:AccordionPane runat="server" ID="AccordionPane4">
                                        <Header>
                                            <div id="div4" class="accordianHeading" runat="server">
                                                <a style="text-decoration: none"><span>Comprehensive Dental Services</span></a>
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label108" Text="Maximum Plan Benefit Coverage Type" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16b_maxbene_type" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-3">
                                                </div>
                                                <div class="col-3">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label31" Text="Is benefit Mandatory" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label47" Text="Coinsurance" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label48" Text="Copayment" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label49" Text="Visits" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label50" Text="Periodicity" runat="server" CssClass="form-control-label" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label83" Text="Medicare Covered Benefits" runat="server" CssClass="form-control-label" />

                                                </div>
                                                <div class="col-2">
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Medicare_Coin" AutoPostBack="true" OnCheckedChanged="chk_Medicare_Coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtPBP_B16B_COINS_PCT_MC_MIN" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator108" ValidationGroup="Insert"
                                                        ControlToValidate="txtPBP_B16B_COINS_PCT_MC_MIN" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Medicare_Copay" AutoPostBack="true" OnCheckedChanged="chk_Medicare_Copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtPBP_B16B_COPAY_AMT_MC_MIN" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator109" ValidationGroup="Insert"
                                                        ControlToValidate="txtPBP_B16B_COPAY_AMT_MC_MIN" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <br />
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="col-2">
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label51" Text="Restorative Services" runat="server" CssClass="form-control-label" />

                                                </div>
                                                <div class="col-2 txtalgn">
                                                    <%-- <asp:CheckBox ID="chk_PBP_B16B_BENDESC_AMO_RS" AutoPostBack="true"  runat="server" />--%>
                                                    <asp:DropDownList runat="server" ID="ddlPBP_B16B_BENDESC_AMO_RS" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Restorative_Coin" AutoPostBack="true" OnCheckedChanged="chk_Restorative_Coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_coins_pct_rs_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator38" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_coins_pct_rs_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Restorative_Copay" AutoPostBack="true" OnCheckedChanged="chk_Restorative_Copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_copay_amt_rs_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator39" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_copay_amt_rs_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b16b_bendesc_numv_rs" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator40" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_bendesc_numv_rs" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16b_bendesc_per_rs" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label52" Text="Endodontics" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2 txtalgn">
                                                    <%-- <asp:CheckBox ID="chk_PBP_B16B_BENDESC_AMO_END" AutoPostBack="true" runat="server" />--%>
                                                    <asp:DropDownList runat="server" ID="ddlPBP_B16B_BENDESC_AMO_END" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Endodontics_Coin" AutoPostBack="true" OnCheckedChanged="chk_Endodontics_Coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_coins_pct_end_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator41" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_coins_pct_end_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Endodontics_Copay" AutoPostBack="true" OnCheckedChanged="chk_Endodontics_Copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_copay_amt_end_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator42" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_copay_amt_end_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b16b_bendesc_num_end" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator43" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_bendesc_num_end" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16b_bendesc_per_end" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label53" Text="Periodontics" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2 txtalgn">
                                                    <%-- <asp:CheckBox ID="chk_PBP_B16B_BENDESC_AMO_PERI" AutoPostBack="true"  runat="server" />--%>
                                                    <asp:DropDownList runat="server" ID="ddlPBP_B16B_BENDESC_AMO_PERI" CssClass="custom-select state-select">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Periodontics_coin" AutoPostBack="true" OnCheckedChanged="chk_Periodontics_coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_coins_pct_peri_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator44" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_coins_pct_peri_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Periodontics_copay" AutoPostBack="true" OnCheckedChanged="chk_Periodontics_copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_copay_amt_peri_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator45" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_copay_amt_peri_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b16b_bendesc_num_peri" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator46" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_bendesc_num_peri" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16b_bendesc_per_peri" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label54" Text="Extractions" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2 txtalgn">
                                                    <%-- <asp:CheckBox ID="chk_PBP_B16B_BENDESC_AMO_EXT"  AutoPostBack="true" runat="server" />--%>
                                                    <asp:DropDownList runat="server" ID="ddlPBP_B16B_BENDESC_AMO_EXT" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Extractions_coin" AutoPostBack="true" OnCheckedChanged="chk_Extractions_coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_coins_pct_ext_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator47" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_coins_pct_ext_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Extractions_copay" AutoPostBack="true" OnCheckedChanged="chk_Extractions_copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_copay_amt_ext_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator48" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_copay_amt_ext_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b16b_bendesc_num_ext" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator49" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_bendesc_num_ext" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16b_bendesc_per_ext" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label55" Text="Prosthodontics, Other Oral/Maxillofacial Surgery, Other Services" runat="server" CssClass="form-control-label" />
                                                    <br />
                                                </div>
                                                <div class="col-2 txtalgn">
                                                    <%--  <asp:CheckBox ID="chk_PBP_B16B_BENDESC_AMO_POO" AutoPostBack="true"  runat="server" />--%>
                                                    <asp:DropDownList runat="server" ID="ddlPBP_B16B_BENDESC_AMO_POO" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Prosthodontics_coin" AutoPostBack="true" OnCheckedChanged="chk_Prosthodontics_coin_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_coins_pct_poo_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator50" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_coins_pct_poo_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:CheckBox ID="chk_Prosthodontics_copay" AutoPostBack="true" OnCheckedChanged="chk_Prosthodontics_copay_CheckedChanged" runat="server" />
                                                    <asp:TextBox ID="txtpbp_b16b_copay_amt_poo_min" runat="server" CssClass="textbx"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_copay_amt_poo_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b16b_bendesc_numv_poo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator52" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b16b_bendesc_numv_poo" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList runat="server" ID="ddlpbp_b16b_bendesc_per_poo" CssClass="custom-select state-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                            <asp:Accordion ID="Accordion5" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                <Panes>
                                    <asp:AccordionPane runat="server" ID="AccordionPane5">
                                        <Header>
                                            <div id="div5" class="accordianHeading" runat="server">
                                                <a style="text-decoration: none"><span>Emergency Room</span></a>
                                            </div>
                                        </Header>
                                        <Content>

                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label58" Text="Indicate Coinsurance percentage/Copayment amount for Medicare covered Benefits" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label63" Text="Coinsurance" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b4a_coins_pct_mc_min" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator54" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b4a_coins_pct_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label64" Text="Copayment" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b4a_copay_amt_mc_min" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator55" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b4a_copay_amt_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                    <br />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label107" Text="Maximum per visit Amount"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:TextBox ID="txtpbp_b4a_max_visit" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator53" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b4a_max_visit" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                </div>
                                            </div>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                            <asp:Accordion ID="Accordion6" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                <Panes>
                                    <asp:AccordionPane runat="server" ID="AccordionPane6">
                                        <Header>
                                            <div id="div6" class="accordianHeading" runat="server">
                                                <a style="text-decoration: none"><span>Speciality Care Physician</span></a>
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="row">
                                                <div class="col-6">
                                                    <asp:Label ID="Label61" Text="Indicate Coinsurance percentage/Copayment amount for Medicare covered Benefits" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label62" Text="Coinsurance" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b7d_coins_pct_mc_min" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator56" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b7d_coins_pct_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label65" Text="Copayment" runat="server" CssClass="form-control-label" />
                                                    <asp:TextBox ID="txtpbp_b7d_copay_amt_mc_min" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator57" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b7d_copay_amt_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                            <asp:Accordion ID="Accordion7" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                <Panes>
                                    <asp:AccordionPane runat="server" ID="AccordionPane7">
                                        <Header>
                                            <div id="div7" class="accordianHeading" runat="server">
                                                <a style="text-decoration: none"><span>Radiology</span></a>
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="row">
                                                <div class="col-9">
                                                    <asp:Label ID="Label74" Text="If a member receives multiple services at the same location on the same day, does only the maximum copay apply?"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:RadioButtonList ID="rd_pbp_b8b_copay_max_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts">
                                                        <asp:ListItem Text="Yes" />
                                                        <asp:ListItem Text="No" />
                                                        <asp:ListItem Text="" style="visibility:hidden;" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-4">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label60" Text="Coinsurance" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label66" Text="Copayment" runat="server" CssClass="form-control-label" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label67" Text="Minimum" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>

                                                <div class="col-2">
                                                    <asp:Label ID="Label68" Text="Maximum" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label69" Text="Minimum" runat="server" CssClass="form-control-label lblTeg" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label70" Text="Maximum" runat="server" CssClass="form-control-label" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label71" Text="X-Ray" runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:CheckBox ID="chck_XRay_Coin" runat="server" AutoPostBack="true"
                                                        OnCheckedChanged="chck_XRay_Coin_CheckedChanged"></asp:CheckBox>
                                                    <asp:TextBox ID="txtpbp_b8b_coins_pct_cmc" CssClass="textbx" runat="server" onChange="checkradiologyCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator58" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_coins_pct_cmc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>

                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b8b_coins_pct_cmc_max" CssClass="textbx" runat="server" onChange="checkradiologyCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator59" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_coins_pct_cmc_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>

                                                <div class="col-3">
                                                    <asp:CheckBox ID="chck_XRay_Copay" runat="server" AutoPostBack="true" OnCheckedChanged="chck_XRay_Copay_CheckedChanged" />
                                                    <asp:TextBox ID="txtpbp_b8b_copay_mc_amt" CssClass="textbx" runat="server" onChange="checkradiologyCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator60" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_copay_mc_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b8b_copay_mc_amt_max" CssClass="textbx" runat="server" onChange="checkradiologyCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator61" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_copay_mc_amt_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-2">
                                                    <asp:Label ID="Label72" Text="Therapeutic Radiology" runat="server"
                                                        CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:CheckBox ID="chck_Therapeutic_Coin" runat="server"
                                                        AutoPostBack="true" OnCheckedChanged="chck_Therapeutic_Coin_CheckedChanged"></asp:CheckBox>
                                                    <asp:TextBox ID="txtpbp_b8b_coins_pct_tmc" CssClass="textbx" runat="server" onChange="checkradiologyCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator62" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_coins_pct_tmc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b8b_coins_pct_tmc_max" CssClass="textbx" runat="server" onChange="checkradiologyCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator63" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_coins_pct_tmc_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:CheckBox ID="chck_Therapeutic_Copay" runat="server" AutoPostBack="true" OnCheckedChanged="chck_Therapeutic_Copay_CheckedChanged" />
                                                    <asp:TextBox ID="txtpbp_b8b_copay_amt_tmc" CssClass="textbx" runat="server" onChange="checkradiologyCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator64" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_copay_amt_tmc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b8b_copay_amt_tmc_max" CssClass="textbx" runat="server" onChange="checkradiologyCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator65" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_copay_amt_tmc_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="row form-group">
                                                <div class="col-2">
                                                    <asp:Label ID="Label73" Text="Diagnostic Radiology" runat="server"
                                                        CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:CheckBox ID="chck_Diagnostic_Coin" runat="server" OnCheckedChanged="chck_Diagnostic_Coin_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                    <asp:TextBox ID="txtpbp_b8b_coins_pct_drs" CssClass="textbx" runat="server" onChange="checkradiologyCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator66" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_coins_pct_drs" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b8b_coins_pct_drs_max" CssClass="textbx" runat="server" onChange="checkradiologyCoins(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator67" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_coins_pct_drs_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:CheckBox ID="chck_Diagnostic_Copay" runat="server" AutoPostBack="true"
                                                        OnCheckedChanged="chck_Diagnostic_Copay_CheckedChanged" />
                                                    <asp:TextBox ID="txtpbp_b8b_copay_amt_drs" CssClass="textbx" runat="server" onChange="checkradiologyCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator68" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_copay_amt_drs" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox ID="txtpbp_b8b_copay_amt_drs_max" CssClass="textbx" runat="server" onChange="checkradiologyCopay(this)"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator69" ValidationGroup="Insert"
                                                        ControlToValidate="txtpbp_b8b_copay_amt_drs_max" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                            <asp:Accordion ID="Accordion8" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                <Panes>
                                    <asp:AccordionPane runat="server" ID="AccordionPane8">
                                        <Header>
                                            <div id="div8" class="accordianHeading" runat="server">
                                                <a style="text-decoration: none"><span>MRx</span></a>
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label75" Text="Part D Benefit Type"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList ID="ddlmrx_benefit_type" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label85" Text="Deductible"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:TextBox ID="txtmrx_alt_ded_amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator70" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_alt_ded_amount" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label76" Text="Initial Coverage Limit"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <asp:TextBox ID="txtmrx_alt_cov_lmt_amt" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator71" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_alt_cov_lmt_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-3">
                                                    <asp:Label ID="Label82" Text="Indicate each tier for which deductible will NOT apply"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-3">
                                                    <telerik:RadComboBox ID="ddlmrx_alt_no_ded_tier" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                                        EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Tier" EnableEmbeddedSkins="false" Skin="Test" DropDownWidth="230px">
                                                        <Localization AllItemsCheckedString="All Tier Selected" ItemsCheckedString="Tier(s) Selected"
                                                            CheckAllString="Select All" />
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:Label ID="Label81" Text="ICL Cost Sharing"
                                                        runat="server" CssClass="form-control-label lblTegHeading" />
                                                </div>
                                                <div class="col-3">
                                                </div>
                                                <div class="col-3">
                                                </div>
                                                <div class="col-3">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label94" Text="INP"
                                                        runat="server" CssClass="form-control-label txtalgn" />
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label25" Text="INNPP"
                                                        runat="server" CssClass="form-control-label txtalgn" />
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label26" Text="INPP"
                                                        runat="server" CssClass="form-control-label txtalgn" />
                                                </div>
                                                <div class="col-2" hidden>
                                                    <asp:Label ID="Label90" Text="OONP"
                                                        runat="server" CssClass="form-control-label txtalgn" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-1">
                                                </div>
                                                <div class="col-2">
                                                    <asp:Label ID="Label24" Text="Cost Share Type"
                                                        runat="server" CssClass="form-control-label " />
                                                </div>
                                                <div class="col-1">
                                                    <asp:Label ID="Label89" Text="Coinsurance"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1">
                                                    <asp:Label ID="Label19" Text="Copayment"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>

                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:Label ID="Label20" Text="Coinsurance"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1">
                                                    <asp:Label ID="Label21" Text="Copayment"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:Label ID="Label22" Text="Coinsurance"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1">
                                                    <asp:Label ID="Label23" Text="Copayment"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:Label ID="Label87" Text="Coinsurance"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:Label ID="Label88" Text="Copayment"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:Label ID="Label84" Text="MRx Tier Gap Cost Share"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:Label ID="Label86" Text="MRx Tier Includes"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-1">
                                                    <asp:Label ID="Label77" Text="Tier 1"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList ID="ddl1mrx_tier_cstshr_struct_type_tier1" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_coins_1m_tier1" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator72" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_coins_1m_tier1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_copay_1m_tier1" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator73" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_copay_1m_tier1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_coins_1m_tier1" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator74" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_coins_1m_tier1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_copay_1m_tier1" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator75" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_copay_1m_tier1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_coins_1m_tier1" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator76" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_coins_1m_tier1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_copay_1m_tier1" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator77" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_copay_1m_tier1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_coins_1m_tier1" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_copay_1m_tier1" CssClass="form-control-MRX" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlmrx_tier_gap_cost_share_tier1" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlMRX_TIER_INCLUDES_tier1" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-1">
                                                    <asp:Label ID="Label15" Text="Tier 2"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList ID="ddl1mrx_tier_cstshr_struct_type_tier2" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_coins_1m_tier2" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator78" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_coins_1m_tier2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_copay_1m_tier2" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator79" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_copay_1m_tier2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_coins_1m_tier2" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator80" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_coins_1m_tier2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_copay_1m_tier2" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator81" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_copay_1m_tier2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_coins_1m_tier2" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator82" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_coins_1m_tier2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_copay_1m_tier2" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator83" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_copay_1m_tier2" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_coins_1m_tier2" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_copay_1m_tier2" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlmrx_tier_gap_cost_share_tier2" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlMRX_TIER_INCLUDES_tier2" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-1">
                                                    <asp:Label ID="Label16" Text="Tier 3"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList ID="ddl1mrx_tier_cstshr_struct_type_tier3" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_coins_1m_tier3" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator84" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_coins_1m_tier3" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_copay_1m_tier3" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator85" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_copay_1m_tier3" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_coins_1m_tier3" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator86" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_coins_1m_tier3" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_copay_1m_tier3" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator87" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_copay_1m_tier3" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_coins_1m_tier3" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator88" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_coins_1m_tier3" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_copay_1m_tier3" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator89" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_copay_1m_tier3" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>

                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_coins_1m_tier3" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_copay_1m_tier3" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlmrx_tier_gap_cost_share_tier3" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlMRX_TIER_INCLUDES_tier3" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-1">
                                                    <asp:Label ID="Label17" Text="Tier 4"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList ID="ddl1mrx_tier_cstshr_struct_type_tier4" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_coins_1m_tier4" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator90" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_coins_1m_tier4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_copay_1m_tier4" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator91" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_copay_1m_tier4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_coins_1m_tier4" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator92" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_coins_1m_tier4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_copay_1m_tier4" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator93" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_copay_1m_tier4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_coins_1m_tier4" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator94" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_coins_1m_tier4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_copay_1m_tier4" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator95" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_copay_1m_tier4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_coins_1m_tier4" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_copay_1m_tier4" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlmrx_tier_gap_cost_share_tier4" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlMRX_TIER_INCLUDES_tier4" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-1">
                                                    <asp:Label ID="Label18" Text="Tier 5"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList ID="ddl1mrx_tier_cstshr_struct_type_tier5" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_coins_1m_tier5" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator96" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_coins_1m_tier5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_copay_1m_tier5" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator98" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_copay_1m_tier5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_coins_1m_tier5" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator97" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_coins_1m_tier5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_copay_1m_tier5" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator99" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_copay_1m_tier5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_coins_1m_tier5" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator100" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_coins_1m_tier5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_copay_1m_tier5" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator101" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_copay_1m_tier5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_coins_1m_tier5" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_copay_1m_tier5" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlmrx_tier_gap_cost_share_tier5" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlMRX_TIER_INCLUDES_tier5" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-1">
                                                    <asp:Label ID="Label56" Text="Tier 6"
                                                        runat="server" CssClass="form-control-label" />
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList ID="ddl1mrx_tier_cstshr_struct_type_tier6" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_coins_1m_tier6" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator102" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_coins_1m_tier6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rstd_copay_1m_tier6" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator103" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rstd_copay_1m_tier6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_coins_1m_tier6" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator104" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_coins_1m_tier6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rsstd_copay_1m_tier6" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator105" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rsstd_copay_1m_tier6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_coins_1m_tier6" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator106" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_coins_1m_tier6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1">
                                                    <asp:TextBox ID="txtmrx_tier_rspfd_copay_1m_tier6" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator107" ValidationGroup="Insert"
                                                        ControlToValidate="txtmrx_tier_rspfd_copay_1m_tier6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ValidationExpression="^[0-9]\d*(\.\d+)?$">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_coins_1m_tier6" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:TextBox ID="txtmrx_tier_oonp_copay_1m_tier6" CssClass="form-control-MRX" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlmrx_tier_gap_cost_share_tier6" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-1" hidden>
                                                    <asp:DropDownList ID="ddlMRX_TIER_INCLUDES_tier6" CssClass="custom-select state-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>


                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="bottom-controls">
                <asp:LinkButton Text="" runat="server" ID="lbRevert" OnClick="lbRevert_Click" CssClass="control-link refresh-link" ToolTip="Revert Changes">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left">
                        <polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" ID="lbSaveAs" CssClass="control-link edit-link" ToolTip="Save As" OnClick="lbSaveAs_Click">
                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor" width="14" height="14" stroke-width="2">
                     <g>
	<path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8"/>
	<path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z"/>
	<polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	"/>
	<polyline class="st0" points="7,3.4 7,7 9.4,7 	"/>
</g>
                 </svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" ID="lbSave" CssClass="control-link edit-link" ToolTip="Save" OnClick="lbSave_Click">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save">
                        <path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </asp:LinkButton>
                <asp:LinkButton Text="" runat="server" ID="lbDownload" CssClass="control-link download-link" ToolTip="Download" OnClick="lbDownload_Click">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download">
                        <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
                </asp:LinkButton>
                <asp:Button ID="btnSimulate" runat="server" Text="Simulate" CssClass="btn control-link ml-auto download-link" OnClientClick="if(Page_ClientValidate('Insert')) ShowModalPopup('pload');" OnClick="btnSimulate_Click" ValidationGroup="Insert" />

            </div>
        </div>
    </section>
    <div id="myModal2" class="custom-modal">
        <!-- Modal content -->
        <div class="custom-modal-header">
            <span>Save Scenario As</span>
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
