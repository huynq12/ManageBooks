using Shared.Enum;

namespace ManageBooks.Dtos
{
	public class UpdateStatusOrderRequest
	{
		public OrderStatus Status { get; set; }	
		public DateTime Returned { get; set; }
		public string Note { get; set; }
	}
}
