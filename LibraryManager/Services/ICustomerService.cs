using ManageBooks.Models;

namespace LibraryManager.Services
{
	public interface ICustomerService
	{
		List<Customer> Customers { get; set; }
		Task GetCustomers();
		Task<Customer?> GetCustomerById(int id);
		Task<bool> CreateCustomer(Customer customer);
		Task<bool> UpdateCustomer(int id, Customer customer);
		Task<bool> DeleteCustomer(int id);

	}
}
