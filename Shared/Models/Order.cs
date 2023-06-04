using Shared.Enum;
using ManageBooks.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageBooks.Models
{
	[Table("Order")]	
	public class Order
	{
		public int OrderId { get; set; }
		[Required]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		[Required]
		public int BookId { get; set; }
		public Book Book { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime CheckedOut { get; set; }
		public DateTime? Returned { get; set; }
		public string? Note { get; set; }
		
	}
}
