namespace YourCloset.Application.Features.Prendas.DTOs;

public class UpdatePrendaRequestDto {
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Talla { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}