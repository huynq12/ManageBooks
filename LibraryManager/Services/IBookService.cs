
using ManageBooks.Models;

namespace LibraryManager.Services
{
	public interface IBookService
	{
		List<Book> Books { get; set; }
		Task GetBooks();
		Task GetBookById(int id);
		Task<Book?> GetBookByTitle(string title);
		Task<Book?> GetBookByAuthor(string author);
		Task CreateBook(Book book);
		Task UpdateBook(int id, Book book);
		Task DeleteBook(int id);

	}
}
