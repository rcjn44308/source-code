function onSubmitBegin() {
    $('#loadmsg').html("Please wait...");
    $('.my-modal').modal("show");
}
function onSubmitSuccess(result) {
    if (result.errCode == 0) {
        $(".my-modal").modal("hide");
        $(".wiggle").effect("shake");
        $("#errorMSG").html(result.msg);
        $(".error-catch").modal("show");
    } else if (result.errCode == 1) {
        $(".my-modal").modal("hide");
        $("#lockedMSG").html(result.msg);
        $(".locked").modal("show");
    } else {
        window.location = result.action;
    }
}
function onFail(jqXHR, exception) {
    $(".my-modal").modal("hide");
    $(".locked").modal("hide");
    var errmsg = '';
    if (jqXHR.status === 0) {
        errmsg = 'Not connect.\n Verify Network.';
        $("#errorMSG").html(errmsg);
    } else if (jqXHR.status == 404) {
        errmsg = 'Requested page not found. [404]';
        $("#errorMSG").html(errmsg);
    } else if (jqXHR.status == 500) {
        errmsg = 'Internal Server Error [500].';
        $("#errorMSG").html(errmsg);
    } else if (exception === 'parsererror') {
        errmsg = 'Requested JSON parse failed.'; 
        $("#errorMSG").html(errmsg);
    } else if (exception === 'timeout') {
        errmsg = 'Time out error.';
        $("#errorMSG").html(errmsg);
    } else if (exception === 'abort') {
        errmsg = 'Ajax request aborted.';
        $("#errorMSG").html(errmsg);
    } else {
        errmsg = 'Uncaught Error.\n' + jqXHR.responseText;
        $("#errorMSG").html(errmsg);
    }
    $(".error-catch").modal("show");
}

$("#loginpass").keyup(function (event) {
    if (event.keyCode == 13) {
        $(".btnlogin").click();
    }
});

$("#btnDownloadUpdates").on("click", function () {
    $(".Download-Updates").modal("hide");
    window.location = $("#btnDownloadUpdates").attr("data-action");
})

$(document).ready(function () {
    $('.check-version').show();
    $('#spanUpdate').hide();
    var Hrefurl = $("#spanUpdate").attr("data-action");
    var dvID = $('#uqid').val();
    var Vrs = $('#uqvs').val();
    jQuery.ajax({
        url: Hrefurl,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ q: dvID, q1 : Vrs }),
        success: function (result) {
            if (result.errCode == 2000) {
                $(".my-modal").modal("hide");
                var item = result.msg.toString().split(',').join('<br>');
                $("#errorMSG").html(item);
                $(".error-catch").modal("show");
                $('.check-version').hide();
            } else {
                $("#latestVersion").html(result.respVal.latestVersion);
                $('.check-version').hide();
                $('#spanUpdate').show();
                
                for (var i in result.respVal.features) {
                    $('#vsDetails').append($("<li class=\"text-capitalize\"></li>").html(result.respVal.features[i]));
                }
                $('#spanUpdate').html(result.msg);
            }
        },
        error: function (jqXHR, exception) {
            $(".my-modal").modal("hide");
            var errmsg = '';
            if (jqXHR.status === 0) {
                errmsg = 'Not connect.\n Verify Network.';
                $("#errorMSG").html(errmsg);
            } else if (jqXHR.status == 404) {
                errmsg = 'Requested page not found. [404]';
                $("#errorMSG").html(errmsg);
            } else if (jqXHR.status == 500) {
                errmsg = 'Internal Server Error [500].';
                $("#errorMSG").html(errmsg);
            } else if (exception === 'parsererror') {
                errmsg = 'Requested JSON parse failed.';
                $("#errorMSG").html(errmsg);
            } else if (exception === 'timeout') {
                errmsg = 'Time out error.';
                $("#errorMSG").html(errmsg);
            } else if (exception === 'abort') {
                errmsg = 'Ajax request aborted.';
                $("#errorMSG").html(errmsg);
            } else {
                errmsg = 'Uncaught Error.\n' + jqXHR.responseText;
                $("#errorMSG").html(errmsg);
            }
            $(".error-catch").modal("show");
        }
    });
})

$(document).ready(function () {
    $("#drop-1").click(function () {
        var check = $(this).is(":checked");
        if (check) {
            $(".gly-sign").html("<i class=\"glyphicon glyphicon-chevron-up\"> </i>");
        }
        else {
            $(".gly-sign").html("<i class=\"glyphicon glyphicon-chevron-down\"> </i>");
        }
    });
});
