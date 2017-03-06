using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.Models
{
    public class User
    {
        public string ConnectionId { get; set; }  
        public bool IM { get; set; }
        public bool SM { get; set; }        
        public double lat { get; set; }
        public double lon { get; set; }
        public string Topic { get; set; }
    }
}