using ManageBooks.Data;
using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

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

		public async Task<Order> DeleteOrder(Order order)
		{
			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
			return order;
		}

		public async Task<List<Order>> GetActiveOrders()
		{
			return await _context.Orders.Where(x => x.Status == Shared.Enum.OrderStatus.Active).ToListAsync();
		}

		public async Task<List<Order>> GetExpiredOrders()
		{
			return await _context.Orders.Where(x=>x.Status==Shared.Enum.OrderStatus.Expired).ToListAsync();
		}

		public async Task<Order?> GetOrderById(int id)
		{
			return await _context.Orders.FirstOrDefaultAsync(x=>x.OrderId == id);
		}

		public async Task<List<OrderDto>> GetOrders()
		{
			return await _context.Orders.Include(x => x.Customer).Include(x => x.Book).Select(x => new OrderDto
			{
				Id = x.OrderId,
				CustomerName = x.Customer.CustomerName,
				BookTitle = x.Book.Title,
				Status = x.Status,
				CheckedOut = x.CheckedOut,
				Returned = x.Returned
			}).ToListAsync();
		}

		public async Task<Order> UpdateOrderStatus(Order order)
		{
			_context.Orders.Update(order);
			if(DateTime.Now > order.CheckedOut.AddDays(14))
			{
				order.Status = Shared.Enum.OrderStatus.Expired;
			}
			await _context.SaveChangesAsync();
			return order;
		}
	}
}
