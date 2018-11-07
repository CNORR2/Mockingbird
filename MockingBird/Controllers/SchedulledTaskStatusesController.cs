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
using MockingBird.Models;

namespace MockingBird.Controllers
{
    public class SchedulledTaskStatusesController : ApiController
    {
        private SchedulledTaskStatusesDBContext db = new SchedulledTaskStatusesDBContext();

        // GET: api/SchedulledTaskStatuses
        public IQueryable<SchedulledTaskStatuses> GetSchedulledTaskStatus()
        {
            return db.SchedulledTaskStatus.OrderBy(o => o.Status);
        }

        // GET: api/SchedulledTaskStatuses/5
        [ResponseType(typeof(SchedulledTaskStatuses))]
        public IHttpActionResult GetSchedulledTaskStatuses(int id)
        {
            SchedulledTaskStatuses schedulledTaskStatuses = db.SchedulledTaskStatus.Find(id);
            if (schedulledTaskStatuses == null)
            {
                return NotFound();
            }

            return Ok(schedulledTaskStatuses);
        }

        // PUT: api/SchedulledTaskStatuses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchedulledTaskStatuses(int id, SchedulledTaskStatuses schedulledTaskStatuses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schedulledTaskStatuses.ID)
            {
                return BadRequest();
            }

            db.Entry(schedulledTaskStatuses).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulledTaskStatusesExists(id))
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

        // POST: api/SchedulledTaskStatuses
        [ResponseType(typeof(SchedulledTaskStatuses))]
        public IHttpActionResult PostSchedulledTaskStatuses(SchedulledTaskStatuses schedulledTaskStatuses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchedulledTaskStatus.Add(schedulledTaskStatuses);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = schedulledTaskStatuses.ID }, schedulledTaskStatuses);
        }

        // DELETE: api/SchedulledTaskStatuses/5
        [ResponseType(typeof(SchedulledTaskStatuses))]
        public IHttpActionResult DeleteSchedulledTaskStatuses(int id)
        {
            SchedulledTaskStatuses schedulledTaskStatuses = db.SchedulledTaskStatus.Find(id);
            if (schedulledTaskStatuses == null)
            {
                return NotFound();
            }

            db.SchedulledTaskStatus.Remove(schedulledTaskStatuses);
            db.SaveChanges();

            return Ok(schedulledTaskStatuses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchedulledTaskStatusesExists(int id)
        {
            return db.SchedulledTaskStatus.Count(e => e.ID == id) > 0;
        }
    }
}