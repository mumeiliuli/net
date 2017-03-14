
$(function () {
    Init();
    function Init() {

        var txt = $("textarea.txt_content").emojioneArea({
            pickerPosition: "bottom"
        });
        var com_txt = $("textarea.comment_txt").emojioneArea({
            pickerPosition: "bottom"
        });
        $("#sendr").click(function () {
            var $this = $(this);
            $this.attr("disabled", true);
            var emoj = txt[0].emojioneArea;
            var content = emoj.getText();
            if (content == null || content == "") {
                return;
            }
            $.post('/LifeStyle/AddRecord', { content: content }, function (result) {
                if (result.Result) {
                    emoj.setText("");
                    GetRecordList();
                } else {
                    $.messager.show({
                        title: 'Error',
                        msg: result.Message
                    });
                }
                $this.attr("disabled", false);
            }, 'json');

        });
        $("#comment").on("click", ".zan", function () {
            var $this = $(this);
            $this.attr("disabled", true);
            var count =parseInt($this.find(".like").text());
            var id = $this.parents(".comment_item").data("id");
            $.post('/LifeStyle/ClickLike', { id: id}, function (result) {
                if (result.Result) {
                    if (result.Data.Like) {
                        $this.addClass("dz");
                        count += 1;
                    } else {
                        $this.removeClass("dz");
                        count -= 1;
                    }
                    $this.find(".like").text(count);
                } else {
                    $.messager.show({
                        title: 'Error',
                        msg: result.Message
                    });
                }
                $this.attr("disabled", false);
            }, 'json');
        })
        $("#comment").on("click", ".btnP", function () {
            $(".comment_list").hide();
            var item = $(this).parents(".comment_item");
            var txt = item.find(".mycomment");
            var $list = item.children(".comment_list");
            
            if (txt.length == 0) {
                var id = item.data("id");
                $(".mycomment").appendTo(item);
                $(".mycomment").show();
                $(".mycomment").attr("data-id", id);
                $list.show();
                GetCommentList($list, id);
                
            } else {
                $(".mycomment").toggle();
                $list.toggle();
            }
            

        });
        $("#comment").on("click", "#sendc", function () {
            var $this = $(this);
            $this.attr("disabled", true);
            var emoj = com_txt[0].emojioneArea;
            var content = emoj.getText();
            if (content == null || content == "") {
                return;
            }
            var $list = $(this).parents(".comment_item").children(".comment_list");
            var id=$(this).parents(".mycomment").data("id");
            $.post('/LifeStyle/AddComment', { content: content, id: id }, function (result) {
                if (result.Result) {
                    emoj.setText("");
                    GetCommentList($list, id);
                } else {
                    $.messager.show({
                        title: 'Error',
                        msg: result.Message
                    });
                }
                $this.attr("disabled", false);
            }, 'json');
        })
        GetRecordList();
    }

    function GetRecordList() {
        $.get('/LifeStyle/GetRecordList', function (result) {
            if (result.Result) {
                $("#comment").html("");
                $("#list_tmpl").tmpl({ datas: result.Data }).appendTo("#comment");
                
            } else {
                $.messager.show({
                    title: 'Error',
                    msg: result.Message
                });
            }
        }, 'json');
    }
    function GetCommentList($list,id) {
        $.get('/LifeStyle/GetCommentList', {id:id}, function (result) {
            if (result.Result) {
                $list.html("");
                var L = result.Data.length;
                $list.parents(".comment_item").find(".count_p").text(L);
                $("#comment_tmpl").tmpl({ datas: result.Data }).appendTo($list);

            } else {
                $.messager.show({
                    title: 'Error',
                    msg: result.Message
                });
            }
        }, 'json');
    }

});