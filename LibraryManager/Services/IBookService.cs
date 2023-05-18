
using ManageBooks.Dtos;
using ManageBooks.Models;

namespace LibraryManager.Services
{
	public interface IBookService
	{
		List<Book> Books { get; set; }
		Task GetBooks(BookListSearch bookListSearch);
		Task<Book?> GetBookById(int id);
		Task<bool> CreateBook(CreateBookRequest request);
		Task UpdateBook(int id, Book book);
		Task DeleteBook(int id);

	}
}
