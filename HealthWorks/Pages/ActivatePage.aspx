<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivatePage.aspx.cs" Inherits="HealthWorks.Pages.ActivatePage" %>

<!DOCTYPE html>
<html lang="en" class="no-js">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>HealthWorksAI</title>
    <meta name="description" content="" />
    <link rel="icon" href="../dist/Images/favicon.png" type="image/png">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.0/css/all.min.css"
        integrity="sha256-h20CPZ0QyXlBuAw7A+KluUYx/3pK+c7lYEpqLTlxjYQ=" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"
        integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
    <link rel="stylesheet" href="../dist/css/Teg_Login_Custom_Styles.css" />
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
            color:#5c276e;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
            <div class="hw-pagecontent">
                <div class="form-group row">
                    <div class="col-md-2"></div>
                    <div class="col-md-8" style="text-align: center">
                        <br />
                        <br />
                        <h2 style="text-align: center">
                            <asp:Label Text="Just one more step..." runat="server" ID="pageName" CssClass="labelcolor" /></h2>
                        <asp:Label runat="server" ID="lblUserName" CssClass="labelcolor" Font-Bold="true" />
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lbl_Activetext" CssClass="labelcolor" />
                        <br />
                        <br />
                        <asp:Button Text="Activate Account" runat="server" ID="btnActivate" CssClass="btnActivate"
                            ClientIDMode="Static" OnClick="btnActivate_Click" Width="200px" Height="35px" />
                        <asp:Button Text="Login" runat="server" ID="btnLogin" CssClass="btnActivate"
                            ClientIDMode="Static" OnClick="btnLogin_Click" Width="200px" Height="35px" />
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
</body>
</html>


