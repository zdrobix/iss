using PharmacyApi.Models.Domain;

namespace PharmacyApi.Utils
{
	public interface ITokenService
	{
		string GenerateToken(User user);
	}
}
