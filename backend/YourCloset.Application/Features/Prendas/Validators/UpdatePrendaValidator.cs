using FluentValidation;
using YourCloset.Application.Features.Prendas.DTOs;

namespace YourCloset.Application.Features.Prendas.Validators;

public class UpdatePrendaValidator : AbstractValidator<UpdatePrendaRequestDto> {
    public UpdatePrendaValidator() {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre de la prenda es obligatorio.");

        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
    }
}