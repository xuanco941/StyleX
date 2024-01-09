using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleX.Models;
using System.Security.Claims;

namespace StyleX.Controllers
{
    [Authorize(AuthenticationSchemes = Common.CookieAuthUser)]
    public class DesignController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public DesignController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index(int? id)
        {
            if (id.HasValue == true)
            {
                string accountID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(accountID) == false)
                {
                    var cartItem = _dbContext.CartItems.Include(e => e.Product).FirstOrDefault(x => x.CartItemID == id.Value && x.AccountID == Convert.ToInt32(accountID));
                    if (cartItem != null)
                    {
                        ViewBag.id = id;
                        ViewBag.src = cartItem.Product.ModelUrl;
                    }
                }

            }
            return View();
        }
        [HttpPost]
        public IActionResult GetCartItems()
        {
            List<CartItem> cartItems = new List<CartItem>();
            try
            {
                string accountID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //status = 0 là những đơn trong cart, design

                if (string.IsNullOrEmpty(accountID) == false)
                {
                    cartItems = _dbContext.CartItems.Include(e => e.Product).Where(e => e.AccountID == Convert.ToInt32(accountID) && e.Status == 0).ToList();
                }

                return new OkObjectResult(new { status = 1, message = "success", data = cartItems });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = cartItems });
            }
        }
        [HttpPost]
        public IActionResult GetProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                products = _dbContext.Products.Where(e => e.Status == true).ToList();

                return new OkObjectResult(new { status = 1, message = "success", data = products });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = products });
            }
        }

    }
}
