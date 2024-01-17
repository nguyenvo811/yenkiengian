
//Format CurrenCy
function RemovenonNumber(s) {
    var o = '';
    for (var member in s) {
        if (s[member].charCodeAt(0) >= 48 && s[member].charCodeAt(0) <= 57) {
            o += s[member];
        }
    }
    return o;
}
function formatString(o, b) {
    var a = o.toString();
    a = replaceChar(a, ',');
    var position = [];
    for (var i = 0; i < a.length; i++) {
        if (i % 3 === 0) {
            position.push(i);
        }
    }
    if (position.length === 0) {
        return a;
    }

    var output = reverse(a);
    var point = 0;
    for (var j = 0; j < position.length; j++) {
        output = [output.slice(0, position[j] + j), b, output.slice(position[j] + j)].join('');
    }
    return reverse(output).substring(0, output.length - 1);
}

// End Format CurrenCy

function formatCurrency(num) {

    if (num === undefined || num === null) {
        return "0"//nếu null thì set = 0
    }
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num === (num = Math.abs(num)));

    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;

    num = Math.floor(num / 100).toString();

    if (cents < 10)
        //cents = "0" + cents;
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    return ((sign) ? '' : '') + num;
    //return ((sign) ? '' : '-') + num;
    //return ((sign) ? '' : '-') + '$' + num;
    //return ((sign) ? '' : '-') + '$' + num + '.' + cents;
}

//Hàm Comma() Format kiểu số khi change TextBox
function Comma(Num) { //function to add commas to textboxes
    Num += '';
    Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
    Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
    x = Num.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    return x1 + x2;
}

function ParseTimeClient(datestr) {
    //http://stackoverflow.com/questions/18014341/how-to-convert-time-correctly-across-timezones
    var d = moment(datestr).format("DD/MM/YYYY HH:mm");//datestr="2013-08-15T17:00:00Z"
    return d;
}

function ParseTimeEn(datestr) {
    //http://stackoverflow.com/questions/18014341/how-to-convert-time-correctly-across-timezones
    var d = moment(datestr).format("DD/MM/YYYY HH:mm");//datestr="2013-08-15T17:00:00Z"
    return d;
}

function FormatTimeClient(datestr) {
    //http://stackoverflow.com/questions/18014341/how-to-convert-time-correctly-across-timezones
    var d = moment(datestr).format("HH:mm:ss");//datestr="2013-08-15T17:00:00Z"
    return d;
}

function ParseDateClient(datestr) {
    //http://stackoverflow.com/questions/18014341/how-to-convert-time-correctly-across-timezones
    var d = moment(datestr).format("DD/MM/YYYY");//datestr="2013-08-15T17:00:00Z"
    return d;
}
//Viết hàm lấy ngày hiện tại dùng chung
function fntoday() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }
    today = mm + '/' + dd + '/' + yyyy;
    //document.write(today);
    return today;
}
//Viết hàm lấy ngày hiện tại -3 dùng chung
function fntoday_3() {
    var today_3 = new Date();
    var dd = today_3.getDate();
    var mm = today_3.getMonth() + 1; //January is 0!
    var yyyy = today_3.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }
    today_3 = mm + '/' + dd + '/' + yyyy;

    //console.log(today_3);
    return today_3;
}

