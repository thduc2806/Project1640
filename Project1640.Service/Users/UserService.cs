using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project1640.Data.EF;
using Project1640.Data.Entities;
using Project1640.Dto.Common;
using Project1640.Dto.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Service.Users
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IConfiguration _config;
		private readonly Project1640DbContext _context;
		public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration condfig, Project1640DbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_config = condfig;
			_context = context;
		}

		public async Task<ApiResult<string>> Login(LoginDto request)
		{
			var user = await _userManager.FindByIdAsync(request.Username);
			if (user == null) return new ApiResultError<string>("Account does not exist");

			var result = await _userManager.CheckPasswordAsync(user, request.Password);
			if (!result)
			{
				return new ApiResultError<string>("Login fail");
			}
			var role = _userManager.GetRolesAsync(user);
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, string.Join(";", role))
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_config["Jwt:Issuer"],
				_config["Jwt:Issuer"],
				claims,
				expires: DateTime.Now.AddHours(3),
				signingCredentials: creds);
			return new ApiSuccessedResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
		}
	}
}
