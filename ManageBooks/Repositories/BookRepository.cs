using AutoMapper;
using ManageBooks.Data;
using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageBooks.Repositories
{
    public class BookRepository : IBookRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public BookRepository(DataContext context,IMapper mapper)
		{
			_context = context;
			_mapper=mapper;
		}
		public async Task<Book> CreateBook(Book book)
		{
		
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
			return book;
		}

		public async Task<Book> DeleteBook(Book book)
		{
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
			return book;
		}

		public Task<Book?> GetBookByAuthor(string author)
		{
			return _context.Books.FirstOrDefaultAsync(book => book.Author.ToLower().Contains(author.ToLower()));
		}

		public async Task<Book?> GetBookById(int id)
		{
			return await _context.Books.FirstOrDefaultAsync(book => book.BookId == id);
		}

		public Task<Book?> GetBookByTitle(string title)
		{
			return _context.Books.FirstOrDefaultAsync(book => book.Title.ToLower().Contains(title.ToLower()));
		}

		public async Task<List<Book>> GetBooks()
		{
			return await _context.Books.ToListAsync();
		}

		public async Task<Book> UpdateBook(Book book)
		{
			_context.Update(book);
			await _context.SaveChangesAsync();
			return book;
		}

		public async Task<Book> UpdateBookQuantityAfterCheckout(Book book)
		{
			_context.Update(book);
			book.AvailableCopies -= 1;
			await _context.SaveChangesAsync();
			return book;
		}
		public async Task<Book> UpdateBookQuantityAfterReturn(Book book)
		{
			_context.Update(book);
			book.AvailableCopies += 1;
			await _context.SaveChangesAsync();
			return book;
		}
	}
}
