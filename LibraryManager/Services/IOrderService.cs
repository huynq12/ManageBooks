using ManageBooks.Dtos;
using ManageBooks.Models;

namespace LibraryManager.Services
{
	public interface IOrderService
	{
		List<OrderDto> OrderDtos { get; set; }
		Task GetOrders();
		Task GetAcitveOrders();
		Task GetExpiredOrders();
		Task<bool> CreateOrder(CreateOrderRequest request);
		Task<bool> UpdateOrder(int id, Order order);
	}
}
