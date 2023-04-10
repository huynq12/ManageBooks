using ManageBooks.Models;
using ManageBooks.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageBooks.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationController : ControllerBase
	{
		private readonly IReservationRepository _reservationRepository;

		public ReservationController(IReservationRepository reservationRepository)
		{
			_reservationRepository = reservationRepository;
		}

		[HttpGet("reservations")]
		public async Task<IActionResult> GetReservations()
		{
			var result = await _reservationRepository.GetReservations();
			return Ok(result);
		}

		[HttpGet("reservation/{id}")]
		public async Task<IActionResult> GetReservationById(int id)
		{
			var result = await _reservationRepository.GetReservationById(id);
			if(result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateReservation(Reservation reservation)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var newReservation = await _reservationRepository.CreateReservation(reservation);
			return Ok(newReservation);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateReservation(int id,[FromBody] Reservation reservation) { 
			var existingReservation = await _reservationRepository.GetReservationById(id);

			if(existingReservation == null)
			{
				return NotFound();
			}

			existingReservation.CustomerName = reservation.CustomerName;
			existingReservation.CustomerPhone = reservation.CustomerPhone;
			existingReservation.CustomerEmail = reservation.CustomerEmail;
			existingReservation.ReservationDetails = reservation.ReservationDetails;

			var updatedReservation = await _reservationRepository.UpdateReservation(existingReservation);
			return Ok(updatedReservation);
		}

		/*[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReservation(int id)
		{
			var reservationToDel = await _reservationRepository.GetReservationById(id);
			if(reservationToDel == null)
			{
				return NotFound();
			}
			var deletedReservation = await _reservationRepository.DeleteReservation(reservationToDel);
			return Ok(deletedReservation);	
		}*/
	}
}
