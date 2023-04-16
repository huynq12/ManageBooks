﻿namespace ManageBooks.Dtos
{
	public class BookDto
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public int TotalCopies { get; set; }
		public int AvailableCopies { get; set; }
		public string Publisher { get; set; }
		public string? Description { get; set; }
		public int CategoryId { get; set; }
	}
}