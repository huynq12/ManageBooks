using Shared.Enum;

namespace ManageBooks.Dtos
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public int BookId { get; set; }
		public int CustomerId { get; set; }
		public Status Status { get; set; }
		public DateTime? CheckedOut { get; set; }
		public DateTime? Returned { get; set; }
	}
}
