﻿var chat = $.connection.chatHub;
var $messages = $('.messages-content'),
    d, h, m;

$(window).load(function () {
    $messages.mCustomScrollbar();    
});

function updateScrollbar() {
    $messages.mCustomScrollbar("update").mCustomScrollbar('scrollTo', 'bottom', {
        scrollInertia: 10,
        timeout: 0
    });
}

function setDate() {
    d = new Date()    
        m = d.getMinutes();
        $('<div class="timestamp">' + d.getHours() + ':' + m + '</div>').appendTo($('.message:last'));    
}

function insertMessage() {
    msg = $('.message-input').val();
    if ($.trim(msg) == '') {
        return false;
    }
    $('<div class="message message-personal">' + msg + '</div>').appendTo($('.mCSB_container')).addClass('new');
    setDate();
    $('.message-input').val(null);
    updateScrollbar();
    chat.server.receiveMessage($('#InterlocutorsId').val(), msg);
}

$('.message-submit').click(function () {
    insertMessage();
});

$(window).on('keydown', function (e) {
    if (e.which == 13) {
        insertMessage();
        return false;
    }
})
chat.client.receiveMessage=function(msg) {
    if ($('.message-input').val() != '') {
        return false;
    }
    //$('<div class="message loading new"><figure class="avatar"><img src="https://pp.userapi.com/c630418/v630418532/3d3b2/--y_i_j0pJw.jpg" /></figure><span></span></div>').appendTo($('.mCSB_container'));
    //updateScrollbar();
    
    //setTimeout(function () {
        //$('.message.loading').remove();
        $('<div class="message new"><figure class="avatar"><img src="https://pp.userapi.com/c630418/v630418532/3d3b2/--y_i_j0pJw.jpg" /></figure>' + msg + '</div>').appendTo($('.mCSB_container')).addClass('new');
        setDate();
        updateScrollbar();
        //i++;
    //}, 1000 + (Math.random() * 20) * 100);

}