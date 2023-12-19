using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StyleX.Controllers
{
    public class MiniGameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
