using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC.Models;

namespace CommunityAssistMVC.Controllers
{
    public class NewDonationController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: NewDonation
        public ActionResult Index()
        {
            if (Session["personkey"] == null) //this session variable is set when a user logs in (see the LoginController.cs)
            {
                Message m = new Message();
                m.MessageText = "You must be logged in to make a donation.";
                return RedirectToAction("Result", m);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "DonationAmount")]NewDonation nd)
        {
            Donation d = new Donation();

            int pKey = (int)Session["personkey"];

            d.PersonKey = pKey;
            d.DonationDate = DateTime.Now;
            d.DonationAmount = nd.DonationAmount;
            d.DonationConfirmationCode = Guid.NewGuid();
            db.Donations.Add(d);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thank you for your donation!";

            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}