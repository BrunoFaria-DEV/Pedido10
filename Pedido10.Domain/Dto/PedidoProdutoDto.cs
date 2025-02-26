namespace Pedido10.Domain.Dto
{
    public class PedidoProdutoDto
    {
        public int? ID_Produto { get; set; }
        public int QTDE_Produto { get; set; }
        public decimal VLR_Unitario_Produto { get; set; }
        public decimal VLR_Total_Produto { get; set; }
    }
}