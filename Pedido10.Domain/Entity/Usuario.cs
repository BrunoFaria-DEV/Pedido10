using Pedido10.Domain.Dto;
using System.ComponentModel.DataAnnotations;

namespace Pedido10.Domain.Entity
{
    public class Usuario
    {
        public Usuario(){}

        public Usuario(string? nome, string email, string senha, char? planoUsuario, char? status)
        {
            Nome = nome ?? "";
            Email = email;
            Senha = senha;
            Plano_Usuario = planoUsuario ?? 'D';
            Status = status ?? 'D';
        }

        [Key]
        public int? ID_Usuario { get; set; }

        [MaxLength(120)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(120)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Senha { get; set; } = string.Empty;
        [MaxLength(1)]
        public char Plano_Usuario { get; set; } = 'D';
        [MaxLength(1)]
        public char Status { get; set; } = 'D';
        [MaxLength(14)]
        public string Tipo_Usuario { get; set; } = "usuario";

        public void AtualizarAtributos(UsuarioDto usuarioDto)
        {
            Nome = usuarioDto.Nome ?? Nome;
            Email = usuarioDto.Email ?? Email;
            Plano_Usuario = usuarioDto.Plano_Usuario ?? Plano_Usuario;
            Status = usuarioDto.Status ?? Status;
        }

        public void AtualizarSenha(string senha)
        {
            Senha = senha;
        }

        public bool Validar()
        {
            if (Nome.Length > 120) return false;
            if (Email.Length > 120) return false;
            
            return true;
        }
    }
}
