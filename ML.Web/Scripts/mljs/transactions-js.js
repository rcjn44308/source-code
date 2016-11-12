/*--------------------------------------------Bills Payment form steps Functions----------------------------------------------------------------------*/
// Initialize modal steps
$('#myModal').modalSteps();
// Load steps form
var form = $("#formbillspay").show();
form.steps({
    headerTag: "h3",
    bodyTag: "fieldset",
    transitionEffect: "slideLeft",

    onStepChanging: function (event, currentIndex, newIndex) {
        // Allways allow previous action even if the current form is not valid!
        if (currentIndex > newIndex) {
            return true;
        }
        if (newIndex === 1) {
            if ($('#lastname').val() == "" | $('#firstname').val() == "" | $('#contact').val() == "" | $('#address').val() == "") {
                $('#divVerifyForm').show().delay(1500).fadeOut("slow");
                return false;
            }
        }
        if (newIndex === 2) {
            if ($('#CompName').val() == "" | $('#CompAccountNo').val() == "" | $('#CompFname').val() == "" | $('#CompLname').val() == "") {
                $('#divVerifyForm').show().delay(1500).fadeOut("slow");
                return false;
            }
            getCharge();
        }
        if (newIndex === 3) {
            confirmationInfo();
        }

        // Needed in some cases if the user went back (clean up)
        if (currentIndex < newIndex) {
            // To remove error styles
            form.find(".body:eq(" + newIndex + ") label.error").remove();
            form.find(".body:eq(" + newIndex + ") .error").removeClass("error");
        }
        form.validate().settings.ignore = ":disabled,:hidden";
        return form.valid();
    },
    onFinishing: function (event, currentIndex) {
        form.validate().settings.ignore = ":disabled";
        return form.valid();
    },
    onFinished: function (event, currentIndex) {
        onSubmitBillsPay();
    }
}).validate({
    errorPlacement: function errorPlacement(error, element) { element.before(error); },
    rules: {
        confirm: {
            equalTo: "#password-2"
        }
    }
});
/*--------------------------------------------Windows Ready----------------------------------------------------------------------*/
$(document).ready(function () {
    SetPayerInfo();
})

// Set payer data
function SetPayerInfo() {
    var payerSession = GetSession("SessionPayer");
    if (payerSession != null) {
        var decryptSession = DecryptSession(payerSession);
        var obj = $.parseJSON(decryptSession);

        $('#payerID').val(obj.CustID);
        $('#lastname').val(obj.Lname);
        $('#firstname').val(obj.Fname);
        $('#middlename').val(obj.Mname);
        $('#contact').val(obj.Contact);
        $('#address').val(obj.Address);
    }

}

// select payer
function SelectPayer(input) {
    if (input != null | input != "") {
        $('.modal').modal("hide");

        SetSessionPayer(input);
        SetPayerInfo();

        $('#txtsLname').val('');
        $('#txtsFname').val('');
        $('#txtsMname').val('');
    }
}

// Add Company
function AddCompany() {
    //Set data
    //divVadidateAddAccount
    var compID = $('#txtcompanyID').val();
    var compName = $('#txtCompanyName').val();
    var CompAccount = $('#txtAccountNo').val();
    var compFname = $('#txtFname').val();
    var compLname = $('#txtLname').val();
    if (compID == "" | compName == "" | CompAccount == "" | compFname == "" | compLname == "") {
        $("#divVadidateAddAccount").show().delay(1000).fadeOut("slow"); return false;
    }

    $('#CompID').val(compID);
    $('#CompName').val(compName);
    $('#CompAccountNo').val(CompAccount);
    $('#CompFname').val(compFname);
    $('#CompLname').val(compLname);
    $('#CompMname').val($('#txtMname').val());
    // Clear data
    $('#txtcompanyID').val('')
    $('#txtCompanyName').val('')
    $('#txtAccountNo').val('')
    $('#txtFname').val('')
    $('#txtLname').val('')
    $('#txtMname').val('')

    $('#modalAddAccount').modal('hide');
    $(".accountverification").show();
}
// Select Company
function selectCompany(id,name,acctnum,fname,lname,mname) {
    $('#CompID').val(id);
    $('#CompName').val(name);
    $('#CompAccountNo').val(acctnum);
    $('#CompFname').val(fname);
    $('#CompLname').val(lname);
    $('#CompMname').val(mname);
    $('.enumerablemodal').modal("hide");
}
// Confirmation info
function confirmationInfo() {
    $('#txtCcompany').val($('#CompName').val());
    $('#txtCaccountName').val($('#CompLname').val() + " " + $('#CompFname').val() + " " + $('#CompMname').val());
    $('#txtCaccountNo').val($('#CompAccountNo').val());
    $('#txtCpayer').val($('#lastname').val() + " " + $('#firstname').val() + " " + $('#middlename').val());
    $('#txtCamountpaid').val($('#amountPaid').val());
    $('#txtCcharge').val($('#charge').val());

    var total = Number($('#amountPaid').val()) + Number($('#charge').val());
    $('#txtCtotal').val(total);
}

