using ManageBooks.Interfaces;
using ManageBooks.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageBooks.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthRepository _authRepository;

		public AuthController(IAuthRepository authRepository)
		{
			_authRepository= authRepository;
		}

		//đăng kí
		[HttpPost("register")]
		public async Task<IActionResult> RegisterAsync(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _authRepository.RegisterUserAsync(model);

				if (result.IsSuccess)
				{
					return Ok(result);
				}
				return BadRequest(result);
			}
			return BadRequest("Some properties are not valid!");
		}
		//đăng nhập
		[HttpPost("login")]
		public async Task<IActionResult> LoginAsync(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _authRepository.LoginUserAsync(model);
				if (result.IsSuccess)
				{
					return Ok(result);
				}
				return BadRequest(result);
			}
			return BadRequest("Some properties are not valid");
		}
	}
}
