using Pedido10.Domain.Entity;

namespace Pedido10.Data.Contract
{
    public interface IClienteRepository
    {
        public Task<List<Cliente>> GetAll();

        public Task<Cliente> Find(int id);
        public Task<Cliente> FindByEmail(string email);
        public Task<bool> Add(Cliente cliente);
        public Task<bool> Update(Cliente cliente);
        public Task<bool> Delete(Cliente cliente);
    }
}
