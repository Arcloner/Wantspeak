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
        public void SimpleBindUsers()
        {
            while (true)
            {
                if (UsersInSearch.Count >= 2)
                {
                    string firstId;
                    string secondId;
                    try
                    {
                        firstId = UsersInSearch[0].ConnectionId;
                        secondId = UsersInSearch[1].ConnectionId;
                    }
                    catch (NullReferenceException)
                    {
                        firstId = UsersInSearch[0].ConnectionId;
                        secondId = UsersInSearch[1].ConnectionId;
                    }
                    hub.Clients.Client(firstId).setInterlocutorId(secondId);
                    hub.Clients.Client(secondId).setInterlocutorId(firstId);
                    hub.Clients.Client(firstId).showChat();
                    hub.Clients.Client(secondId).showChat();
                    UsersInSearch.Remove(UsersInSearch[1]);
                    UsersInSearch.Remove(UsersInSearch[0]);                  
                }
            }
        }
        public void BindUsers()
        {
            while (true)
            {
                bool find = false;    
                RangeExeption:       
                for (int i = 0; i < UsersInSearch.Count; i++)
                {
                    double MinDistance=-1;
                    string firstId=null;
                    string secondId=null;
                    for (int z = 0; z < UsersInSearch.Count; z++)
                    {                      
                    Found:
                        try
                        {
                            if (UsersInSearch[i].SM == UsersInSearch[z].IM && UsersInSearch[z].SM == UsersInSearch[i].IM && UsersInSearch[i] != UsersInSearch[z])
                            {
                                double distance = Math.Sqrt(Math.Pow(UsersInSearch[i].lat - UsersInSearch[z].lat, 2) + Math.Pow(UsersInSearch[i].lon - UsersInSearch[z].lon, 2));
                                if (MinDistance == -1||distance<MinDistance)
                                {
                                    MinDistance = distance;
                                    firstId = UsersInSearch[i].ConnectionId;
                                    secondId = UsersInSearch[z].ConnectionId;
                                }
                                if (z == UsersInSearch.Count-1)
                                {
                                    hub.Clients.Client(firstId).setInterlocutorId(secondId);
                                    hub.Clients.Client(secondId).setInterlocutorId(firstId);
                                    hub.Clients.Client(firstId).showChat();
                                    hub.Clients.Client(secondId).showChat();
                                    hub.Clients.Client(firstId).StartConnection();
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
                            goto Found;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            goto RangeExeption;
                        }
                    }
                    if (find == true)
                    {
                        break;
                    }
                }
            }
        }
        public static void AddUserInSearch(string id,bool IM,bool SM,double lat,double lon)
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