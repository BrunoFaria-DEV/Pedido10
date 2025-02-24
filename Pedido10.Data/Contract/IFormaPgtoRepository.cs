using Pedido10.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Data.Contract
{
    public interface IFormaPgtoRepository
    {
        Task<List<Forma_PGTO>> GetAll();
        Task<Forma_PGTO> Find(int id);
        Task<Forma_PGTO> FindByName(string descFormaPgto);
        Task<bool> Add(Forma_PGTO formaPgto);
        Task<bool> Update(Forma_PGTO formaPgto);
        Task<bool> Delete(Forma_PGTO formaPgto);
    }
}
