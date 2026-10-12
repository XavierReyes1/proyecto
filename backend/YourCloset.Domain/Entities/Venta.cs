namespace YourCloset.Domain.Entities;

public class Venta {
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int PrendaId { get; set; }
    public int Cantidad { get; set; } = 1;
    public DateTime FechaVenta { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }

    // Navegación
    public Cliente Cliente { get; set; } = null!;
    public Prenda Prenda { get; set; } = null!;
}