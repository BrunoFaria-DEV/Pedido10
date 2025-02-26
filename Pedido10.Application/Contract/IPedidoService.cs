using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;

namespace Pedido10.Application.Contract
{
    public interface IPedidoService
    {
        Task<List<PedidoDto>?> GetAll();
        Task<Pedido> CriarPedidoAsync(PedidoCreateDto dto);
        Task<bool> Delete(int id);
    }
}
