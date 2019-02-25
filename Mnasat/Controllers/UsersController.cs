using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mnasat.Models;
using System.Net.Http;

namespace Mnasat.Controllers
{

    public class UsersController : Controller
    {
        private MnasatDb db = new MnasatDb();

        // GET: Users
        public ActionResult Index()
        {
            IEnumerable<Usr> usrs = null;
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(Startup.GloboApiUrl);
                var responseTask = client.GetAsync("usr");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Usr>>();
                    readTask.Wait();

                    usrs = readTask.Result;
                }
                else //web api sent error response 
                {
                    return HttpNotFound();
                }
            }
            return View(usrs);
            
        }
        
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
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
            Usr user = null;
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(Startup.GloboApiUrl);
                var responseTask = client.GetAsync("usr/"+id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Usr>();
                    readTask.Wait();
                    user = readTask.Result;
                }
                else 
                {
                    //log response status here..
                    return HttpNotFound();
                }
            }
            return View(user);
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
