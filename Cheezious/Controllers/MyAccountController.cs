using Cheezious.Models;
using Cheezious.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cheezious.Controllers
{
    
    public class MyAccountController : Controller
    {
        private readonly CheeziousContext _context;
        public MyAccountController (CheeziousContext context)
        {
            _context = context;
        }
        public IActionResult MyAccount( int id)
        {

            var users = _context.Users.Where(x=>x.Id ==  id)
                .Include(x=>x.Role).FirstOrDefault();
           
            if (users.Role.Id == 3) // if USer is Buyer
            {
                var orders = _context.Orders.Where(x => x.BuyerId == id)
               .Include(x => x.Product)
               .Include(x => x.Buyer)
               .Include(x => x.OrderStatus)
               
               
               .ToList();

                var viewModel = new UserViewModel
                {
                    Users = users,
                    Orders = orders,
                    AssignOrders = new List<AssignOrder>()
                };

                return View(viewModel);


            }
            else if (users.Role.Id == 2) // if USer is Rider
            {
                var assignedOrders = _context.AssignOrders.Where(x => x.RiderId == id)
                    .Include(x => x.Order)
                    .ThenInclude(x=>x.Buyer)
                    
                    .Include(x => x.Order)
               .ThenInclude(x => x.OrderStatus)
               
               .Include(x => x.Order)
               .ThenInclude(x => x.Product)

               .Include(x=>x.Rider)
                    .ToList();

                var viewModel = new UserViewModel
                {
                    Users = users,
                    Orders = new List<Order>(),
                    AssignOrders = assignedOrders
                };

                return View(viewModel);
            }

            else // If User is Admin
            {

                var orders = _context.Orders
                .Include(x => x.Product)
               .Include(x => x.Buyer)
               .Include(x => x.OrderStatus)
                .ToList();

                var assignedOrders = _context.AssignOrders
                   .Include(x => x.Order)
                   .ThenInclude(x => x.Buyer)

                   .Include(x => x.Order)
              .ThenInclude(x => x.OrderStatus)

              .Include(x => x.Order)
              .ThenInclude(x => x.Product)

              .Include(x => x.Rider)
                   .ToList();

                var viewModel = new UserViewModel
                {
                    Users = users,
                    Orders =  orders,
                    AssignOrders = assignedOrders,
                };

                return View(viewModel);
            }
        }


        [HttpPost]
        public IActionResult AddUpdateUserAccount(User users, IFormFile file)
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
            return RedirectToAction("Home" , "Home");


        }

    }
}
