using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Domain.Dto
{
    internal class CidadeDto
    {
        public int? ID_Cidade { get; set; }
        public string Nome_Cidade { get; set; }
        public char UF { get; set; }
        public int Codigo_IBGE { get; set; }
    }
}