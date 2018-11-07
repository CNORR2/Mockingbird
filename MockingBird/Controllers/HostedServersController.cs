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
    public class HostedServersController : Controller
    {
        private ServerDBContext sdb = new ServerDBContext();
        private EnvironmentDBContext edb = new EnvironmentDBContext();

        // GET: HostedServers
        public ActionResult Index()
        {
            return View(sdb.Servers.OrderBy(o => o.ServerName).ToList());
        }

        // GET: HostedServers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HostedServers hostedServers = sdb.Servers.Find(id);
            if (hostedServers == null)
            {
                return HttpNotFound();
            }
            return View(hostedServers);
        }

        // GET: HostedServers/Create
        public ActionResult Create()
        {
            ViewBag.EnvironmentList = new SelectList(edb.Environments.OrderBy(x => x.EnvironmentName), "EnvironmentName", "EnvironmentName");

            return View();
        }

        // POST: HostedServers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ServerName,Environment,Comments")] HostedServers hostedServers)
        {
            if (ModelState.IsValid)
            {
                sdb.Servers.Add(hostedServers);
                sdb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hostedServers);
        }

        // GET: HostedServers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HostedServers hostedServers = sdb.Servers.Find(id);
            if (hostedServers == null)
            {
                return HttpNotFound();
            }
            return View(hostedServers);
        }

        // POST: HostedServers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ServerName,Environment,Comments")] HostedServers hostedServers)
        {
            if (ModelState.IsValid)
            {
                sdb.Entry(hostedServers).State = EntityState.Modified;
                sdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hostedServers);
        }

        // GET: HostedServers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HostedServers hostedServers = sdb.Servers.Find(id);
            if (hostedServers == null)
            {
                return HttpNotFound();
            }
            return View(hostedServers);
        }

        // POST: HostedServers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HostedServers hostedServers = sdb.Servers.Find(id);
            sdb.Servers.Remove(hostedServers);
            sdb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sdb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
