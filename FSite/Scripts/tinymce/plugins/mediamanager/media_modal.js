/* Jquery and required */

function media_manager_show_modal(current_value, callback) {
	var mm_dom = $('#media_manager_modal');
	if (mm_dom.length == 0) {
		$.get("/public/static/mediamodal")
			.then(function (data) {
				$(document.body).append(data);
				media_manager_set_callback(callback);
				$('#media_manager_modal').modal('show');
			})
			.fail(function (err) {
				alert("Internal error");
			});
	}
	else {
		media_manager_set_callback(callback);
		mm_dom.modal('show');
	}
}