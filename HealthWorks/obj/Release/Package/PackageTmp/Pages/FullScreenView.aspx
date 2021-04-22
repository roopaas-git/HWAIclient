<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullScreenView.aspx.cs"
    Inherits="HealthWorks.Pages.FullScreenView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
     <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <title>HealthWorksAI - Help</title>
    <style>
       
       
        .iFrameClass
        {
            width: 100% !important;
            height: 620px;
        }
        .toolbarButton download hiddenMediumView
        {
            visibility: hidden !important;
        }
        html[dir='ltr'] #toolbarViewerLeft > *, html[dir='ltr'] #toolbarViewerMiddle > *, html[dir='ltr'] #toolbarViewerRight > *, html[dir='ltr'] .findbar > *
        {
            visibility: hidden !important;
        }
        #download
        {
            display: none;
        }
        #outerContainer #mainContainer div.toolbar
        {
            display: none !important; /* hide PDF viewer toolbar */
        }
        #outerContainer #mainContainer #viewerContainer
        {
            top: 0 !important; /* move doc up into empty bar space */
        }
        div.pdf
        {
            position: absolute;
            top: -93px;
            left: 0;
            width: 100%;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            focus();
            var listener = window.addEventListener('blur', function() {
                if (document.activeElement === document.getElementById('iframe')) {
                    alert('clicked');
                }
                window.removeEventListener('blur', listener);
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <div  style="height: 522px; width: 80%; border: 0px; padding: 0px; margin: 0px" runat="server">
            <iframe id="displayPDF"  width="75%" runat="server" style="height: 97vh;" clientidmode="Static">
            </iframe>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        document.onmousedown = disableRightclick;
        var message = "Right Click not Avaibalbe !!!";
        function disableRightclick(evt) {
            if (evt.button == 2) {
                alert(message);
                return false;
            }
        }
    </script>

</body>
</html>
