//override defaults

alertify.defaults.transition = "slide";
alertify.defaults.pinnable = true;
alertify.defaults.reverseButtons = true;
//alertify.defaults.basic = !false;
alertify.defaults.closable = true;
//alertify.defaults.modal=!true,
//alertify.defaults.movable=!true,
//alertify.defaults.moveBounded = !false,
//alertify.defaults.padding= !true,

alertify.defaults.glossary={
    // dialogs default title
   // title: '<img class="header-notify-img" src="/img/logo/logo.png"><i class="fa fa-bell"></i>Thông báo',
    title: '<a class="header-notify-icon" href="#"><i class="fa fa-bell"></i>Thông báo</a>',

    // ok button text
        ok: 'Đồng ý',
    // cancel button text
        cancel: 'Hủy'            
};


alertify.defaults.notifier = {
    // auto-dismiss wait time (in seconds)  
    delay: 5,
    //default position
    position: 'top-right',
    closeButton: true
};

// theme settings
alertify.defaults.theme = {
    // class name attached to prompt dialog input textbox.
    input: 'form-control cus-alert-input',
    // class name attached to ok button
    ok: 'btn btn-info cus-alert-bt-ok',
    // class name attached to cancel button 
    cancel: 'btn btn-danger cus-alert-bt-cancel'
};

//mutil dialog


alertify.apiMapDialog || alertify.dialog('apiMapDialog',function(){
    var iframe;
    return {
        // dialog constructor function, this will be called when the user calls alertify.apiMapDialog(geoLatLng)
        main:function(geoLatLng){
            //set the geoLatLng setting and return current instance for chaining.
            return this.set({ 
                'geoLatLng': geoLatLng,

            });
        },
        // we only want to override two options (padding and overflow).
        setup:function(){
            return {
                options:{
                    //disable both padding and overflow control.
                    padding : !1,
                    overflow: !1,
                }
            };
        },
        // This will be called once the DOM is ready and will never be invoked again.
        // Here we create the iframe to embed the video.
        build:function(){           
            // create the iframe element
            iframe = document.createElement('iframe');
            iframe.frameBorder = "no";
            iframe.width = "100%";
            iframe.height = "100%";
            // add it to the dialog
            this.elements.content.appendChild(iframe);

            //give the dialog initial height (half the screen height).
            this.elements.body.style.minHeight = screen.height * .5 + 'px';
        },
        // dialog custom settings
        settings:{
            geoLatLng:undefined
        },
        // listen and respond to changes in dialog settings.
        settingUpdated:function(key, oldValue, newValue){
            switch(key){
               case 'geoLatLng':
                   iframe.src = "/map/get-map/" + newValue.lat + "/" + newValue.lng + '/' + newValue.content;
                    break;
               // case 'typeId'://project id or productid
                 //  iframe.src = "/map/get-map-by-id/" + newValue.type + "/"+newValue.id;
             
                //   break;
            }
        },
        // listen to internal dialog events.
        hooks:{
            // triggered when the dialog is closed, this is seperate from user defined onclose
            onclose: function(){
               // iframe.contentWindow.postMessage('{"event":"command","func":"pauseVideo","args":""}','*');
            },
            // triggered when a dialog option gets update.
            // warning! this will not be triggered for settings updates.
            onupdate: function(option,oldValue, newValue){
                switch(option){
                    case 'resizable':
                        if(newValue){
                            this.elements.content.removeAttribute('style');
                            iframe && iframe.removeAttribute('style');
                        }else{
                            this.elements.content.style.minHeight = 'inherit';
                            iframe && (iframe.style.minHeight = 'inherit');
                        }
                    break;    
                }    
            }
        }
    };
});
//show the dialog
//alertify.apiMapDialog({lat:2,lng:3}).set({frameless:true});


