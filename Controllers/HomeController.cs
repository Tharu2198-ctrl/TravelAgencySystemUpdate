using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TravelAgencySystem.Data;
using TravelAgencySystem.Models;

namespace TravelAgencySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        // CONSTRUCTOR
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // HOME PAGE
        public IActionResult Index(string country)
            {
                try
                {
                   // var featuredPackages = _context.Packages.ToList();
                    var packages = _context.Packages
                        .Include(p => p.Destination)
                        .AsQueryable();
                    // .ToList();
                    // FILTER BY COUNTRY (Paris / Maldives / Dubai)
                    if (!string.IsNullOrEmpty(country))
                    {
                        packages = packages.Where(p => p.Country == country);
                    }
                    return View(packages.ToList());
                
                //return View(featuredPackages);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View(new List<Package>());

                //return View();
                 }
            }

    

        // COUNTRY PAGE (Paris / Maldives / Dubai)
        public IActionResult Country(string name)
        {
            ViewBag.Country = name?.Trim();
            return View();
        }


        // DESTINATIONS PAGE
        public IActionResult Destinations()
        {
            var destinations = _context.Destinations
                                       .ToList();

            return View(destinations);
        }

        // ABOUT PAGE
        public IActionResult About()
        {
            return View();
        }

        // CONTACT PAGE (GET)
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // CONTACT PAGE (POST)
        [HttpPost]
        public IActionResult Contact(string name, string email, string message)
        {
            // Validation
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Name and Email are required!";
                return View();
            }

            // Success message
            ViewBag.Success = "Message sent successfully!";

            return View();
        }
    }
}