using ManageBooks.Dtos;
using ManageBooks.Models;
using System.Net;

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

		public async Task<Order> GetOrderById(int id)
		{
			var result = await _httpClient.GetAsync($"/api/Order/{id}");
			if (result.StatusCode == HttpStatusCode.OK)
			{
				return await result.Content.ReadFromJsonAsync<Order>();
			}
			return null;
		}

		public async Task<OrderDto> GetOrderDtoById(int id)
		{
			var result = await _httpClient.GetAsync($"/api/OrderDto/{id}");
			if (result.StatusCode == HttpStatusCode.OK)
			{
				return await result.Content.ReadFromJsonAsync<OrderDto>();
			}
			return null;
		}
		public async Task GetOrders()
		{
			var listOrder = await _httpClient.GetFromJsonAsync<List<OrderDto>>("/api/Order");
			if(listOrder != null)
			{
				OrderDtos = listOrder;
			}
		}

		public async Task<bool> UpdateOrder(int id, OrderDto orderDto)
		{
			var result = await _httpClient.PutAsJsonAsync($"/api/Order/{id}", orderDto);
			return result.IsSuccessStatusCode;
		}
	}
}
