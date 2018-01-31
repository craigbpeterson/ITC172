using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC.Models;

namespace CommunityAssistMVC.Controllers
{
    public class RegistrationController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: Registration
        public ActionResult Index()
        {
            return View(db.People.ToList()); //listing people allows us to see if the registration is working
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "LastName, FirstName, Email, Password, Apartment, Street, City, State, Zipcode, Phone")]NewPerson p)
        {
            int result = db.usp_Register(
                p.LastName, 
                p.FirstName, 
                p.Email, 
                p.PlainPassword, 
                p.Apartment, 
                p.Street, 
                p.City, 
                p.State, 
                p.Zipcode, 
                p.Phone
            );

            Message m = new Message();

            if (result != -1) //result would be -1 if previous block failed
            {
                m.MessageText = "Thank You for Registering!";
                return RedirectToAction("Result",m);
            }

            m.MessageText = "Oops. Something went wrong. Registration unsuccessful.";
            return RedirectToAction("Result",m); //this line only runs when the previous if statement doesn't evaluate true
        }

    }
}