/*--------------------------------------------Navigate button Functions----------------------------------------------------------------------*/
$('#btnAddPayor').on("click", function () {
    $('#modalSearchPayor').modal("hide");
    $('#loadmsg').html("Please wait...");
    $('.my-modal').modal("show");
    window.location = $('#btnAddPayor').attr("data-url");
});

/*--------------------------------------------Transactional Functions----------------------------------------------------------------------*/
//search Payer
$('#btnSearchPayer').on("click", function () {
    var fname = $('#txtsFname').val();
    var lname = $('#txtsLname').val();
    var mname = $('#txtsMname').val();

    if (fname == "" | fname == null) {
        $("#divSearchVadidate").show().delay(1000).fadeOut("slow"); return false;
    }
    if (lname == "" | lname == null) {
        $("#divSearchVadidate").show().delay(1000).fadeOut("slow"); return false;
    }
    modalLoad(1, "Please wait...");

    $.ajax({
        url: $('#btnSearchPayer').attr("data-url"),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Fname: fname, Lname: lname, Mname: mname }),
        datatype: "json",
        success: function (result) {
            if (result.status) {
                modalLoad(0, "Please wait...");

                $('#resulthead').html("Search Result");
                $('#modalDivMsg').html(result.ListOfPayer);
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
// View Recent Transactions
$('#btnSearchCompany').on("click", function () {
    modalLoad(1, "Please wait...");

    var customerID = $('#payerID').val();
   
    $.ajax({
        url: $('#btnSearchCompany').attr("data-url"),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ custID: customerID }),
        success: function (result) {
            modalLoad(0, "Please wait...");
            if (result.status) {
                $('#resulthead').html("Recent Transaction Company/s");
                $('#modalDivMsg').html(result.ListOfRecentTrans);
                $('.enumerablemodal').modal("show");
                $('#btnenumerablemodal').html("<button type=\"button\" class=\"btn btn-default pull-left\" data-dismiss=\"modal\">Close</button><button type=\"button\" class=\"btn btn-success\" data-toggle=\"modal\" data-target=\"#modalAddAccount\" data-dismiss=\"modal\"><i class=\"glyphicon glyphicon-plus\"></i> Add Company</button>");
            } else {
                modalLoad(0, "Please wait...");
                $('#divDialogEnumModal').removeClass("modal-lg");

                $('#resulthead').html("Recent Transaction Company/s");
                $('#modalDivMsg').html("<h5 style=\"color: #cc0001;text-align:center\"><b>No Recent Transaction!</b></h5>");
                $('.enumerablemodal').modal("show");
                $('#btnenumerablemodal').html("<button type=\"button\" class=\"btn btn-default pull-left\" data-dismiss=\"modal\">Close</button><button type=\"button\" class=\"btn btn-success\" data-toggle=\"modal\" data-target=\"#modalAddAccount\" data-dismiss=\"modal\"><i class=\"glyphicon glyphicon-plus\"></i> Add Company</button>");
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

// Get Charge
function getCharge() {
    var accountID = $('#CompID').val();
    var Currency = $("#currency option:selected").text();
    $('.getcharge').show();
    $.ajax({
        url: $('#divCharge').attr("data-url"),
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ acctID: accountID, Curr: Currency }),
        success: function (result) {
            if (result.status) {
                $('.getcharge').hide();
                $('#charge').val(result.charge);
            } else {
                $('.getcharge').hide();
                $('#charge').val('0');
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

// Submit Bills Payment
function onSubmitBillsPay() {
    $(".my-modal").modal("show");
    printUrl = $('#printUrl').attr('data-url');
    jQuery.ajax({
        url: $("#formbillspay").attr("action"),
        type: 'POST',
        data: $('#formbillspay').serialize(),
        success: function (result) {
            if (result.status) {
                RemoveSession("SessionPayer");
                $(".my-modal").modal("hide");
                ModalMsg("Confirmation Message",
                        "<b style=\"color:#267513;\">"
                        + result.msg
                        + "</b>"
                        + "<br/>"
                        + "<b>"
                        + "KPTN : "
                        + result.kptn
                        + "</b>",
                        "<button type=\"button\" onclick=\"location.href='" + printUrl + "?kptn=" + result.kptn + "'\" class=\"btn btn-success\" data-dismiss=\"modal\"><i class=\"glyphicon glyphicon-ok\"></i> Okay</button>");
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