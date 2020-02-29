using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TourMarket.DAL;
using TourMarket.Entities;
using TourMarket.Interfaces;
using System.Text;

namespace TourMarketApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserService userService;
        
        public BaseController(IUserService userService)
        {
            this.userService = userService;
        }
        protected User GetUser()
        {
            if (HttpContext.Session.TryGetValue("login", out byte[] value))
            {
                var email = Encoding.ASCII.GetString(value);
                var user = userService.GetUser(email);
                return user;
            }
            return null;
        }
    }
}