<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HealthWorks.Pages.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HealthWorks | TEG</title>
    <meta charset="utf-8" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
    <link href="../dist/css/main.css" rel="stylesheet" />
    <link href="../dist/css/TEG_Custom_Styles.css" rel="stylesheet" />
    <link href="../dist/css/Card.css" rel="stylesheet" />
    <style type="text/css">
        .card-title {
            font-size: 14px;
        }

        .card-body {
            background-color: #7E538C;
            color: #FFF;
            height: 30px;
        }

            .card-body > h4 {
                text-align: center;
                vertical-align: middle;
            }

        .card-title {
            margin-top: -10px !important;
        }

        .cardDesc {
            visibility: hidden;
        }

        .card:hover .overlay {
            opacity: 1;
        }

        .card:hover .overlaylg {
            opacity: 1;
        }

        .card {
            height: 320px;
        }


        .overlay {
            position: absolute;
            top: 50;
            bottom: 0;
            left: 0;
            right: 0;
            height: 35%;
            width: 100%;
            opacity: 0;
            background-color: #FFF;
            padding-bottom: 10px;
        }

        .overlaylg {
            position: absolute;
            top: 50;
            bottom: 0;
            left: 0;
            right: 0;
            height: 50%;
            width: 100%;
            opacity: 0;
            background-color: #FFF;
        }

        .text {
            color: white;
            font-size: 14px;
            position: absolute;
            top: 28%;
            width: 100%;
            -webkit-transition: all 0.5s linear;
            -moz-transition: all 0.5s linear;
            -o-transition: all 0.5s linear;
            transition: all 0.5s linear;
        }

        .textlg {
            top: 21%;
        }


        li {
            float: left;
            width: 100%;
            line-height: 25px;
        }

        .card-title-overlay {
            width: 100%;
            background-color: #7E538C;
            height: 30px;
            font-size: 14px;
            text-align: center;
            vertical-align: middle;
            padding-top: 10px;
            padding-bottom: 10px;
        }

            .card-title-overlay > h4 {
                font-size: 14px;
                color: #FFF;
            }

        .content-wrapper {
            background-color: #fff;
        }

        .container-strip > h6 {
            text-align : center;
            padding-top: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper" class="wrapper">
            <div class="top-bar navbar-fixed-top">
                <div class="container">
                    <div class="clearfix">
                        <div class="pull-left left logo international">
                            <a href="NewHome.aspx">
                                <img src="../dist/Images/HWLogo.png" alt="HealthWorks AI" class="logo-teg" />
                            </a>
                        </div>
                        <div class="pull-right">
                            <div class="top-bar-right">
                                <div class="logged-user">
                                    <h5 style="font-size: 22px; margin-right: 7px;">
                                        <asp:Label ID="lblwelcometext" runat="server" Text="SUBBU"></asp:Label></h5>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="content-wrapper">
            <div class="row" style="margin: 0px;">
                <div class="container-strip">
                    
                        <h6>-- NEWS TICKER --</h6>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="row">
                    <div class="card" style="width: 23%">
                        <img class="card-img-top" src="../dist/CardImages/CUS-ACQ.png" alt="Card image" style="width: 100%" />
                        <div class="card-body">
                            <h4 class="card-title">Market Intelligence</h4>
                        </div>
                        <div class="overlay">
                            <div class="card-title-overlay">
                                <h4>Market Intelligence</h4>
                            </div>
                            <div class="text">
                                <ul>
                                    <li>
                                        <asp:LinkButton Text="Competitor Analysis" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Oppurtunity Analysis" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Market Snapshot" runat="server" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="card" style="width: 23%">
                        <img class="card-img-top" src="../dist/CardImages/CUS-RET.png" alt="Card image" style="width: 100%" />
                        <div class="card-body">
                            <h4 class="card-title">Product Intelligence</h4>
                        </div>
                        <div class="overlaylg">
                            <div class="card-title-overlay">
                                <h4>Product Intelligence</h4>
                            </div>
                            <div class="text textlg">
                                <ul>
                                    <li>
                                        <asp:LinkButton Text="Plan Comparision 2020" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Plan Comparision 2020(download)" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="MRX Plan Comparision 2020" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Winning Plans 2020" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Product Insights 2020" runat="server" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="card" style="width: 23%">
                        <img class="card-img-top" src="../dist/CardImages/CUS-SCORE-CARD.png" alt="Card image" style="width: 100%" />
                        <div class="card-body">
                            <h4 class="card-title">Simulator</h4>
                        </div>
                        <div class="overlay">
                            <div class="card-title-overlay">
                                <h4>Simulator</h4>
                            </div>
                            <div class="text">
                                <ul>
                                    <li>
                                        <asp:LinkButton Text="OOPC Simulator" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="MPF Simulator" runat="server" /></li>
                                    <li></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="card" style="width: 23%">
                        <img class="card-img-top" src="../dist/CardImages/HA.png" alt="Card image" style="width: 100%" />
                        <div class="card-body">
                            <h4 class="card-title">Planning</h4>
                        </div>
                        <div class="overlay">
                            <div class="card-title-overlay">
                                <h4>Planning</h4>
                            </div>
                            <div class="text">
                                <ul>
                                    <li>
                                        <asp:LinkButton Text="Enrollement Trends" runat="server" /></li>
                                    <li></li>
                                    <li></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="card" style="width: 23%">
                        <img class="card-img-top" src="../dist/CardImages/CUS-ACQ.png" alt="Card image" style="width: 100%" />
                        <div class="card-body">
                            <h4 class="card-title">Post AEP Analysis</h4>
                        </div>
                        <div class="overlaylg">
                            <div class="card-title-overlay">
                                <h4>Post AEP Analysis</h4>
                            </div>
                            <div class="text textlg">
                                <ul>
                                    <li>
                                        <asp:LinkButton Text="Enrollement Analysis" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Competitor Analysis" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Regional Analysis" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Product Analysis" runat="server" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="card" style="width: 23%">
                        <img class="card-img-top" src="../dist/CardImages/CUS-ACQ.png" alt="Card image" style="width: 100%" />
                        <div class="card-body">
                            <h4 class="card-title">Data Slicer</h4>
                        </div>
                        <div class="overlaylg">
                            <div class="card-title-overlay">
                                <h4>Data Slicer</h4>
                            </div>
                            <div class="text textlg">
                                <ul>
                                    <li>
                                        <asp:LinkButton Text="Competitor Analysis" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Population Health" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Census" runat="server" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="card" style="width: 23%">
                        <img class="card-img-top" src="../dist/CardImages/CUS-ACQ.png" alt="Card image" style="width: 100%" />
                        <div class="card-body">
                            <h4 class="card-title">Performance Management</h4>
                        </div>
                        <div class="overlay">
                            <div class="card-title-overlay">
                                <h4>Performance Intelligence</h4>
                            </div>
                            <div class="text">
                                <ul>
                                    <li>
                                        <asp:LinkButton Text="Restrospective Report" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Sales & Terminations" runat="server" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="card" style="width: 23%">
                        <img class="card-img-top" src="../dist/CardImages/CUS-ACQ.png" alt="Card image" style="width: 100%" />
                        <div class="card-body">
                            <h4 class="card-title">Provider Intelligence</h4>
                        </div>
                        <div class="overlay">
                            <div class="card-title-overlay">
                                <h4>Provider Intelligence</h4>
                            </div>
                            <div class="text">
                                <ul>
                                    <li>
                                        <asp:LinkButton Text="Hospital Compare" runat="server" /></li>
                                    <li>
                                        <asp:LinkButton Text="Provider Scorecard" runat="server" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
