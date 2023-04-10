using ManageBooks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManageBooks.Data
{
	public class DataContext : IdentityDbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		public DbSet<Book> Books { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<ReservationDetail> ReservationDetails { get; set; }
 	}
}
