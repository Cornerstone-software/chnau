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
InitPage();

//窗口初始化
function InitPage(){
	var screenH = window.innerHeight|| document.documentElement.clientHeight|| document.body.clientHeight;
	var headerH=$("#header").height();
	var contentH=screenH-headerH;
	$("#leftMenu").css("minHeight",contentH);
	
}


});