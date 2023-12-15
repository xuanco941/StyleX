using Microsoft.AspNetCore.Mvc;

namespace StyleX.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
