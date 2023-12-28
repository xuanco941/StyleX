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
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Product()
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
        public IActionResult AddMaterial([FromForm] AddMaterialModel addMaterialModel)
        {
            try
            {
                if (addMaterialModel.file != null && addMaterialModel.aoMap != null && addMaterialModel.normalMap != null && addMaterialModel.metalnessMap != null && addMaterialModel.roughnessMap != null)
                {
                    string folderName = Guid.NewGuid().ToString();
                    string fileNameImagePreview = "preview" + Path.GetExtension(addMaterialModel.file.FileName);
                    string fileNameImageAoMap = "aoMap" + Path.GetExtension(addMaterialModel.aoMap.FileName);
                    string fileNameImageNormalMap = "normalMap" + Path.GetExtension(addMaterialModel.normalMap.FileName);
                    string fileNameImageMetalnessMap = "metalnessMap" + Path.GetExtension(addMaterialModel.metalnessMap.FileName);
                    string fileNameImageRoughnessMap = "roughnessMap" + Path.GetExtension(addMaterialModel.roughnessMap.FileName);


                    var filePath1 = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileNameImagePreview);
                    var filePath2 = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileNameImageAoMap);
                    var filePath3 = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileNameImageNormalMap);
                    var filePath4 = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileNameImageMetalnessMap);
                    var filePath5 = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileNameImageRoughnessMap);


                    //tạo folder
                    if (!string.IsNullOrEmpty(filePath1))
                    {
                        string? directoryPath = Path.GetDirectoryName(filePath1);
                        if (directoryPath != null && !Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                    }


                    using (var stream = new FileStream(filePath1, FileMode.Create))
                    {
                        addMaterialModel.file.CopyTo(stream);
                    }
                    using (var stream = new FileStream(filePath2, FileMode.Create))
                    {
                        addMaterialModel.aoMap.CopyTo(stream);
                    }
                    using (var stream = new FileStream(filePath3, FileMode.Create))
                    {
                        addMaterialModel.normalMap.CopyTo(stream);
                    }
                    using (var stream = new FileStream(filePath4, FileMode.Create))
                    {
                        addMaterialModel.metalnessMap.CopyTo(stream);
                    }
                    using (var stream = new FileStream(filePath5, FileMode.Create))
                    {
                        addMaterialModel.roughnessMap.CopyTo(stream);
                    }

                    string pathSave = $"/{Common.FolderImageMaterials}/{folderName}/";

                    _dbContext.Materials.Add(new Material()
                    {
                        Name = addMaterialModel.name,
                        Status = addMaterialModel.status,
                        Url = pathSave + fileNameImagePreview,
                        AoMap = pathSave + fileNameImageAoMap,
                        NormalMap = pathSave + fileNameImageNormalMap,
                        MetalnessMap = pathSave + fileNameImageMetalnessMap,
                        RoughnessMap = pathSave + fileNameImageRoughnessMap
                    });
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
        [HttpPost]
        public IActionResult UpdateMaterial([FromForm] UpdateMaterialModel md)
        {
            try
            {
                var mat = _dbContext.Materials.Find(md.materialID);
                if (mat == null)
                {
                    return new OkObjectResult(new { status = -1, message = "Chất liệu này không khả dụng." });
                }
                mat.Name = md.name;
                mat.Status = md.status;

                string[] cacPhan = mat.Url.Split('/');
                string folderName = cacPhan[cacPhan.Length - 2];
                string pathSave = $"/{Common.FolderImageMaterials}/{folderName}/";

                if (md.file != null && md.file.Length > 0)
                {

                    // Xóa file cũ
                    var oldFilePath1 = Path.Combine(_environment.WebRootPath, mat.Url.TrimStart('/'));

                    if (System.IO.File.Exists(oldFilePath1))
                    {
                        System.IO.File.Delete(oldFilePath1);

                    }
                    string fileName = "preview" + Path.GetExtension(md.file.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        md.file.CopyTo(stream);
                    }
                    mat.Url = pathSave + fileName;
                }
                if (md.aoMap != null && md.aoMap.Length > 0)
                {

                    // Xóa file cũ
                    var oldFilePath1 = Path.Combine(_environment.WebRootPath, mat.AoMap.TrimStart('/'));

                    if (System.IO.File.Exists(oldFilePath1))
                    {
                        System.IO.File.Delete(oldFilePath1);

                    }
                    string fileName = "aoMap" + Path.GetExtension(md.aoMap.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        md.aoMap.CopyTo(stream);
                    }
                    mat.AoMap = pathSave + fileName;
                }
                if (md.normalMap != null && md.normalMap.Length > 0)
                {

                    // Xóa file cũ
                    var oldFilePath1 = Path.Combine(_environment.WebRootPath, mat.NormalMap.TrimStart('/'));

                    if (System.IO.File.Exists(oldFilePath1))
                    {
                        System.IO.File.Delete(oldFilePath1);

                    }
                    string fileName = "normalMap" + Path.GetExtension(md.normalMap.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        md.normalMap.CopyTo(stream);
                    }
                    mat.NormalMap = pathSave + fileName;
                }
                if (md.roughnessMap != null && md.roughnessMap.Length > 0)
                {

                    // Xóa file cũ
                    var oldFilePath1 = Path.Combine(_environment.WebRootPath, mat.RoughnessMap.TrimStart('/'));

                    if (System.IO.File.Exists(oldFilePath1))
                    {
                        System.IO.File.Delete(oldFilePath1);

                    }
                    string fileName = "roughnessMap" + Path.GetExtension(md.roughnessMap.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        md.roughnessMap.CopyTo(stream);
                    }
                    mat.RoughnessMap = pathSave + fileName;
                }
                if (md.metalnessMap != null && md.metalnessMap.Length > 0)
                {

                    // Xóa file cũ
                    var oldFilePath1 = Path.Combine(_environment.WebRootPath, mat.MetalnessMap.TrimStart('/'));

                    if (System.IO.File.Exists(oldFilePath1))
                    {
                        System.IO.File.Delete(oldFilePath1);

                    }
                    string fileName = "metalnessMap" + Path.GetExtension(md.metalnessMap.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, Common.FolderImageMaterials, folderName, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        md.metalnessMap.CopyTo(stream);
                    }
                    mat.MetalnessMap = pathSave + fileName;
                }
                _dbContext.SaveChanges();
                return new OkObjectResult(new { status = 1, message = "Cập nhật chất liệu thành công." });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message });
            }

        }
        [HttpPost]
        public IActionResult GetMaterials()
        {
            try
            {
                var result = _dbContext.Materials.ToList();
                return new OkObjectResult(new { status = 1, message = "success", data = result });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = DBNull.Value });
            }

        }

        #endregion
        #region Account
        public IActionResult Account()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetAccounts([FromBody] SearchAccountModel model)
        {
            try
            {
                //var data = _dbContext.Accounts.Where(a => (model.isActive == null || a.isActive == model.isActive) && (string.IsNullOrEmpty(model.accountName) || a.Email.Contains(model.accountName))).ToList();

                var data = from account in _dbContext.Accounts
                           where account.Role == Common.RoleUser
                           && (model.isActive == 0 || (model.isActive==1 && account.isActive == true) || (model.isActive == 2 && account.isActive == false))
                           && (string.IsNullOrEmpty(model.accountName) || account.Email.Contains(model.accountName))
                           select account;
                return new OkObjectResult(new { status = 1, message = "success", data = data.ToList() });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = new List<Account>() });
            }

        }
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel md)
        {
            try
            {
                var account = _dbContext.Accounts.Find(md.accountID);
                if (account == null)
                {
                    return new OkObjectResult(new { status = -1, message = "Không tìm thấy tài khoản này." });
                }
                account.FullName = md.fullName;
                account.NumberPlayGame = md.numberPlayGame;
                account.Password = md.password;
                account.PhoneNumber = md.phoneNumber;
                account.Address = md.address;

                _dbContext.SaveChanges();
                return new OkObjectResult(new { status = 1, message = "Cập nhật tài khoản thành công." });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message });
            }

        }
        public IActionResult DeleteAccount([FromBody] IDModel md)
        {
            try
            {
                var account = _dbContext.Accounts.Find(md.ID);
                if (account == null)
                {
                    return new OkObjectResult(new { status = -1, message = "Không tìm thấy tài khoản này." });
                }

                _dbContext.Accounts.Remove(account);

                _dbContext.SaveChanges();
                return new OkObjectResult(new { status = 1, message = "Xóa thành công." });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message });
            }

        }

        #endregion
        #region Category
        public IActionResult Category()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory([FromForm] AddCategoryModel model)
        {
            try
            {
                if (model.file != null)
                {
                    string folderName = Guid.NewGuid().ToString();
                    string fileNameImagePreview = "preview" + Path.GetExtension(model.file.FileName);


                    var filePath1 = Path.Combine(_environment.WebRootPath, Common.FolderImageCategories, folderName, fileNameImagePreview);

                    //tạo folder
                    if (!string.IsNullOrEmpty(filePath1))
                    {
                        string? directoryPath = Path.GetDirectoryName(filePath1);
                        if (directoryPath != null && !Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                    }


                    using (var stream = new FileStream(filePath1, FileMode.Create))
                    {
                        model.file.CopyTo(stream);
                    }

                    string pathSave = $"/{Common.FolderImageCategories}/{folderName}/";

                    _dbContext.Categories.Add(new Category()
                    {
                        Name = model.name,
                        Description = model.description,
                        Image = pathSave + fileNameImagePreview
                    });
                    _dbContext.SaveChanges();
                    return new OkObjectResult(new { status = 1, message = "Tạo danh mục mới thành công." });

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
        [HttpPost]
        public IActionResult UpdateCategory([FromForm] UpdateCategoryModel model)
        {
            try
            {
                var mat = _dbContext.Categories.Find(model.categoryID);
                if (mat == null)
                {
                    return new OkObjectResult(new { status = -1, message = "Chất liệu này không khả dụng." });
                }
                mat.Name = model.name;
                mat.Description = model.description;

                if (string.IsNullOrEmpty(mat.Image) == false && model.file != null && model.file.Length > 0)
                {
                    string[] cacPhan = mat.Image.Split('/');
                    string folderName = cacPhan[cacPhan.Length - 2];
                    string pathSave = $"/{Common.FolderImageCategories}/{folderName}/";
                    // Xóa file cũ
                    var oldFilePath1 = Path.Combine(_environment.WebRootPath, mat.Image.TrimStart('/'));

                    if (System.IO.File.Exists(oldFilePath1))
                    {
                        System.IO.File.Delete(oldFilePath1);

                    }
                    string fileName = "preview" + Path.GetExtension(model.file.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, Common.FolderImageCategories, folderName, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.file.CopyTo(stream);
                    }
                    mat.Image = pathSave + fileName;
                }
                _dbContext.SaveChanges();
                return new OkObjectResult(new { status = 1, message = "Cập nhật danh mục thành công." });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message });
            }

        }
        [HttpPost]
        public IActionResult GetCategories()
        {
            try
            {
                var result = _dbContext.Categories.ToList();
                return new OkObjectResult(new { status = 1, message = "success", data = result });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = DBNull.Value });
            }

        }
        [HttpPost]
        public IActionResult DeleteCategory([FromBody] IDModel md)
        {
            try
            {
                var account = _dbContext.Categories.Find(md.ID);
                if (account == null)
                {
                    return new OkObjectResult(new { status = -1, message = "Không tìm thấy danh mục này." });
                }

                _dbContext.Categories.Remove(account);

                _dbContext.SaveChanges();
                return new OkObjectResult(new { status = 1, message = "Xóa thành công." });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message });
            }

        }

        #endregion
        #region Promotion
        public IActionResult Promotion()
        {
            return View();
        }
        public IActionResult GetPromotions([FromBody] SearchPromotionModel model)
        {
            try
            {
                //var data = from p in _dbContext.Promotions
                //           join a in _dbContext.Accounts on p.AccountID equals a.AccountID
                //           join o in _dbContext.Orders on p.OrderID equals o.OrderID into temp
                //           from t in temp.DefaultIfEmpty()
                //           where (model.status == 0 || (model.status == 1 && p.Status == true) || (model.status == 2 && p.Status == false))
                //           && (string.IsNullOrEmpty(model.accountName) || a.Email.Contains(model.accountName))
                //           select new
                //           {
                //               p.ResultSpin,
                //               p.Number,
                //               p.Status,
                //               a.Email,
                //               p.CreateAt,
                //               p.UsedAt,
                //               p.ExpiredAt,
                //               p.OrderID
                //           };
                var data = from p in _dbContext.Promotions
                           join a in _dbContext.Accounts on p.AccountID equals a.AccountID
                           where (model.status == 0 || (model.status == 1 && p.Status == true) || (model.status == 2 && p.Status == false))
                           && (string.IsNullOrEmpty(model.accountName) || a.Email.Contains(model.accountName))
                           select new
                           {
                               p.ResultSpin,
                               p.Number,
                               p.Status,
                               a.Email,
                               p.CreateAt,
                               p.UsedAt,
                               p.ExpiredAt,
                               p.OrderID
                           };
                return new OkObjectResult(new { status = 1, message = "success", data = data.ToList() });

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message, data = DBNull.Value});
            }

        }

        #endregion
    }
}