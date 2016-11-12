$(document).ready(function () {
    generateReceiptData();
});

// generate print receipt
function generateReceiptData() {
    modalLoad(1, "Initializing...");
    var kptnNo = $('#kptn').val();
    jQuery.ajax({
        url: $("#divData").attr("data-url"),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ kptn: kptnNo }),
        success: function (result) {
            modalLoad(0, "Please wait...");
            if (result.status) {
                $('#lblbranch').html(result.branch);
                $('#lbldate').html(result.date);
                $('#lblpayto').html(result.paymentTo);
                $('#lblaccntName').html(result.acctName);
                $('#lblname').html(result.Name);
                $('#lbladdress').html(result.address);
                $('#lblcontact').html(result.contactNo);
                $('#lblref').html(result.BPrefNo);
                $('#lblamtpaid').html(result.PaidAmt);
                $('#lblcharge').html(result.PaidCharge);
                $('#lbltotal').html(result.totalPaid);
            } else {
                $(".my-modal").modal("hide");
                ModalMsg("Error Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
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