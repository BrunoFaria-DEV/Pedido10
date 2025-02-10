using System.ComponentModel.DataAnnotations;

namespace Pedido10.Domain.Dto
{
    public class UsuarioDto
    {
        public int? ID_Usuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public char? Plano_Usuario { get; set; }
        public char? Status { get; set; }
        public string? Tipo_Usuario { get; set; }
    }
}
