using Ecommerce.Application.DTOs;
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
            var allCategories = (await _categoryService.GetAllAsync()).ToList();
            var parentCategories = allCategories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            var brands = (await _brandService.GetAllAsync()).ToList();

            var brandsByCategory = new Dictionary<int, List<BrandResponseDto>>();

            foreach (var parent in parentCategories)
            {
                var categoryIds = allCategories
                    .Where(c => c.Id == parent.Id || IsDescendantOf(c, parent.Id, allCategories))
                    .Select(c => c.Id)
                    .ToHashSet();

                brandsByCategory[parent.Id] = brands
                    .Where(b => b.CategoryIds.Any(categoryIds.Contains))
                    .OrderBy(b => b.Name)
                    .ToList();
            }

            var viewModel = new CategoryMenuViewModel
            {
                Categories = parentCategories,
                Brands = brands,
                BrandsByCategory = brandsByCategory
            };

            return View(viewModel);
        }

        private static bool IsDescendantOf(
            CategoryResponseDto category,
            int parentId,
            List<CategoryResponseDto> allCategories)
        {
            var currentParentId = category.ParentCategoryId;

            while (currentParentId.HasValue)
            {
                if (currentParentId.Value == parentId)
                    return true;

                var parent = allCategories.FirstOrDefault(c => c.Id == currentParentId.Value);
                if (parent == null)
                    return false;

                currentParentId = parent.ParentCategoryId;
            }

            return false;
        }

    }
}
