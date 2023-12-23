using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StyleX.Models;
using System.Diagnostics;

namespace StyleX.Controllers
{
    [Authorize(AuthenticationSchemes = Common.CookieAuthAdmin)]
    public class AdminDashboardController : Controller
    {
        [HttpGet]
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }


    }
}