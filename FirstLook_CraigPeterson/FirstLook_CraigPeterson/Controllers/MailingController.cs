using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstLook_CraigPeterson.Models;

namespace FirstLook_CraigPeterson.Controllers
{
    public class MailingController : Controller
    {
        // GET: Mailing
        public ActionResult Index()
        {
            Mailing m1 = new Mailing(); //here we are hard-coding 3 records to the Mailing data model
            m1.FirstName = "Dude";      //ordinarily we will not hard code data like this
            m1.LastName = "ManBro";     //instead, data objects would be modeled from an actual database
            m1.Email = "dudemanbro@fakeemail.com";

            Mailing m2 = new Mailing();
            m2.FirstName = "Craig";
            m2.LastName = "Peterson";
            m2.Email = "craig.b.peterson@gmail.com";

            Mailing m3 = new Mailing();
            m3.FirstName = "Clint";
            m3.LastName = "Dempsey";
            m3.Email = "clintdempsey@soundersfc.com";

            List<Mailing> mailings = new List<Mailing>(); //list is like an array
            mailings.Add(m1); //here we are manually adding the objects we just created to the list
            mailings.Add(m2);
            mailings.Add(m3);

            return View(mailings);
        }
    }
}