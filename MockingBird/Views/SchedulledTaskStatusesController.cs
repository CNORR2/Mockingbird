using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MockingBird.Models;

namespace MockingBird.Views
{
    public class SchedulledTaskStatusesController : Controller
    {
        private SchedulledTaskStatusesDBContext db = new SchedulledTaskStatusesDBContext();

        // GET: SchedulledTaskStatuses
        public ActionResult Index()
        {
            return View(db.SchedulledTaskStatus.ToList());
        }

        // GET: SchedulledTaskStatuses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulledTaskStatuses schedulledTaskStatuses = db.SchedulledTaskStatus.Find(id);
            if (schedulledTaskStatuses == null)
            {
                return HttpNotFound();
            }
            return View(schedulledTaskStatuses);
        }

        // GET: SchedulledTaskStatuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchedulledTaskStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Status,Description")] SchedulledTaskStatuses schedulledTaskStatuses)
        {
            if (ModelState.IsValid)
            {
                db.SchedulledTaskStatus.Add(schedulledTaskStatuses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedulledTaskStatuses);
        }

        // GET: SchedulledTaskStatuses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulledTaskStatuses schedulledTaskStatuses = db.SchedulledTaskStatus.Find(id);
            if (schedulledTaskStatuses == null)
            {
                return HttpNotFound();
            }
            return View(schedulledTaskStatuses);
        }

        // POST: SchedulledTaskStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Status,Description")] SchedulledTaskStatuses schedulledTaskStatuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedulledTaskStatuses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedulledTaskStatuses);
        }

        // GET: SchedulledTaskStatuses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulledTaskStatuses schedulledTaskStatuses = db.SchedulledTaskStatus.Find(id);
            if (schedulledTaskStatuses == null)
            {
                return HttpNotFound();
            }
            return View(schedulledTaskStatuses);
        }

        // POST: SchedulledTaskStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchedulledTaskStatuses schedulledTaskStatuses = db.SchedulledTaskStatus.Find(id);
            db.SchedulledTaskStatus.Remove(schedulledTaskStatuses);
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
