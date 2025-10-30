using Security.Common.Models;

namespace Security.Common.Services;

public interface ITokenService
{
    string GenerateAccessToken(UserClaims userClaims);
    string GenerateRefreshToken();
    UserClaims? ValidateToken(string token);
}
