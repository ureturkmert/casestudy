using HPASS.Authentication.Foundation.Option;
using HPASS.Authentication.Foundation.Secret;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HPASS.Authentication.Foundation.Validation
{
    public class HpassTokenValidator
    {
        public static bool ValidateJwtToken(string validatingToken, TokenValidationOptions tokenValidationOptions)
        {
            var secret = SecretKeyProvider.GetAuthenticationSecretKeyFromEnvironmentVariables();
            var securitySecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(validatingToken, new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = tokenValidationOptions.ValidateSignKey,
                    ValidateLifetime = tokenValidationOptions.ValidateLifetime,
                    ValidateIssuer = tokenValidationOptions.ValidateIssuer,
                    ValidateAudience = tokenValidationOptions.ValidateAudience,
                    ValidIssuer = tokenValidationOptions.Issuer,
                    ValidAudience = tokenValidationOptions.Audience,
                    IssuerSigningKey = securitySecret
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}