﻿$(function(){
    function init() {
        $(".login_bg").width($(window).width());
        $(".login_bg").height($(window).height());
        validate();
    }
    function validate() {
        $(".login_form").validate({
            rules: {
                username: {
                    required: true,
                },
                password: {
                    required: true,
                },
                verify: {
                    required: true,
                    remote: {
                        url: '/Account/CheckVerifyCode',
                        type: 'get',
                        data: {
                            verifycode: function () {
                                return $("#verify").val();
                            }
                        }

                    }
                }
            },
            messages: {
                username: {
                    required: "用户名不能为空",
                },
                password: {
                    required: "密码不能为空",
                },
                verify: {
                    required: "验证码不能为空",
                    remote:"验证码错误"
                },
            },
            errorPlacement: function(error, element) {  
                $(".login_error").html(error);  
            },
            submitHandler: function (form) {
                submit($(form));
            }
        });
    }
    function submit($form) {
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
    init();
})