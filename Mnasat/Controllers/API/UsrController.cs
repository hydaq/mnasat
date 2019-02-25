using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Mnasat.Models;

namespace Mnasat.Controllers.API
{
    public class UsrController : ApiController
    {
        private MnasatDb db = new MnasatDb();

        // GET: api/Usr
        public IQueryable<Usr> GetUsrs()
        {
            return db.Usrs;
        }

        // GET: api/Usr/5
        [ResponseType(typeof(Usr))]
        public IHttpActionResult GetUsr(int id)
        {
            Usr usr = db.Usrs.Find(id);
            if (usr == null)
            {
                return NotFound();
            }

            return Ok(usr);
        }

        // PUT: api/Usr/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsr(int id, Usr usr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usr.UsrID)
            {
                return BadRequest();
            }

            db.Entry(usr).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsrExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usr
        [ResponseType(typeof(Usr))]
        public IHttpActionResult PostUsr(Usr usr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usrs.Add(usr);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usr.UsrID }, usr);
        }

        // DELETE: api/Usr/5
        [ResponseType(typeof(Usr))]
        public IHttpActionResult DeleteUsr(int id)
        {
            Usr usr = db.Usrs.Find(id);
            if (usr == null)
            {
                return NotFound();
            }

            db.Usrs.Remove(usr);
            db.SaveChanges();

            return Ok(usr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsrExists(int id)
        {
            return db.Usrs.Count(e => e.UsrID == id) > 0;
        }
    }
}