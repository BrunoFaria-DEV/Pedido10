using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pedido10.Application.Service.Auth;
using Pedido10.Domain.Dto;

namespace Pedido10.Application.Contract.Auth
{
    public interface IAuthService
    {
        string GenerateJwtToken(UsuarioDto usuarioDto);
    }
}
