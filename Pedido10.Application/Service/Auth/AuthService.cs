using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Pedido10.Domain.Dto;
using Pedido10.Application.Contract.Auth;
using Pedido10.Shared.Options;

namespace Pedido10.Application.Service.Auth
{
    public class AuthService(IOptions<JwtOptions> jwtOptions) : IAuthService
    {
        private readonly JwtOptions _jtwOptions = jwtOptions.Value;

        public string GenerateJwtToken(UsuarioDto usuarioDto)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioDto.ID_Usuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuarioDto.Email),
                new Claim(ClaimTypes.Role, "Usuario")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jtwOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jtwOptions.Issuer,
                audience: _jtwOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
