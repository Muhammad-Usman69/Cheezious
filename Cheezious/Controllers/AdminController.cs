using Microsoft.AspNetCore.Mvc.Rendering;
using Cheezious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Cheezious;


namespace Cheezious.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        private readonly CheeziousContext _context;
        public AdminController(CheeziousContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            ViewBag.Orders = _context.Orders.Count();
            ViewBag.AssignOrders = _context.AssignOrders.Count();
            ViewBag.Products = _context.Products.Count();
            ViewBag.Users = _context.Users.Count();
            return View();
        }

        // AssignOrders


        [HttpGet]
        public IActionResult AssignOrders()
        {
            List<AssignOrder> assignOrders = _context.AssignOrders.Include(x => x.Order.Product).Include(x => x.Order.Buyer).ThenInclude(x => x.Role).Include(x => x.Rider).ToList();
            return View(assignOrders);
        }
        [HttpGet]
        public IActionResult AddUpdateAssignOrders(int id = 0)
        {
            ViewBag.Orders = _context.Orders.Select(x => new SelectListItem { Text = x.Product.Title, Value = x.Id.ToString() }).ToList();
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
            AssignOrder assignOrders = _context.AssignOrders.Find(id);
            _context.AssignOrders.Remove(assignOrders);
            _context.SaveChanges();
            return RedirectToAction("AssignOrders");
        }
        [HttpGet]
        public IActionResult ViewAssignOrders(int id)
        {
            AssignOrder assignOrders = _context.AssignOrders.Where(x => x.Id == id).Include(x => x.Order.Buyer).Include(x => x.Order.Product).Include(x => x.Order.OrderStatus).Include(x => x.Rider).FirstOrDefault();
            return View(assignOrders);
        }
        [HttpGet]
        public IActionResult AssignOrdersDetails(int id)
        {
            AssignOrder assignOrders = _context.AssignOrders.Where(x => x.Id == id).Include(x => x.Order.Buyer).Include(x => x.Order.Product).Include(x => x.Order.OrderStatus).FirstOrDefault();
            return View(assignOrders);
        }





        // Categories
        [HttpGet]
        public IActionResult Categories()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult AddUpdateCategories(int id = 0)
        {
            if (id == 0)
            {

                return View();
            }
            else
            {
                Category categories = _context.Categories.Find(id);
                return View(categories);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateCategories(Category categories)
        {
            _context.Categories.Update(categories);
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public IActionResult DeleteCategories(int id)
        {
            Category categories = _context.Categories.Find(id);
            _context.Categories.Remove(categories);
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }
        [HttpGet]
        public IActionResult ViewCategories(int id)
        {
            Category categories = _context.Categories.Find(id);
            return View(categories);
        }
        // Order

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
            Order orders = _context.Orders.Find(id);
            List<AssignOrder> assignOrders = _context.AssignOrders.Where(x => x.OrderId == id).ToList();
            foreach (AssignOrder assignOrder in assignOrders)
            {
                _context.AssignOrders.Remove(assignOrder);
            }
            _context.Orders.Remove(orders);
            _context.SaveChanges();
            return RedirectToAction("Orders");
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



        // OrderStatus

        [HttpGet]
        public IActionResult OrderStatuses()
        {
            List<OrderStatus> orderStatuses = _context.OrderStatuses.ToList();
            return View(orderStatuses);
        }
        [HttpGet]
        public IActionResult AddUpdateOrderStatuses(int id = 0)
        {
            if (id == 0)
            {

                return View();
            }
            else
            {
                OrderStatus orderStatuses = _context.OrderStatuses.Find(id);
                return View(orderStatuses);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateOrderStatuses(OrderStatus orderStatuses)
        {
            _context.OrderStatuses.Update(orderStatuses);
            _context.SaveChanges();
            return RedirectToAction("OrderStatuses");
        }
        [HttpGet]
        public IActionResult DeleteOrderStatuses(int id)
        {
            OrderStatus orderStatuses = _context.OrderStatuses.Find(id);
            _context.OrderStatuses.Remove(orderStatuses);
            _context.SaveChanges();
            return RedirectToAction("OrderStatuses");
        }
        [HttpGet]
        public IActionResult ViewOrderStatuses(int id)
        {
            OrderStatus orderStatuses = _context.OrderStatuses.Find(id);
            return View(orderStatuses);
        }

        //Product


        [HttpGet]
        public IActionResult Products()
        {
            List<Product> products = _context.Products.ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult AddUpdateProducts(int id = 0)
        {
            ViewBag.Categories = _context.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.ProductStatuses = _context.ProductStatuses.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            if (id == 0)
            {

                return View();
            }
            else
            {
                Product products = _context.Products.Find(id);
                return View(products);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateProducts(Product products, IFormFile file)
        {
            List<string> allowedExtention = new List<string> { ".jpg", ".png", ".svg", ".jpeg" };
            int maxSize = 1024 * 1024 * 5;
            if (file != null && file.Length > 0)
            {
                string extention = file.FileName.Substring(file.FileName.LastIndexOf("."));
                if (!allowedExtention.Contains(extention))
                {
                    ViewBag.Error = "Extention NOt Supported";
                }
                else if (file.Length > maxSize)
                {
                    ViewBag.Error = "File must be less than 5mb";
                }
                else
                {
                    string name = DateTime.UtcNow.Ticks.ToString() + extention;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/imgs/product-images/{name}");
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }
                    products.Image = name;
                }

                if (!string.IsNullOrEmpty(ViewBag.Error))
                {
                    return View(products);
                }

            }

            products.AddedOn = DateTime.UtcNow.AddHours(5);
            _context.Products.Update(products);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }
        [HttpGet]
        public IActionResult DeleteProducts(int id)
        {

            Product products = _context.Products.Find(id);
            List<Order> orders = _context.Orders.Where(x => x.ProductId == id).ToList();
            foreach (Order order in orders)
            {
                _context.Orders.Remove(order);
            }
            _context.Products.Remove(products);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }
        [HttpGet]
        public IActionResult ViewProducts(int id)
        {
            Product products = _context.Products.Where(x => x.Id == id).Include(x => x.ProductStatus).Include(x => x.Category).FirstOrDefault();
            return View(products);
        }




        // ProductStatus


        [HttpGet]
        public IActionResult ProductStatuses()
        {
            List<ProductStatus> productStatuses = _context.ProductStatuses.ToList();
            return View(productStatuses);
        }
        [HttpGet]
        public IActionResult AddUpdateProductStatuses(int id = 0)
        {
            if (id == 0)
            {

                return View();
            }
            else
            {
                ProductStatus productStatuses = _context.ProductStatuses.Find(id);
                return View(productStatuses);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateProductStatuses(ProductStatus productStatuses)
        {
            _context.ProductStatuses.Update(productStatuses);
            _context.SaveChanges();
            return RedirectToAction("ProductStatuses");
        }
        [HttpGet]
        public IActionResult DeleteProductStatuses(int id)
        {
            ProductStatus productStatuses = _context.ProductStatuses.Find(id);
            _context.ProductStatuses.Remove(productStatuses);
            _context.SaveChanges();
            return RedirectToAction("ProductStatuses");
        }
        [HttpGet]
        public IActionResult ViewProductStatuses(int id)
        {
            ProductStatus productStatuses = _context.ProductStatuses.Find(id);
            return View(productStatuses);
        }


        //Roles

        [HttpGet]
        public IActionResult Roles()
        {
            List<Roles> roles = _context.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult AddUpdateRoles(int id = 0)
        {
            if (id == 0)
            {

                return View();
            }
            else
            {
                Roles roles = _context.Roles.Find(id);
                return View(roles);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateRoles(Roles roles)
        {

            _context.Roles.Update(roles);
            _context.SaveChanges();
            return RedirectToAction("Roles");
        }
        [HttpGet]
        public IActionResult DeleteRoles(int id)
        {
            Roles roles = _context.Roles.Find(id);
            _context.Roles.Remove(roles);
            _context.SaveChanges();
            return RedirectToAction("Roles");
        }
        [HttpGet]
        public IActionResult ViewRoles(int id)
        {
            Roles roles = _context.Roles.Find(id);
            return View(roles);
        }

        //Users

        [HttpGet]
        public IActionResult Users(int id)
        {
            List<User> users = _context.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public IActionResult AddUpdateUsers(int id = 0)
        {
            ViewBag.Roles = _context.Roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            if (id == 0)
            {

                return View();
            }
            else
            {

                User users = _context.Users.Find(id);
                return View(users);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateUsers(User users, IFormFile file)
        {


            List<string> allowedExtention = new List<string> { ".jpg", ".png", ".svg", ".jpeg" };
            int maxSize = 1024 * 1024 * 5;
            if (file != null && file.Length > 0)
            {
                string extention = file.FileName.Substring(file.FileName.LastIndexOf("."));
                if (!allowedExtention.Contains(extention))
                {
                    ViewBag.Error = "Extention NOt Supported";
                }
                else if (file.Length > maxSize)
                {
                    ViewBag.Error = "File must be less than 5mb";
                }
                else
                {
                    string name = DateTime.UtcNow.Ticks.ToString() + extention;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/imgs/user-images/{name}");
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }
                    users.Image = name;
                }

                if (!string.IsNullOrEmpty(ViewBag.Error))
                {
                    return View(users);
                }
            }

            users.JoinedOn = DateTime.UtcNow.AddHours(5);
            users.AccessToken = DateTime.UtcNow.Ticks.ToString();
            _context.Update(users);
            _context.SaveChanges();
            return RedirectToAction("Users");


        }




        [HttpGet]
        public IActionResult DeleteUsers(int id)
        {
            User users = _context.Users.Find(id);
            var orders = _context.Orders.Where(x => x.BuyerId == id).ToList();
            foreach (var order in orders)
            {
                _context.Orders.Remove(order);
            }
            _context.Users.Remove(users);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }
        [HttpGet]
        public IActionResult ViewUsers(int id)
        {
            User users = _context.Users.Where(x => x.Id == id).Include(x => x.Role).FirstOrDefault();
            return View(users);
        }





    }

}
