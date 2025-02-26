using System.ComponentModel.DataAnnotations;

namespace Pedido10.Domain.Dto
{
    public class PedidoDto
    {
        [Required]
        public int? ID_Pedido { get; set; }

        public string? Observacao { get; set; }

        [Required]
        public decimal VLR_Total_Pedido { get; set; }

        public char Status_Entrega_Pedido { get; set; }

        public DateOnly DT_Pedido { get; set; }

        public DateOnly? DT_Entrega { get; set; }

        public DateTime? Hora_Entrega { get; set; }

        // Relacionamento com Cliente
        public int? ID_Cliente { get; set; }

        // Relacionamento com Pedido_Produto
        public List<PedidoProdutoDto> Pedido_Produtos { get; set; } = new();

        // Relacionamento com Parcela
        public List<ParcelaDto> Parcelas { get; set; } = new();
    }
}