alertify.YoutubeDialog || alertify.dialog('YoutubeDialog', function () {
    var iframe;
    return {
        // dialog constructor function, this will be called when the user calls alertify.YoutubeDialog(videoId)
        main: function (videoId) {
            //set the videoId setting and return current instance for chaining.
            return this.set({
                'videoId': videoId
            });
        },
        // we only want to override two options (padding and overflow).
        setup: function () {
            return {
                options: {
                    //disable both padding and overflow control.
                    padding: !1,
                    overflow: !1,
                }
            };
        },
        // This will be called once the DOM is ready and will never be invoked again.
        // Here we create the iframe to embed the video.
        build: function () {
            // create the iframe element
            iframe = document.createElement('iframe');
            iframe.frameBorder = "no";
            iframe.width = "100%";
            iframe.height = "100%";
            // add it to the dialog
            this.elements.content.appendChild(iframe);

            //give the dialog initial height (half the screen height).
            this.elements.body.style.minHeight = screen.height * .5 + 'px';
        },
        // dialog custom settings
        settings: {
            videoId: undefined
        },
        // listen and respond to changes in dialog settings.
        settingUpdated: function (key, oldValue, newValue) {
            switch (key) {
                case 'videoId':
                    iframe.src = "https://www.youtube.com/embed/" + newValue + "?enablejsapi=1";
                    break;
            }
        },
        // listen to internal dialog events.
        hooks: {
            // triggered when the dialog is closed, this is seperate from user defined onclose
            onclose: function () {
                iframe.contentWindow.postMessage('{"event":"command","func":"pauseVideo","args":""}', '*');
            },
            // triggered when a dialog option gets update.
            // warning! this will not be triggered for settings updates.
            onupdate: function (option, oldValue, newValue) {
                switch (option) {
                    case 'resizable':
                        if (newValue) {
                            this.elements.content.removeAttribute('style');
                            iframe && iframe.removeAttribute('style');
                        } else {
                            this.elements.content.style.minHeight = 'inherit';
                            iframe && (iframe.style.minHeight = 'inherit');
                        }
                        break;
                }
            }
        }
    };
});
//show the dialog
//alertify.YoutubeDialog('GODhPuM5cEE').set({ frameless: false });


//alertify.apiGeneralDialog('title','mesage').set({ frameless: false });

alertify.apiUrlDialog || alertify.dialog('apiUrlDialog', function () {
	var iframe;
	return {
		// dialog constructor function, this will be called when the user calls alertify.apiMapDialog(geoLatLng)
		main: function (url) {
			//set the url setting and return current instance for chaining.
			return this.set({
				'url': url

			});
		},
		// we only want to override two options (padding and overflow).
		setup: function () {
			return {
				options: {
					//disable both padding and overflow control.
					padding: !1,
					overflow: !1,
				}
			};
		},
		// This will be called once the DOM is ready and will never be invoked again.
		// Here we create the iframe to embed the video.
		build: function () {
			// create the iframe element
			iframe = document.createElement('iframe');
			iframe.frameBorder = "no";
			iframe.width = "100%";
			iframe.height = "100%";
			// add it to the dialog
			this.elements.content.appendChild(iframe);

			//give the dialog initial height (half the screen height).
            this.elements.body.style.minHeight = screen.height * .5 + 'px';

		},
		// dialog custom settings
		settings: {
			url: undefined
		},
		// listen and respond to changes in dialog settings.
		settingUpdated: function (key, oldValue, newValue) {
			switch (key) {
				case 'url':
					iframe.src =  newValue.url ;
					break;
				// case 'typeId'://project id or productid
				//  iframe.src = "/map/get-map-by-id/" + newValue.type + "/"+newValue.id;

				//   break;
			}
		},
		// listen to internal dialog events.
		hooks: {
			// triggered when the dialog is closed, this is seperate from user defined onclose
			onclose: function () {
				// iframe.contentWindow.postMessage('{"event":"command","func":"pauseVideo","args":""}','*');
			},
			// triggered when a dialog option gets update.
			// warning! this will not be triggered for settings updates.
			onupdate: function (option, oldValue, newValue) {
				switch (option) {
					case 'resizable':
						if (newValue) {
							this.elements.content.removeAttribute('style');
							iframe && iframe.removeAttribute('style');
						} else {
							this.elements.content.style.minHeight = 'inherit';
							iframe && (iframe.style.minHeight = 'inherit');
						}
						break;
				}
			}
		}
	};
});



alertify.apiGeneralDialog || alertify.dialog('apiGeneralDialog', function () {
	return {
		main: function (title, message,_width) {
			this.setting('title', title);
			this.message = message; 
            this._width = _width;
            if (!_width) {
                _width = '95%';
            }
			
		},
		setup: function () {
			return {
				options: {
					maximizable: true,
				//	closableByDimmer: false,
					closableByDimmer: !1,
					resizable: false,
					padding: false, overflow: false,
					transition: 'fade',
					/*disable autoReset, to prevent the dialog from resetting it's size on window resize*/
					autoReset: false
				}
			};
		},
		prepare: function () {
			this.setContent(this.message);
			this.elements.footer.style.visibility = "hidden";
		},
		settingUpdated: function (key, oldValue, newValue) {
			console.log(newValue);
			this.setContent(newValue.message);
		},
		hooks: {
			onshow: function () {
				
				this.elements.dialog.style.maxWidth = 'none';
				this.elements.dialog.style.width = this._width;
			}
		}
	};
}, false, 'alert');

alertify.genericDialog || alertify.dialog('genericDialog', function () {
    return {
        main: function (content) {
            this.setContent(content);
        },
        setup: function () {
            return {
                options: {
                    basic: true,
                    maximizable: false,
                    resizable: false,
                    padding: false
                }
            };
        },
        settings: {
            selector: undefined
        }
    };
});
