using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;
using OnlineStore.WebApi.Configurations;

namespace OnlineStore.WebApi.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;

    public JwtTokenService(JwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
    }

    public string GenerateToken(Account account)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }
        IClock clock = new Clock();
        var now = clock.GetCurrentTime();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                // new Claim(ClaimTypes.Role, string.Join(",",account.Roles))
            }),
            Expires = now.Add(_jwtConfig.LifeTime),
            Audience = _jwtConfig.Audience,
            Issuer = _jwtConfig.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public interface IClock
{
    DateTime GetCurrentTime();
}

public class Clock : IClock
{
    public DateTime GetCurrentTime() => DateTime.Now;
}