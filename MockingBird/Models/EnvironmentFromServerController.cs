using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockingBird.Models
{
    public class EnvironmentFromServerController : Controller
    {
        public ServerDBContext db = new ServerDBContext();

        // GET: EnvironmentFromServer
        public ActionResult Index(string ServerName)
        {
            var EnvironmentSelected = db.Servers.Where(e => e.ServerName == ServerName).FirstOrDefault();

            return View(EnvironmentSelected);
        }

        // GET: EnvironmentFromServer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EnvironmentFromServer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnvironmentFromServer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EnvironmentFromServer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EnvironmentFromServer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EnvironmentFromServer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EnvironmentFromServer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
