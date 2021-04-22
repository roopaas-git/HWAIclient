<%@ Page Title="HealthWorksAI - 2021 Post AEP Findings Report" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="PostAEPFindingsReport.aspx.cs" Inherits="HealthWorks.Pages.PostAEPFindingsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #lbFullScreen {
            text-align: right !important;
            font-weight: 700;
            text-transform: capitalize;
            font-family: Sans-Serif;
        }

            #lbFullScreen:hover {
                color: Orange;
                text-decoration: Underline !important;
                font-family: Sans-Serif;
            }

        #htmlDiv {
            position: absolute;
            top: 0px;
            left: 300px;
            height: 650px;
            width: 1130px;
        }

        #htmlFrame {
            position: absolute;
            top: 0px;
            left: 0px;
            margin: 10px 10px 10px 10px;
            padding: 0px 0px 0px 0px;
            height: 630px;
            width: 1100px;
        }

        #overlap {
            position: absolute;
            background-color: transparent;
            top: 0px;
            left: 0px;
            margin: 10px 10px 10px 10px;
            padding: 0px 0px 0px 0px;
            width: 1100px;
            height: 630px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="content">
                <div class="main-content">
                    <div class="row tableau-container">
                        <asp:LinkButton ID="lbFullScreen" Width="98%" Font-Size="Small" runat="server" OnClick="lbFullScreen_Click"
                            class="text" Font-Underline="false" Text="View In Fullscreen" ClientIDMode="Static"></asp:LinkButton>
                        <iframe id="pdfdisplay" runat="server" width="99%"
                            height="600px"></iframe>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
