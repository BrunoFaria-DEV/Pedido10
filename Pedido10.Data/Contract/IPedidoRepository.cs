using Pedido10.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Data.Contract
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> GetAll();
        Task<Pedido> AdicionarPedidoAsync(Pedido pedido);
        Task<bool> Delete(int id);
    }
}
