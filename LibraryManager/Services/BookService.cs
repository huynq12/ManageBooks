using ManageBooks.Dtos;
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

		public async Task<bool> CreateBook(CreateBookRequest request)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/Book/create", request);
			return result.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteBook(int id)
		{
			var result = await _httpClient.DeleteAsync($"/api/Book/{id}");
			return result.IsSuccessStatusCode;
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


		public async Task GetBooks(BookListSearch bookListSearch)
		{
			string url = $"/api/Book?title={bookListSearch.Title}&author={bookListSearch.Author}&genre={bookListSearch.Genre}";

			var listBooks = await _httpClient.GetFromJsonAsync<List<Book>>(url);
			Books = listBooks;
		}

		public async Task<bool> UpdateBook(int id, Book book)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/Book/{id}",book);
			return result.IsSuccessStatusCode;
		}
	}
}
