using Intl.Realty.Firm.Models.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Intl.Realty.Firm.Helper
{
    public static class JWTToken
    {
        public static string GenerateJwtToken(User entity, string base64Key, List<string> permissions)
        {
            // TO DO : use entity.RoleId

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(base64Key);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, entity.Email),
                new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                new Claim("Role", entity.RoleId.ToString()),
                new Claim("Department", entity.DepartmentId.ToString())
            };

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "INTLRealtyFirmIssuer.com",
                Audience = "INTLRealtyFirmAudience.com"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
