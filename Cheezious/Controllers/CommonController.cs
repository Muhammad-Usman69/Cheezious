using Cheezious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cheezious.Models;

namespace ShoppingApplication.Controllers
{
    public class CommonController : Controller
    {
        private readonly CheeziousContext _context;

        public CommonController(CheeziousContext context)
        {
            _context = context;
        }
        public int GetUserId(HttpRequest httpRequest)
        {
            string accessToken = httpRequest.Cookies["user-cookies"];
            if (accessToken != null)
            {
                return _context.Users.Where(x => x.AccessToken == accessToken).Select(x => x.Id).FirstOrDefault();
            }
            return 0;
        }

      



    }
}
