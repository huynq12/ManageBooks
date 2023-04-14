using ManageBooks.Models;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace LibraryManager.Services
{
	public class BookService : IBookService
	{
		private readonly HttpClient _httpClient;
		private readonly NavigationManager _navigationManager;

		public BookService(HttpClient httpClient, NavigationManager navigationManager) {
			_httpClient = httpClient;
			_navigationManager = navigationManager;
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

		public async Task<Book?> GetBookById(int id)
		{
			var result = await _httpClient.GetAsync($"/api/Book/id/{id}");
			if (result.StatusCode == HttpStatusCode.OK)
			{
				return await result.Content.ReadFromJsonAsync<Book>();
			}
			return null;
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

		public async Task UpdateBook(int id, Book book)
		{
			await _httpClient.PutAsJsonAsync($"/api/Book/{id}",book);

		}
	}
}
