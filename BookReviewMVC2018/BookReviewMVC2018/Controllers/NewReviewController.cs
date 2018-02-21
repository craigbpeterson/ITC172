using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReviewMVC2018.Models;

namespace BookReviewMVC2018.Controllers
{
    public class NewReviewController : Controller
    {
        BookReviewDbEntities db = new BookReviewDbEntities(); //instantiate the database model
        // GET: NewReview
        public ActionResult Index()
        {
            if(Session["reviewerKey"]==null) //check to make sure the user is logged in
            {
                Message m = new Message();
                m.MessageText = "You must be logged in to add a review.";
                return RedirectToAction("Result", m);
            }

            ViewBag.BookKey = new SelectList(db.Books, "BookKey", "BookTitle"); //if they are logged in, pass these two columns from the database to the view using a new ViewBag named BookKey

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "BookKey, ReviewerKey, ReviewDate, ReviewTitle, ReviewRating, ReviewText")]Review r)
        {
            r.ReviewerKey = (int)Session["reviewerKey"];
            r.ReviewDate = DateTime.Now;
            db.Reviews.Add(r);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thank you for your review!";
            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}