using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KawasakeChat.Application.Interfaces.Helpers;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KawasakeChat.Application.Helpers;

public class Token(IConfiguration configuration, IAppRepository appRepository) : IToken
{
    private readonly IConfiguration _config = configuration;
    private readonly IAppRepository _repApp = appRepository;
    
    public string? CreateAccessToken(Useraccount userAccount, DateTime expiration)
    {
        try
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("UserId", userAccount.UserId.ToString()));

            var usrRole = _repApp.GetUserAccountRole(userAccount.Role) ?? throw new Exception("user invalid role");
            claims.AddClaim(new Claim("Role", usrRole.Name));

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"] ?? throw new Exception("jwt secret key is null")));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                audience: _config["Jwt:Audience"] ?? throw new Exception("jwt audience is null"),
                issuer: _config["Jwt:Issuer"] ?? throw new Exception("jwt issuer is null"),
                claims: claims.Claims,
                expires: expiration,
                signingCredentials: signingCredentials);
          
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string CreateRefreshToken(Useraccount userAccount)
    {
        try
        {
            return "";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}