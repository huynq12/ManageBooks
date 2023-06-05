using ManageBooks.Data;
using ManageBooks.Dtos;
using ManageBooks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageBooks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly DataContext _context;

		public ReportController(DataContext context) {
			_context = context;
		}

		[HttpPost]
		/*public async Task<IActionResult> GetReport()
		{
			var bookCount = await _context.Books.CountAsync();			
			var totalCopies = await _context.Books.SumAsync(x=> x.TotalCopies);
			var totalAvailableCopies = await _context.Books.SumAsync(x=> x.AvailableCopies);
			var totalOrderCount = await _context.Books.SumAsync(x=>x.OrderCount);
			var theBestOrderBook = await _context.Books.OrderByDescending(x=>x.OrderCount).FirstOrDefaultAsync();
			var theBestOrderCustomer = await _context.Customers.OrderByDescending(x => x.OrderCount).FirstOrDefaultAsync();
			if (theBestOrderBook == null || theBestOrderCustomer == null)
			{
				return BadRequest();
			}
			var nameOfTheBestCustomer = theBestOrderCustomer.CustomerName;
			var nameOfTheBestBook = theBestOrderBook.Title;

			return Ok(new Report
			{
				BookCount = bookCount,
				TotalBookCopies= totalCopies,
				TotalBookAvailable = totalAvailableCopies,
				TotalOrderCount = totalOrderCount,
				TheBestOrderBook = nameOfTheBestBook,
				TheBestOrderCustomer = nameOfTheBestCustomer
			});
		}*/
	}
}
