<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Health.Master" AutoEventWireup="true" CodeBehind="EnrollmentQuickAccessSimulation.aspx.cs"  Inherits="HealthWorks.Pages.EnrollmentQuickAccessSimulation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../MySkin_Default/Test.css" rel="stylesheet" />
    <script type="text/javascript">
        function PostToNewWindow() {
            originalTarget = document.forms[0].target;
            document.forms[0].target = '_blank';
            window.setTimeout("document.forms[0].target=originalTarget;", 300);
            return true;
        }
    </script>
    <style>
        @media (min-width: 1025px) and (max-width: 1366px){
            .BenefitDesc {}
        }       
        @media (min-width: 1367px) {
            .BenefitDesc {
                width: 100% !important;
            }
        }               
    </style>
    <style type="text/css">

        marquee {
            display: none !important;
        }

   .tooltip {
      position: relative !important;
      display: inline-block !important;
      border-bottom: 1px dotted black !important;
      opacity: 1 !important;
      z-index: 0 !important;
    }

    .tooltip .tooltiptext {
        font-weight: 600 !important;
        visibility: hidden !important;
        width: 200px !important;
        background-color: #5c276e !important;
        color: #fff !important;
        text-align: center !important;
        border-radius: 0px !important;
        padding: 8px 0 !important;
        position: absolute !important;
        z-index: 0 !important;
        top: 0px !important;
        right: 110% !important;
        webkit-box-shadow: 2px 4px 10px rgb(0 0 0 / 20%);
        box-shadow: 2px 4px 10px rgb(0 0 0 / 20%);  
    }

    .tooltip:hover .tooltiptext {
        visibility: visible !important;
    }

    .tooltip .tooltiptext::after {
        content: " ";
        position: absolute;
        top: 50%;
        left: 100%; /* To the right of the tooltip */
        margin-top: -5px;
        border-width: 5px;
        border-style: solid;
        border-color: transparent transparent transparent black;
    }

    .box {
      width: 40%;
      margin: 0 auto;
      background: rgba(255,255,255,0.2);
      padding: 35px;
      border: 2px solid #fff;
      border-radius: 20px/50px;
      background-clip: padding-box;
      text-align: center;
    }


    .overlay {
      z-index: 999;
      position: fixed;
      top: 0;
      bottom: 0;
      left: 0;
      right: 0;
      background: rgba(0, 0, 0, 0.7);
      transition: opacity 500ms;
      visibility: hidden;
      opacity: 0;
    }
    .overlay:target {
      visibility: visible;
      opacity: 1;
    }

    .popup {
      margin: 70px auto;
      padding: 20px;
      background: #fff;
      border-radius: 5px;
      width: 50%;
      position: relative;
      transition: all 5s ease-in-out;
    }

    .popup h2 {
      margin-top: 0;
      color: #333;
      font-family: Tahoma, Arial, sans-serif;
    }
    .popup .close {
      position: absolute;
      top: 20px;
      right: 30px;
      transition: all 200ms;
      font-size: 30px;
      font-weight: bold;
      text-decoration: none;
      color: #333;
    }
    .popup .close:hover {
  
    }
    .popup .content {
      max-height: 30%;
      overflow: auto;
    }


        .unique-id{
            width:100%;
            background-color:#333;
        }

        .scenario-table-outer-wrapper .scenario-table-header-wrapper {        
        border-bottom: none;        
        }

       

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
    <style>
        /*.validator {
            color: red;
            margin-right: 30px;
        }*/

        /*Enrollment*/
.textboxRepeater{
    border: 1px solid gray;
    width: 227px;
}
                  
.card {
    z-index: 0;border: none;position: relative
}

#progressbar {   
    overflow: hidden;color: lightgrey;margin-bottom: 0.5rem;
}

#progressbar .itemcolor {
    color: #c1c1c1;
}

#progressbar li.active > strong > a {
    color: #5c276e !important;
}

#progressbar li {
    list-style-type: none;font-size: 12px;width: 25%;float: left;position: relative
}

#progressbar #CreateScenario:before {  
   content: "1";font-size: 15px;font-weight: 900;
}

#progressbar #DownloadPBP:before {   
   content: "2";font-size: 15px;font-weight: 900;
}

#progressbar #UploadPBP:before {   
    content: "3";font-size: 15px;font-weight: 900;
}

#progressbar #ResultsReady:before {   
    content: "4";font-size: 15px;font-weight: 900;
}

