using ManageBooks.Dtos;
using ManageBooks.Models;

namespace ManageBooks.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrders(OrderListSearch orderListSearch);
        Task<List<Order>> GetActiveOrders();
        Task<List<Order>> GetExpiredOrders();
        Task<Order> GetOrderById(int id);
        Task<OrderDto> GetOrderDtoById(int id);
		Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrderStatus(Order order);
        Task<Order> DeleteOrder(Order order);
     }
}
