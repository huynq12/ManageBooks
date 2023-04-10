namespace ManageBooks.Models
{
	public class ReservationDetail
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public bool IsCheckedOut { get; set; }
		public bool IsReturned { get; set; }
		public DateTime? CheckedOut { get; set; }
		public DateTime? Returned { get; set; }
		
	}
}
