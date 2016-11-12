//--------------------------------------------------------------------Form Kyc Load----------------------------
// Load steps form
var form = $("#formkyc").show();
form.steps({
    headerTag: "h3",
    bodyTag: "fieldset",
    transitionEffect: "slideLeft",

    onStepChanging: function (event, currentIndex, newIndex) {
        // Allways allow previous action even if the current form is not valid!
        if (currentIndex > newIndex) {
            return true;
        }
        // Forbid next action on "Warning" step if the user is to young
        if (newIndex === 3 && Number($("#age-2").val()) < 18) {
            return false;
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
        submitKyc();
    }
}).validate({
    errorPlacement: function errorPlacement(error, element) { element.before(error); },
    rules: {
        confirm: {
            equalTo: "#password-2"
        }
    }
});
//--------------------------------------------------------------------Submit KYC----------------------------
function submitKyc() {
    modalLoad(1, "Please wait...");
    var btnLik = $('#billspaylink').attr("href");
    var postUrl = '';
    var isUpdate = $('#isUpdate').val();
    if (isUpdate == "1"){
        postUrl = $('#divUdateUrl').attr("data-url");
    }    
    else {
        postUrl = $("#formkyc").attr("action");
    }

    $.ajax({
        url: postUrl,
        type: 'POST',
        data: $("#formkyc").serialize(),
        success: function (result) {
            if (result.status) {
                modalLoad(0, "Please wait...");
                var jsonResult = JSON.stringify(result.PayorData);
                var obj = $.parseJSON(jsonResult);
                var inputData = obj.payorID + "|" + obj.payorFname + "|" + obj.payorLname + "|" + obj.payorMname + "|" + obj.payorContact + "|" + obj.payorAddress;
                SetSessionPayer(inputData);
                ModalMsg("Confirmation Message", result.msg, "<button onclick=\"window.location.href='"+ btnLik +"'\" type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
            } else {
                modalLoad(0, "Please wait...");
                ModalMsg("Error Message", result.msg, "<button onclick=\"window.location.href='" + btnLik + "'\" type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
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