using System.ComponentModel.DataAnnotations.Schema;

namespace ManageBooks.Models
{
	[Table("OrderDetail")]	
	public class OrderDetail
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }
		public int BookId { get; set; }
		public Book Book { get; set; }


	}
}
