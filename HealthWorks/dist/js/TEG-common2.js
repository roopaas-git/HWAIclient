$(document).ready(function() {

    /************************
    /*	LAYOUT
    /************************/

    // set minimum height for content wrapper
    $(window).bind("load resize scroll", function() {
        calculateContentMinHeight();
    });

    function calculateContentMinHeight() {
        $('#main-content-wrapper').css('min-height', $(window).height() - 64 + 'px');
    }


    /************************
    /*	MAIN NAVIGATION
    /************************/

    //	$('.main-menu .js-sub-menu-toggle').on('click', function(e){

    //		e.preventDefault();

    //		$li = $(this).parent('li');

    //		if( !$li.hasClass('active')){
    //			$li.find(' > a .toggle-icon').removeClass('fa-angle-left').addClass('fa-angle-down');
    //			$li.addClass('active');
    //			$li.find('ul.sub-menu')
    //				.slideDown(500);

    //            $li.siblings().find(' > a .toggle-icon').removeClass('fa-angle-down').addClass('fa-angle-left');
    //            $li.siblings().removeClass('active');
    //            $li.siblings().find('ul.sub-menu').slideUp(500);
    //		}
    //		else {
    //			$li.find(' > a .toggle-icon').removeClass('fa-angle-down').addClass('fa-angle-left');
    //			$li.removeClass('active');
    //			$li.find('ul.sub-menu')
    //				.slideUp(300);
    //		}

    //	});

    $('.main-menu .js-sub-menu-toggle').on('click', function(e) {
        e.preventDefault();
        $li = $(this).parent('li');
        if (!$li.hasClass('active')) {
            if ($li.find(' > a .toggle-icon').hasClass('fa-angle-right')) {
                $li.find(' > a .toggle-icon').removeClass('fa-angle-right').addClass('fa-angle-down');
            }
            else {
                $li.find(' > a .toggle-icon').removeClass('fa-angle-down').addClass('fa-angle-right');
            }
        }
        else {
            $li.find(' > a .toggle-icon').removeClass('fa-angle-down').addClass('fa-angle-right');
        }
        $li.find("ul.sub-menu").slideToggle();
        $li.siblings().find('ul.sub-menu').slideUp(500);
        $li.siblings().find('> a .toggle-icon').removeClass('fa-angle-down').addClass('fa-angle-right')
    });

    $('.sub-menu > li').on('click', function(e) {
        $ul = $(this).parent('ul');
        $li = $ul.closest("li");
        $li.find(' > a .toggle-icon').removeClass('fa-angle-right').addClass('fa-angle-down');
    });

    // checking for minified left sidebar
    checkMinified();

    $('.js-toggle-minified').on('click', function() {
        if (!$('.left-sidebar').hasClass('minified')) {
            $('.left-sidebar').addClass('minified');
            $('.content-wrapper').addClass('expanded');

        } else {
            $('.left-sidebar').removeClass('minified');
            $('.content-wrapper').removeClass('expanded');
        }

        checkMinified();
    });

    function checkMinified() {
        if (!$('.left-sidebar').hasClass('minified')) {
            setTimeout(function() {

                $('.left-sidebar .sub-menu.open')
                    .css('display', 'block')
                    .css('overflow', 'visible')
                    .siblings('.js-sub-menu-toggle').find('.toggle-icon').removeClass('fa-angle-right').addClass('fa-angle-down');
            }, 200);

            $('.main-menu > li > a > .text').animate({
                opacity: 1
            }, 1000);

        } else {
            $('.left-sidebar .sub-menu.open')
                .css('display', 'none')
                .css('overflow', 'hidden');

            $('.main-menu > li > a > .text').animate({
                opacity: 0
            }, 200);
        }
    }

//    $('.toggle-sidebar-collapse').on('click', function() {
//        if ($(window).width() < 992) {
//            // use float sidebar
//            if (!$('.left-sidebar').hasClass('sidebar-float-active')) {
//                $('.left-sidebar').addClass('sidebar-float-active');
//            } else {
//                $('.left-sidebar').removeClass('sidebar-float-active');
//            }
//        } else {
//            // use collapsed sidebar
//            if (!$('.left-sidebar').hasClass('sidebar-hide-left')) {
//                $('.left-sidebar').addClass('sidebar-hide-left');
//                $('.content-wrapper').addClass('expanded-full');
//            } else {
//                $('.left-sidebar').removeClass('sidebar-hide-left');
//                $('.content-wrapper').removeClass('expanded-full');
//            }
//        }
//    });

    $('.collapse-menu-icon').on('click', function () {
        if ($(window).width() < 992) {
            // use float sidebar
            if (!$('.left-sidebar').hasClass('sidebar-float-active')) {
                $('.left-sidebar').addClass('sidebar-float-active');
            } else {
                $('.left-sidebar').removeClass('sidebar-float-active');
            }
        } else {
            // use collapsed sidebar
            if (!$('.left-sidebar').hasClass('sidebar-hide-left')) {
                $('.left-sidebar').addClass('sidebar-hide-left');
                $('.content-wrapper').addClass('expanded-full');
                $('#btnToggle').find('i').removeClass("fa-chevron-right"); 
                $('#btnToggle').find('i').addClass("fa-chevron-left");
                
            } else {
                $('.left-sidebar').removeClass('sidebar-hide-left');
                $('.content-wrapper').removeClass('expanded-full');
                $('#btnToggle').find('i').removeClass("fa-chevron-left"); 
                $('#btnToggle').find('i').addClass("fa-chevron-right");
            }
            //$('i').toggleClass("fa-chevron-right"); 
           
            
        }
    });

    $(window).bind("load resize", determineSidebar);

    function determineSidebar() {

        if ($(window).width() < 992) {
            $('body').addClass('sidebar-float');

        } else {
            $('body').removeClass('sidebar-float');
        }
    }

    // main responsive nav toggle
    $('.main-nav-toggle').clickToggle(
        function() {
            $('.left-sidebar').slideDown(300)
        },
        function() {
            $('.left-sidebar').slideUp(300);
        }
    );

    //$('.left-sidebar').addClass('sidebar-hide-left');
    //$('.content-wrapper').addClass('expanded-full');

    // slimscroll left navigation
   

    //*******************************************
    /*	LIVE SEARCH
    /********************************************/




    /************************
    /*	BOOTSTRAP POPOVER
    /************************/

    $('.btn-help').popover({
        container: 'body',
        placement: 'top',
        html: true,
        trigger: 'hover',
        title: '<i class="fa fa-book"></i> Quick Help',
        content: "Help summary goes here. Options can be passed via data attributes <code>data-</code> or JavaScript. You can change the popover trigger to 'click' instead of 'hover'."
    });

    $('.demo-popover1 #popover-title').popover({
        html: true,
        title: '<i class="fa fa-cogs"></i> Popover Title',
        content: 'This popover has title and support HTML content. Quickly implement process-centric networks rather than compelling potentialities. Objectively reinvent competitive technologies after high standards in process improvements. Phosfluorescently cultivate 24/365.'
    });

    $('.demo-popover1 #popover-hover').popover({
        html: true,
        title: '<i class="fa fa-cogs"></i> Popover Title',
        trigger: 'hover',
        content: 'Activate the popover on hover. Objectively enable optimal opportunities without market positioning expertise. Assertively optimize multidisciplinary benefits rather than holistic experiences. Credibly underwhelm real-time paradigms with.'
    });

    $('.demo-popover2 .btn').popover();


    /*****************************
    /*	WIDGET WITH AJAX ENABLE
    /*****************************/

    $('.widget-header-toolbar .btn-ajax').click(function(e) {
        e.preventDefault();
        $theButton = $(this);

        $.ajax({
            url: 'php/widget-ajax.php',
            type: 'POST',
            dataType: 'json',
            cache: false,
            beforeSend: function() {
                $theButton.prop('disabled', true);
                $theButton.find('i').removeClass().addClass('fa fa-spinner fa-spin');
                $theButton.find('span').text('Loading...');
            },
            success: function(data, textStatus, XMLHttpRequest) {

                setTimeout(function() {
                    getResponseAction($theButton, data['msg'])
                }, 1000);
                /* setTimeout is used for demo purpose only */

            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                console.log("AJAX ERROR: \n" + errorThrown);
            }
        });
    });

    function getResponseAction(theButton, msg) {

        $('.widget-ajax .alert').removeClass('alert-info').addClass('alert-success')
            .find('span').text(msg);

        $('.widget-ajax .alert').find('i').removeClass().addClass('fa fa-check-circle');

        theButton.prop('disabled', false);
        theButton.find('i').removeClass().addClass('fa fa-floppy-o');
        theButton.find('span').text('Update');
    }


    //*******************************************
    /*	WIDGET QUICK NOTE
    /********************************************/

    if ($('.quick-note-create').length > 0) {
        $('.quick-note-create textarea, .quick-note-create input').focusin(function() {
            $(this).attr('rows', 7);
            $('.quick-note-create').find('.widget-footer').show();
        });

        $('.quick-note-create').focusout(function() {
            $(this).find('textarea').attr('rows', 1);
            $(this).find('.widget-footer').hide();
        });
    }

    if ($('.quick-note-saved').length > 0) {
        $('.quick-note-saved').click(function() {
            $('#quick-note-modal').modal();
        });
    }

    if ($('.quick-note-edit').length > 0) {
        $('.quick-note-edit .btn-save').click(function() {
            $('#quick-note-modal').modal('hide');
        });
    }


    //*******************************************
    /*	WIDGET SLIM SCROLL
    /********************************************/

    if ($('.widget-scrolling').length > 0) {
        $('.widget-scrolling .widget-content').slimScroll({
            height: '410px',
            wheelStep: 5,
        });
    }


    //*******************************************
    /*	WIDGET WITH AJAX STATE
    /********************************************/

    if ($('#btn-ajax-state').length > 0) {
        $('#btn-ajax-state').click(function() {
            $statusPlaceholder = $(this).parents('.widget').find('.process-status');
            ajaxCallToDo($statusPlaceholder);
        });
    }


    /**************************************
    /*	MULTISELECT/SINGLESELECT DROPDOWN
    /**************************************/

    if ($('.widget-header .multiselect').length > 0) {

        $('.widget-header .multiselect').multiselect({
            dropRight: true,
            buttonClass: 'btn btn-warning btn-sm'
        });
    }


    //*******************************************
    /*	SWITCH INIT
    /********************************************/

    if ($('.bs-switch').length > 0) {
        $('.bs-switch').bootstrapSwitch();
    }


    /************************
    /*	TOP BAR
    /************************/

    if ($('.top-general-alert').length > 0) {

        if (localStorage.getItem('general-alert') == null) {
            $('.top-general-alert').delay(800).slideDown('medium');
            $('.top-general-alert .close').click(function() {
                $(this).parent().slideUp('fast');
                localStorage.setItem('general-alert', 'closed');
            });
        }
    }

    $btnGlobalvol = $('.btn-global-volume');
    $theIcon = $btnGlobalvol.find('i');

    // check global volume setting for each loaded page
    checkGlobalVolume($theIcon, localStorage.getItem('global-volume'));

    $btnGlobalvol.click(function() {
        var currentVolSetting = localStorage.getItem('global-volume');
        // default volume: 1 (on)
        if (currentVolSetting == null || currentVolSetting == "1") {
            localStorage.setItem('global-volume', 0);
        } else {
            localStorage.setItem('global-volume', 1);
        }

        checkGlobalVolume($theIcon, localStorage.getItem('global-volume'));
    });

    function checkGlobalVolume(iconElement, vSetting) {
        if (vSetting == null || vSetting == "1") {
            iconElement.removeClass('fa-volume-off').addClass('fa-volume-up');
        } else {
            iconElement.removeClass('fa-volume-up').addClass('fa-volume-off');
        }
    }


    //*******************************************
    /*	SELECT2
    /********************************************/

    if ($('.select2').length > 0) {
        $('.select2').select2();
    }

    if ($('.select2-multiple').length > 0) {
        $('.select2-multiple').select2();
    }


    //*******************************************
    /*	DRAG & DROP TO-DO LIST
    /********************************************/

    if ($('.todo-list').length > 0) {
        $('#dragdrop-todo').sortable({
            revert: true,
            placeholder: "ui-state-highlight",
            handle: '.handle',
            update: function() {
                $status = $(this).parents('.widget').find('.process-status');
                ajaxCallToDo($status);
            }
        });

        $('.todo-list input').change(function() {
            if ($(this).prop('checked')) {
                $(this).parents('li').addClass('completed');
            } else {
                $(this).parents('li').removeClass('completed');
            }

            $status = $(this).parents('.widget').find('.process-status');
            ajaxCallToDo($status);
        });

        function ajaxCallToDo($status) {
            $.ajax({
                url: 'php/widget-ajax.php',
                type: 'POST',
                dataType: 'json',
                cache: false,
                beforeSend: function() {
                    $status.find('.loading').fadeIn(300);
                },
                success: function(data, textStatus, XMLHttpRequest) {

                    setTimeout(function() {
                        $status.find('span').hide();
                        $status.find('.saved').fadeIn(300);
                        console.log("AJAX SUCCESS");
                    }, 1000);

                    setTimeout(function() {
                        $status.find('.saved').fadeOut(300);
                    }, 2000);
                    /* all setTimeout is used for demo purpose only */

                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    $status.find('span').hide();
                    $status.find('.failed').addClass('active');
                    console.log("AJAX ERROR: \n" + errorThrown);
                }
            });
        }
    }

    function ajaxCallToDo($status) {
        $.ajax({
            url: 'php/widget-ajax.php',
            type: 'POST',
            dataType: 'json',
            cache: false,
            beforeSend: function() {
                $status.find('.loading').fadeIn(300);
            },
            success: function(data, textStatus, XMLHttpRequest) {

                setTimeout(function() {
                    $status.find('span').hide();
                    $status.find('.saved').fadeIn(300);
                    console.log("AJAX SUCCESS");
                }, 1000);

                setTimeout(function() {
                    $status.find('.saved').fadeOut(300);
                }, 2000);
                /* all setTimeout is used for demo purpose only */

            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                $status.find('span').hide();
                $status.find('.failed').addClass('active');
                console.log("AJAX ERROR: \n" + errorThrown);
            }
        });
    }


    //*******************************************
    /*	TEXTAREA FOR CHAT
    /********************************************/

    // enabling shift enter for new line
    $(".textarea-chat").keydown(function(e) {
        if (e.keyCode == 13 && !e.shiftKey) {
            e.preventDefault();
            console.log('send message');
            $(this).val('');
        }
    });


    //*******************************************
    /*	DATA COMPLETENESS METER
    /********************************************/

    if ($('.completeness-meter').length > 0) {
        var cPbar = $('.completeness-progress');

        if ($('.progress-bar').length > 0) {
            cPbar.progressbar({
                display_text: 'fill',
                update: function(current_percentage) {
                    $('.completeness-percentage').text(current_percentage + '%');

                    if (current_percentage == 100) {
                        $('.complete-info').addClass('text-success').html('<i class="ion ion-checkmark-circled"></i> Hooray, it\'s done!');
                        cPbar.removeClass('progress-bar-info').addClass('progress-bar-success');
                        $('.completeness-meter .editable').editable('disable');
                    }
                }
            });
        }

        $.fn.editable.defaults.mode = 'inline';

        $('#complete-phone-number').on('shown', function(e, editable) {
            editable.input.$input.mask('(999) 999-9999');
        }).on('hidden', function(e, reason) {
            if (reason == 'save') {
                $(this).parent().prepend('Phone: ');
                updateProgressBar(cPbar, 10);
            }
        });
        $('#complete-sex').on('hidden', function(e, reason) {
            if (reason == 'save') {
                $(this).parent().prepend('Sex: ');
                updateProgressBar(cPbar, 10);
            }
        });
        $('#complete-birthdate').on('hidden', function(e, reason) {
            if (reason == 'save') {
                $(this).parent().prepend('Birthdate: ');
                updateProgressBar(cPbar, 10);
            }
        });
        $('#complete-nickname').on('shown', function(e, editable) {
            editable.input.$input.val('');
        }).on('hidden', function(e, reason) {
            if (reason == 'save') {
                $(this).parent().prepend('Nickname: ');
                updateProgressBar(cPbar, 10);
            }
        });

        $('.completeness-meter #complete-phone-number').editable();
        $('#complete-sex').editable({
            source: [
                { value: 1, text: 'Male' },
                { value: 2, text: 'Female' }
            ]
        });
        $('#complete-birthdate').editable();
        $('#complete-nickname').editable();
    }

    function updateProgressBar(pbar, valueAdded) {
        pbar.attr('data-transitiongoal', parseInt(pbar.attr('data-transitiongoal')) + valueAdded).progressbar();
    }

    $('#commentsModal .custom-modal-content').slimScroll({
        height: '100%',
        wheelStep: 5,
    });

});

