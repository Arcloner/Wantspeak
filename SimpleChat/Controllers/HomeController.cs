using SimpleChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ubs.GeoBazaAPI;

namespace SimpleChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Spy()
        {                 
            return View();
        }
        public ActionResult MainPage(RegisterModel model)
        {
            bool vkidHere;
            bool ModelHere;
            try
            {
                string Model = HttpContext.Request.Cookies["ModelHere"].Value;
                ModelHere = true;                       
            }
            catch (NullReferenceException)
            {
                ModelHere = false;      
            }
            try
            {
                string vkid = HttpContext.Request.Cookies["vkid"].Value;
                vkidHere = true;
            }
            catch (NullReferenceException)
            {
                vkidHere = false;         
            }                        
            if (vkidHere == true || ModelHere == true)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Rigistration", "Account");
            }
        }
        public ActionResult TestPictureUpload(RegisterModel model)
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {            
            return RedirectToAction("MainPage");
        }
    }
}