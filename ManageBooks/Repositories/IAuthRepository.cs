using ManageBooks.Users;

namespace ManageBooks.Repositories
{
	public interface IAuthRepository
	{
		public Task<UserManagerResponse> RegisterUserAsync(RegisterModel model);
		public Task<UserManagerResponse> LoginUserAsync(LoginModel model);
	}
}
