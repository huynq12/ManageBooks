using Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManageBooks.Models
{
	[Table("Book")]
	public class Book
	{
		public int BookId { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int TotalCopies { get; set; }
		public int AvailableCopies { get; set; }
		public string Publisher { get; set; }
		public string? Description { get; set; }
		public int? CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category Category { get; set; }
	}
}
