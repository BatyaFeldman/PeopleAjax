using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeopleAjax.Data;

namespace PeopleAjax.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PeopleDb pdb = new PeopleDb(Properties.Settings.Default.conStr);
            IEnumerable<Person> people = pdb.GetAllPeople();
            return View(people);
        }

        [HttpPost]
        public ActionResult AddPerson(Person person)
        {
            PeopleDb pdb = new PeopleDb(Properties.Settings.Default.conStr);
            pdb.AddPerson(person);
            return Json(person);
        }

        public ActionResult GetPeople()
        {
            PeopleDb pdb = new PeopleDb(Properties.Settings.Default.conStr);
            IEnumerable<Person>people=pdb.GetAllPeople();
            return Json(people, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult EditPerson(Person person)
        {
            PeopleDb pdb = new PeopleDb(Properties.Settings.Default.conStr);
            pdb.EditPerson(person);
            return Json(person);

        }

        [HttpPost]
        public ActionResult DeletePerson(int personId)
        {
            PeopleDb pdb = new PeopleDb(Properties.Settings.Default.conStr);
            pdb.DeletePerson(personId);
            return Json(personId);
        }

       
    }

   
}