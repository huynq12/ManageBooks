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
			customer.OrderingStatus = Shared.Enum.Status.Active;
			await _context.SaveChangesAsync();	
			return customer;
		}

		public  Customer? GetCustomerById(int id)
		{
			return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
		}

		public async Task<List<Customer>> GetCustomers()
		{
			return await _context.Customers.OrderBy(x=>x.CustomerName).ToListAsync();
		}

		public async Task<List<Customer>> GetOrderingCustomers()
		{
			return await _context.Customers.Where(x=>x.OrderingStatus == Shared.Enum.Status.Active).ToListAsync();
		}

		public async Task<Customer> UpdateCustomer(Customer customer)
		{
			_context.Customers.Update(customer);
			await _context.SaveChangesAsync();
			return customer;
		}

		/*public async Task<Customer> UpdateCustomerStatus(Customer customer)
		{
			
			await _context.SaveChangesAsync();
			return customer;
		}*/
	}
}
