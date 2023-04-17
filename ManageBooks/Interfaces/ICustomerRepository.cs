using Shared.Models;

namespace ManageBooks.Interfaces
{
	public interface ICustomerRepository
	{
		Task<List<Customer>> GetCustomers();
		Task<List<Customer>> GetOrderingCustomers();
		Task<List<Customer>> GetExpiredCustomers();

		Customer? GetCustomerById(int id);
		Task<Customer> CreateCustomer(Customer customer);
		Task<Customer> UpdateCustomer(Customer customer);
	}
}
