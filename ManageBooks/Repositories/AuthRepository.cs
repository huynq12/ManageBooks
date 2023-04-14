using ManageBooks.Interfaces;
using ManageBooks.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageBooks.Repositories
{
    public class AuthRepository : IAuthRepository
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthRepository(UserManager<IdentityUser> userManager,IConfiguration configuration) {
			_userManager = userManager;
			_configuration = configuration;
		}
		public async Task<UserManagerResponse> LoginUserAsync(LoginModel model)
		{
			var user = await _userManager.FindByNameAsync(model.Username);
			if (user == null)
				return new UserManagerResponse
				{
					IsSuccess = false
				};

			var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
			if (!checkPassword)
				return new UserManagerResponse
				{
					IsSuccess = false
				};

			var claims = new[]
			{
				new Claim("Username",model.Username),
				new Claim(ClaimTypes.NameIdentifier,user.Id)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
			);


			return new UserManagerResponse
			{
				IsSuccess = true,
				Expired = token.ValidTo,
				Token = new JwtSecurityTokenHandler().WriteToken(token)
			};
		}

		public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel model)
		{
			if (model == null)
			{
				throw new NullReferenceException("Register is not exist!");
			}
			if (model.Password != model.ConfirmPassword)
				return new UserManagerResponse
				{
				
					IsSuccess = false
				};
			var identityUser = new IdentityUser
			{
				UserName = model.Username
			};

			var result = await _userManager.CreateAsync(identityUser, model.Password);

			if (result.Succeeded)
				return new UserManagerResponse
				{
					IsSuccess = true
				};
			return new UserManagerResponse
			{ 
				IsSuccess = false
			};
		}
	}
}
