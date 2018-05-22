using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SimpleChat.Models;
using SimpleChat.DataBase;

namespace SimpleChat.Controllers
{
    public class AccountController : Controller
    {      
        public ActionResult Rigistration()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult Rigistration(RegisterModel model)
        {
            model.Errors.Clear();
            Checker.CheckEmpty(ref model);
            if (model.Errors.Count != 0)
            {
                var ModelHere = new HttpCookie("ModelHere", "false");
                Response.SetCookie(ModelHere);
                var Nickname = new HttpCookie("Nickname");
                Nickname.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(Nickname);
                var Old = new HttpCookie("Old");
                Old.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(Old);
                var City = new HttpCookie("City");
                City.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(City);
                var Sex = new HttpCookie("Sex");
                Sex.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(Sex);
                return View(model);
            }
            else
            {
                var ModelHere = new HttpCookie("ModelHere", "true");
                Response.SetCookie(ModelHere);
                var Nickname = new HttpCookie("Nickname", model.Nickname);
                Response.SetCookie(Nickname);
                var Old = new HttpCookie("Old", Convert.ToString(model.Old));
                Response.SetCookie(Old);
                var City = new HttpCookie("City", model.City);
                Response.SetCookie(City);
                var Sex = new HttpCookie("Sex", model.Sex);
                Response.SetCookie(Sex);
            }
            using (WantspeakDbContext db = new WantspeakDbContext())
            {
                db.Users.Add(model);
                db.SaveChanges();
            }
                return RedirectToAction("MainPage", "Home", new { Nickname = model.Nickname, Old = model.Old, City = model.City, Sex = model.Sex });
        }     
    }
}