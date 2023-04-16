using ManageBooks.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace ManageBooks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository) {
			_categoryRepository = categoryRepository;
		}


		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			var result = await _categoryRepository.GetCategories();
			return Ok(result);
		}

		[HttpGet("id/{id}")]
		public async Task<IActionResult> GetBookById(int id)
		{
			var result = await _categoryRepository.GetCategoryById(id);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateCategory(Category category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var result = await _categoryRepository.CreateCategory(category);
			return Ok(result);

		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id,Category category)
		{
			var existingCategory = await _categoryRepository.GetCategoryById(id);

			if (existingCategory == null)
			{
				return NotFound();
			}
			existingCategory.CategoryName = category.CategoryName;
			existingCategory.Description = category.Description;

			var updatedCategory = await _categoryRepository.UpdateCategory(existingCategory);
			return Ok(updatedCategory);
		}
	}
}
