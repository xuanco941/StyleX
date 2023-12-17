using Microsoft.AspNetCore.Mvc;

namespace StyleX.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

    }
}
