/// <reference path="jquery.1.11.1.js" />

$(function () {

  var showSideBar = window.localStorage.getItem("show-side-bar") == "1";
  if (showSideBar) {
    $("#menu").show();
  }
});

$("#btn-left-nav-toggle").click(function (e) {
  switchMenu();

})

function isMenuVisible() {
  return $("#menu").is(':visible');
}
function switchMenu(cmd) {
  var visible = cmd == "show" ? false : cmd == "hide" ? true : isMenuVisible();
  if (visible) {
    $("#menu").css("width", "0").delay(200).fadeOut();
  }
  else {
    $("#menu").fadeIn().css("width", "40vw");
  }
  
  window.localStorage.setItem("show-side-bar", visible ? "0" : "1");
}

$(document).click(function (event) {
  event.stopPropagation();
  var _avoidTar = $('#sitebar-page');  // 设置目标区域
  if (!_avoidTar.is(event.target) && _avoidTar.has(event.target).length === 0) {
    //$("#menu").slideUp('fast');
    switchMenu('hide');

    window.localStorage.setItem("show-side-bar", "0");
  }


});

