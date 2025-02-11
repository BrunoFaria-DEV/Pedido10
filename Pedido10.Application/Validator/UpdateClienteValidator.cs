using FluentValidation;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;

namespace Pedido10.Application.Validator
{
    public class UpdateClienteValidator : AbstractValidator<ClienteDto>
    {
        private readonly IClienteRepository _repository;

        public UpdateClienteValidator(IClienteRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.ID_Cliente)
                .NotEmpty().WithMessage("Oops, temos um problema. tente novamente mais tarde, se o problema persistir contate o suporte: id faltando.");

            RuleFor(c => c.Tipo_Pessoa)
                .Must(tipo => tipo == true || tipo == false)
                .WithMessage("O tipo do cliente é obrigatório.");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .Length(3, 120).WithMessage("O nome do cliente de ter pelo menos 3 caracteres e no máximo 120.");

            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("O CPF do cliente é obrigatório.")
                .Must(c => !string.IsNullOrWhiteSpace(c))
                .Must(c => FormatCnpjCpf.SemFormatacao(c).Length == 11).WithMessage("O CPF deve conter exatamente 11 dígitos numéricos.")
                .Must(c => CpfCnpjUtils.IsValid(FormatCnpjCpf.SemFormatacao(c))).WithMessage("O CPF informado é inválido.")
                .When(c => c.Tipo_Pessoa == true);

            RuleFor(c => c.CNPJ)
                .NotEmpty().WithMessage("O CNPJ do cliente é obrigatório.")
                .Must(c => !string.IsNullOrWhiteSpace(c))
                .Must(c => FormatCnpjCpf.SemFormatacao(c).Length == 14).WithMessage("O CNPJ deve conter exatamente 14 dígitos numéricos.")
                .Must(c => CpfCnpjUtils.IsValid(FormatCnpjCpf.SemFormatacao(c))).WithMessage("O CNPJ informado é inválido.")
                .When(c => c.Tipo_Pessoa == false);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.")
                .MustAsync(async (dto, email, cancellation) =>
                {
                    bool exists = await _repository.UpdateEmailExists(email, (int)dto.ID_Cliente);
                    dto.ID_Cliente = null;

                    return !exists;
                }).WithMessage("O email já foi cadastrado.");
        }

        public int id { get; set; }
    }
}
