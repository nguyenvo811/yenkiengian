

//(function () {

//var _div = document.createElement('div');

//jQuery.fn.dataTable.ext.type.search.html = function ( data ) {
//	_div.innerHTML = data;

//	return _div.textContent ?
//		_div.textContent.replace(/\n/g," ") :
//		_div.innerText.replace(/\n/g," ");
//};

//})();



(function () {
    var _div = document.createElement('div');
    jQuery.fn.dataTable.ext.type.search.html = function (data) {
        _div.innerHTML = data;
        return _div.textContent ?
            _div.textContent
                .replace(/[aàảãáạăằẳẵắặâầẩẫấậAÀẢÃÁẠĂẰẲẴẮẶÂẦẨẪẤẬäÄãÃåÅæÆ]/g, 'a')
                .replace(/[çÇ]/g, 'c')
                .replace(/[dĐ]/g, 'd')
                .replace(/[eèẻẽéẹêềểễếệEÈẺẼÉẸÊỀỂỄẾỆëË]/g, 'e')
                .replace(/[iìỉĩíịIÌỈĨÍỊîÎïÏîĩĨĬĭ]/g, 'i')
                .replace(/[ñÑ]/g, 'n')
                .replace(/[oòỏõóọôồổỗốộơờởỡớợOÒỎÕÓỌÔỒỔỖỐỘƠỜỞỠỚỢöÖœŒ]/g, 'o')
                .replace(/[ß]/g, 's')
                .replace(/[uùủũúụưừửữứựUÙỦŨÚỤƯỪỬỮỨỰûÛüÜ]/g, 'u')
                .replace(/[yỳỷỹýỵYỲỶỸÝỴŷŶŸÿ]/g, 'n') :
            _div.innerText.replace(/[üÜ]/g, 'u')
               .replace(/[aàảãáạăằẳẵắặâầẩẫấậAÀẢÃÁẠĂẰẲẴẮẶÂẦẨẪẤẬäÄãÃåÅæÆ]/g, 'a')
                .replace(/[çÇ]/g, 'c')
                .replace(/[dĐ]/g, 'd')
                .replace(/[eèẻẽéẹêềểễếệEÈẺẼÉẸÊỀỂỄẾỆëË]/g, 'e')
                .replace(/[iìỉĩíịIÌỈĨÍỊîÎïÏîĩĨĬĭ]/g, 'i')
                .replace(/[ñÑ]/g, 'n')
                .replace(/[oòỏõóọôồổỗốộơờởỡớợOÒỎÕÓỌÔỒỔỖỐỘƠỜỞỠỚỢöÖœŒ]/g, 'o')
                .replace(/[ß]/g, 's')
                .replace(/[uùủũúụưừửữứựUÙỦŨÚỤƯỪỬỮỨỰûÛüÜ]/g, 'u')
                .replace(/[yỳỷỹýỵYỲỶỸÝỴŷŶŸÿ]/g, 'n') ;
    };
})();