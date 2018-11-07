using MockingBird.Enums;
using MockingBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceProcess;
using System.Web;
using System.Web.Mvc;

namespace MockingBird.Controllers
{
    public class RemoteServicesController : Controller
    {
        private ServerDBContext sdb = new ServerDBContext();

        // GET: RemoteServices
        public ActionResult Index(string ServerName, string ServiceName, string Start, string Stop, string Restart)
        {
            List<string> Services = new List<string>();

            //SET Service
            if (Start != null | Stop != null | Restart != null)
            {
                ViewBag.Message = null;

                using (WindowsIdentity.GetCurrent().Impersonate())
                {
                    ServiceController ServiceControl = new ServiceController(ServiceName, ServerName);

                    if (!string.IsNullOrEmpty(Start) & ServiceControl.Status != ServiceControllerStatus.Running)
                    {
                        ServiceControl.Start();
                        ServiceControl.WaitForStatus(ServiceControllerStatus.Running);
                        ViewBag.Message = ServiceStatuses.Service_Is_Now_Running.ToString().Replace("_", " ");
                    }
                    else
                    {
                        ViewBag.Message = ServiceStatuses.Service_Has_Already_Started.ToString().Replace("_", " ");
                    }

                    if (!string.IsNullOrEmpty(Stop) & ServiceControl.Status != ServiceControllerStatus.Stopped)
                    {
                        ServiceControl.Stop();
                        ServiceControl.WaitForStatus(ServiceControllerStatus.Stopped);
                        ViewBag.Message = ServiceStatuses.Service_Has_Been_Stopped.ToString().Replace("_", " ");
                    }
                    else if (!string.IsNullOrEmpty(Stop))
                    {
                        ViewBag.Message = ServiceStatuses.Service_Is_Already_Stopped.ToString().Replace("_", " ");
                    }

                    if (!string.IsNullOrEmpty(Restart))
                    {
                        try
                        {
                            ServiceControl.Stop();
                            ServiceControl.WaitForStatus(ServiceControllerStatus.Stopped);
                            ServiceControl.Start();
                        }
                        catch
                        {
                            ServiceControl.Start();
                        }
                        finally
                        {
                            ServiceControl.WaitForStatus(ServiceControllerStatus.Running);
                            ViewBag.Message = ServiceStatuses.Service_Has_Been_Restarted.ToString().Replace("_", " ");
                        }
                    }
                }
            }

            ViewBag.SelectedServerName = ServerName;
            ViewBag.ServiceDetails = null;

            if (!string.IsNullOrEmpty(ServerName))
            {
                ServiceController[] services = ServiceController.GetServices(ServerName);
                foreach (ServiceController service in services)
                {
                    Services.Add(service.DisplayName);
                }
                Services.Sort();
            }

            if (!string.IsNullOrEmpty(ServiceName))
            {
                ServiceController sc = new ServiceController(ServiceName, ServerName);
                RemoteServices ServiceDetails = new RemoteServices();

                ServiceDetails.DisplayName = sc.DisplayName;
                ServiceDetails.ServiceName = sc.ServiceName;
                ServiceDetails.ServiceType = sc.ServiceType.ToString();
                ServiceDetails.StartupType = sc.StartType.ToString();
                ServiceDetails.Status = sc.Status.ToString();

                ViewBag.ServiceDetails = ServiceDetails;
            }
            ViewBag.ServerList = new SelectList(sdb.Servers.OrderBy(x => x.ServerName), "ServerName", "ServerName");
            ViewBag.Services = Services;

            return View();
        }

        // GET: RemoteServices/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RemoteServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RemoteServices/Create
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

        // GET: RemoteServices/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RemoteServices/Edit/5
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

        // GET: RemoteServices/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RemoteServices/Delete/5
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
