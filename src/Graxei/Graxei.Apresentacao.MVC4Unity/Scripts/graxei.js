var sSecs = 0;
var sms = -1;
var closeTimer = false;

function openL() {
    sSecs = 0;
    sms = -1;
    var divbg = $("<div class='background-alert-max'></div>");
    $('#msg-display').append(divbg);

    closeTimer = false;
    initTimer();
}

function closeL() {
    closeTimer = true;
    $('.background-alert-max').remove();
    
}

function initTimer() {
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