#progressbar li:before {
    width: 32px;height: 32px;line-height: 28px;display: block;font-size: 18px;color: #ffffff;background: #c1c1c1;border-radius: 50%;margin: 0 auto 10px auto;padding: 2px;
}
 #CreateScenario:after {
    content: '';height: 2px;background: lightgray;position: absolute;top: 16px;z-index: -1;
}

  #DownloadPBP:after {
    content: '';width: 100%;height: 2px;background: lightgray;position: absolute;left: -50%;top: 16px;z-index: -1;
}

   #UploadPBP:after {
    content: '';width: 100%;height: 2px;background: lightgray;position: absolute;left: -50%;top: 16px;z-index: -1;
}

    #ResultsReady:after {
    content: '';width: 100%;height: 2px;background: lightgray;position: absolute;left: -50%;top: 16px;z-index: -1
}

        #progressbar li.active:before, #progressbar li.active:after {
            background: #5c276e;
        }
        .validator {
            color: red;
            margin-right: 10px;
            margin-left: -5px;
        }

        .applyBtnFilter {
            width: 158px;
            font-size: 14px;
            color: #FFF;
            background: #5C276E !important;
        }

            .applyBtnFilter:hover {
                border-color: #5C276E !important;
                box-shadow: 0 3px 6px #5C276E;
                color: #fff;
            }

        .RadComboBoxDropDown .rcbHeader, .RadComboBoxDropDown .rcbFooter {
            padding: 0 !important;
        }

        .scenario-table-outer-wrapper .scenario-table-controls .filter {
            width: 96% !important;
        }

        .RadComboBox .rcbEmptyMessage {
            font-size: 12px !important;
        }

        .label {
            color: gray;
            font-size: smaller;
            margin-left: 1px;
            text-align: left;
            line-height: 13px;
        }

        .labeld {
            font-size: 14px;
        }
        label {
            font-weight: 400;
            margin-left: 7px !important;
        }
        .custom-instruction
        {
           font-size: 16px;
           line-height: 1.75;
        }
        .custom-box-shadow{
            box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.5); 
            -webkit-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.5);
            -moz-box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.5);
        }
        .change-benefit-value
        {
            font-size:12px !important;
            text-align:right;
        }
    </style>
    <style>
      
        /* Table Style */

        .significant-benefit-list{
            text-align: center;
            color: #fff;
            background: #5c276e;
            padding: 8px;
            font-size: 16px;
            font-weight: bold;
            border-bottom: 1px solid #cbcbcb;
        }

        .table-wrapper {
            margin: 10px 70px 70px;
            box-shadow: 0px 35px 50px rgba( 0, 0, 0, 0.2 );
        }

        .fl-table {
            border-radius: 5px;
            font-size: 12px;
            font-weight: normal;
            border: none;
            border-collapse: collapse;
            width: 100%;
            max-width: 100%;
            white-space: nowrap;
            background-color: white;
        }

            .fl-table td, .fl-table th {
                text-align: center;
                padding: 8px;
            }

            .fl-table tbody tr td {
                text-align: left;
                padding: 8px 16px;
                color: #464648;
            }

            .fl-table td {
                border: 1px solid #cbcbcb;
                font-size: 12px;
            }   

            .fl-table thead th {
                color: #5c276e;
                background: #eee;
            }

            .fl-table thead th:last-child {
                color: #ffffff;
                background: #bda7c4;
            }

            .fl-table tbody tr > td:last-child {
                color: #ffffff;
                background: #BDA7C4; 
                /*background: #5c276e;*/
                width: 20%;
                padding: 0;
            }           

        /* Responsive */

        @media (max-width: 767px) {
            .fl-table {
                display: block;
                width: 100%;
            }

            .table-wrapper:before {
                content: "Scroll horizontally >";
                display: block;
                text-align: right;
                font-size: 11px;
                color: white;
                padding: 0 0 10px;
            }

            .fl-table thead, .fl-table tbody, .fl-table thead th {
                display: block;
            }

                .fl-table thead th:last-child {
                    border-bottom: none;
                }

            .fl-table thead {
                float: left;
            }

            .fl-table tbody {
                width: auto;
                position: relative;
                overflow-x: auto;
            }

            .fl-table td, .fl-table th {
                padding: 20px .625em .625em .625em;
                height: 60px;
                vertical-align: middle;
                box-sizing: border-box;
                overflow-x: hidden;
                overflow-y: auto;
                width: 120px;
                font-size: 13px;
                text-overflow: ellipsis;
            }

            .fl-table thead th {
                text-align: left;
                border-bottom: 1px solid #f7f7f9;
            }

            .fl-table tbody tr {
                display: table-cell;
            }

                .fl-table tbody tr:nth-child(odd) {
                    background: none;
                }

            .fl-table tr:nth-child(even) {
                background: transparent;
            }

            .fl-table tr td:nth-child(odd) {
                background: #F8F8F8;
                border-right: 1px solid #E6E4E4;
            }

            .fl-table tr td:nth-child(even) {
                border-right: 1px solid #E6E4E4;
            }

            .fl-table tbody td {
                display: block;
                text-align: center;
            }
        }
    </style>
    <script type="text/javascript">   
        $(document).ready(function () {
            maintainScrollPosition();                   
        });
        function pageLoad() {
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#divPlanView").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }

        function UploadFileChange(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("Upload").click();
            }
        }

        function ValidateUpload() {
            var fileUpload = document.getElementById('UploadFile');                        
            if (fileUpload.value != '') {
                return true;
            }
            else {
                return false;
            }
        }

        function InitializeUploadDialog() {      
            $("#UploadFile").click();
            ShowHideDynamicUploadDiv();
        }
        
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function removeToolTip() {
            $(".tooltiptext").remove();
            //$(".change-benefit-value").val("");    
        }

        function dynamicFormat(obj, e) {

            var dtType = $(obj).data('bdt-attr').toLowerCase();
            var benfitCurrentValue = $(obj).data('bcv-attr');
            var benfitRange_Min = $(obj).data('br-attr').split("|")[0];
            var benfitRange_Max = $(obj).data('br-attr').split("|")[1];
            var maxlength = parseInt(($(obj).data('br-attr').split("|")[1]).length);
            var benfitNewValue = $(obj).val();
            var min = parseInt(benfitRange_Min);
            var max = parseInt(benfitRange_Max);
            var tooltip_Message = "";
            var div = $(obj).parent("div");
           
            if (benfitRange_Min.includes("/")) {
                benfitRange_Min = benfitRange_Min.split("/")[1];
            }

            $(".tooltiptext").remove();
            switch (dtType) {

                case 'int':

                        $(obj).attr('maxlength', maxlength);


                        var allowedKeys = new Array();
                        allowedKeys.push(8); // backspace
                        allowedKeys.push(9); // tab
                        allowedKeys.push(16); // shift
                        allowedKeys.push(20); // caps lock
                        allowedKeys.push(37); // arrow left
                        allowedKeys.push(38); // arrow up
                        allowedKeys.push(39); // arrow right
                        allowedKeys.push(40); // arrow down

                        allowedKeys.push(48);
                        allowedKeys.push(49);
                        allowedKeys.push(50);
                        allowedKeys.push(51);
                        allowedKeys.push(52);
                        allowedKeys.push(53);
                        allowedKeys.push(54);
                        allowedKeys.push(55);
                        allowedKeys.push(56);
                        allowedKeys.push(57);
                        allowedKeys.push(96);
                        allowedKeys.push(97);
                        allowedKeys.push(98);
                        allowedKeys.push(99);
                        allowedKeys.push(100);
                        allowedKeys.push(101);
                        allowedKeys.push(102);
                        allowedKeys.push(103);
                        allowedKeys.push(104);
                        allowedKeys.push(105);

                        if (!(allowedKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {
                            $(obj).val("");
                            tooltip_Message = "Allowed Range : " + benfitRange_Min + " - " + benfitRange_Max;
                        }
                        else {
                            if (benfitNewValue > max) {
                                $(obj).val("");
                                tooltip_Message = "Allowed Range : " + benfitRange_Min + " - " + benfitRange_Max;
                            }
                            else if (benfitNewValue < min) {
                                $(obj).val("");
                                tooltip_Message = "Allowed Range : " + benfitRange_Min + " - " + benfitRange_Max;
                            }
                        }                   
                    break;

                case 'string':

                    break;

                case 'string_nc_money':

                    if ((e.keyCode == "78") ||  (e.keyCode == "67")) {                      
                        $(obj).val("NC");
                    }
                    else { 
                        var temp = "";

                        if ($(obj).val().includes("$")) {
                            temp = $(obj).val().replace("$", "");
                            temp = temp.replace(",", "");
                            temp = parseFloat(temp);
                            $(obj).attr('maxlength', maxlength + 3);
                        }
                        else {
                            temp = $(obj).val();
                        }
                        
                        if ((e.keyCode == "67") && ( $(obj).val().length > 2)) {
                            // do nothing
                            //$(obj).addClass("bound");                          
                            $(obj).val("");
                            tooltip_Message = "Allowed Range : NC (or) "+benfitRange_Min +" - "+ benfitRange_Max;                           
                        }
                        else if (isNaN(temp)) {

                            $(obj).attr('maxlength', 2);

                            var allowedKeys = new Array();

                            allowedKeys.push(8); // backspace
                            allowedKeys.push(9); // tab
                            allowedKeys.push(16); // shift
                            allowedKeys.push(20); // caps lock
                            allowedKeys.push(37); // arrow left
                            allowedKeys.push(38); // arrow up
                            allowedKeys.push(39); // arrow right
                            allowedKeys.push(40); // arrow down

                            allowedKeys.push(78);
                            allowedKeys.push(67);

                            if (!(allowedKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {
                                $(obj).val("");
                                tooltip_Message = "Allowed Range : NC (or) " + benfitRange_Min + " - " + benfitRange_Max;
                            } else if ($(obj).val().length == 2 && $(obj).val().toLowerCase() != "nc") {
                                $(obj).val("");
                                tooltip_Message = "Allowed Range : NC (or) " + benfitRange_Min + " - " + benfitRange_Max;
                            } else {
                                $(obj).val($(obj).val().toUpperCase());
                            }
                        }
                        else {
                            if (temp > max) {
                                //$(obj).val("$0");
                                $(obj).val("");
                                tooltip_Message = "Allowed Range : NC (or) " + benfitRange_Min + " - " + benfitRange_Max;
                            }
                            else if (temp < min) {
                                if (temp.length >= 2) {
                                    //$(obj).val("$0");
                                    $(obj).val("");
                                    tooltip_Message = "Allowed Range : NC (or) " + benfitRange_Min + " - " + benfitRange_Max;
                                }
                            }
                            else {
                                if ((e.keyCode == "37") || (e.keyCode == "38") || (e.keyCode == "39") ||(e.keyCode == "40")) {
                                    //no nothing
                                }
                                else if ($(obj).length == 0){
                                    $(obj).val(temp);
                                }
                                else{
                                    $(obj).val(new Intl.NumberFormat("en-US", { style: "currency", currency: "USD", minimumFractionDigits: 0, maximumFractionDigits: 0 }).format(temp));
                                }                                
                            }
                        }
                    }

                    break;

                case 'string_nc_percentage':
                
                    if ((e.keyCode == "78") ||  (e.keyCode == "67")) {                   
                        $(obj).val("NC");
                    }                    
                    else {
                        var temp = "";

                        if ($(obj).val().includes("%")) {
                            temp = $(obj).val().replace("%", "");
                            temp = parseInt(temp);
                            $(obj).attr('maxlength', maxlength + 1);
                        }
                        else {
                            temp = $(obj).val();
                        }                    
                        
                        if ((e.keyCode == "67") && ( $(obj).val().length > 2)) {                                             
                            $(obj).val("");
                            tooltip_Message = "Allowed Range : NC (or) "+benfitRange_Min +" - "+ benfitRange_Max +"%";                           
                        }
                        else if (isNaN(temp)) {                         
                            $(obj).attr('maxlength', 2);
                            var allowedKeys = new Array();

                            allowedKeys.push(8); // backspace
                            allowedKeys.push(9); // tab
                            allowedKeys.push(16); // shift
                            allowedKeys.push(20); // caps lock
                            allowedKeys.push(37); // arrow left
                            allowedKeys.push(38); // arrow up
                            allowedKeys.push(39); // arrow right
                            allowedKeys.push(40); // arrow down

                            allowedKeys.push(78);
                            allowedKeys.push(67);
                            
                            if (!(allowedKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {
                                $(obj).val("");
                                 tooltip_Message = "Allowed Range : NC (or) "+benfitRange_Min +" - "+ benfitRange_Max +"%";
                            } else if ($(obj).val().length == 2 && $(obj).val().toLowerCase() != "nc") {
                                $(obj).val("");
                                 tooltip_Message = "Allowed Range : NC (or) "+benfitRange_Min +" - "+ benfitRange_Max +"%";
                            } else {
                                $(obj).val($(obj).val().toUpperCase());                            
                            }
                        }
                        else {

                            if (temp > max) {
                                $(obj).val("");
                                tooltip_Message = "Allowed Range : NC (or) "+benfitRange_Min +" - "+ benfitRange_Max +"%";
                            }
                            else if (temp < min) {
                                if (temp.length >= 2) {
                                    $(obj).val("");
                                    tooltip_Message = "Allowed Range : NC (or) "+benfitRange_Min +" - "+ benfitRange_Max +"%";
                                }
                            }
                            else {
                                if (temp.length != 0) {
                                    $(obj).val(temp + "%");
                                }
                            }
                        }
                    }

                    break;

                case 'string_tickcross':
                 
                        $(obj).attr('maxlength', maxlength);
                        var allowedKeys = new Array();
                        allowedKeys.push(8); // backspace
                        allowedKeys.push(9); // tab
                        allowedKeys.push(16); // shift
                        allowedKeys.push(20); // caps lock
                        allowedKeys.push(37); // arrow left
                        allowedKeys.push(38); // arrow up
                        allowedKeys.push(39); // arrow right
                        allowedKeys.push(40); // arrow down    
                        allowedKeys.push(49);
                        allowedKeys.push(48);
                        allowedKeys.push(96);
                        allowedKeys.push(97);
                        var tickcrossErrorMsg = "Allowed Range : "+benfitRange_Min +" - "+ benfitRange_Max;
                        if (!(allowedKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {
                            $(obj).val("");
                             tooltip_Message = tickcrossErrorMsg;
                        } else {
                            if($(obj).val().length <= maxlength)
                            {    
                                if($(obj).val().length == maxlength){
                                   // tooltip_Message = tickcrossErrorMsg;
                                }      
                                if (parseInt(benfitNewValue) > max) {
                                    $(obj).val("");
                                    tooltip_Message = tickcrossErrorMsg;
                                }
                                else if (parseInt(benfitNewValue) < min) {
                                    $(obj).val("");
                                    tooltip_Message = tickcrossErrorMsg;
                                }
                            }
                            else {
                                $(obj).val("");
                                tooltip_Message = tickcrossErrorMsg;                           
                            }
                        }
                    
                    break;

                case 'float':
                   
                        $(obj).attr('maxlength', maxlength + 2);

                        var allowedKeys = new Array();
                        allowedKeys.push(8); // backspace
                        allowedKeys.push(9); // tab
                        allowedKeys.push(16); // shift
                        allowedKeys.push(20); // caps lock
                        allowedKeys.push(37); // arrow left
                        allowedKeys.push(38); // arrow up
                        allowedKeys.push(39); // arrow right
                        allowedKeys.push(40); // arrow down
                        allowedKeys.push(110); // dot
                        allowedKeys.push(190); // dot

                        allowedKeys.push(48);
                        allowedKeys.push(49);
                        allowedKeys.push(50);
                        allowedKeys.push(51);
                        allowedKeys.push(52);
                        allowedKeys.push(53);
                        allowedKeys.push(54);
                        allowedKeys.push(55);
                        allowedKeys.push(56);
                        allowedKeys.push(57);
                        allowedKeys.push(96);
                        allowedKeys.push(97);
                        allowedKeys.push(98);
                        allowedKeys.push(99);
                        allowedKeys.push(100);
                        allowedKeys.push(101);
                        allowedKeys.push(102);
                        allowedKeys.push(103);
                        allowedKeys.push(104);
                        allowedKeys.push(105);


                        if (!(allowedKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {
                            $(obj).val("");
                            tooltip_Message = "Allowed Range : " + benfitRange_Min + " - " + benfitRange_Max;
                        }
                        else {

                            var validRange = new Array();

                            var loopRange = ((parseInt(benfitRange_Max) - parseInt(benfitRange_Min)) + (parseInt(benfitRange_Max) - parseInt(benfitRange_Min))) + 1;
                           
                            var startRange = benfitRange_Min;
                            validRange.push(parseInt(startRange));
                            for (var i = 1; i < loopRange; i++) {
                                var temp;
                                if (i % 2 != 0) {
                                    temp = parseFloat(parseInt(startRange) + .5);
                                }
                                else {
                                    temp = parseInt(parseFloat(startRange + .5));
                                }
                                validRange.push(temp);
                                startRange = temp;
                            }
                            
                            if (!(validRange.indexOf(parseInt($(obj).val())) != -1)) {
                                $(obj).val("");
                                tooltip_Message = "Allowed Range : " + benfitRange_Min + " - " + benfitRange_Max
                            }
                            else if (!(validRange.indexOf(parseFloat($(obj).val())) != -1)) {
                                $(obj).val("");
                                tooltip_Message = "Allowed Range : " + benfitRange_Min + " - " + benfitRange_Max
                            }
                        }
                                                          
                    break;

                case 'boolean':
                
                if (e.keyCode == "89") {                     
                        $(obj).val("Yes");
                    }  
                  else if (e.keyCode == "78"){
                    $(obj).val("No");
                }
                else { 
                    $(obj).attr('maxlength', maxlength);
                    var allowedKeys = new Array();
                    allowedKeys.push(8); // backspace
                    allowedKeys.push(9); // tab
                    allowedKeys.push(16); // shift
                    allowedKeys.push(20); // caps lock
                    allowedKeys.push(37); // arrow left
                    allowedKeys.push(38); // arrow up
                    allowedKeys.push(39); // arrow right
                    allowedKeys.push(40); // arrow down                    
                    allowedKeys.push(89);
                    allowedKeys.push(69);
                    allowedKeys.push(83);
                    allowedKeys.push(78);
                    allowedKeys.push(79);    
                    var booleanErrorMsg = "Allowed Range : " + benfitRange_Min + " (or) " + benfitRange_Max;           
                    if (!(allowedKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {
                        $(obj).val("");
                        tooltip_Message = booleanErrorMsg;
                    }
                    else{
                        if($(obj).val().length <= maxlength)
                            {                                    
                                if ($(obj).val().length == 1 && (($(obj).val().toLowerCase() != "n") && ($(obj).val().toLowerCase() != "y"))) {
                                    $(obj).val("");
                                    tooltip_Message = booleanErrorMsg;
                                }
                                else if ($(obj).val().length == 2 && (($(obj).val().toLowerCase() != "no") && ($(obj).val().toLowerCase() != "ye"))) {
                                    $(obj).val("");
                                    tooltip_Message = booleanErrorMsg;
                                }
                                else if ($(obj).val().length == 3 && $(obj).val().toLowerCase() != "yes") {
                                    $(obj).val("");
                                    tooltip_Message = booleanErrorMsg;
                                } else {
                                    $(obj).val($(obj).val());
                                    $(obj).css("text-transform", "capitalize");
                                }
                            }
                            else {
                                $(obj).val("");
                                tooltip_Message = booleanErrorMsg;   
                            }
                    }
                }
                    
                    break;

                case 'money':
                
                        var allowedKeys = new Array();
                        allowedKeys.push(8); // backspace
                        allowedKeys.push(9); // tab
                        allowedKeys.push(16); // shift
                        allowedKeys.push(20); // caps lock
                        allowedKeys.push(37); // arrow left
                        allowedKeys.push(38); // arrow up
                        allowedKeys.push(39); // arrow right
                        allowedKeys.push(40); // arrow down

                        allowedKeys.push(48);
                        allowedKeys.push(49);
                        allowedKeys.push(50);
                        allowedKeys.push(51);
                        allowedKeys.push(52);
                        allowedKeys.push(53);
                        allowedKeys.push(54);
                        allowedKeys.push(55);
                        allowedKeys.push(56);
                        allowedKeys.push(57);
                        allowedKeys.push(96);
                        allowedKeys.push(97);
                        allowedKeys.push(98);
                        allowedKeys.push(99);
                        allowedKeys.push(100);
                        allowedKeys.push(101);
                        allowedKeys.push(102);
                        allowedKeys.push(103);
                        allowedKeys.push(104);
                        allowedKeys.push(105);

                        var moneyErrorMsg = "Allowed Range : "+benfitRange_Min +" - "+ benfitRange_Max;
                            if (!(allowedKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {
                                $(obj).val("");
                                tooltip_Message = moneyErrorMsg
                            }
                            else{
                                var temp = benfitNewValue.replace("$", "");

                                temp = temp.replace(",", "");
                                temp = parseFloat(temp);
                                $(obj).attr('maxlength', maxlength + 3);

                                if (isNaN(temp)) {
                                    $(obj).val("");
                                    tooltip_Message = moneyErrorMsg;
                                }
                                else {                                       
                                    if (temp > max) {                                       
                                        $(obj).val("");
                                        tooltip_Message = moneyErrorMsg;
                                    }
                                    else if (temp < min) {
                                        if (temp.length >= 2) {                                           
                                            $(obj).val("");
                                            tooltip_Message = moneyErrorMsg;
                                        }
                                    }
                                    else {
                                        if ((e.keyCode == "37") || (e.keyCode == "38") || (e.keyCode == "39") ||(e.keyCode == "40")) {
                                            //no nothing
                                        }
                                        else{
                                           $(obj).val(new Intl.NumberFormat("en-US", { style: "currency", currency: "USD", minimumFractionDigits: 0, maximumFractionDigits: 0 }).format(temp));
                                        } 
                                    }                                
                                }
                            }
                      
                    break;

                case 'percentage':
                               
                        $(obj).attr('maxlength', maxlength +1);
                        
                        var allowedKeys = new Array();
                        allowedKeys.push(8); // backspace
                        allowedKeys.push(9); // tab
                        allowedKeys.push(16); // shift
                        allowedKeys.push(20); // caps lock
                        allowedKeys.push(37); // arrow left
                        allowedKeys.push(38); // arrow up
                        allowedKeys.push(39); // arrow right
                        allowedKeys.push(40); // arrow down

                        allowedKeys.push(48);
                        allowedKeys.push(49);
                        allowedKeys.push(50);
                        allowedKeys.push(51);
                        allowedKeys.push(52);
                        allowedKeys.push(53);
                        allowedKeys.push(54);
                        allowedKeys.push(55);
                        allowedKeys.push(56);
                        allowedKeys.push(57);
                        allowedKeys.push(96);
                        allowedKeys.push(97);
                        allowedKeys.push(98);
                        allowedKeys.push(99);
                        allowedKeys.push(100);
                        allowedKeys.push(101);
                        allowedKeys.push(102);
                        allowedKeys.push(103);
                        allowedKeys.push(104);
                        allowedKeys.push(105);
                        var percentageErrorMsg = "Allowed Range : "+benfitRange_Min +" - "+ benfitRange_Max +"%";
                        if (!(allowedKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {
                            $(obj).val("");
                            tooltip_Message = percentageErrorMsg
                        }
                        else {
                           
                                        var temp = parseInt(obj.value.replace("%", ""));
                                        if (isNaN(temp))
                                        {
                                            $(obj).val("");
                                            tooltip_Message = percentageErrorMsg;
                                        }else{                            
                                            if (temp > max) {
                                                $(obj).val("");
                                                tooltip_Message = percentageErrorMsg;
                                            }
                                            else if (temp < min) {
                                                if (temp.length >= 2) {
                                                    $(obj).val("");
                                                    tooltip_Message = percentageErrorMsg;
                                                }
                                            }
                                            else {
                                                $(obj).val(temp + "%");
                                            }
                                        }                               
                                    }
                    
                    break;

                default:
                    console.log("default");
                    break;
                    }
            
            if (tooltip_Message != "") {
                div.addClass("tooltip");
                div.append("<span style='visibility: visible !important;' class='tooltiptext'>" + tooltip_Message + '</span>');
                /* $('.tooltip').trigger('mouseenter');  */               
            }
        }
    



        function changeCSS()
        {
           // $("#DownloadPBP").addClass("active");    
        }
    </script>
    <script>
        //for showing and hiding buttons and repeater
        function ShowHideDynamicUploadDiv() {
            var chkBenefitChangeOption = document.getElementById('chkBenefitChangeOption');
            
            var excelUpload = document.getElementById('excelUpload');
            var dynamicUpload = document.getElementById('dynamicUpload');
            var btnSimulate = document.getElementById('btnSimulate');
            var lbRevert = document.getElementById('lbRevert');
           
            if (chkBenefitChangeOption.checked) {
                excelUpload.style.display = 'block';
                dynamicUpload.style.display = 'none';
                btnSimulate.style.display = 'none';
                lbRevert.style.display = 'none';
            }
            else {
                excelUpload.style.display = 'none';
                dynamicUpload.style.display = 'block';
                btnSimulate.style.display = 'block';
                lbRevert.style.display = 'block';
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
    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
    <div class="container-fluid" >
        <div class="row justify-content-center mt-0">
            <div class="col-md-12 text-center p-0 mt-3 mb-2">
                <div class="card px-0 pt-3 pb-0 mt-1 mb-1">
                    <div class="row">
                        <div class="col-md-12 mx-0">
                            <ul id="progressbar">
                                <li id="CreateScenario" class="active">
                                    <strong>
                                    <asp:LinkButton class="itemcolor" ID="LBScenarios" runat="server" Text="Create Scenario" OnClick="LBScenarios_Click"></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="DownloadPBP" runat="server" ClientIDMode="Static" >
                                    <strong>
                                    <asp:LinkButton class="itemcolor" ID="LBPlans" runat="server" Text="Plan Selection" OnClick="LBPlans_Click" ></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="UploadPBP">
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBPlans_UploadPBP" runat="server" Text="UPLOAD PBP / CHANGE BENEFIT(S)" ></asp:LinkButton>
                                    </strong>
                                </li>
                                <li id="ResultsReady" >
                                    <strong>
                                        <asp:LinkButton Enabled="false" class="itemcolor" ID="LBPlans_ResultsReady" runat="server" Text="Results Ready" ></asp:LinkButton>
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
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
                        <hr style="margin-top: 0;">
                        <div class="row">
                            <div class="col-md-6">Note: 
                                <span style="font-size: 12px;font-style: italic;">Please change the Significant Benefits below (or in excel) to see the impact on your enrollment.</span>
                            </div>
                            <div class="col-md-6 text-right">
                                <asp:CheckBox ID="chkBenefitChangeOption" ClientIDMode="Static" runat="server" onclick="ShowHideDynamicUploadDiv()" Text="Do you wish to Download & Upload the Benefits?" />
                            </div>
                        </div>
                    </div>


                     <div class="scenario-table-container" >
                         <div class="filter" >
                             <div id="dynamicUpload" class="custom-box-shadow" runat="server" ClientIDMode="Static" style="width:100%;">   
                                <asp:Repeater ID="rptBenefits" runat="server" >
                                     <HeaderTemplate>  
                                         <table class="fl-table" style="width:100%;">
                                             <div class ="significant-benefit-list">Significant Benefit List 
                                                  	<a class="button" href="#significant-benefit-desc"> <svg   style="color:#fff;margin-top: -5px;" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-info"><circle cx="12" cy="12" r="10"></circle><line x1="12" y1="16" x2="12" y2="12"></line><line x1="12" y1="8" x2="12.01" y2="8"></line>
                                                     </svg></a>      
                                             </div>

                                             <div id="significant-benefit-desc" class="overlay">
                                                 <div class="popup">
                                                     <h4>INFO</h4>
                                                     <hr />
                                                     <a class="close" href="#">&times;</a>
                                                     <div class="content">
                                                             <b>"Significant Benefits</b> have significant evidence of correlation to the change in enrollment. The other benefits are not going to have much change in the enrollment, if you maintain it within a competitive range.
                                                         <br /> <br />
                                                              These significant benefits need to be offered in the market to get an upper hand. Most often than not, the significant benefit’s copay coins will have a linear negative impact – which means if you increase the costs, the enrollment will decrease and vice versa. However, the relationships are not always linear and optimal costs can also operate in a range."

                                                         </div>
                                                 </div>
                                             </div>


                                             <thead>
                                                 <tr>
                                                     <th scope="col" style="width: 10%">Benefit Group
                                                     </th>
                                                     <%if (this.IsChecked)
                                                     { %>
                                                     <th scope="col">Benefits
                                                     </th>
                                                     <%} %>
                                                     <th scope="col" style="width: 60%">Benefit Description
                                                     </th>
                                                     <th scope="col" style="width: 10%">Current Value
                                                     </th>
                                                     <th scope="col" style="width: 13%">New Value
                                                     </th>
                                                 </tr>
                                             </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                             <tr>
                                               <td style="width: 10%">
                                                  <asp:Label ID="lblBenefitGrp" runat="server" Text='<%# Eval("Benefit Group") %>' />
                                               </td>
                                                 <%if(this.IsChecked){ %>
                                                   <td >
                                                      <asp:Label ID="lblBenefits" runat="server" Text='<%# Eval("Benefits") %>' />
                                                   </td>
                                                  <%} %>
                                               <td Style="width: 60%;">
                                                    <asp:Label ID="lblBenefitDesc" CssClass="BenefitDesc" runat="server" Text='<%# Eval("Benefit Description") %>' style="width:700px !important; max-width:1366px; display:block; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"></asp:Label>
                                               </td>
                                               <td style="width: 10%; text-align:right">
                                                   <asp:Label ID="lblCurrent" runat="server" Text='<%# Eval("Current Value") %>'></asp:Label>     
                                               </td>
                                               <td style="width: 13%">
                                                 <div style="display:inline-block;width: 85%;">                                                    
                                                    <asp:TextBox ID="lblChanged" autocomplete="off" runat="server" Text='<%# Eval("Current Value") %>' CssClass="form-control change-benefit-value" 
                                                        data-comma-attr ='<%# Eval("isComma") %>' 
                                                        data-bdt-attr ='<%# Eval("Benefit Data Type").ToString().ToLower() %>' 
                                                        data-bcv-attr ='<%# Eval("Current Value").ToString().ToLower() %>' 
                                                        data-br-attr ='<%# Eval("Range").ToString().ToLower() %>' 
                                                        onkeydown="dynamicFormat(this,event)"
                                                        onmouseenter="removeToolTip()"
                                                        onkeyup="dynamicFormat(this,event)"/>                                                  
                                                 </div>
                                                 <div style="display:inline-block;">
                                                     <svg style="width:65%" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path></svg>  
                                                 </div>
                                               </td>
                                            </tr>
                                  </ItemTemplate>
                                  <FooterTemplate>
                                            </table>
                                  </FooterTemplate>
                              </asp:Repeater>
                            </div>                                                     
                            <div id="excelUpload" runat="server" class="custom-box-shadow" style="width:100%; display: none; height:240px;  margin: 15px 0" ClientIDMode="Static">                            
                                
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                
                                    <ContentTemplate>
                                        <asp:FileUpload ID="UploadFile" ClientIDMode="Static" Style="display: none" runat="server" CssClass="sim simbtn ml-auto download-link" />   
                                        <div style="width:50%; height:240px; padding: 50px 30px; display:inline-block; background-color: #BDA7C4 !important; color: #fff;">     
                                            <span> 
                                                <h3 style="text-decoration: underline;">Instructions:</h3>
                                                <ul>
                                                    <li class="custom-instruction">Download the PDP file.</li>
                                                    <li class="custom-instruction">Open the downloaded PDP file & change the benefit value(s) for simulation.</li>
                                                    <li class="custom-instruction">Save & Upload the PDP file with the same file name (bid_id).</li>
                                                </ul>
                                            </span>
                                        </div>    
                                        <div style="width: 50%; height:220px;  display: inline-block; background-color: #eee !important; float:right;">
                                            <div style="width: 50%; display: inline-block; text-align: center; margin: 65px auto;">
                                                <asp:LinkButton style="min-width: 60%; padding: 15px; border-radius: 10px; font-weight: bold;" runat="server" ID="Download" ValidationGroup="Simulate" CssClass="sim btn ml-auto download-link" OnClick="Download_Click" ClientIDMode="Static" OnClientClick="changeCSS();">
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg> Download PBP
                                                    <span style="display: block; font-size: 10px; text-align: center; padding: 5px 0 0px 0; border-top: 1px solid #fff; margin: 5px 0 0 0;">(Existing Benefit(s) Data)</span>
                                                </asp:LinkButton>
                                               
                                            </div>
                                            <div style="width: 50%;  display: inline-block;float: right; text-align: center; margin: 65px auto;">     
                                                <asp:LinkButton style="min-width: 60%; padding: 15px; border-radius: 10px; font-weight: bold;" runat="server" ID="InitializeUpload" ValidationGroup="Simulate" CssClass="sim btn ml-auto download-link" OnClick="InitializeUpload_Click" ClientIDMode="Static" OnClientClick="changeCSS();">                                                    
                                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-upload"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="17 8 12 3 7 8"></polyline><line x1="12" y1="3" x2="12" y2="15"></line></svg> Upload PBP
                                                    <span onclick="InitializeUploadDialog();" style="display: block; font-size: 10px; text-align: center; padding: 5px 0 0px 0; border-top: 1px solid #fff; margin: 5px 0 0 0;">(Changed Benefit(s) Data)</span>
                                                 </asp:LinkButton>                                                
                                                <asp:Button Text="Trigger Upload" OnClientClick="ShowModalPopup('pload');"  ClientIDMode="Static" Style="display: none" runat="server" ID="Upload" OnClick="Upload_Click" CssClass="sim btn ml-auto download-link" />
                                            </div>
                                            <div id="bidIdFileName" runat="server" style="padding: 4px 15px; font-size: 12px; font-weight: 700; background-color: #eeeeee; width: 100%; text-align: right; border-top: 1px solid #d3d3d3;"></div>
                                        </div>                                           
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Upload" />
                                        <asp:PostBackTrigger ControlID="Download" />
                                    </Triggers>
                                </asp:UpdatePanel>                            
                            </div>                            
                         </div>
                    </div>
                </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </div>
    </section>
    <div class="bottom-controls" style="position: fixed;bottom: 0;left: 0;">
                <asp:LinkButton runat="server" CssClass="control-link refresh-link" ID="lbRevert" OnClick="lbRevert_Click" ToolTip="Revert Changes" ClientIDMode="Static">
                   <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-corner-up-left"><polyline points="9 14 4 9 9 4"></polyline><path d="M20 20v-7a4 4 0 0 0-4-4H4"></path></svg>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;
                <asp:Button Text="Simulate" runat="server" ID="btnSimulate" OnClientClick="ShowModalPopup('pload');" OnClick="btnSimulate_Click"  CssClass="sim btn ml-auto download-link" ValidationGroup="Simulate"  ClientIDMode="Static" />
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
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
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



