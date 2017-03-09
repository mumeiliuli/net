$(function () {
    Init();
    function Init() {
        $("#add").on("click", function () {
            newUser();
        });
        $("#edit").on("click", function () {
            editUser();
        });
        $("#delete").on("click", function () {
            deleteUser();
        });
        $("#save").on("click", function () {
            saveUser();
        });
    }
    function newUser() {
        $('#dlg').dialog('open').dialog('setTitle', 'New User');
        $('#fm').form('clear');
    }
    function editUser() {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $('#dlg').dialog('open').dialog('setTitle', 'Edit User');
            $('#fm').form('load', row);
        }
    }
    function deleteUser() {
        var row = $('#dg').datagrid('getSelected');
        if (row) {
            $.messager.confirm('Confirm', '确定要删除该用户?', function (r) {
                if (r) {
                    $.post('/User/Delete', { id: row.Id }, function (result) {
                        if (result.Result) {
                            $('#dg').datagrid('reload');	
                        } else {
                            $.messager.show({	
                                title: 'Error',
                                msg: result.Message
                            });
                        }
                    }, 'json');
                }
            });
        }
    }
    function saveUser() {
        $('#fm').form('submit', {
            url: '/User/Save',
            onSubmit: function () {
                return $(this).form('validate');
            },
            success: function (result) {
                var result = eval('(' + result + ')');
                if (!result.Result) {
                    $(".fitem .error").html(result.Message);
                    $(".fitem .error").show();
                    
                } else {
                    $(".fitem .error").hide();
                    $('#dlg').dialog('close');		// close the dialog
                    $('#dg').datagrid('reload');	// reload the user data
                }
            }
        });
    }

});