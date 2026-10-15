using Ecommerce.Application.DTOs;

namespace Ecommerce.Web.ViewModels
{
    public class CategoryMenuViewModel
    {
        public IEnumerable<CategoryResponseDto> Categories { get; set; } = Enumerable.Empty<CategoryResponseDto>();
        public IEnumerable<BrandResponseDto> Brands { get; set; } = Enumerable.Empty<BrandResponseDto>();
    }
}

