<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HealthMaster.Master" AutoEventWireup="true" CodeBehind="Whitepaper.aspx.cs" Inherits="HealthWorks.Pages.Whitepaper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
        .main-header {
            margin-bottom: 10px !important;
        }
    </style>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            if ($(window).width() < 992) {
                if (!$('.left-sidebar').hasClass('sidebar-float-active')) {
                    $('.left-sidebar').addClass('sidebar-float-active');
                } else {
                    $('.left-sidebar').removeClass('sidebar-float-active');
                }
            } else {
                if (!$('.left-sidebar').hasClass('sidebar-hide-left')) {
                    $('.left-sidebar').addClass('sidebar-hide-left');
                    $('.content-wrapper').addClass('expanded-full');
                } else {
                    $('.left-sidebar').removeClass('sidebar-hide-left');
                    $('.content-wrapper').removeClass('expanded-full');
                }
                $('#id_ProviderIntelligence').find('i').removeClass('fa-angle-right').addClass('fa-angle-down')
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content">
                <div class="main-content">
                    <div class="row tableau-container">
                        <iframe align="center" frameborder="0" scrolling="yes" runat="server" id="tableauViewFrame"
                            style="height: 6900px; width: 100%;" clientidmode="Static"></iframe>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
