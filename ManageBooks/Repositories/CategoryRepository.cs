using ManageBooks.Data;
using ManageBooks.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ManageBooks.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DataContext _context;

		public CategoryRepository(DataContext dataContext) {
			_context = dataContext;
		}
		public async Task<Category> CreateCategory(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return category;
		}

		public async Task<List<Category>> GetCategories()
		{
			return await _context.Categories.OrderBy(x=>x.Id).ToListAsync();
		}

		public async Task<Category> GetCategoryById(int id)
		{
			return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Category> UpdateCategory(Category category)
		{
			_context.Categories.Update(category);
			await _context.SaveChangesAsync();
			return category;
		}
	}
}
