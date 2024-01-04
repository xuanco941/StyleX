using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleX.DTOs;
using StyleX.Models;
using System.Security.Claims;

namespace StyleX.Controllers
{
    [Authorize(AuthenticationSchemes = Common.CookieAuthUser)]
    public class CartController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public CartController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart([FromBody] IDModel model)
        {
            try
            {
                string accountID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var product = _dbContext.Products.Find(model.ID);
                if(string.IsNullOrEmpty(accountID) || product == null)
                {
                    return new OkObjectResult(new { status = -1, message = "Không khả dụng." });
                }

                int checkCart = _dbContext.CartItems.Where(e => e.ProductID == model.ID && e.AccountID == Convert.ToInt32(accountID)).Count();
                if (checkCart > 0)
                {
                    return new OkObjectResult(new { status = 2, message = "Sản phẩm đã có trong giỏ hàng." });
                }

                _dbContext.CartItems.Add(new CartItem()
                {
                   ProductID = model.ID,
                   AccountID = Convert.ToInt32(accountID),
                   Amount = 0,
                   Size = "",
                   PosterUrl= product.PosterUrl,
                   Status = 0,
                   OrderID = null
                });
                _dbContext.SaveChanges();
                return new OkObjectResult(new { status = 1, message = "Đã thêm vào giỏ hàng." });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message });
            }

        }


        [HttpPost]
        public IActionResult GetCarts()
        {
            try
            {
                string accountID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //status = 0 là những đơn trong cart, được phép design
                var listCart = _dbContext.CartItems.Include(e => e.Product).Where(c => c.AccountID == Convert.ToInt32(accountID) && c.Status == 0).ToList();

                return new OkObjectResult(new { status = 1, message = "success", data = listCart });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = new List<CartItem>() });
            }

        }

    }
}
