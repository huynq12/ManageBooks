using ManageBooks.Users;

namespace LibraryManager.Services
{
	public interface IAuthService
	{
		Task<UserManagerResponse> Login(LoginModel loginModel);
		Task Logout();


	}
}
