using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC.Models;

namespace CommunityAssistMVC.Controllers
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
            CommunityAssist2017Entities db = new CommunityAssist2017Entities();
            int loginResult = db.usp_Login(lc.UserName, lc.Password);

            if(loginResult != -1)
            {
                var uid = (from r in db.People
                           where r.PersonEmail.Equals(lc.UserName)
                           select r.PersonKey).FirstOrDefault();

                int pKey = (int)uid;
                Session["personkey"] = pKey;

                Message msg = new Message();
                msg.MessageText = "Thank you, " + lc.UserName + " for logging in. You can now donate or apply for assistance.";
                return RedirectToAction("Results", msg);
            }

            Message failmsg = new Message();
            failmsg.MessageText = "Invalid Login. Please try again, or register if you have not done so yet.";
            return View("Results", failmsg);
        }

        public ActionResult Results(Message m)
        {
            return View(m);
        }
    }
}