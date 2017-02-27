function LoadMap()
{
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            $('#ShowLatitude').text($('#ShowLatitude').text() + position.coords.latitude);
            $('#ShowLongitude').text($('#ShowLongitude').text() + position.coords.longitude);
        });
    } else { alert("Geolocation API не поддерживается в вашем браузере") }
}