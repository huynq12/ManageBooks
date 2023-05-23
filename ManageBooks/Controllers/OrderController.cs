using AutoMapper;
using ManageBooks.Dtos;
using ManageBooks.Interfaces;
using ManageBooks.Models;
using ManageBooks.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace ManageBooks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IBookRepository _bookRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;

		public OrderController(IOrderRepository orderRepository,
								IBookRepository bookRepository,
								ICustomerRepository customerRepository,
								IMapper mapper)
		{
			_orderRepository = orderRepository;
			_bookRepository = bookRepository;
			_customerRepository = customerRepository;
			_mapper = mapper;
		}
		

		//lấy ra danh sách tất cả các đơn đặt mượn sách(bao gồm cả đơn đã trả)
		[HttpGet]
		public async Task<IActionResult> GetOrders([FromQuery] OrderListSearch orderListSearch)
		{
			var listOrder = await _orderRepository.GetOrders(orderListSearch);
			var result = listOrder.Select(x => new OrderDto
			{
				OrderId = x.OrderId,
				CustomerName = x.Customer.CustomerName,
				BookTitle = x.Book.Title,
				Status = x.Status
			}).ToList();
			return Ok(result);
		}
		//lấy ra danh sách các đơn đang mượn
		[HttpGet("active-orders")]
		public async Task<IActionResult> GetActiveOrders()
		{
			var result = await _orderRepository.GetActiveOrders();
			
			return Ok(result);
		}
		//lấy ra danh sách các đơn quá hạn để thông báo nhắc nhở bạn đọc
		[HttpGet("expired-orders")]
		public async Task<IActionResult> GetExpiredOrders()
		{
			var result = await _orderRepository.GetExpiredOrders();
			
			return Ok(result);
		}
		//tìm đon mượn qua id
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
		[HttpGet("/api/OrderDto/{id}")]
		public async Task<IActionResult> GetOrderDtoById(int id)
		{
			var order = await _orderRepository.GetOrderDtoById(id);
			if (order == null)
			{
				return NotFound();
			}
			return Ok(order);
		}
		//khởi tạo đơn mượn sách
		[HttpPost]
		public async Task<ActionResult> CreateOrder([FromBody] CreateOrderRequest orderDto)
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
			var orderingCustomer = _customerRepository.GetCustomerById(orderDto.CustomerId);
			if (orderingCustomer == null || orderingCustomer.Status == Shared.Enum.CustomerStatus.Active)
			{
				return NotFound("The customer does not exist or not be permit");
			}

			var newOrder = await _orderRepository.CreateOrder(new Order()
			{
				CustomerId = orderDto.CustomerId,
				Customer = orderingCustomer,
				BookId = orderDto.BookId,
				Book = book,
				CheckedOut = DateTime.Now,
				Returned = null,
				Status = Shared.Enum.OrderStatus.Active,
				Note = orderDto.Note,
			});

			//update customer status
			orderingCustomer.Status = Shared.Enum.CustomerStatus.Active;
			await _customerRepository.UpdateCustomer(orderingCustomer);

			//update book quantity after customer return book
			await _bookRepository.UpdateBookAfterCheckout(book);
			
			return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);

		}
		//update trạng thái đơn mượn
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateStatusOrderRequest request)
		{
			var existingOrder = await _orderRepository.GetOrderById(id);

			if (existingOrder == null)
			{
				return NotFound();
			}
			// existing book in active status
			var bookToUpdate = await _bookRepository.GetBookById(existingOrder.BookId);
			//get customer who returns
			var customer = _customerRepository.GetCustomerById(existingOrder.CustomerId);

			//update status
			existingOrder.Status = Shared.Enum.OrderStatus.Returned;
			existingOrder.Returned = DateTime.Now;
			//check status to update quantity
			if(existingOrder.Status== Shared.Enum.OrderStatus.Returned)
			{
				await _bookRepository.UpdateBookQuantityAfterReturn(bookToUpdate);
				customer.Status = Shared.Enum.CustomerStatus.None;
				await _customerRepository.UpdateCustomer(customer);
			}
			var updatedOrder = await _orderRepository.UpdateOrderStatus(existingOrder);
			return Ok(updatedOrder);

		}
		//xoá đơn mượn
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var existingOrder = await _orderRepository.GetOrderById(id);

			if(existingOrder == null)
			{
				return NotFound();
			}
			Book bookToUpdate = _bookRepository.GetABookById(existingOrder.BookId);
			bookToUpdate.AvailableCopies++;
			bookToUpdate.OrderCount--;

			var orderingCustomer = _customerRepository.GetCustomerById(existingOrder.CustomerId);
			orderingCustomer.Status = Shared.Enum.CustomerStatus.None;
			await _customerRepository.UpdateCustomer(orderingCustomer);

			var deletedOrder = await _orderRepository.DeleteOrder(existingOrder);
			return Ok(deletedOrder);
		}
	}
}
