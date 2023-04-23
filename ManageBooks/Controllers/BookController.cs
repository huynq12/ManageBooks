using AutoMapper;
using Azure.Core;
using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Enum;
using System.Text.RegularExpressions;

namespace ManageBooks.Controllers
{
    [Route("api/[controller]")]
	[ApiController]

	public class BookController : ControllerBase
	{
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper;
		public BookController(IBookRepository bookRepository, IMapper mapper) {
			_bookRepository = bookRepository;
			_mapper = mapper;
		}

		//in ra danh sách các cuốn sách của thư viện
		[HttpGet]
		public async Task<IActionResult> GetBooks()
		{
			var result = await _bookRepository.GetBooks();
			return Ok(result);
		}

		//tìm sách theo id
		[HttpGet("id/{id}")]
		public async Task<IActionResult> GetBookById(int id)
		{
			var result = await _bookRepository.GetBookById(id);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("books/{genre}")]
		public async Task<IActionResult> GetBooksByGenre(string genre)
		{
			var list = await _bookRepository.GetBooksByGenre(genre);
			return Ok(list);	
		}
		[HttpGet("favorite-list")]
		public async Task<IActionResult> GetFavBooks()
		{
			var list = await _bookRepository.GetFavBooks();
			return Ok(list);	
		}
		[HttpGet("new-books")]
		public async Task<IActionResult> GetNewBooks()
		{
			var list = await _bookRepository.GetNewBooks();
			return Ok(list);
		}
		//tìm sách theo tên
		[HttpGet("title/{title}")]
		public async Task<IActionResult> GetBookByTitle(string title)
		{
			var result = await _bookRepository.GetBookByTitle(title);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);	
		}
		//tìm sách theo tác giả
		[HttpGet("author/{author}")]
		public async Task<IActionResult> GetBookByAuthor(string author)
		{
			var result = await _bookRepository.GetBookByAuthor(author);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("search-list/{text}")]
		public async Task<IActionResult> GetBooksByText(string text)
		{
			var list = await _bookRepository.GetBooksByText(text);
			return Ok(list);
		}

		
		//tạo sách trên hệ thống
		[HttpPost("create")] 
		public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
		{

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var book = await _bookRepository.CreateBook(new Book()
			{
				Title = request.Title,
				Author = request.Author,
				TotalCopies= request.TotalCopies,
				AvailableCopies = request.AvailableCopies,
				Publisher = request.Publisher,
				Description = request.Description,
				Genre = request.Genre,
				OrderCount = 0,
				Release = DateTime.Now
			});

			return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);

		}
		//chỉnh sửa thông tin sách
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id,[FromBody]CreateBookRequest  bookDto) {
		
			var existingBook = await _bookRepository.GetBookById(id);
			if(existingBook == null)
			{
				return NotFound();
			}

			existingBook.Title = bookDto.Title;
			existingBook.Author = bookDto.Author;
			existingBook.AvailableCopies= bookDto.AvailableCopies;
			existingBook.TotalCopies= bookDto.TotalCopies;
			existingBook.Publisher = bookDto.Publisher;
			existingBook.Genre = bookDto.Genre;
			existingBook.Description = bookDto.Description;
			existingBook.OrderCount = bookDto.OrderCount;
			existingBook.Release = bookDto.Release;

			var updatedBook = await _bookRepository.UpdateBook(existingBook);	
			return Ok(updatedBook);

		}
		[HttpPut("/info/{id}")]
		public async Task<IActionResult> UpdateBookInfor(int id,CreateBookRequest bookDto)
		{

			var existingBook = await _bookRepository.GetBookById(id);
			if (existingBook == null)
			{
				return NotFound();
			}

			existingBook.Release = bookDto.Release;

			var updatedBook = await _bookRepository.UpdateBook(existingBook);
			return Ok(updatedBook);

		}
		//xoá sách
		[HttpDelete("delete/{id}")]
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
