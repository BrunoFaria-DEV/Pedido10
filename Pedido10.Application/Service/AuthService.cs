using Pedido10.Application.Contract;
using Pedido10.Domain.Dto;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Pedido10.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly string _key = "Pedido10KeyExampleComPeloMenos32caracteres";
        private readonly string _issuer = "Pedido10.API";
        private readonly string _audience = "Pedido10.Front";

        public string GenerateJwtToken(UsuarioDto usuarioDto)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioDto.ID_Usuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuarioDto.Email),
                new Claim(ClaimTypes.Role, "Usuario") // Exemplo: papel do usuário
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
