using Shared.Enum;

namespace ManageBooks.Dtos
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public int BookId { get; set; }
		public string BookTitle { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public int CustomerId { get; set; }
		public string CustomerName { get; set; }
		public string CustomerEmail { get; set; }
		public string CustomerPhone { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime CheckedOut { get; set; }
		public DateTime? Returned { get; set; }
	}
}
