using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MockingBird.Models;
using PagedList;

namespace MockingBird.Controllers
{
    public class DiskCheckersController : Controller
    {
        public DriveSpaceDBContext db = new DriveSpaceDBContext();
        private ServerDBContext sdb = new ServerDBContext();

        // GET: DiskCheckers

        public ActionResult Index(string searchString, int? page)
        {
            var DiskCheckList = from s in db.DiskCheckers select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                DiskCheckList = DiskCheckList.Where(s => s.ServerName.Contains(searchString));
                page = 1;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(DiskCheckList.OrderBy(s => s.ServerName).ToPagedList(pageNumber, pageSize));
        }

        // GET: DiskCheckers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiskChecker diskChecker = db.DiskCheckers.Find(id);
            if (diskChecker == null)
            {
                return HttpNotFound();
            }
            return View(diskChecker);
        }

        // GET: DiskCheckers/Create
        public ActionResult Create()
        {
            var Integer100List = new List<int>();
            for (var i = 1; i < 101; i++)
            {
                Integer100List.Add(i);
            }

            var EmailAlertMinuteList = new List<int>();
            EmailAlertMinuteList.Add(1);
            EmailAlertMinuteList.Add(5);
            EmailAlertMinuteList.Add(10);
            EmailAlertMinuteList.Add(15);
            EmailAlertMinuteList.Add(30);
            EmailAlertMinuteList.Add(60);
            EmailAlertMinuteList.Add(90);
            EmailAlertMinuteList.Add(120);
            EmailAlertMinuteList.Add(180);
            EmailAlertMinuteList.Add(360);
            EmailAlertMinuteList.Add(720);
            EmailAlertMinuteList.Add(1080);
            EmailAlertMinuteList.Add(1440);

            ViewBag.Integer100List = new SelectList(Integer100List, "LowDiskPercentage");
            ViewBag.EmailAlertMinuteList = new SelectList(EmailAlertMinuteList, "PollTime");
            ViewBag.ServerList = new SelectList(sdb.Servers.OrderBy(x => x.ServerName), "ServerName", "ServerName");

            return View();
        }

        // POST: DiskCheckers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ServerName,DriveLetter,LowDiskPercentage,PollTime,AdminEmailAlert,AlertSubscribers,SubcriptionEmailAddresses,Disable,ShowOnDash,LastRan,DriveSize,AvailableSpace,IgnorePollTimeOnNextRun")] DiskChecker diskChecker)
        {
            if (ModelState.IsValid)
            {
                db.DiskCheckers.Add(diskChecker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diskChecker);
        }

        // GET: DiskCheckers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiskChecker diskChecker = db.DiskCheckers.Find(id);
            if (diskChecker == null)
            {
                return HttpNotFound();
            }
            return View(diskChecker);
        }

        // POST: DiskCheckers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ServerName,DriveLetter,LowDiskPercentage,PollTime,AdminEmailAlert,AlertSubscribers,SubcriptionEmailAddresses,Disable,ShowOnDash,LastRan,DriveSize,AvailableSpace,IgnorePollTimeOnNextRun")] DiskChecker diskChecker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diskChecker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diskChecker);
        }

        // GET: DiskCheckers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiskChecker diskChecker = db.DiskCheckers.Find(id);
            if (diskChecker == null)
            {
                return HttpNotFound();
            }
            return View(diskChecker);
        }

        // POST: DiskCheckers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiskChecker diskChecker = db.DiskCheckers.Find(id);
            db.DiskCheckers.Remove(diskChecker);
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
