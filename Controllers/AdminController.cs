using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencySystem.Data;
using TravelAgencySystem.Models;

namespace TravelAgencySystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔥 ADMIN DASHBOARD
        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return Unauthorized();

            var bookings = _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .ToList();

            return View(bookings);
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            var booking = _context.Bookings.Find(id);
            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
            return RedirectToAction("AdminDashboard");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return RedirectToAction("AdminDashboard");
        }
    }
}