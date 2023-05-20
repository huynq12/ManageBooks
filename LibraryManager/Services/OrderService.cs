using ManageBooks.Dtos;
using ManageBooks.Models;

namespace LibraryManager.Services
{
	public class OrderService : IOrderService
	{
		private readonly HttpClient _httpClient;

		public OrderService(HttpClient http) {
			_httpClient = http; 
		}
		public List<OrderDto> OrderDtos { get; set ; } = new List<OrderDto>();

		public async Task<bool> CreateOrder(CreateOrderRequest request)
		{
			var newOrder = await _httpClient.PostAsJsonAsync("/api/Order", request);
			return newOrder.IsSuccessStatusCode;
		}

        public Task GetAcitveOrders()
        {
            throw new NotImplementedException();
        }

        public Task GetExpiredOrders()
        {
            throw new NotImplementedException();
        }

        public async Task GetOrders()
		{
			var listOrder = await _httpClient.GetFromJsonAsync<List<OrderDto>>("/api/Order");
			if(listOrder != null)
			{
				OrderDtos = listOrder;
			}
		}

		public async Task<bool> UpdateOrder(int id, Order order)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/Order/{id}", order);
			return result.IsSuccessStatusCode;
		}
	}
}
