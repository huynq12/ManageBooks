using ManageBooks.Data;
using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ManageBooks.Repositories
{
    public class OrderRepository : IOrderRepository
	{
		private readonly DataContext _context;

		public OrderRepository(DataContext dataContext)
		{
			_context = dataContext;
		}

		public async Task<Order> CreateOrder(Order order)
		{

			_context.Orders.Add(order);
			
			await _context.SaveChangesAsync();

			return order;
		}

		public Task<Order> GetOrderById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Order>> GetOrders()
		{
			throw new NotImplementedException();
		}

		public Task<Order> UpdateOrder(int id,Order reservation)
		{
			throw new NotImplementedException();
		}
	}
}
