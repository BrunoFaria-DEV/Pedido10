using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Contract
{
    public interface IUsuarioRepository
    {
        Task<bool> EmailExists(string email);
        Task<bool> UpdateEmailExists(string email, int id);
        Task<List<Usuario>> GetAll();
        Task<bool> Add(Usuario dto);
        Task<Usuario> Find(int id);
        Task<Usuario> FindByEmail(string email);
        Task<bool> Update(Usuario usuario);
        Task<bool> Delete(Usuario usuario);
    }
}
