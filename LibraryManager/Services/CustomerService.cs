using Azure.Core;
using LibraryManager.Pages.Customer;
using ManageBooks.Dtos;
using ManageBooks.Models;

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

        public List<Customer> Customers { get ; set ; } = new List<Customer>();

        public async Task<bool> CreateCustomer(Customer customer)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/Customer", customer);
			return result.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteCustomer(int id)
		{
			var result = await _httpClient.DeleteAsync($"/api/Customer/{id}");
			return result.IsSuccessStatusCode;
		}

		public async Task<Customer?> GetCustomerById(int id)
		{
			var result = await _httpClient.GetAsync($"/api/Customer/{id}");
			if (result.StatusCode == HttpStatusCode.OK)
			{
				return await result.Content.ReadFromJsonAsync<Customer>();
			}
			return null;
		}


        public async Task<bool> UpdateCustomer(int id, Customer customer)
        {
			var result = await _httpClient.PutAsJsonAsync($"/api/Customer/{id}", customer);
			return result.IsSuccessStatusCode;
		}

        public async Task GetCustomers()
        {
			var list = await _httpClient.GetFromJsonAsync<List<Customer>>("/api/Customer");
			if(list != null)
			{
				Customers = list;
			}
        }
    }
}
