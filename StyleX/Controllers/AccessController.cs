using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StyleX.Models;

namespace StyleX.Controllers
{
    public class AccessController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public AccessController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection form)
        {
            User? user = _dbContext.Users.SingleOrDefault(u => u.Email == form["email"] && u.Password == form["password"]);
            if(user != null)
            {
                if(user.isAuthen == true)
                {
                    List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties() { AllowRefresh = true, IsPersistent = true };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                }
                else
                {
                    ViewBag.Message = "Tài khoản của bạn chưa được kích hoạt.";
                }
            }
            else
            {
                ViewBag.Message = "Tài khoản hoặc mật khẩu không chính xác.";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(IFormCollection form)
        {
            User? user = _dbContext.Users.SingleOrDefault(u => u.Email == form["email"] && u.Password == form["password"]);
            if (user != null)
            {
                if (user.isAuthen == true)
                {
                    List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties() { AllowRefresh = true, IsPersistent = true };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                }
                else
                {
                    ViewBag.Message = "Tài khoản của bạn chưa được kích hoạt.";
                }
            }
            else
            {
                ViewBag.Message = "Tài khoản hoặc mật khẩu không chính xác.";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Access");
        }
    }
}
