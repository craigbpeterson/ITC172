using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReviewMVC2018.Models;

namespace BookReviewMVC2018.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName, Password")]LoginClass lc)
        {
            BookReviewDbEntities db = new BookReviewDbEntities();
            int loginResult = db.usp_ReviewerLogin(lc.UserName, lc.Password);

            if(loginResult != -1)
            {
                var uid = (from r in db.Reviewers
                           where r.ReviewerUserName.Equals(lc.UserName)
                           select r.ReviewerKey).FirstOrDefault();

                int rKey = (int)uid;
                Session["reviewerKey"] = rKey;

                Message msg = new Message();
                msg.MessageText = "Thank you, " + lc.UserName + " for logging in. You can now donate or apply for assistance.";
                return RedirectToAction("Result", msg);
            }

            Message failmsg = new Message();
            failmsg.MessageText = "Invalid Login. Please try again.";
            return View("Result", failmsg);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}