﻿var sSecs = 0; var sms = -1; var closeTimer = false; function openL(msg) { sSecs = 0; sms = -1; var divbg = $("<div class='background-alert-max'></div>"); if (msg != null && msg != '' && msg != 'undefined') { divbg = $("<div class='background-alert-max'><div class='alert alert-warning alert-load'>" + msg + "</div></div>"); } $('#msg-display').append(divbg); closeTimer = false; initTimer(); } function closeL() { closeTimer = true; $('.background-alert-max').remove(); window.scrollTo(0, 0);} function initTimer() { return; sms++; if (sms == 1000) { sms = 0; sSecs++; } if (sms <= 9) sms = "0" + sms; $("#clock1").html(sSecs + "," + sms + " seg"); if (!closeTimer) { setTimeout('initTimer()', 1); } } function validate(evt) { var charCode = (evt.which) ? evt.which : evt.keyCode; if (charCode != 46 && charCode > 31 && (charCode != 44) && (charCode < 48 || charCode > 57)) return false; return true; } $(document).ready(function () { $(".container-body").css("min-height", Math.round($(window).height() * 0.75) + "px"); $("#openMenu").on("click", openMenuClick); }); $("#menuAdmin a").on("click", function () { $("#openMenu").find("span").text(""); openMenuClick(); }); openMenuClick = function () { $("#menuAdmin").toggle(); if ($("#openMenu").attr("estaAberto") === "true") { $("#openMenu").css("left", "0em"); $("#openMenu").find("span").text("Menu"); } else { $("#openMenu").css("left", "15em"); $("#openMenu").find("span").text(""); } $("#openMenu").attr("estaAberto", $("#openMenu").attr("estaAberto") === "false"); }
function RemoverLoja() {
    var loja = sessionStorage.getItem("lojaNome_graxei", "");
    $(".label-search").css("visibility", "hidden");
    sessionStorage.setItem("lojaNome_graxei", "");
    var q = $("#q").val();
    q = q.toLowerCase().replace("loja:" + loja, "");
    $("#q").val(q);
    $("#lojaNome").val("");

    $("#fq").submit();
 
}