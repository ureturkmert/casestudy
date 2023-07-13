using HPASS.Authentication.Foundation.Option;
using HPASS.Authentication.Foundation.Secret;
using HPASS.Authentication.Foundation.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HPASS.Authentication.Foundation.Generation
{
    public class HpassTokenGenerator
    {
        public static HpassAuthenticationToken GenerateToken(TokenGenerationOptions tokenGenerationOptions)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string secretKey = SecretKeyProvider.GetAuthenticationSecretKeyFromEnvironmentVariables();
            var secretKeyEncoded = Encoding.UTF8.GetBytes(secretKey);

            List<Claim> generatingClaims = new List<Claim>();

            if (!tokenGenerationOptions.ClaimsDictionary.Any())
            {
                throw new Exception("Generating token must have at least 1 claim");
            }

            foreach (var dictionaryItem in tokenGenerationOptions.ClaimsDictionary)
            {
                string claimName = dictionaryItem.Key;
                string claimValue = dictionaryItem.Value;

                generatingClaims.Add(new Claim(claimName, claimValue));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(generatingClaims),
                IssuedAt = DateTime.UtcNow,
                NotBefore = tokenGenerationOptions.TokenValidStartDate,
                Expires = tokenGenerationOptions.TokenValidEndDate,
                Issuer = tokenGenerationOptions.Issuer,
                Audience = tokenGenerationOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyEncoded), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            string generatedToken = tokenHandler.WriteToken(token);

            HpassAuthenticationToken generatedHpassAccessToken = new HpassAuthenticationToken();
            generatedHpassAccessToken.Token = generatedToken;
            generatedHpassAccessToken.TokenIssuedAt = tokenDescriptor.IssuedAt.Value;
            generatedHpassAccessToken.TokenValidAfter = tokenDescriptor.NotBefore.Value;
            generatedHpassAccessToken.TokenValidUntil = tokenDescriptor.Expires.Value;
            generatedHpassAccessToken.TokenValidPeriodAsMinute = (generatedHpassAccessToken.TokenValidUntil - generatedHpassAccessToken.TokenValidAfter).TotalMinutes;
            generatedHpassAccessToken.Issuer = tokenGenerationOptions.Issuer;
            generatedHpassAccessToken.Audience = tokenGenerationOptions.Audience;

            return generatedHpassAccessToken;
        }
    }
}