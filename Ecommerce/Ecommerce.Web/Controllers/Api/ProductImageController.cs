using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/images")]
    [Authorize]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _imageService;

        public ProductImageController(IProductImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(int productId, ProductImageCreateDto dto)
        {
            var result = await _imageService.AddAsync(productId,dto.ImageUrl,dto.IsPrimary);

            if (!result)
                return NotFound(new
                {
                    message = "Product not found."
                });

            return Ok(new
            {
                message = "Product image added successfully."
            });
        }

        [HttpDelete("{imageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int imageId)
        {
            var result = await _imageService.DeleteAsync(imageId);

            if (!result)
                return NotFound(new
                {
                    message = "Product image not found."
                });

            return Ok(new
            {
                message = "Product image deleted successfully."
            });
        }
    }
}
