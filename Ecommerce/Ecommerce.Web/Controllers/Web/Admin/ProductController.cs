using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers.Web.Admin
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoriesGroupService _categoriesGroupService;

        public ProductController(ICategoriesGroupService categoriesGroupService)
        {
            _categoriesGroupService = categoriesGroupService;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesGroups = await _categoriesGroupService.GetAllAsync();

            return View(categoriesGroups);
        }
    }
}
