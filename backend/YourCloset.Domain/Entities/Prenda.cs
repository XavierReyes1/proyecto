namespace YourCloset.Domain.Entities;

public class Prenda {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Talla { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    // Navegación
    public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
}