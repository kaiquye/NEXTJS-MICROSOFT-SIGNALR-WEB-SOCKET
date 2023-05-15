using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebSocket.Domain.Entitys;
using WebSocket.Domain.Interfaces.Authentication;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebSocket.Infra.Authentication;

public class AuthenticationService: IAuthenticationService<Person>
{
    public string generate(Person payload)
    {
        byte[] secretBytes = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("secret_key"));
        var key = new SymmetricSecurityKey(secretBytes);
        var claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Name, payload.name));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, payload.email));
        var jwt = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
        };
        var securityToken = new JwtSecurityTokenHandler();
        var token = securityToken.CreateToken(jwt);
        
        return securityToken.WriteToken(token).ToString();
    }
}