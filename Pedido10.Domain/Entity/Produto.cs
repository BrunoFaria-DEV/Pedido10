using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedido10.Domain.Entity
{
    public class Produto
    {
        public Produto() { }

        public Produto(int? iD_Produto, string nome_Produto, string? descricao,
            decimal? custo_Producao, decimal? margem_Lucro, decimal preco, int qTDE_Estoque) 
        {
            ID_Produto = iD_Produto;
            Nome_Produto = nome_Produto;
            Descricao = Descricao;
            Custo_Producao = custo_Producao;
            Margem_Lucro = margem_Lucro;
            Preco = preco;
            QTDE_Estoque = qTDE_Estoque;
        }

        [Key]
        public int? ID_Produto { get; set; }
        [MaxLength(50)]
        public string Nome_Produto { get; set; }
        [MaxLength(150)]
        public string? Descricao { get; set; }
        //[Precision(7, 2)]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal? Custo_Producao { get; set; }
        public decimal? Margem_Lucro { get; set; }
        public decimal Preco { get; set; }
        public int QTDE_Estoque { get; set; }
    }
}
