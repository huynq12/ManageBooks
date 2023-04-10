namespace ManageBooks.Models
{
	public class Reservation
	{
		public int Id { get; set; }
		public string CustomerName { get; set; }
		public string CustomerEmail { get; set; }
		public string CustomerPhone { get; set; }
		public List<ReservationDetail> ReservationDetails { get; set; }
	}
}
