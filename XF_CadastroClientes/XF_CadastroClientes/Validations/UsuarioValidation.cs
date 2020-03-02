using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using XF_CadastroClientes.Models;

namespace XF_CadastroClientes.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {


        public UsuarioValidation()
        {
            UsuarioValid();
        }

        void UsuarioValid()
        {
            RuleFor(x => x.Login)
                .NotNull()
                .MaximumLength(100)
                .MinimumLength(2)
                .WithMessage("O nome de login fornecido é inválido.");

            RuleFor(x => x.Senha)
                .NotNull()
                .MaximumLength(100)
                .MinimumLength(2)
                .WithMessage("A senha fornecida é inválido.");
        }


    }
}
