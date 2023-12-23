using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleX.DTOs;
using StyleX.Models;
using System.Security.Claims;
using System.Collections.Generic;

namespace StyleX.Controllers
{
    [Authorize(AuthenticationSchemes = Common.CookieAuthUser)]
    public class OrderController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public OrderController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult GetOrderItems([FromBody] IDModel iDModel)
        {
            try
            {
                List<ProductDesign>? list = _dbContext.ProductDesigns.Include(d => d.Product).Where(u => u.OrderID == iDModel.ID).ToList();
                if (list == null)
                {
                    list = new List<ProductDesign>();
                }
                return new OkObjectResult(new { status = 1, message = "success.", data = list });

            }
            catch (Exception e)
            {
                return new OkObjectResult(new { status = -2, message = e.Message, data = "" });

            }
        }

    }
}
