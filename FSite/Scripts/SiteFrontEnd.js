function scrolltopview(top) {
    if (!top)
        top = 0;
    $('html, body').animate({
        scrollTop: top
    }, 1000);
}

    $(".js-select2-basic").select2();

$('.js-select2-multiple').each(function () {
    var sl = this;
    $(sl).select2MultiCheckboxes({
        // placeholder: "Choose multiple elements",
        //width: "auto",
        templateSelection: function (selected, total) {

            if (selected.length === 0) {
                return $(sl).attr("placeholder");
            }
            return "Chọn " + selected.length + " của " + total;
        }
    })
})

$('body').on('click', '.js-view3d', function () {
    var id = $(this).parents("[data-pid]").data('pid');
    alertify.apiUrlDialog({ url: "/view3d/" + id }).set({ frameless: true });
    // alert(id);
    return false;
    // alertify.alert("aa")
})
$('body').on('click', '.js-set-value', function () {
    var el = this;
    var target = $(el).data('target');
    var value = $(el).data('value');
   // alert(value);
    $(target).val(value).trigger("change");
    return false;
})
$('body').on('click', '.js-filter-remove', function () {
    var el = this;
    var target = $(el).data('target');
    $(target).val('').trigger("change");
    $(target).parents('form').submit();
    $(el).remove();
    
    return false;
})

$('body').on("click", '.js-contact-to-user', function () {
    var _id = $(this).parents('[data-pid]').data('pid');
    $.get("/yeu-cau-ve-bat-dong-san", { id: _id }, function (res) {
     
        alertify.alert("<div class='boxRequestItem'>" + res + "</div>").set(
            {
                'basic': true
              ,  'padding':false
                //'modal': false
            })
            //.set('resizable', true).resizeTo('90%');
        $.validator.unobtrusive.parse("#customercontactbasic");
    })
    return false;
})
//  alertify.alert("chay thu
function _modalBasicRequest() {
    $.get("/Home/BasicRequest", function (trave) {
        alertify.alert(trave).set({ basic: true }); ///
        $.validator.unobtrusive.parse("#customercontactbasic");
    })
}

$('body').on('click', '.js-modal-basicrequest', function () {
    _modalBasicRequest();
    return false;
})
$('body').on("click", '.js-page-modal', function () {

    var _u = $(this).attr('href');
    //   _u = "/map1.html";
    if ($(this).data('type') === "iframe") {
        alertify.apiUrlDialog({ url: _u }).set({
            title: $(this).data('title'), overflow: false, onshow: function () {

                //this.elements.dialog.style.maxWidth = 'none';
                //this.elements.dialog.style.width = '85%';
            }
        });;
    } else {


        $.get(_u, function (respond) {
            // var _c = $("<div/>").addClass('pad-10').append(respond).html();

            alertify.apiGeneralDialog("", "<div class='pad-10'>" + respond + "</div>", '@(isMobile?"100%":"90%")').set({ padding: false, overflow: true });

            //  setTimeout(function () { _nivoSlider('.ajs-content .js-slide-nivoSlider') }, 500);
        })
    }
    return false;
})

$(window).scroll(function () {
    if ($(window).scrollTop() >= 200) {
        $('#back-top').fadeIn();

    } else {
        $('#back-top').fadeOut();
    }

});


$("#back-top").on('click', function () {
    $('body,html').animate({
        scrollTop: 0
    }, 800);
    return false;
});

$("#back-top a").on('click', function () {
    $('body,html').animate({
        scrollTop: 0
    }, 800);
    return false;
});

function removeURLParameter(sourceURL, key) {
    var rtn = sourceURL.split("?")[0],
        param,
        vparam,
        params_arr = [],
        queryString = (sourceURL.indexOf("?") !== -1) ? sourceURL.split("?")[1] : "";
    if (queryString !== "") {
        params_arr = queryString.split("&");
        for (var i = params_arr.length - 1; i >= 0; i -= 1) {
            param = params_arr[i].split("=")[0];
            vparam = params_arr[i].split("=")[1];
            if (param === key || vparam === "") {
                params_arr.splice(i, 1);
            }
        }
        rtn = rtn + "?" + params_arr.join("&");
    }
    return rtn;
}

