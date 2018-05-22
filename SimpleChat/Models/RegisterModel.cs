using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleChat.Models
{
    public class RegisterModel
    { 
        public int Id { get; set; }

        public string Nickname { get; set; }
        
        public int Old { get; set; }     
        
        public string City { get; set; }

        public string Sex { get; set; }

        [NotMapped]
        public List<string> Errors = new List<string>();
    }
}