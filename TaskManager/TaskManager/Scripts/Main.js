$(document).ready(function () {
    $("#ShowMenuButton").click(ToggleMenu);
});

function ToggleMenu() {
    if ($("body").hasClass("showmenu")) {
        $("body").removeClass("showmenu");
    } else {
        $("body").addClass("showmenu");
    }
}


 $(document).ready(function () {
     $("#SubirAdjuntoAceptar").click(function () {
         $("#SubirAdjuntoAceptar").addClass("disabled");
         $("#SubirAdjuntoContainer form").submit();
     });
 });
