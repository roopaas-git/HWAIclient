
var TEGTour = new Tour({
    smartPlacement: true,
    steps: [
        {
            element: "#abc",
            title: "IIW Tour",
            content: "Market Intelligence",
            placement: "right",
            backdrop: true,
        },
        {
            element: "#li_ProductIntelligence",
            title: "IIW Tour",
            content: "Product Intelligence",
            placement: "right",
            backdrop: true,
        },
        {
            element: "#Simulator",
            title: "IIW Tour",
            content: "Simulator",
            placement: "right",
            backdrop: true,
        },
       

        {
            element: ".Alert",
            title: "IIW Tour",
            content: "Get all the latest news and updates here!",
            placement: "auto",
            backdrop: true,
        },
        {
            element: ".frequent",
            title: "IIW Tour",
            content: "Get your Favorite links below.",
            placement: "auto",
            backdrop: true,
        },
        {
            element: ".news",
            title: "IIW Tour",
            content: "International Syndicated News",
            placement: "auto",
            backdrop: true,
        },
        {
            element: ".about",
            title: "IIW Tour",
            content: "Description About IIW",
            placement: "auto",
            backdrop: true,
        },
        {
            element: ".recent",
            title: "IIW Tour",
            content: "Your recently used links",
            placement: "right",
            backdrop: true,
        },
        {
            element: ".coming",
            title: "IIW Tour",
            content: "Latest updated will display in this section.",
            placement: "right",
            backdrop: true,
        },
        {
            element: ".ddlRegion",
            title: "IIW Tour",
            content: "Please select Region.",
            placement: "auto",
            backdrop: true,
        },
        {
            element: ".ddlCat",
            title: "IIW Tour",
            content: "Please select Category.",
            placement: "auto",
            backdrop: true,
        },
        {
            element: ".ddlCount",
            title: "IIW Tour",
            content: "Please select Country.",
            placement: "auto",
            backdrop: true,
        },
        {
            element: ".ddlBrand",
            title: "IIW Tour",
            content: "Please select Brand.",
            placement: "auto",
            backdrop: true,
        },
        {
            element: "#ddlYear",
            title: "IIW Tour",
            content: "Please select Year.",
            placement: "auto",
            backdrop: true,
        },
        {
            element: "#ddlYearIIW",
            title: "IIW Tour",
            content: "Please select Year.",
            placement: "left",
            backdrop: true,
        }, {
            element: "#ddlYearIBP",
            title: "IIW Tour",
            content: "Please select Year.",
            placement: "left",
            backdrop: true,
        },
        {
            element: "#ddlType",
            title: "IIW Tour",
            content: "Please select type(Monthly/Weekly).",
            placement: "left",
            backdrop: true,
        },
        {
            element: "#ddlMonth",
            title: "IIW Tour",
            content: "Please select month.",
            placement: "left",
            backdrop: true,
        },
        {
            element: ".data_slider_item:first",
            title: "IIW Tour",
            content: "Latest report with download,share and comment options.Please click the icons in the top right of the box.",
            placement: "right",
            backdrop: true,
        },
        {
            element: ".FileComment",
            title: "IIW Tour",
            content: "Comments will get displayed based on user selected report on the top boxes.",
            placement: "top",
            backdrop: true,
        },
        {
            element: ".related",
            title: "IIW Tour",
            content: "Related Items will display based on users selection on the top boxes.",
            placement: "top",
            backdrop: true,
        },
        {
            element: ".comments",
            title: "IIW Tour",
            content: "Comments will get displayed based on user selected report on the top boxes.",
            placement: "top",
            backdrop: true,
        },
        {
            element: ".switchContainer",
            title: "IIW Tour",
            content: "On/Off switch to subscribe the report.",
            placement: "top",
            backdrop: true,
        },
        {
            element: "#btnSubscribe",
            title: "IIW Tour",
            content: "Click to subscribe the reports which is having ON.",
            placement: "top",
            backdrop: true,
        },
        {
            element: ".userCreation",
            title: "IIW Tour",
            content: "Please provide details to create an account.",
            placement: "top",
            backdrop: true,
        },
        {
            element: ".deleteUser:first",
            title: "IIW Tour",
            content: "Delete user account from here.",
            placement: "left",
            backdrop: true,
        },
        {
            element: ".startDate",
            title: "IIW Tour",
            content: "Select start date.",
            placement: "bottom",
            backdrop: true
        },
        {
            element: ".endDate",
            title: "IIW Tour",
            content: "Select start enddate.",
            placement: "bottom",
            backdrop: true
        },
        {
            element: ".ViewReport",
            title: "IIW Tour",
            content: "Click to view report based on dates.",
            placement: "bottom",
            backdrop: true
        },
        {
            element: ".DashboardClick",
            title: "IIW Tour",
            content: "Click to view dashboard report.",
            placement: "bottom",
            backdrop: true
        },
        {
            element: ".GridReport",
            title: "IIW Tour",
            content: "Report Details.",
            placement: "top",
            backdrop: true
        },
        {
            element: ".TopUsrPage",
            title: "IIW Tour",
            content: "Displaying Top 3 username and page name.",
            placement: "top",
            backdrop: true
        },
        {
            element: ".lbImagBtnq",
            title: "IIW Tour",
            content: "Click to import report into excel sheet",
            placement: "bottom",

            backdrop: true
        },
        {
            element: ".ContactUs:first",
            title: "IIW Tour",
            content: "Showing Contact Details of Users",
            placement: "bottom",

            backdrop: true
        },
        {
            element: "#starttour",
            title: "Start/Restart Tour",
            content: "Click this link later if you need to <strong>restart the tour</strong>.",
            placement: "bottom",
            backdrop: false
        },
        {
            element: ".UsefulLinks",
            title: "Start/Restart Tour",
            content: "List of Useful Links",
            placement: "top",
            backdrop: true
        }

    ],
    template: "<div class='popover tour'> " +
        "<div class='arrow'></div> " +
        "<h3 class='popover-title'></h3>" +
        "<div class='popover-content'></div>" +
        "<div class='popover-navigation'>" +
        "<button class='btn btn-default btn-sm' data-role='prev'>« Prev</button> &nbsp;&nbsp;" +
        "<button class='btn btn-primary btn-sm ' data-role='next'>Next »</button> &nbsp;&nbsp;" +
        "<button class='btn btn-default btn-sm' data-role='end'>End tour</button>" +
        "</div>" +
        "</div>",
    onEnd: function (tour) {
        $('#starttour').prop('disabled', false);
    }
});

TEGTour.init();

$('#starttour').click(function () {
    alert("Start");
});

