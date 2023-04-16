using ManageBooks.Dtos;
using ManageBooks.Models;

namespace ManageBooks.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<Book?> GetBookByTitle(string title);
        Task<Book?> GetBookByAuthor(string author);
        Task<Book> CreateBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<Book> UpdateBookQuantityAfterCheckout(Book book);
        Task<Book> UpdateBookQuantityAfterReturn(Book book);

		Task<Book> DeleteBook(Book book);
    }
}
