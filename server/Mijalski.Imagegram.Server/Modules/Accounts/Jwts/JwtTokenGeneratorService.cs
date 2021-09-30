using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mijalski.Imagegram.Domain.Accounts;

namespace Mijalski.Imagegram.Server.Modules.Accounts.Jwts;

public interface IJwtTokenGeneratorService
{
    ValueTask<string> GenerateTokenAsync(Account account, CancellationToken cancellationToken = default);
}

class JwtTokenGeneratorService : IJwtTokenGeneratorService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenGeneratorService(IOptions<JwtOptions> options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _jwtOptions = options.Value;
    }

    public ValueTask<string> GenerateTokenAsync(Account account, CancellationToken cancellationToken = default)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        var jwtToken = jwtTokenHandler.WriteToken(token);

        return new ValueTask<string>(jwtToken);
    }
}