function pushState(respond, title, targetUrl) {
    //if (typeof (targetUrl) === 'undefined' || targetUrl == null) {
    //    targetUrl = $fullpath;
    //}
  
 //  targetUrl = targetUrl.replace(/&amp;/g, '&');
    //targetUrl = removeURLParameter(targetUrl, "X-Requested-With");
 //   targetUrl = removeURLParameter(targetUrl, "_");
    //  var targetUrl = $(this).attr('href'),
    var targetTitle = $(this).attr('title');
    if (history.pushState) //history.pushState({}, targetTitle, targetUrl);
        window.history.pushState({ url: "" + targetUrl + "" }, targetTitle, targetUrl);


    // setCurrentPage(targetUrl);

    // window.onpopstate = function (e) {
    //  $("#menu-nav a").fadeTo('fast', 1.0);
    //   setCurrentPage(e.state ? e.state.url : null);
    // };
    //
    // console.log("url.replace(/&amp;/g, '&')",url)
    // url= removeURLParameter(url, "Length");
    // console.log("removeURLParameter",url)
    // url = removeURLParameter(url, "X-Requested-With");
    // url = removeURLParameter(url, "_");
    //   if (window.history && window.history.pushState)
    //  {  window.history.pushState(respond, title, url);}
    //if (data.t == "LocationCreate") {
    //    var o = data.d;
    //    for (var prop in o) {
    //        if (o.hasOwnProperty(prop)) {
    //            $('.' + prop).val(o[prop]);
    //        }
    //    }
    //}
    //document.getElementById("content").innerHTML = response.html;
    //document.title = response.pageTitle;
    //window.history.pushState({"html":response.html,"pageTitle":response.pageTitle},"", urlPath);
}

function setValueSelect2(select, data) {
    var sl2 = $(select);
    // create the option and append to sSelect2
    var option = new Option(data.text, data.id, true, true);
    sl2.append(option).trigger('change');

    // manually trigger the `select2:select` event
    sl2.trigger({
        type: 'select2:select',
        params: {
            data: data
        }
    });
}

//function CustomerRequestOnEnd() {
//    if ($('#CustomerRequest .field-validation-error').text() !== "") {
//        alertify.notify("Vui lòng kiểm tra lại thông tin", 'erros', 2, function () { });

//    } else {
//        $("#CustomerRequest")[0].reset();
//        alertify.notify("Gửi thông tin thành công,  Xin chân thành cảm ơn quý khách", 'success', 2, function () { });
//    }
   
//}

//function CustomerRequestOnSuccess(res) {
   
//    CustomerRequestOnEnd();
//}
//function CustomerRequestOnFailure() {
//    CustomerRequestOnEnd();
//}
var navMain=$('#navbar');
if (navMain.length !== 0) {
    //trick menu
    //if (navMain.find('ul:first').hasClass('dropdown-menu')) {
    //    $('#navbar ul:first').removeClass('dropdown-menu').addClass('nav navbar-nav');
    //} else {
    //    $('#navbar ul:first').removeClass('dl-submenu').addClass('dl-menu');
    //}
    //setTimeout(function () { $('#navbar').removeClass('hidden');}, 500);
    //active menu
    var pageid = $('body').data('page');
    if (pageid.indexOf('m_') !== -1) {
        pageid = "#" + pageid;
    } else {
        pageid = "." + pageid;
    }
    var elcurrent = navMain.find(pageid);//li hien tai
    $(elcurrent).addClass('active');
    //gio kiem tra parent
    $(elcurrent).parent().parent().addClass('active');//tam thoi trick thang nay di luc nao cung add 2 cap cha la class active
}


function socialShare(url, winWidth, winHeight) {
    // url=  encodeURIComponent(url);
    var winTop = (screen.height / 2) - (winHeight / 2);
    var winLeft = (screen.width / 2) - (winWidth / 2);
    window.open(url, 'sharer', 'top=' + winTop + ',left=' + winLeft + ',toolbar=0,status=0,width=' + winWidth + ',height=' + winHeight);
    // ex: <a href="javascript:socialShare('http://jsfiddle.net/stichoza/EYxTJ/', 520, 350)">Share</a>
}

