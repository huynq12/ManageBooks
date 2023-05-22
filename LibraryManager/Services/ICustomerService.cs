using Shared.Models;

namespace LibraryManager.Services
{
	public interface ICustomerService
	{
		Task<List<Customer>> GetCustomers();
		Task<Customer> GetCustomerById(int id);
		Task<bool> CreateCustomer(Customer customer);
		Task<bool> UpdateCustomer(Customer customer);
		Task<bool> DeleteCustomer(int id);

	}
}
