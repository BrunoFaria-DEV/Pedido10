using Microsoft.EntityFrameworkCore;
using Pedido10.Data.Context;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;
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
                .OrderByDescending(p => p.ID_Pedido)
                .ToListAsync();

            return pedidos;
        }

        public async Task<Pedido> Find(int id)
        {
            var pedido = await _context.Pedido
                 .Include(p => p.Pedido_Produtos)
                     .ThenInclude(pp => pp.Produto)
                 .Include(p => p.Parcelas)
                     .ThenInclude(pa => pa.Forma_PGTO)
                 .FirstOrDefaultAsync(p => p.ID_Pedido == id);
            return pedido;
        }

        public async Task<Pedido> AdicionarPedidoAsync(Pedido pedido)
        {
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task AtualizarProdutosEPacelas(Pedido pedido, PedidoCreateDto dto)
        {
            // Remove os Pedido_Produtos antigos
            _context.Pedido_Produto.RemoveRange(pedido.Pedido_Produtos);

            // Remove as Parcelas antigas
            _context.Parcela.RemoveRange(pedido.Parcelas);

            // Adiciona os novos produtos
            pedido.Pedido_Produtos = dto.Pedido_Produtos.Select(p => new Pedido_Produto
            {
                ID_Produto = (int)p.ID_Produto,
                QTDE_Produto = p.QTDE_Produto,
                VLR_Unitario_Produto = p.VLR_Unitario_Produto,
                VLR_Total_Produto = p.VLR_Total_Produto
            }).ToList();

            // Adiciona as novas parcelas
            pedido.Parcelas = dto.Parcelas.Select(p => new Parcela
            {
                Numero_Parcela = p.Numero_Parcela,
                DT_Vencimento = p.DT_Vencimento,
                ID_Forma_PGTO = p.ID_Forma_PGTO,
                Valor_Parcela = p.Valor_Parcela,
                Status_Parcela = p.Status_Parcela,
                Valor_Pago_Parcela = p.Valor_Pago_Parcela,
                Data_Pagamento = p.Data_Pagamento
            }).ToList();

            // Recalcula status do pagamento
            int parcelasPagas = pedido.Parcelas.Count(p => p.Valor_Pago_Parcela != null && p.Valor_Pago_Parcela > 0);
            pedido.Pago = (parcelasPagas == pedido.Parcelas.Count) ? 'P' : 'D';

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Pedido_Produtos)
                .Include(p => p.Parcelas)
                .FirstOrDefaultAsync(p => p.ID_Pedido == id);

            if (pedido == null)
                return false;

            _context.Pedido_Produto.RemoveRange(pedido.Pedido_Produtos);
            _context.Parcela.RemoveRange(pedido.Parcelas);

            _context.Pedido.Remove(pedido);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
