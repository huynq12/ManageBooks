using ManageBooks.Interfaces;
using ManageBooks.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace ManageBooks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerController(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}
		//lấy ra danh sách bạn đọc trên hệ thống
		[HttpGet]
		public async Task<IActionResult> GetCustomers()
		{
			var result = await _customerRepository.GetCustomers();
			return Ok(result);
		}
		//tìm bạn đọc theo id

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomerById(int id)
		{
			var result = _customerRepository.GetCustomerById(id);
			return Ok(result);
		}
		//lấy danh sách các bạn đọc đang mượn sách
		[HttpGet("ordering-customer")]
		public async Task<IActionResult> GetOrderingCustomers()
		{
			var result = await _customerRepository.GetOrderingCustomers();
			return Ok(result);
		}

		[HttpGet("expired-customer")]
		public async Task<IActionResult> GetExpiredCustomers()
		{
			var result = await _customerRepository.GetExpiredCustomers();
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCustomer([FromBody]Customer customer)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var newCustomer = await _customerRepository.CreateCustomer(customer);
			return Ok(newCustomer);
		}
		//chỉnh sửa thông tin bạn đọc
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCustomer(int id,[FromBody]Customer customer) {
			var existingCustomer = _customerRepository.GetCustomerById(id);
			if (existingCustomer == null)
			{
				return NotFound();
			}

			existingCustomer.CustomerName = customer.CustomerName;
			existingCustomer.CustomerEmail = customer.CustomerEmail;
			existingCustomer.CustomerPhone = customer.CustomerPhone;
			existingCustomer.Status = customer.Status;

			var updatedCustomer = await _customerRepository.UpdateCustomer(existingCustomer);
			return Ok(updatedCustomer);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			var existingCustomer = _customerRepository.GetCustomerById(id);
			if (existingCustomer == null)
			{
				return NotFound();
			}
			var deletedCustomer = await _customerRepository.DeleteCustomer(existingCustomer);
			return Ok(deletedCustomer);
		}
	}
}
