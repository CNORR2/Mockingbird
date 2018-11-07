using MockingBird.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MockingBird.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Mockingbird";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Environment()
        {
            ViewBag.Message = "Your contact page.";

            List<HostedEnvironment> lstEnvironments = new List<HostedEnvironment>();

            return View(lstEnvironments);
        }
    }
}