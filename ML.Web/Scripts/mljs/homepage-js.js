$(document).ready(function () {
    generateUserInf();
})

function generateUserInf() {
    $('.pleaseWait').show();
    $.ajax({
        url: $('#divUrl').attr("data-url"),
        type: 'GET',
        success: function (result) {
            $('.pleaseWait').hide();
            if (result.status) {
                if (result.hasImage) {
                    $('#divUserImage').html('<img  style="height: 218px;border-radius: 100%;" alt="profile pict" src="data:image/png;base64,' + result.response.Photo + '" />');
                } else {
                    $('#imgNo').show();
                }
                $('#lblFullname').html(result.response.Name);
                $('#lblAddress').html(result.response.Address);
                $('#lblEmail').html(result.response.EmailAdd);
            } else {
                $('#lblFullname').html("Something went wrong.");
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


$('#btnChangePass').on("click", function () {
   
});
//function _calculateAge(birthday) { // birthday is a date
//    var ageDifMs = Date.now() - birthday.getTime();
//    var ageDate = new Date(ageDifMs); // miliseconds from epoch
//    return Math.abs(ageDate.getUTCFullYear() - 1970);
//}
