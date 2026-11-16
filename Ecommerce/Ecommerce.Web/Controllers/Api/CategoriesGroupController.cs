using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesGroupController : ControllerBase
    {
        private readonly ICategoriesGroupService _categoriesGroupService;

        public CategoriesGroupController(
            ICategoriesGroupService categoriesGroupService)
        {
            _categoriesGroupService = categoriesGroupService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var categoriesGroups = await _categoriesGroupService.GetAllAsync();
            return Ok(categoriesGroups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoriesGroup = await _categoriesGroupService.GetByIdAsync(id);

            if (categoriesGroup == null)
                return NotFound();

            return Ok(categoriesGroup);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoriesGroupCreateDto dto)
        {
            var created = await _categoriesGroupService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int id,
            CategoriesGroupUpdateDto dto)
        {
            var updated = await _categoriesGroupService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoriesGroupService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

