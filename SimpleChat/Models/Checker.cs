using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.Models
{
    public class Checker
    {
        public static void CheckEmpty(ref RegisterModel model)
        {
            if (model.Nickname == ""|| model.Nickname == null)
            {
                model.Errors.Add("Empty Nickname");
            }
            if (model.City == null|| model.City == "")
            {
                model.Errors.Add("Empty City");
            }
        }
    }
}