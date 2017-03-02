$(function () {    
    $('#chatBody').hide();
    $('#loginBlock').show();    
    var chat = $.connection.chatHub;      
    chat.client.addMessage = function (name, message) {        
        $('#chatroom').append('<p><b>' + htmlEncode(name)
            + '</b>: ' + htmlEncode(message) + '</p>');
    };
    chat.client.setId = function (id) {
        SetMyIdCookies(id);
        $('#ShowMyId').text($('#ShowMyId').text() + id);
    };
    chat.client.setInterlocutorId = function (id) {
        SetInterlocutorsIdCookies(id);
        $('#InterlocutorsId').val(id);
        RemoteId = id;
        $('#ShowInterlocutorsId').text($('#ShowInterlocutorsId').text() + id);
    };
    chat.client.showChat= function()
    {
        addEventListener("keypress", SendByEnter);
        $('#loginBlock').hide();
        $('#Loading').attr("style", "visibility:hidden");
        $('#chatBody').attr("style", "visibility:visible");        

    }    
    chat.client.onConnected = function (id, allUsers) { 
        $('#hdId').val(id);                  
        for (i = 0; i < allUsers.length; i++) {
            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }    
    chat.client.onNewUserConnected = function (id) {
        AddUser(id);
    }    
    chat.client.onUserDisconnected = function (id, userName) {
        $('#' + id).remove();
    }
    function SendByEnter(e) {
        if (e.keyCode == 13) {
            chat.server.sendTo($('#InterlocutorsId').val(), $('#hdId').val(), $('#message').val());
            $('#message').val('');
        }
    }    
    $.connection.hub.start().done(function () {               
        LoadMap();          
        getUserMedia_starts();
        chat.server.connect();
        $('#sendmessage').click(function () {                        
            chat.server.sendTo($('#InterlocutorsId').val(), $('#hdId').val(), $('#message').val());
                $('#message').val('');          
        });             
        $('#btnSearch').click(function () {           
            $('#Loading').attr("style", "visibility:visible");
            if ($('#btnSearch').attr("style") != "visibility:hidden") {
                var IM;
                var SM;
                if ($('#IM').prop("checked") == true) {
                    IM = true;
                }
                else { IM = false; }
                if ($('#SM').prop("checked") == true) {
                    SM = true;
                }
                else { SM = false; }                
                chat.server.startSearch(IM, SM, $('#latitude').attr("value"), $('#longitude').attr("value"))
            }           
            $('#btnSearch').attr("style", "visibility:hidden");
        });     
    });
});
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;   
}
function AddUser(id, name) {
    var userId = $('#hdId').val();
    if (userId != id) {
        $("#chatusers").append('<p id="' + id + '"><b>' + userId + '</b></p>');
        $('#listboxusers').append('<option>'+userId+'</option>')
    }
}
function SetMyIdCookies(id)
{
    document.cookie = "MyId=" + id;
}
function SetInterlocutorsIdCookies(id)
{
    document.cookie = "InterlocutorsId=" + id;
}
function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}
