using FluentValidation;
using YourCloset.Application.Features.Ventas.DTOs;

namespace YourCloset.Application.Features.Ventas.Validators;

public class UpdateVentaValidator : AbstractValidator<UpdateVentaRequestDto> {
    public UpdateVentaValidator() {
        RuleFor(x => x.ClienteId).GreaterThan(0).WithMessage("Debe seleccionar un cliente válido.");
        RuleFor(x => x.PrendaId).GreaterThan(0).WithMessage("Debe seleccionar una prenda válida.");
        RuleFor(x => x.Cantidad).GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");
        RuleFor(x => x.Total).GreaterThan(0).WithMessage("El total debe ser mayor a cero.");
    }
}