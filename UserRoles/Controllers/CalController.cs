using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventBook.Models;
using UserRoles.Models;
using UserRoles;
using UserRoles.Controllers;


namespace EventBook.Controllers
{
    [Authorize]
    public class CalController : Controller
    {
        public ApplicationDbContext  db = new ApplicationDbContext();

        // GET: Cal
        public ActionResult Client()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEv(Event e)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                if (e.EventId > 0)
                {
                    var v = dc.Events.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.EventType = e.EventType;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Fname = e.Fname;
                        v.contactNum = e.contactNum;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status } };
        }

        [HttpPost]
        public JsonResult DeleteEv(int eventID)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var v = dc.Events.Where(a => a.EventId == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}