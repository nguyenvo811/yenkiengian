tinymce.PluginManager.add("filemanager", function (instance) {
	function handle_file_cb(field_name, url, type, win) {
		/* by order
			field_name: id/name of the form element that the browser should insert its URL into
			url: value that is currently inside the field
			type: file, image or flash depending on what dialog is calling the function
			win: reference to the dialog/window that executes the function
		*/
		var width = window.innerWidth - 40,
			height = window.innerHeight - 60;

		if (width > 1800) width = 1800;
		if (height > 1200) height = 1200;
		if (width > 600) {
			var tmp = (width - 20) % 138;
			width = width - tmp + 10;
		}
	    /* Here goes the URL to your server-side script which manages all file browser things. */
		var cmsURL = window.location.pathname;     // your URL could look like "/scripts/my_file_browser.php"
        var syntax = cmsURL.split('/')[1];//project, product, blog ...
        if (syntax.toLocaleLowerCase() === "admin")//trick to get 
            syntax = cmsURL.split('/')[2];////project, product, blog ...
        
		//var searchString = window.location.search; // possible parameters
		//if (searchString.length < 1) {
		//    // add "?" to the URL to include parameters (in other words: create a search string because there wasn't one before)
		//    searchString = "?";
	    //}
		
		//console.log("cmsURL", cmsURL.split('/')[1]);
	//	console.log("searchString", searchString);
		// open the dialog using content retrieved from 'file'
		instance.windowManager.open({
			title: "Media Manager",
			file: "/public/static/MediaEditorMCE?type=" + type + "&syntax=" + syntax,
			width,
			height,
			resizable: true,
			maximizable: true,
			inline: 1
		}, {
				setUrl: function (dataToSet) {
					var element = win.document.getElementById(field_name);
					element.value = instance.convertURL(dataToSet);

					if ("fireEvent" in element)
						element.fireEvent("onchange");
					else {
						var a = document.createEvent("HTMLEvents");
						a.initEvent("change", false, true), element.dispatchEvent(a)
					}
				},
				input: field_name,
				win: win
			})
	}

	// now attach it to tinyMCE
	return instance.settings.file_browser_callback = handle_file_cb;
});