using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;

namespace Pedido10.Application.Contract
{
    public interface IUsuarioService
    {
        Task<bool> Add(UsuarioDto dto);
        Task<UsuarioDto> Find(int id);
        Task<bool> Update(int usuarioId, UsuarioDto dto);
    }
}
