using FluentValidation;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;

namespace Pedido10.Application.Validator
{
    public class CreateProdutoValidator : AbstractValidator<ProdutoDto>
    {
        private readonly IProdutoRepository _repository;
        public CreateProdutoValidator(IProdutoRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.Nome_Produto)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .Length(3, 50).WithMessage("O nome deve ter pelo menos 3 caracteres e no máximo 50");

            RuleFor(c => c.Descricao)
                .MaximumLength(150).WithMessage("A descrição deve ter no máximo 150 caracteres.");

            RuleFor(c => c.Custo_Producao)
                .GreaterThanOrEqualTo(0).WithMessage("O preço não pode ser negativo.")
                //.LessThanOrEqualTo(99999.99m).WithMessage("O preço deve ser no máximo 99.999,99.")  avaliando se é realmente nescessário
                .PrecisionScale(7, 2, false).WithMessage("O preço deve ter no máximo 7 dígitos e 2 casas decimais.");


            RuleFor(c => c.Margem_Lucro)
                .GreaterThanOrEqualTo(0).WithMessage("O preço não pode ser negativo.")
                //.LessThanOrEqualTo(99999.99m).WithMessage("O preço deve ser no máximo 99.999,99.")  avaliando se é realmente nescessário
                .PrecisionScale(7, 2, false).WithMessage("O preço deve ter no máximo 7 dígitos e 2 casas decimais.");


            RuleFor(c => c.Preco)
                .NotEmpty().WithMessage("O preço do produto é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O preço não pode ser negativo.")
                //.LessThanOrEqualTo(99999.99m).WithMessage("O preço deve ser no máximo 99.999,99.")  avaliando se é realmente nescessário
                .PrecisionScale(7, 2, false).WithMessage("O preço deve ter no máximo 7 dígitos e 2 casas decimais.");
        }
    }
}
