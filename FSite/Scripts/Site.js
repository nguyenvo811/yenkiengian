jQuery.validator.methods["date"] = function (value, element) { return true; }
$.fn.select2.defaults.set("theme", "bootstrap");
$.fn.select2.defaults.set("language", "vi");
$(".js-select2-basic").select2();
$(".js-select2-basic-tag").select2({
    tags: true
});

function setValue(el, value) {
    $(el).val(value);
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
//use for bedroom, bathroom
function setValueSelect2CheckExits(select, data) {
    var sl2 = $(select);
    if ($(sl2).find("option[value='" + data.id + "']").length) {
        $(sl2).val(data.id).trigger('change');
    } else {
        // Create a DOM Option and pre-select by default
        var option = new Option(data.text, data.id, true, true);
        // Append it to the select
        sl2.append(option).trigger('change');
    }
}

function removeCharacter(alias) {
    var str = alias;
    str = str.toLowerCase();

    str = str.replace(/\r?\n|\r/, "");//new line
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
    str = str.replace(/ + /g," ");//mutil space to one space
    str = str.replace(/\W/g, '-');
    str = str.replace(/&nbsp;/g, '');//&nbsp;
    str = str.trim();
    return str;
}
var naviActive = function (idEl) {
    var $e = $('.sidebar-menu li#m' + idEl);
   // console.log("e", idEl)
    if ($e) {
        //if ($e.hasClass('treeview'))
        //    $e.addClass('menu-open');
        $e.addClass('active');

        $e.parent().parent().addClass('menu-open active');

    }
}


//setDate('#productCommissionStartDate', data.commissionStartDate);
//var setDate = function (id, d) {
//    if (d !== undefined && d !== null) {
//        $(id).val(getJsonDate(d));
//    }
//};

var getJsonDate = function (d) {
    if (d !== undefined && d !== null) {
        var date = new Date(parseInt(d.replace("/Date(", "").replace(")/", ""), 10));
        var day = ('0' + date.getDate()).slice(-2);
        var month = ('0' + (date.getMonth() + 1)).slice(-2);
        //var parsedDate = date.getFullYear() + "-" + (month) + "-" + (day);
        var parsedDate = (day) + "/" + (month) + "/" + date.getFullYear();
        return parsedDate;
    }
    return "";
};
var getJsonTime = function (t) {
    if (t !== undefined && t !== null) {
        var hours = t.Hours;
        var minutes = t.Minutes;
        var seconds = t.Seconds;
        return (hours + ':' + minutes + ':' + seconds);
    }
    return "";
};
String.format = function() {
    // The string containing the format items (e.g. "{0}")
    // will and always has to be the first argument.
    var theString = arguments[0];
    // start with the second argument (i = 1)
    for (var i = 1; i < arguments.length; i++) {
        // "gm" = RegEx options for Global search (more than one instance)
        // and for Multiline search
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }
    
    return theString;
    //ex:   var link = String.format('<a href="{0}/{1}/{2}" title="{3}">{3}</a>',url, year, titleEncoded, title);
}


//var datatableRowDetail = ["<div class='btn-group'>",
//    "<button type = 'button' class= 'btn btn-default dropdown-toggle' data - toggle='dropdown' >",
//"<i class='fa fa-cog' aria-hidden='true'></i> <span class='caret'></span>",
//    "</button >",
//   " <ul class='row-control-setting dropdown-menu' role='menu' data-id='0'>",
//        "<li><a class='item-edit' title='Sửa' href='#'><i class='fa fa-edit' aria-hidden='true'></i>Sửa</a></li>",
//        "<li class='divider'></li>",
//        "<li><a class='item-delete' title='Xóa' href='#'><i class='fa fa-trash' aria-hidden='true'></i>Xóa</a></li>",
//    "</ul>",
//    "</div>"].join('\n');

function data_Init(el) {
    if ($(el)) {
  //el = '.cdate'
    $(el).daterangepicker({
        "timePicker": true,
        timePickerIncrement: 15,
        // "timePicker24Hour": true,
        singleDatePicker: true,
        calender_style: "picker_4",
        "locale": {
            format: 'DD/MM/YYYY h:mm A',
            "separator": " - ",
            "applyLabel": "Chọn",
            "cancelLabel": "Hủy",
            "fromLabel": "Từ",
            "toLabel": "Đến",
            "customRangeLabel": "Custom",
            "weekLabel": "Tuần",
            "daysOfWeek": [
               "CN", "T2", "T3", "T4", "T5", "T6", "T7"
            ],
            "monthNames": [
              "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
            "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"
            ],
            "firstDay": 1
        }
    }, function (start, end, label) {
        //  $(this).val(start.format('DD/MM/YYYY'));
        //console.log(start.toISOString(), end.toISOString(), label);
    });
    }
  
}

data_Init('.cdate');
function _ajaxLoadControl() {
    //$.validator.unobtrusive.parse("#frm");
    //editor
    if (document.getElementsByClassName("Editor")) {
        configTinymce(1, '#Detail');
    }
    //check box
    $('[type="checkbox"]').each(function () {
        var lb = $(this).parent().find('label');
        $(lb).insertAfter($(this));
    })
    data_Init('.cdate');

    //map
  //  google.maps.event.addDomListener(window, 'load', initialize);

}


$('body').on('click', '.js-select-path', function () {
    var url = $(this).attr('href');
    alertify.alert().set({
        'startMaximized': true, 'message': "<iframe src='" + url + "' width='100%' height='100%'></iframe>"
        , basic: true,
        padding: false
    }).show();
    return false;
})
$('body').on('click', '.js-select-cropper', function () {
    var path = $(this).parent().find('.real-path').val();
    var url = $(this).attr('href')+"&url="+path;
    alertify.alert().set({
        'startMaximized': true, 'message': "<iframe src='" + url + "' width='100%' height='100%'></iframe>"
        , basic: true,
        padding: false
    }).show();
    return false;
})


//-- js for Index 
$('body').on("click", '.item-delete', function () {
  
    var id = $(this).data('id');
    var _url = $(this).data('href');
    var row = $(this).parents('tr');
    if (confirm("Bạn muốn xóa?")) {
        $.post(_url, { id: id }, function (resp) {
          //  table.row(row).remove().draw();
            $(row).remove();
        })
    }
    return false;
})
$('body').on("click", '.item-edit', function () {
    var _url = $(this).data('href');
    var id = $(this).data('id');
    window.location = _url + "/" + id;
})
//-- js for Index end

//-- js for Create,Edit
$('body').on('keyup', '.js-seo-title', function () {
    if ($('#MetaTitle')) {
        $('#MetaTitle').val($(this).val());
    }
    if ($('#Key')) {
        $('#Key').val(removeCharacter($(this).val()));
    }
})
$('body').on('keyup', '.js-seo-description', function () {
    if ($('#MetaDescription')) {
        $('#MetaDescription').val($(this).val());
    }
})
//-- js for Create,Edit

//-- js for Images Patical in Edit
$('.js-img-remove').click(function () {
    if (confirm("Bạn muốn xóa hình này?")) {
        var _r = $(this).parents('tr');
        var id = $(_r).data('iid');
        var url = $(_r).data('ajax-delete');
        //ajax-active
        $.post(url, { Id: id }, function (resp) {
            $("tr[data-iid=" + resp.Id + "]").remove();
        })
    }
})

$('body').on('click', '[name="imgActives"]', function () {
    var _r = $(this).parents('tr');
    var id = $(_r).data('iid');
    var url = $(_r).data('ajax-active');
    $.post(url, { Id: id }, function () { })
})
//-- js for Images Patical in Edit end


//js for map update
$('body').on('click', '.js-map-location-edit', function () {
    alertify.genericDialog($('#BoxMap')[0]);
    setValue('#autocomplete', $('.js-map-location').val());
    setValue('#pLat', $('.js-map-lat').val());
    setValue('#pLng', $('.js-map-lng').val());

    return false;
})
$('body').on('click', '.js-map-location-update', function () {
    setValue('.js-map-location', document.getElementById('autocomplete').value);
    setValue('.js-map-lat', document.getElementById('pLat').value);
    setValue('.js-map-lng', document.getElementById('pLng').value);
    alertify.closeAll();
})
