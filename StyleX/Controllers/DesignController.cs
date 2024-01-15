using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleX.DTOs;
using StyleX.Models;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        [HttpPost]
        public IActionResult GetProductSetting([FromBody] IDModel model)
        {
            try
            {
                var cartItem = _dbContext.CartItems.FirstOrDefault(e => e.CartItemID == model.ID);

                if (cartItem == null)
                {
                    return new NotFoundObjectResult(new { status = -98, message = "Mẫu thiết kế này không tồn tại", data = DBNull.Value });
                }

                var query1 = from ps in _dbContext.ProductSettings
                             join psm in _dbContext.ProductSettingMaterials on ps.ProductSettingID equals psm.ProductSettingID into leftJoinT
                             from psml in leftJoinT.DefaultIfEmpty()
                             join m in _dbContext.Materials on psml.MaterialID equals m.MaterialID into leftJoinTable
                             from mat in leftJoinTable.DefaultIfEmpty()
                             where ps.ProductID == cartItem.ProductID
                             select new
                             {
                                 ps.ProductSettingID,
                                 ps.ProductPartNameDefault,
                                 ps.ProductPartNameCustom,
                                 ps.IsDefault,
                                 ps.NameMaterialDefault,
                                 ps.AoMap,
                                 ps.NormalMap,
                                 ps.RoughnessMap,
                                 ps.MetalnessMap,
                                 material = mat
                             };

                var query2 = from q in query1
                             group q by q.ProductSettingID into g
                             select new
                             {
                                 productSettingID = g.Key,
                                 nameDefault = g.First().ProductPartNameDefault,
                                 nameCustom = g.First().ProductPartNameCustom,
                                 isDefault = g.First().IsDefault,
                                 nameMaterialDefault = g.First().NameMaterialDefault,
                                 aoMap = g.First().AoMap,
                                 normalMap = g.First().NormalMap,
                                 roughnessMap = g.First().RoughnessMap,
                                 metalnessMap = g.First().MetalnessMap,
                                 materials = g.Select(x => x.material).ToList()
                             };

                return new OkObjectResult(new { status = 1, message = "success", data = query2.ToList() });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = DBNull.Value });
            }
        }
        [HttpPost]
        public IActionResult GetMaterials([FromBody] IDModel model)
        {
            try
            {

                var query1 = _dbContext.ProductSettingMaterials.Include(e => e.Material).Where(e => e.ProductSettingID == model.ID).ToList();
                var result = from p in query1
                             where p.Material.Status == true
                             select new
                             { materialID = p.Material.MaterialID, name = p.Material.Name, preview = p.Material.Url, aoMap = p.Material.AoMap, normalMap = p.Material.NormalMap, roughnessMap = p.Material.RoughnessMap, metalnessMap = p.Material.MetalnessMap };
                return new OkObjectResult(new { status = 1, message = "success", data = result.ToList() });



            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = DBNull.Value });
            }
        }

    }
}