// toggle function
$.fn.clickToggle = function(f1, f2) {
    return this.each(function() {
        var clicked = false;
        $(this).bind('click', function() {
            if (clicked) {
                clicked = false;
                return f2.apply(this, arguments);
            }

            clicked = true;
            return f1.apply(this, arguments);
        });
    });
}

// toggle function


//var prm = Sys.WebForms.PageRequestManager.getInstance();
//prm.add_endRequest(function () {
//    myState();
//    ToggleDiv();
//});


//function myState()
//{
//    var affectedElement = $('.widget-content');
//    $parentID = $("#hfSate").val();  
//    $upDown = $("#hfUpDown").val();
//    if($upDown == "slideUp")
//    {
//        alert('SlideUp');
//        $($parentID).parents('.widget').find(affectedElement).slideUp(1);
//        $($parentID).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
//    }
//    else
//    {
//        alert('SlideDown');
//        $($parentID).parents('.widget').find(affectedElement).slideDown(300);
//        $($parentID).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
//    }
//}

//function ToggleDiv() {
//var affectedElement = $('.widget-content');
//   $('a.btn-toggle-expand').click(function() {
//        $parentId = "#" + $(this).parent().attr("id");
//        if( $($parentId).parents('.widget').find('.slimScrollDiv').length > 0 ) {
//				affectedElement = $('.slimScrollDiv');
//		}
//        if ($(this).find('i.fa-chevron-up').hasClass("fa-chevron-down")) 
//        {
//            $($parentId).parents('.widget').find(affectedElement).slideDown(300);
//			$($parentId).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
//            $("#hfSate").val($parentId);
//            $("#hfUpDown").val('slideDown');
//        }
//        else
//        {
//            $($parentId).parents('.widget').find(affectedElement).slideUp(300);
//			$($parentId).find('i.fa-chevron-up').toggleClass('fa-chevron-down');
//            $("#hfSate").val($parentId);
//            $("#hfUpDown").val('slideUp');
//        }
//    });
//}