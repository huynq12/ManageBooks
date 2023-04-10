namespace ManageBooks.Models
{
	public class Book
	{
		public int BookId { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int TotalCopies { get; set; }
		public int AvailableCopies { get; set; }
		public string Publisher { get; set; }
		public string Genre { get; set; }
		public string? Description { get; set; }
		public List<Reservation> Reservations { get; set; }
	}
}
