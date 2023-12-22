using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StyleX.Models;
using StyleX.Utils;
using StyleX.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace StyleX.Controllers
{
    public class AccessController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public AccessController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string keyActive)
        {
            if (!string.IsNullOrEmpty(keyActive))
            {
                Account? user = _dbContext.Accounts.FirstOrDefault(u => u.keyActive == keyActive && u.isActive == false);
                if (user != null)
                {
                    user.isActive = true;
                    _dbContext.SaveChanges();
                    ViewBag.IsActive = 1;
                    ViewBag.email = user.Email;
                    ViewBag.password = user.Password;
                }
            }

            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginModel loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.email) || string.IsNullOrEmpty(loginDTO.password))
            {
                return new OkObjectResult(new { status = -4, message = "Tài khoản hoặc mật khẩu không được để trống." });
            }
            try
            {
                Account? user = _dbContext.Accounts.SingleOrDefault(u => u.Email == loginDTO.email && u.Password == loginDTO.password);

                if (user != null)
                {
                    ViewBag.Status = 200;
                    if (user.isActive == true)
                    {
                        List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.NameIdentifier, user.AccountID.ToString()), new Claim(ClaimTypes.Role,user.Role) };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        AuthenticationProperties properties = new AuthenticationProperties() { AllowRefresh = true, IsPersistent = true };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                        return new OkObjectResult(new { status = 1, message = "/" });

                    }
                    else
                    {
                        return new OkObjectResult(new { status = -1, message = "Tài khoản của bạn chưa được kích hoạt." });
                    }
                }
                else
                {
                    return new OkObjectResult(new { status = -2, message = "Tài khoản hoặc mật khẩu không chính xác." });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { status = -3, message = e.Message });

            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp([FromBody] LoginModel sigupDto)
        {
            try
            {
                sigupDto.email = sigupDto.email.Trim();
                sigupDto.password = sigupDto.password.Trim();
                if (string.IsNullOrEmpty(sigupDto.email) || string.IsNullOrEmpty(sigupDto.password))
                {
                    return new OkObjectResult(new { status = -1, message = "Tài khoản hoặc mật khẩu không được để trống." });
                }

                Account? user = _dbContext.Accounts.FirstOrDefault(u => u.Email == sigupDto.email);
                if (user != null)
                {
                    if (user.isActive == false)
                    {
                        return new OkObjectResult(new { status = -2, message = "Tài khoản đã đăng ký nhưng chưa được kích hoạt." });

                    }
                    return new OkObjectResult(new { status = -3, message = "Tài khoản này đã tồn tại." });

                }
                else
                {
                    string keyActive = Guid.NewGuid().ToString();
                    string linkActive = $"<a href=\"https://{HttpContext.Request.Host.Value}/Access/Login/?keyActive={keyActive}\">StyleX - Nhấn vào đây để kích hoạt tài khoản của bạn.</a>";

                    _dbContext.Accounts.Add(new Models.Account() { Email = sigupDto.email, Password = sigupDto.password, isActive = false, keyActive = keyActive });
                    if (_dbContext.SaveChanges() > 0)
                    {
                        new SendMail().SendEmailByGmail(sigupDto.email, "Kích hoạt tài khoản", "<html><body>" + linkActive + "</body></html>");
                        return new OkObjectResult(new { status = 1, message = "Đã gửi link kích hoạt về email của bạn."});

                    }
                    else
                    {
                        return new OkObjectResult(new { status = -4, message = "Lỗi hệ thống vui lòng thử lại sau." });
                    }
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { status = -99, message = e.Message });
            }

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
