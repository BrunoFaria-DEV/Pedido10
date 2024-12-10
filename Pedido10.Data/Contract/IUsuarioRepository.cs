using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Contract
{
    public interface IUsuarioRepository
    {
        Task<bool> Add(Usuario dto);
        Task<Usuario> Find(int id);
        Task<Usuario> FindByEmail(string email);
        Task<bool> Update(Usuario usuario);

        // a Entidade Paginação é uma tabela também?
        //Task<List<UsuarioDto>> Paginacao(Paginacao paginacao);
    }
}
