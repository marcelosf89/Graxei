var sSecs = 0;
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


function validate(e) {
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        // Allow: Ctrl+A
                (e.keyCode == 65 && e.ctrlKey === true) ||
        // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
}

