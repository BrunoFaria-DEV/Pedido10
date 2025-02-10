using FluentValidation;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Application.Validator
{
    public class CreateUsuarioValidator : AbstractValidator<UsuarioDto>
    {
        private readonly IUsuarioRepository _repository;
        public CreateUsuarioValidator(IUsuarioRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .Length(3, 120).WithMessage("O nome deve ter pelo menos 3 caracteres e no máximo 120");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.")
                .MustAsync(async (email, cancellation) =>
                {
                    bool exists = await _repository.EmailExists(email);
                    return !exists;
                }).WithMessage("O email já foi cadastrado.");

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .Length(8, 32).WithMessage("A senha deve ter pelo menos 3 caracteres e no máximo 32");
        }
    }
}
