using System.ComponentModel.DataAnnotations;
using Pedido10.Domain.Dto;

namespace Pedido10.Domain.Entity
{
    public class Cliente
    {
        public Cliente() { }

        public Cliente(int? iD_Cliente, 
                        Boolean tipo_Pessoa,
                        string nome,
                        string? cPF, 
                        string? cNPJ, 
                        DateOnly? nascimento, 
                        string email, 
                        string? telefone, 
                        string endereco, 
                        string? localizador, 
                        int iD_Cidade)
        {
            ID_Cliente = iD_Cliente;
            Tipo_Pessoa = tipo_Pessoa;
            Nome = nome;
            CPF = cPF ?? string.Empty;
            CNPJ = cNPJ ?? string.Empty;
            Nascimento = nascimento;
            Email = email;
            Telefone = telefone ?? string.Empty;
            Endereco = endereco;
            Localizador = localizador ?? null;
            ID_Cidade = iD_Cidade;
        }

        public Cliente(ClienteDto clienteDto)
        {
            Tipo_Pessoa = clienteDto.Tipo_Pessoa;
            Nome = clienteDto.Nome;
            CPF = clienteDto.CPF;
            CNPJ = clienteDto.CNPJ;
            Email = clienteDto.Email;
            Endereco = clienteDto.Endereco;
            Telefone = clienteDto.Telefone;
            Nascimento = clienteDto.Nascimento;
            Localizador = clienteDto.Localizador;
            ID_Cidade = clienteDto.ID_Cidade;
        }

        [Key]
        public int? ID_Cliente { get; set; }
        public Boolean Tipo_Pessoa { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
        [MaxLength(14)]
        public string? CPF { get; set; } = string.Empty;
        [MaxLength(18)]
        public string? CNPJ { get; set; } = string.Empty;
        public DateOnly? Nascimento { get; set; }
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(15)]
        public string Telefone { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Endereco { get; set; } = string.Empty;
        private string? _localizador;
        [MaxLength(255)]
        public string? Localizador
        {
            get => _localizador;
            set => _localizador = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public int? ID_Cidade { get; set; }
    }
}
