using Microsoft.EntityFrameworkCore;
using Pedido10.Data.Context;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Repository
{
    public class PedidoRepository
    {
        private readonly Pedido10Context _context;

        public PedidoRepository(Pedido10Context context)
        {
            _context = context;
        }

        public async Task<Pedido> AdicionarPedidoAsync(Pedido pedido)
        {
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }
    }
}
