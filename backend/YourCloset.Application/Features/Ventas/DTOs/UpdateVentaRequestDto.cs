namespace YourCloset.Application.Features.Ventas.DTOs;

public class UpdateVentaRequestDto {
    public int ClienteId { get; set; }
    public int PrendaId { get; set; }
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
}