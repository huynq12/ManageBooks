using ManageBooks.Data;
using ManageBooks.Interfaces;
using Microsoft.EntityFrameworkCore;
using ManageBooks.Models;

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

		public  Customer? GetCustomerById(int id)
		{
			return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
		}

		public async Task<List<Customer>> GetCustomers()
		{
			return await _context.Customers.OrderByDescending(x=>x.Status).ToListAsync();
		}

		public async Task<List<Customer>> GetExpiredCustomers()
		{
			return await _context.Customers.Where(x => x.Status == Shared.Enum.CustomerStatus.Expired).ToListAsync();

		}

		public async Task<List<Customer>> GetOrderingCustomers()
		{
			return await _context.Customers.Where(x=>x.Status	 == Shared.Enum.CustomerStatus.Active).ToListAsync();
		}

		public async Task<Customer> UpdateCustomer(Customer customer)
		{
			_context.Customers.Update(customer);
			await _context.SaveChangesAsync();
			return customer;
		}
		public async Task<Customer> DeleteCustomer(Customer customer)
		{
			_context.Customers.Remove(customer);
			await _context.SaveChangesAsync();
			return customer;
		}



	}
}
