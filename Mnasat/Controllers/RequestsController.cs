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
    public class RequestsController : Controller
    {
        private MnasatDb db = new MnasatDb();

        // GET: Requests
        public ActionResult Index()
        {
            if (((Usr)Session["user"]).Privilege == Privileges.Admin)
            {
                return View(db.Requests.ToList());
            }
            else if (((Usr)Session["user"]).Privilege == Privileges.Employee)
            {
                return RedirectToAction("ByEmployee");
            }
            else if (((Usr)Session["user"]).Privilege == Privileges.Customer)
            {
                return RedirectToAction("ByCustomer");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpGet]
        public ActionResult ByEmployee()
        {
            int usr_id = ((Usr)Session["user"]).UsrID;
            var Teams = from res1 in db.TeamMembers where res1.MemberID == usr_id select res1.TeamID;
            var EmployeeRequests = from res in db.Requests where Teams.Contains(res.AssignedTeam) select res;
            return View(EmployeeRequests.ToList());
        }
        public ActionResult ByCustomer()
        {
            int usr_id = ((Usr)Session["user"]).UsrID;
            var CustomerRequests = from res in db.Requests where res.Customer == usr_id select res;
            return View(CustomerRequests.ToList());
        }
        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AssignTask(int? id)
        {
            Session["task"] = id;
            return View(db.Teams.ToList());
        }
        public ActionResult AssignConfirmed(int? id)
        {
            int request_id = (int)Session["task"];
            Request req = db.Requests.Find(request_id);
            Team team = db.Teams.Find(id);
            req.AssignedTeam = team.TeamID;
            req.AssignedTeamName = team.TeamName;
            req.CurrentState = RequestState.Assigned;

            db.SaveChanges();
            Session["task"] = null;
            return RedirectToAction("Index");
        }
        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestID,RequestDescription,CurrentState,RequestDate,Customer,Admin,AssigningDate,AssignedTeam,HandlingEmployee,HandledDate")] Request request)
        {
            request.CurrentState = RequestState.Posted;
            request.AssigningDate = DateTime.Now;
            request.HandledDate = DateTime.Now;
            request.RequestDate = DateTime.Now;
            request.Customer = ((Usr)(Session["user"])).UsrID;
            request.CustomerName = ((Usr)(Session["user"])).Username;
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestID,RequestDescription,CurrentState,RequestDate,Customer,Admin,AssigningDate,AssignedTeam,HandlingEmployee,HandledDate")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
