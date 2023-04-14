using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using ManageBooks.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageBooks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IBookRepository _bookRepository;

		public OrderController(IOrderRepository orderRepository,IBookRepository bookRepository)
		{
			_orderRepository = orderRepository;
			_bookRepository = bookRepository;
		}

		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
		{
			var book = await _bookRepository.GetBookById(orderDto.BookId);
			if (book == null)
			{
				return NotFound("The book does not exist.");
			}
			book.AvailableCopies -= 1;
			var order = new Order
			{
				CustomerId= orderDto.CustomerId,
				CheckedOut = DateTime.UtcNow,
				Returned = DateTime.UtcNow.AddDays(14),
				Status = Shared.Enum.Status.Active
			};

			var orderDetail = new OrderDetail
			{
				BookId = book.BookId,
				OrderId = order.OrderId
			};

			return Ok(order);
		}
	}
}
