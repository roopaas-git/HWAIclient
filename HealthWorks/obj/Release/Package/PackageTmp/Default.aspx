<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HealthWorks.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>HealthWorksAI - Login</title>
    <meta name="description" content="" />
    <link rel="icon" href="dist/Images/favicon.png" type="image/png" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.0/css/all.min.css"
        integrity="sha256-h20CPZ0QyXlBuAw7A+KluUYx/3pK+c7lYEpqLTlxjYQ=" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"
        integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous" />
    <link rel="stylesheet" href="dist/css/Teg_Login_Custom_Styles.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <style>
        .text-danger {
            color: #fcf301 !important;
        }
    </style>
    <style>
        .item {
            background: #333;
            text-align: center;
            height: 200px !important;
        }

        .control-buttons {
            text-align: center;
        }

        .transition-timer-carousel-progress-bar {
            height: 3px;
            background-color: #ffffff;
            width: 0%;
            max-width: 100%;
            margin: 0px 0px 0px 0px;
            border: none;
            z-index: 11;
            position: relative;
        }

        .textspan {
            font-size: 36px !important;
        }

        .image {
            font-size: 12px;
            text-align: center !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            var percent = 0, i = 0, bar = $('.transition-timer-carousel-progress-bar'), crsl = $('#homeCarousel');
            function progressBarCarousel() {
                bar.css({ width: percent + '%' });
                percent = percent + 0.15;
                if (percent > 97) {
                    percent = 97;
                    i = i + 1;
                    if (i == 10) {
                        percent = 0;
                        i = 0;
                        crsl.carousel('next');
                    }
                }
                //if (percent > 97) {
                //    percent = 97;
                //    setTimeout(function () {
                //        percent = 0;
                //        crsl.carousel('next');
                //    }, 500);
                //}
            }
            crsl.carousel({
                interval: false,
                pause: true
            }).on('slid.bs.carousel', function () { }); var barInterval = setInterval(progressBarCarousel, 15);
            crsl.hover(
                function () {
                    clearInterval(barInterval);
                },
                function () {
                    barInterval = setInterval(progressBarCarousel, 15);

                })
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" class="hw-login-form" style="background-color: #5c276e">
        <div class="hw-page login-page">
            <header class="hw-header fixed-top">
                <nav class="navbar navbar-expand-md">
                    <div class="container-fluid">
                        <div class="hw-header-logo-container">
                            <a class="navbar-brand hw-header-logo primary" href="#">
                                <img src="dist/Images/healthworks_ai-logo-white_login.png" alt="healthworks logo">
                            </a>
                        </div>
                    </div>
                </nav>
            </header>
            <div class="hw-pagecontent">
                <div class="hw-body">
                    <div class="login-page-bg"></div>
                    <div id="homeCarousel" class="hw-carousel carousel slide carousel-fade" data-ride="carousel"
                        data-interval="4000">
                        <div class="carousel-inner">
                            <hr class="transition-timer-carousel-progress-bar" />
                            <div class="carousel-item active">
                                <div class="carousel-item-content">
                                    <div class="carousel-item-caption">
                                        <%--  <h2 class="carousel-item-caption-header">Design competitive MA plans using ProductIntel!</h2>
                                        <p class="carousel-item-caption-desc">
                                            Comprehensive self-serve analytics around bid development, product portfolio design, performance management and product lifecycle management. 
											<br />
                                            <br />
                                            Reach out to your account manager to measure & manage your product portfolio!
                                        </p>
                                      <a href="#" class="btn btn-sm btn-light">Learn more</a>--%>

                                        <div class="row">
                                            <div class="col-sm-8">
                                                <p class="carousel-item-caption-desc-img">
                                                    <span class="textspan">Marketing<i>Intel</i>&trade;</span>
                                                    provides your marketing team with a robust, on-demand centralized solution to increase marketing ROI and gain a higher share of the market.
                                                </p>
                                                <a href="RequestDemo.aspx" target="_blank" class="btn btn-sm btn-light">Book a Demo</a>
                                            </div>
                                            <div class="col-sm-4" style="text-align:center;">
                                                <a href="dist/Images/banner1.png" target="_blank">
                                                    <img src="dist/Images/banner1.jpg" /></a> <span class="image"><i>Click on image to enlarge</i></span>
                                            </div>
                                        </div>

                                    </div>
                                    <ol class="carousel-indicators">
                                        <li data-target="#homeCarousel" data-slide-to="0" class="active"></li>
                                        <li data-target="#homeCarousel" data-slide-to="1"></li>
                                        <li data-target="#homeCarousel" data-slide-to="2"></li>
                                    </ol>
                                </div>
                            </div>
                            <div class="carousel-item">
                                <div class="carousel-item-content">
                                    <div class="carousel-item-caption">
                                        <%--  <h2 class="carousel-item-caption-header">2021 AEP Findings Report</h2>
                                        <p class="carousel-item-caption-desc">
                                            More plans, lower premiums, and increased Supplemental Benefits are the key Medicare Advantage trends of 2021.
                                            <br />
                                            <br />
                                            Available <a href="https://www.healthworksai.com/2021-aep-findings-report/" title="HealthworksAI" target="_blank">here</a> or under the HealthWorksAI Insights section of the lobby. 
                                        </p>

                                      <a href="https://whitepaper_healthworksai.analytics-hub.com" title="HealthworksAI" target="_blank" class="btn btn-sm btn-light">Learn more</a>--%>
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <p class="carousel-item-caption-desc-img">
                                                    What do you think about the various dashboards?
                                                    <br />

                                                    Let us know by clicking on <span class="textspan">Like/Unlike</span> button which is now available besides each dashboard in the platform.
                                                </p>
                                            </div>
                                            <div class="col-sm-4" style="text-align:center;">
                                                <a href="dist/Images/Banner2large.jpg" target="_blank">
                                                    <img src="dist/Images/banner2.jpg" /></a>
                                                <span class="image"><i>Click on image to enlarge</i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <ol class="carousel-indicators">
                                        <li data-target="#homeCarousel" data-slide-to="0"></li>
                                        <li data-target="#homeCarousel" data-slide-to="1" class="active"></li>
                                        <li data-target="#homeCarousel" data-slide-to="2"></li>
                                    </ol>
                                </div>
                            </div>
                            <div class="carousel-item">
                                <div class="carousel-item-content">
                                    <div class="carousel-item-caption">
                                        <%-- <h2 class="carousel-item-caption-header">Supplemental, Enhanced, and SSBCI Benefit Trends</h2>
                                        <p class="carousel-item-caption-desc">
                                            Explore year-over-year changes in Medicare Advantage benefit design in our newest interactive whitepaper.
                                            <br />
                                            <br />
                                            Available under the HealthWorksAI Insights section of the lobby.
                                        </p>
                                       <a href="#" target="_blank" class="btn btn-sm btn-light" type="button"
                                            data-toggle="modal" data-target="#fpModaldownload">Learn more</a>--%>

                                        <div class="row">
                                            <div class="col-sm-8">
                                                <p class="carousel-item-caption-desc-img">
                                                    <span class="textspan">Product<i>Intel</i>&trade;</span> lets you design competitive MA Plans and simulate enrollment predictions for different plan designs.                                                   
                                                   
                                                </p>
                                                <a href="RequestDemo.aspx" target="_blank" class="btn btn-sm btn-light">Book a Demo</a>
                                            </div>
                                            <div class="col-sm-4" style="text-align:center;">
                                                <a href="dist/Images/banner3.png" target="_blank">
                                                    <img src="dist/Images/banner3.jpg" /></a> <span class="image"><i>Click on image to enlarge</i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <ol class="carousel-indicators">
                                        <li data-target="#homeCarousel" data-slide-to="0"></li>
                                        <li data-target="#homeCarousel" data-slide-to="1"></li>
                                        <li data-target="#homeCarousel" data-slide-to="2" class="active"></li>
                                    </ol>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="hw-login-slider">
                        <div class="hw-login-client-logo">
                            <img src="dist/Images/default.png" alt="healthworks logo" />
                        </div>
                        <div class="hw-login-container">
                            <div class="hw-login-header">
                                <h2>Welcome!</h2>
                            </div>
                            <div class="hw-login-body">
                                <div class="form-group email">
                                    <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" type="email" placeholder="Email Address" ValidationGroup="login" required />
                                    <!-- <label for="hwLoginEmail">Email Address</label> -->
                                </div>
                                <div class="form-group password">
                                    <asp:TextBox runat="server" ID="txtPassWord" CssClass="form-control" TextMode="Password" placeholder="Password" ValidationGroup="login" required />
                                    <!-- <label for="hwLoginPassword">Password</label> -->
                                </div>
                                <div class="form-group d-flex align-items-center justify-content-between">
                                    <div class="form-check">
                                        <asp:CheckBox Text="Remember Me" runat="server" ID="chkRemember" CssClass="form-check-label" />
                                    </div>
                                    <div class="hw-login-fp">
                                        <a class="hw-login-fp-link" data-backdrop="static" data-keyboard="false" href="#" type="button" data-toggle="modal" onclick="openModal();" data-target="#fpModal">Forgot
                      Password ?</a>
                                    </div>
                                </div>
                                <div class="hw-btn-wrapper">
                                    <asp:Button Text="Sign in" runat="server" CssClass="btn hw-login-btn-primary" ID="btnLogin" OnClick="btnLogin_Click"
                                        ClientIDMode="Static" ValidationGroup="login" />
                                    <div class="text-danger text-center">
                                        <asp:Label Text="Your account or password is incorrect." runat="server" Visible="false" ID="lblErrorMessage" />
                                        <asp:Label Text="Your last session was closed abnormally. Please click on reset button to reset your session." runat="server" Visible="false" ID="LblLogoutMsg" />
                                        <asp:HiddenField ID="Hdn_username" runat="server" />
                                    </div>
                                    <div class="text-danger text-center">
                                        <asp:Button Text="Reset" runat="server" OnClientClick="openModal();" CssClass="btn hw-login-btn-primary" ID="BtnLogOut" OnClick="btnLogout_Click"
                                            ClientIDMode="Static" Visible="false" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="hw-social-links">
                            <a class="hw-social-link" href="https://www.linkedin.com/company/healthworksai/" target="_blank"><i class="fab fa-linkedin-in"></i></a>
                            <a class="hw-social-link" href="https://www.healthworksai.com/" target="_blank"><i class="fas fa-cubes"></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade hw-fp-modal" id="fpModal" tabindex="-1" role="dialog" aria-labelledby="fpModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="fpModalLabel">Recover your Account</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">x</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="hwLoginEmail">Email Address</label>
                            <%--<input name="txtEmail" type="text" id="txtEmail" class="form-control">--%>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" type="email" />
                        </div>
                        <div>
                            <asp:Button Text="Reset Password" runat="server" class="btn btn-primary" ID="btnReset"
                                OnClick="btnReset_Click" OnClientClick="return ValidateForgotEmail();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal for pdf download -->
        <div class="modal fade hw-fp-modal" id="fpModaldownload" tabindex="-1" role="dialog" aria-labelledby="fpModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="fpModalLabel1">Download</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">x</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="hwLoginEmail">Email Address</label>
                            <asp:TextBox runat="server" ID="txtEmailDownload" pattern="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="form-control" type="email" />
                        </div>
                        <div>
                            <asp:Button Text="Download" runat="server" class="btn btn-primary" ID="BtnDownload" OnClientClick="return ValidateRegForm();" OnClick="BtnDownload_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <script lang="javascript" type="text/javascript">
            function ValidateRegForm() {
                var email = document.getElementById("<%=txtEmailDownload.ClientID%>").value;
                if (email == "") {
                    alert("A professional email address is required. Please enter a new email or contact info@healthworksai.com for support");
                    return false;
                }
                var blocked = ["gmail", "hotmail", "yahoo", "rediff", "outlook", "live", "test", "abc", "info@teganalytics.com", "info@healthworksai.com"];
                for (var i = 0; i < blocked.length; i++) {
                    if (email.indexOf(blocked[i]) != -1) {
                        alert("A professional email address is required. Please enter a new email or contact info@healthworksai.com for support");
                        return false;
                    }
                }
            }
            function ValidateForgotEmail() {
                var email = document.getElementById("<%=txtEmail.ClientID%>").value;
                if (email == "") {
                    alert("Please enter an professional email address.");
                    return false;
                }
            }

            function sucessmsg() {
                alert('A temporary password has been sent to the email address provided.');
            }
            function errormsg() {
                alert('The email address provided has not been registered. Please contact support@healthworksai.com for assistance.');
                <%--openModal();
                var email = document.getElementById("<%=txtEmail.ClientID%>");
                email.value = '';
                $('#fpModal').modal('show');--%>
            }
        </script>
        <script type="text/javascript">
            var email = document.getElementById("<%=txtUserID.ClientID%>");
            email.value = '';

            var pass = document.getElementById("<%=txtPassWord.ClientID%>");
            pass.value = '';
            // Get the modal
            var modal = document.getElementById('myModal');
            // Get the button that opens the modal
            var btn = document.getElementsByClassName("jsShare");
            // Get the <span> element that closes the modal
            var span = document.getElementsByClassName("close")[0];
            // Get the <span> element that closes the modal
            var cancel = document.getElementsByClassName("cancelBtn")[0];
            // When the user clicks the button, open the modal
            function openModal() {
                var email = document.getElementById("<%=txtUserID.ClientID%>");
                email.value = 'support@healhworksai.com';

                var pass = document.getElementById("<%=txtPassWord.ClientID%>");
                pass.value = 'i@123';

                var pass1 = document.getElementById("<%=lblErrorMessage.ClientID%>");
                pass1.innerHTML = '';
                //  modal.style.display = "block";
            }
            // When the user clicks on <span> (x), close the modal
            span.onclick = function () {
                var email = document.getElementById("<%=txtUserID.ClientID%>");
                email.value = '';

                var pass = document.getElementById("<%=txtPassWord.ClientID%>");
                pass.value = '';
                //  modal.style.display = "none";
            }
        </script>
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
