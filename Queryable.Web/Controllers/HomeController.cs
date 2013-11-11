using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Queryable.Models;

namespace Queryable.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Person> _personRepository;

        public HomeController(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public ActionResult Index()
        {
            var people = _personRepository.Query(PersonSearchModelQueryAdapter.AllPeopleSearchModel);
            return View(people);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View(new PersonSearchModel());
        }

        [HttpGet]
        public ActionResult SearchResults(PersonSearchModel searchModel)
        {
            IEnumerable<Person> people = Enumerable.Empty<Person>();
            if (ModelState.IsValid)
            {
                var query = new PersonSearchModelQueryAdapter(searchModel);
                people = _personRepository.Query(query);
            }

            return View("Index", people);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult Add(Person person)
        {
            _personRepository.Create(person);
            _personRepository.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
