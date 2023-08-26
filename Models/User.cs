using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaperCalc.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public bool Admin { get; set; }

        public static bool VerifyToken(IConfiguration config, string? token)
        {
            if(String.IsNullOrEmpty(token)) { return false; }
            var key = config.GetSection("AppSettings:Token").Value;
            if(String.IsNullOrEmpty(key)) { return false; }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidIssuer = "Parrot",
                    ValidAudience = "CreateDesignStudio",
                    ValidateLifetime = false,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool IsAdmin(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == "Role").Value;

            if(stringClaimValue == "0")
            {
                return true;
            }

            return false;
        }
    }
}
