using System.Web;
using System.Web.Mvc;
using Eventmanagement.Entities;
using Eventmanagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementWeb.Controllers
{
    public class EventManagementController : Controller
    {
        // GET: EventManagement
        public ActionResult Index()
        {
            EventRepository eventRepository = RepositoryFactory.GetEventRepository();
            ViewData["events"] = eventRepository.GetAll();
            return View();
        }

        public ActionResult EventDetails(int? id)
        {
            EventRepository eventRepository = RepositoryFactory.GetEventRepository();
            ViewData["event"] = eventRepository.GetById(id.Value);
            return View();
        }

        [HttpGet]
        public ActionResult EditEvent(int? id)
        {
            EventRepository eventRepository = RepositoryFactory.GetEventRepository();

            Event even = null;
            if (id == null)
                even = new Event();
            else
                even = eventRepository.GetById(id.Value);

            ViewData["event"] = even;

            return View();
        }

        [HttpPost]
        public ActionResult EditEvent(Event even)
        {   
            EventRepository eventRepository = RepositoryFactory.GetEventRepository();
            eventRepository.Save(even);
            int x = DateTime.Compare(even.StartDate, even.EndDate);
            if (x > 0)
            {
                return RedirectToAction("EditDates", "EventManagement", new { id = even.Id });
            }

            return RedirectToAction("Index", "EventManagement");
        }

        public ActionResult DeleteEvent(int id)
        {
            EventRepository eventRepository = RepositoryFactory.GetEventRepository();
            Event even = eventRepository.GetById(id);
            eventRepository.Delete(even);

            return RedirectToAction("Index", "EventManagement");
        }

        [HttpGet]
        public ActionResult EditDates(int? id)
        {
            EventRepository Repo = RepositoryFactory.GetEventRepository();

            if (id == null)
            {
                return RedirectToAction("EditEvent", "EventManagement", new { id = id });
            }

            else
            {
                Event even = Repo.GetById(id.Value);
                ViewData["event"] = even;
                return View();
            } 
        }
        

        [HttpPost]
        public ActionResult EditDates(Event even)
        {
            EventRepository Repo = RepositoryFactory.GetEventRepository();
            Repo.Save(even);
            int x = DateTime.Compare(even.StartDate, even.EndDate);
            if (x > 0)
            {
                return RedirectToAction("EditDates", "EventManagement", new { id = even.Id });
            }

            return RedirectToAction("Index", "EventManagement");
        }
    }
}
