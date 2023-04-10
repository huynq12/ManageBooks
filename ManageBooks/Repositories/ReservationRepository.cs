using ManageBooks.Data;
using ManageBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageBooks.Repositories
{
	public class ReservationRepository : IReservationRepository
	{
		private readonly DataContext _context;

		public ReservationRepository(DataContext context)
		{
			_context = context;
		}
		public async Task<Reservation> CreateReservation(Reservation reservation)
		{
			_context.Reservations.Add(reservation);

			await _context.SaveChangesAsync();
			return reservation;
		}

		public async Task<Reservation> DeleteReservation(Reservation reservation)
		{
			_context.Reservations.Remove(reservation);
			await _context.SaveChangesAsync();
			return reservation;
		}

		public async Task<Reservation?> GetReservationById(int id)
		{
			return await _context.Reservations.FindAsync(id);
		}

		public async Task<List<Reservation>> GetReservations()
		{
			return await _context.Reservations.OrderBy(x => x.Id).ToListAsync();
		}

		public async Task<Reservation> UpdateReservation(Reservation reservation)
		{
			_context.Reservations.Update(reservation);
			await _context.SaveChangesAsync();
			return reservation;
		}
	}
}
