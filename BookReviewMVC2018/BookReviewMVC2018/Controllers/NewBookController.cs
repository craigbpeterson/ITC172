using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReviewMVC2018.Models;

namespace BookReviewMVC2018.Controllers
{
    public class NewBookController : Controller
    {
        BookReviewDbEntities db = new BookReviewDbEntities();
        // GET: NewBook
        public ActionResult Index()
        {
            if(Session["reviewerKey"] == null) //this session variable is set when a user logs in (see the LoginController.cs)
            {
                Message m = new Message();
                m.MessageText = "You must be logged in to enter a new book!";
                return RedirectToAction("Result", m);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Title, ISBN, AuthorName")]NewBook nb)
        {
            //we should check if the author exists in the database before adding new author
            Author a = new Author();
            a.AuthorName = nb.AuthorName;
            db.Authors.Add(a);
            db.SaveChanges();

            /* for the donation assignment, get the userkey from the session so that you can save who made the donation */

            Book b = new Book();
            b.BookTitle = nb.Title;
            b.BookISBN = nb.ISBN;
            b.BookEntryDate = DateTime.Now;

            //the following step is not necessary for the donation assignment
            Author author = db.Authors.FirstOrDefault(x => x.AuthorName == nb.AuthorName);
            b.Authors.Add(author);

            db.Books.Add(b);
            db.SaveChanges();

            Message m = new Message();
            m.MessageText = "New book successfully added. Thank you!";

            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}