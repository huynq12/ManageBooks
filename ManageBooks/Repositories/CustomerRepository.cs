using ManageBooks.Data;
using ManageBooks.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ManageBooks.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly DataContext _context;

		public CustomerRepository(DataContext context) {
			_context = context;
		}
		public async Task<Customer> CreateCustomer(Customer customer)
		{
			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();	
			return customer;
		}

		public async Task<Customer> GetCustomerById(int id)
		{
			return await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
		}

		public async Task<List<Customer>> GetCustomers()
		{
			return await _context.Customers.OrderBy(x=>x.CustomerName).ToListAsync();
		}

		/*public Task<List<Customer>> GetOrderingCustomers()
		{
			var orders = _context.Orders.Where(x => x.Status == Shared.Enum.Status.Active).ToList();

			var orderingCustomers = new List<Customer>();
			foreach (var order in orders)
			{
				return 
			}
		}*/

		public async Task<Customer> UpdateCustomer(Customer customer)
		{
			_context.Customers.Update(customer);
			await _context.SaveChangesAsync();
			return customer;
		}
	}
}
