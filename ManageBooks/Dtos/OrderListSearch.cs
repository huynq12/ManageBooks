using Shared.Enum;

namespace ManageBooks.Dtos
{
	public class OrderListSearch
	{
		public int? OrderId { get; set; }
		public string BookTitle { get; set; }
		public string CustomerName { get; set; }
		public OrderStatus? Status { get; set; }

	}
}
