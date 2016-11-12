//History Report
$('#btnSearchHistory').on("click", function () {
    modalLoad(1, "Please wait...");
    $.ajax({
        url: $('#btnSearchHistory').attr("data-url"),
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ dtfrom: $('#dtFrom').val(), dtto: $('#dtTo').val() }),
        success: function (result) {
            if (result.status) {
                modalLoad(0, "Please wait...");
                if (result.hasRecord > 0) {
                    //report head
                    $('#lblTransDate').html(result.otherData.Date);
                    $('#lblDate').html(result.otherData.runDate);
                    $('#lblPrintBy').html(result.otherData.PrintBy);
                    //report footer
                    $('#lbldebitCount').html('(' + result.otherData.TotalDebitCount + ')');
                    $('#lblDebitAmt').html('P ' + result.otherData.TotalDebit);
                    $('#lblcreditCount').html('(' + result.otherData.TotalCreditCount + ')');
                    $('#lblCreditAmt').html('P ' + result.otherData.TotalCredit);
                    $('#lblEndingBal').html('P ' + result.otherData.EndingBalance);
                    // report string details
                    $('#txtReportDetails').html(result.otherData.Date
                                                + '|' + result.otherData.runDate
                                                + '|' + result.otherData.PrintBy
                                                + '|' + result.otherData.TotalDebitCount
                                                + '|' + result.otherData.TotalDebit
                                                + '|' + result.otherData.TotalCreditCount
                                                + '|' + result.otherData.TotalCredit
                                                + '|' + result.otherData.EndingBalance);
                    // table data
                    $('#divSearchHistoryResult').html(result.historyData);
                    //hidden div
                    $('#divResultHead').show();
                    $('#divResultFooter').show();

                } else {
                    modalLoad(0, "Please wait...");
                    ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
                }
            } else {
                modalLoad(0, "Please wait...");
                ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
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
//Daily Report
$('#btnSearchDaily').on("click", function () {
    modalLoad(1, "Please wait...");
    $.ajax({
        url: $('#btnSearchDaily').attr("data-url"),
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ dtfrom: $('#dtFrom').val() }),
        success: function (result) {
            if (result.status) {
                modalLoad(0, "Please wait...");
                if (result.hasRecord > 0) {
                    // report head
                    $('#lblTransDate').html(result.otherData.Date);
                    $('#lblDate').html(result.otherData.runDate);
                    $('#lblPrintBy').html(result.otherData.PrintBy);
                    // report footer
                    $('#lblCom').html('P ' + result.otherData.TotalCommission);
                    $('#lblTotalAmt').html('P ' + result.otherData.TotalAmount);
                    // report string details
                    $('#txtReportDetails').html(result.otherData.Date
                                                + '|' + result.otherData.runDate
                                                + '|' + result.otherData.PrintBy
                                                + '|' + result.otherData.TotalCommission
                                                + '|' + result.otherData.TotalAmount);
                    // report data
                    $('#divSearchDailyResult').html(result.dailyReport);
                    // hidden div
                    $('#divResultHead').show();
                    $('#divResultFooter').show();
                    
                } else {
                    modalLoad(0, "Please wait...");
                    ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
                }
            } else {
                modalLoad(0, "Please wait...");
                ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
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
// Monthly Report
$('#btnMonthlySearch').on("click", function () {
    modalLoad(1, "Please wait...");
    $.ajax({
        url: $('#btnMonthlySearch').attr("data-url"),
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ year: $("#txtyear").val(), month: $("#drpMonth option:selected").val() }),
        success: function (result) {
            if (result.status) {
                modalLoad(0, "Please wait...");
                if (result.hasRecord > 0) {
                    // report head
                    $('#lblTransDate').html(result.otherData.Date);
                    $('#lblDate').html(result.otherData.runDate);
                    $('#lblPrintBy').html(result.otherData.PrintBy);
                    //report footer
                    $('#lbltotalCount').html('('+result.otherData.TotalCount + ')');
                    $('#lblCommission').html('P ' + result.otherData.TotalCommission);
                    $('#lblTotalAmt').html('P ' + result.otherData.TotalAmount);
                    // report string details
                    $('#txtReportDetails').html(result.otherData.Date
                                                + '|' + result.otherData.runDate
                                                + '|' + result.otherData.PrintBy
                                                + '|' + result.otherData.TotalCount
                                                + '|' + result.otherData.TotalCommission
                                                + '|' + result.otherData.TotalAmount);
                    // report data
                    $('#divSearchMonthlyResult').html(result.monthlyReport);
                    // hidden div
                    $('#divResultHead').show();
                    $('#divResultFooter').show();
                } else {
                    modalLoad(0, "Please wait...");
                    ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
                }
            } else {
                modalLoad(0, "Please wait...");
                ModalMsg("Confirmation Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Okay</button>");
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

// download Report
$('.btnPrint').on("click", function () {
    var reportType = $('#txtReportType').val();
    downloadReport(reportType);
});
// download rpt
function downloadReport(type) {
    /*
    Type 0 - History Report
    Type 1 - Daily Transaction Report
    Type 2 - Monthly Transaction Report
    Type 3 - Daily Cancel Report
    Type 4 - Monthly Cancel Report
    Type 5 - Daily Change Details (RFC) Report
    Type 6 - Monthly Change Details (RFC) Report
    */
    switch (Number(type)) {
        case 0:
            historyReport();
            break;
        case 1:
            dailyReport();
            break;
        case 2:
            monthlyReport();
            break;
        case 3:
            dailyCancelReport();
            break;
        case 4:
            monthlyCancelReport();
            break;
        case 5:
            dailyRfcReport();
            break;
        case 6:
            monthlyRfcReport();
            break;
        default:
            ModalMsg("Error Message", "Something went wrong in downloading report.", "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Close</button>");
            break;
    }
}
// History report dowload function
function historyReport() {
    modalLoad(1, "Initializing report...");

    var lblparams = $('#txtReportDetails').html();
    $.ajax({
        url: $("#divResultHead").attr("data-initial"),
        type: 'POST',
        data: $('#forRptHistory :input').serialize() + "&param=" + lblparams,
        success: function (result) {
            modalLoad(0, "Please wait...");
            if (result.status) {
                window.location = $("#divResultHead").attr("data-url") + "?type=" + 0 + "&pdfname=" + result.pdfName;
            } else {
                modalLoad(0, "Please wait...");
                ModalMsg("Error Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Close</button>");
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
// daily report dowload function
function dailyReport() {
    modalLoad(1, "Initializing report...");

    var lblparams = $('#txtReportDetails').html();
    $.ajax({
        url: $("#divResultHead").attr("data-initial"),
        type: 'POST',
        data: $('#fromRptDaily :input').serialize() + "&param=" + lblparams,
        success: function (result) {
            modalLoad(0, "Please wait...");
            if (result.status) {
                window.location = $("#divResultHead").attr("data-url") + "?type=" + 1 + "&pdfname=" + result.pdfName;
            } else {
                modalLoad(0, "Please wait...");
                ModalMsg("Error Message", result.msg, "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Close</button>");
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
// monthly report dowload function
function monthlyReport() {

}