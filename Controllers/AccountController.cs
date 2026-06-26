using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using TravelAgencySystem.Data;
using TravelAgencySystem.Helpers;
using TravelAgencySystem.Models;

namespace TravelAgencySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // REGISTER (GET)
        public IActionResult Register()
        {
            return View();
        }

        // REGISTER (POST)
        [HttpPost]
        public IActionResult Register(Customer c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    c.Role = "Customer";   // 🔥 IMPOR

                    _context.Add(c);
                    _context.SaveChanges();

                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            foreach (var item in ModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(c);
        }

        // LOGIN (GET)
        public IActionResult Login()
        {
            return View();
        }

        // LOGIN (POST)
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            /*  var user = _context.Users
                  .FirstOrDefault(u => u.Email == email && u.Password == password);*/
            var user = _context.Persons
                  .FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user != null)
            {
                /*  HttpContext.Session.SetInt32("UserId", user.UserId);
                 HttpContext.Session.SetString("Role", user.Role); // 👈 ADD THIS*/
                   HttpContext.Session.SetInt32(SessionHelper.UserId, user.UserId);
                   HttpContext.Session.SetString(SessionHelper.UserName, user.FullName);
                   HttpContext.Session.SetString("Role", user.Role);
                    // ROLE BASED REDIRECT
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else if (user.Role == "Customer")
                    {
                        return RedirectToAction("Index", "Home");
                    }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Email or Password";
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        {
            var admin = _context.Persons
                 .FirstOrDefault(a =>
                    a.Email == username &&
                    a.Password == password &&
                    a.Role == "Admin");

            if (admin != null)
            {
                HttpContext.Session.SetString("AdminAuth", "true");
                HttpContext.Session.SetInt32(SessionHelper.UserId, admin.UserId);
                HttpContext.Session.SetString(SessionHelper.UserName, admin.FullName);

                return RedirectToAction("AdminDashboard", "Admin");
            }

            ViewBag.Error = "Invalid admin login";
            return View();
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}