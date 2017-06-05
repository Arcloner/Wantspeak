$(function () {    
    $('#chatBody').hide();
    $('#loginBlock').show();    
    var chat = $.connection.chatHub;      
    chat.client.addMessage = function (name, message) {        
        $('#chatroom').append('<p><b>' + htmlEncode(name)
            + '</b>: ' + htmlEncode(message) + '</p>');
        var height = document.getElementById('chatroom').scrollHeight;
        $("#chatroom").animate({"scrollTop":height},"fast");
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
    chat.client.StartConnection = function ()
    {
        StartWebRTC();
    }
    chat.client.AnotherDisconnect = function ()
    {
        $('#loginBlock').show();
        $('#chatBody').attr("style", "visibility:hidden");
        $('#btnSearch').attr("style", "visibility:visible");        
        $('#ShowInterlocutorsId').text("Связан с :");
        document.getElementById('chatroom').innerHTML = '';
    }
    chat.client.showChat= function()
    {
        addEventListener("keypress", SendByEnter);
        $('#loginBlock').hide();
        $('#Loading').attr("style", "visibility:hidden");
        //$('#chatBody').attr("style", "visibility:visible");
        $('#NewChat').attr("style", "visibility:visible");
    }    
    chat.client.showCallNotification = function ()
    {
        $('#CallNotification').attr("style", "visibility:visible");
        $('#AnswerLoading').attr("style", "visibility:hidden");
        $('#CallButton').attr("style", "visibility:hidden");
        CallSound();
    }    
    chat.client.onCall = function ()
    {
        $('#OnCall').attr("style", "visibility:visible");
        $('#AnswerLoading').attr("style", "visibility: visible;z-index: 1;position: relative;margin-right: 0;");
        $('#CallButton').attr("style", "visibility:hidden");
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
    chat.client.callIs = function (status)
    {
        if (status == true) {
            $('#CallNotification').attr("style", "display:none");
            $('#OnCall').attr("style", "display:none");
            $('#CallButton').attr("style", "display:none");
        }
        else
        {
            $('#CallNotification').attr("style", "display:none");
            $('#OnCall').attr("style", "display:none");
            $('#CallButton').attr("style", "display:normal");
            StopStream();
        }
    }
    chat.client.DisconnectCall = function ()
    {
        StopStream();
        document.querySelector('video').srcObject = null;
        $('#Video').attr("style", "display:none");
        $('#Disconnectvideoblock').attr("style", "visibility:hidden");
        $('#CallButton').attr("style", "display:normal");
    }
    $('#Accept').click(function () {
        StopCallSound();
        getUserMedia_starts();
        $('#GetUserMediaType').attr("value", "Accept");             
    });
    $('#Disconnect').click(function () {
        chat.server.disconnect($('#InterlocutorsId').val(), $('#hdId').val());
    });
    $('#Reject').click(function () {
        StopCallSound();
        chat.server.callRejected($('#InterlocutorsId').val(), $('#hdId').val());
    });
    $.connection.hub.start().done(function () {       
        LoadMap();        
        chat.server.connect();
        $('#btnSearch').attr("style", "visibility:visible");
        $('#sendmessage').click(function () {                        
            chat.server.sendTo($('#InterlocutorsId').val(), $('#hdId').val(), $('#message').val());
            $('#message').val('');
            
        });
        $('#CallButton').click(function () {
            getUserMedia_starts();
            $('#GetUserMediaType').attr("value", "Call");                   
        });
        $('#btnSearch').click(function () {
            var TopicsForm = document.forms.TopicsForm;
            var ChosenTopic;
            var Topics = TopicsForm.elements['Topics[]'];
            for (var i = 0; i < Topics.length; i++) {
                if ($('#'+Topics[i].id).prop('checked'))
                {
                    ChosenTopic = Topics[i].id;
                }
            }
            $('#Loading').attr("style", "visibility: visible;z-index: 1;position: relative;margin-right: 0;");
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
                chat.server.startSearch(IM, SM, $('#latitude').attr("value"), $('#longitude').attr("value"),ChosenTopic)              
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
function setChatroomHeight() {
    $('#chatroom').css({
        height: $(window).height()*0.3 + 'px'
    });
}
setChatroomHeight(); // устанавливаем высоту окна при первой загрузке страницы
$(window).resize(setChatroomHeight);
function DisconnectClick()
{
    chat.server.disconnect($('#InterlocutorsId').val(), $('#hdId').val());

}

 function BackToSelect() {
    $('#loginBlock').show();
    $('#chatBody').attr("style", "visibility:hidden");
    $('#btnSearch').attr("style", "visibility:visible");
    chat.server.sendAnotherDisconnect($('#InterlocutorsId').val());    
    $('#ShowInterlocutorsId').text("Связан с :");
    document.getElementById('chatroom').innerHTML = '';
 }
 var audio = new Audio();
 function CallSound() {
     audio.src = "Audio/zvuki-zvonok_stacionarnogo_telefona.mp3";
     audio.autoplay = true;
     audio.loop = true; 
 }
 function StopCallSound()
 {
     audio.pause();
 }