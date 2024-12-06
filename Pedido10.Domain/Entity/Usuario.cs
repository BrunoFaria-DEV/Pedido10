using Pedido10.Domain.Dto;
using System.ComponentModel.DataAnnotations;

namespace Pedido10.Domain.Entity
{
    public class Usuario
    {
        public Usuario(){}

        public Usuario(string nome, string email, string senha, char planoUsuario, char status)
        {
            Nome = nome;
            Senha = senha;
        }

        public int ID_Usuario { get; set; }
        [MaxLength(100)]

        public string Nome { get; set; } = string.Empty;

        public string Senha { get; set; }
        [MaxLength(60)]

        public char PlanoUsuario { get; set; }
        [MaxLength(1)]

        public char Status { get; set; }
        [MaxLength(1)]
    }
}
