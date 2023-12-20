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
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            User? user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
            ViewBag.user = user;
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
