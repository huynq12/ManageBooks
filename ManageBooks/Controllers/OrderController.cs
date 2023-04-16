using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using ManageBooks.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

		[HttpGet]
		public async Task<IActionResult> GetOrders()
		{
			var result = await _orderRepository.GetOrders();
			return Ok(result);
		}
		[HttpGet("expiredOrders")]
		public async Task<IActionResult> GetExpiredOrders()
		{
			var result = await _orderRepository.GetExpiredOrders();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			var order = await _orderRepository.GetOrderById(id);
			if (order == null)
			{
				return NotFound();
			}
			return Ok(order);
		}

		[HttpPost]
		public async Task<ActionResult> CreateOrder([FromBody] OrderDto orderDto)
		{
			var book = await _bookRepository.GetBookById(orderDto.BookId);

			if (book == null)
			{
				return NotFound("The book does not exist.");
			}

			if(book.AvailableCopies < 2)
			{
				return BadRequest("The book is not available for borrowing");
			}

			var newOrder = await _orderRepository.CreateOrder(new Order()
			{
				CustomerId = orderDto.CustomerId,
				BookId = orderDto.BookId,
				CheckedOut = DateTime.Now,
				Returned = DateTime.Now.AddDays(14),
				Status = Shared.Enum.Status.Active
			});

			var result = await _bookRepository.UpdateBookQuantityAfterCheckout(book);

			return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);

		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] Order order)
		{
			var existingOrder = await _orderRepository.GetOrderById(id);

			if (existingOrder == null)
			{
				return NotFound();
			}
			// existing book in active status
			var bookToUpdate = await _bookRepository.GetBookById(existingOrder.BookId);
			//update status
			existingOrder.Status = order.Status;
			//check status to update quantity
			if(existingOrder.Status== Shared.Enum.Status.Returned)
			{
				await _bookRepository.UpdateBookQuantityAfterReturn(bookToUpdate);
			}

			var updatedOrder = await _orderRepository.UpdateOrderStatus(existingOrder);
			return Ok(updatedOrder);

		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var existingOrder = await _orderRepository.GetOrderById(id);

			if(existingOrder == null)
			{
				return NotFound();
			}

			var deletedOrder = await _orderRepository.DeleteOrder(existingOrder);
			return Ok(deletedOrder);
		}



	}
}
