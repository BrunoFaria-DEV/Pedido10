using Pedido10.Domain.Dto;

namespace Pedido10.Application.Contract
{
    public interface IAuthService
    {
        string GenerateJwtToken(UsuarioDto usuarioDto);
    }
}
