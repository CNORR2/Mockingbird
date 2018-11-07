using MockingBird.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MockingBird.Controllers
{
    public class AdUserGroupLookupController : Controller
    {
        // GET: AdUserGroupLookup
        public ActionResult Index(string ADGroupName)
        {
            AdUserGroupLookup lookupResults = new AdUserGroupLookup();
            ViewBag.AdUserGroupLookup = lookupResults;

            if (ADGroupName != null)
            {
                PrincipalContext domain = new PrincipalContext(ContextType.Domain);
                UserPrincipal users = new UserPrincipal(domain);
                PrincipalSearcher search = new PrincipalSearcher(users);

                foreach (var found in search.FindAll())
                {
                    UserPrincipal user = found as UserPrincipal;

                    if (user != null)
                    {
                        try
                        {
                            PrincipalSearchResult<Principal> oPrincipalSearchResult = user.GetGroups();
                            foreach (Principal oResult in oPrincipalSearchResult)
                            {
                                lookupResults.UserNames.Add(oResult.Name.ToString());
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                try
                {
                    lookupResults.AdGroupName = ADGroupName;
                    ViewBag.AdUserGroupLookup = lookupResults;
                }
                catch
                {

                }
            }
            return View();
        }

        // GET: AdUserGroupLookup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdUserGroupLookup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdUserGroupLookup/Create
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

        // GET: AdUserGroupLookup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdUserGroupLookup/Edit/5
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

        // GET: AdUserGroupLookup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdUserGroupLookup/Delete/5
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
