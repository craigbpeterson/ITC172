using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReviewMVC2018.Models;

namespace BookReviewMVC2018.Controllers
{
    public class RegistrationController : Controller
    {
        BookReviewDbEntities db = new BookReviewDbEntities();
        // GET: Registration
        public ActionResult Index()
        {
            return View(db.Reviewers.ToList()); //listing reviewiers allows us to see if the registration is working
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "reviewerUserName,reviewerFirstName,reviewerLastName,reviewerEmail,reviewPlainPassword")]Reviewer r)
        {
            int result = db.usp_NewReviewer(
                r.ReviewerUserName, 
                r.ReviewerFirstName, 
                r.ReviewerLastName, 
                r.ReviewerEmail, 
                r.ReviewPlainPassword //this one is Review instead of Reviewer only because Steve made a mistake in the BookReview db
            );

            if(result != -1) //result would be -1 if previous block failed
            {
                return RedirectToAction("Index"); //for community assist, create a new view called ThankYou
            }

            return View(); //this line only runs when the previous if statement doesn't evaluate true
        }
    }
}