using Microsoft.IdentityModel.Tokens;
using PharmacyApi.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmacyApi.Utils
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration Configuration;
		private string Key = Environment.GetEnvironmentVariable("pass") + "" + Environment.GetEnvironmentVariable("pass");

		public TokenService(IConfiguration configuration)
		{
			Configuration = configuration;
		}


		public string GenerateToken(User user)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.Username),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var token = new JwtSecurityToken(
				issuer: Environment.GetEnvironmentVariable("issuer"),
				audience: Environment.GetEnvironmentVariable("audience"),
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(60)),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
