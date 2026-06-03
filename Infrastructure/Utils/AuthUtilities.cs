using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CarDepo.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace CarDepo.Infrastructure.Utils;

// Esta clase es una utilidad intermedia principalmente usada por el repositorio para dos funcionalidades relacionadas con el encriptado y otorgar los tokens.

public class AuthUtilities
{
    private readonly IConfiguration _config; // Parametro para definir configuraciones 

    public AuthUtilities(IConfiguration configuration)
    {
        _config = configuration;
    }

    // Funcionalidad de encriptado. Recibe un texto como parametro.
    // Al ejecutarse, crea un hash de SHA256, procesa el parametro y, con lo resultante, crea un nuevo string construido caracter a caracter de una versión cifrada del string.
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

    // Funcionalidad destinada para la generación de Tokens JWT. Recibe el usuario solicitante como parametro.
    // Para crear este token se crean 2 peticiones con el correo e ID del usuario.
    // Despues creamos una clave de seguridad y unas credenciales.
    // La clave de seguridad es creada apartir de la clave JWT de la configuración del programa 
    // Esas credenciales son creadas con la anterior clave, y un algoritmo (en nuestro caso SHA256)
    // Despues de configurar ambos, se crea una configuración para escribir el nuevo token.
    // En esa configuración esta incluido el issuer y la audiencia, las peticiones, el momento de expiración y las credenciales
    // Tras crear estas configuraciones, devolvemos finalmente un nuevo token creado a partir de esta configuración 
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