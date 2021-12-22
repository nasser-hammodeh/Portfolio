using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Repositories;

namespace Portfolio.Controllers
{
    public class DefaultHomeController : Controller
    {
        private readonly IUserRepository userRepository;
        public DefaultHomeController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public IActionResult newhome()
        {if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = userRepository.GetUserById(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            return View();

        }
        public IActionResult UpdateError()
        {
            return View();
        }
        public IActionResult DeleteError()
        {
            return View();
        }
        public IActionResult CreateError()
        {
            return View();
        }
    }
}
