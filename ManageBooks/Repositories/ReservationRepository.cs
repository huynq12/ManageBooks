using ManageBooks.Data;
using ManageBooks.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
			
			foreach(var item in reservation.ReservationDetails)
			{

				var bookToUpdate = _context.Books.Find(item.BookId);
				if(bookToUpdate != null)
				{
					bookToUpdate.AvailableCopies -= 1;
					var reservationDetail = new ReservationDetail();
					reservationDetail.BookId = item.BookId;
					reservationDetail.IsCheckedOut = true;
					reservationDetail.CheckedOut = DateTime.Now;
					reservationDetail.IsReturned = false;
					_context.ReservationDetails.Add(reservationDetail);
				}
				
			}
			await _context.SaveChangesAsync();
			return reservation;
		}

		/*public async Task<Reservation> DeleteReservation(Reservation reservation)
		{
			foreach(var reservationDetail in reservation.ReservationDetails)
			{
				var bookToUpdate = _context.Books.Find(reservationDetail.BookId);
				if(bookToUpdate != null)
				{
					bookToUpdate.AvailableCopies += 1;
					_context.Remove(reservationDetail);
				}
			}
			_context.Reservations.Remove(reservation);
			await _context.SaveChangesAsync();

			return reservation;
		}*/

		public async Task<Reservation> GetReservationById(int id)
		{
			return await _context.Reservations.FindAsync(id);
		}

		public async Task<List<Reservation>> GetReservations()
		{
			return await _context.Reservations.OrderBy(x => x.Id).ToListAsync();
		}

		public async Task<Reservation> UpdateReservation(Reservation reservation)
		{
			foreach (var item in reservation.ReservationDetails)
			{

				var bookToUpdate = _context.Books.Find(item.BookId);
				if (bookToUpdate != null)
				{
					
					var reservationDetail = new ReservationDetail();
					reservationDetail.BookId = item.BookId;
					reservationDetail.IsCheckedOut = item.IsCheckedOut;
					reservationDetail.CheckedOut = item.CheckedOut;
					reservationDetail.IsReturned = item.IsReturned;
					reservationDetail.Returned = item.Returned;
					if(reservationDetail.IsReturned == true)
					{
						bookToUpdate.AvailableCopies += 1; 
					}
					_context.ReservationDetails.Add(reservationDetail);
					_context.Reservations.Update(reservation);
				}

			}
			await _context.SaveChangesAsync();
			return reservation;
		}
	}
}
