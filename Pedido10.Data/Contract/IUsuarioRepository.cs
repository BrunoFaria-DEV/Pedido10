using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Contract
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAll();
        Task<bool> Add(Usuario dto);
        Task<Usuario> Find(int id);
        Task<Usuario> FindByEmail(string email);
        Task<bool> Update(Usuario usuario);

        //Task<List<UsuarioDto>> Paginacao(Paginacao paginacao); // estudo posterior
    }
}
