using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Domain.Dto
{
    public class ProdutoDto
    {
        public int? ID_Produto { get; set; }
        public string Nome_Produto { get; set; }
        public string? Descricao { get; set; }
        public decimal? Custo_Producao { get; set; }
        public decimal? Margem_Lucro { get; set; }
        public decimal Preco { get; set; }
        public int QTDE_Estoque { get; set; }
    }
}
