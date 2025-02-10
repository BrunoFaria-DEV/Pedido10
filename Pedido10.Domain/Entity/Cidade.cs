using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedido10.Domain.Entity
{
    public class Cidade
    {
        public Cidade() { }

        [Key]
        public int? ID_Cidade {  get; set; }
        [MaxLength(120)]
        public string Nome_Cidade { get; set; }
        [MaxLength(2)]
        public char UF { get; set; }
        public int Codigo_IBGE { get; set; }
    }
}