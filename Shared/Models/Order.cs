using Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageBooks.Models
{
	[Table("Order")]	
	public class Order
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public int BookId { get; set; }
		public Status Status { get; set; }
		public DateTime? CheckedOut { get; set; }
		public DateTime? Returned { get; set; }
		
	}
}
