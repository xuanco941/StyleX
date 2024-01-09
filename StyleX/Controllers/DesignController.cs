using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StyleX.Models;

namespace StyleX.Controllers
{
    public class DesignController : Controller
	{
        private readonly DatabaseContext _dbContext;
        public DesignController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int? id)
		{
            if (id.HasValue == true)
            {
            }
            return View();
		}

    }
}
