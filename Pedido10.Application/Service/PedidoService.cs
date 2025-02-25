using Pedido10.Data.Repository;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;

namespace Pedido10.Application.Service
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoService(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido> CriarPedidoAsync(PedidoCreateDto dto)
        {
            var pedido = new Pedido
            {
                ID_Cliente = dto.ID_Cliente,
                VLR_Total_Pedido = dto.VLR_Total_Pedido,
                Status_Pedido = 'A', // A = Aberto
                DT_Pedido = DateOnly.FromDateTime(DateTime.UtcNow),
                Pago = 'N', // N = Não Pago
                Pedido_Produtos = dto.Pedido_Produtos.Select(p => new Pedido_Produto
                {
                    ID_Produto = p.ID_Produto,
                    QTDE_Produto = p.QTDE_Produto,
                    VLR_Unitario_Produto = p.VLR_Unitario_Produto,
                    VLR_Total_Produto = p.VLR_Total_Produto
                }).ToList(),
                Parcelas = dto.Parcelas.Select(p => new Parcela
                {
                    Numero_Parcela = p.Numero_Parcela,
                    DT_Vencimento = p.DT_Vencimento,
                    ID_Forma_PGTO = p.ID_Forma_PGTO,
                    Valor_Parcela = p.Valor_Parcela,
                    Status_Parcela = p.Status_Parcela,
                    Valor_Pago_Parcela = p.Valor_Pago_Parcela,
                    Data_Pagamento = p.Data_Pagamento
                }).ToList()
            };

            return await _pedidoRepository.AdicionarPedidoAsync(pedido);
        }
    }
}
