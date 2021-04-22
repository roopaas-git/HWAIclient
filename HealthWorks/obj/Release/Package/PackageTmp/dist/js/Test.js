$(document).ready(function () {
    $parentID = $("#hfSate").val();
    $Status = $("#hfFocus").val();
    if ($parentID == 0) {
        HideAndShow();
    }
    else {
        myState();
        HideAndShow();
    }
    if ($Status == 0) {
        Focus();
    }
    else {
        FocusStatus();
        Focus();
    }
    RemoveWidget();
});

var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_endRequest(function () {
    myState();
    HideAndShow();
    FocusStatus();
    Focus();
    RemoveWidget();
});

function Focus() {
    $('a.btn-focus').click(function () {
        $theWidget = $(this).parents('.widget');
        $focusID = "#" + $(this).attr("id");
        if ($(this).find('i.fa-eye').hasClass("fa-eye-slash")) {
            var numItems = $('.removeFocus').length;
            $(this).find('i.fa-eye').toggleClass('fa-eye-slash');
            $theWidget.find('.btn-remove').removeClass('link-disabled');
            $('body').removeClass('focus-mode');
            $('body').find('.removeFocus').fadeOut(function () {
                $(this).remove();
                $theWidget.removeClass('widget-focus-enabled');
            });
            $("#hfFocus").val($focusID);
            $("#hfFocusStatus").val('fadeOut');
        }
        else {
            $(this).find('i.fa-eye').toggleClass('fa-eye-slash');
            $(this).parents('.widget').find('.btn-remove').addClass('link-disabled');
            $(this).parents('.widget').addClass('widget-focus-enabled');
            $('body').addClass('focus-mode');
            $('<div id="focus-overlay" class="removeFocus"></div>').hide().appendTo('body').fadeIn(300);
            $("#hfFocus").val($focusID);
            $("#hfFocusStatus").val('fadeIn');
        }
    });
}

function HideAndShow() {
    var affectedElement = $('.widget-content');

    $('a.btn-toggle-expand').click(function () {
        $parentId = "#" + $(this).parent().attr("id");
        if ($($parentId).parents('.widget').find('.slimScrollDiv').length > 0) {
            affectedElement = $('.slimScrollDiv');
        }
        if ($(this).find('i.fa-chevron-up').hasClass("fa-chevron-down")) {
            $($parentId).parents('.widget').find(affectedElement).slideDown(300);
            $($parentId).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
            $("#hfSate").val($parentId);
            $("#hfUpDown").val('slideDown');
        }
        else {
            $($parentId).parents('.widget').find(affectedElement).slideUp(300);
            $($parentId).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
            $("#hfSate").val($parentId);
            $("#hfUpDown").val('slideUp');
        }
    });
}

function myState() {
    var affectedElement = $('.widget-content');
    $parentID = $("#hfSate").val();
    $upDown = $("#hfUpDown").val();
    if ($upDown == "slideUp") {
        $($parentID).parents('.widget').find(affectedElement).slideUp(1);
        $($parentID).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
    }
    else {
        $($parentID).parents('.widget').find(affectedElement).slideDown(300);
        $($parentID).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
    }
}

function FocusStatus() {
    $parentID = $("#hfFocus").val();
    $statusID = $("#hfFocusStatus").val();
    $theWidgetActivated = $($parentID).parents('.widget');
    if ($statusID == 'fadeIn') {
        $($parentID).parents('.widget').find('i.fa-eye').addClass('fa-eye-slash');
        $($parentID).parents('.widget').find('.btn-remove').addClass('link-disabled');

        $('body').addClass('focus-mode');
        $('<div id="focus-overlay" class="removeFocus"></div>').hide().appendTo('body').fadeIn(300);
        $($parentID).parents('.widget').addClass('widget-focus-enabled');
    }
}

function RemoveWidget() {
    $('.widget .btn-remove').click(function (e) {

        e.preventDefault();
        $(this).parents('.widget').fadeOut(300, function () {
            $(this).remove();
        });
    });
}


function clickToggle(f1, f2) {
    return this.each(function () {
        var clicked = false;
        $(this).bind('click', function () {
            if (clicked) {
                clicked = false;
                return f2.apply(this, arguments);
            }

            clicked = true;
            return f1.apply(this, arguments);
        });
    });
}
