﻿var sSecs = 0;
var sms = -1;
var closeTimer = false;

function openL(msg) {
    sSecs = 0;
    sms = -1;

    var divbg = $("<div class='background-alert-max'></div>");
    if (msg != null && msg != '' && msg != 'undefined' ) {
        divbg = $("<div class='background-alert-max'><div class='alert alert-warning alert-load'>" + msg + "</div></div>");

    }
    $('#msg-display').append(divbg);

    closeTimer = false;
    initTimer();
}

function closeL() {
    closeTimer = true;
    $('.background-alert-max').remove();

}

function initTimer() {
    return;
    sms++;
    if (sms == 1000) {
        sms = 0; sSecs++;
    }
    if (sms <= 9) sms = "0" + sms;
    $("#clock1").html(sSecs + "," + sms + " seg");

    if (!closeTimer) {
        setTimeout('initTimer()', 1);
    }
}

