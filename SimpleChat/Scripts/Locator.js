window.onload = function ()
{
    var Lat = getCookie("Latitude");
    var Lon = getCookie("Longitude");
    //alert(getCookie("Latitude"))
    if (Lat == "undefined" || Lon == "undefined" || Lat == undefined || Lon == undefined)
    {
        AskCoords();
    }
}
function AskCoords() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            //$('#ShowLatitude').text($('#ShowLatitude').text() + position.coords.latitude);
            //$('#ShowLongitude').text($('#ShowLongitude').text() + position.coords.longitude);
            //$('#longitude').attr("value", position.coords.longitude);
            //$('#latitude').attr("value", position.coords.latitude);
            //$.ajax({
            //    url: "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + position.coords.latitude + ","+position.coords.longitude+"&key=AIzaSyCmed0Ytfp2HTsbjIV-criJTifdo73wZxk",
            //    success: function (data) {
            //        alert("Прибыли данные: " + data);
            //    }
            //});
            WriteCookie("Latitude", position.coords.latitude);
            WriteCookie("Longitude", position.coords.longitude);
        });
    } else { alert("Geolocation API не поддерживается в вашем браузере") }
}