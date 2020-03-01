using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using XF_CadastroClientes.Models;

namespace XF_CadastroClientes.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            ClienteValid();
        }
        private void ClienteValid()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(4)
                .WithMessage("O Nome informado não é valido!");   

            RuleFor(x => x.Endereco)
                .NotNull()
                .WithMessage("Endereço não pode ser nulo ou vazio.");

            RuleFor(x => x.Cidade)
                .NotNull()
                .WithMessage("Cidade não pode ser nulo ou vazio.");

            RuleFor(x => x.Estado)
                .NotNull()
                .WithMessage("Estado não pode ser nulo ou vazio.");

            RuleFor(x => x.Cep)
                .NotNull()
                .WithMessage("Cep não pode ser nulo ou vazio.");

            RuleFor(x => x.Telefone)
                .NotNull()
                .WithMessage("Telefone não pode ser nulo ou vazio.");
        }

    }
}
