namespace ManageBooks.Users
{
	public class UserManagerResponse
	{
		public string Token { get; set; }
		public bool IsSuccess { get; set; }
		public string Errors { get; set; }
		public DateTime Expired { get; set; }
	}
}
