using Feedback.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FeedBackDB _context;

        public HomeController(ILogger<HomeController> logger, FeedBackDB FeedbackDB)
        {
            _logger = logger;
            _context = FeedbackDB;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(User user)
        {
            if(ModelState.IsValid)
            {
                user.Status = 1;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Users");
            }
            ModelState.AddModelError("", "Error");
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
