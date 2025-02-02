using Pedido10.Domain.Dto;
using Pedido10.Shared.Results.Repository;

namespace Pedido10.Application.Contract
{
    public interface IProdutoService
    {
        Task<List<ProdutoDto>> GetAll();
        Task<OperationResultGeneric<ProdutoDto>> Find(int id);
        Task<OperationResultGeneric<ProdutoDto>> FindByName(string name);
        Task<OperationResultGeneric<ProdutoDto>> Add(ProdutoDto produtoDto);
        Task<OperationResultGeneric<ProdutoDto>> Update(int id, ProdutoDto produtoDto);
        Task<OperationResultGeneric<ProdutoDto>> Delete(int id);
    }
}
