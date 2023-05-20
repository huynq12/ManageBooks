using Shared.Enum;
using Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageBooks.Models
{
	[Table("Order")]	
	public class Order
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public int BookId { get; set; }
		public Book Book { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime CheckedOut { get; set; }
		public DateTime? Returned { get; set; }
		
	}
}
