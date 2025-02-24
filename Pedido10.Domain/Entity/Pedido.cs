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
        [Column(TypeName = "decimal(7, 2)")]
        public decimal VLR_Total_Pedido { get; set; }
        [MaxLength(1)]
        public char Status_Pedido { get; set; }
        public DateOnly DT_Pedido { get; set; }
        public DateOnly? DT_Entrega { get; set; }
        public TimeOnly? Hora_Entrega { get; set; }
        [MaxLength(1)]
        public char Pago { get; set; }

        public int ID_Cliente { get; set; }
        public Cliente Cliente { get; set; }

        public int ID_Usuario { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Pedido_Produto> Pedido_Produtos { get; set; } = new List<Pedido_Produto>();
        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();
    }
}



