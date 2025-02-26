using Microsoft.EntityFrameworkCore;
using Pedido10.Data.Context;
using Pedido10.Data.Contract;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly Pedido10Context _context;

        public PedidoRepository(Pedido10Context context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetAll()
        {
            var pedidos = await _context.Pedido
                .Include(p => p.Pedido_Produtos)
                    .ThenInclude(pp => pp.Produto)
                .Include(p => p.Parcelas)
                    .ThenInclude(pa => pa.Forma_PGTO)
                .ToListAsync();

            return pedidos;
        }

        public async Task<Pedido> AdicionarPedidoAsync(Pedido pedido)
        {
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<bool> Delete(int id)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Pedido_Produtos)
                .Include(p => p.Parcelas)
                .FirstOrDefaultAsync(p => p.ID_Pedido == id);

            if (pedido == null)
                return false;

            // Remove as relações antes de excluir o pedido
            _context.Pedido_Produto.RemoveRange(pedido.Pedido_Produtos);
            _context.Parcela.RemoveRange(pedido.Parcelas);

            // Remove o pedido
            _context.Pedido.Remove(pedido);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
