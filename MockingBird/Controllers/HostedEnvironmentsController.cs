using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MockingBird.Models;

namespace MockingBird.Controllers
{
    public class HostedEnvironmentsController : Controller
    {
        private EnvironmentDBContext db = new EnvironmentDBContext();

        // GET: HostedEnvironments
        public ActionResult Index()
        {


            return View(db.Environments.OrderBy(o => o.EnvironmentName).ToList());
        }

        // GET: HostedEnvironments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HostedEnvironment hostedEnvironment = db.Environments.Find(id);
            if (hostedEnvironment == null)
            {
                return HttpNotFound();
            }
            return View(hostedEnvironment);
        }

        // GET: HostedEnvironments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HostedEnvironments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EnvironmentName")] HostedEnvironment hostedEnvironment)
        {
            if (ModelState.IsValid)
            {
                db.Environments.Add(hostedEnvironment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hostedEnvironment);
        }

        // GET: HostedEnvironments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HostedEnvironment hostedEnvironment = db.Environments.Find(id);
            if (hostedEnvironment == null)
            {
                return HttpNotFound();
            }
            return View(hostedEnvironment);
        }

        // POST: HostedEnvironments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EnvironmentName")] HostedEnvironment hostedEnvironment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hostedEnvironment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hostedEnvironment);
        }

        // GET: HostedEnvironments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HostedEnvironment hostedEnvironment = db.Environments.Find(id);
            if (hostedEnvironment == null)
            {
                return HttpNotFound();
            }
            return View(hostedEnvironment);
        }

        // POST: HostedEnvironments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HostedEnvironment hostedEnvironment = db.Environments.Find(id);
            db.Environments.Remove(hostedEnvironment);
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
