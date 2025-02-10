using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Pedido10.Shared.Results.Repository;

namespace Pedido10.Application.Contract
{
    public interface IUsuarioService
    {
        Task<UsuarioDto?> Login(UsuarioDto usuarioDto);
        Task<List<UsuarioDto>> GetAll();
        Task<OperationResultGeneric<UsuarioDto>> Find(int id);
        Task<OperationResultGeneric<UsuarioDto>> Add(UsuarioDto dto);
        Task<OperationResultGeneric<UsuarioDto>> Update(int usuarioId, UsuarioDto dto);
        Task<OperationResultGeneric<UsuarioDto>> Delete(int id);
    }
}