
$(function () {
    Init();
    function Init() {

        var txt=$(".txt_content").emojioneArea({
            pickerPosition: "bottom"
        });
        $("#sendr").click(function () {
            var content = txt[0].emojioneArea.getText();
            if (content == null || content == "") {
                return;
            }
            $.post('/LifeStyle/AddRecord', { content: content }, function (result) {
                if (result.Result) {
                    GetRecordList();
                } else {
                    $.messager.show({
                        title: 'Error',
                        msg: result.Message
                    });
                }
            }, 'json');

        });
        $("#comment").on("click", ".zan", function () {
            var $this = $(this);
            var id = $this.parents(".comment_item").data("id");
            $.post('/LifeStyle/ClickLike', { id: id }, function (result) {
                if (result.Result) {
                    $this.find(".like").text(result.Data.Count);
                } else {
                    $.messager.show({
                        title: 'Error',
                        msg: result.Message
                    });
                }
            }, 'json');
        })
        GetRecordList();
    }

    function GetRecordList() {
        $.get('/LifeStyle/GetRecordList', function (result) {
            if (result.Result) {
                $("#list_tmpl").tmpl({ datas: result.Data }).appendTo("#comment");
            } else {
                $.messager.show({
                    title: 'Error',
                    msg: result.Message
                });
            }
        }, 'json');
    }

});