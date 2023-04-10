using ManageBooks.Models;

namespace ManageBooks.Repositories
{
	public interface IReservationRepository
	{
		Task<List<Reservation>> GetReservations();
		Task<Reservation> GetReservationById(int id);
		Task<Reservation> CreateReservation(Reservation reservation);
		Task<Reservation> UpdateReservation(Reservation reservation);
		Task<Reservation> DeleteReservation(Reservation reservation);
	}
}
