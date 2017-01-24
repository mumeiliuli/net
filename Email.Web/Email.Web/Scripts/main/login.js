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
                self.submit($(form));
            }
        });
    },
    submit: function ($form) {
        $.ajax({
            url: "/Account/Login",
            type:"post",
            data: $form.serialize(),
            dataType: "json",
            success: function (r) {
                if (r.Result) {
                    var url = $("#return_url").val();
                    if (url == null || url == ""||url=="\/") {
                        url = "/Home/Index";
                    }
                    window.location.href = url;
                } else {
                    $(".login_error").html(r.Message);
                }
            }
        })
    }


})