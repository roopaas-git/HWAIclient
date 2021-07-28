<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestDemo.aspx.cs" Inherits="HealthWorks.RequestDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>HealthWorksAI</title>
    <link rel="icon" href="../dist/Images/favicon.png" type="image/png" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.0/css/all.min.css"
        integrity="sha256-h20CPZ0QyXlBuAw7A+KluUYx/3pK+c7lYEpqLTlxjYQ=" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"
        integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
    <link rel="stylesheet" href="dist/css/TEG_Custom_Styles.css" />

    <style type="text/css">
        .widget .widget-header {
            float: left;
            width: 100%;
            background-color: #5C276E !important;
            color: #0c8bd9;
            height: auto !important;
            border-width: 0px !important;
            padding: 20px 25px 20px 25px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        .login-container1 {
            max-width: 600px;
            margin: 0px auto;
            margin-top: 50px;
            overflow: hidden;
            background-color: transparent;
            box-shadow: none !important;
        }

        .text {
            text-align: left;
            color: black;
            margin-left: 20px;
            font-size:16px;
        }

        .form-group {
            padding-top: 10px !important;
            padding-bottom: 10px !important;
        }

        .firstrow {
            padding-top: 50px !important;
            padding-bottom: 10px !important;
        }
    </style>
</head>
<body class="MainWrapper">
    <form id="form1" runat="server">
        <div class="login-container1" style="text-align: center;">
            <div class="main-content">
                <div class="widget userCreation">
                    <div class="widget-header">
                        <img src="dist/Images/healthworks_ai-logo-white_login.png" width="267px" height="56px" />
                    </div>
                    <div class="widget-content">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group row firstrow">
                                    <div class="col-sm-3 text">
                                        <asp:Label ID="Label2" Text="Name" runat="server">                                           
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control " />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required"
                                            ControlToValidate="txtFirstName" ForeColor="Red" ValidationGroup="upload" runat="server"
                                            CssClass="form-control-label" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-3 text">
                                        <asp:Label ID="Label3" Text="Company" runat="server" />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtCompanyName" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-2 control-label">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required"
                                            CssClass="form-control-label" ControlToValidate="txtCompanyName" ForeColor="Red"
                                            ValidationGroup="upload" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-3 text">
                                        <asp:Label ID="Label7" Text="Request Demo" runat="server" />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList runat="server" ID="ddlUserType" CssClass="custom-select">
                                            <asp:ListItem Text="Select" Value="Select" Selected="True" />
                                            <asp:ListItem Text="MarketIntel" Value="MarketIntel" />
                                            <asp:ListItem Text="ProductIntel" Value="ProductIntel" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Required"
                                            CssClass="form-control-label" ControlToValidate="ddlUserType" ForeColor="Red"
                                            ValidationGroup="upload" runat="server" InitialValue="Select" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-3 text">
                                        <asp:Label ID="Label6" Text="Email" runat="server" />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Required"
                                            CssClass="form-control-label" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="upload"
                                            runat="server" />
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-sm-4 text">
                                        <asp:Label ID="Label1" runat="server" />
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button Text="Register" runat="server" ID="btnRegister" CssClass="btn form-control"
                                            ClientIDMode="Static" ValidationGroup="upload" OnClick="btnRegister_Click"/>
                                    </div>
                                    <div class="col-sm-2 control-label">
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>

            </div>
        </div>
        <%--<div class="main-content" style="text-align: center;">
           
            </div>

            <iframe frameborder="0" style="height: 650px; width: 99%; border: none;" src='https://forms.zohopublic.com/arvindprasad/form/ClientDetails/formperma/KVIlXfAM1Vf5qVQvCs2IQzHcszWyleVygH61UzaH_qk'></iframe>
        </div>--%>
    </form>
</body>
</html>
