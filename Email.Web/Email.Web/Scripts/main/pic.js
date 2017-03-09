$(function () {
    function Init() {
        
        $("#upload_btn").click(function () {
            Upload();
            $('#dlg').dialog('open').dialog('setTitle', '上传图片');
        });

        $("#save_img").click(function () {
            SaveImg();
            $('#dlg').dialog('close');
        });
        GetPicList();
    }

    function GetPicList() {
        $.ajax({
            url: "/Pic/GetList",
            type: "get",
            dataType: "json",
            success: function (r) {
                if (r.Result) {
                    $("#albums").html("");
                    $("#list_tmpl").tmpl({ datas: r.Data }).appendTo("#albums");
                } 

            }
        });
    }
    function Upload() {
        $("#upload_pic").fileinput({
            language: 'zh',
            showUpload: true,
            uploadUrl: "/Pic/Upload",
            showCaption: false,
            browseClass: "btn btn-primary",
            fileType: "any",
            overwriteInitial: true,
            initialPreviewAsData: true,
            initialPreview: [
                "http://localhost:4797/Content/images/imageno.png",
            ],
            initialPreviewConfig: [
                { caption: "imageno.jpg", size: 2161, width: "120px", url: "{$url}", key: 1 }
            ],
            previewSettings: {
                image: { width: "auto", height: "120px" }
            }
        });
        $("#upload_pic").on("fileuploaded", function (event, data, previewId, index) {
            var url = data.response.Data.url;
            $("#" + previewId + "").attr("data-url", url);
        });
    }
    function SaveImg() {
        var urls = new Array();
        $("div.file-preview-thumbnails .kv-preview-thumb").each(function () {
            var url = $(this).data("url");
            if (url != undefined && url != "") {
                urls.push(url);
            }
        });
        if (urls.length != 0) {
            var name=$("#albumname").val();
            $.ajax({
                url: "/Pic/SaveImg",
                type: "post",
                data: {name:name,urls:urls},
                dataType: "json",
                success: function (r) {
                    if (r.Result) {
                        GetPicList();
                    } else {
                        $.messager.alert(r.Message);
                    }
                    
                }
            });
        }
        
       
    }
    Init();



});