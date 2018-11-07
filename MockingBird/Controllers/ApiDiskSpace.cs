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
    public class ApiDiskSpace : ApiController
    {
        private DriveSpaceDBContext db = new DriveSpaceDBContext();

        // GET: api/ApiDiskSpace
        public IQueryable<DiskChecker> GetDiskCheckers()
        {
            return db.DiskCheckers;
        }

        // GET: api/ApiDiskSpace/5
        [ResponseType(typeof(DiskChecker))]
        public IHttpActionResult GetDiskChecker(int id)
        {
            DiskChecker diskChecker = db.DiskCheckers.Find(id);
            if (diskChecker == null)
            {
                return NotFound();
            }

            return Ok(diskChecker);
        }

        // PUT: api/ApiDiskSpace/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiskChecker(int id, DiskChecker diskChecker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diskChecker.ID)
            {
                return BadRequest();
            }

            db.Entry(diskChecker).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiskCheckerExists(id))
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

        // POST: api/ApiDiskSpace
        [ResponseType(typeof(DiskChecker))]
        public IHttpActionResult PostDiskChecker(DiskChecker diskChecker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DiskCheckers.Add(diskChecker);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = diskChecker.ID }, diskChecker);
        }

        // DELETE: api/ApiDiskSpace/5
        [ResponseType(typeof(DiskChecker))]
        public IHttpActionResult DeleteDiskChecker(int id)
        {
            DiskChecker diskChecker = db.DiskCheckers.Find(id);
            if (diskChecker == null)
            {
                return NotFound();
            }

            db.DiskCheckers.Remove(diskChecker);
            db.SaveChanges();

            return Ok(diskChecker);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiskCheckerExists(int id)
        {
            return db.DiskCheckers.Count(e => e.ID == id) > 0;
        }
    }
}