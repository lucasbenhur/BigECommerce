using BigECommerce.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BigECommerce.Auth.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;
        private readonly BigECommerceDbContext _dbContext;

        public JwtService(
            BigECommerceDbContext dbContext)
        {
            _settings = new JwtSettings();
            _dbContext = dbContext;
        }

        public async Task<string> GenerateTokenAsync(string email, string password)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user is null)
                throw new InvalidOperationException("Email ou senha inválidos");

            var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
