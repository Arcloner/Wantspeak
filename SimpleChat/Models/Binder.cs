using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SimpleChat.Models
{
    public class Binder
    {
        private IHubContext hub;
        static List<User> UsersInSearch = new List<User>();
        public Binder(IHubContext hub)
        {
            this.hub = hub;
        }        
        public void BindUsers()
        {            
            while (true)
            {
                List<User> LocalUsersInSearch = new List<User>();
                LocalUsersInSearch = UsersInSearch.GetRange(0, UsersInSearch.Count);
                bool find = false;                    
                for (int i = 0; i < LocalUsersInSearch.Count; i++)
                {
                    double MinDistance=-1;
                    string firstId=null;
                    string secondId=null;
                    for (int z = 0; z < LocalUsersInSearch.Count; z++)
                    {
                        FromNullExeption:
                        try
                        {
                            if (LocalUsersInSearch[i].SM == LocalUsersInSearch[z].IM && LocalUsersInSearch[z].SM == LocalUsersInSearch[i].IM && LocalUsersInSearch[i] != LocalUsersInSearch[z])
                            {
                                double distance = Math.Sqrt(Math.Pow(LocalUsersInSearch[i].lat - LocalUsersInSearch[z].lat, 2) + Math.Pow(LocalUsersInSearch[i].lon - LocalUsersInSearch[z].lon, 2));
                                if (MinDistance == -1 || distance < MinDistance)
                                {
                                    MinDistance = distance;
                                    firstId = LocalUsersInSearch[i].ConnectionId;
                                    secondId = LocalUsersInSearch[z].ConnectionId;
                                }
                                if (z == LocalUsersInSearch.Count - 1)
                                {
                                    hub.Clients.Client(firstId).setInterlocutorId(secondId);
                                    hub.Clients.Client(secondId).setInterlocutorId(firstId);
                                    hub.Clients.Client(firstId).showChat();
                                    hub.Clients.Client(secondId).showChat();
                                    //hub.Clients.Client(firstId).StartConnection();
                                    if (i > z)
                                    {
                                        UsersInSearch.Remove(UsersInSearch[i]);
                                        UsersInSearch.Remove(UsersInSearch[z]);
                                    }
                                    else
                                    {
                                        UsersInSearch.Remove(UsersInSearch[z]);
                                        UsersInSearch.Remove(UsersInSearch[i]);
                                    }
                                    find = true;
                                    break;
                                }
                            }
                        }
                        catch (NullReferenceException)
                        {
                            LocalUsersInSearch= UsersInSearch.GetRange(0, UsersInSearch.Count);
                            goto FromNullExeption;
                        }                      
                    }
                    if (find == true)
                    {
                        break;
                    }
                }
            }
        }
        public static void AddUserInSearch(string id, bool IM, bool SM, double lat, double lon)
        {           
            UsersInSearch.Add(new User { ConnectionId = id ,IM=IM,SM=SM,lat=lat,lon=lon});
        }
        public static void DeleteUserInSearch(string Id)
        {
            var item = UsersInSearch.FirstOrDefault(x => x.ConnectionId == Id);
            UsersInSearch.Remove(item);
        }
    }
}