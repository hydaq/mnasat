using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mnasat.Models;

namespace Mnasat.Controllers
{
    public class UsersController : Controller
    {
        private MnasatDb db = new MnasatDb();

        // GET: Users
        public ActionResult Index()
        {
            if ((Session["user"] != null) && ((Usr)Session["user"]).Privilege == Privileges.Admin)
            {
                return View(db.Usrs.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return Redirect("../Home");
        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usr usr = db.Usrs.Find(id);
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["user"] != null)
                return Redirect("../Home");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Login(Usr usr)
        {
            var LoginResult = from resultant in db.Usrs where resultant.Username == usr.Username && resultant.Password == usr.Password select resultant;
            if (LoginResult.ToList().Count>0)
            {
                Session["user"] = (Usr)LoginResult.ToList().First();
                return Redirect("../Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsrID,Username,Password,Privilege")] Usr usr)
        {
            if (ModelState.IsValid)
            {
                db.Usrs.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usr);
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usr usr = db.Usrs.Find(id);
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsrID,Username,Password,Privilege")] Usr usr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usr);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usr usr = db.Usrs.Find(id);
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usr usr = db.Usrs.Find(id);
            db.Usrs.Remove(usr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
