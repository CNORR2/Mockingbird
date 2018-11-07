using MockingBird.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockingBird.Controllers
{
    public class AdGroupLookupController : Controller
    {
        // GET: AdGroupLookup
        public ActionResult Index(string UserName)
        {
            ViewBag.AdGroupLookup = null;
            AdGroupLookup lookupResults = new AdGroupLookup();

            PrincipalContext domain = new PrincipalContext(ContextType.Domain);
            List<string> domainGroups = new List<string>();

            try
            {
                if (UserName != null)
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(domain, UserName);
                    PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();
                    lookupResults.Name = user.Name;

                    foreach (Principal p in groups)
                    {
                        if (p is GroupPrincipal)
                        {
                            domainGroups.Add(p.ToString());
                        }
                    }
                }

                lookupResults.UserName = UserName;
                lookupResults.AdGroupName = domainGroups.OrderBy(o => o).ToList();

                ViewBag.AdGroupLookup = lookupResults;
            }
            catch
            {
                lookupResults.UserName = "Username does not exists";
                ViewBag.AdGroupLookup = lookupResults;
            }

            return View();
        }

        // GET: AdGroupLookup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdGroupLookup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdGroupLookup/Create
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

        // GET: AdGroupLookup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdGroupLookup/Edit/5
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

        // GET: AdGroupLookup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdGroupLookup/Delete/5
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
