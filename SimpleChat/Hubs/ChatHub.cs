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
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
        public void SendTo(string recipient, string sender, string message)
        {
            Clients.Client(recipient).addMessage(sender, message);
            Clients.Client(sender).addMessage(sender, message);
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
        public void StartSearch(bool IM,bool SM)
        {
            Binder.AddUserInSearch(Context.ConnectionId,IM,SM);
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
    }
}