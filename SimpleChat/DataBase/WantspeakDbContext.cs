using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SimpleChat.Models;

namespace SimpleChat.DataBase
{
    public class WantspeakDbContext:DbContext
    {
        public DbSet<RegisterModel> Users { get; set; }
        public WantspeakDbContext() : base("WantspeakDatabase")
        { }
    }
}