using Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageBooks.Models
{
	[Table("Book")]
	public class Book
	{
		[Key]
		public int BookId { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Author { get; set; }
		public int TotalCopies { get; set; }
		public int AvailableCopies { get; set; }
		[Required]
		public string Genre { get; set; }
		public string Publisher { get; set; }
		public int OrderCount { get; set; }
		public DateTime Release { get; set; }
		public string? Description { get; set; }
		
		
	}
}
