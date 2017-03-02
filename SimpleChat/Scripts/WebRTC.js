var chat = $.connection.chatHub;
var localStream = null;
var streamConstraints = { "audio": true, "video": true };
var offerConstraints = {};
var  Servers = {
    "iceServers": [
      { "url": "stun:stun2.l.google.com:19302   " },
    ]
};
var connection ;
var RemoteId;
chat.client.addIceCandidate=function(candidate)
{
    connection.addIceCandidate(new RTCIceCandidate(JSON.parse(candidate)));
}
function onicecandidateEvent(event) {
    if (event.candidate) {
        console.log("pc1_onicecandidate():\n" + event.candidate.candidate.replace("\r\n", ""), event.candidate);       
        chat.server.onIceCandidate(RemoteId,JSON.stringify(event.candidate))

    }
}
function onaddstreamEvent(event) {
    console.log("pc_onaddstream()");
    document.querySelector('video').srcObject = event.stream;    
}
function createOffer_success(desc) {
    console.log("pc1_createOffer_success(): \ndesc.sdp:\n" + desc.sdp + "desc:", desc);
    connection.setLocalDescription(desc);                  
    chat.server.sendReceivedOffer(RemoteId, JSON.stringify(desc));
}
function createOffer_error(error) {
    console.log("pc1_createOffer_success_error(): error:", error);
}
function getUserMedia_success(stream) {
    console.log("getUserMedia_success():", stream);    
    localStream = stream; 
}
function getUserMedia_error(error) {
    console.log("getUserMedia_error():", error);
}
function getUserMedia_starts() {
    console.log("getUserMedia_starts");             
    //navigator.getUserMedia = (navigator.getUserMedia ||
    //                   navigator.webkitGetUserMedia ||
    //                   navigator.mozGetUserMedia ||
    //                   navigator.msGetUserMedia || navigator.mediaDevices.getUserMedia);
    //navigator.mediaDevices.getUserMedia(streamConstraints).then(function (stream) {
    //    getUserMedia_success(stream);
    //}).catch(function (err) {
    //    getUserMedia_error(err);
    //});
        navigator.getUserMedia(
          streamConstraints,
          getUserMedia_success,
          getUserMedia_error
        );    
}
chat.client.StartConnection= function ()
{    
    connection = new RTCPeerConnection(Servers)
    connection.onicecandidate = onicecandidateEvent;
    connection.onaddstream = onaddstreamEvent;
    connection.addStream(localStream);
    connection.createOffer(            
    createOffer_success,
    createOffer_error,
    offerConstraints
 );    
}
function createAnswer_success(desc) {
    connection.setLocalDescription(desc);
    console.log("pc2_createAnswer_success()", desc.sdp);    
    chat.server.setRemoteDesc(RemoteId,JSON.stringify(desc));
}
function createAnswer_error(error) {
    console.log('pc2_createAnswer_error():', error);
}
var answerConstraints = {
    'mandatory': { 'OfferToReceiveAudio': true, 'OfferToReceiveVideo': true }
};
chat.client.setRemoteDescFromServer = function (desc)
{
    connection.setRemoteDescription(JSON.parse(desc));
}
chat.client.receivedOffer = function (desc) {
    console.log("pc2_receiveOffer()", JSON.parse(desc));
    // Создаем объект RTCPeerConnection для второго участника аналогично первому
    connection = new RTCPeerConnection(Servers);
    connection.onicecandidate = onicecandidateEvent; // Задаем обработчик события при появлении ICE-кандидата
    connection.onaddstream = onaddstreamEvent; // При появлении потока подключим его к HTML <video>
    connection.addStream(localStream); // Передадим локальный медиапоток (в нашем примере у второго участника он тот же, что и у первого)
    // Теперь, когда второй RTCPeerConnection готов, передадим ему полученный Offer SDP (первому мы передавали локальный поток)
    connection.setRemoteDescription(new RTCSessionDescription(JSON.parse(desc)));
    // Запросим у второго соединения формирование данных для сообщения Answer      
    connection.createAnswer(
      createAnswer_success,
      createAnswer_error,
      answerConstraints
    );
}


