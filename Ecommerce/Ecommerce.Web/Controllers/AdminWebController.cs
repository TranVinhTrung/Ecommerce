using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;




namespace Ecommerce.Web.Controllers
{
    [Authorize(
    AuthenticationSchemes = "Identity.Application",
    Roles = "Admin")]
    public class AdminWebController : Controller
    {

        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminWebController(IProductService productService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _productService = productService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products([FromQuery] ProductQueryDto query)
        {
            var products = await _productService.GetAllAsync(query);

            return View(products);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid login information."
                });
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                dto.Password,
                lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    message = "You do not have permission to access the admin dashboard."
                });
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok(new
            {
                message = "Login successful."
            });
        }
    }
}
