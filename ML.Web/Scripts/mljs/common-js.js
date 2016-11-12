/*-----------------------------------------Document Ready-------------------------------------------------------------------------*/
// show loading info every navigate
$(document).ready(function () {
    $.fn.checknet();
    //checknet.config.checkInterval = 10000;
    //checknet.config.warnMsg = "There is no connection!!";

    $('.navload a').click(function (e) {
        modalLoad(1,"Please wait..");
    });
    $('.linkbutton a').click(function (e) {
        $('.modal').modal("hide");
        modalLoad(1, "Please wait..");
    });

    RefreshRunningBalance();
});
/*------------------------------------------Timer------------------------------------------------------------------------*/
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
        $('#divInactivity').show();
        $('#divLogout').hide();
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
/*----------------------------------------Session--------------------------------------------------------------------------*/
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
//Encrypt Session
function EncryptSession(jsonString) {
    var encode = btoa(jsonString);
    return encode;
}
//Decrypt Session
function DecryptSession(jsonString) {
    var decode = atob(jsonString);
    return decode;
}
/*----------------------------------------function Set Session--------------------------------------------------------------------------*/
function SetSessionPayer(stringPayer) {
    var stringArray = stringPayer.split("|");
    var text = '{"CustID":"' + stringArray[0] + '","Fname":"' + stringArray[1] + '","Lname":"' + stringArray[2] + '","Mname":"' + stringArray[3] + '","Contact":"' + stringArray[4] + '","Address":"' + stringArray[5] + '"}';
    var jsonEncrypt = EncryptSession(text);
    SetSession("SessionPayer", jsonEncrypt);
}

/*--------------------------------------------General Button Functions----------------------------------------------------------------------*/
$('.btnLogout').on("click", function () {
    modalLoad(1, "Please wait..");
    ClearAllSession();
    window.location = $(".btnLogout").attr("data-action");
});
$('.logoutLink a').on("click", function () {
    $('#divLogout').show();
    $('#divInactivity').hide();
    $(".modal-inactivity").modal("show");
});
/*--------------------------------------------General Functions----------------------------------------------------------------------*/
function modalLoad(type,msg) {
    switch (type) {
        case 1:
            $('#loadmsg').html(msg);
            $('.my-modal').modal("show");
            break;
        default:
            $('.my-modal').modal("hide");
    }
}
// Modal message
function ModalMsg(header,msg,button) {
    $('#msgHead').html(header);
    $('#msgBody').html(msg);
    $('.modalmsg').modal("show");
    $('#msgButtons').html(button);
}

// select company
function selectCompanyName(name, id) {
    $('#txtcompanyID').val(id);
    $('#txtCompanyName').val(name);
    $('.enumerablemodal').modal("hide");
}

// funtion refresh balance
function RefreshRunningBalance() {
    $('#bRefreshBal').show();
    $.ajax({
        url: $('#divRefreshAccount').attr("data-url"),
        type: 'GET',
        success: function (result) {
            if (result.status) {
                $('#bRefreshBal').hide();
                $('#bRunningBalance').html("P " + result.RunBal);
                $('#bRunningBalance').show();
            } else {
                $('#bRefreshBal').hide();
                $('#bRunningBalance').html("Something went wrong.").show();
                $('#bRunningBalance').show();
            }
        },
        error: function (jqXHR, exception) {
            modalLoad(0, "Please wait...");
            var btn = "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>";
            if (jqXHR.status === 0) {
                ModalMsg("Error Message", "Not connect.\n Verify Network.", btn);
            } else if (jqXHR.status == 404) {
                ModalMsg("Error Message", "Requested page not found. [404]", btn);
            } else if (jqXHR.status == 500) {
                ModalMsg("Error Message", "Internal Server Error [500].", btn);
            } else if (exception === 'parsererror') {
                ModalMsg("Error Message", "Requested JSON parse failed.", btn);
            } else if (exception === 'timeout') {
                ModalMsg("Error Message", "Time out error.", btn);
            } else if (exception === 'abort') {
                ModalMsg("Error Message", "Ajax request aborted.", btn);
            } else {
                var errmsg = 'Uncaught Error.\n' + jqXHR.responseText;
                ModalMsg("Error Message", errmsg, btn);
            }
        }
    });
}

// Search Company
$('#btnViewCompanies').on("click", function () {
    modalLoad(1, "Please wait...");
    $.ajax({
        url: $('#btnViewCompanies').attr("data-url"),
        type: 'GET',
        success: function (result) {
            if (result.status) {
                modalLoad(0, "Please wait...");
                $('#divDialogEnumModal').removeClass("modal-lg");

                $('#resulthead').html("Search Result");
                $('#modalDivMsg').html(result.ListofCompany);
                $('.enumerablemodal').modal("show");
                $('#btnenumerablemodal').html("<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Close</button>");
            } else {
                modalLoad(0, "Please wait...");
                ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Close</button>");
            }
        },
        error: function (jqXHR, exception) {
            modalLoad(0, "Please wait...");
            var btn = "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>";
            if (jqXHR.status === 0) {
                ModalMsg("Error Message", "Not connect.\n Verify Network.", btn);
            } else if (jqXHR.status == 404) {
                ModalMsg("Error Message", "Requested page not found. [404]", btn);
            } else if (jqXHR.status == 500) {
                ModalMsg("Error Message", "Internal Server Error [500].", btn);
            } else if (exception === 'parsererror') {
                ModalMsg("Error Message", "Requested JSON parse failed.", btn);
            } else if (exception === 'timeout') {
                ModalMsg("Error Message", "Time out error.", btn);
            } else if (exception === 'abort') {
                ModalMsg("Error Message", "Ajax request aborted.", btn);
            } else {
                var errmsg = 'Uncaught Error.\n' + jqXHR.responseText;
                ModalMsg("Error Message", errmsg, btn);
            }
        }
    });
});
//Update Ml express User
$("#btnUserUpdatePass").on("click", function () {
    var oldPass = $('#txtcurpass').val();
    var curPass = $('#txtnewpass').val();
    var confirm = $('#txtconfrmpass').val();
    if (oldPass == "" | curPass == "" | confirm == "") {
        $('#divCpValidate').show().delay(1000).fadeOut("slow"); return false;
    }
    $("#loadingImg").show();
    jQuery.ajax({
        url: $("#btnUserUpdatePass").attr("data-action"),
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ oldPass: oldPass, newPass: curPass, confirm: confirm }),
        success: function (result) {
            if (result.status) {
                $("#loadingImg").hide();
                ModalMsg("Confirmation Message", result.msg, "");
            } else {
                $("#loadingImg").hide();
                ModalMsg("Error Message", result.msg , "");
            }
        },
        error: function (jqXHR, exception) {
            modalLoad(0, "Please wait...");
            var btn = "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>";
            if (jqXHR.status === 0) {
                ModalMsg("Error Message", "Not connect.\n Verify Network.", btn);
            } else if (jqXHR.status == 404) {
                ModalMsg("Error Message", "Requested page not found. [404]", btn);
            } else if (jqXHR.status == 500) {
                ModalMsg("Error Message", "Internal Server Error [500].", btn);
            } else if (exception === 'parsererror') {
                ModalMsg("Error Message", "Requested JSON parse failed.", btn);
            } else if (exception === 'timeout') {
                ModalMsg("Error Message", "Time out error.", btn);
            } else if (exception === 'abort') {
                ModalMsg("Error Message", "Ajax request aborted.", btn);
            } else {
                var errmsg = 'Uncaught Error.\n' + jqXHR.responseText;
                ModalMsg("Error Message", errmsg, btn);
            }
        }
    });
})