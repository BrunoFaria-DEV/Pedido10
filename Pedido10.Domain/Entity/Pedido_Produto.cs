using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pedido10.Domain.Entity
{
    public class Pedido_Produto
    {
        [Key]
        public int? ID_Pedido_Produto { get; set; }
        [ForeignKey("Pedido")]
        public int ID_Pedido { get; set; }
        [JsonIgnore]
        public Pedido Pedido { get; set; }
        [ForeignKey("Produto")]
        public int ID_Produto { get; set; }
        public Produto? Produto { get; set; }

        public int QTDE_Produto { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal VLR_Unitario_Produto { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal VLR_Total_Produto { get; set; }
    }
}