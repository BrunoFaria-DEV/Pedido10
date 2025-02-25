using System.ComponentModel.DataAnnotations;

namespace Pedido10.Domain.Dto
{
    public class PedidoCreateDto
    {
        [Required]
        public int ID_Cliente { get; set; }

        [Required]
        public decimal VLR_Total_Pedido { get; set; }

        public List<PedidoProdutoDto> Pedido_Produtos { get; set; } = new();
        public List<ParcelaDto> Parcelas { get; set; } = new();
    }
}
