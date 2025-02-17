using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Shared.Results.Http
{
    public class OperationResultResponse
    {
        public string? status { get; set; } = string.Empty;
        public string? type { get; set; } = string.Empty;
        public string? message { get; set; }
 
        //type = "https://tools.ietf.org/html/rfc9110#section-15.5.1", 
        //                title = "Bad Request", 
        //                status = 400, 
        //                mensagem = "Dados incorretos",
        //                errors = validatorErrors
    }
}
