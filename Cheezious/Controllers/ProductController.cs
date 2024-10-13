using Cheezious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApplication;
using ShoppingApplication.Controllers;

namespace Cheezious.Controllers
{
  
    public class ProductController : Controller
    {
        private readonly CheeziousContext _context;
        public ProductController(CheeziousContext context)
        {
            _context = context;
        }

        public IActionResult Products()
        { 


            var products = _context.Products
                .Include(p => p.Category)
                .ToList();

            var query = products
                .GroupBy(p => p.Category.Name)
                .ToDictionary(g => g.Key, g => g.ToList());

            return View(query);
        }

        public IActionResult OrderProducts(int id)
        {


            return View(_context.Products.Where(x => x.Id.Equals(id)).FirstOrDefault());

        }
        [Buyer]
        public IActionResult Orders(int id)

        {
            int userId = new CommonController(_context).GetUserId(Request);
            Order order = new Order()
            {
                OrderStatusId = _context.OrderStatuses.Where(x => x.Name == "Pending").Select(x => x.Id).FirstOrDefault(),
                BuyerId = userId,
                CreatedOn = DateTime.UtcNow.AddHours(5),
                ProductId = id

            };
            _context.Add(order);
            _context.SaveChanges();

            Product product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            product.ProductStatusId = _context.ProductStatuses.Where(x => x.Name.Equals("Sold")).Select(x => x.Id).FirstOrDefault();
            _context.SaveChanges();

            return Redirect("/Product/Products");
        }
    }
}

