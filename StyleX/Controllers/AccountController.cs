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
                string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                Account? user = _dbContext.Accounts.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    int promotion1 = _dbContext.Promotions.Where(p => p.AccountID == user.AccountID && p.Status == true && p.Number == 10).Count();
                    int promotion2 = _dbContext.Promotions.Where(p => p.AccountID == user.AccountID && p.Status == true && p.Number == 20).Count();
                    int promotion3 = _dbContext.Promotions.Where(p => p.AccountID == user.AccountID && p.Status == true && p.Number == 30).Count();
                    int promotion4 = _dbContext.Promotions.Where(p => p.AccountID == user.AccountID && p.Status == true && p.Number == 40).Count();

                    List<Order> orders = _dbContext.Orders.Where(o => o.AccountID == user.AccountID).ToList();

                    ViewBag.user = user;
                    ViewBag.promotion1 = promotion1;
                    ViewBag.promotion2 = promotion2;
                    ViewBag.promotion3 = promotion3;
                    ViewBag.promotion4 = promotion4;
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
                string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

                Account? user = _dbContext.Accounts.FirstOrDefault(u => u.Email == userEmail);
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
