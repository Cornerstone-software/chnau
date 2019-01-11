$(function () {
    //给dropdownlist增加css
    $('select').addClass("txt");
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
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
    $("#txtStartDate,#txtEndDate").datepicker({
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        monthNamesShort: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
        dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
        changeYear: true,
        changeMonth: true,
        dateFormat: 'yy-mm-dd'
    });
    //设置时间点格式
    $("#txtStartTime,#txtEndTime").timepicker({
        timeOnlyTitle: '选择时间',
        timeText: '时间',
        hourText: '小时',
        minuteText: '分钟',
        secondText: '秒',
        currentText: '现在',
        closeText: '完成',
        showSecond: true, //显示秒  
        timeFormat: 'HH:mm:ss'
    });
    ///////////////////////////////////////////////////////
    ///验证产品查询条件
    $("form.prodQuery").submit(function () {

        if ($("#Company").val()) {
            $("#Company").parents("dl").children(".errMsg").hide();
        } else {
            $("#Company").parents("dl").children(".errMsg").show();
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
    //////////////////////////////////////////////////////////////////////////////
    //日期错误提示信息显示或隐藏
    $("#txtStartDate,#txtStartTime,#txtEndDate,#txtEndTime").blur(function () {
        if ($(this).val()) {
            $(this).parents("dl").children(".errMsg").hide();
        } else {
            $(this).parents("dl").children(".errMsg").show();
        }
    });
    ////////////////////////////////////////////////////////////////////////////
    //日期错误提示信息显示或隐藏
    $("#txtStartDate,#txtStartTime,#txtEndDate,#txtEndTime").change(function () {
        if ($(this).val()) {
            $(this).parents("dl").children(".errMsg").hide();
        } else {
            $(this).parents("dl").children(".errMsg").show();
        }
    });
    ///////////////////////////////////////////////////////////////////////////
    //生成条形码图片
    generateBarcode();
    //////////////////////////////////////////////////////////////////////////
    //产品类别和执行标准的对应切换
    $('input[name="Category"]').click(function () {
        if ($("input[name='Category']:checked").hasClass("DCategory")) {
            $(".GStandard").addClass("hide");
            $(".DStandard").removeClass("hide");
        } else {
            $(".GStandard").removeClass("hide");
            $(".DStandard").addClass("hide");
        }

    });
    //////////////////////////////////////////////////////////////////////////////
    //更新公司信息时，验证
    $(".companyInfo form").submit(function () {
        alert("test");
        var strName = $("#TxtCName").val();
        if (strName != "") {
            strName = trim(strName);
        }

        //公司名称不能为空
        if (strName == null || strName.length < 1 || strName == "") {
            $("#TxtCName").siblings(".errMsg").show();
            return false;
        }
        else {
            $("#TxtCName").siblings(".errMsg").hide();
        }
        //验证公司网址
        var strUrl = trim($("#TxtCUrl").val());
        if (strUrl.length > 0) {
            if (isURL(strUrl) == true) {
                $("#TxtCUrl").siblings(".errMsg").hide();
            } else {
                $("#TxtCUrl").siblings(".errMsg").show();
                return false;
            }
        }
        //验证电话号码
        var strTel = trim($("#TxtCTel").val());
        if (strTel.length > 0) {
            if (checkTel(strTel) == true) {
                $("#TxtCTel").siblings(".errMsg").hide();
            } else {
                $("#TxtCTel").siblings(".errMsg").show();
                return false;
            }
        }
    });
    ////////////////////////////////////////////////////////////////////////////
    //删除公司信息
    $(".btn .deleteBtn").click(function () {
        if (!confirm("确定要删除吗？")) {
            return false;
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

//////////////////////////////////////////////////////////////////////
 (function($) {

                $(function() {
                    $.datepicker.regional['zh-CN'] = {
                        changeMonth: true,
                        changeYear: true,
                        clearText: '清除',
                        clearStatus: '清除已选日期',
                        closeText: '关闭',
                        closeStatus: '不改变当前选择',
                        prevText: '<上月',
                        prevStatus: '显示上月',
                        prevBigText: '<<',
                        prevBigStatus: '显示上一年',
                        nextText: '下月>',
                        nextStatus: '显示下月',
                        nextBigText: '>>',
                        nextBigStatus: '显示下一年',
                        currentText: '今天',
                        currentStatus: '显示本月',
                        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
                        monthNamesShort: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                        monthStatus: '选择月份',
                        yearStatus: '选择年份',
                        weekHeader: '周',
                        weekStatus: '年内周次',
                        dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
                        dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
                        dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
                        dayStatus: '设置 DD 为一周起始',
                        dateStatus: '选择 m月 d日, DD',
                        dateFormat: 'yy-mm-dd',
                        firstDay: 1,
                        initStatus: '请选择日期',
                        isRTL: false
                    };

                });
 });
//////////////////////////////////////////////////////////////////////////////
//generate barcode
function generateBarcode() {
    var value = $("#barCode").val();
    var btype = "code128";
    var renderer = "css";
    
    var quietZone = false;
    var bgColor="#FFFFFF";
    var color="#000000";
    var barWidth='1';
    var barHeight="40";
    var moduleSize="5";
    var posX="10";
    var posY="20";
    var settings = {
        output: renderer,
        bgColor: bgColor,
        color: color,
        barWidth: barWidth,
        barHeight: barHeight,
        moduleSize: moduleSize,
        posX: posX,
        posY: posY,
        addQuietZone: quietZone
    };
   
        $("#barcodeTarget").html("").show().barcode(value, btype, settings);
    }
    ////////////////////////////////////////////////////////////////////////////////
    //去掉str前后的空格
    function trim(str) {
        return str.replace(/(^\s*)|(\s*$)/g, "");
    }
    ////////////////////////////////////////////////////////////////////////////////
    //验证url格式是否正确
     //varreg=/[0-9a-zA-z]+.(html|htm|shtml|jsp|asp|php|com|cn|net|com.cn|org)$/; 
    //必须包含.(最后面一个.前面最少有一个字符)且.后面最少有一个单词字符，最后一个字符必须为单词字符或/    
    function isURL(str) {
       var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
		+ "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" // ftp的user@
		+ "(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP形式的URL- 199.194.52.184
		+ "|" // 允许IP和DOMAIN（域名）
		+ "([0-9a-z_!~*'()-]+\.)*" // 域名- www.
		+ "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // 二级域名
		+ "[a-z]{2,6})" // first level domain- .com or .museum
		+ "(:[0-9]{1,4})?" // 端口- :80
		+ "((/?)|" // a slash isn't required if there is no file name
		+ "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
       var re = new RegExp(strRegex);
       return re.test(str);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////
    //支持手机号码、含区号固定电话、不含区号固定电话
    function checkTel(tel) {
        var pattern = /(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)/;
        if (pattern.test(tel)) {
            return true;
        }
        else {
            return false;
        }

    }