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
    public class SchedulledTasksController : Controller
    {
        private ServerDBContext sdb = new ServerDBContext();
        private EnvironmentDBContext edb = new EnvironmentDBContext();
        private SchedulledTaskStatusesDBContext db = new SchedulledTaskStatusesDBContext();

        // GET: SchedulledTasks
        public ActionResult Index(string searchString)
        {
            var SchedulledTaskList = from s in db.SchedulledTasks select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                SchedulledTaskList = SchedulledTaskList.Where(s => s.ServerName.Contains(searchString) || s.SchedulledTaskName.Contains(searchString));
            }

            return View(SchedulledTaskList.OrderBy(o =>o.ServerName).ToList());
        }

        // GET: SchedulledTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulledTasks schedulledTasks = db.SchedulledTasks.Find(id);
            if (schedulledTasks == null)
            {
                return HttpNotFound();
            }
            return View(schedulledTasks);
        }

        // GET: SchedulledTasks/Create
        public ActionResult Create()
        {
            ViewBag.ServerList = new SelectList(sdb.Servers.OrderBy(x => x.ServerName), "ServerName", "ServerName");
            ViewBag.ServerEnvironments = sdb.Servers.ToList();

            return View();
        }

        // POST: SchedulledTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SchedulledTaskName,ServerName,Status,LastTaskResult,LastRunTime,NextRunTime,PollTime,AdminEmailAlert,AlertSubscribers,SubcriptionEmailAddresses,Disable,ShowOnDash,IgnorePollTimeOnNextRun")] SchedulledTasks schedulledTasks)
        {
            if (ModelState.IsValid)
            {
                db.SchedulledTasks.Add(schedulledTasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedulledTasks);
        }

        // GET: SchedulledTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulledTasks schedulledTasks = db.SchedulledTasks.Find(id);
            if (schedulledTasks == null)
            {
                return HttpNotFound();
            }
            return View(schedulledTasks);
        }

        // POST: SchedulledTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SchedulledTaskName,ServerName,Status,LastTaskResult,LastRunTime,NextRunTime,PollTime,AdminEmailAlert,AlertSubscribers,SubcriptionEmailAddresses,Disable,ShowOnDash,IgnorePollTimeOnNextRun")] SchedulledTasks schedulledTasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedulledTasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedulledTasks);
        }

        // GET: SchedulledTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulledTasks schedulledTasks = db.SchedulledTasks.Find(id);
            if (schedulledTasks == null)
            {
                return HttpNotFound();
            }
            return View(schedulledTasks);
        }

        // POST: SchedulledTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchedulledTasks schedulledTasks = db.SchedulledTasks.Find(id);
            db.SchedulledTasks.Remove(schedulledTasks);
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
