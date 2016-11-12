$(document).ready(function(){
    window.history.forward(-1);
})

$(".dropdown-menu li a").click(function () {
    var selText = $(this).text();
    $(this).parents('.input-group-btn').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
});






