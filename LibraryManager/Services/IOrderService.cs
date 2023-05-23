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
		Task<Order> GetOrderById(int id);
		Task<OrderDto> GetOrderDtoById(int id);
		Task<bool> CreateOrder(CreateOrderRequest request);
		Task<bool> UpdateOrderStatus(int id, Order order);

	}
}
