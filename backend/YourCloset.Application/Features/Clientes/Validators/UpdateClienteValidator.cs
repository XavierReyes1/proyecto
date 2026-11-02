using FluentValidation;
using YourCloset.Application.Features.Clientes.DTOs;

namespace YourCloset.Application.Features.Clientes.Validators;

public class UpdateClienteValidator : AbstractValidator<UpdateClienteRequestDto> {
    public UpdateClienteValidator() {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio en la actualización.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.");
    }
}