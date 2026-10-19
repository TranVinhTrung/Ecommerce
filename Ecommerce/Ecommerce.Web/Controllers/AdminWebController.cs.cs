using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class AdminWebController : Controller
    {

        private readonly IProductService _productService;

        public AdminWebController(IProductService productService)
        {
            _productService = productService;
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
    }
}
