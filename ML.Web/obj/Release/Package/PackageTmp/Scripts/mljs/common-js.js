// show loading info every navigate
$(document).ready(function () {
    $.fn.checknet();
    //checknet.config.checkInterval = 10000;
    //checknet.config.warnMsg = "There is no connection!!";

    $('.navload a').click(function (e) {
        $('#loadmsg').html("Please wait...");
        $('.my-modal').modal("show");
    });
});

// Monitor User Popup in 5 mins inactivity
startTimerp();
var cp = 0; max_countp = 295; logoutp = true;
function startTimerp() {
    setTimeout(function () {
        logoutp = logoutp;
        c = cp;
        max_countp = 295;
        //$('#timerp').html(max_countp);
        startCountp();

    }, 10000);
}
function timedCountp() {
    cp = cp + 1;
    remaining_timep = max_countp - cp;
    if (remaining_timep == 0 && logoutp) {
        startTimer();
    } else {
        //$('#timerp').html(remaining_timep);
        tp = setTimeout(function () { timedCountp() }, 1000);
    }
}
function startCountp() {
    timedCountp();
}
$(this).mousemove(function (e) {
    max_countp = 295;
    cp = 0;
    logoutp = true;
});
$(this).keypress(function (e) {
    max_countp = 295;
    cp = 0;
    logoutp = true;
});

// logged out in 1mn after warning.
var c = 0; max_count = 60; logout = true;
function startTimer() {
    setTimeout(function () {
        logout = logout;
        c = c;
        max_count = 60;

        $('#timer').html(max_count);
        $(".modal-inactivity").modal("show");

        startCount();

    }, 10000);
}
function timedCount() {
    c = c + 1;
    remaining_time = max_count - c;
    if (remaining_time == 0 && logout) {
        $('#logout_popup').modal('hide');
        window.location = $(".btnLogout").attr("data-for-desktop-action");
        $(".btnLogout").click();

    } else {
        $('#timer').html(remaining_time);
        t = setTimeout(function () { timedCount() }, 1000);
    }
}
function startCount() {
    timedCount();
}

// Set Session
function SetSession(key, value) {
    $.session.set(key, value);
}
// delete session
function RemoveSession(key) {
    $.session.remove(key);
}
// Clear all session
function ClearAllSession() {
    $.session.clear();
}
// Get Session
function GetSession(key) {
    return $.session.get(key);
}