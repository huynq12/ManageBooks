using ManageBooks.Models;
using ManageBooks.Users;

namespace ManageBooks.Dtos
{
	public class Report
	{
		public int BookCount { get; set; }
		public int TotalBookCopies { get; set; }
		public int TotalBookAvailable { get; set; }
		public int TotalOrderCount { get; set; }
		public string TheBestOrderBook { get; set; }
		public string TheBestOrderCustomer { get; set; }

		public DateTime? Time { get; set; }
		

	}
}
