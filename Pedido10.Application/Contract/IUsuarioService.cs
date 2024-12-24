using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;

namespace Pedido10.Application.Contract
{
    public interface IUsuarioService
    {
        Task<UsuarioDto?> Login(UsuarioDto usuarioDto);
        Task<List<UsuarioDto>> GetAll();
        Task<UsuarioDto> Find(int id);
        Task<bool> Add(UsuarioDto dto);
        Task<bool> Update(int usuarioId, UsuarioDto dto);
    }
}
