using Pedido10.Domain.Dto;
using Pedido10.Shared.Results.Repository;

namespace Pedido10.Application.Contract
{
    public interface IClienteService
    {
        public Task<List<ClienteDto>?> GetAll();
        public Task<OperationResultGeneric<ClienteDto>> Find(int id);
        public Task<OperationResultGeneric<ClienteDto>> FindByEmail(string email);
        public Task<OperationResultGeneric<ClienteDto>> Add(ClienteDto clienteDto);
        public Task<OperationResultGeneric<ClienteDto>> Update(int id, ClienteDto clienteDto);
        public Task<OperationResultGeneric<ClienteDto>> Delete(int id);
    }
}
