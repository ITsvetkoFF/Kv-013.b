using Data.gov.ua.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Data.gov.ua.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            PersonsXMLParser parser = new PersonsXMLParser();
            List<Bill> bills = parser.GetBills();

            return View();
        }
    }
}
