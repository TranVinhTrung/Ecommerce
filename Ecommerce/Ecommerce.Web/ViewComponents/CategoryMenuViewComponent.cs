using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public CategoryMenuViewComponent(ICategoryService categoryService, IBrandService brandService)
        {
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new CategoryMenuViewModel
            {
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync()
            };

            return View(viewModel);
        }
    }
}
