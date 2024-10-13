using Cheezious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingApplication;

namespace Cheezious.Controllers
{
    [Buyer]
    public class BuyerController : Controller
    {
        private readonly CheeziousContext _context;

        public BuyerController(CheeziousContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Orders(int id)
        {
            List<Order> orders = _context.Orders.Include(x => x.Product).Include(x => x.Buyer).ThenInclude(x => x.Role).ToList();
            return View(orders);
        }
        [HttpGet]
        public IActionResult AddUpdateOrders(int id = 0)
        {
            ViewBag.Products = _context.Products.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
            ViewBag.Buyers = _context.Users.Where(x => x.Role.Name.Equals("Buyer")).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.OrderStatuses = _context.OrderStatuses.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            if (id == 0)
            {

                return View();
            }
            else
            {
                Order orders = _context.Orders.Find(id);
                return View(orders);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateOrders(Order orders)
        {
            orders.CreatedOn = DateTime.UtcNow.AddHours(5);
            _context.Orders.Update(orders);
            _context.SaveChanges();
            return RedirectToAction("Orders");
        }
        [HttpGet]
        public IActionResult DeleteOrders(int id)
        {
            var users = _context.Users.Where(x => x.Id == id)
                 .Include(x => x.Role).FirstOrDefault();
            Order orders = _context.Orders.Find(id);
            _context.Orders.Remove(orders);
            _context.SaveChanges();
             
            return Redirect("/Home/Home");
            
        }
        [HttpGet]
        public IActionResult ViewOrders(int id)
        {
            Order orders = _context.Orders.Where(x => x.Id == id).Include(x => x.Buyer).Include(x => x.Product).Include(x => x.OrderStatus).FirstOrDefault();
            return View(orders);
        }
        public IActionResult OrderDetails(int id)
        {
            Order orders = _context.Orders.Where(x => x.Id == id).Include(x => x.Buyer).Include(x => x.Product).Include(x => x.OrderStatus).FirstOrDefault();
            return View(orders);
        }


    }

}