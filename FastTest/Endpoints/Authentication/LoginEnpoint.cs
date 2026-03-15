using FastEndpoints;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FastTest.Endpoints.Authentication
{
    // endpoint devolver  token
    public class LoginEnpoint : EndpointWithoutRequest<string>
    {

        public override void Configure()
        {
            Post("/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            // se toma la clave desde config
            var key = Encoding.UTF8.GetBytes(Config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Config["Jwt:Issuer"],
                Subject = new ClaimsIdentity(new[]
                  {
                    new Claim(ClaimTypes.Name,"testuser"),
                        new Claim(ClaimTypes.Role, "User")
                }),

                Expires = DateTime.UtcNow.AddHours(1), // duración del token

                SigningCredentials =
                      new SigningCredentials(
                      new SymmetricSecurityKey(key),
                          SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // devolver token como string
            await SendAsync(tokenHandler.WriteToken(token));
        }

    }
}