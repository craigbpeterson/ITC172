using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssistMVC.Models;

namespace CommunityAssistMVC.Controllers
{
    public class NewGrantAppController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: NewGrantApp
        public ActionResult Index()
        {
            if(Session["personkey"] == null)
            {
                Message m = new Message();
                m.MessageText = "You must be logged in to apply for a grant.";
                return RedirectToAction("Result", m);
            }

            ViewBag.GrantTypeKey = new SelectList(db.GrantTypes, "GrantTypeKey", "GrantTypeName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "GrantApplicationKey, PersonKey, GrantAppicationDate, GrantTypeKey, GrantTypeName, GrantApplicationRequestAmount, GrantApplicationReason, GrantApplicationStatusKey, GrantApplicationAllocationAmount")]GrantApplication g)
        {
            g.PersonKey = (int)Session["personkey"];
            g.GrantAppicationDate = DateTime.Now;
            g.GrantApplicationStatusKey = 1;
            db.GrantApplications.Add(g);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "Thank you for applying for a grant.";
            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}