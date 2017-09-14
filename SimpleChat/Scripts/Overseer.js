 function CheckRegistration () {
    var ModelHere = getCookie("ModelHere");
    var vkid = getCookie("vkid");
    var vkidHere;
    if (getCookie("vkid") != undefined) {
        vkidHere = true;
    }
    else {
        vkidHere = false;
    }
    if (vkidHere == true || ModelHere == "true") {

    }
    else {
        var url = $("#RedirectToRegistration").val();
        location.href = url;
    }
 }
 document.addEventListener("DOMContentLoaded", CheckRegistration);