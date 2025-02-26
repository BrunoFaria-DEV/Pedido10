using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pedido10.Domain.Entity
{
    public class Parcela
    {
        [Key]
        public int? ID_Parcela { get; set; }

        [MaxLength(1)]
        public char Status_Parcela { get; set; }

        public DateOnly? Data_Pagamento { get; set; }

        public DateOnly DT_Vencimento { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal Valor_Parcela { get; set; }

        public int Numero_Parcela { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal? Valor_Pago_Parcela { get; set; }

        // Relação com Pedido
        [ForeignKey("Pedido")]
        public int ID_Pedido { get; set; }
        [JsonIgnore]
        public Pedido Pedido { get; set; }

        // Relação com Forma_PGTO
        [ForeignKey("Forma_PGTO")]
        public int ID_Forma_PGTO { get; set; }
        public Forma_PGTO? Forma_PGTO { get; set; }
    }
}