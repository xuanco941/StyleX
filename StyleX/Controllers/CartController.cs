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

                //int checkCart = _dbContext.CartItems.Where(e => e.ProductID == model.ID && e.AccountID == Convert.ToInt32(accountID)).Count();
                //if (checkCart > 0)
                //{
                //    return new OkObjectResult(new { status = 2, message = "Sản phẩm đã có trong giỏ hàng." });
                //}

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
        public IActionResult DeleteCartItem([FromBody] IDModel model)
        {
            try
            {
                string accountID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var cartItem = _dbContext.CartItems.FirstOrDefault(e => e.CartItemID==model.ID && e.AccountID == Convert.ToInt32(accountID));
                if (cartItem == null)
                {
                    return new OkObjectResult(new { status = -1, message = "Không khả dụng." });
                }
                else
                {
                    _dbContext.CartItems.Remove(cartItem);
                    _dbContext.SaveChanges();
                    return new OkObjectResult(new { status = 1, message = "Xóa thành công." });
                }

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

                //status = 0 là những đơn trong cart, design
                var query = from c in _dbContext.CartItems.Include(c => c.Product)
                            join wh in _dbContext.Warehouses on c.ProductID equals wh.ProductID into leftJoinTableW
                            from w in leftJoinTableW.DefaultIfEmpty()
                            where c.AccountID == Convert.ToInt32(accountID) && c.Status == 0
                            select new
                            {
                                c.CartItemID,
                                c.ProductID,
                                c.PosterUrl,
                                c.Amount,
                                c.Size,
                                c.Status,
                                Warehouse = w,
                                c.Product
                            };

                var query2 = from c in query
                             group c by c.CartItemID into g
                             select new
                             {
                                 CartItemID = g.Key,
                                 ProductID = g.First().ProductID,
                                 PosterUrl = g.First().PosterUrl,
                                 Status = g.First().Status,
                                 Amount = g.First().Amount,
                                 Size = g.First().Size,

                                 Warehouses = g.Select(x => x.Warehouse).ToList(),
                                 Product = g.First().Product
                             };

                return new OkObjectResult(new { status = 1, message = "success", data = query2.ToList() });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = new List<CartItem>() });
            }

        }

    }
}
