using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("test")]
        [Authorize(Roles = "Admin")]
        public IActionResult TestAdmin()
        {
            return Ok("Bạn là Admin và có quyền truy cập!");
        }
    }
}
