using Shared.Enum;

namespace ManageBooks.Dtos
{
	public class CreateOrderRequest
	{
		public int BookId { get; set; }
		public int CustomerId { get; set; }
		public string Note { get; set; }
	}
}
