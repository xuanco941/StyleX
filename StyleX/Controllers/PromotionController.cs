using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleX.DTOs;
using StyleX.Models;
using System.Security.Claims;

namespace StyleX.Controllers
{
    [Authorize]
    public class PromotionController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public PromotionController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetNumberPlay()
        {

            try
            {
                string userEmail = User.FindFirstValue(ClaimTypes.Email);
                User? user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    return new OkObjectResult(new { status = 1, message = "success", number = user.NumberPlayGame });
                }
                else
                {
                    return new OkObjectResult(new { status = -1, message = "Tài khoản của bạn không tồn tại.", number=0 });
                }
            }
            catch
            {
                return new OkObjectResult(new { status = -2, message = "Có lỗi xảy ra, vui lòng thử lại sau.",number=0 });
            }
        }

        [HttpPost]
        public IActionResult GetResult()
        {
            //kết quả của 1 lượt quay là 3 mảng gồm 7 số sắp xếp ngẫu nhiên,
            //trong 3 mảng ấy nếu phần tử thứ 3,4,5 của mỗi mảng mà bằng nhau thì win
            //1 dãy trùng nhau phiếu 5%
            //2 dãy trùng nhau phiếu 10%
            //3 dãy trùng nhau phiếu 15%
            //3 dãy trùng nhau và 1 trong 3 có phần tử là 1 thì nhận phiếu 20%

            try
            {
                string userEmail = User.FindFirstValue(ClaimTypes.Email);
                User? user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    if (user.NumberPlayGame > 0)
                    {
                        user.NumberPlayGame = user.NumberPlayGame - 1;
                        var result1 = Utils.Utils.GenerateRandomArray(1, 7, 7);
                        var result2 = Utils.Utils.GenerateRandomArray(1, 7, 7);
                        var result3 = Utils.Utils.GenerateRandomArray(1, 7, 7);

                        int count = 0;
                        if (result1[2] == result2[2] && result2[2] == result3[2])
                        {
                            count++;
                        }
                        if (result1[3] == result2[3] && result2[3] == result3[3])
                        {
                            count++;
                        }
                        if (result1[4] == result2[4] && result2[4] == result3[4])
                        {
                            count++;
                        }
                        if (count == 3 && (result1[2] == 1 || result1[3] == 1 || result1[4] == 1))
                        {
                            count++;
                        }
                        if (count > 0)
                        {
                            _dbContext.Promotions.Add(new Promotion() { UserID = user.UserID, Status = false, Number = count * 5, ResultSpin = result1.ToString() + " " + result2.ToString() + " " + result3.ToString() });
                        }

                        return new OkObjectResult(new { status = 1, message = "success.", result = new { result1, result2, result3 }, numberSale = count * 5 }); ;

                    }
                    else
                    {
                        return new OkObjectResult(new { status = -1, message = "Bạn không còn lượt quay nào." });
                    }
                }
                else
                {
                    return new OkObjectResult(new { status = -2, message = "Tài khoản của bạn không tồn tại." });
                }
            }
            catch
            {
                return new OkObjectResult(new { status = -3, message = "Có lỗi xảy ra, vui lòng thử lại sau." });
            }
        }
    }

}

