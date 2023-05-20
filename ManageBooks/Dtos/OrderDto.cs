using Shared.Enum;

namespace ManageBooks.Dtos
{
	public class OrderDto
	{
		public int Id { get; set; }
		public string BookTitle { get; set; }
		public string CustomerName { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime CheckedOut { get; set; }
		public DateTime? Returned { get; set; }
	}
}
