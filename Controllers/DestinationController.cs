using Microsoft.AspNetCore.Mvc;
using TravelAgencySystem.Data;
using TravelAgencySystem.Models;

namespace TravelAgencySystem.Controllers
{
    public class DestinationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DestinationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ
        public IActionResult Index()
        {
            return View(_context.Destinations.ToList());
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        public IActionResult Create(Destination d)
        {
            _context.Destinations.Add(d);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var data = _context.Destinations.Find(id);
            return View(data);
        }

        // EDIT (POST)
        [HttpPost]
        public IActionResult Edit(Destination d)
        {
            _context.Destinations.Update(d);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var data = _context.Destinations.Find(id);
            _context.Destinations.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}