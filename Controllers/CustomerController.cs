using Microsoft.AspNetCore.Mvc;
using TravelAgencySystem.Data;
using TravelAgencySystem.Models;

namespace TravelAgencySystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Persons.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer c)
        {
            _context.Persons.Add(c);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var data = _context.Persons.Find(id);
            _context.Persons.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}