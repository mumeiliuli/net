$(function () {
    KindEditor.ready(function (K) {
        var editor1 = K.create('#note', {
            uploadJson: '/Note/PicUpload',
            fileManagerJson: '/Note/PicManage',
            allowFileManager: true,
            afterCreate: function () {
                var self = this;
                K.ctrl(document, 13, function () {
                    self.sync();
                });
                K.ctrl(self.edit.doc, 13, function () {
                    self.sync();
                });
            },
            afterChange: function () { //编辑器内容发生变化后，将编辑器的内容设置到原来的textarea控件里
                this.sync();
            },
        });
        prettyPrint();
    });
    $('#save').click(function () {
     console.log(KindEditor.html('note'));
        //$.ajax({
        //    url: "/Note/Save",
        //    type:"post",
        //    dataType: "json",
        //    success: function () {

        //    }
        //});
        $('#notef').form('submit', {
            url: '/Note/Save',
            onSubmit: function () {
                return $(this).form('validate');
            },
            success: function (result) {
                var result = eval('(' + result + ')');
                $.messager.alert('', result.Message);
            }
        });

    });
   
});