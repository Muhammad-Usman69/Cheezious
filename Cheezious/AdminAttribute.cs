﻿using Cheezious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Cheezious
{
    public class AdminAttribute : Attribute, IActionFilter
    {


        public void OnActionExecuting(ActionExecutingContext context)
        {
            string accessToken = context.HttpContext.Request.Cookies["user-cookies"];
            if (accessToken != null)
            {

                CheeziousContext _db = context.HttpContext.RequestServices.GetService<CheeziousContext>();
                User user = _db.Users.Where(x => x.AccessToken == accessToken && x.Role.Name == "Admin").FirstOrDefault();
                if (user == null)
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}