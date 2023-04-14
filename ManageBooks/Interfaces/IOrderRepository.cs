using ManageBooks.Dtos;
using ManageBooks.Models;

namespace ManageBooks.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(int id,Order order);

    }
}
