using AutoMapper;
using Azure.Core;
using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Enum;

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

		[HttpGet("searchList/{text}")]
		public async Task<IActionResult> GetBooksByText(string text)
		{
			var list = await _bookRepository.GetBooksByText(text);
			return Ok(list);
		}

		//tạo sách trên hệ thống
		[HttpPost("create")] 
		public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
		{

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var book = await _bookRepository.CreateBook(new Book()
			{
				Title = bookDto.Title,
				Author = bookDto.Author,
				TotalCopies= bookDto.TotalCopies,
				AvailableCopies = bookDto.AvailableCopies,
				Publisher = bookDto.Publisher,
				Description = bookDto.Description,
				CategoryId = bookDto.CategoryId
			});

			return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);

		}
		//chỉnh sửa thông tin sách
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateBook(int id,[FromBody]BookDto bookDto) {
		
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
			existingBook.CategoryId = bookDto.CategoryId;
			existingBook.Description = bookDto.Description;

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
