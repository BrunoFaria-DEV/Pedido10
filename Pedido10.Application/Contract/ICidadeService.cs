using Pedido10.Domain.Dto;

namespace Pedido10.Application.Contract
{
    public interface ICidadeService
    {
        Task<List<CidadeDto>?> GetAll();
    }
}
