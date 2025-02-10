using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Domain.Entity
{
    public class Forma_PGTO
    {
        [Key]
        public int? ID_Forma_PGTO { get; set; }

        [MaxLength(15)]
        public string Desc_Forma_PGTO { get; set; }
    }
}