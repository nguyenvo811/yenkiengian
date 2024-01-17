var tinymceConfigs = [
    {
    height: 100,
    // selector: "textarea#ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty)",
    menubar: false,
    setup: function (editor) {
        editor.on('change', function () {
           tinymce.triggerSave();
        });
        editor.on('keyup', function () {
            tinymce.triggerSave();
             edtiorKeyUp(editor.getContent());
        });
       // editor.onKeyUp.add(keyupfunc);
    },
    plugins: [
      'autolink lists link charmap preview anchor textcolor',
      'help wordcount'
    ],
    toolbar: 'insert | undo redo |  formatselect | bold italic backcolor  | alignleft aligncenter alignright alignjustify | removeformat | help'
    },
{
    // selector: "textarea#ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty)",
    theme: "modern",
    setup: function (editor) {
        editor.on('change', function () {
            tinymce.triggerSave();
        });
       
        //editor.on('keyup', function (e) {
        //    console.log("editor.getContent()", editor.getContent())
        //   // alert(editor.getContent());
        //});
    },
    //width: 680,
    height: 500,
    plugins: [
            "advlist autolink link image lists charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking",
            "table contextmenu directionality emoticons paste textcolor code fullscreen filemanager"
    ],
    toolbar1: "undo redo | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | styleselect | tools | link unlink anchor | image media | forecolor backcolor | preview",
    toolbar2: null,
    image_advtab: true,
    relative_urls: false,

    //filemanager_crossdomain: true,
    external_filemanager_path: "/Scripts/tinymce/plugins/mediamanager/",
    external_plugins: { "filemanager": "/Scripts/tinymce/plugins/mediamanager/plugin.js" },
    filemanager_title: "Media manager",
    //file_browser_callback: function(field_name, url, type, win) {
    //    win.document.getElementById(field_name).value = 'my browser value';
    //},
    setup: function (editor) {
        //Config để set nội dung 1 file lên tinyMCE tại vị trí ta trỏ chuột
        editor.on('change', function (e) {
            var cotenteditor = e.level.content;

            var links = $(cotenteditor).find("a[href*='/Uploads/Template']");
            $.each($(links), function (i, o) {
                var linkdettail = $(o).attr("href");
              //  console.log("editet", editor)
                $.get(linkdettail, function (respond) {
                    populateMyTinyMCE(respond);
                });
            });
        });
    }
    //http://stackoverflow.com/questions/19627785/responsive-filemanager-in-tinymce-directory-settings
    //End Config để set nội dung 1 file lên tinyMCE tại vị trí ta trỏ chuột
},
{
    // selector: "textarea#ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty)",
    theme: "modern",
    setup: function (editor) {
        editor.on('change', function () {
            tinymce.triggerSave();
        });

        //editor.on('keyup', function (e) {
        //    console.log("editor.getContent()", editor.getContent())
        //   // alert(editor.getContent());
        //});
    },
    //width: 680,
    height: 500,
    plugins: [
            "advlist autolink link image lists charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking",
            "table contextmenu directionality emoticons paste textcolor code fullscreen filemanager"
    ],
    toolbar1: "undo redo | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | styleselect | tools | link unlink anchor | image media | forecolor backcolor | preview",
    toolbar2: null,
    image_advtab: true,
    relative_urls: false,

    //filemanager_crossdomain: true,
    external_filemanager_path: "/Scripts/tinymce/plugins/mediamanager/",
    external_plugins: { "filemanager": "/Scripts/tinymce/plugins/mediamanager/plugin.js" },
    filemanager_title: "Media manager",
    //file_browser_callback: function(field_name, url, type, win) {
    //    win.document.getElementById(field_name).value = 'my browser value';
    //},
    setup: function (editor) {
        //Config để set nội dung 1 file lên tinyMCE tại vị trí ta trỏ chuột
        editor.on('change', function (e) {
            var cotenteditor = e.level.content;

            var links = $(cotenteditor).find("a[href*='/Uploads/Template']");
            $.each($(links), function (i, o) {
                var linkdettail = $(o).attr("href");
                //  console.log("editet", editor)
                $.get(linkdettail, function (respond) {
                    populateMyTinyMCE(respond);
                });
            });
        });
    }
    //http://stackoverflow.com/questions/19627785/responsive-filemanager-in-tinymce-directory-settings
    //End Config để set nội dung 1 file lên tinyMCE tại vị trí ta trỏ chuột
}
,
{
    // selector: "textarea#ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty)",
    theme: "modern",
    setup: function (editor) {
        editor.on('change', function () {
            tinymce.triggerSave();
        });

        //editor.on('keyup', function (e) {
        //    console.log("editor.getContent()", editor.getContent())
        //   // alert(editor.getContent());
        //});
    },
    //width: 680,
    height: 500,
    plugins: [
            "advlist autolink link image lists charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking",
            "table contextmenu directionality emoticons paste textcolor code fullscreen filemanager"
    ],
    toolbar1: "undo redo | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | styleselect | tools | link unlink anchor | image media | forecolor backcolor | preview",
    toolbar2: null,
    image_advtab: true,
    relative_urls: false,

    //filemanager_crossdomain: true,
    external_filemanager_path: "/Scripts/tinymce/plugins/mediamanager/",
    external_plugins: { "filemanager": "/Scripts/tinymce/plugins/mediamanager/plugin.js" },
    filemanager_title: "Media manager",
    //file_browser_callback: function(field_name, url, type, win) {
    //    win.document.getElementById(field_name).value = 'my browser value';
    //},
    setup: function (editor) {
        //Config để set nội dung 1 file lên tinyMCE tại vị trí ta trỏ chuột
        editor.on('change', function (e) {
            var cotenteditor = e.level.content;

            var links = $(cotenteditor).find("a[href*='/Uploads/Template']");
            $.each($(links), function (i, o) {
                var linkdettail = $(o).attr("href");
                //  console.log("editet", editor)
                $.get(linkdettail, function (respond) {
                    populateMyTinyMCE(respond);
                });
            });
        });
    }
    //http://stackoverflow.com/questions/19627785/responsive-filemanager-in-tinymce-directory-settings
    //End Config để set nội dung 1 file lên tinyMCE tại vị trí ta trỏ chuột
},
{
    // selector: "textarea#ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty)",
    theme: "modern",
    setup: function (editor) {
        editor.on('change', function () {
            tinymce.triggerSave();
        });

        //editor.on('keyup', function (e) {
        //    console.log("editor.getContent()", editor.getContent())
        //   // alert(editor.getContent());
        //});
    },
    //width: 680,
    height: 500,
    plugins: [
            "advlist autolink link image lists charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking",
            "table contextmenu directionality emoticons paste textcolor code fullscreen filemanager"
    ],
    toolbar1: "undo redo | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | styleselect | tools | link unlink anchor | image media | forecolor backcolor | preview",
    toolbar2: null,
    image_advtab: true,
    relative_urls: false,

    //filemanager_crossdomain: true,
    external_filemanager_path: "/Scripts/tinymce/plugins/mediamanager/",
    external_plugins: { "filemanager": "/Scripts/tinymce/plugins/mediamanager/plugin.js" },
    filemanager_title: "Media manager",
    //file_browser_callback: function(field_name, url, type, win) {
    //    win.document.getElementById(field_name).value = 'my browser value';
    //},
    setup: function (editor) {
        //Config để set nội dung 1 file lên tinyMCE tại vị trí ta trỏ chuột
        editor.on('change', function (e) {
            var cotenteditor = e.level.content;

            var links = $(cotenteditor).find("a[href*='/Uploads/Template']");
            $.each($(links), function (i, o) {
                var linkdettail = $(o).attr("href");
                //  console.log("editet", editor)
                $.get(linkdettail, function (respond) {
                    populateMyTinyMCE(respond);
                });
            });
        });
    }
    //http://stackoverflow.com/questions/19627785/responsive-filemanager-in-tinymce-directory-settings
    //End Config để set nội dung 1 file lên tinyMCE tại vị trí ta trỏ chuột
}];

//viết hàm này để set content cho nó tại vị trí ta trỏ chuột.
function populateMyTinyMCE(content) {
    tinyMCE.activeEditor.dom.remove(tinyMCE.activeEditor.dom.select("a[href*='/Uploads/Template']"));
    tinyMCE.activeEditor.execCommand('mceInsertContent', false, content);
}
//End Config để sử dụng Tiny MCE

function configTinymce(configId, el) {
    var currentConfig = tinymceConfigs[configId];
    currentConfig.selector = el;
    tinymce.init(tinymceConfigs[configId]);
    // tinymce.settings = tinymceConfigs[configId];
    // tinymce.execCommand('mceAddControl', true, el);
}
function removeTinymce(elId) {
    tinymce.remove(elId);
}

function edtiorKeyUp(content) {
  
    if ($('.js-meta-desc').length !== 0) {
        //remove html and &nbsp;
        //content.replace(/&nbsp;/g, '').replace(/<\/?[^>]+>/g, "")
        
        $('.js-meta-desc').val($('<div>' + content + '</div>').text())
    }
}