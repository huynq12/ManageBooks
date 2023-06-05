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

		public async Task<Order> GetOrderById(int id)
		{
			return await _context.Orders.Where(x => x.OrderId == id).FirstOrDefaultAsync();
		}

		public async Task<OrderDto?> GetOrderDtoById(int id)
		{
			return await _context.Orders
				.Include(x => x.Customer)
				.Include(x => x.Book)
				.Where(x => x.OrderId == id)
				.Select(x => new OrderDto
			{
				OrderId = x.OrderId,
				CustomerId = x.CustomerId,
				CustomerName = x.Customer.CustomerName,
				CustomerPhone = x.Customer.CustomerPhone,
				CustomerEmail = x.Customer.CustomerEmail,
				BookId = x.BookId,
				BookTitle = x.Book.Title,
				Author= x.Book.Author,
				Status = x.Status,
				CheckedOut = x.CheckedOut,
				Returned = x.Returned
			}).FirstOrDefaultAsync();
		}

		public async Task<List<Order>> GetOrders(OrderListSearch orderListSearch)
		{
			var query = _context.Orders.Include(x=>x.Book).Include(x => x.Customer).AsQueryable();

			/*if (orderListSearch.OrderId.HasValue)
				query = query.Where(x=>x.OrderId== orderListSearch.OrderId.Value);*/

			if (!string.IsNullOrEmpty(orderListSearch.BookTitle))
				query = query.Where(x => x.Book.Title.Contains(orderListSearch.BookTitle));

			if (!string.IsNullOrEmpty(orderListSearch.CustomerName))
				query = query.Where(x => x.Customer.CustomerName.Contains(orderListSearch.CustomerName));

			if (orderListSearch.Status.HasValue)
				query = query.Where(x => x.Status == orderListSearch.Status.Value);

			return await query.ToListAsync();
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
