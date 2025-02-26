using Microsoft.AspNetCore.Mvc.RazorPages;
using Pedido10.Application.Contract;
using Pedido10.Data.Contract;
using Pedido10.Data.Repository;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Pedido10.Shared.Results.Repository;

namespace Pedido10.Application.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoService(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<PedidoDto>?> GetAll()
        {
            var pedidos = await _pedidoRepository.GetAll();
            if (pedidos.Count < 1 || !pedidos.Any())
            {
                return null;
            }

            List<PedidoDto> listPedidoDto = [];

            foreach (var pedido in pedidos)
            {
                listPedidoDto.Add(new PedidoDto()
                {
                    ID_Pedido = pedido.ID_Pedido,
                    VLR_Total_Pedido = pedido.VLR_Total_Pedido,
                    Status_Entrega_Pedido = pedido.Status_Entrega_Pedido,
                    DT_Pedido = pedido.DT_Pedido,
                    DT_Entrega = pedido.DT_Entrega,
                    Hora_Entrega = pedido.Hora_Entrega,
                    ID_Cliente = pedido.ID_Cliente,
                    Pedido_Produtos = pedido.Pedido_Produtos.Select(pp => new PedidoProdutoDto
                    {
                        ID_Produto = pp.Produto.ID_Produto,
                    }).ToList(),
                    Parcelas = pedido.Parcelas.Select(pa => new ParcelaDto
                    {
                        ID_Parcela = pa.ID_Parcela,
                        Valor_Parcela = pa.Valor_Parcela
                    }).ToList(),
                });
            }

            return listPedidoDto;
        }

        public async Task<Pedido> CriarPedidoAsync(PedidoCreateDto dto)
        {

            decimal calculoTotal = 0;
            int parcelasPagas = 0;
            var pedido = new Pedido
            {
                ID_Cliente = dto.ID_Cliente,
                DT_Pedido = DateOnly.FromDateTime(DateTime.UtcNow),
                DT_Entrega = dto.DT_Entrega,
                Status_Entrega_Pedido = dto.Status_Entrega_Pedido,
                Pago = 'D',
                Pedido_Produtos = dto.Pedido_Produtos.Select(p => new Pedido_Produto
                {
                    ID_Produto = (int)p.ID_Produto,
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

            foreach (var parcela in pedido.Parcelas)
            {
                if (parcela.Valor_Pago_Parcela != null || parcela.Valor_Pago_Parcela != 0) 
                { 
                    parcelasPagas++;
                }
                calculoTotal += parcela.Valor_Parcela;
            }
            if (pedido.Parcelas.Count == parcelasPagas)
            {
                pedido.Pago = 'P';
            }
            
            return await _pedidoRepository.AdicionarPedidoAsync(pedido);
        }

        public async Task<bool> Delete(int id)
        {
            return await _pedidoRepository.Delete(id);
        }
    }
}