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

		[HttpGet]
		public async Task<IActionResult> GetCustomers()
		{
			var result = await _customerRepository.GetCustomers();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomerById(int id)
		{
			var result = _customerRepository.GetCustomerById(id);
			return Ok(result);
		}

		[HttpGet("orderingCustomer")]
		public async Task<IActionResult> GetOrderingCustomers()
		{
			var result = await _customerRepository.GetOrderingCustomers();
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

			var updatedCustomer = await _customerRepository.UpdateCustomer(existingCustomer);
			return Ok(updatedCustomer);
		}
	}
}
