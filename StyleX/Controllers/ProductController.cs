using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleX.DTOs;
using StyleX.Models;

namespace StyleX.Controllers
{
    public class ProductController : Controller
    {
        private readonly DatabaseContext _dbContext;
        public ProductController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            ViewBag.pageName = "Sản phẩm";
            return View();
        }
        [HttpGet("product/detail/{productID}")]
        public IActionResult Detail(int productID)
        {
            ViewBag.pageName = "Sản phẩm";

            Product? product = _dbContext.Products.Find(productID);
            ViewBag.product = product;


            return View();
        }
        public IActionResult GetProducts([FromBody] IDModel model)
        {
            try
            {
                var query1 = from p in _dbContext.Products
                             join c in _dbContext.Categories on p.CategoryID equals c.CategoryID
                             join wh in _dbContext.Warehouses on p.ProductID equals wh.ProductID into leftJoinTableW
                             from w in leftJoinTableW.DefaultIfEmpty()
                             where (model.ID == 0 || c.CategoryID == model.ID) && p.Status == true
                             select new
                             {
                                 p.ProductID,
                                 p.PosterUrl,
                                 p.Name,
                                 p.Description,
                                 p.Price,
                                 p.Sale,
                                 p.SaleEndAt,
                                 p.Status,
                                 Warehouse = w,
                                 CategoryID = c.CategoryID,
                                 CategoryName = c.Name,
                                 p.ModelUrl

                             };

                var query2 = from p in query1
                             group p by p.ProductID into g
                             select new
                             {
                                 ProductID = g.Key,
                                 PosterUrl = g.First().PosterUrl,
                                 Name = g.First().Name,
                                 Description = g.First().Description,
                                 Price = g.First().Price,
                                 Sale = g.First().Sale,
                                 SaleEndAt = g.First().SaleEndAt,
                                 Status = g.First().Status,
                                 Warehouses = g.Select(x => x.Warehouse).ToList(),
                                 CategoryID = g.First().CategoryID,
                                 CategoryName = g.First().CategoryName,
                                 ModelUrl = g.First().ModelUrl

                             };
                return new OkObjectResult(new { status = 1, message = "success", data = query2.ToList() });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = DBNull.Value });
            }

        }

    }
}
