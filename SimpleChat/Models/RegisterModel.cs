using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleChat.Models
{
    public class RegisterModel
    { 
        public string Nickname { get; set; }
        
        public int Old { get; set; }     
        
        public string City { get; set; }

        public string Sex { get; set; }

        public List<string> Errors = new List<string>();
    }
}