$('body').on('click', '.js-social-share', function () {
    var url = $(this).attr('href');
    url = window.location.href;
    var p = $(this).data('provider');
    //http://www.sharelinkgenerator.com/
    switch (p) {
        case "facebook":
            url = "http://www.facebook.com/sharer.php?u=" + url;
            break;
        case "twitter":
            url = "https://twitter.com/intent/tweet?url=" + url;
            break;
        case "google":
            url = "https://plus.google.com/share?url=" + url;
            break;
        case "pinterest":
            url = "http://pinterest.com/pin/create/link/?url=" + url + "&media=" + $(this).data('img') + "&description=" + $(this).data('desc');
            break;
        case "envelope":
            url = "mailto:?bcc=" + $(this).data('bcc') + "&subject=" + $(this).data('title') + "&body=" + url;
            break;
        default:

    }
    socialShare(url, 520, 350);
    return false;
})


    //(function FixedAction() {

    function fixtedInfo() {
       
            if ($(window).width() > 800) {
                var w = $(window);
                var padding = 0;

                var elfix = $('.js-scroll-fix');
                var eltop = $(elfix).data('el-top');
                var elcontent = $(elfix).data('el-content');
                var elbottom = $(elfix).data('el-bottom');
                if ($('.js-cut-bottom').length !== 0) {
                    elbottom = $('.js-cut-bottom:first');
                }
                var eltopmargin = $(elfix).data('el-top-margin');//get height fixed scrool bar 
                var eltopmarginH = $(eltopmargin).length !== 0 ? $(eltopmargin).height() : 0;
                // console.log(eltopmarginH)
                if (w.scrollTop() > $(eltop).offset().top) {
                    $(elfix).css("position", "fixed");
                    $(elfix).css("width", $(eltop).width() + padding + "px");
                    $(elfix).css("top", eltopmarginH);
                } else {
                    $(elfix).css("position", "relative");
                    $(elfix).css("top", 0);
                }
                var marginbottomtop = 30;
                if (w.scrollTop() > ($(elbottom).offset().top - $(elfix).height()) - marginbottomtop) {
                    $(elfix).css("position", "absolute");
                    $(elfix).css("top", $(elcontent).height() - $(elfix).height() - marginbottomtop + "px");
                }
            }
        
    }

   // $(window).on("load scroll", fixtedInfo);
//if ($('.js-scroll-fix').length !== 0) {
//    fixtedInfo();
//}
    //})();
    //class="js-scroll-fix" data-el-top="#" data-el-content="#" data-el-bottom=""


function showImage(input, target,callback) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(target).attr('src', e.target.result);
            if (callback) {
                callback(e.target.result);
            }
        };
        reader.readAsDataURL(input.files[0]);
       
    }
}

//var src = document.getElementById("src");
//var target = document.getElementById("target");
//showImage(src, target);
function displayMoreItems(elbox, elitem, limit) {
    //  set a limit to # of li's to show
    //  cache the item's
    $items = $(elitem);
    if ($items.length>limit) {
        $items.slice(limit, $items.length).hide();
        $('<div>', {
            html: '<a href="javascript:void(0)" class="view-more-cyan" ><span>Xem các bài khác</span> <i class="fa fa-chevron-down"></i></a>'
        })
        .appendTo(elbox)
        .bind('click', function () {
            $(this).remove();
            $items.show();
        });
    }
   
}


//master

//jQuery(document).ready(function () {
//    jQuery('.contact-group').find('.icon').on('click', function () {
//        jQuery(this).toggleClass('active');
//        jQuery(this).next('.button-action-group').toggleClass('active');
//        if (jQuery(this).hasClass('active')) {
//            Cookies.set('hotline', 'active', { expires: 1 })//1 day
//        } else { Cookies.remove('hotline') }
//    })
//    if (Cookies.get('hotline')) { $('.contact-group .icon,.contact-group .button-action-group').addClass('active'); } else { $('.contact-group .icon,.contact-group .button-action-group').removeClass('active'); }

//});


function goToTop() {
    var n = $(".bus-top");
    n.on("click", function () {
        return $("html,body").animate({ scrollTop: 0 }, 800),
            $(this).addClass("bus-run"),
            setTimeout(function () { n.removeClass("bus-run") }, 1e3), !1
    });

    $(window).on("scroll", function () {
        $(window).scrollTop() >= 200 ? (n.addClass("show"),
            n.addClass("bus-down")) : (n.removeClass("show"),
                setTimeout(function () { n.removeClass("bus-down") }, 300))
    })
}
goToTop();

//if (!Cookies.get('adver')) {
//    setTimeout(function () {
//        alertify.alert("<img src='/Uploads/images/Blogs/Blogs-c05fddc-banner-xe-nha-tet-2020.jpg' />").set({
//            transition: 'zoom',
//            'modal': true,
//            'frameless': true,
//            padding: false, onclose: function () {
//                var date = new Date(); date.setTime(date.getTime() + (60 * 60 * 1000));

//                Cookies.set('adver', 'active', { expires: date })// // expires after 60min second
//            }
//        });},1000)
//} 

_countdown('body');
function _countdown(el) {

    $(el).find('.defaultCountdown').each(function () {
        var distance = (new Date($(this).data('time-end')));

        //console.log(distance)

        $(this).countdown({ until: distance });
    })
}