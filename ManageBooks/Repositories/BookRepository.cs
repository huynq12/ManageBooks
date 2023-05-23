using AutoMapper;
using ManageBooks.Data;
using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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
			return book ;
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

		public async Task<List<Book>> GetBooks(BookListSearch bookListSearch)
		{
			var query = _context.Books.AsQueryable();

			if (!string.IsNullOrEmpty(bookListSearch.Title))
				query = query.Where(x => x.Title.Contains(bookListSearch.Title));

			if (!string.IsNullOrEmpty(bookListSearch.Author))
				query = query.Where(x => x.Author.Contains(bookListSearch.Author));

			if (!string.IsNullOrEmpty(bookListSearch.Genre))
				query = query.Where(x => x.Genre.Contains(bookListSearch.Genre));

			return await query.ToListAsync(); 
		}

		public async Task<List<Book>> GetBooksByGenre(string genre)
		{
			return await _context.Books.Where(x => x.Genre.ToLower().Contains(genre.ToLower())).ToListAsync();
		}

		public async Task<List<Book>> GetBooksByText(string text)
		{
			text = text.ToLower();
			return await _context.Books.Where(x=>x.Title.ToLower().Contains(text)
												|| x.Author.ToLower().Contains(text)
												|| x.Publisher.ToLower().Contains(text)
												|| x.Description.ToLower().Contains(text)).ToListAsync();
		}

		public async Task<List<Book>> GetFavBooks()
		{
			return await _context.Books.OrderByDescending(x => x.OrderCount).ToListAsync();
		}

		public async Task<List<Book>> GetNewBooks()
		{
			return await _context.Books.OrderByDescending(x => x.Release).ToListAsync();
		}

		public async Task<Book> UpdateBook(Book book)
		{
			_context.Update(book);
			await _context.SaveChangesAsync();
			return book;
		}

		

		public async Task<Book> UpdateBookAfterCheckout(Book book)
		{
			_context.Update(book);
			book.AvailableCopies -= 1;
			book.OrderCount += 1;
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

        public bool isValidBookData(Book book)
        {
			if(book.AvailableCopies > book.TotalCopies || book.AvailableCopies < 1)
			{
				return false;
			}
			return true;
        }

		public Book? GetABookById(int id)
		{
			return _context.Books.Where(x=>x.BookId==id).FirstOrDefault();
		}
	}
}
