using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserRoles.Controllers
{
    public class HomeController : ApplicationBaseController
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View("AdminIndex");
            else if (User.IsInRole("Manager"))
                return View("ManagerIndex");
            else
            {
                return View();
            }
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

       
    }

    
}