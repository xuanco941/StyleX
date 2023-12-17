using Microsoft.AspNetCore.Mvc;

namespace StyleX.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

    }
}
