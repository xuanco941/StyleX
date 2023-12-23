using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StyleX.Controllers
{
    public class DesignController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

    }
}
