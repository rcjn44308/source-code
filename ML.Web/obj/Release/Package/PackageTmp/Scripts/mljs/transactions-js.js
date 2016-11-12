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

// Submit Bills Payment
function onSubmitBillsPay() {
    $(".my-modal").modal("show");
    jQuery.ajax({
        url: $("#formbillspay").attr("action"),
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ sample: "asdf" }),
        success: function (result) {
            $(".my-modal").modal("hide");
            alert(result);
        },
        error: function (jqXHR, exception) {
            $(".my-modal").modal("hide");
            var errmsg = '';
            if (jqXHR.status === 0) {
                errmsg = 'Not connect.\n Verify Network.';
                $("#kptnMSG").html(errmsg);
            } else if (jqXHR.status == 404) {
                errmsg = 'Requested page not found. [404]';
                $("#kptnMSG").html(errmsg);
            } else if (jqXHR.status == 500) {
                errmsg = 'Internal Server Error [500].';
                $("#kptnMSG").html(errmsg);
            } else if (exception === 'parsererror') {
                errmsg = 'Requested JSON parse failed.';
                $("#kptnMSG").html(errmsg);
            } else if (exception === 'timeout') {
                errmsg = 'Time out error.';
                $("#kptnMSG").html(errmsg);
            } else if (exception === 'abort') {
                errmsg = 'Ajax request aborted.';
                $("#kptnMSG").html(errmsg);
            } else {
                errmsg = 'Uncaught Error.\n' + jqXHR.responseText;
                $("#kptnMSG").html(errmsg);
            }
            $(".Kptn-Not-Found").modal("show");
        }
    });
}