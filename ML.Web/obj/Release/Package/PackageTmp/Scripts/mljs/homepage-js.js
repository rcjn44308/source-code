$('#btnChangePass').on("click", function () {
    //alert(GetSession("987"));
    SetSession("987", "Sample,Sample,Sample,Sample");
    alert("Done!");
});
$('#btnChangePass1').on("click", function () {
    //alert(GetSession("987"));
    alert(GetSession("987"));
});
