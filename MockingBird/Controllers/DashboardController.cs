using MockingBird.Models;
using MockingBird.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockingBird.Controllers
{
    public class DashboardController : Controller
    {
        public DriveSpaceDBContext db = new DriveSpaceDBContext();
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        [OutputCache(Duration = 0)]
        public ActionResult DiskMonitoring()
        {


            return PartialView(db.DiskCheckers.ToList());
        }
    }
}