using ManageBooks.Users;

namespace ManageBooks.Interfaces
{
    public interface IAuthRepository
    {
        public Task<UserManagerResponse> RegisterUserAsync(RegisterModel model);
        public Task<UserManagerResponse> LoginUserAsync(LoginModel model);
    }
}
