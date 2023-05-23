using ManageBooks.Dtos;
using ManageBooks.Models;

namespace ManageBooks.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooks(BookListSearch bookListSearch);
        Task<List<Book>> GetBooksByGenre(string genre);
        Task<List<Book>> GetFavBooks();
        Task<List<Book>> GetNewBooks(); 
        Task<Book> GetBookById(int id);
        Book GetABookById(int id);
        Task<Book?> GetBookByTitle(string title);
        Task<Book?> GetBookByAuthor(string author);
        Task<List<Book>> GetBooksByText(string text);
        Task<Book> CreateBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<Book> UpdateBookAfterCheckout(Book book);
        Task<Book> UpdateBookQuantityAfterReturn(Book book);
		Task<Book> DeleteBook(Book book);

        bool isValidBookData(Book book);
    }
}
