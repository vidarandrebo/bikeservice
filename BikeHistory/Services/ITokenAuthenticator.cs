using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace BikeHistory.Services
{
    public interface ITokenAuthenticator
    {
        string GenerateToken(int userId);
        bool ValidateToken(string token);
        string GetUserId(string token);
    }

    public class TokenAuthenticator : ITokenAuthenticator {
        public string GenerateToken(int userId) {
            var mySecret = "asdfghjhgfdsasdfhjhgfdsdfgh";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mySecret));
            var myIssuer = "https://localhost:5001";
            var myAudience = "https://localhost:5001";
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                        }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token) {
            var mySecret = "asdfghjhgfdsasdfhjhgfdsdfgh";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            var myIssuer = "https://localhost:5001";
            var myAudience = "https://localhost:5001";

            var tokenHandler = new JwtSecurityTokenHandler();
            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = myIssuer,
                        ValidAudience = myAudience,
                        IssuerSigningKey = mySecurityKey,
                        }, out SecurityToken validatedToken);
            }
            catch {
                return false;
            }
            return true;
        }
        public string GetUserId(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            //var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            var stringClaimValue = securityToken.Claims.First().Value;
            return stringClaimValue;
        }
    }
}
