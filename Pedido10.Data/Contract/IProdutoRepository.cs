using Pedido10.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Data.Contract
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> GetAll();
        Task<Produto> Find(int id);
        Task<Produto> FindByName(string nome);
        Task<bool> Add(Produto produto);
        Task<bool> Update(Produto produto);
        Task<bool> Delete(Produto produto);
    }
}
