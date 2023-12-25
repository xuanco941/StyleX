using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StyleX.Models;
using System.Diagnostics;

namespace StyleX.Controllers
{
    [Authorize(AuthenticationSchemes = Common.CookieAuthAdmin)]
    public class AdminController : Controller
    {
        [HttpGet]
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Material()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Promotion()
        {
            return View();
        }
        public IActionResult Warehouse()
        {
            return View();
        }

    }
}