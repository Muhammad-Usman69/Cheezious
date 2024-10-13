using Cheezious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cheezious.Controllers
{
    public class HomeController : Controller
    {
        private readonly CheeziousContext _context;

        public HomeController(CheeziousContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult EmtyCart()
        {
            return View();
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
