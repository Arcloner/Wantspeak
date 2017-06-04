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
        public ActionResult Location()
        {         
            //// получаем ip клиента (если не локальный хост)
            //string IP = "85.142.15.254"/*HttpContext.Request.UserHostAddress*/;
            //string coordinates = "";
            //// получаем географию
            //ViewBag.Location = DefineLocation(IP, ref coordinates);
            //ViewBag.Coords = coordinates;
            return View();
        }
        protected string DefineLocation(string IP, ref string coordinates)
        {
            GeoBazaAPI geo = new GeoBazaAPI(Server.MapPath("~/BD/geobaza.dat"));
            string result = "Не определено";
            // получаем географию по ip
            List<IPLocation> locList = geo.GetLocationByIP(IP);
            if (locList != null && locList.Count != 0 && locList[0].ID != -1)
            {
                IPLocation country = GetCountry(locList);

                if (country != null)
                    result = country.ISOID + ", " + country.NameRU + ", " + locList[0].NameRU + ", долгота: " + locList[0].Longitude + ", долгота: " + locList[0].Latitude;
                else
                    result = locList[0].NameRU + ", долгота: " + locList[0].Longitude + ", долгота: " + locList[0].Latitude;
                coordinates = locList[0].Latitude + ", " + locList[0].Longitude;
            }

            return result;
        }
        private IPLocation GetCountry(List<IPLocation> locList)
        {
            for (int i = 0; i < locList.Count; i++)
            {
                if (locList[i].Type == LocationType.Country)
                    return locList[i];
            }
            return null;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TestChatDesign()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }
    }
}