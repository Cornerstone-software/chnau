﻿$(function () {
    //左侧菜单栏效果控制
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

    //点击“确认按钮时，进行验证”
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
        if ($(this).parents(".table").children(".tableBody").length > 1) {
            if (confirm("确定要删除此产品信息吗")) {
                $(this).parent(".tableBody").remove();
            }
            else {
                return false;
            }
        } else {
            alert("仅剩一条记录，不可再删除！");
        }

    });

    //点击已保存的产品信息列表，查看产品详情
    $(".prodDetail").click(function () {
        var cerNum = $(this).children(".cernumCol").text();
        var barcode = $(this).children(".barCol").text();
        var href = "/Product/GetProduct/?cerNum=" + cerNum + "&barcode=" + barcode;
        window.location = href;
    });

    //设置日期格式
    $("#txtStartDate,#txtEndDate").datepicker({
        dateFormat: 'yy-mm-dd'
    });
    //设置时间点格式
    $("#txtStartTime,#txtEndTime").timepicker({
        timeFormat: 'HH:mm:ss'
    });
    ///////////////////////////////////////////////////////
    ///验证产品查询条件
    $("form.prodQuery").submit(function () {

        if ($("#TxtCompany").val()) {
            $("#TxtCompany").parents("dl").children(".errMsg").hide();
        } else {
            $("#TxtCompany").parents("dl").children(".errMsg").show();
            return false;
        }

        if ($("#txtStartDate").val().length > 0) {
            $("#txtStartDate").parents("dl").children(".errMsg").hide();
        } else {
            $("#txtStartDate").parents("dl").children(".errMsg").show();
            return false;
        }
        if ($("#txtStartTime").val()) {
            $("#txtStartTime").parents("dl").children(".errMsg").hide();
        } else {
            $("#txtStartTime").parents("dl").children(".errMsg").html("请重新选择起始时间点！");
            $("#txtStartTime").parents("dl").children(".errMsg").show();
            return false;
        }

        if ($("#txtEndDate").val()) {
            $("#txtEndDate").parents("dl").children(".errMsg").hide();
        } else {
            $("#txtEndDate").parents("dl").children(".errMsg").show();
            return false;
        }
        if ($("#txtEndTime").val()) {
            $("#txtEndTime").parents("dl").children(".errMsg").hide();
        } else {
            $("#txtEndTime").parents("dl").children(".errMsg").html("请重新选择截止时间点！");
            $("#txtEndTime").parents("dl").children(".errMsg").show();
            return false;
        }
        var startDate = $("#txtStartDate").val();
        var endDate = $("#txtEndDate").val();
        var startTime = $("#txtStartTime").val();
        var endTime = $("#txtEndTime").val();
        var iRel = compareDate(startDate, endDate, "-");
        if (iRel > 0) {
            if (iRel == 1) {
                var fcomp = compareTime(beginTime, endTime);
                if (!fcomp) {
                    $("#txtStartTime").parents("dl").children(".errMsg").html("开始的时间点必须大于截止的时间点，请重新选择！");
                    $("#txtStartTime").parents("dl").children(".errMsg").show();
                    return false;
                }
            }
        } else {
            $("#txtStartDate").parents("dl").children(".errMsg").html("起始日期必须大于截止日期，请重新选择！");
            $("#txtStartDate").parents("dl").children(".errMsg").show();
            return false;
        }
    });

    $("#txtStartDate,#txtStartTime,#txtEndDate,#txtEndTime").blur(function () {
        if ($(this).val()) {
            $(this).parents("dl").children(".errMsg").hide();
        } else {
            $(this).parents("dl").children(".errMsg").show();
        }
    });
    $("#txtStartDate,#txtStartTime,#txtEndDate,#txtEndTime").change(function () {
        if ($(this).val()) {
            $(this).parents("dl").children(".errMsg").hide();
        } else {
            $(this).parents("dl").children(".errMsg").show();
        }
    });
});
//时间比较
function compareTime(btime, etime) {
    if (btime && etime) {
        var arrBT = btime.split(":");
        var arrET = etime.split(":");
        var iBT = parseInt(arrBT[0]) * 60 * 60 + parseInt(arrBT[1]) * 60 + parseInt(arrBT[2]);
        var iET = parseInt(arrET[0]) * 60 * 60 + parseInt(arrET[1]) * 60 + parseInt(arrET[2]);
        if (iBT > iET || iBT == iET) {

            return false;
        } else {
            return true;
        }
    }
}
//////////////////////////////////////////////////////////////
//日期比较
//0:表示起始日期较大;1:起始日期等于截止日期;2:表示起始日期较小
//////////////////////////////////////////////////////////////            
///参数 bDate:起始日期
///参数 eDate:结束日期
function compareDate(bDate, eDate, limit) { 
    if(limit.length==1){
        if (limit == "/" || limit == "-") { 
            if(bDate && eDate){
               var arrBD = bDate.split(limit);
               var arrED = eDate.split(limit);
               var iBYear = parseInt(arrBD[0]);
               var iBMon = parseInt(arrBD[1]);
               var iBDay = parseInt(arrBD[2]);
               var iEYear = parseInt(arrED[0]);
               var iEMon = parseInt(arrED[1]);
               var iEDay = parseInt(arrED[2]);
               
               if(iBYear>iEYear){//若起始日期的年份>截止时间的年份
                    return 0;
               }else if(iBYear==iEYear){ //若起始日期的年份=截止时间的年份
                    if(iBMon>iEMon){
                        return 0;
                    }else if(iBMon==iEMon){
                        if(iBDay>iEDay){
                            return 0;
                        }else if(iBDay==iEDay){
                            return 1;
                        }else{
                            return 2;
                        }
                    }else{
                        return 2;
                    }

               }else{//若起始日期的年份<截止时间的年份
                    return 2;
               }    
            }
        }
    }
    
}