namespace YourCloset.Application.Features.Ventas.DTOs;

public class VentaResponseDto {
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string NombreCliente { get; set; } = string.Empty; // Extraído de la relación
    public int PrendaId { get; set; }
    public string NombrePrenda { get; set; } = string.Empty;  // Extraído de la relación
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
    public DateTime FechaVenta { get; set; }
}