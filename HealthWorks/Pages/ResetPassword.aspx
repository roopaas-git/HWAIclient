<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="HealthWorks.Pages.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HealthWorksAI</title>
    <link rel="icon" href="../dist/Images/favicon.png" type="image/png" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" />
    <link href="../dist/css/main.css" rel="stylesheet" />
    <link href="../dist/CSS/Help.css" rel="stylesheet" type="text/css" />
    <link href="../dist/css/TEG_Custom_Styles.css" rel="stylesheet" />
    <link href="../dist/css/style1.css" rel="stylesheet" />
    <link href="../dist/css/teg1.css" rel="stylesheet" />
    <link href="../dist/css/Simstyle.css" rel="stylesheet" />
    <link href="../dist/css/Simteg.css" rel="stylesheet" />
    <link href="../dist/css/HeaderStyles.css" rel="stylesheet" />
    <style>
        .btnActivate {
            background: linear-gradient(90deg, #5C276E, #bda7ce 70%) !important;
            /*border-color: #F16365 !important;*/
            font-weight: 500;
            border-radius: 2px;
            color: #fff;
            transition: all ease 0.3s;
            padding: 0px 5px;
        }

            .btnActivate:hover {
                background-color: #E5EDD5 !important;
                border-color: #5C276E !important;
                box-shadow: 0 3px 6px #5C276E;
            }

        .labelcolor {
            color: #5c276e;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManagerId" ScriptMode="Release" AsyncPostBackTimeout="360000">
        </asp:ScriptManager>
        <div id="wrapper" class="wrapper">
            <header class="hw-header fixed-top">
                <nav class="navbar navbar-expand-md navbar-light bg-white">
                    <div class="container-fluid">
                        <div class="hw-header-logo-container">
                            <a class="navbar-brand hw-header-logo primary">
                                <img src="../dist/Images/healthworks_ai-logo.png" alt="healthworks logo">
                            </a>
                        </div>
                        <div id="hwNavbar" class="hw-header-navbar">
                        </div>
                    </div>
                </nav>
            </header>
            <div id="main-content-wrapper" class="content-wrapper expanded-full">
                <div class="content">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="main-header" runat="server" id="divMainHeader">
                                <h2>
                                    <asp:Label Text="Reset Password" runat="server" ID="pageName" /></h2>
                                <em>
                                    <asp:Label Text="" runat="server" ID="lbHeaderDescription" /></em>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="main-content">
                        <div class="form-group row">
                            <div class="col-sm-12">
                                <div class="widget" id="reset" runat="server">
                                    <div class="widget-header">
                                        <h3><i class="fa fa-key" aria-hidden="true"></i>Reset your password</h3>
                                        <div class="btn-group widget-header-toolbar" id="divCreateTicket" runat="server"
                                            clientidmode="Static">
                                            <a href="#" title="Focus" class="btn-borderless btn-focus" id="lbCreateTicketFocus"
                                                runat="server" clientidmode="Static"><i class="fa fa-eye"></i></a><a href="#" title="Expand/Collapse"
                                                    class="btn-borderless btn-toggle-expand" id="lbCreateTicket" runat="server" clientidmode="Static">
                                                    <i class="fa fa-chevron-up"></i></a>
                                        </div>
                                    </div>
                                    <div class="widget-content">
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <asp:Label ID="Label1" Text="Email :" runat="server" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" ID="txtPWD" CssClass="form-control" TextMode="Password"
                                                    Text="" ClientIDMode="Static" EnableViewState="false" Visible="false" />
                                                <asp:TextBox runat="server" ID="txt_Email" CssClass="form-control disabled"
                                                    Text="" ClientIDMode="Static" EnableViewState="false" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="required"
                                                    ControlToValidate="txt_Email" ValidationGroup="Check" ForeColor="Red" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <asp:Label ID="Label2" Text="New Password :" runat="server" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" ID="txtNewPassword" CssClass="form-control" TextMode="Password" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="required"
                                                    ControlToValidate="txtNewPassword" ValidationGroup="Check" ForeColor="Red" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                                <asp:Label ID="Label3" Text="Confirm Password :" runat="server" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" ID="txtConfirmPWD" CssClass="form-control" TextMode="Password" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="required"
                                                    ControlToValidate="txtConfirmPWD" ValidationGroup="Check" ForeColor="Red" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="txtNewPassword"
                                                    ControlToValidate="txtConfirmPWD" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                                                    ValidationGroup="ChangePassword1" CssClass="form-control-required" ForeColor="Red"></asp:CompareValidator>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Button ID="Button1" Text="Reset" runat="server" CssClass="btn form-control"
                                                    ValidationGroup="Check" OnClick="btnChangePWD_Click" />
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row" id="resetsucess" runat="server" visible="false">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-8" style="text-align: center">
                                        <br />
                                        <br />
                                        <h2 style="text-align: center">
                                            <asp:Label Text="Reset password confirmation" runat="server" ID="Label4" CssClass="labelcolor" /></h2>

                                        <br />
                                        <br />
                                        <asp:Label runat="server" ID="lbl_Activetext" CssClass="labelcolor" Text="Your password has been reset, Click on the button below to login." />
                                        <br />
                                        <br />
                                        <asp:Button Text="Login" runat="server" ID="btnLogin" CssClass="btn form-control"
                                            ClientIDMode="Static" OnClick="btnLogin_Click" Width="200px" Height="35px" />
                                    </div>
                                </div>
                                <div class="form-group row" id="resetfailure" runat="server" visible="false">
                                     <div class="col-md-2"></div>
                                    <div class="col-md-8" style="text-align: center">
                                        <br />
                                        <br />
                                        <h2 style="text-align: center">
                                            <asp:Label Text="Link has been expired" runat="server" ID="Label5" CssClass="labelcolor" /></h2>

                                        <br />
                                        <br />
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://code.jquery.com/jquery-3.4.1.slim.min.js"
            integrity="sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n"
            crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"
            integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo"
            crossorigin="anonymous"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"
            integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"
            crossorigin="anonymous"></script>
    </form>
    </form>
</body>
</html>
