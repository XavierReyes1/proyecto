using FluentValidation;
using YourCloset.Application.Features.Clientes.DTOs;

namespace YourCloset.Application.Features.Clientes.Validators;

public class CreateClienteValidator : AbstractValidator<CreateClienteRequestDto> {
    public CreateClienteValidator() {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.");
    }
}