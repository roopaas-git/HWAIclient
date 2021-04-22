<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="ManageQuickAccess.aspx.cs" Inherits="HealthWorks.Pages.ManageQuickAccess" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="../MySkin_Default/Test.css" rel="stylesheet" />
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
            overflow:hidden !important;
        }

        .accordianIcon {
            padding-left: 5px;
            font-size: 14px;
        }

        .accordianHeading span {
            padding: 10px;
            padding-left: 20px;
        }

        .custom-select {
            width: 90% !important;
        }
        .textbx {
            max-width: 90% !important;
        }
        .RadComboBox_Test {
            line-height: 25px !important;   
            height: 35px !important;            
            width: 90% !important;
        }

        .RadComboBox .rcbInput {
            padding: 5px 5px 1px 8px !important;
        }

        .rbFonts {
            font-size: 14px !important;
        }

        /*.RadComboBox {
            width: 100% !important;
        }*/

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

    <script type="text/javascript">  
        function validateCustomInput(evt) {           
            var charCode = (evt.which) ? evt.which : event.keycode;
            if (charCode != 44 && charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
            {
                return false;
            }
            else
            {
                return true;
            }        
        }
        </script>
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
     <div class="tab-nav-container">
        <nav class="tab-nav nav-light navbar">
            <ul class="nav">
                <li class="nav-item tab-nav-item plan-nav ">
                    <a class="nav-link" href="#">Plan</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBScenarios" runat="server" Text="Scenario List" OnClick="LBScenarios_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item ">
                            <asp:LinkButton CssClass="nav-link active" ID="LBPlans" runat="server" Text="Plan List" OnClick="LBPlans_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link " href="#">Market Insight</a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item tab-nav-item design-nav active" id="id_Quick" runat="server">
                    <a class="nav-link" href="#">Design</a>
                    <ul class="nav nav-pills sub-nav">
                        <li class="nav-item">
                            <asp:LinkButton CssClass="nav-link active" ID="LBQuickAccess" runat="server" Text="Quick Access" OnClick="LBQuickAccess_Click"></asp:LinkButton>
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
                            <asp:LinkButton CssClass="nav-link" ID="LBSimulatedOutput" runat="server" Text="Simulated Rank" OnClick="LBSimulatedOutput_Click"></asp:LinkButton>
                        </li>
                      <%--  <li class="nav-item">
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
                        <div class="row">
                            <div class="col-6 mt-2">
                                <h1><span class="header-label">Scenario: </span>
                                    <asp:Label ID="lblScenarioName" Text="" runat="server" CssClass="header-text" />
                                </h1>
                            </div>
                           
                            <div class="col-6">
                                <asp:DropDownList ID="ddlBidId" Enabled ="false" runat="server" CssClass="custom-select state-select"
                                    Style="float: left; 
                                           Width: 100% !important; 
                                           background-image: none !important;
                                           border: 0;
                                           background-color: #fff !important;
                                           margin-bottom: 0 !important;
                                           direction: rtl;
                                           font-weight: bolder;
                                           color: #5c276e;">
                                </asp:DropDownList>
                            </div>                            
                        </div>
                    </div>
                   <%-- <div class="scenario-table-controls">--%>
                     <div class="scenario-table-container">
                         <div class="filter">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                                 <ContentTemplate>
                                     <asp:Accordion ID="Accordion1" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                         SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                         HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                         <Panes>
                                             <asp:AccordionPane runat="server" ID="AccordionPane1">
                                                 <Header>
                                                     <div id="div1" class="accordianHeading" runat="server">
                                                         <a style="text-decoration: none"><span>Plan Features</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-2">
                                                             <asp:Label ID="Label37" Text="Premium" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtMonthly_Consolidated_Premium_C_D" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server" ></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator24" ValidationGroup="Insert"
                                                                 ControlToValidate="txtMonthly_Consolidated_Premium_C_D" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-2"></div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label38" Text="Health Deductible" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtAnnual_Health_Deductible" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator22" ValidationGroup="Insert"
                                                                 ControlToValidate="txtAnnual_Health_Deductible" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-2"></div>
                                                     </div>
                                                     <div class="row form-group">
                                                         <div class="col-2">
                                                             <asp:Label ID="Label34" Text="MOOP" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtIn_network_MOOP_Amount" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator16" ValidationGroup="Insert"
                                                                 ControlToValidate="txtIn_network_MOOP_Amount" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-2"></div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label35" Text="Drug Deductible" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtAnnual_Drug_Deductible" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator18" ValidationGroup="Insert"
                                                                 ControlToValidate="txtAnnual_Drug_Deductible" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-2"></div>
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
                                                     <div id="div3" class="accordianHeading" runat="server">
                                                         <a style="text-decoration: none"><span>Inpatient Hospital</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-4">
                                                             <asp:Label ID="Label9" Text="Select enhanced benefits :" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                            
                                                             <telerik:RadComboBox ID="ddlpbp_b1a_bendesc_ad_up_nmcs" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Visible="false"
                                                                EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Enhanced Benefits" EnableEmbeddedSkins="false" Skin="Test"
                                                                DropDownWidth="250px">
                                                                <Localization AllItemsCheckedString="All" ItemsCheckedString="" CheckAllString="Select All" />
                                                             </telerik:RadComboBox>

                                                         </div>                                                         
                                                         <div class="col-3">
                                                             <asp:Label ID="Label78" Text="Indicate number of Additional Days per benefit period:" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b1a_bendesc_amt_ad" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator39" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_bendesc_amt_ad" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>   
                                                         <div class="col-1"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-4">
                                                             <asp:Label ID="Label1" Text="Indicate Copayment amount for the Medicare-covered stay:" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_amt_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator40" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_amt_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-4">
                                                         </div>
                                                         <div class="col-3">
                                                         </div>
                                                     </div>
                                                     <div class="row mt-3">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label2" Text="Copayment Amt Interval 1:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_amt_int1_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_amt_int1_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-3">
                                                             <asp:Label ID="Label3" Text="Begin Day Interval 1:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_bgnd_int1_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_bgnd_int1_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-3">
                                                             <asp:Label ID="Label4" Text="End Day Interval 1:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_endd_int1_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)" ClientIDMode="Static"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_endd_int1_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label6" Text="Copayment Amt Interval 2:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_amt_int2_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_amt_int2_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-3">
                                                             <asp:Label ID="Label7" Text="Begin Day Interval 2:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_bgnd_int2_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_bgnd_int2_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-3">
                                                             <asp:Label ID="Label8" Text="End Day Interval 2:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_endd_int2_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)" ClientIDMode="Static"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_endd_int2_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                     </div>
                                                     <div class="row mb-3 form-group">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label11" Text="Copayment Amt Interval 3:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_amt_int3_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_amt_int3_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-3">
                                                             <asp:Label ID="Label12" Text="Begin Day Interval 3:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_bgnd_int3_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_bgnd_int3_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-3">
                                                             <asp:Label ID="Label13" Text="End Day Interval 3:" runat="server" CssClass="form-control-label" />
                                                             <asp:TextBox ID="txtpbp_b1a_copay_mcs_endd_int3_t1" runat="server" CssClass="textbx" onkeypress="return validateCustomInput(event)" ClientIDMode="Static"> </asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b1a_copay_mcs_endd_int3_t1" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                             <br />
                                                         </div>
                                                         <div class="col-1"></div>
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
                                                         <a style="text-decoration: none"><span>PCP, Specialist & ER</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-3">
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label27" Text="Coinsurance" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-1">
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label28" Text="Copayment" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-4">
                                                         </div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label5" Text="Primary Care Physician" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b7a_coins_pct_mc_min" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b7a_coins_pct_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b7a_copay_amt_mc_min" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b7a_copay_amt_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>

                                                         </div>
                                                         <div class="col-3"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label33" Text="Physician Specialist" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b7d_coins_pct_mc_min" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b7d_coins_pct_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b7d_copay_amt_mc_min" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator20" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b7d_copay_amt_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-4"></div>
                                                     </div>

                                                     <div class="row form-group">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label10" Text="Emergency Care" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b4a_coins_pct_mc_min" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b4a_coins_pct_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b4a_copay_amt_mc_min" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator17" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b4a_copay_amt_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-4"></div>
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
                                                         <a style="text-decoration: none"><span>Outpatient Medicare Services</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-3">
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label54" Text="Coinsurance" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-1">
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label55" Text="Copayment" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-4">
                                                         </div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label16" Text="Outpatient Lab" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8a_coins_pct_lab" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8a_coins_pct_lab" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8a_lab_copay_amt" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8a_lab_copay_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-4"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label17" Text="Diagnostics tests & Procedures" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8a_coins_pct_dmc" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator19" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8a_coins_pct_dmc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1">
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8a_copay_min_dmc_amt" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator21" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8a_copay_min_dmc_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-4"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label18" Text="Outpatient X-ray" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8b_coins_pct_cmc" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator23" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8b_coins_pct_cmc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8b_copay_mc_amt" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator25" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8b_copay_mc_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-4"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label15" Text="Diagnostic Radiological Services" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8b_coins_pct_drs" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator28" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8b_coins_pct_drs" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8b_copay_amt_drs" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator29" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8b_copay_amt_drs" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-4"></div>
                                                     </div>
                                                     <div class="row form-group mb-3">
                                                         <div class="col-3">
                                                             <asp:Label ID="Label14" Text="Therapeutic Radiation" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8b_coins_pct_tmc" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator26" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8b_coins_pct_tmc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b8b_copay_amt_tmc" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator27" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b8b_copay_amt_tmc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-4"></div>
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
                                                     <div id="div7" class="accordianHeading" runat="server">
                                                         <a style="text-decoration: none"><span>Outpatient Blood Services</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label74" Text="Does the plan provide Outpatient Blood Services as a supplemental benefit under Part C?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b9d_bendesc_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row mt-4">
                                                         <div class="col-5">
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label60" Text="Coinsurance" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label66" Text="Copayment" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2"></div>
                                                     </div>
                                                     <div class="row form-group mb-3">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label73" Text="Indicate Coinsurance/Copayment for Medicare-covered Benefits:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:CheckBox ID="chkpbp_b9d_coins_yn" runat="server" OnCheckedChanged="chck_Diagnostic_Coin_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                             <asp:TextBox ID="txtpbp_b9d_coins_pct_mc_min" CssClass="textbx" onkeypress="return validateCustomInput(event)" Enabled="false" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator66" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b9d_coins_pct_mc_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-1"></div>
                                                         <div class="col-2">
                                                             <asp:CheckBox ID="chkpbp_b9d_copay_yn" runat="server" AutoPostBack="true"
                                                                 OnCheckedChanged="chck_Diagnostic_Copay_CheckedChanged" />
                                                             <asp:TextBox ID="txtpbp_b9d_copay_mc_amt_min" CssClass="textbx" onkeypress="return validateCustomInput(event)" Enabled="false" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator68" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b9d_copay_mc_amt_min" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-2"></div>
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
                                                     <div id="div5" class="accordianHeading" runat="server">
                                                         <a style="text-decoration: none"><span>OTC</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label19" Text="Does the plan provide Over-The-Counter (OTC) Items as a supplemental benefit under Part C?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b13b_bendesc_otc" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts" OnSelectedIndexChanged="rdpbp_b13b_bendesc_otc_SelectedIndexChanged" AutoPostBack="true">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row mt-4 form-group">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label22" Text="Indicate Maximum Plan Benefit Coverage amount:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b13b_maxplan_amt" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator30" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b13b_maxplan_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b13b_otc_maxplan_per" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
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
                                                     <div id="div6" class="accordianHeading" runat="server">
                                                         <a style="text-decoration: none"><span>Preventive Dental</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label20" Text="Does the plan provide Preventive Dental Items as a supplemental benefit under Part C?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b16a_bendesc_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts" AutoPostBack="true" OnSelectedIndexChanged="rdpbp_b16a_bendesc_yn_SelectedIndexChanged">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row mt-4">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label23" Text="Select enhanced benefits:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <telerik:RadComboBox ID="ddlpbp_b16a_bendesc_ehc" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Visible="false"
                                                                EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Enhanced Benefits" EnableEmbeddedSkins="false" Skin="Test"
                                                                DropDownWidth="250px">                                                                 
                                                                <Localization AllItemsCheckedString="All" ItemsCheckedString="" CheckAllString="Select All" />
                                                             </telerik:RadComboBox>                                                             
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label21" Text="Indicate Maximum Plan Benefit Coverage amount & periodicity:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b16a_maxplan_amt" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator31" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b16a_maxplan_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>                                                        
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b16a_maxplan_per" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value ="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value ="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value ="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value ="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value ="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value ="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
                                                     </div>
                                                     
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label31" Text="Indicate number of visits & Periodicity:" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:Label ID="Label24" Text="Visits" runat="server" CssClass="form-control-label" />
                                                         </div>                                                        
                                                         <div class="col-2">
                                                             <asp:Label ID="Label25" Text="Periodicity" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-3">
                                                         </div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label26" Text="Oral Exam" runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b16a_bendesc_numv_oe" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator32" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b16a_bendesc_numv_oe" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>                                                       
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_per_oe" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label29" Text="Dental X-Rays" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b16a_bendesc_numv_dx" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator34" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b16a_bendesc_numv_dx" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>                                                         
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_per_dx" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label53" Text="Prophylaxis (Cleaning)" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b16a_bendesc_numv_pc" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator33" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b16a_bendesc_numv_pc" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>                                                         
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_per_pc" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
                                                     </div>
                                                     <div class="row form-group">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label30" Text="Fluoride Treatment" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b16a_bendesc_numv_ft" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator36" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b16a_bendesc_numv_ft" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>                                                         
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b16a_bendesc_per_ft" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
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
                                                         <a style="text-decoration: none"><span>Comprehensive Dental</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label32" Text="Does the plan provide Comprehensive Dental Items as a supplemental benefit under Part C?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b16b_bendesc_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts" OnSelectedIndexChanged="rdpbp_b16b_bendesc_yn_SelectedIndexChanged" AutoPostBack="true">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row mt-4">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label36" Text="Select enhanced benefits:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">                                                             
                                                             <telerik:RadComboBox ID="ddlpbp_b16b_bendesc_ehc" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Visible="false" 
                                                                EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Enhanced Benefits" EnableEmbeddedSkins="false" Skin="Test"
                                                                DropDownWidth="250px">
                                                                <Localization AllItemsCheckedString="All" ItemsCheckedString="" CheckAllString="Select All" />
                                                             </telerik:RadComboBox>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label46" Text="Is there a service-specific Maximum Plan Benefit Coverage amount?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b16b_maxplan_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts" OnSelectedIndexChanged="rdpbp_b16b_maxplan_yn_SelectedIndexChanged" AutoPostBack="true">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label47" Text="Select the Maximum Plan Benefit Coverage type:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b16b_maxbene_type" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Covered under Preventive Dental Category 16a</asp:ListItem>
                                                                 <asp:ListItem Value="2">Plan-specified amount per period</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row form-group">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label39" Text="Indicate Maximum Plan Benefit Coverage amount & Periodicity:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:TextBox ID="txtpbp_b16b_maxplan_amt" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator38" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b16b_maxplan_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>                                                         
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b16b_maxplan_per" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
                                                     </div>
                                                 </Content>
                                             </asp:AccordionPane>
                                         </Panes>
                                     </asp:Accordion>
                                     <asp:Accordion ID="Accordion9" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                         SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                         HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                         <Panes>
                                             <asp:AccordionPane runat="server" ID="AccordionPane9">
                                                 <Header>
                                                     <div id="div9" class="accordianHeading" runat="server">
                                                         <a style="text-decoration: none"><span>Eyewear</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label40" Text="Does the plan provide Eyewear as a supplemental benefit under Part C?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b17b_bendesc_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts" OnSelectedIndexChanged="rdpbp_b17b_bendesc_yn_SelectedIndexChanged" AutoPostBack="true">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label41" Text="Select enhanced benefits:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <telerik:RadComboBox ID="ddlpbp_b17b_bendesc_ehc" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Visible="false" 
                                                                EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Enhanced Benefits" EnableEmbeddedSkins="false" Skin="Test"
                                                                DropDownWidth="250px">
                                                                <Localization AllItemsCheckedString="All" ItemsCheckedString="" CheckAllString="Select All" />
                                                             </telerik:RadComboBox>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label42" Text="Is there a service-specific Maximum Plan Benefit Coverage amount?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b17b_maxplan_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts" OnSelectedIndexChanged="rdpbp_b17b_maxplan_yn_SelectedIndexChanged" AutoPostBack="true">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label43" Text="Select the Maximum Plan Benefit Coverage type:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b17b_maxplan_type" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Covered under Eye Exams Category 17a</asp:ListItem>
                                                                 <asp:ListItem Value="2">Plan-specified amount per period</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row form-group">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label44" Text="Indicate Maximum Plan Benefit Coverage amount & Periodicity:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">   
                                                             <asp:TextBox ID="txtpbp_b17b_comb_maxplan_amt" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator37" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b17b_comb_maxplan_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>                                                         
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b17b_comb_maxplan_per" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
                                                     </div>
                                                 </Content>
                                             </asp:AccordionPane>
                                         </Panes>
                                     </asp:Accordion>
                                     <asp:Accordion ID="Accordion10" runat="server" SelectedIndex="1" FadeTransitions="True" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false"
                                         SuppressHeaderPostbacks="true" HeaderCssClass="HeaderCSS" ContentCssClass="ContentCSS"
                                         HeaderSelectedCssClass="HeaderSelectedCSS" BorderColor="#D2D2D2" BorderWidth="1" CssClass="accordian">
                                         <Panes>
                                             <asp:AccordionPane runat="server" ID="AccordionPane10">
                                                 <Header>
                                                     <div id="div10" class="accordianHeading" runat="server">
                                                         <a style="text-decoration: none"><span>Hearing Aid</span></a>
                                                     </div>
                                                 </Header>
                                                 <Content>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label45" Text="Does the plan provide Hearing Aids as a supplemental benefit under Part C?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b18b_bendesc_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts" OnSelectedIndexChanged="rdpbp_b18b_bendesc_yn_SelectedIndexChanged" AutoPostBack="true">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row mt-4">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label48" Text="Select enhanced benefits:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <telerik:RadComboBox ID="ddlpbp_b18b_bendesc_ehc" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Visible="false"
                                                                EnableVirtualScrolling="true" DropDownCssClass="textfont" ClientIDMode="Static" EmptyMessage="Select Enhanced Benefits" EnableEmbeddedSkins="false" Skin="Test"
                                                                DropDownWidth="250px">                                                               
                                                                <Localization AllItemsCheckedString="All" ItemsCheckedString="" CheckAllString="Select All" />
                                                             </telerik:RadComboBox>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label49" Text="Is there a service-specific Maximum Plan Benefit Coverage amount?"
                                                                 runat="server" CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:RadioButtonList ID="rdpbp_b18b_maxplan_yn" runat="server" RepeatDirection="Horizontal" CssClass="rbFonts" OnSelectedIndexChanged="rdpbp_b18b_maxplan_yn_SelectedIndexChanged" AutoPostBack="true">
                                                                 <asp:ListItem Text="Yes" />
                                                                 <asp:ListItem Text="No" />
                                                                 <asp:ListItem Text="" style="visibility: hidden;" />
                                                             </asp:RadioButtonList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label50" Text="Does the Maximum Plan Benefit Coverage Amount apply per ear or for both ears combined?" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b18b_maxplan_perear" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Per ear</asp:ListItem>
                                                                 <asp:ListItem Value="2">One single ear</asp:ListItem>
                                                                 <asp:ListItem Value="3">Both ears combined</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label52" Text="Select the Maximum Plan Benefit Coverage type:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b18b_maxplan_type" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Covered under Hearing Exams Category - 18a</asp:ListItem>
                                                                 <asp:ListItem Value="2">Plan-specified amount per period</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-5"></div>
                                                     </div>
                                                     <div class="row form-group">
                                                         <div class="col-5">
                                                             <asp:Label ID="Label51" Text="Indicate Maximum Plan Benefit Coverage amount & Periodicity:" runat="server"
                                                                 CssClass="form-control-label" />
                                                         </div>
                                                         <div class="col-2">   
                                                             <asp:TextBox ID="txtpbp_b18b_maxplan_amt" CssClass="textbx" onkeypress="return validateCustomInput(event)" runat="server"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator35" ValidationGroup="Insert"
                                                                 ControlToValidate="txtpbp_b18b_maxplan_amt" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                 ValidationExpression="^[0-9]\d*(\,\d+)*(\.\d+)?$|^[0-9]\d*(\.\d+)*(\,\d+)?$">
                                                             </asp:RegularExpressionValidator>
                                                         </div>                                                         
                                                         <div class="col-2">
                                                             <asp:DropDownList runat="server" ID="ddlpbp_b18b_maxplan_per" CssClass="custom-select state-select">
                                                                 <asp:ListItem Value="0">Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">Every three years</asp:ListItem>
                                                                 <asp:ListItem Value="2">Every two years</asp:ListItem>
                                                                 <asp:ListItem Value="3">Every year</asp:ListItem>
                                                                 <asp:ListItem Value="4">Every six months</asp:ListItem>
                                                                 <asp:ListItem Value="5">Every three months</asp:ListItem>
                                                                 <asp:ListItem Value="6">Other, Describe</asp:ListItem>
                                                             </asp:DropDownList>
                                                         </div>
                                                         <div class="col-3"></div>
                                                     </div>
                                                 </Content>
                                             </asp:AccordionPane>
                                         </Panes>
                                     </asp:Accordion>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         </div>
                    </div>
                </div>
            </div>

            <div class="bottom-controls">
                <asp:LinkButton runat="server" CssClass="control-link refresh-link" ID="lbRevert" OnClick="lbRevert_Click" ToolTip="Revert Changes">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left"><polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="control-link edit-link" ID="lbSaveAs" OnClick="lbSaveAs_Click" ToolTip="Save As">
                   <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" fill="none" stroke="currentColor"  width="14" height="14" stroke-width="2">
                     <g>
	                    <path class="st0" d="M12,3.4H5.1c-1.1,0-2,0.9-2,2V19c0,1.1,0.9,2,2,2h13.7c1.1,0,2-0.9,2-2v-6.8"/>
	                    <path class="st0" d="M18,3.5c0.6-0.6,1.7-0.6,2.3,0s0.6,1.7,0,2.3l-7.3,7.3L10,13.8l0.8-3.1L18,3.5z"/>
	                    <polyline class="st0" points="7,21 7,17.2 16.9,17.2 16.9,21 	"/>
	                    <polyline class="st0" points="7,3.4 7,7 9.4,7 	"/>
                     </g>
                  </svg>
                </asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="control-link edit-link" ID="lbSave" OnClick="lbSave_Click" ToolTip="Save">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-save"><path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
                </asp:LinkButton>
                <asp:LinkButton runat="server" CssClass="control-link download-link" ID="lbDownload" OnClick="lbDownload_Click" ToolTip="Download">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
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


