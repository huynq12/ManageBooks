using ManageBooks.Dtos;
using Shared.Models;
using System.Net;

namespace LibraryManager.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly HttpClient _httpClient;

		public CustomerService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public Task<bool> CreateCustomer(Customer customer)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteCustomer(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<Customer> GetCustomerById(int id)
		{
			var result = await _httpClient.GetAsync($"/api/Customer/{id}");
			if (result.StatusCode == HttpStatusCode.OK)
			{
				return await result.Content.ReadFromJsonAsync<Customer>();
			}
			return null;
		}

		public Task<List<Customer>> GetCustomers()
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateCustomer(Customer customer)
		{
			throw new NotImplementedException();
		}
	}
}
