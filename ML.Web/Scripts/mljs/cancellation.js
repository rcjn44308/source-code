// document ready
$(document).ready(function () {
    $('.row-1').show();
});

// Search Cancellation
$('#btnCancellationSearch').on("click", function () {
    var kptn = $('#txtkptn').val();
    var companyID = $('#txtcompanyID').val();
    modalLoad(1, "Please wait...");
    $.ajax({
        url: $('#btnCancellationSearch').attr("data-url"),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ kptnOrRef: kptn, CompID: companyID }),
        datatype: "json",
        success: function (result) {
            if (result.status) {
                modalLoad(0, "Please wait...");
                $('.row-1').hide();
                
                $('#divCancelDetails').html(result.modelDetails);
                $('.row-2').show();
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
// submit cancellation
$('#btnCancellationSubmit').on("click", function () {
    var submitType = $('#txtCancellationType').val();
    if (submitType == "0") {
        var kptn = $('#txtkptn').val();
        var reason = $('#reason').val();
        modalLoad(1, "Please wait...");
        $.ajax({
            url: $('#form0').attr("action"),
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ kptnOrRef: kptn, details: reason }),
            datatype: "json",
            success: function (result) {
                modalLoad(0, "Please wait...");
                if (result.status) {
                    ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" onclick=\"window.location.reload()\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
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
    } else {
        var kptn = $('#txtkptn').val();
        var reason = $('#reason').val();
        modalLoad(1, "Please wait...");
        $.ajax({
            url: $('#form0').attr("action"),
            type: 'POST',
            data: $('#form0').serialize(),
            success: function (result) {
                modalLoad(0, "Please wait...");
                if (result.status) {
                    ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" onclick=\"window.location.reload()\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
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
    }
});