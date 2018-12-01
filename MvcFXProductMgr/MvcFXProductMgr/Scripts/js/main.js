$(function () {
    $(".leftMenu-list-header").click(function (e) {
        if ($(this).parent().siblings().hasClass("hover")) {
            $(this).parent().siblings().removeClass("hover");
            $(this).parent().siblings().children("h3").children("i.menu-onOff").removeClass("glyphicon-menu-up");
            $(this).parent().siblings().children("h3").children("i.menu-onOff").addClass("glyphicon-menu-down");
        }
        if ($(this).parent().hasClass("hover")) {
            $(this).parent().removeClass("hover");
            $(this).parent().children("h3").children("i.menu-onOff").removeClass("glyphicon-menu-up");
            $(this).parent().children("h3").children("i.menu-onOff").addClass("glyphicon-menu-down");
        }
        else {
            $(this).parent().addClass("hover");
            $(this).parent().children("h3").children("i.menu-onOff").removeClass("glyphicon-menu-down");
            $(this).parent().children("h3").children("i.menu-onOff").addClass("glyphicon-menu-up");
        }
    });
    $(".file-box input[name='submit']").click(function () {
        if ($("#fileField").val().length < 1) {
            alert("请选择要上传的Excel文件");
            return false;
        }
        if ($("input[name='Standard']:checked").length < 1) {
            alert("请选择执行标准");
            return false;
        }
    });

    //刷新验证码
    $("#valiCode").click(function () {
        this.src = "../Account/GetValidateCode?time=" + (new Date()).getTime();
    });

    //删除批量上传的数据行
    $(".tableBody .btnDel").click(function () {
        if (confirm("确定要删除此产品信息吗")) {
            $(this).parent(".tableBody").remove();
        }
        else {
            return false;
        }
    });
});