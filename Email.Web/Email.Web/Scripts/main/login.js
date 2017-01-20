define({
    init: function () {
        var self = this;
        $(".login_bg").width($(window).width());
        $(".login_bg").height($(window).height());
        self.validate();
    },
    validate: function () {
        var self = this;
        $(".login_form").validate({
            rules: {
                username: {
                    required: true,
                },
                password: {
                    required: true,
                },
            },
            messages: {
                username: {
                    required: "用户名不能为空",
                },
                password: {
                    required: "密码不能为空",
                },
            },
            errorPlacement: function(error, element) {  
                $(".login_error").html(error);  
            },
            submitHandler: function (form) {
                alert("提交事件!");
                self.submit($(form));
            }
        });
    },
    submit: function ($form) {

    }


})