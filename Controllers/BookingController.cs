using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencySystem.Data;
using TravelAgencySystem.Models;
using TravelAgencySystem.Services;

namespace TravelAgencySystem.Controllers
{

    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ ADD THIS METHOD HERE (INSIDE CLASS)
        public bool IsAdmin()
        {
            return HttpContext.Session.GetString("AdminAuth") == "true";
        }
        // VIEW ALL BOOKINGS (ADMIN)
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bookings= _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .ToList();

            return View(bookings);
        }

        // CREATE BOOKING
        [HttpGet]
        public IActionResult Create(int packageId)
        {
            var package = _context.Packages
                .FirstOrDefault(p => p.PackageId == packageId);

            if (package == null)
            {
                return NotFound();
            }
            ViewBag.Package = package;
            ViewBag.Customers = _context.Persons.OfType<Customer>().ToList(); // ✅ REQUIRED FIX

            var booking = new Booking
            {
                PackageId = package.PackageId,
                Package = package
            };

            return View(booking);
        }

        // SAVE BOOKING (OPTIONAL BASIC VERSION)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TravelAgencySystem.Models.Booking booking)
        {
            Console.WriteLine("Booking Submitted");
            if (ModelState.IsValid)
            {

                Console.WriteLine("Model Invalid");

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewBag.Package = _context.Packages
                    .FirstOrDefault(p => p.PackageId == booking.PackageId);

                ViewBag.Customers = _context.Persons.ToList();

                return View(booking);
                //  _context.Bookings.Add(booking);
                //   _context.SaveChanges();

                //   return RedirectToAction("Index");
            }

            Console.WriteLine("Saving...");

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            Console.WriteLine("Saved");

            // ✅ ADD THIS PART (Export Service)
            var exportService = new BookingExportService(booking);
            exportService.Export();

            return RedirectToAction(nameof(Index));

            //  ViewBag.Package = _context.Packages
            //     .FirstOrDefault(p => p.PackageId == booking.PackageId);

            //  ViewBag.Customers = _context.Customers.ToList();

            //  return View(booking);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return Unauthorized();

            var booking = _context.Bookings.Find(id);
            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return RedirectToAction("Manage");
        }

        //Update[post]
        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();

            return RedirectToAction("Manage");
        }

        //Edit
        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
            {
                return Unauthorized();
            }

            var booking = _context.Bookings.Find(id);
            return View(booking);
        }

        public IActionResult Manage()
        {
            if (!IsAdmin())
                return RedirectToAction("AdminLogin", "Account");

            var bookings = _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .ToList();

            return View(bookings);
        }
    }
}