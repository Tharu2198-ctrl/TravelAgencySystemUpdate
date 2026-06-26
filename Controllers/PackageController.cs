using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencySystem.Data;
using TravelAgencySystem.Models;

namespace TravelAgencySystem.Controllers
{
    public class PackageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PackageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ
        /*public IActionResult Index()
        {
            var data = _context.Packages.Include(p => p.Destination).ToList();
            return View(data);
        }*/

        // CREATE
        public IActionResult Create()
        {
            ViewBag.Destinations = _context.Destinations.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Package p)
        {
            _context.Packages.Add(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // SHOW PACKAGES (FILTER BY COUNTRY)
        public IActionResult Index(string country)
        {
            var packages = _context.Packages.AsQueryable();

            if (!string.IsNullOrEmpty(country))
            {
                country = country.Trim();

                packages = packages.Where(p => p.Country != null && p.Country == country);
            }

            return View(packages.ToList());
        }

        // PACKAGE DETAILS PAGE
        public IActionResult Details(int id)
        {
            var package = _context.Packages
                .Include(p => p.Destination)
                .FirstOrDefault(p => p.PackageId == id);

            return View(package);
        }
        /*public IActionResult Index()
        {
            return View(_context.Packages.ToList());
        }

        public IActionResult Filter(string country)
        {
            var packages = _context.Packages.Where(p => p.Country == country).ToList();
            return View("Index", packages);
        }*/

        // EDIT
        public IActionResult Edit(int id)
        {
            ViewBag.Destinations = _context.Destinations.ToList();
            return View(_context.Packages.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Package p)
        {
            _context.Packages.Update(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var data = _context.Packages.Find(id);
            _context.Packages.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Search(string keyword)
        {
            var result = _context.Packages
                .Where(p => p.PackageName.Contains(keyword))
                .ToList();

            return View("Index", result);
        }

        
        
    }
}