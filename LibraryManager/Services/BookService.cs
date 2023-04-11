using ManageBooks.Models;

namespace LibraryManager.Services
{
	public class BookService : IBookService
	{
		private readonly HttpClient _httpClient;

		public BookService(HttpClient httpClient) {
			_httpClient = httpClient;
		}
		public List<Book> Books { get ; set ; } = new List<Book>();

		public Task CreateBook(Book book)
		{
			throw new NotImplementedException();
		}

		public Task DeleteBook(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Book?> GetBookByAuthor(string author)
		{
			throw new NotImplementedException();
		}

		public Task GetBookById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Book?> GetBookByTitle(string title)
		{
			throw new NotImplementedException();
		}

		public async Task GetBooks()
		{
			var listBooks = await _httpClient.GetFromJsonAsync<List<Book>>("/api/Book");
			if(listBooks != null)
			{
				Books = listBooks;
			}
		}

		public Task UpdateBook(int id, Book book)
		{
			throw new NotImplementedException();
		}
	}
}
