using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Queryable.Web.Models;

namespace Queryable.Web.Controllers
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
            var query = new PersonSearchModelQueryAdapter(searchModel);
            var people = _personRepository.Query(query);
            return View("Index", people);
        }

        [HttpPost]
        public ActionResult DeletePerson(int id)
        {
            var person = _personRepository.Get(id);
            _personRepository.Delete(person);
            _personRepository.SaveChanges();

            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new CreatePersonCommand());
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
