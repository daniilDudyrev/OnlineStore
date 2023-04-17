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
    private readonly IClock _clock;

    public JwtTokenService(JwtConfig jwtConfig, IClock clock)
    {
        _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    public string GenerateToken(Account account)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }
        var now = _clock.GetCurrentTime();
        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
        });
        foreach (var role in account.Roles)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
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

public class RealClock : IClock
{
    public DateTime GetCurrentTime() => DateTime.Now;
}