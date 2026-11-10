using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Ecommerce.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _environment;

        public BrandController(IBrandService brandService, IWebHostEnvironment environment)
        {
            _brandService = brandService;
            _environment = environment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandService.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] BrandCreateDto dto)
        {
            if (dto.Logo != null && !IsValidLogo(dto.Logo))
            {
                return BadRequest(new
                {
                    message = "Logo must be JPG, JPEG, PNG, or WEBP and no larger than 2 MB."
                });
            }

            var logoUrl = dto.Logo != null
                ? await SaveLogoAsync(dto.Logo) 
                : null;

            var created = await _brandService.CreateAsync(dto, logoUrl);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] BrandUpdateDto dto)
        {

            if (dto.Logo != null && !IsValidLogo(dto.Logo))
            {
                return BadRequest(new
                {
                    message = "Logo must be JPG, JPEG, PNG, or WEBP and no larger than 2 MB."
                });
            }

            string? logoUrl = null;

            if (dto.Logo != null)
            {
                logoUrl = await SaveLogoAsync(dto.Logo);
            }

            var updated = await _brandService.UpdateAsync(id, dto, logoUrl);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _brandService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        private async Task<string> SaveLogoAsync(IFormFile logo)
        {
            var uploadFolder = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "brands");

            Directory.CreateDirectory(uploadFolder);

            var extension = Path.GetExtension(logo.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await logo.CopyToAsync(stream);

            return $"/uploads/brands/{fileName}";
        }

        private static bool IsValidLogo(IFormFile? logo)
        {
            if (logo == null || logo.Length == 0)
                return false;

            const long maxFileSize = 2 * 1024 * 1024; // 2 MB

            if (logo.Length > maxFileSize)
                return false;

            var extension = Path.GetExtension(logo.FileName).ToLowerInvariant();

            return extension is ".jpg" or ".jpeg" or ".png" or ".webp";
        }
    }
}
