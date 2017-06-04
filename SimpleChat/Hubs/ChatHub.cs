using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SimpleChat.Models;

namespace SimpleChat.Hubs
{
    public class ChatHub : Hub
    {                        
        static List<User> Users = new List<User>();
        public void ReceiveMessage(string recipient, string message)
        {
            Clients.Client(recipient).receiveMessage(message);
        }
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
        public void SendTo(string recipient, string sender, string message)
        {
            Clients.Client(recipient).addMessage(sender, message);
            Clients.Client(sender).addMessage(sender, message);
        }
        public void VideoCall(string recipient, string sender)
        {
            Clients.Client(recipient).showCallNotification();
            Clients.Client(sender).onCall();
        }
        public void CallAccepted(string recipient, string sender)
        {
            Clients.Client(recipient).StartConnection();
            Clients.Client(recipient).callIs(true);
            Clients.Client(sender).callIs(true);
        }
        public void CallRejected(string recipient, string sender)
        {
            Clients.Client(recipient).callIs(false);
            Clients.Client(sender).callIs(false);
        }
        public void Disconnect(string recipient, string sender)
        {
            Clients.Client(recipient).DisconnectCall();
            Clients.Client(sender).DisconnectCall();
        }
        public void Connect()
        {
            var id = Context.ConnectionId;                        
            if (!Users.Any(x => x.ConnectionId == id))
            {               
                Users.Add(new User { ConnectionId = id });         
                Clients.Caller.setId(id);
                Clients.Caller.onConnected(id, Users);
                Clients.AllExcept(id).onNewUserConnected(id);                
            }
        }      
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);           
            if (item != null)
            {
                Users.Remove(item);
                Binder.DeleteUserInSearch(Context.ConnectionId);               
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id);               
            }
            return base.OnDisconnected(stopCalled);
        }
        public void StartSearch(bool IM,bool SM,string lat, string lon,string Topic)
        {            
            string x=lat.Replace(".", ",");
            string y=lon.Replace(".", ",");
            if (lat == "" || lon == "")
            {
                x = "0";
                y = "0";
            }
            int subi = 0;
        Correct:           
            try
            {
                Binder.AddUserInSearch(Context.ConnectionId, IM, SM, Convert.ToDouble(x.Substring(0, x.Length - subi)), Convert.ToDouble(y.Substring(0, y.Length - subi)),Topic);
            }
            catch (FormatException)
            {
                subi++;
                goto Correct;
            }
        }
        public void OnIceCandidate(string RemoteId,string candidate)
        {
            Clients.Client(RemoteId).addIceCandidate(candidate);
        }
        public void SendReceivedOffer(string RemoteId,string desc)
        {
            Clients.Client(RemoteId).receivedOffer(desc);
        }
        public void SetRemoteDesc(string RemoteId,string desc)
        {
            Clients.Client(RemoteId).setRemoteDescFromServer(desc);
        }
        public void sendAnotherDisconnect(string recipient)
        {
            Clients.Client(recipient).AnotherDisconnect();
        }
    }
}