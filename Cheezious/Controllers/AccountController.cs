using Azure;
using Cheezious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cheezious.Controllers
{
    public class AccountController : Controller
    {
        private readonly CheeziousContext _context;
        public AccountController(CheeziousContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserRegister()
        {
            return View();
        }
       
    
        [HttpPost]
        public IActionResult Login(User user)
        {
            User dbuser = _context.Users.Where(x=>x.Email.Equals(user.Email.ToLower()) && x.Password.Equals(user.Password)).FirstOrDefault();
            if (dbuser == null)
            {
                ViewBag.Error = "Email or Password is incorrect";
            return View();
            }

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("user-cookies" , dbuser.AccessToken, cookieOptions);
            return RedirectToAction("Home", "Home");

        }

        [HttpGet]
        public IActionResult Register(int id = 0)
        {


            ViewBag.Role = _context.Roles
                   .Where(x => x.Name == "Buyer" || x.Name == "Rider")
                   .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                   .ToList();
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
        public IActionResult Register(User user , IFormFile file)
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
                    user.Image = name;
                }

                if (!string.IsNullOrEmpty(ViewBag.Error))
                {
                    return View(user);
                    
                }

            }


            user.JoinedOn = DateTime.UtcNow.AddHours(5);
            user.AccessToken = DateTime.UtcNow.Ticks.ToString();
            _context.Users.Add(user);
            _context.SaveChanges();
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("user-cookies", user.AccessToken, cookieOptions);
            return RedirectToAction("Home", "Home");

        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user-cookies");
            return RedirectToAction("Home", "Home");
        }


    }
}
