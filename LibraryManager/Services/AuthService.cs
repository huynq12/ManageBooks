using Blazored.LocalStorage;
using ManageBooks.Users;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LibraryManager.Services
{
	public class AuthService : IAuthService
	{
		private readonly HttpClient _http;
		private readonly AuthenticationStateProvider _authenticationStateProvider;
		private readonly ILocalStorageService _localStorage;

		public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
			ILocalStorageService localStorageService)
		{
			_http = httpClient;
			_authenticationStateProvider = authenticationStateProvider;
			_localStorage = localStorageService;
		}
		public async Task<UserManagerResponse> Login(LoginModel loginRequest)
		{
			var result = await _http.PostAsJsonAsync("/api/Auth/login", loginRequest);
			var content = await result.Content.ReadAsStringAsync();
			var loginResponse = JsonSerializer.Deserialize<UserManagerResponse>(content,
				new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});
			if (!result.IsSuccessStatusCode)
			{
				return loginResponse;
			}
			try
			{
				await _localStorage.SetItemAsync("token", loginResponse.Token);
				((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginRequest.Username);
				_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
				return loginResponse;
			}
			catch (Exception)
			{
				throw;
			}

		}

		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("token");
			((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
			_http.DefaultRequestHeaders.Authorization = null;

		}
	}
}
