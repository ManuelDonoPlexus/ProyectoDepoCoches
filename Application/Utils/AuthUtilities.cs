using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CarDepo.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace CarDepo.Application.Utils;

public class AuthUtilities
{
    private readonly IConfiguration _config;

    public AuthUtilities(IConfiguration configuration)
    {
        _config = configuration;
    }

    public string EncryptToSHA256(string txt)
    {
        using (SHA256 hash = SHA256.Create())
        {
            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(txt));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) { builder.Append(bytes[i].ToString("x2")); }
            return builder.ToString();
        }
    }

    public string GenerateJWTToken(User user)
    {
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtConfig = new JwtSecurityToken(
            issuer: "cardepo.company",
            audience: "cardepo.company",
            claims: userClaims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
    }
}