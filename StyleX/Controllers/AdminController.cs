using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StyleX.DTOs;
using StyleX.Models;
using StyleX.Utils;
using System.Diagnostics;

namespace StyleX.Controllers
{
    [Authorize(AuthenticationSchemes = Common.CookieAuthAdmin)]
    public class AdminController : Controller
    {
        private readonly DatabaseContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public AdminController(DatabaseContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        [HttpGet]
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Promotion()
        {
            return View();
        }
        public IActionResult Warehouse()
        {
            return View();
        }

        #region Material
        [HttpGet]
        public IActionResult Material()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMaterial([FromBody] AddMaterialModel addMaterialModel)
        {
            try
            {
                if (addMaterialModel.file != null && addMaterialModel.file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(addMaterialModel.file.FileName);

                    var filePath = Path.Combine(_environment.WebRootPath, "materials", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        addMaterialModel.file.CopyTo(stream);
                    }

                    _dbContext.Materials.Add(new Material() { Name = addMaterialModel.name, Status = addMaterialModel.status, Url = $"/material/{fileName}" });
                    _dbContext.SaveChanges();
                    return new OkObjectResult(new { status = 1, message = "Tải lên chất liệu mới thành công." });

                }
                else
                {
                    return new OkObjectResult(new { status = -1, message = "File không khả dụng." });
                }

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message });
            }

        }
        public IActionResult GetMaterials()
        {
            try
            {
                var result = _dbContext.Materials.ToList();
                return new OkObjectResult(new { status = 1, message = "success", data = result});

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = DBNull.Value });
            }

        }

        #endregion

    }
}