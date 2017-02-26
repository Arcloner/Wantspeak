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
        //public enum Gender { female, male };
        //public Gender sex { get; set; }
    }
}