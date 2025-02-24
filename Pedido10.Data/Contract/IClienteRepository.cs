using Microsoft.EntityFrameworkCore;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Contract
{
    public interface IClienteRepository
    {
        public Task<List<Cliente>> GetAll();

        Task<Cliente> Find(int id);
        Task<Cliente> FindByEmail(string email);
        Task<bool> Add(Cliente cliente);
        Task<bool> Update(Cliente cliente);
        Task<bool> Delete(Cliente cliente);
        Task<bool> EmailExists(string email);
        Task<bool> UpdateEmailExists(int id, string email);
        Task<bool> CpfExists(string cpf);
        Task<bool> UpdateCpfExists(int id, string cpf);
        Task<bool> CnpjExists(string cnpj);
        Task<bool> UpdateCnpjExists(int id, string cnpj);
    }
}
