using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Domain.Entity
{
    public class Pedido
    {
        public Pedido () { }

        [Key]
        public int? ID_Pedido { get; set; }
        [MaxLength(150)]
        public string? Observacao { get; set; }
        [Column(TypeName = "decimal(9, 2)")]
        public decimal VLR_Total_Pedido { get; set; }
        [MaxLength(1)]
        public char Status_Entrega_Pedido { get; set; }
        public DateOnly DT_Pedido { get; set; }
        public DateOnly DT_Entrega { get; set; }
        public DateTime? Hora_Entrega { get; set; }
        [MaxLength(1)]
        public char? Pago { get; set; }
        [ForeignKey("Cliente")]
        public int? ID_Cliente { get; set; }
        public Cliente Cliente { get; set; }
        [ForeignKey("Usuario")]
        public int? ID_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Pedido_Produto> Pedido_Produtos { get; set; } = new List<Pedido_Produto>();
        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();
    }
}



