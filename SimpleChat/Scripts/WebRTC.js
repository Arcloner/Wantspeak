var chat = $.connection.chatHub;
var localStream = null;
var streamConstraints = {
    "audio": true, "video": { width: 768, height: 432 }
};
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
    $('#Video').attr("style", "display:normal;background:black");
    $('#VideoStream').attr("style", "display:normal");
    $('#Disconnect').attr("style", "display:normal");
    $('#Disconnectvideoblock').attr("style", "display:normal");
    setVideoSize();
    document.querySelector('video').play();
}
function StopStream()
{
    localStream.getVideoTracks()[0].stop();
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
    if ($('#GetUserMediaType').attr("value") == "Accept")
    {
        chat.server.callAccepted($('#InterlocutorsId').val(), $('#hdId').val());
    }
    if ($('#GetUserMediaType').attr("value") == "Call")
    {
        chat.server.videoCall($('#InterlocutorsId').val(), $('#hdId').val());
    }
}
function getUserMedia_error(error) {
    console.log("getUserMedia_error():", error);
}
function getUserMedia_starts() {
    console.log("getUserMedia_starts");                
        navigator.getUserMedia(
          streamConstraints,
          getUserMedia_success,
          getUserMedia_error
        );    
}
function StartWebRTC ()
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
    connection = new RTCPeerConnection(Servers);
    connection.onicecandidate = onicecandidateEvent; 
    connection.onaddstream = onaddstreamEvent; 
    connection.addStream(localStream);     
    connection.setRemoteDescription(new RTCSessionDescription(JSON.parse(desc)));        
    connection.createAnswer(
      createAnswer_success,
      createAnswer_error,
      answerConstraints
    );
}
function setVideoSize() {
    $('#VideoStream').css({
    "width":$(window).width*0.6+'px',
    "height": $(window).height() * 0.4 + 'px',    
    "z-index":"5"
    });
    $('#Disconnect').css({              
        "position": "absolute",
        "margin-left": $('#VideoStream').width()+"px",
        "z-index": "5"
    });
}
$(window).resize(setVideoSize);

