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

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .Length(3, 100).WithMessage("O nome do cliente de ter pelo menos 3 caracteres e no máximo 100.");

            RuleFor(c => c.Tipo_Pessoa)
                .Must(tipo => tipo == true || tipo == false)
                .WithMessage("O tipo do cliente é obrigatório.");


            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("O CPF do cliente é obrigatório.")
                .Length(11).WithMessage("O CPF deve conter exatamente 11 dígitos numéricos.")
                //.Must(c => CpfCnpjUtils.IsValid(FormatCnpjCpf.SemFormatacao(c))).WithMessage("O CPF informado é inválido.")
                .MustAsync(async (dto, cpf, cancellation) =>
                {
                    bool exists = await _repository.UpdateCpfExists((int)dto.ID_Cliente, cpf);
                    return !exists;
                }).WithMessage("O cpf já foi cadastrado.")
                .When(c => c.Tipo_Pessoa == true);

            RuleFor(c => c.CNPJ)
                .NotEmpty().WithMessage("O CNPJ do cliente é obrigatório.")
                .Length(14).WithMessage("O CNPJ deve conter exatamente 14 dígitos numéricos.")
                //.Must(c => CpfCnpjUtils.IsValid(FormatCnpjCpf.SemFormatacao(c))).WithMessage("O CNPJ informado é inválido.")
                .MustAsync(async (dto, cnpj, cancellation) =>
                {
                    bool exists = await _repository.UpdateCnpjExists((int)dto.ID_Cliente, cnpj);
                    return !exists;
                }).WithMessage("O cnpj já foi cadastrado.")
                .When(c => c.Tipo_Pessoa == false);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.")
                .MaximumLength(50).WithMessage("O e-mail deve conter até no máximo 50 caracteres.")
                .MustAsync(async (dto, email, cancellation) =>
                {
                    bool exists = await _repository.UpdateEmailExists((int)dto.ID_Cliente, email);
                    dto.ID_Cliente = null;

                    return !exists;
                }).WithMessage("O email já foi cadastrado.");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .MinimumLength(10).WithMessage("O telefone deve conter pelo menos 10 caracteres.")
                .MaximumLength(15).WithMessage("O telefone deve conter até no máximo 15 caracteres.");

            RuleFor(c => c.Endereco)
                .NotEmpty().WithMessage("O endereço é obrigatório.")
                .MaximumLength(150).WithMessage("O Endereço deve conter até no máximo 150 caracteres.");

            RuleFor(c => c.Localizador)
                .MaximumLength(255).WithMessage("O atributo Localizador deve conter até no máximo 255 caracteres.");
        }
    }
}
