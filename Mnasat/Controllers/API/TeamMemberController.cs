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
using System.Web.Http.Results;
using System.Web.Script.Serialization;

namespace Mnasat.Controllers.API
{
    public class TeamMemberController : ApiController
    {
        private MnasatDb db = new MnasatDb();

        // GET: api/TeamMember
        public String GetTeamMembers()
        {
            var TeamMembersCollection = from joinTable in db.TeamMembers
                                        join teamsTable in db.Teams on joinTable.TeamID equals teamsTable.TeamID
                                        join usrsTable in db.Usrs on joinTable.MemberID equals usrsTable.UsrID
                                        select new { TeamName = teamsTable.TeamName, Username = usrsTable.Username };

            //return db.TeamMembers;
            return new JavaScriptSerializer().Serialize(TeamMembersCollection.ToList());

        }

        // GET: api/TeamMember/5
        [ResponseType(typeof(TeamMember))]
        public IHttpActionResult GetTeamMember(int id)
        {
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return Ok(teamMember);
        }

        // PUT: api/TeamMember/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeamMember(int id, TeamMember teamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamMember.ID)
            {
                return BadRequest();
            }

            db.Entry(teamMember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMemberExists(id))
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

        // POST: api/TeamMember
        [ResponseType(typeof(TeamMember))]
        public IHttpActionResult PostTeamMember(TeamMember teamMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeamMembers.Add(teamMember);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teamMember.ID }, teamMember);
        }

        // DELETE: api/TeamMember/5
        [ResponseType(typeof(TeamMember))]
        public IHttpActionResult DeleteTeamMember(int id)
        {
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            db.TeamMembers.Remove(teamMember);
            db.SaveChanges();

            return Ok(teamMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamMemberExists(int id)
        {
            return db.TeamMembers.Count(e => e.ID == id) > 0;
        }
    }
}