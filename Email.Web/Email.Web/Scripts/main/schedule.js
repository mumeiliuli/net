function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var h = date.getHours();
    var mm = date.getMinutes();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d) + " " + (h < 10 ? ('0' + h) : h) + ":" + (mm < 10 ? ('0' + mm) : mm);
}
function myparser(s) {
    if (!s) return new Date();
    var t = s.split(' ');
    var ss = (t[0].split('-'));
    var tt = t[1].split(':');
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    var h = parseInt(tt[0], 10);
    var mm = parseInt(tt[1], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d) && !isNaN(h) && !isNaN(mm)) {
        return new Date(y, m - 1, d, h, mm);
    } else {
        return new Date();
    }
}
$(function () {
    init();
    function init() {
        $('#calendar').fullCalendar({
            //weekends:false, //不显示周末  
            dayClick: function (date, allDay, jsEvent, view) {
                var selDate = $.fullCalendar.formatDate(date, 'YYYY-MM-DD HH:mm');//格式化日期  
                $('#dlg').dialog('open').dialog('setTitle', '新建日程');
                $('#fm').form('load', { StartTime: selDate, EndTime: selDate });
            },
            eventClick: function (calEvent, jsEvent, view) {
                var start = $.fullCalendar.formatDate(calEvent.start, 'YYYY-MM-DD HH:mm');//格式化日期  
                var end;
                if (calEvent.end == null) {
                    end = start;
                } else {
                    end=$.fullCalendar.formatDate(calEvent.end, 'YYYY-MM-DD HH:mm');
                }
                $('#dlg').dialog('open').dialog('setTitle', '编辑日程');
                $('#fm').form('load', { StartTime: start, EndTime: end, Title: calEvent.title, Color: calEvent.color });
            },   
            header: {//设置日历头部信息   
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            firstDay: 7,//每行第一天为周一  
            events: "/Schedule/GetList"
            //events: [{ id: "aa", title: "today", start: "2017-02-20 00:00", end: "2017-02-20 24:00", allDay: true, colr: "#ff0" }, { id: "bb", title: "friday", start: "2017-02-21 10:00", end: "2017-02-21 24:00", allDay: false, color: "#f00" }]
        });
        $('#colorpicker').ColorPicker({
            onSubmit: function (hsb, hex, rgb, el) {
                $(el).val('#'+hex);
                $(el).ColorPickerHide();
            },
            onBeforeShow: function () {
                $(this).ColorPickerSetColor('00ff00');
            }
        })
		.bind('keyup', function () {
		    $(this).ColorPickerSetColor(this.value);
		});

        $("#save").on("click", function () {
            saveSchedule();
        });
    }

    function saveSchedule() {
        $('#fm').form('submit', {
            url: '/Schedule/Save',
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
                }
            }
        });
    }


});