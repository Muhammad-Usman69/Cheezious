using Cheezious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingApplication;

namespace Cheezious.Controllers
{
    [Rider]
    public class RiderController : Controller
    {
        private readonly CheeziousContext _context;
        public RiderController (CheeziousContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AssignOrders(int id)
        {
            List<AssignOrder> assignOrders = _context.AssignOrders.Include(x => x.Order.Product).Include(x => x.Order.Buyer).ThenInclude(x => x.Role).Include(x => x.Rider).ToList();
            return View(assignOrders);
        }
        [HttpGet]
        public IActionResult AddUpdateAssignOrders(int id = 0)
        {
            ViewBag.Products = _context.Orders.Select(x => new SelectListItem { Text = x.Product.Title, Value = x.Id.ToString() }).ToList();
            ViewBag.Riders = _context.Users.Where(x => x.Role.Name.Equals("Rider")).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            if (id == 0)
            {

                return View();
            }
            else
            {
                AssignOrder assignOrders = _context.AssignOrders.Find(id);
                return View(assignOrders);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateAssignOrders(AssignOrder assignOrders)
        {
            assignOrders.DeliverdOn = DateTime.UtcNow.AddHours(5);
            _context.AssignOrders.Update(assignOrders);
            _context.SaveChanges();
            return RedirectToAction("AssignOrders");
        }
        [HttpGet]
        public IActionResult DeleteAssignOrders(int id)
        {
            var users = _context.Users.Where(x => x.Id == id)
                .Include(x => x.Role).FirstOrDefault();
            AssignOrder assignOrders = _context.AssignOrders.Find(id);
            _context.AssignOrders.Remove(assignOrders);
            _context.SaveChanges();
            return Redirect("/Home/Home");
        }
        [HttpGet]
        public IActionResult ViewAssignOrders(int id)
        {
            AssignOrder assignOrders = _context.AssignOrders.Where(x => x.Id == id).Include(x => x.Order.Buyer).Include(x => x.Order.Product).Include(x => x.Order.OrderStatus).Include(x => x.Rider).FirstOrDefault();
            return View(assignOrders);
        }
        public IActionResult AssignOrdersDetails(int id)
        {
            AssignOrder assignOrders = _context.AssignOrders.Where(x => x.Id == id).Include(x => x.Order.Buyer).Include(x => x.Order.Product).Include(x => x.Order.OrderStatus).FirstOrDefault();
            return View(assignOrders);
        }



    }
}
