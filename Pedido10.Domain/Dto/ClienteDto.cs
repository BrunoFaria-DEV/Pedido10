namespace Pedido10.Domain.Dto
{
    public class ClienteDto
    {
        public int? ID_Cliente { get; set; }
        public bool? Tipo_Pessoa { get; set; } = false;
        public string? Nome { get; set; } = string.Empty;
        public string? CPF { get; set; } = string.Empty;
        public string? CNPJ { get; set; } = string.Empty;
        public DateOnly? Nascimento { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Telefone { get; set; } = string.Empty;
        public string? Endereco { get; set; } = string.Empty;
        public string? Localizador { get; set; }
        public int? ID_Cidade { get; set; }
    }
}
