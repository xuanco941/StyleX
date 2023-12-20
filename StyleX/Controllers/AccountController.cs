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
    public class AccountController : Controller
	{
        private readonly DatabaseContext _dbContext;

        public AccountController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
		{
            try
            {
                string userEmail = User.FindFirstValue(ClaimTypes.Email);
                User? user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    int promotion5Percent = _dbContext.Promotions.Where(p => p.UserID == user.UserID && p.Status == true && p.Number == 5).Count();
                    int promotion10Percent = _dbContext.Promotions.Where(p => p.UserID == user.UserID && p.Status == true && p.Number == 10).Count();
                    int promotion15Percent = _dbContext.Promotions.Where(p => p.UserID == user.UserID && p.Status == true && p.Number == 15).Count();
                    int promotion20Percent = _dbContext.Promotions.Where(p => p.UserID == user.UserID && p.Status == true && p.Number == 20).Count();

                    List<Order> orders = _dbContext.Orders.Where(o => o.UserID == user.UserID).ToList();

                    ViewBag.user = user;
                    ViewBag.promotion5Percent = promotion5Percent;
                    ViewBag.promotion10Percent = promotion10Percent;
                    ViewBag.promotion15Percent = promotion15Percent;
                    ViewBag.promotion20Percent = promotion20Percent;
                    ViewBag.orders = orders;

                }
            }
            catch
            {
                return RedirectToAction("Index","Home");
            }
            return View();
		}
        [HttpPost]
        public IActionResult Update([FromBody] UserModel userUpdate)
        {
            if (userUpdate.password.Length <5)
            {
                return new OkObjectResult(new { status = -1, message = "Mật khẩu tối thiểu có 5 ký tự." });
            }

            try
            {
                string userEmail = User.FindFirstValue(ClaimTypes.Email);

                User? user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
                if(user != null) {
                    user.FullName = userUpdate.fullName;
                    user.Password = userUpdate.password;
                    user.Address = userUpdate.address;
                    user.PhoneNumber = userUpdate.phoneNumber;
                    _dbContext.SaveChanges();
                    return new OkObjectResult(new { status = 1, message = "Cập nhật thông tin thành công." });
                }
                else
                {
                    return new OkObjectResult(new { status = -1, message = "Tài khoản không tồn tại." });
                }

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { status = -2, message = e.Message });

            }
        }

    }
}
