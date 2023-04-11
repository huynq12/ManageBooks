using ManageBooks.Models;
using ManageBooks.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageBooks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class BookController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;

		public BookController(IBookRepository bookRepository) {
			_bookRepository = bookRepository;
		}


		[HttpGet]
		public async Task<IActionResult> GetBooks()
		{
			var result = await _bookRepository.GetBooks();
			return Ok(result);
		}

		[HttpGet("book/{id}")]
		public async Task<IActionResult> GetBookById(int id)
		{
			var result = await _bookRepository.GetBookById(id);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("books/{title}")]
		public async Task<IActionResult> GetBookByTitle(string title)
		{
			var result = await _bookRepository.GetBookByTitle(title);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);	
		}

		[HttpGet("books/{author}")]
		public async Task<IActionResult> GetBookByAuthor(string author)
		{
			var result = await _bookRepository.GetBookByAuthor(author);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpPost("book/create")] 
		public async Task<IActionResult> CreateBook(Book book)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest();
			}
			if(book.AvailableCopies > book.TotalCopies)
			{
				return BadRequest();
			}
			var result = await _bookRepository.CreateBook(book);
			return Ok(result);

		}

		[HttpPut("book/update/{id}")]
		public async Task<IActionResult> UpdateBook(int id,[FromBody]Book book) {
		
			var existingBook = await _bookRepository.GetBookById(id);
			if(existingBook == null)
			{
				return NotFound();
			}
			existingBook.Title = book.Title;
			existingBook.Author = book.Author;
			existingBook.AvailableCopies= book.AvailableCopies;
			existingBook.TotalCopies= book.TotalCopies;
			existingBook.Publisher = book.Publisher;
			existingBook.Genre= book.Genre;
			existingBook.Description = book.Description;
			
			var updatedBook = await _bookRepository.UpdateBook(existingBook);	
			return Ok(updatedBook);

		}

		[HttpDelete("book/delete/{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var existingBook = await _bookRepository.GetBookById(id);
			if (existingBook == null)
			{
				return NotFound();
			}
			var deletedBook = await _bookRepository.DeleteBook(existingBook);
			return Ok(deletedBook);
		}
	}
}
