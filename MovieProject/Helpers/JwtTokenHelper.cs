using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieProject.Helpers
{
    public static class JwtTokenHelper
    {
        public static string GenerateJwtToken(ApplicationUser user)
        {
            var key = Encoding.ASCII.GetBytes("S3uqIiNqHM4Ib+J5QrGC8KgBt6to4C15Ntl0qtofTCI=");

            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
             };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
